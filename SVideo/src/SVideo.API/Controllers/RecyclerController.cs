using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SVideo.API.Models.Responses;
using SVideo.Application.Interfaces;

namespace SVideo.API.Controllers
{
    /// <summary>
    /// Recycler controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RecyclerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecyclerService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="service"></param>
        public RecyclerController(IMapper mapper, IRecyclerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// status running
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>status running</returns>
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("status")]
        public ActionResult Status() => Ok(_service.StatusRunning());

        /// <summary>
        /// Removed videos
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Removed videos</returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpDelete("process/{days}")]
        public ActionResult Delete(int days) {
            _service.RemoveVideo(days);
            return NoContent();
        }

        /// <summary>
        /// Update videos
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Update videos</returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPut]
        public ActionResult Update()
        {
            _service.UpdateRunning();
            return NoContent();
        }
    }
}
