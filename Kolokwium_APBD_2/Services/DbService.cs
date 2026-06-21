using Kolokwium_APBD_2.Data;
using Kolokwium_APBD_2.DTOs;
using Kolokwium_APBD_2.Entities;
using Kolokwium_APBD_2.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_APBD_2.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _db;
    public DbService(AppDbContext db) { _db = db; }

    public async Task<SampleEntityWithDetailsDto> GetByIdAsync(int id)
    {
        var item = await _db.SampleEntities
            .Where(e => e.Id == id)
            .Select(e => new SampleEntityWithDetailsDto
            {
                Id = e.Id,
                Name = e.Name,
                // TODO: podmień RelatedItems na właściwość nawigacyjną ze swojej encji
                // np. jeśli PC ma PCComponents: e.PCComponents.Select(...)
                RelatedItems = e.RelatedItems.Select(r => new RelatedItemDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    // TODO: jeśli jest kolejny poziom zagnieżdżenia (np. producent komponentu):
                    NestedItem = new NestedRelatedItemDto
                    {
                        Id = r.NestedItem.Id,
                        Name = r.NestedItem.Name
                    }
                })
            })
            .FirstOrDefaultAsync();

        if (item == null) throw new NotFoundException($"Nie znaleziono rekordu o id={id}");
        return item;
    }

    public async Task<SampleEntityDto> AddAsync(AddSampleEntityDto dto)
    {
        var item = new SampleEntity
        {
            Name = dto.Name
            // TODO: dopisz pozostałe właściwości z dto
        };
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
        return new SampleEntityDto { Id = item.Id, Name = item.Name };
    }
}
