using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Sample_KB2894854
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : ODataService
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.UseVerboseErrors = true;
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }

    }

    public class ODataService : DataService<FooDataContext>
    {
        public ODataService()
        {
            var uri = HttpContext.Current.Request.Url;
            /* uri.OriginalString here is http://localhost:1111/sample.svc/Foos('foo%7B---%7D') */
            /* uri.ToString() here is http://localhost:1111/sample.svc/Foos('foo{---}') */
        }
    }

    [DataServiceKey("Id")]
    public class Foo
    {
        public string Id { get; set; }
    }

    public class FooDataContext
    {
        public IQueryable<Foo> Foos { get; set; }

        public FooDataContext()
        {
            Foos = new EnumerableQuery<Foo>(new []
            {
                new Foo {Id = "foo1"}, 
                new Foo {Id = "foo{---}"}
            });
        }
    }

}
