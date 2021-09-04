using System;

namespace Paybook.ServiceLayer.Models
{
    public class LastSavedNumberModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Business { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public string Prefix { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string LastNumber { get; set; }
        public string Seperator { get; set; }
        public string NewNumber { get; set; }
    }
}
