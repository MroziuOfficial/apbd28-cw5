using apbd28_cw5.Database;
using apbd28_cw5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace apbd28_cw5.Controllers;
[ApiController]
[Route("[controller]")]
public class VisitsController : ControllerBase
{
    private readonly MockDb _db;

    public VisitsController(MockDb db)
    {
        _db = db;
    }

    [HttpGet("animal/{animalId}")]
    public IActionResult GetVisitsForAnimal(int animalId)
    {
        var visits = _db.Visits.Where(v => v.AnimalId == animalId).ToList();
        return Ok(visits);
    }

    [HttpPost]
    public IActionResult AddVisit(Visit visit)
    {
        _db.Visits.Add(visit);
        return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, visit);
    }

    [HttpGet("{id}")]
    public IActionResult GetVisit(int id)
    {
        var visit = _db.Visits.FirstOrDefault(v => v.Id == id);
        if (visit == null)
            return NotFound();

        return Ok(visit);
    }
}