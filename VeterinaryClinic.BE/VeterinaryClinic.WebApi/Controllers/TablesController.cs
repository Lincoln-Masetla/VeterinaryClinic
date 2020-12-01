using System;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Domain.Commands.Tables;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Services;

namespace VeterinaryClinic.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TablesController : ControllerBase
  {

    private readonly DomainContext domainContext;

    public TablesController(DomainContext domainContext)
    {
      this.domainContext = domainContext;
    }

    [HttpPost(nameof(Create))]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult Create()
    {
      try
      {
        var created = new CreateTable(domainContext).ExecuteAsync();
        return Ok(created);
      }
      catch (Exception ex)
      {
        //TODO: log exception ...
        Debug.WriteLine(ex);
        return BadRequest();
      }
    }
  }
}
