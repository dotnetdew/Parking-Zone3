using Moq;
using Parking_Zone.Models;
using Parking_Zone.Repositories;
using Parking_Zone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class ParkingZoneServiceTests
    {
        public Guid testId = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc");

        public ParkingZone testParkingZone = new ParkingZone()
        {
            Id = Guid.Parse("dd09a090-b0f6-4369-b24a-656843d227bc"),
            Name = "Sharafshon",
            Address = "Andijon",
            Description = "Arzon"
        };
        #region GetAll
        [Fact]
        public void GivenNothing_WhenGetAllIsCalled_ThenRepositoryIsCalledOnceAndReturnedExpectedZones()
        {
            //Arrange
            var parkingZones = new List<ParkingZone>()
            {
                new ParkingZone()
                {
                    Id = testId,
                    Name = "Sharafshon",
                    Address = "Andijon",
                    Description = "Arzon"
                },
                new ParkingZone()
                {
                    Id = testId,
                    Name = "R7",
                    Address = "Farg'ona",
                    Description = "Qimmat"
                }
            };
            var mockRepository = new Mock<IParkingZoneRepository>();

            mockRepository
                .Setup(repo => repo.GetAll())
                .Returns(parkingZones);

            var service = new ParkingZoneService(mockRepository.Object);

            //Act
            var result = service.GetAll();

            //Asert
            Assert.Equal(JsonSerializer.Serialize(result), JsonSerializer.Serialize(parkingZones));
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
        #endregion

        #region GetById
        [Fact]
        public void GivenId_WhenGetByIdIsCalled_ThenRepositoryIsCalledOnceAndReturnedExpectedParkingZone()
        {
            //Arrange
            var mockRepository = new Mock<IParkingZoneRepository>();

            mockRepository
                .Setup(repo => repo.GetById(testId))
                .Returns(testParkingZone);

            var service = new ParkingZoneService(mockRepository.Object);

            //Act
            var result = service.GetById(testId);

            //Assert
            Assert.Equal(JsonSerializer.Serialize(testParkingZone), JsonSerializer.Serialize(result));
            mockRepository.Verify(repo => repo.GetById(testId), Times.Once);
        }
        #endregion

        #region Insert
        [Fact]
        public void GivenParkingZoneModel_WhenInsertIsCalled_ThenRepositoryIsCalledTwice()
        {
            //Arrange
            var parkingZone = new ParkingZone();

            var mockRepository = new Mock<IParkingZoneRepository>();

            mockRepository
                .Setup(repo => repo.Insert(parkingZone));
            mockRepository
                .Setup(repo => repo.Save());

            var service = new ParkingZoneService(mockRepository.Object);

            //Act
            service.Insert(parkingZone);

            //Assert
            mockRepository.Verify(repo => repo.Insert(parkingZone), Times.Once);
            mockRepository.Verify(repo => repo.Save(), Times.Once);
        }
        #endregion

        #region Update
        [Fact]
        public void GivenParkingZoneModel_WhenUpdateIsCalled_ThenRepositoryIsCalledTwice()
        {
            //Arrange
            var mockRepository = new Mock<IParkingZoneRepository>();

            mockRepository
                .Setup(repo => repo.Update(testParkingZone));
            mockRepository
                .Setup(repo => repo.Save());

            var service = new ParkingZoneService(mockRepository.Object);

            //Act
            service.Update(testParkingZone);

            //Assert
            mockRepository.Verify(repo => repo.Update(testParkingZone), Times.Once);
            mockRepository.Verify(repo => repo.Save(), Times.Once);
        }
        #endregion

        #region Delete
        [Fact]
        public void GivenParkingZoneModel_WhenDeleteIsCalled_ThenRepositoryIsCalledTwice()
        {
            //Arrange
            var mockRepository = new Mock<IParkingZoneRepository>();
            mockRepository
                .Setup(repo => repo.Delete(testParkingZone));
            mockRepository
                .Setup(repo => repo.Save());

            var service = new ParkingZoneService(mockRepository.Object);

            //Act
            service.Delete(testParkingZone);

            //Assert
            mockRepository.Verify(repo => repo.Delete(testParkingZone), Times.Once);
            mockRepository.Verify(repo => repo.Save(), Times.Once);
        }
        #endregion
    }
}
