using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    public class pmMasterLogic
    {
        public Nullable<int> ID { get; set; }
        public string Logic { get; set; }
        public string Result { get; set; }
        public string login_id { get; set; }
        public bool Flag { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class MasterRole : pmMasterLogic
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class MasterMainMenu : pmMasterLogic
    {
        public string MenuId { get; set; }
        public string RoleId { get; set; }
        public string MenuName { get; set; }
    }
    public class MasterSubMenu : pmMasterLogic
    {
        public int AutoId { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuUrl { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public bool DisableFlag { get; set; }
        public string Loginid { get; set; }
        public int SequenceNo { get; set; }
        public string UsedFor { get; set; }
        public int MenuSequence { get; set; }
    }
    public class MasterEmployee : pmMasterLogic
    {
        public int emp_id { get; set; }
        public string EmployeeCode { get; set; }
        public string Title { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNo { get; set; }
        public string LocAddress { get; set; }
        public string LocLocality { get; set; }
        public string LocCity { get; set; }
        public string LocState { get; set; }
        public string PerAddress { get; set; }
        public string PerLocality { get; set; }
        public string PerCity { get; set; }
        public string PerState { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string HusbandWife { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Designation { get; set; }
        public string BloodGroup { get; set; }
        public string AadharNo { get; set; }
        public string UserCode { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string LoginId { get; set; }
    }
    public class MasterState : pmMasterLogic
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
    public class MasterCity : pmMasterLogic
    {
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
    }
    public class MasterDesignation : pmMasterLogic
    {
        public string DesigCode { get; set; }
        public string DesigName { get; set; }
    }
    public class MasterDetails : pmMasterLogic
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string DesigCode { get; set; }
        public string DesigName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int AutoId { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuUrl { get; set; }
        public bool DisableFlag { get; set; }
        public string Loginid { get; set; }
        public int SequenceNo { get; set; }
        public string UsedFor { get; set; }
        public int MenuSequence { get; set; }
        public int emp_id { get; set; }
        public string EmployeeCode { get; set; }
        public string Title { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? DOB { get; set; }
        public string MobileNo { get; set; }
        public string LocAddress { get; set; }
        public string LocLocality { get; set; }
        public string LocCity { get; set; }
        public string LocState { get; set; }
        public string PerAddress { get; set; }
        public string PerLocality { get; set; }
        public string PerCity { get; set; }
        public string PerState { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string HusbandWife { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Designation { get; set; }
        public string BloodGroup { get; set; }
        public string AadharNo { get; set; }
        public string UserCode { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string LoginId { get; set; }
    }
    public class CPOETemplateItemsBO : pmMasterLogic
    {
        public string TemplateType { get; set; }
        public string DoctorId { get; set; }
        public string TemplateId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
    }
}