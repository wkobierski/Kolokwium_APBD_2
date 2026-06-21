namespace Kolokwium_APBD_2.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public ICollection<Delivery> Deliveries { get; set; } = [];
    
}