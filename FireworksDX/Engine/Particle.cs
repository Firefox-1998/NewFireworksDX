namespace FireworksDX.Engine
{
    public class Particle
    {
        public Vec2 Position;
        public Vec2 Velocity;
        public int Life;
        public Color BaseColor;
        public Queue<Vec2> Trail { get; } = new();
        public double TwinklePhase;
    }
}