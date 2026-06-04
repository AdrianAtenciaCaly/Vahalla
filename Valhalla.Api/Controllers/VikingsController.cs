using Microsoft.AspNetCore.Mvc;
using Valhalla.Application.Interfaces;
using Valhalla.Domain.Entities;
using Valhalla.Shared.DTOs;

namespace Valhalla.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VikingsController : ControllerBase
    {
        private readonly IVikingRepository _repository;
        private readonly IVikingClassificationService _service;

        public VikingsController( IVikingRepository repository,IVikingClassificationService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var viking = _repository.GetById(id);

            if (viking == null)
                return NotFound();

            return Ok(viking);
        }

        [HttpPost]
        public IActionResult Post(VikingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new{ message =  "Datos inválidos"});
            
            if (dto.BattlesWon < 0)
                return BadRequest(new{message = "Las batallas no pueden ser negativas"});
            
            var viking = new Viking
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                BattlesWon = dto.BattlesWon,
                FavoriteWeapon = dto.FavoriteWeapon,
                HonorLevel = dto.HonorLevel,

                DeathCause = dto.DeathCause
            };

            _service.Classify(viking);

            _repository.Add(viking);

            return Ok(viking);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id,VikingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var current = _repository.GetById(id);

            if (current == null)
                return NotFound();

            current.Name = dto.Name;

            current.BattlesWon = dto.BattlesWon;

            current.FavoriteWeapon =
                dto.FavoriteWeapon;

            current.HonorLevel =
                dto.HonorLevel;

            current.DeathCause =
                dto.DeathCause;

            _service.Classify(current);

            _repository.Update(current);

            return Ok(current);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _repository.Delete(id);

            return Ok();
        }
    }
}
