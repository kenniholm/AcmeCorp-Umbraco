using System;
using Xunit;
using AcmeCorporation.Controllers;
using AcmeCorporation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace AcmeCorpTests
{
    public class SubmissionControllerTests
    {
        private AcmeCorporationContext getInMemDB(string dbname)
        {
            DbContextOptionsBuilder<AcmeCorporationContext> builder = new DbContextOptionsBuilder<AcmeCorporationContext>()
                .UseInMemoryDatabase(dbname);

            return new AcmeCorporationContext(builder.Options);
        }

        private Submission MockSubmission(Guid serial)
        {
             Submission mockSubmission =
                new Submission
                {
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAdress = "John@doe.com",
                    ProductSerial = serial
                };

            return mockSubmission;
        }

        [Fact]
        public async Task SubmissionWithNoErrors()
        {
            Guid serial = Guid.NewGuid();
            AcmeCorporationContext context = getInMemDB("Submit");
            SubmissionsController controller = new SubmissionsController(context);

            Submission mock = MockSubmission(serial);
            Submission mock1 = MockSubmission(serial);

            await context.PurchasedProduct.AddAsync(new PurchasedProduct
            {
                ProductSerial = serial
            });

            await context.SaveChangesAsync();

            await controller.Create(mock);
            await controller.Create(mock1);

            Assert.Equal(2, await context.Submission.CountAsync());
        }

        [Fact]
        public async Task SubmissionWithInvalidSerial()
        {
            Guid invalidSerial = Guid.NewGuid();
            Guid validSerial = Guid.NewGuid();
            AcmeCorporationContext context = getInMemDB("InvalidSerial");
            SubmissionsController controller = new SubmissionsController(context);

            Submission mock = MockSubmission(invalidSerial);

            await context.PurchasedProduct.AddAsync(new PurchasedProduct
            {
                ProductSerial = validSerial
            });

            await context.SaveChangesAsync();

            IActionResult result = await controller.Create(mock);

            RedirectToActionResult actionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("ErrorPage", actionResult.ActionName);
            Assert.Equal("Home", actionResult.ControllerName);
            Assert.Equal(3, actionResult.RouteValues["contentId"]);
        }

        [Fact]
        public async Task SubmissionWithMaxEntries()
        {
            Guid serial = Guid.NewGuid();
            AcmeCorporationContext context = getInMemDB("MaxEntriesOnOneSerial");
            SubmissionsController controller = new SubmissionsController(context);

            Submission mock1 = MockSubmission(serial);
            Submission mock2 = MockSubmission(serial);
            Submission mock3 = MockSubmission(serial);

            await context.Submission.AddAsync(mock1);
            await context.Submission.AddAsync(mock2);
            await context.PurchasedProduct.AddAsync(new PurchasedProduct
            {
                ProductSerial = serial
            });
            await context.SaveChangesAsync();

            IActionResult result = await controller.Create(mock3);
            RedirectToActionResult actionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("ErrorPage", actionResult.ActionName);
            Assert.Equal("Home", actionResult.ControllerName);
            Assert.Equal(3, actionResult.RouteValues["contentId"]);
        }
    }
}
