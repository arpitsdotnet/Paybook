using System;

namespace Paybook.ServiceLayer.Models
{
    public class NoteModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }
        public string Text { get; set; }
        public int WorkTypeId { get; set; }
        public virtual CategoryMasterModel WorkTypeCategoryMaster { get; set; }
        public string VehicleNumber { get; set; }
        public string ClientName { get; set; }
        public string MobileNumber { get; set; }
        public string Amount { get; set; }
        public string Awak { get; set; }
        public string Jawak { get; set; }
        public string Balance { get; set; }

    }
}
