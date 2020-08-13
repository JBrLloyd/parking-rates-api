using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carpark.Register.WebUI.Controllers
{
    public class ParkingRateController : ApiController
    {
        /// <summary>
        /// Calculates Parking Rate and returns the best price.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/parkingrate
        ///     {
        ///        "entryDateTime": "2020-08-13T06:37:18.732Z",
        ///        "exitDateTime": "2020-08-13T07:37:18.732Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="entryDateTime">Datetime of park entry</param>
        /// <param name="exitDateTime">Datetime of park exit</param>
        /// <returns>A newly created calculated price</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">Incorrect Request Payload. Errors array show request payload failures.</response>    
        /// <response code="415">Unsupported Media Type - Accepts only application/json</response>    
        /// <response code="500">An error</response>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<GetParkingRateResponse>> Get([FromBody] GetParkingRateQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
