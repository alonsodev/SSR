using ExpressiveAnnotations.MvcUnobtrusive.Providers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Presentation.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, logger);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            ModelValidatorProviders.Providers.Remove(
                  ModelValidatorProviders.Providers
                      .FirstOrDefault(x => x is DataAnnotationsModelValidatorProvider));
                        ModelValidatorProviders.Providers.Add(
                            new ExpressiveAnnotationsModelValidatorProvider());

            ModelMetadataProviders.Current = new DataAnnotationsModelMetadataProvider();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
