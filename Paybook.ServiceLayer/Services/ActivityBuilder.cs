namespace Paybook.ServiceLayer.Services
{

    public class ActivityBuilder
    {
        public string Header { get; set; } = "";
        private string HeaderHtml { get; set; } = "";
        public string Body { get; set; } = "";
        private string BodyHtml { get; set; } = "";

        public ActivityBuilder AddHeader(string title, string date, string titleColorCss = "")
        {
            this.Header += title + " (" + date + ")";
            this.HeaderHtml += "<div class=\"" + titleColorCss + " fwt-large\"><i class='fa fa-info-circle'></i>&nbsp;" + title + " <span class=\"small text-grey\">(" + date + ")</span></div>";

            return this;
        }

        public ActivityBuilder AddBody(string heading, string headingId, string toName, string status, string ofAmount = "")
        {
            string amountHtml = "";
            if (!string.IsNullOrWhiteSpace(ofAmount))
            {
                amountHtml = "of <span class='nowrap text-green'><i class='fa fa-inr'></i>" + ofAmount + "</span>";
                ofAmount = " of " + ofAmount;
            }
            this.Body = $"{heading} ({headingId}) to {toName}{ofAmount} is {status}";
            this.BodyHtml += "<div class=\"small\">"
                        + heading + " <span class=\"text-blue\">" + headingId + "</span> to <span class=\"text-blue\">" + toName + amountHtml + "</span> is " + status + "."
                    + "</div>";

            return this;
        }

        public override string ToString()
        {
            return this.Header + this.Body;
        }
        public string ToStringHtml()
        {
            return this.HeaderHtml + this.BodyHtml;
        }
    }

}
