using System.ComponentModel.DataAnnotations;

namespace HardwareAcc.MVVM.Models;

public class StatusModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}