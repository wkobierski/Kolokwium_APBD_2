namespace Kolokwium_APBD_2.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    
    public ICollection<ProductDelivery> ProductDeliveries { get; set; } = [];
}