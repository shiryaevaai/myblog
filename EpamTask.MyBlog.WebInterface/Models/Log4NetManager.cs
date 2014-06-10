namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using log4net;
    using log4net.Config;
    using log4net.Core;

    public class Log4NetManager
    {
        public static void InitializeLog4Net()
        {
            //initialize the log4net configuration based on the log4net.config file
            XmlConfigurator.ConfigureAndWatch(new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\Log4Net.config"));

            log4net.Repository.Hierarchy.Hierarchy hier = log4net.LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
            if (hier != null)
            {
                // Get ADONetAppender by name
                log4net.Appender.AdoNetAppender adoAppender = (from appender in hier.GetAppenders()
                                              where appender.Name.Equals("DbAppender", StringComparison.InvariantCultureIgnoreCase)
                                                               select appender).FirstOrDefault() as log4net.Appender.AdoNetAppender;

                // Change only when the auto setting is set
                if (adoAppender != null && adoAppender.ConnectionString.Contains("{auto}"))
                {
                    adoAppender.ConnectionString = ExtractConnectionStringFromEntityConnectionString(
                            GetEntitiyConnectionStringFromWebConfig());

                    //refresh settings of appender
                    adoAppender.ActivateOptions();
                }
            }
        }

        private static string GetEntitiyConnectionStringFromWebConfig()
        {
            //return System.Configuration.ConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString;
            return System.Configuration.ConfigurationManager.ConnectionStrings["MyBlogDBConnection"].ConnectionString;
        }

        private static string ExtractConnectionStringFromEntityConnectionString(string entityConnectionString)
        {
            // create a entity connection string from the input
            //EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder(entityConnectionString);

            //// read the db connectionstring
            //return entityBuilder.ProviderConnectionString;
            return System.Configuration.ConfigurationManager.ConnectionStrings["MyBlogDBConnection"].ConnectionString;
        }
    }
}