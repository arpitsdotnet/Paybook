using System;
using System.Web.Routing;

namespace Paybook.WebUI
{
    public partial class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        public static void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.MapPageRoute("LinkForLogin", "login", "~/login.aspx");
            routeCollection.MapPageRoute("LinkForLogout", "logout", "~/logout.aspx");

            routeCollection.MapPageRoute("LinkForHome", "home", "~/index.aspx");
            routeCollection.MapPageRoute("LinkForSearchInvoice", "search_invoice", "~/invoices.aspx");
            routeCollection.MapPageRoute("LinkForCreateInvoice", "add_invoice", "~/invoice_add.aspx");
            routeCollection.MapPageRoute("LinkForMakePayment", "add_invoice/{MakePayment}", "~/invoice_add.aspx");
            // routeCollection.MapPageRoute("LinkForInvoice", "particular", "~/invoice_add.aspx");           
            routeCollection.MapPageRoute("LinkForSearchParticular", "particular/{Invoice_ID}/{category}", "~/particular.aspx");

            routeCollection.MapPageRoute("LinkForSearchAgent", "search_agent", "~/agents.aspx");
            routeCollection.MapPageRoute("LinkForAgent", "agent", "~/agent_add.aspx");

            routeCollection.MapPageRoute("LinkForSearchCustomer", "search_customer", "~/customers.aspx");
            routeCollection.MapPageRoute("LinkForCustomer", "customer", "~/customer_add.aspx");
            routeCollection.MapPageRoute("LinkForCustomerEdit", "customer/{customer_id}", "~/customer_add.aspx");

            routeCollection.MapPageRoute("LinkForAgency", "agency", "~/ageny_add.aspx");
            routeCollection.MapPageRoute("LinkForAgencyEdit", "agency/{agency_id}", "~/ageny_add.aspx");

            routeCollection.MapPageRoute("LinkForCategories", "categories", "~/categories.aspx");
            //routeCollection.MapPageRoute("LinkForPassword", "password", "~/change_password.aspx");
            routeCollection.MapPageRoute("LinkForSearchCompany", "company", "~/company_profile.aspx");
            routeCollection.MapPageRoute("LinkForPayment_add", "payment", "~/payment_add.aspx");
            //routeCollection.MapPageRoute("LinkForRegistration", "registration", "~/registration.aspx");

            routeCollection.MapPageRoute("LinkForNotes", "notes", "~/daily_notes.aspx");

        }


        protected void Session_Start(object sender, EventArgs e)
        {
            if (Application["Path"] == null || Application["Path"].ToString() == "")
                if (Request.Url.Host == "localhost")
                { Application.Add("Path", string.Concat("http://", Request.Url.Host, ":", Request.Url.Port, Request.ApplicationPath)); }
                else { Application.Add("Path", string.Concat("http://", Request.Url.Host, "/")); }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}