using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking_Zone.Areas.Admin;
using Parking_Zone.Models;
using Parking_Zone.Services;
using Parking_Zone.ViewModels.ParkingZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class ParkingZoneControllerTests
    {
        private readonly Guid testId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc");

        private readonly ParkingZone testParkingZone = new ParkingZone()
        {
            Id = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
            Name = "Sharafshon",
            Address = "Andijon",
            Description = "Arzon"
        };

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
            var expectedParkingZoneDetailsVM = new ParkingZoneDetailsVM()
            {
                Id = testId,
                Name = "Sharafshon",
                Address = "Andijon",
                Description = "Arzon"
            };

            var mockService = new Mock<IParkingZoneService>();

            mockService.Setup(service => service.GetById(testId)).Returns(testParkingZone);

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Details(testId);

            //Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ParkingZoneDetailsVM>((result as ViewResult).Model);
            Assert.Equal(JsonSerializer.Serialize(expectedParkingZoneDetailsVM), JsonSerializer.Serialize(model));
            mockService.Verify(service => service.GetById(testId), Times.Once);
        }

        [Fact]
        public void GivenIdOfNotExistingParkingZone_WhenGetDetailsIsCalled_ThenServiceIsCalledOnceAndReturnedNotFound()
        {
            //Arrange
            var mockService = new Mock<IParkingZoneService>();

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Details(testId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockService.Verify(service => service.GetById(testId), Times.Once);
        }
        #endregion

        #region Create
        [Fact]
        public void GivenNothing_WhenGetCreateIsCalled_ThenEmptyViewResultIsReturned()
        {
            //Arrange
            var controller = new ParkingZoneController(null);

            //Act
            var result = controller.Create();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Null((result as ViewResult).Model);
        }

        [Fact]
        public void GivenValidModel_WhenPostCreateIsCalled_ThenServiceIsCalledOnceAndRedirectedToIndexView()
        {
            //Arrange
            var parkingZoneVM = new ParkingZoneCreateVM()
            {
                Name = "Sharafshon",
                Address = "Andijon",
                Description = "Qimmat"
            };

            var mockService = new Mock<IParkingZoneService>();

            mockService.Setup(service => service.Insert(It.IsAny<ParkingZone>()));

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Create(parkingZoneVM);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockService.Verify(service => service.Insert(It.IsAny<ParkingZone>()), Times.Once);
        }

        [Fact]
        public void GivenModelWithNullName_WhenPostCreateIsCalled_ThenCreateViewReturnedAndModelStateIsInvalid()
        {
            //Arrange
            var parkingZoneVM = new ParkingZoneCreateVM()
            {
                Address = "Andijon",
                Description = "Qimmat"
            };

            var controller = new ParkingZoneController(Mock.Of<IParkingZoneService>());

            controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var result = controller.Create(parkingZoneVM);

            //Assert
            Assert.False(controller.ModelState.IsValid);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(JsonSerializer.Serialize(parkingZoneVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }

        [Fact]
        public void GivenModelWithNullAddress_WhenPostCreateIsCalled_ThenCreateViewReturnedAndModelStateIsInvalid()
        {
            //Arrange
            var parkingZoneVM = new ParkingZoneCreateVM()
            {
                Name = "Sharafshon",
                Description = "Qimmat"
            };

            var controller = new ParkingZoneController(Mock.Of<IParkingZoneService>());

            controller.ModelState.AddModelError("Address", "Address is required");

            //Act
            var result = controller.Create(parkingZoneVM);

            //Assert
            Assert.False(controller.ModelState.IsValid);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(JsonSerializer.Serialize(parkingZoneVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }
        #endregion

        #region Edit
        [Fact]
        public void GivenIdOfNotExistingParkingZone_WhenEditIsCalled_ThenReturnedNotFoundResultAndServiceIsCalledOnce()
        {
            //Arrange
            var mockService = new Mock<IParkingZoneService>();

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Edit(testId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockService.Verify(service => service.GetById(testId), Times.Once);
        }

        [Fact]
        public void GivenId_WhenGetEditIsCalled_ThenNotEmptyViewResultIsReturned()
        {
            //Arrange
            var expectedParkingZoneEditVM = new ParkingZoneEditVM()
            {
                Id = testId,
                Name = "Sharafshon",
                Address = "Andijon",
                Description = "Arzon"
            };

            var mockService = new Mock<IParkingZoneService>();

            mockService.Setup(service => service.GetById(testId)).Returns(testParkingZone);

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Edit(testId);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ParkingZoneEditVM>((result as ViewResult).Model);

            var model = (result as ViewResult).Model as ParkingZoneEditVM;

            Assert.Equal(JsonSerializer.Serialize(expectedParkingZoneEditVM), JsonSerializer.Serialize(model));
            mockService.Verify(service => service.GetById(testId), Times.Once);
        }

        [Fact]
        public void GivenIdAndModelWithDifferentId_WhenPostEditIsCalled_ThenReturnedNotFoundResult()
        {
            //Arrange
            var parkingZoneEditVM = new ParkingZoneEditVM()
            {
                Id = Guid.NewGuid()
            };

            var controller = new ParkingZoneController(Mock.Of<IParkingZoneService>());

            //Act
            var result = controller.Edit(testId, parkingZoneEditVM);

            //Assert
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void GivenIdOfNotExistingParkingZoneAndValidModel_WhenPostEditIsCalled_ThenReturnedNotFoundAndServiceIsCalledOnce()
        {
            //Arrange
            var parkingZoneEditVM = new ParkingZoneEditVM()
            {
                Id = testId,
                Name = "Sharafshon",
                Address = "Andijon",
                Description = "Arzon"
            };

            var mockService = new Mock<IParkingZoneService>();

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Edit(testId, parkingZoneEditVM);

            //Asert
            Assert.True(result is NotFoundResult);
            mockService.Verify(service => service.GetById(testId), Times.Once);
        }

        [Fact]
        public void GivenIdAndModelWithNullName_WhenPostEditIsCalled_ThenEditViewReturnedAndModelStateIsInvalid()
        {
            //Arrange
            var parkingZoneEditVM = new ParkingZoneEditVM()
            {
                Id = testId,
                Name =  null,
                Address = "Andijon",
                Description = "Arzon"
            };

            var controller = new ParkingZoneController(Mock.Of<IParkingZoneService>());

            controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var result = controller.Edit(testId, parkingZoneEditVM);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(JsonSerializer.Serialize(parkingZoneEditVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }

        [Fact]
        public void GivenIdAndModelWithNullAddress_WhenPostEditIsCalled_ThenEditViewReturnedAndModelStateIsInvelid()
        {
            //Arrange
            var _testId = new Guid("dd09a090-b0f6-4369-b24a-656843d227bc");

            var pakingZoneEditVM = new ParkingZoneEditVM()
            {
                Id = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Name = "Sharafshon",
                Address = "Andijon",
                Description = "Arzon"
            };

            var mockService = new Mock<IParkingZoneService>();

            var controller = new ParkingZoneController(mockService.Object);

            controller.ModelState.AddModelError("Address", "Address is required");

            //Act
            var result = controller.Edit(_testId, pakingZoneEditVM);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(JsonSerializer.Serialize(pakingZoneEditVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }

        [Fact]
        public void GivenIdAndValidModel_WhenPostEditIsCalled_ThenServiceIsCalledTwiceAndRedirectedToIndexView()
        {
            //Arrange
            var _testId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc");

            var _testParkingZone = new ParkingZoneEditVM()
            {
                Id = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Name = "Sharafshon",
                Address = "Andijon",
                Description = "Arzon"
            };

            var mockService = new Mock<IParkingZoneService>();

            mockService
                .Setup(service => service.GetById(testId))
                .Returns(testParkingZone);
            mockService
                .Setup(service => service.Update(It.IsAny<ParkingZone>()));

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Edit(_testId, _testParkingZone);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            mockService.Verify(service => service.GetById(testId), Times.Once);
            mockService.Verify(service => service.Update(It.IsAny<ParkingZone>()), Times.Once);
        }
        #endregion

        #region Delete
        [Fact]
        public void GivenId_WhenGetDeleteIsCalledAndServiceIsReturnedNull_ThenReturnedNotFoundResultAndServiceIsCalledOnce()
        {
            //Arrange
            var mockService = new Mock<IParkingZoneService>();

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Delete(testId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockService.Verify(service => service.GetById(testId), Times.Once);
        }

        [Fact]
        public void GivenId_WhenGetDeleteIsCalled_ThenNotEmptyViewResultIsReturned()
        {
            //Arrange
            var _testId = Guid.Parse("02a3f2c6-25d6-4d9d-9f0c-43b2d96eecd1");
            string testName = "Sharafshon";
            string testAdress = "Andijon";
            string testDescription = "Arzon";

            var mockService = new Mock<IParkingZoneService>();
            mockService.Setup(service => service.GetById(testId)).Returns(testParkingZone);

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.Delete(testId);

            //Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ParkingZone>((result as ViewResult).Model);
            Assert.Equal(testId, model.Id);
            Assert.Equal(testName, model.Name);
            Assert.Equal(testAdress, model.Address);
            Assert.Equal(testDescription, model.Description);
        }

        [Fact]
        public void GivenIdOfExistingZone_WhenPostDeleteConfirmedIsCalled_ThenReturnedNotFoundServiceIsCalledOnce()
        {
            //Arrange
            var mockService = new Mock<IParkingZoneService>();
            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.DeleteConfirmed(testId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockService.Verify(service => service.GetById(testId), Times.Once());
        }

        [Fact]
        public void GivenId_WhenPostDeleteConfirmedIsCalled_ThenServiceCalledTwiceAndRedirectedToIndexView()
        {
            //Arrange
            var mockService = new Mock<IParkingZoneService>();

            mockService
                .Setup(service => service.GetById(testId)).Returns(testParkingZone);

            mockService
                .Setup(service => service.Delete(It.IsAny<ParkingZone>()));

            var controller = new ParkingZoneController(mockService.Object);

            //Act
            var result = controller.DeleteConfirmed(testId);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Null(redirectToActionResult.ControllerName);
            mockService.Verify(service => service.GetById(testId), Times.Once);
            mockService.Verify(service => service.Delete(It.IsAny<ParkingZone>()), Times.Once);
        }
        #endregion
    }
}
