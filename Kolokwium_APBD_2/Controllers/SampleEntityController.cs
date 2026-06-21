using Kolokwium_APBD_2.DTOs;
using Kolokwium_APBD_2.Exceptions;
using Kolokwium_APBD_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_APBD_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SampleEntityController : ControllerBase
{
    private readonly IDbService _db;
    public SampleEntityController(IDbService db) { _db = db; }

    // GET /api/sampleentity/{id} — zwraca SampleEntityWithDetailsDto z zagnieżdżonymi relacjami
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try { return Ok(await _db.GetByIdAsync(id)); }
        catch (NotFoundException e) { return NotFound(e.Message); }
    }

    // POST /api/sampleentity — przyjmuje AddSampleEntityDto, zwraca SampleEntityDto z Id
    [HttpPost]
    public async Task<IActionResult> Add(AddSampleEntityDto dto)
    {
        var created = await _db.AddAsync(dto);
        return Created($"api/sampleentity/{created.Id}", created);
    }
}
