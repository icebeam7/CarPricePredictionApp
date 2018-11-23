namespace CarPricePredictionApp.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string BodyStyle { get; set; }
        public int WheelSize { get; set; }
        public int EngineSize { get; set; }
        public int HorsePower { get; set; }
        public int PeakRPM { get; set; }
        public int HighwayMPG { get; set; }
        public double Price { get; set; }
    }
}
