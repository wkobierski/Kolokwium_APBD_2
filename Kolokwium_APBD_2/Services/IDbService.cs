using Kolokwium_APBD_2.DTOs;

namespace Kolokwium_APBD_2.Services;

public interface IDbService
{
    Task<SampleEntityWithDetailsDto> GetByIdAsync(int id);                     // GET szczegóły z relacjami
    Task<SampleEntityDto> AddAsync(AddSampleEntityDto dto);                    // POST
}
