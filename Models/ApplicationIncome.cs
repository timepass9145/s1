using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SIBSAPI.Models
{


    [Table("Applicable_Incomes")]
    public class ApplicationIncome
    {
        [Key]
        [StringLength(3)]
        public string Profcen_Cd { get; set; }
        public bool basic { get; set; }
        public bool fix_basic { get; set; }
        public bool DA { get; set; }
        public bool var_DA { get; set; }
        public bool LTA { get; set; }
        public bool medical_allow { get; set; }
        public bool child_edu { get; set; }
        public bool uniform { get; set; }
        public bool HRA { get; set; }
        public bool conv_allow { get; set; }
        public bool canteen { get; set; }
        public bool misc1 { get; set; }
        public bool misc2 { get; set; }
        public bool misc3 { get; set; }
        public bool misc4 { get; set; }
        public bool magazine { get; set; }
        public bool driver { get; set; }
        public bool guest { get; set; }
        public bool soft_furnishing { get; set; }
        public bool misc5 { get; set; }
        public bool misc6 { get; set; }
        public bool misc7 { get; set; }
        public bool misc8 { get; set; }
        public bool misc9 { get; set; }
        public string N_misc1 { get; set; }
        public string N_misc2 { get; set; }
        public string N_misc3 { get; set; }
        public string N_misc4 { get; set; }
        public string N_magazine { get; set; }
        public string N_driver { get; set; }
        public string N_guest { get; set; }
        public string N_soft_furnishing { get; set; }
        public string N_misc5 { get; set; }
        public string N_misc6 { get; set; }
        public string N_misc7 { get; set; }
        public string N_misc8 { get; set; }
        public string N_misc9 { get; set; }
        public string N_DA { get; set; }
        public string N_Days_Misc1 { get; set; }
        public string N_Days_Misc2 { get; set; }
        public string N_Days_Misc3 { get; set; }
        public string N_Days_Misc4 { get; set; }
        public string N_Deduction_Misc1 { get; set; }
        public string N_Deduction_Misc2 { get; set; }
        public string N_Deduction_Misc3 { get; set; }
        public string N_Deduction_Misc4 { get; set; }
        public string N_Soc_Contri { get; set; }
        public string N_Soc_Loan { get; set; }
    }
}
