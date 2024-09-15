using System.ComponentModel.DataAnnotations;

namespace HardwareAcc.Models;

public class AudienceModel
{
    public int Id { get; set; }
    
    [StringLength(60)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Code { get; set; }
}