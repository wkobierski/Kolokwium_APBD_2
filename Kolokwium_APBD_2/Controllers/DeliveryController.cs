using Kolokwium_APBD_2.DTOs;
using Kolokwium_APBD_2.Exceptions;
using Kolokwium_APBD_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_APBD_2.Controllers;

public class DeliveryController : ControllerBase
{
    private readonly IDbService _db;
    public DeliveryController(IDbService db) { _db = db; }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try { return Ok(await _db.GetByIdAsync(id)); }
        catch (NotFoundException e) { return NotFound(e.Message); }
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(PostDeliveryDto dto)
    {
        var created = await _db.AddAsync(dto);
        return Created($"api/deliveries/{created.CustomerId}", created);
    }
}