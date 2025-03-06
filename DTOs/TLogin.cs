using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIBSAPI.DTOs
{
    [Table("Login")]
    public class TLogin
    {
        [Key]
        [StringLength(10)]
        public string Login_Name { get; set; }

        [StringLength(15)]
        public string Login_Pwd { get; set; }

        //public object DataJ { get; set; }

        //[StringLength(15)]
        //public string? Activity_Name { get; set; }

        //public DateTime? Exp_Dt { get; set; }

        //[StringLength(10)]
        //public string Alternate_Login { get; set; }

        //[StringLength(50)]
        //public string Division { get; set; }

        //[StringLength(5)]
        //public string Emp_No { get; set; }

        //[StringLength(20)]
        //public string Login_Desc { get; set; }

        //public byte[] Signature_Image { get; set; } // Image stored as byte array

        //[StringLength(15)]
        //public string IP_Address { get; set; }

        //[StringLength(15)]
        //public string System_Name { get; set; }

        //[StringLength(20)]
        //public string Profile_Name { get; set; }

        //public char? MultiLogin { get; set; }

        //[StringLength(50)]
        //public string EmailId { get; set; }
    }
}
