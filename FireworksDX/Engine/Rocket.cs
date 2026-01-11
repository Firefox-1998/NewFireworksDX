namespace FireworksDX.Engine
{
    public class Rocket
    {
        public Vec2 Position;
        public Vec2 Velocity;
        public double TargetY { get; set; }
        public Color Color { get; set; }
        
        /// <summary>
        /// Tempo stimato di volo (in secondi) fino all'esplosione.
        /// </summary>
        public double EstimatedFlightTime { get; set; }
    }
}