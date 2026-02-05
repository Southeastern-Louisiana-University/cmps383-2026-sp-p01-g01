using Microsoft.AspNetCore.Mvc;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.Entities;
using System.Linq;

namespace Selu383.SP26.Api.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController : ControllerBase
{
    private readonly DataContext _dataContext;

    public LocationsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _dataContext.Locations
            .Select(x => new LocationGetDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                TableCount = x.TableCount
            })
            .ToList();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _dataContext.Locations
            .Select(x => new LocationGetDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                TableCount = x.TableCount
            })
            .FirstOrDefault(x => x.Id == id);

        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(LocationCreateUpdateDto dto)
    {
        var location = new Location 
        { 
            Name = dto.Name, 
            Address = dto.Address, 
            TableCount = dto.TableCount 
        };

        if (string.IsNullOrWhiteSpace(location.Name))
        {
            return BadRequest("Name cannot be empty.");
        }
        if (location.Name.Length > 100)
        {
            return BadRequest("Name is too long.");
        }
        if (location.Name.Length < 1)
        {
            return BadRequest("Name is too short.");
        }
        if (string.IsNullOrWhiteSpace(location.Address))
        {
            return BadRequest("Address cannot be empty.");
        }
        if (location.Address.Length < 3)
        {
            return BadRequest("Address is too short.");
        }
        if (location.TableCount < 1)    
        {
            return BadRequest("Table count must be at least 1.");
        }
        if (location.TableCount > 100)    
        {
            return BadRequest("Table count must not exceed 100.");
        }


        _dataContext.Locations.Add(location);
        _dataContext.SaveChanges();

        var response = new LocationGetDto
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address,
            TableCount = location.TableCount
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, LocationCreateUpdateDto dto)
    {
        var location = _dataContext.Locations.FirstOrDefault(x => x.Id == id);
        if (location == null) return NotFound();

        location.Name = dto.Name;
        location.Address = dto.Address;
        location.TableCount = dto.TableCount;

        if (string.IsNullOrWhiteSpace(location.Name))
        {
            return BadRequest("Name cannot be empty.");
        }

        if (location.Name.Length > 100)
        {
            return BadRequest("Name is too long.");
        }
        if (location.TableCount < 1)    
        {
            return BadRequest("Table count must be at least 1.");
        }

        _dataContext.SaveChanges();

        var response = new LocationGetDto
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address,
            TableCount = location.TableCount
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var location = _dataContext.Locations.FirstOrDefault(x => x.Id == id);
        if (location == null) return NotFound();

        _dataContext.Locations.Remove(location);
        _dataContext.SaveChanges();

        return Ok();
    }
}