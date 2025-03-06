using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIBSAPI.Models
{
    [Table("PAY_MASTER")]
    public class PAY_MASTER
    {
        [StringLength(3)]
        public string grade { get; set; }

        [StringLength(1)]
        public string? basic { get; set; }

        [StringLength(1)]
        public string? fix_basic { get; set; }

        [StringLength(1)]
        public string? DA { get; set; }

        [StringLength(1)]
        public string? var_DA { get; set; }

        [StringLength(1)]
        public string? conv_allow { get; set; }

        [StringLength(1)]
        public string? LTA { get; set; }

        [StringLength(1)]
        public string? medical_allow { get; set; }

        [StringLength(1)]
        public string? child_edu { get; set; }

        [StringLength(1)]
        public string? uniform { get; set; }

        [StringLength(1)]
        public string? HRA { get; set; }

        [StringLength(1)]
        public string? misc1 { get; set; }

        [StringLength(1)]
        public string? misc2 { get; set; }

        [StringLength(1)]
        public string? misc3 { get; set; }

        [StringLength(1)]
        public string? misc4 { get; set; }

        [StringLength(1)]
        public string? magazine { get; set; }

        [StringLength(1)]
        public string? canteen { get; set; }

        [StringLength(1)]
        public string? driver { get; set; }

        [StringLength(1)]
        public string? guest { get; set; }

        [StringLength(1)]
        public string? soft_furnishing { get; set; }

        [StringLength(1)]
        public string? V_basic { get; set; }

        [StringLength(1)]
        public string? V_fix_basic { get; set; }

        [StringLength(1)]
        public string? V_DA { get; set; }

        [StringLength(1)]
        public string? V_var_DA { get; set; }

        [StringLength(1)]
        public string? V_conv_allow { get; set; }

        [StringLength(1)]
        public string? V_LTA { get; set; }

        [StringLength(1)]
        public string? V_medical_allow { get; set; }

        [StringLength(1)]
        public string? V_child_edu { get; set; }

        [StringLength(1)]
        public string? V_uniform { get; set; }

        [StringLength(1)]
        public string? V_HRA { get; set; }

        [StringLength(1)]
        public string? V_misc1 { get; set; }

        [StringLength(1)]
        public string? V_misc2 { get; set; }

        [StringLength(1)]
        public string? V_misc3 { get; set; }

        [StringLength(1)]
        public string? V_misc4 { get; set; }

        [StringLength(1)]
        public string? V_magazine { get; set; }

        [StringLength(1)]
        public string? V_canteen { get; set; }

        [StringLength(1)]
        public string? V_driver { get; set; }

        [StringLength(1)]
        public string? V_guest { get; set; }

        [StringLength(1)]
        public string? V_soft_furnishing { get; set; }

        [StringLength(12)]
        public string? A_basic { get; set; }

        [StringLength(12)]
        public string? A_fix_basic { get; set; }

        [StringLength(12)]
        public string? A_DA { get; set; }

        [StringLength(12)]
        public string? A_var_DA { get; set; }

        [StringLength(12)]
        public string? A_conv_allow { get; set; }

        [StringLength(12)]
        public string? A_LTA { get; set; }

        [StringLength(12)]
        public string? A_medical_allow { get; set; }

        [StringLength(12)]
        public string? A_child_edu { get; set; }

        [StringLength(12)]
        public string? A_uniform { get; set; }

        [StringLength(12)]
        public string? A_HRA { get; set; }

        [StringLength(12)]
        public string? A_misc1 { get; set; }

        [StringLength(12)]
        public string? A_misc2 { get; set; }

        [StringLength(12)]
        public string? A_misc3 { get; set; }

        [StringLength(12)]
        public string? A_misc4 { get; set; }

        [StringLength(12)]
        public string? A_magazine { get; set; }

        [StringLength(12)]
        public string? A_canteen { get; set; }

        [StringLength(12)]
        public string? A_driver { get; set; }

        [StringLength(12)]
        public string? A_guest { get; set; }

        [StringLength(12)]
        public string? A_soft_furnishing { get; set; }

        [StringLength(1)]
        public string? leave_encash { get; set; }

        [StringLength(1)]
        public string? v_leave_encash { get; set; }

        [StringLength(12)]
        public string? A_leave_encash { get; set; }

        [StringLength(1)]
        public string? ot_amt { get; set; }

        [StringLength(1)]
        public string? v_ot_amt { get; set; }

        [StringLength(12)]
        public string? A_ot_amt { get; set; }

        [StringLength(3)]
        public string Profcen_cd { get; set; }

        [StringLength(12)]
        public string? A_att_bonus { get; set; }

        [StringLength(12)]
        public string? A_prod_inct { get; set; }

        [StringLength(1)]
        public string? att_bonus { get; set; }

        [StringLength(1)]
        public string? V_att_bonus { get; set; }

        [StringLength(1)]
        public string? misc5 { get; set; }

        [StringLength(1)]
        public string? misc6 { get; set; }

        [StringLength(1)]
        public string? misc7 { get; set; }

        [StringLength(1)]
        public string? misc8 { get; set; }

        [StringLength(1)]
        public string? misc9 { get; set; }

        [StringLength(1)]
        public string? V_misc5 { get; set; }

        [StringLength(1)]
        public string? V_misc6 { get; set; }

        [StringLength(1)]
        public string? V_misc7 { get; set; }

        [StringLength(1)]
        public string? V_misc8 { get; set; }

        [StringLength(1)]
        public string? V_misc9 { get; set; }

        [StringLength(12)]
        public string? A_misc5 { get; set; }

        [StringLength(12)]
        public string? A_misc6 { get; set; }

        [StringLength(12)]
        public string? A_misc7 { get; set; }

        [StringLength(12)]
        public string? A_misc8 { get; set; }

        [StringLength(12)]
        public string? A_misc9 { get; set; }

        [StringLength(12)]
        public string? a_other_days_income { get; set; }
    }
}
