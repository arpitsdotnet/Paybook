namespace Paybook.ServiceLayer.Services
{

    public class ActivityBuilder
    {
        public string Header { get; private set; } = "";
        private string HeaderHtml { get; set; } = "";
        public string Body { get; private set; } = "";
        private string BodyHtml { get; set; } = "";

        /// <summary>
        /// Example1: Created (27/Jan/2021)
        /// Example1: Overdue (22/Jan/2021)
        /// </summary>
        /// <param name="status"></param>
        /// <param name="date"></param>
        /// <param name="statusIcon">OPTIONAL | DEFAULT: <i class='glyphicon glyphicon-exclamation-sign'></i></param>
        /// <param name="titleColorCss">OPTIONAL</param>
        /// <returns></returns>
        public ActivityBuilder AddHeader(string status, string date, string titleColorCss = "text-success", string statusIcon = "glyphicon glyphicon-exclamation-sign")
        {
            this.Header += "(" + date + ") ";
            this.HeaderHtml += $"<div class=\"header\"><i class='{statusIcon} {titleColorCss}'></i><span class=\"title {titleColorCss}\">{status} </span><small>({date})</small></div>";

            return this;
        }

        /// <summary>
        /// Example1: Invoice (INV/2021/A0001) to Customer Name of ₹ 1.00 is Created.
        /// Example2: Invoice (INV/2021/A0001) to Customer Name of ₹ 1.00 is Overdue.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeNumber"></param>
        /// <param name="toName"></param>
        /// <param name="status"></param>
        /// <param name="ofAmount">OPTIONAL</param>
        /// <returns></returns>
        public ActivityBuilder AddBody(string type, string typeNumber, string toName, string status, string ofAmount = "")
        {
            string typeNumberHtml = "", ofAmountHtml = "";
            if (!string.IsNullOrWhiteSpace(typeNumber))
            {
                typeNumberHtml = $" <span class=\"text-info\">{typeNumber}</span>";
                typeNumber = $" ({typeNumber})";
            }
            if (!string.IsNullOrWhiteSpace(ofAmount))
            {
                ofAmountHtml = " of <span class='nowrap text-success'>#rupeesign# " + ofAmount + "</span>";
                ofAmount = " of #rupeesign# " + ofAmount;
            }

            this.Body = $"{type}{typeNumber} {status}{ofAmount} for {toName}.";
            this.BodyHtml += $"<div class=\"body\">{type}{typeNumberHtml} {status}{ofAmountHtml} for <span class=\"text-primary\">{toName}</span>.</div>";

            return this;
        }

        public override string ToString()
        {
            return this.Header + " " + this.Body;
        }
        public string ToStringHtml()
        {
            return this.HeaderHtml + this.BodyHtml;
        }
    }

}
