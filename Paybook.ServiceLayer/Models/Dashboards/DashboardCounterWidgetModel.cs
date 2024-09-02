namespace Paybook.ServiceLayer.Models.Dashboards
{
    public class DashboardCounterWidgetModel
    {
        public string BgColorClass { get; set; }
        public string BgColorHoverClass { get; set; }
        public string WidgetIcon { get; set; } = "fa-rupee";
        public string WidgetIconColor { get; set; }
        public decimal Total { get; set; }
        public int Count { get; set; }
        public string CountText { get; set; }
    }
}