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

    public async Task<GetDeliveryDto> GetByIdAsync(int id)
    {
        var item = await _db.Deliveries
            .Where(e => e.DeliveryId == id)
            .Select(e => new GetDeliveryDto
            {
                Date = e.Date,
                Customer = new CustomerDto
                {
                    FirstName = e.Customer.FirstName,
                    LastName = e.Customer.LastName,
                    DateOfBirth = e.Customer.BirthDate
                },
                Driver = new DriverDto
                {
                    FirstName = e.Driver.FirstName,
                    LastName = e.Driver.LastName,
                    LicenceNumber = e.Driver.LicenceNumber
                },
                Products = e.ProductDeliveries.Select(r => new GetProductDto
                {
                    Name = r.Product.Name,
                    Price = r.Product.Price,
                    Amount = r.Amount
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (item == null) throw new NotFoundException($"Nie znaleziono rekordu o id={id}");
        return item;
    }

    public async Task<PostDeliveryDto> AddAsync(PostDeliveryDto dto)
    {
        var item = new Delivery
        {
            CustomerId = dto.CustomerId,
            DriverId = _db.Drivers
                .Where(e => e.LicenceNumber == dto.LicenceNumber)
                .Select(e => e.DriverId)
                .FirstOrDefault(),
            Date = DateTime.Now,
            Customer = _db.Customers.Find(dto.CustomerId)!,
            Driver = _db.Drivers
                .Where(e => e.LicenceNumber == dto.LicenceNumber)
                .Select(e => e)
                .FirstOrDefault()!,
            ProductDeliveries = dto.Products.Select(e => new ProductDelivery
            {
                ProductId = _db.Products.Where(p => p.Name == e.Name).Select(p => p.ProductId).FirstOrDefault(),
                Amount = e.Amount
            }).ToList()
        };
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
        return new PostDeliveryDto
        {
            CustomerId = item.CustomerId, 
            LicenceNumber = item.Driver.LicenceNumber,
            Products = item.ProductDeliveries.Select(e => new PostProductDto
            {
                Name = e.Product.Name,
                Amount = e.Amount
            })
        };
    }
}
