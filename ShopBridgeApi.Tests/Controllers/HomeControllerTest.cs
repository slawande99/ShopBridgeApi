using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridgeApi;
using ShopBridgeApi.Controllers;

namespace ShopBridgeApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ApiHomeController controller = new ApiHomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
