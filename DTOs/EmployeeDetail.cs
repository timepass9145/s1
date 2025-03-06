using Microsoft.EntityFrameworkCore;

namespace SIBSAPI.DTOs
{
    public class EmployeeDetail
    {
        public Guid Id { get; set; }
        public string Refer { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
        public string Grade { get; set; }
        public string EmpCategory { get; set; }
        public string NoticePeriod { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string PermanentAddress { get; set; }
        public string City { get; set; }
        public string PinNo { get; set; }
        public string State { get; set; }
        public string ContactNo { get; set; }
        public string AdharCardNo { get; set; }
        public string TransferDivision { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string JoinDate { get; set; }
        public string WeekOff1 { get; set; }
        public string WeekOff2 { get; set; }
        public string Designation { get; set; }
        public string ReferredBy { get; set; }
        public string Reporting { get; set; }
        public string PanNo { get; set; }
        public string NoOfChildren { get; set; }
        public string Qualification { get; set; }
        public string PrevExperience { get; set; }
        public string TemporaryStatus { get; set; }
        public string TemporaryStatusDate { get; set; }
        public string ConfirmDate { get; set; }
        public string GratuityID { get; set; }


    }
}
