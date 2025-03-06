using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIBSAPI.Models
{
    [Table("DAYS_MST")]
    public class DAYS_MST
    {
        [Key]
        [StringLength(5)]
        public string emp_no { get; set; }
        public double? CL { get; set; }
        public double? SL { get; set; }
        public double? PL { get; set; }
        public double? OT_hrs { get; set; }
        public double? encash_CL { get; set; }
        public double? encash_SL { get; set; }
        public double? encash_PL { get; set; }
        public double? arrear_days { get; set; }
        public double? misc_earn1 { get; set; }
        public double? misc_earn2 { get; set; }
        public double? other_income { get; set; }
        [StringLength(6)]
        public string period { get; set; }
        public double? week_off { get; set; }
        public double? arr_CL { get; set; }
        public double? arr_SL { get; set; }
        public double? arr_PL { get; set; }
        public double? arr_OT { get; set; }
        public double? present_days { get; set; }
        public double? esi_leave { get; set; }
        public double? lw_a { get; set; }
        public double? ph { get; set; }
        [StringLength(3)]
        public string profcen_cd { get; set; }
        public double? lw_un { get; set; }
        public double? coff { get; set; }
        public double? Att_Bonus_Days { get; set; }
        public double? misc_earn3 { get; set; }
        public double? misc_earn4 { get; set; }
        public double? Conv_Allow { get; set; }
        public double? LTA { get; set; }
        public double? Medical_Allow { get; set; }
        public double? Magazine { get; set; }
        public double? Driver { get; set; }
        public double? Guest { get; set; }
        public double? Soft_Furnishing { get; set; }
        [StringLength(100)]
        public string Remark { get; set; }
        public int? half_days { get; set; }
        public double? ML { get; set; }
        public double? ARR_ML { get; set; }
        public double? earn_hours { get; set; }
        public double? qbdays { get; set; }
        public double? wsaadays { get; set; }
        public double? wsabdays { get; set; }
        public double? wsacdays { get; set; }
        public double? LD { get; set; }
    }
}
