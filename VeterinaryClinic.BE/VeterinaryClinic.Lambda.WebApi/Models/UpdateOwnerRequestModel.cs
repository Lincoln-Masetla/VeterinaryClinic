using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Lambda.WebApi.Models
{
  public class UpdateOwnerRequestModel
  {
    [Required]
    public int? OwnerId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string CellNo { get; set; }
    [Required]
    public string Address { get; set; }
  }
}
