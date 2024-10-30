using System;
using System.ComponentModel.DataAnnotations;

namespace HardwareAcc.MVVM.Models;

public class HardwareAudienceHistoryModel
{
    public int Id { get; set; }

    [Required]
    public int HardwareId { get; set; }

    [Required]
    public int AudienceId { get; set; }

    [Required]
    public DateTime TransferredDate { get; set; }

    public DateTime? RemovedDate { get; set; }

    public string AudienceCode { get; set; }
    public string RemovedDateText => RemovedDate?.ToString("dd.MM.yyyy") ?? "";
}