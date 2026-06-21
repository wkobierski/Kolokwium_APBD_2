using Kolokwium_APBD_2.Entities;

namespace Kolokwium_APBD_2.DTOs;

public class PostDeliveryDto
{
    public int CustomerId { get; set; }
    public string LicenceNumber { get; set; } = string.Empty;
    public IEnumerable<PostProductDto> Products { get; set; } = [];
}