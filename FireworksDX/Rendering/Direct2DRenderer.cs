using FireworksDX.Config;
using FireworksDX.Engine;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

namespace FireworksDX.Rendering
{
    public class Direct2DRenderer
    {
        private readonly DirectXHost host;

        public Direct2DRenderer(DirectXHost host)
        {
            this.host = host;
        }

        private RawColor4 ToRaw(Color c, byte alphaOverride = 255)
        {
            byte a = alphaOverride == 255 ? c.A : alphaOverride;
            return new RawColor4(c.R / 255f, c.G / 255f, c.B / 255f, a / 255f);
        }

        public void Render(FireworksEngine engine, int width, int height)
        {
            var rt = host.D2DRenderTarget;

            if (rt == null)
                return;

            rt.BeginDraw();
            rt.Clear(new RawColor4(0, 0, 0, 1));

            DrawRockets(rt, engine);
            DrawParticles(rt, engine);
            DrawSmoke(rt, engine);

            rt.EndDraw();
            host.SwapChain.Present(1, PresentFlags.None);
        }

        private void DrawRockets(RenderTarget rt, FireworksEngine engine)
        {
            foreach (var r in engine.Rockets)
            {
                using var brush = new SolidColorBrush(rt, ToRaw(r.Color));
                rt.FillEllipse(new Ellipse(
                    new RawVector2((float)r.Position.X, (float)r.Position.Y),
                    4, 8), brush);
            }
        }

        private void DrawParticles(RenderTarget rt, FireworksEngine engine)
        {
            foreach (var p in engine.Particles)
            {
                double lf = p.Life / 100.0;
                if (lf < 0) lf = 0;

                byte alpha = (byte)(80 + lf * 175);

                if (FireworksConfig.EnableTwinkle)
                {
                    p.TwinklePhase += 0.3;
                    alpha = (byte)(alpha * (0.5 + 0.5 * Math.Sin(p.TwinklePhase)));
                }

                float size = (float)(2 + 4 * lf);

                if (FireworksConfig.EnableGlow)
                {
                    using var glow = new SolidColorBrush(rt, ToRaw(p.BaseColor, (byte)(alpha / 3)));
                    rt.FillEllipse(new Ellipse(
                        new RawVector2((float)p.Position.X, (float)p.Position.Y),
                        size * 3, size * 3), glow);
                }

                using var brush = new SolidColorBrush(rt, ToRaw(p.BaseColor, alpha));
                rt.FillEllipse(new Ellipse(
                    new RawVector2((float)p.Position.X, (float)p.Position.Y),
                    size, size), brush);

                byte trailAlpha = (byte)(alpha * 0.7);
                foreach (var t in p.Trail)
                {
                    using var tb = new SolidColorBrush(rt, ToRaw(p.BaseColor, trailAlpha));
                    rt.FillEllipse(new Ellipse(
                        new RawVector2((float)t.X, (float)t.Y),
                        2, 2), tb);

                    if (trailAlpha > 15)
                        trailAlpha -= 15;
                }
            }
        }

        private void DrawSmoke(RenderTarget rt, FireworksEngine engine)
        {
            foreach (var s in engine.Smoke)
            {
                double lf = s.Life / 120.0;
                if (lf < 0) lf = 0;

                byte alpha = (byte)(40 + lf * 80);
                var c = Color.FromArgb(alpha, 180, 180, 180);

                using var brush = new SolidColorBrush(rt, ToRaw(c));
                rt.FillEllipse(new Ellipse(
                    new RawVector2((float)s.Position.X, (float)s.Position.Y),
                    (float)s.Size, (float)(s.Size * 0.7)), brush);
            }
        }
    }
}