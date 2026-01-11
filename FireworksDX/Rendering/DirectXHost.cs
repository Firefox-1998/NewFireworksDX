using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace FireworksDX.Rendering
{
    public class DirectXHost : IDisposable
    {
        public Device D3DDevice { get; private set; }
        public SwapChain SwapChain { get; private set; }
        public RenderTarget? D2DRenderTarget { get; private set; }
        public SharpDX.Direct2D1.Factory D2DFactory { get; private set; }
        public bool IsUsingHardwareAcceleration { get; private set; }

        public DirectXHost(IntPtr hwnd, int width, int height)
        {
            try
            {
                D3DDevice = new Device(SharpDX.Direct3D.DriverType.Hardware,
                                       DeviceCreationFlags.BgraSupport);
                IsUsingHardwareAcceleration = true;
            }
            catch (Exception)
            {
                IsUsingHardwareAcceleration = false;                
                D3DDevice = new Device(SharpDX.Direct3D.DriverType.Warp,
                                       DeviceCreationFlags.BgraSupport);
            }

            var desc = new SwapChainDescription
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(width, height,
                    new Rational(60, 1), Format.B8G8R8A8_UNorm),
                IsWindowed = true,
                OutputHandle = hwnd,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            var factory = new SharpDX.DXGI.Factory1();
            SwapChain = new SwapChain(factory, D3DDevice, desc);

            D2DFactory = new SharpDX.Direct2D1.Factory();
            CreateRenderTarget(width, height);

            // Log info GPU dopo l'inizializzazione
            LogGpuInfo();
        }

        private void CreateRenderTarget(int width, int height)
        {
            using Texture2D backBuffer = SharpDX.Direct3D11.Resource.FromSwapChain<Texture2D>(SwapChain, 0);
            using Surface surface = backBuffer.QueryInterface<Surface>();
            D2DRenderTarget = new RenderTarget(
                D2DFactory,
                surface,
                new RenderTargetProperties(
                    new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied)
                )
            );
        }

        public void Resize(int width, int height)
        {
            D2DRenderTarget?.Dispose();
            SwapChain.ResizeBuffers(1, width, height, Format.B8G8R8A8_UNorm, SwapChainFlags.None);
            CreateRenderTarget(width, height);
        }

        public void Dispose()
        {
            D2DRenderTarget?.Dispose();
            SwapChain?.Dispose();
            D3DDevice?.Dispose();
            D2DFactory?.Dispose();
        }

        public string GetAdapterInfo()
        {
            try
            {
                using (var dxgiDevice = D3DDevice.QueryInterface<SharpDX.DXGI.Device>())
                using (var adapter = dxgiDevice.Adapter)
                {
                    var desc = adapter.Description;
                    return $"GPU: {desc.Description.Trim()}\n" +
                           $"Memoria dedicata: {desc.DedicatedVideoMemory / 1024 / 1024} MB\n" +
                           $"Accelerazione: {(IsUsingHardwareAcceleration ? "Hardware (GPU)" : "Software (CPU)")}";
                }
            }
            catch
            {
                return $"Accelerazione: {(IsUsingHardwareAcceleration ? "Hardware" : "Software")}";
            }
        }

        private void LogGpuInfo()
        {
            try
            {
                using (var dxgiDevice = D3DDevice.QueryInterface<SharpDX.DXGI.Device>())
                using (var adapter = dxgiDevice.Adapter)
                {
                    var desc = adapter.Description;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}