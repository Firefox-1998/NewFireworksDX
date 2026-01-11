using FireworksDX.Engine;
using FireworksDX.Rendering;

namespace FireworksDX.WinForms
{
    public class FireworksPanel : Panel
    {
        private DirectXHost? host;
        private Direct2DRenderer? renderer;
        private FireworksEngine engine = new();
        private System.Windows.Forms.Timer timer = new();

        public string GetGpuInfo() => host?.GetAdapterInfo() ?? "DirectX non inizializzato";
        public bool IsUsingGpu => host?.IsUsingHardwareAcceleration ?? false;

        public FireworksPanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.Opaque |
                     ControlStyles.UserPaint, true);

            timer.Interval = 16; // ~60 FPS
            timer.Tick += (s, e) =>
            {
                if (Width > 0 && Height > 0)
                {
                    engine.Update(Width, Height);
                    renderer?.Render(engine, Width, Height);
                }
            };
        }

        /// <summary>
        /// Lancia una raffica di esplosioni immediate.
        /// </summary>
        /// <param name="count">Numero di esplosioni da lanciare.</param>
        public void LaunchBurst(int count)
        {
            if (count <= 0 || Width <= 0 || Height <= 0) return;

            for (int i = 0; i < count; i++)
            {
                engine.LaunchImmediateBurstRocket(Width, Height);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!DesignMode)
            {
                host = new DirectXHost(Handle, Width, Height);
                renderer = new Direct2DRenderer(host);
                timer.Start();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (host != null && Width > 0 && Height > 0)
                host.Resize(Width, Height);
        }

        protected override void Dispose(bool disposing)
        {
            timer?.Stop();
            host?.Dispose();
            base.Dispose(disposing);
        }
    }
}