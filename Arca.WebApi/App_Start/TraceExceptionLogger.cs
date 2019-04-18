using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Arca.WebApi.App_Start
{
    public class TraceExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TraceExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            string postData = string.Empty;

            try
            {
                var data = ((System.Web.Http.ApiController)
          context.ExceptionContext.ControllerContext.Controller)
          .ActionContext.ActionArguments.Values;

                postData = JsonConvert.SerializeObject(data);
            }
            catch
            {
                // DO SOMETHING ??          
            }

            Logger.FatalFormat("Fatal error WEBAPI: URL:{ 0}  "+Environment.NewLine+ " METHOD: { 1} " + Environment.NewLine + "POST DATA:{ 2} " + Environment.NewLine + " Exception: { 3}", 
              context.Request.RequestUri, context.Request.Method, 
   postData, context.ExceptionContext.Exception);
        }
    }
}