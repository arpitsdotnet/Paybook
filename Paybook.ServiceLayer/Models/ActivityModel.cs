using System;

namespace Paybook.ServiceLayer.Models
{
    public class ActivityModel : BaseResultStatusModel
    {
        public int ID { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Business { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Text { get; set; }
        public string TextHtml { get; set; }
        public string Status { get; set; }
    }
}
