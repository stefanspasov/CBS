namespace CBS
{
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using CBS.Logic.Filters;
    using CBS.Logic.Loggers.Implementations;
    using CBS.Logic.Unity;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Owin;

    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Filters.Add(new ModelAttributeFilter());
            config.Filters.Add(new ExceptionFilter(new DummyLogger()));
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings 
                = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()};
            config.DependencyResolver = new UnityResolver(UnityContainerFactory.Create());

            appBuilder.UseWebApi(config);
        }
    }
}
