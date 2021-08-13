using System;

namespace Paybook.ServiceLayer.Models
{
    public class CategoryMasterModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }
        public string CategotyTypeId { get; set; }
        public virtual CategoryTypeMasterModel CategoryTypes { get; set; }
        public string Name { get; set; }
        public string Core { get; set; }
        public string Value { get; set; }
        public int OrderBy { get; set; }
    }
}
