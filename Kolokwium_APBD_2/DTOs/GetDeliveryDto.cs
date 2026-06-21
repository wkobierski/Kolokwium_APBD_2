namespace Kolokwium_APBD_2.DTOs;

public class GetDeliveryDto
{
    public DateTime Date { get; set; }
    public CustomerDto Customer { get; set; } = null!;
    public DriverDto Driver { get; set; } = null!;
    public IEnumerable<GetProductDto> Products { get; set; } = [];
}