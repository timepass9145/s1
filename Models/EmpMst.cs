using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIBSAPI.Models
{
    [Table("emp_mst")]
    public class EmpMst
    {
        [Key]
        [StringLength(5)]
        public string EMP_NO { get; set; } // Unchecked - Not nullable

        [StringLength(3)]
        public string? dept_code { get; set; } // Checked - Nullable

        [StringLength(3)]
        public string? profit_center { get; set; }

        [StringLength(1)]
        public string? dm_indicator { get; set; }

        [StringLength(3)]
        public string? grade { get; set; }

        [StringLength(1)]
        public string? tech_category { get; set; }

        [StringLength(1)]
        public string? pf_flag { get; set; }

        [StringLength(15)]
        public string? pf_no { get; set; }

        [StringLength(1)]
        public string? esic_flag { get; set; }

        [StringLength(15)]
        public string? esic_no { get; set; }

        public double? ot_flag { get; set; }

        [StringLength(1)]
        public string? status { get; set; }

        [StringLength(1)]
        public string? temp_flag { get; set; }

        public DateTime? confirm_date { get; set; }

        public DateTime? prob_date { get; set; }

        public double? BASIC { get; set; }

        public double? FIX_BASIC { get; set; }

        public double? DA { get; set; }

        public double? VAR_DA { get; set; }

        public double? CONV_ALLOW { get; set; }

        public double? lta { get; set; }

        public double? medical_allow { get; set; }

        public double? child_edu { get; set; }

        public double? uniform { get; set; }

        public double? HRA { get; set; }

        public double? misc1 { get; set; }

        public double? misc2 { get; set; }

        public double? misc3 { get; set; }

        public double? misc4 { get; set; }

        public double? magazine { get; set; }

        public double? canteen { get; set; }

        public double? driver { get; set; }

        public double? guest { get; set; }

        public double? soft_furnishing { get; set; }

        [StringLength(6)]
        public string? bnk_code { get; set; }

        [StringLength(50)]
        public string? bnk_led_no { get; set; }

        public DateTime? left_status { get; set; }

        [StringLength(7)]
        public string period { get; set; } // Unchecked - Not nullable

        [StringLength(1)]
        public string? pay_type { get; set; }

        public double? UNION_CONTR { get; set; }

        public double? WELFARE { get; set; }

        [StringLength(3)]
        public string profcen_cd { get; set; } // Unchecked - Not nullable

        [StringLength(16)]
        public string? bnk_acc_no { get; set; }

        [StringLength(1)]
        public string? ptax_flag { get; set; }

        [StringLength(1)]
        public string? prod_inct_flag { get; set; }

        [StringLength(1)]
        public string? Var_da_flag { get; set; }

        public double? extra_pf_flag { get; set; }

        [StringLength(1)]
        public string? fpf_flag { get; set; }

        [StringLength(1)]
        public string? attn_bonus_flag { get; set; }

        public DateTime? end_temp { get; set; }

        public DateTime? start_prob { get; set; }

        public double? temp_period { get; set; }

        [StringLength(1)]
        public string? arrear_flag { get; set; }

        [StringLength(1)]
        public string? lw_flag { get; set; }

        [StringLength(50)]
        public string? approve_by { get; set; }

        public DateTime? approve_date { get; set; }

        [StringLength(1)]
        public string? approve_flag { get; set; }

        public double? misc5 { get; set; }

        public double? misc6 { get; set; }

        public double? misc7 { get; set; }

        public double? misc8 { get; set; }

        public double? misc9 { get; set; }

        [StringLength(1)]
        public string? Super_Ann_Flag { get; set; }

        public double? Slip_Total { get; set; }

        public double? Voucher_Total { get; set; }

        public double? PF { get; set; }

        public double? Bonus { get; set; }

        public double? Super_Ann { get; set; }

        public double? Gratuity { get; set; }

        public DateTime? Increment_Date { get; set; }

        public byte? Increment_Cnt { get; set; }

        public short? Incr_Percentage { get; set; }

        [StringLength(50)]
        public string? Incr_Remark { get; set; }

        public double? ESIC { get; set; }

        public DateTime? pf_date { get; set; }

        [StringLength(3)]
        public string? Work_division { get; set; }

        public DateTime? Transfer_from { get; set; }

        [StringLength(3)]
        public string? old_div { get; set; }

        public DateTime? old_trf_date { get; set; }

        [StringLength(1)]
        public string? gratuty_flag { get; set; }

        [StringLength(3)]
        public string? desigcode { get; set; }

        [StringLength(15)]
        public string? UAN_no { get; set; }

        [StringLength(1)]
        public string? ctcpf { get; set; }
    }

}
