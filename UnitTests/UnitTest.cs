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
        public void TestGetById()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Accept", "application/json");
                const string id = "foo1";
                var getOneAddress = string.Format("{0}('{1}')", ServiceUrl, id);
                var resp = client.DownloadString(getOneAddress);
                dynamic result = Json.Decode(resp);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.d);
                Assert.IsTrue(result.d.id == "foo1");
            }
        }

        [TestMethod]
        public void TestGetByDoubleEncodedId()
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
