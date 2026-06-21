namespace Kolokwium_APBD_2.Entities;

public class Driver
{
    public int DriverId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenceNumber { get; set; } = string.Empty;
    
    public ICollection<Delivery> Deliveries { get; set; } = [];
}