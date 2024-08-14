namespace Core.Entities;

public class GeocodingResponse
{
    public string name { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    public string country { get; set; }
    public string state { get; set; }
}