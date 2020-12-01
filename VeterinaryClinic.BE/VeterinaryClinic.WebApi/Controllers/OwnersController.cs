using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Domain.Commands.Owners;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands.Owners;
using VeterinaryClinic.WebApi.Models.Requests;

namespace VeterinaryClinic.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OwnersController : ControllerBase
  {

    private readonly DomainContext domainContext;

    public OwnersController(DomainContext domainContext)
    {
      this.domainContext = domainContext;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreateOwnerRequestModel request)
    {
      try
      {
        if (!ModelState.IsValid)
          return BadRequest("Request state is invalid");

        var created = await new CreateOwner(domainContext)
        {
          Address = request.Address,
          Email = request.Email,
          CellNo = request.CellNo,
          Name = request.Name
        }.ExecuteAsync();

        return Ok(created);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var created = await new GetOwners(domainContext).ExecuteAsync();

        return Ok(created);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetByKey([FromRoute] int key)
    {
      try
      {
        var created = await new GetOwner(domainContext)
        {
          Key = key
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
    public async Task<IActionResult> Update(UpdateOwnerRequestModel request)
    {
      try
      {
        if (!ModelState.IsValid)
          return BadRequest("Request state is invalid");

        var created = await new UpdateOwner(domainContext)
        {
          OwnerId = request.OwnerId.GetValueOrDefault(),
          Address = request.Address,
          Email = request.Email,
          CellNo = request.CellNo,
          Name = request.Name
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
