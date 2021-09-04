using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models
{
    public class CategoryTypeMasterModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Business { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }
        public string Name { get; set; }
        public string Core { get; set; }
    }
}
