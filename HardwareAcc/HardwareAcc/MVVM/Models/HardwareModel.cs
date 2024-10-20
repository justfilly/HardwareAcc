using System.ComponentModel.DataAnnotations;

namespace HardwareAcc.MVVM.Models;

public class HardwareModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(500)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(255)]
    public string InventoryNumber { get; set; }
    
    public double Price { get; set; }
    
    public int ResponsibleUserId { get; set; }
    public string ResponsibleUserLogin { get; set; }
    
    public int AudienceId { get; set; }
    public string AudienceCode { get; set; }
    
    public int StatusId { get; set; }
    public string StatusName { get; set; }
}