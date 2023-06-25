namespace WeatherApplication.Models
{
    public class WeatherData
    {
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;    
        public string Temperature { get; set; } = string.Empty;
        public string MinTemperature { get; set; } = string.Empty;
        public string MaxTemperature { get; set; } = string.Empty;
        public string WindSpeed { get; set; } = string.Empty;
        public string Humidity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
       

    }
}
