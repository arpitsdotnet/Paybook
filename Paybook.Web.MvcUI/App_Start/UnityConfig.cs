using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Identity;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Note;
using Paybook.BusinessLayer.Payment;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Logger;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Paybook.Web.MvcUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterInstance<ILogger>(LoggerFactory.Instance);

            container.RegisterType<IActivityProcessor, ActivityProcessor>();
            container.RegisterType<IDashboardProcessor, DashboardProcessor>();
            container.RegisterType<ILastSavedNumberProcessor, LastSavedNumberProcessor>();
            container.RegisterType<ICategoryTypeProcessor, CategoryTypeProcessor>();
            container.RegisterType<ICategoryProcessor, CategoryProcessor>();
            container.RegisterType<ICountryProcessor, CountryProcessor>();
            container.RegisterType<IStateProcessor, StateProcessor>();

            container.RegisterType<IUserProcessor, UserProcessor>();
            container.RegisterType<ILoginProcessor, LoginProcessor>();
            container.RegisterType<IBusinessProcessor, BusinessProcessor>();
            //container.RegisterType<IAgencyProcessor, AgencyProcessor>();
            container.RegisterType<IClientProcessor, ClientProcessor>();
            container.RegisterType<IInvoiceProcessor, InvoiceProcessor>();
            container.RegisterType<IInvoiceServiceProcessor, InvoiceServiceProcessor>();
            //container.RegisterType<IRemarkProcessor, RemarkProcessor>();
            container.RegisterType<IPaymentProcessor, PaymentProcessor>();
            container.RegisterType<INoteProcessor, NoteProcessor>();
            //container.RegisterType<IReportProcessor, ReportProcessor>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}