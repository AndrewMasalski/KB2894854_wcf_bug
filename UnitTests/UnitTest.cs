using System.Net;
using System.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        private const string ServiceUrl = "http://localhost:1111/sample.svc/Foos";

        [TestMethod]
        public void TestGetAll()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Accept", "application/json");
                var resp = client.DownloadString(ServiceUrl);
                dynamic result = Json.Decode(resp);
                var foos = result.d as DynamicJsonArray;
                Assert.IsNotNull(foos);
                Assert.IsTrue(foos.Length == 2);
            }
        }

        [TestMethod]
        public void TestGetById()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Accept", "application/json");
                const string id = "foo%257B---%257D";
                var getOneAddress = string.Format("{0}('{1}')", ServiceUrl, id);
                var resp = client.DownloadString(getOneAddress);
                dynamic result = Json.Decode(resp);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.d);
                Assert.IsTrue(result.d.id == "foo{---}");
            }
        }
    }
}
