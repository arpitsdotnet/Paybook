using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Identity;
using Paybook.BusinessLayer.Abstracts.Invoices;
using Paybook.BusinessLayer.Abstracts.Outbox;
using Paybook.BusinessLayer.Abstracts.Payments;
using Paybook.BusinessLayer.Abstracts.Utilities;
using Paybook.BusinessLayer.Features.Admins;
using Paybook.BusinessLayer.Features.Customers;
using Paybook.BusinessLayer.Features.Identity;
using Paybook.BusinessLayer.Features.Outbox;
using Paybook.BusinessLayer.Features.Payments;
using Paybook.BusinessLayer.Features.Utilities;
using Paybook.BusinessLayer.Invoice;
using Paybook.ServiceLayer.Abstracts;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Services;
using Paybook.ServiceLayer.Xml;
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

            container.RegisterType<IMessageProvider, XmlMessageHelperWrapper>();
            container.RegisterInstance<ILogger>(LoggerFactory.Instance);

            container.RegisterType<IDateTimeProvider, DateTimeHelper>();
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
            container.RegisterType<IInvoicePayProcessor, InvoicePayProcessor>();
            //container.RegisterType<IRemarkProcessor, RemarkProcessor>();
            container.RegisterType<IPaymentProcessor, PaymentProcessor>();
            container.RegisterType<INoteProcessor, NoteProcessor>();
            //container.RegisterType<IReportProcessor, ReportProcessor>();

            
            //container.RegisterType<>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}