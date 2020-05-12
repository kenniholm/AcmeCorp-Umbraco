using AcmeCorporation.Controllers;
using ageCalc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AcmeCorpTests
{
    public class HomeControllerTests
    {


        [Fact]
        public void TestRedirectIfAbove18()
        {
            var logger = Mock.Of<ILogger<HomeController>>();
            IAgeCalculation verifier = new AgeCalculator();
            HomeController controller = new HomeController(logger, verifier);

            string dateOfBirth = "1996, 02, 25";


            IActionResult result = controller.AgeVerification(dateOfBirth);
            RedirectToActionResult actionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", actionResult.ActionName);
            Assert.Equal("Submissions", actionResult.ControllerName);
        }

        [Fact]
        public void TestRedirectIfUnder18()
        {
            var logger = Mock.Of<ILogger<HomeController>>();
            IAgeCalculation verifier = new AgeCalculator();
            HomeController controller = new HomeController(logger, verifier);

            string dateOfBirth = "2006, 02, 25";

            IActionResult result = controller.AgeVerification(dateOfBirth);
            RedirectToActionResult actionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("ErrorPage", actionResult.ActionName);
            Assert.Equal(1, actionResult.RouteValues["contentId"]);
        }

        [Fact]
        public void InvalidDobInput()
        {
            var logger = Mock.Of<ILogger<HomeController>>();
            IAgeCalculation verifier = new AgeCalculator();
            HomeController controller = new HomeController(logger, verifier);

            string dateOfBirth = "thisinputisnotabirthday";

            IActionResult result = controller.AgeVerification(dateOfBirth);
            RedirectToActionResult actionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("ErrorPage", actionResult.ActionName);
            Assert.Equal(2, actionResult.RouteValues["contentId"]);
        }
    }
}
