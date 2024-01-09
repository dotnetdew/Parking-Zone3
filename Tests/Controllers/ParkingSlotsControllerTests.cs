using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking_Zone.Areas.Admin.Controllers;
using Parking_Zone.Enums;
using Parking_Zone.Models;
using Parking_Zone.Services;
using Parking_Zone.ViewModels.ParkingSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests.Controllers
{
    public class ParkingSlotsControllerTests
    {
        private readonly Guid _testParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc");
        private readonly Guid _testParkingSlotId = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5");
        private readonly ParkingSlot _testParkingSlot = new ParkingSlot()
        {
            Id = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5"),
            Number = 1,
            ParkingZone = new ParkingZone()
            {
                Id = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Name = "Sharafshon",
                Address = "Andijon",
                Description = "Arzon"
            },
            ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
            Category = SlotCategoryEnum.Business,
            IsAvailableForBooking = true,
        };
        private readonly ParkingZone _testParkingZone = new ParkingZone()
        {
            Id = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
            Name = "Sharafshon",
            Address = "Andijon",
            Description = "Arzon"
        };
        private readonly IEnumerable<ParkingSlot> _testParkingSlots = new List<ParkingSlot>()
        {
            new ParkingSlot()
            {
                Id = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5"),
                Number = 1,
                ParkingZone = new ParkingZone() { },
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Category = SlotCategoryEnum.Business,
                IsAvailableForBooking = true
            },
            new ParkingSlot()
            {
                Id = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5"),
                Number = 1,
                ParkingZone = new ParkingZone() { },
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Category = SlotCategoryEnum.Business,
                IsAvailableForBooking = true
            },
        };

        #region Index
        [Fact]
        public void GivenParkingZoneId_WhenIndexIsCalled_ThenParkingZoneAndParkingSlotServicesCallledOnceAndReturnedNotEmptyViewResult()
        {
            //Arrange
            var mockParkingZoneService = new Mock<IParkingZoneService>();
            var mockParkingSlotService = new Mock<IParkingSlotService>();

            mockParkingZoneService.Setup(service => service.GetById(_testParkingZoneId)).Returns(_testParkingZone);
            mockParkingSlotService.Setup(service => service.GetByParkingZoneId(_testParkingZoneId)).Returns(_testParkingSlots);

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Index(_testParkingZoneId);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull((result as ViewResult).Model);
            mockParkingZoneService.Verify(s => s.GetById(_testParkingZoneId), Times.Once);
            mockParkingSlotService.Verify(s => s.GetByParkingZoneId(_testParkingZoneId), Times.Once);
        }
        #endregion

        #region Details
        [Fact]
        public void GivenSlotId_WhenDetailsIsCalled_ThenServiceIsCalledOnceAndReturnedViewResultWithDetailsParkingSlotVmModel()
        {
            //Arrange
            var expectedParkingSlotDetailsVM = new ParkingSlotDetailsVM()
            {
                Id = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5"),
                Number = 1,
                ParkingZoneName = "Sharafshon",
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Category = SlotCategoryEnum.Business,
                IsAvailableForBooking = true,
            };

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            mockParkingSlotService.Setup(service => service.GetById(_testParkingSlotId)).Returns(_testParkingSlot);

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Details(_testParkingSlotId);

            //Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ParkingSlotDetailsVM>((result as ViewResult).Model);
            Assert.Equal(JsonSerializer.Serialize(expectedParkingSlotDetailsVM), JsonSerializer.Serialize(model));
            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
        }

        [Fact]
        public void GivenIdOfNotExistingParkingSlot_WhenDetailsIsCalled_ThenServiceIsCalledOnceAndReturnedNotFoundResult()
        {
            //Arrange
            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Details(_testParkingSlotId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
        }
        #endregion

        #region Create
        [Fact]
        public void GivenParkingZoneId_WhenGetCreateIsCalled_ThenParkingZoneServiceIsCalledAndReturnedNotEmptyViewResult()
        {
            //Arrange
            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            mockParkingZoneService.Setup(service => service.GetById(_testParkingZoneId)).Returns(_testParkingZone);

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Create(_testParkingZoneId);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull((result as ViewResult).Model);
            mockParkingZoneService.Verify(s => s.GetById(_testParkingZoneId), Times.Once);
        }

        [Fact]
        public void GivenValidModel_WhenPostCreateIsCalled_ThenParkingSlotServiceIsCalledOnceAndRedirectedToIndexView()
        {
            //Arrange
            var parkingSlotCreateVM = new ParkingSlotCreateVM()
            {
                Number = 1,
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Category = SlotCategoryEnum.Business,
                IsAvailableForBooking = true
            };

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            mockParkingSlotService.Setup(service => service.Insert(It.IsAny<ParkingSlot>()));

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Create(parkingSlotCreateVM);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("ParkingSlots", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockParkingSlotService.Verify(s => s.Insert(It.IsAny<ParkingSlot>()), Times.Once);
        }

        [Fact]
        public void GivenModelWithNullNumber_WhenPostCreateIsCalled_ThenCreateViewReturnedAndModelStateIsInvalid()
        {
            //Arrange
            var parkingSlotCreateVM = new ParkingSlotCreateVM()
            {
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Category = SlotCategoryEnum.Business,
                IsAvailableForBooking = true
            };

            var controller = new ParkingSlotsController(Mock.Of<IParkingZoneService>(), Mock.Of<IParkingSlotService>());

            controller.ModelState.AddModelError("Number", "Number of Slot is Required");

            //Act
            var result = controller.Create(parkingSlotCreateVM);

            //Assert
            Assert.False(controller.ModelState.IsValid);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(JsonSerializer.Serialize(parkingSlotCreateVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }

        [Fact]
        public void GivenModelWithNullCategory_WhenPostCreateIsCalled_ThenCreateViewReturnedAndModelStateIsInvalid()
        {
            //Arrange
            var parkingSlotCreateVM = new ParkingSlotCreateVM()
            {
                Number = 1,
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                IsAvailableForBooking = true
            };

            var controller = new ParkingSlotsController(Mock.Of<IParkingZoneService>(), Mock.Of<IParkingSlotService>());

            controller.ModelState.AddModelError("Category", "Category of Slot is Required");

            //Act
            var result = controller.Create(parkingSlotCreateVM);

            //Assert
            Assert.False(controller.ModelState.IsValid);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(JsonSerializer.Serialize(parkingSlotCreateVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }
        #endregion

        #region Edit
        [Fact]
        public void GivenIdOfNotExistingSlot_WhenEditIsCalled_ThenServiceIsCalledOnceAndReturnedNotFoundResult()
        {
            //Arrange
            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Edit(_testParkingSlotId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
        }

        [Fact]
        public void GivenId_WhenGetEditIsCalled_ThenNotEmptyViewResultIsReturned()
        {
            //Arrange
            var expextedParkingSlotEditVm = new ParkingSlotEditVM()
            {
                Id = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5"),
                Number = 1,
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Category = SlotCategoryEnum.Business,
                IsAvailableForBooking = true,
                ParkingZoneName = "Sharafshon"
            };

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            mockParkingSlotService.Setup(service => service.GetById(_testParkingSlotId)).Returns(_testParkingSlot);

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Edit(_testParkingSlotId);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ParkingSlotEditVM>((result as ViewResult).Model);

            var model = (result as ViewResult).Model as ParkingSlotEditVM;
            Assert.Equal(JsonSerializer.Serialize(expextedParkingSlotEditVm), JsonSerializer.Serialize(model));
            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
        }

        [Fact]
        public void GivenIdAndModelWithDifferentId_WhenPostEditIsCalled_ThenReturnedNotFoundResult()
        {
            //Arrange
            var parkingSlotEditVM = new ParkingSlotEditVM()
            {
                Id = Guid.NewGuid()
            };

            var controller = new ParkingSlotsController(Mock.Of<IParkingZoneService>(), Mock.Of<IParkingSlotService>());

            //Act
            var result = controller.Edit(_testParkingSlotId, parkingSlotEditVM);

            //Assert
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void GivenIdOfNotExistingSlotAndValidModel_WhenEditIsCalled_ThenReturnedNotFoundResultAndSlotServiceCalledOnce()
        {
            //Arrange
            var parkingSlotEditVM = new ParkingSlotEditVM()
            {
                Id = _testParkingSlotId,
                Number = 22,
                Category = SlotCategoryEnum.Standard,
                IsAvailableForBooking = true,
                ParkingZoneName = "Sharafshon"
            };

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Edit(_testParkingSlotId, parkingSlotEditVM);

            //Assert
            Assert.True(result is NotFoundResult);
            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
        }

        [Fact]
        public void GivenIdAndModelWithNullNumber_WhenPostEditIsCalled_ThenEditViewIsReturnedAndModelStateIsInValid()
        {
            //Arrange
            var parkingSlotEditVM = new ParkingSlotEditVM()
            {
                Id = _testParkingSlotId,
                Category = SlotCategoryEnum.Standard,
                IsAvailableForBooking = true,
                ParkingZoneName = "Sharafshon"
            };

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            controller.ModelState.AddModelError("Number", "Number is Required");

            //Act
            var result = controller.Edit(_testParkingSlotId, parkingSlotEditVM);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(JsonSerializer.Serialize(parkingSlotEditVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }

        [Fact]
        public void GivenIdAndModelWithNullCategory_WhenPostEditIsCalled_ThenEditViewIsReturnedAndModelStateIsInValid()
        {
            //Arrange
            var parkingSlotEditVM = new ParkingSlotEditVM()
            {
                Id = _testParkingSlotId,
                Number = 22,
                IsAvailableForBooking = true,
                ParkingZoneName = "Sharafshon"
            };

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            controller.ModelState.AddModelError("Category", "Category is Required");

            //Act
            var result = controller.Edit(_testParkingSlotId, parkingSlotEditVM);

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(JsonSerializer.Serialize(parkingSlotEditVM), JsonSerializer.Serialize((result as ViewResult).Model));
        }

        [Fact]
        public void GivenIdAndValidModel_WhenPostEditIsCalled_ThenServiceIsCalledTwiceAndRedirectedToIndexView()
        {
            //Arrange
            var parkingSlotEditVM = new ParkingSlotEditVM()
            {
                Id = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5"),
                Number = 1,
                ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
                Category = SlotCategoryEnum.Business,
                IsAvailableForBooking = true,
            };

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            mockParkingSlotService.Setup(service => service.GetById(_testParkingSlotId)).Returns(_testParkingSlot);

            mockParkingSlotService.Setup(service => service.Update(It.IsAny<ParkingSlot>()));

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Edit(_testParkingSlotId, parkingSlotEditVM);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.Equal("ParkingSlots", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
            mockParkingSlotService.Verify(s => s.Update(It.IsAny<ParkingSlot>()), Times.Once);
        }
        #endregion

        #region Delete
        [Fact]
        public void GivenId_WhenGetDeleteIsCalled_ThenReturnedNotFoundResultAndServiceIsCalledOnce()
        {
            //Arrange
            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Delete(_testParkingSlotId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
        }

        [Fact]
        public void GivenId_WhenGetDeleteIsCalled_ThenNotEmptyViewResultIsReturned()
        {
            //Arrange
            Guid Id = Guid.Parse("ab8e46f4-a343-4571-a1a5-14892bccc7f5");
            int Number = 1;
            Guid ParkingZoneId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc");
            bool IsAvailableForBooking = true;

            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            mockParkingSlotService.Setup(service => service.GetById(_testParkingSlotId)).Returns(_testParkingSlot);

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.Delete(_testParkingSlotId);

            //Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ParkingSlotDetailsVM>((result as ViewResult).Model);

            Assert.Equal(Id, model.Id);
            Assert.Equal(Number, model.Number);
            Assert.Equal(ParkingZoneId, model.ParkingZoneId);
            Assert.Equal(IsAvailableForBooking, model.IsAvailableForBooking);
        }

        [Fact]
        public void GivenIdOfNotExistingSlot_WhenDeleteConfirmedIsCalled_ThenNotFoundResultIsReturnedAndServiceIsCalledOnce()
        {
            //Arrange
            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.DeleteConfirmed(_testParkingSlotId, _testParkingZoneId);

            //Assert
            Assert.True(result is NotFoundResult);
            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
        }

        [Fact]
        public void GivenIds_WhenDeleteConfirmedIsCalled_ThenSlotServiceIsCalledTwiceAndRedirectedToIndexView()
        {
            //Arrange
            var mockParkingSlotService = new Mock<IParkingSlotService>();
            var mockParkingZoneService = new Mock<IParkingZoneService>();

            mockParkingSlotService.Setup(service => service.GetById(_testParkingSlotId)).Returns(_testParkingSlot);
            mockParkingSlotService.Setup(service => service.Delete(It.IsAny<ParkingSlot>()));

            var controller = new ParkingSlotsController(mockParkingZoneService.Object, mockParkingSlotService.Object);

            //Act
            var result = controller.DeleteConfirmed(_testParkingSlotId, _testParkingZoneId);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.Equal("ParkingSlots", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            mockParkingSlotService.Verify(s => s.GetById(_testParkingSlotId), Times.Once);
            mockParkingSlotService.Verify(s => s.Delete(It.IsAny<ParkingSlot>()), Times.Once);
        }
        #endregion
    }
}
