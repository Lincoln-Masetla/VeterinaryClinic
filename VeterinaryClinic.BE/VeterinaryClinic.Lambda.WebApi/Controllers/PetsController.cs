using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Domain.Commands.Pets;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Lambda.WebApi.Models;

namespace VeterinaryClinic.Lambda.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetsController : ControllerBase
  {
    private readonly DomainContext domainContext;

    public PetsController(DomainContext domainContext)
    {
      this.domainContext = domainContext;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreatePetRequestModel request)
    {
      try
      {
        if (!ModelState.IsValid)
          return BadRequest("Request state is invalid");

        var created = await new CreatePet(domainContext)
        {
          Name = request.Name,
          BirthDate = request.BirthDate.GetValueOrDefault(),
          SpeciesType = request.SpeciesType,
          Colour = request.Colour,
          OwnerId = request.OwnerId,
          Notes = request.Notes
        }.ExecuteAsync();

        return Ok(created);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }

    [HttpGet("{ownerId}")]
    public async Task<IActionResult> GetAllPetsByOwner(int ownerId)
    {
      try
      {
        var created = await new GetPetsByOwner(domainContext)
        {
          OwnerId = ownerId
        }.ExecuteAsync();

        return Ok(created);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }

    [HttpGet("{petId}/{ownerId}")] //:( didnt get to use this method
    public async Task<IActionResult> GetPetByKey(int petId, int ownerId)
    {
      try
      {
        var created = await new GetPetByKey(domainContext)
        {
          PetId = petId,
          OwnerId = ownerId
        }.ExecuteAsync();

        return Ok(created);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }

    [HttpPut()]
    public async Task<IActionResult> Update(UpdatePetRequestModel request)
    {
      try
      {
        if (!ModelState.IsValid)
          return BadRequest("Request state is invalid");

        var created = await new UpdatePet(domainContext)
        {
          PetId = request.PetId.GetValueOrDefault(),
          Name = request.Name,
          BirthDate = request.BirthDate.GetValueOrDefault(),
          SpeciesType = request.SpeciesType,
          Colour = request.Colour,
          OwnerId = request.OwnerId.GetValueOrDefault(),
          Notes = request.Notes
        }.ExecuteAsync();

        return Ok(created);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }

    [HttpDelete("{petId}/{ownerId}")]
    public async Task<IActionResult> DeletePet(int petId, int ownerId)
    {
      try
      {
        var created = await new DeletePet(domainContext)
        {
          PetId = petId,
          OwnerId = ownerId
        }.ExecuteAsync();

        return Ok(created);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }

  }
}
