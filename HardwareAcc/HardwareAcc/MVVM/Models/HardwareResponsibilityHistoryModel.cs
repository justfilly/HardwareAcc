using System;
using System.ComponentModel.DataAnnotations;

namespace HardwareAcc.MVVM.Models;

public class HardwareResponsibilityHistoryModel
{
    public int Id { get; set; }
    
    [Required]
    public int HardwareId { get; set; }
    
    [Required]
    public int ResponsibleUserId { get; set; }
    
    [StringLength(500)]
    public string Comment { get; set; }
    
    [Required]
    public DateTime ResponsibilityStartDate { get; set; }
    
    public DateTime ResponsibilityEndDate { get; set; }

}