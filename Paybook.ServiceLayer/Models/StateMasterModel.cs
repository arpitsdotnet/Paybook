using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models
{
    public class StateMasterModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }
        public int CountryId { get; set; }
        public virtual CountryMasterModel CountryMaster { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
    }
}
