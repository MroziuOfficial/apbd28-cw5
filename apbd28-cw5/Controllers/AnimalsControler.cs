using apbd28_cw5.Database;
using Microsoft.AspNetCore.Mvc;

namespace apbd28_cw5.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly MockDb _db;

	public AnimalsController(MockDb db){
		_db = db;
	}

	[HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_db.Animals);
    }

	[HttpGet("{id}")]
	public IActionResult GetAnimal(int id){
		var animal = _db.Animals.FirstOrDefault(a => a.Id == id);
		if(animal == null)
			return NotFound();
		return Ok(animal);
	}

	[HttpPost]
	public IActionResult AddAnimal(Animal animal){
		_db.Animals.Add(animal);
		return CreatedAtAction(nameof(GetAnimal), new {id = animal.Id}, animal);
	}

	[HttpPut("{id}")]
	public IActionResult UpdateAnimal(int id, Animal animal){
		var existingAnimal = _db.Animals.FirstOrDefault(a => a.Id == id);
		if(existingAnimal == null){
			return NotFound();
		}
		existingAnimal.FirstName = animal.FirstName;
		existingAnimal.Category = animal.Category;
		existingAnimal.Weight = animal.Weight;
		existingAnimal.FurColor = animal.FurColor;
		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteAnimal(int id)
	{
		var animalToRemove = _db.Animals.FirstOrDefault(a => a.Id == id);
		if (animalToRemove == null)
		{
			return NotFound();
		}

		_db.Animals.Remove(animalToRemove);
		return NoContent();
	}
}	