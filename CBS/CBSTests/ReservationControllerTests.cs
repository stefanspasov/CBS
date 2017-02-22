namespace CBSTests
{
    using System;
    using System.Web.Http.Results;

    using CBS.DAL.Models;
    using CBS.DAL.Repositories.Decorators.Interfaces;
    using CBS.Logic.Controllers;
    using CBS.Logic.Factories;
    using CBS.Logic.Handlers.Implementations;
    using CBS.Logic.Mappers.Implementations;
    using CBS.Logic.Models;
    using CBS.Logic.Models.DTOs;

    using FakeItEasy;

    using NUnit.Framework;

    public class ReservationControllerTests
    {
        private const decimal PricePerDay = 100m;
        private const decimal PricePerKilometer = 10.5m;
        private const int ReservationId = 10;

        private ReservationController reservationController;
        private IReservationRepository reservationRepository;

        [SetUp]
        public void SetUp()
        {
            this.reservationRepository = A.Fake<IReservationRepository>();
            var settingRepository = A.Fake<ISettingRepository>();
            A.CallTo(() => settingRepository.GetSetting<decimal>("PricePerDay")).Returns(PricePerDay);
            A.CallTo(() => settingRepository.GetSetting<decimal>("PricePerKilometer")).Returns(PricePerKilometer);
            A.CallTo(() => this.reservationRepository.MakeReservationAsync(A<Reservation>._)).Returns(ReservationId);

            var reservableFactory = new ReservableFactory(settingRepository);

            var reservationHandler = new ReservationHandler(
                new ReservationMapper(), 
                this.reservationRepository,
                reservableFactory);
            this.reservationController = new ReservationController(reservationHandler);
        }

        [Test]
        public void Should_return_reservation_number_from_reservation_respository_when_making_reservations()
        {
            // Act
            var bookingResponse =
                this.reservationController.MakeReservation(A.Dummy<MakeReservationDto>()).Result as
                OkNegotiatedContentResult<MakeReservationResponseDto>;
            var reservationId = bookingResponse?.Content.ReservationId;

            // Assert
            Assert.AreEqual(ReservationId, reservationId);
        }

        [TestCase(VehicleType.Small, ExpectedResult = 100)]
        [TestCase(VehicleType.Van, ExpectedResult = 1170)]
        [TestCase(VehicleType.MiniBuss, ExpectedResult = 1745)]
        public decimal Should_finalize_reservation_for_vehicle_type(VehicleType vehicleType)
        {
            // Arrange
            var updatedReservation = new Reservation
                                         {
                                             ReservationID = ReservationId,
                                             BookingDate = new DateTime(2017, 1, 25, 21, 30, 0),
                                             ReturnDate = new DateTime(2017, 1, 26, 21, 30, 0),
                                             BookingKilometers = 100,
                                             ReturnKilometers = 200,
                                             CustomerNumber = "12345",
                                             VehicleType = vehicleType
                                         };

            A.CallTo(() => this.reservationRepository.UpdateAndGetReservationAsync(ReservationId, A<DateTime>._, A<int>._))
                .Returns(updatedReservation);

            // Act
            var result =
                (this.reservationController.FinalizeReservation(
                    new UpdateReservationDto { ReservationId = ReservationId, ReturnDate = DateTime.Now}).Result as
                OkNegotiatedContentResult<UpdateReservationResponseDto>)?.Content;

            // Assert
            Assert.AreEqual(100, result?.KilometersTravelled);
            Assert.AreEqual(1, result?.NumberOfDays);
            return result.TotalPrice;
        }
    }
}