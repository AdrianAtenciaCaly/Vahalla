using Microsoft.AspNetCore.Mvc;
using Valhalla.Application.Interfaces;
using Valhalla.Application.Mappers;
using Valhalla.Shared.Constants;
using Valhalla.Shared.DTOs;

namespace Valhalla.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VikingsController : ControllerBase
{
    private readonly IVikingRepository _repository;
    private readonly IVikingClassificationService _service;

    public VikingsController(
        IVikingRepository repository,
        IVikingClassificationService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _repository.GetAll().Select(VikingMapper.ToDto);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var viking = _repository.GetById(id);

        return viking is null
            ? NotFound(new { message = ErrorMessages.Vikings.NotFound })
            : Ok(VikingMapper.ToDto(viking));
    }

    [HttpPost]
    public IActionResult Post(VikingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = ErrorMessages.Vikings.InvalidData });

        if (dto.BattlesWon < 0)
            return BadRequest(new { message = ErrorMessages.Vikings.NegativeBattles });

        var viking = VikingMapper.ToEntity(dto);

        _service.Classify(viking);
        _repository.Add(viking);

        return CreatedAtAction(nameof(Get), new { id = viking.Id }, VikingMapper.ToDto(viking));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, VikingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = ErrorMessages.Vikings.InvalidData });

        var current = _repository.GetById(id);

        if (current is null)
            return NotFound(new { message = ErrorMessages.Vikings.NotFound });

        VikingMapper.ApplyChanges(current, dto);

        _service.Classify(current);
        _repository.Update(current);

        return Ok(VikingMapper.ToDto(current));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var viking = _repository.GetById(id);

        if (viking is null)
            return NotFound(new { message = ErrorMessages.Vikings.NotFound });

        _repository.Delete(id);
        return NoContent();
    }
}