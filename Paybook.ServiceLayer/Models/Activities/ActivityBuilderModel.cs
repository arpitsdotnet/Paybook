using System;

namespace Paybook.ServiceLayer.Models.Activities
{
    public class ActivityBuilderModel
    {
        public int ID { get; set; }
        public int BusinessId { get; set; }
        public string CreateBy { get; set; }

        public string Title { get; set; }
        public string TitleCss { get; set; } = "text-success";
        public string Date { get; set; }

        public string Type { get; set; }
        public string TypeNumber { get; set; } = "";
        public string ClientName { get; set; }
        public string Amount { get; set; } = "";

    }
}
