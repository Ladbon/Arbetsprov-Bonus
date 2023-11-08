using Arbetsprov_Bonus.Data;
using Arbetsprov_Bonus.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Arbetsprov_Bonus.Controllers;

[Route("[controller]")]
[ApiController]
public class ConsultantController : ControllerBase
{
    private readonly IConsultantRepository _consultantRepository;

    public ConsultantController(
        IConsultantRepository consultantRepository
    )
    {
        _consultantRepository = consultantRepository;
    }

    [HttpGet]
    public IEnumerable<Consultant> Get()
    {
        return _consultantRepository.Get();
    }

    [HttpGet("{id}")]
    public ActionResult<Consultant> GetById(int id)
    {
        var consultant = _consultantRepository.GetById(id);
        if (consultant is null)
        {
            return NotFound();
        }
        return consultant;
    }
    [HttpPost]
    public ActionResult<Consultant> Add([FromBody] Consultant consultant)
    {
        _consultantRepository.Add(consultant);
        return CreatedAtAction(nameof(GetById), new { id = consultant.Id }, consultant);
    }

    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {
        var existingConsultant = _consultantRepository.GetById(id);
        if (existingConsultant == null)
        {
            return NotFound();
        }
        _consultantRepository.Remove(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Consultant consultant)
    {
        if (id != consultant.Id)
        {
            return BadRequest();
        }

        var existingConsultant = _consultantRepository.GetById(id);
        if (existingConsultant == null)
        {
            return NotFound();
        }

        existingConsultant.FirstName = consultant.FirstName;
        existingConsultant.LastName = consultant.LastName;
        existingConsultant.StartDate = consultant.StartDate;

        _consultantRepository.Update(existingConsultant);
        return NoContent();
    }
}