using System.ComponentModel.DataAnnotations;

namespace TSD.Api.Models;

public class CustomerDto : BaseDto
{
    [Required]
    public string? FullName { get; set; }

    [Required]
    public int? ExternalId { get; set; }
}