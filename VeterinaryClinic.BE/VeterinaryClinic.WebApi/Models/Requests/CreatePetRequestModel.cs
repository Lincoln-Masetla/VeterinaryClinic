using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.WebApi.Models.Requests
{
  public class CreatePetRequestModel
  {
    [Required]
    public int OwnerId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string SpeciesType { get; set; }
    [Required]
    public DateTime? BirthDate { get; set; }
    [Required]
    public string Colour { get; set; }
    [Required]
    public string Notes { get; set; }
  }
}
