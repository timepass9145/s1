using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIBSAPI.Models;
[Table("DEDUCTION")]
public class Deduction
{
    [Key]
    [StringLength(5)]
    public string emp_no { get; set; }
    [StringLength(6)]
    public string period { get; set; }
    public double? advance_amt { get; set; }
    public double? soc_contri { get; set; }
    public double? soc_loan { get; set; }
    public double? canteen { get; set; }
    public double? travel { get; set; }
    public double? HDFC_loan { get; set; }
    public double? LIC_loan { get; set; }
    public double? other_loan { get; set; }
    public double? misc_deduc1 { get; set; }
    public double? misc_deduc2 { get; set; }
    public double? itax { get; set; }
    public double? other_recovery { get; set; }
    public double? LIC_inst { get; set; }
    [StringLength(3)]
    public string profcen_cd { get; set; }
    public double? sal_recovery { get; set; }
    public double? misc_deduc3 { get; set; }
    public double? misc_deduc4 { get; set; }
    [StringLength(3)]
    public string? Remark { get; set; }
}
