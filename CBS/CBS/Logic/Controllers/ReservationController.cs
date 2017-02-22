namespace CBS.Logic.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Filters;
    using Handlers;
    using Models.DTOs;


    [RoutePrefix("vehicle")]
    public class ReservationController : ApiController
    {
        private readonly IReservationHandler reservationHandler;

        public ReservationController(IReservationHandler reservationHandler)
        {
            if (reservationHandler == null)
            {
                throw new ArgumentNullException(nameof(reservationHandler));
            }
            this.reservationHandler = reservationHandler;
        }

        [HttpPost]
        [Route("book")]
        public async Task<IHttpActionResult> MakeReservation(MakeReservationDto makeReservation)
        {
            var result = await this.reservationHandler.MakeReservation(makeReservation);
            return this.Ok(result);
        }

        [HttpPatch]
        [Route("finalize")]
        [AuthorizationFilter]
        public async Task<IHttpActionResult> FinalizeReservation(UpdateReservationDto updateReservation)
        {
            var result = await this.reservationHandler.UpdateReservationGetTotalPrice(updateReservation);
            return this.Ok(result);
        }
    }
}
