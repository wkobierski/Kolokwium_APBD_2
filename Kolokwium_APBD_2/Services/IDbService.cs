using Kolokwium_APBD_2.DTOs;

namespace Kolokwium_APBD_2.Services;

public interface IDbService
{
    Task<GetDeliveryDto> GetByIdAsync(int id);
    Task<PostDeliveryDto> AddAsync(PostDeliveryDto dto);
}
