using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Identity;
using Paybook.BusinessLayer.Abstracts.Invoices;
using Paybook.BusinessLayer.Abstracts.Outbox;
using Paybook.BusinessLayer.Abstracts.Payments;
using Paybook.BusinessLayer.Abstracts.Utilities;
using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Features.Admins;
using Paybook.BusinessLayer.Features.Customers;
using Paybook.BusinessLayer.Features.Identity;
using Paybook.BusinessLayer.Features.Outbox;
using Paybook.BusinessLayer.Features.Payments;
using Paybook.BusinessLayer.Features.Utilities;
using Paybook.BusinessLayer.Invoice;
using Paybook.ServiceLayer.Logger;
using System;
using System.Web.Routing;
using Unity;

namespace Paybook.WebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            IUnityContainer container = new UnityContainer();

            container.RegisterInstance<ILogger>(LoggerFactory.Instance);

            container.RegisterType<IActivityProcessor, ActivityProcessor>();
            container.RegisterType<IDashboardProcessor, DashboardProcessor>();
            container.RegisterType<ILastSavedNumberProcessor, LastSavedNumberProcessor>();
            container.RegisterType<ICategoryTypeProcessor, CategoryTypeProcessor>();
            container.RegisterType<ICategoryProcessor, CategoryProcessor>();

            container.RegisterType<ILoginProcessor, LoginProcessor>();
            container.RegisterType<IBusinessProcessor, BusinessProcessor>();
            container.RegisterType<IAgencyProcessor, AgencyProcessor>();
            container.RegisterType<IClientProcessor, ClientProcessor>();
            container.RegisterType<IInvoiceProcessor, InvoiceProcessor>();
            container.RegisterType<IInvoiceServiceProcessor, InvoiceServiceProcessor>();
            //container.RegisterType<IRemarkProcessor, RemarkProcessor>();
            container.RegisterType<INoteProcessor, NoteProcessor>();
            container.RegisterType<IPaymentProcessor, PaymentProcessor>();


        }
        public static void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.MapPageRoute("Login", "identity/login", "~/identity/Login.aspx");
            routeCollection.MapPageRoute("Logout", "identity/logout", "~/identity/Logout.aspx");

            routeCollection.MapPageRoute("BusinessDashboard", "dashboard", "~/business/dashboard.aspx");
            routeCollection.MapPageRoute("BusinessCreate", "business/profile", "~/business/create.aspx");

            routeCollection.MapPageRoute("InvoiceList", "invoices", "~/invoice/index.aspx");
            routeCollection.MapPageRoute("InvoiceSearch", "invoice/search", "~/invoice/index.aspx");
            routeCollection.MapPageRoute("InvoiceParticular", "invoice/service/create/{invoiceId}", "~/invoice/_CreateServicePartial.aspx");
            routeCollection.MapPageRoute("InvoiceParticular", "invoice/service/update/{id}", "~/invoice/_CreateServicePartial.aspx");
            routeCollection.MapPageRoute("InvoiceCreate", "invoice/create", "~/invoice/create.aspx");
            routeCollection.MapPageRoute("InvoiceUpdate", "invoice/update/{id}", "~/invoice/createold.aspx");

            routeCollection.MapPageRoute("PaymentList", "payments", "~/payment/index.aspx");
            routeCollection.MapPageRoute("PaymentCreate", "payment/create", "~/payment/_createpaymentpartial.aspx");
            routeCollection.MapPageRoute("PaymentUpdate", "payment/update/{id}", "~/payment/_createpaymentpartial.aspx");
            // routeCollection.MapPageRoute("Invoice", "particular", "~/invoice_add.aspx");
            // 

            routeCollection.MapPageRoute("AgentList", "agents", "~/agent/index.aspx");
            routeCollection.MapPageRoute("AgentSearch", "agent/search", "~/agent/index.aspx");
            routeCollection.MapPageRoute("AgentCreate", "agent/create", "~/agent/create.aspx");
            routeCollection.MapPageRoute("AgentUpdate", "agent/update/{id}", "~/agent/create.aspx");

            routeCollection.MapPageRoute("ClientList", "clients", "~/client/index.aspx");
            routeCollection.MapPageRoute("ClientSearch", "client/search", "~/client/index.aspx");
            routeCollection.MapPageRoute("ClientCreate", "client/create", "~/client/_createclientpartial.aspx");
            routeCollection.MapPageRoute("ClientEdit", "client/update/{id}", "~/client/_createclientpartial.aspx");

            routeCollection.MapPageRoute("AgencyList", "agencies", "~/agency/index.aspx");
            routeCollection.MapPageRoute("AgencyCreate", "agency/create", "~/agency/_createagencypartial.aspx");
            routeCollection.MapPageRoute("AgencyUpdate", "agency/update/{id}", "~/agency/_createagencypartial.aspx");

            routeCollection.MapPageRoute("CategoryList", "settings/category", "~/setting/category.aspx");
            routeCollection.MapPageRoute("CategoryCreate", "settings/category/create", "~/setting/_CreateCategoryPartial.aspx");
            routeCollection.MapPageRoute("CategoryUpdate", "settings/category/update/{id}", "~/setting/_CreateCategoryPartial.aspx");
            //routeCollection.MapPageRoute("Password", "password", "~/change_password.aspx");

            //routeCollection.MapPageRoute("Registration", "registration", "~/registration.aspx");

            routeCollection.MapPageRoute("NoteList", "notes", "~/note/index.aspx");
            routeCollection.MapPageRoute("NoteCreate", "note/create", "~/note/_createnotepartial.aspx");
            routeCollection.MapPageRoute("NoteUpdate", "note/update/{id}", "~/note/_createnotepartial.aspx");

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