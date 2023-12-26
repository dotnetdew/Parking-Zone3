using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking_Zone.Areas.Admin;
using Parking_Zone.Models;
using Parking_Zone.Services;
using Parking_Zone.ViewModels.ParkingZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class ParkingZoneControllerTests
    {
        public Guid testId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc");
        //public ParkingZone GetTestParkingZoneDetails()
        //{
        //    var testParkingZone = new ParkingZone()
        //    {
        //        Id = testId,
        //        Name = "Sharafshon",
        //        Address = "Andijon",
        //        Description = "Qimmat"
        //    };
        //    return testParkingZone;
        //}
        //public ParkingZone testParkingZone { get; set; }

        #region Index
        [Fact]
        public void GivenNothing_WhenIndexIsCalled_ThenServiceIsCalledOnceAndReturnedNotEmptyViewResult()
        {
            //Arrange
            var mockService = new Mock<IParkingZoneService>();

            mockService.Setup(service => service.GetAll());

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull((result as ViewResult).Model);
            mockService.Verify(service => service.GetAll(), Times.Once);
        }
        #endregion

        #region Details
        [Fact]
        public void GivenId_WhenGetDetailsIsCalled_ThenServiceIsCalledOnceAndReturnedViewResultWithDetailsParkingZoneVmModel()
        {
            //Arrange
            //var expectedDetailsVM = new ParkingZoneDetailsVM()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Sharafshon",
            //    Address = "Andijon",
            //    Description = "Qimmat"
            //};

            //var mockService = new Mock<IParkingZoneService>();

            //mockService.Setup(service => service.GetById(fakeGuid)).Returns(expectedDetailsVM);
        }
        #endregion
    }
}
