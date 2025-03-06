using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SIBSAPI.Models
{
    [Table("PERSONAL_MST")]
    public class PersonalMst
    {
    //[Required]
    [Key]
    [StringLength(5)]
    public string emp_no { get; set; }

    [StringLength(25)]
    public string? fname { get; set; }
    
    [StringLength(25)]
    public string? mname { get; set; }
    
    [StringLength(25)]
    public string? lname { get; set; }
    
    [StringLength(3)]
    public string? dept_code { get; set; }
    
    [StringLength(1)]
    public string? gender { get; set; }
    
    public DateTime? birth_date { get; set; }
    
    [StringLength(1)]
    public string? marital_status { get; set; }
    
    public DateTime? join_date { get; set; }
    
    [StringLength(30)]
    public string? addr1 { get; set; }
    
    [StringLength(30)]
    public string? addr2 { get; set; }
    
    [StringLength(30)]
    public string? addr3 { get; set; }
    
    [StringLength(30)]
    public string? addr4 { get; set; }
    
    [StringLength(50)]
    public string? phone { get; set; }
    
    [StringLength(40)]
    public string? email { get; set; }
    
    [StringLength(3)]
    public string? blood_group { get; set; }
    
    [StringLength(10)]
    public string? week_off { get; set; }
    
    [StringLength(1)]
    public string? refer { get; set; }
    
    [StringLength(5)]
    public string? cndt_no { get; set; }
    
    [StringLength(50)]
    public string? short_name { get; set; }
    
    [StringLength(1)]
    public string? status { get; set; }
    
    [StringLength(3)]
    public string? desig_code { get; set; }
    
    [StringLength(1)]
    public string? emp_flag { get; set; }
    
    [StringLength(15)]
    public string? pan_no { get; set; }
    
    [StringLength(3)]
    public string? profcen_cd { get; set; }
    
    [StringLength(5)]
    public string? report_to { get; set; }
    
    [StringLength(10)]
    public string? week_off1 { get; set; }
    
    [StringLength(30)]
    public string? Taddr1 { get; set; }
    
    [StringLength(30)]
    public string? Taddr2 { get; set; }
    
    [StringLength(30)]
    public string? Taddr3 { get; set; }
    
    [StringLength(30)]
    public string? Taddr4 { get; set; }
    
    [StringLength(70)]
    public string? sign { get; set; }
    
    [StringLength(30)]
    public string? Religion { get; set; }
    
    [StringLength(50)]
    public string? Drive_LIC { get; set; }
    
    [StringLength(50)]
    public string? passport { get; set; }
    
    [StringLength(5)]
    public string? reffered_by { get; set; }
    
    [StringLength(2)]
    public string? no_child { get; set; }
    
    [StringLength(1)]
    public string? approve_flag { get; set; }
    
    [StringLength(3)]
    public string? grade { get; set; }
    
    [StringLength(1)]
    public string? tech_category { get; set; }
    
    public DateTime? left_status { get; set; }
    
    public DateTime? confirm_date { get; set; }
    
    public DateTime? prob_date { get; set; }
    
    public DateTime? end_temp { get; set; }
    
    public DateTime? start_prob { get; set; }
    
    public double? temp_period { get; set; }
    
    [StringLength(7)]
    public string? period { get; set; }
    
    [StringLength(1)]
    public string? Salary_Det { get; set; }
    
    public DateTime? Passport_Valid_Dt { get; set; }
    
    public short? Notice_Period { get; set; }
    
    public DateTime? Resign_Dt { get; set; }
    
    [StringLength(1)]
    public string? Rotation_Code { get; set; }
    
    [StringLength(3)]
    public string? Loc_Code { get; set; }
    
    [StringLength(1)]
    public string? Temp_Flag { get; set; }
    
    public DateTime? Temp_Flag_Date { get; set; }
    
    [StringLength(30)]
    public string? Reason_Of_Left { get; set; }
    
    public DateTime? Rotation_Start { get; set; }
    
    public byte? No_Of_Punching { get; set; }
    
    [StringLength(30)]
    public string? State { get; set; }
    
    public int? Route_Id { get; set; }
    
    [StringLength(30)]
    public string? Working_State { get; set; }
    
    [StringLength(20)]
    public string? Pin_No { get; set; }
    
    [StringLength(20)]
    public string? TPin_No { get; set; }
    
    [StringLength(30)]
    public string? TState { get; set; }
    
    [StringLength(40)]
    public string? Birth_Place { get; set; }
    
    [StringLength(200)]
    public string? Qualification { get; set; }
    
    public float? Prev_Experiance { get; set; }
    
    public bool? TMS_Applicable { get; set; }
    
    [StringLength(21)]
    public string? Mobile_No { get; set; }
    
    [StringLength(1)]
    public string? Canteen_Ded_Flag { get; set; }
    
    [StringLength(3)]
    public string? Proj_Code { get; set; }
    
    [StringLength(10)]
    public string? AdvGL_Code { get; set; }
    
    [StringLength(1)]
    public string? Union_Code { get; set; }
    
    public int? Bond_Months { get; set; }
    
    public double? Bond_Amount { get; set; }
    
    [StringLength(3)]
    public string? Work_division { get; set; }

    public DateTime? Transfer_from { get; set; }
    
    [StringLength(12)]
    public string? adhar_card_no { get; set; }
    
    [StringLength(10)]
    public string? Gratuty_ID { get; set; }
    
    [StringLength(3)]
    public string? OLD_DIV { get; set; }
    
    [StringLength(5)]
    public string? Replacement_for { get; set; }
    
    public double? mediclaim_parent { get; set; }
    
    public DateTime? Old_trf_date { get; set; }
    
    [StringLength(2)]
    public string? Emp_skill { get; set; }

    }

}
