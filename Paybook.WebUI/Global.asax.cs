using System;
using System.Web.Routing;

namespace Paybook.WebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        public static void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.MapPageRoute("Login", "identity/login", "~/identity/Login.aspx");
            routeCollection.MapPageRoute("Logout", "identity/logout", "~/identity/Logout.aspx");

            routeCollection.MapPageRoute("Home", "dashboard", "~/business/dashboard.aspx");

            routeCollection.MapPageRoute("InvoiceList", "invoice", "~/invoice/index.aspx");
            routeCollection.MapPageRoute("InvoiceSearch", "invoice/search", "~/invoice/index.aspx");
            routeCollection.MapPageRoute("InvoiceCreate", "invoice/create", "~/invoice/create.aspx");
            routeCollection.MapPageRoute("InvoiceUpdate", "invoice/update/{id}", "~/invoice/create.aspx");
            routeCollection.MapPageRoute("InvoiceParticular", "invoice/particular/{id}", "~/invoice/particular.aspx");

            routeCollection.MapPageRoute("PaymentList", "payment", "~/payment/index.aspx");
            routeCollection.MapPageRoute("PaymentCreate", "payment/create", "~/payment/create.aspx");
            routeCollection.MapPageRoute("PaymentUpdate", "payment/update/{id}", "~/payment/create.aspx");
            // routeCollection.MapPageRoute("Invoice", "particular", "~/invoice_add.aspx");
            // 

            routeCollection.MapPageRoute("AgentList", "agent", "~/agent/index.aspx");
            routeCollection.MapPageRoute("AgentSearch", "agent/search", "~/agent/index.aspx");
            routeCollection.MapPageRoute("AgentCreate", "agent/create", "~/agent/create.aspx");
            routeCollection.MapPageRoute("AgentUpdate", "agent/update/{id}", "~/agent/create.aspx");

            routeCollection.MapPageRoute("ClientList", "client", "~/client/index.aspx");
            routeCollection.MapPageRoute("ClientSearch", "client/search", "~/client/index.aspx");
            routeCollection.MapPageRoute("ClientCreate", "client/create", "~/client/create.aspx");
            routeCollection.MapPageRoute("ClientEdit", "client/update/{id}", "~/client/create.aspx");

            routeCollection.MapPageRoute("AgencyCreate", "agency/create", "~/agency/create.aspx");
            routeCollection.MapPageRoute("AgencyUpdate", "agency/update/{id}", "~/agency/create.aspx");

            routeCollection.MapPageRoute("CategoryList", "category", "~/setting/category.aspx");
            routeCollection.MapPageRoute("CategoryCreate", "category/create", "~/setting/_CreateCategoryPartial.aspx");
            routeCollection.MapPageRoute("CategoryUpdate", "category/update/{id}", "~/setting/_CreateCategoryPartial.aspx");
            //routeCollection.MapPageRoute("Password", "password", "~/change_password.aspx");
            routeCollection.MapPageRoute("SearchCompany", "business/profile", "~/business/profile.aspx");
            //routeCollection.MapPageRoute("Registration", "registration", "~/registration.aspx");

            routeCollection.MapPageRoute("NoteList", "notes", "~/note/index.aspx");
            routeCollection.MapPageRoute("NoteCreate", "notes/create", "~/note/_createnotepartial.aspx");
            routeCollection.MapPageRoute("NoteUpdate", "notes/update/{id}", "~/note/_createnotepartial.aspx");

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