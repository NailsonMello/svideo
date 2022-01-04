using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SVideo.API.Models.Request;
using SVideo.API.Models.Responses;
using SVideo.Application.Interfaces;
using SVideo.Application.Models;
using SVideo.Application.Models.ViewModels;
using SVideo.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace SVideo.API.Controllers
{
    /// <summary>
    /// Server controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ServersController : ControllerBase
    {


        private readonly IMapper _mapper;
        private readonly IServerService _service;
        private readonly IVideoService _videoservice;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="service"></param>
        /// <param name="videoservice"></param>
        public ServersController(IMapper mapper, IServerService service, IVideoService videoservice)
        {
            _mapper = mapper;
            _service = service;
            _videoservice = videoservice;
        }


        /// <summary>
        /// Get server by id
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Server by id</returns>
        [ProducesResponseType(typeof(ApplicationResponseItem<ServerViewModel>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{serverId}")]
        public async Task<ActionResult> GetById(Guid serverId) => Ok(await _service.GetAsyncById(serverId));

        /// <summary>
        /// List server
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>List server</returns>
        [ProducesResponseType(typeof(ApplicationResponseList<ServerViewModel>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetAll() => Ok(await _service.GetAsyncAll());

        /// <summary>
        /// Check available server
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Server is available</returns>
        [ProducesResponseType(typeof(ApplicationResponseStruct<bool>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("available/{serverId}")]
        public async Task<ActionResult> IsAvailable(Guid serverId) => Ok(await _service.IsAvailable(serverId));

        /// <summary>
        /// Create new Server
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Server Created</returns>
        [ProducesResponseType(typeof(ApplicationResponseStruct<Guid>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("/api/Server")]
        public async Task<ActionResult> Create([FromBody] ServerRequest request)
        => Ok(await _service.CreateAsync(_mapper.Map<ServerCreateDto>(request)));

        /// <summary>
        /// Update Server
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Changed server</returns>
        [ProducesResponseType(typeof(ApplicationResponseStruct<Guid>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPut("{serverId}")]
        public async Task<ActionResult> Update(Guid serverId, [FromBody] ServerRequest request)
        => Ok(await _service.UpdateAsync(serverId, _mapper.Map<ServerUpdateDto>(request)));

        /// <summary>
        /// Removed server
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Removed server</returns>
        [ProducesResponseType(typeof(ApplicationResponseStruct<bool>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpDelete("{serverId}")]
        public async Task<ActionResult> Delete(Guid serverId) => Ok(await _service.DeleteAsync(serverId));


        /// <summary>
        /// Create new Video
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Video Created</returns>
        [ProducesResponseType(typeof(ApplicationResponseStruct<Guid>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("{serverId}/videos")]
        public async Task<ActionResult> CreateVideo(Guid serverId, [FromForm] VideoRequest request)
        => Ok(await _videoservice.CreateVideoAsync(serverId, _mapper.Map<VideoCreateDto>(request)));

        /// <summary>
        /// Removed video
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Removed video</returns>
        [ProducesResponseType(typeof(ApplicationResponseStruct<bool>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpDelete("{serverId}/videos/{videoId}")]
        public async Task<ActionResult> DeleteVideo(Guid serverId, Guid videoId) => Ok(await _videoservice.DeleteAsync(serverId, videoId));

        /// <summary>
        /// List videos
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>List videos</returns>
        [ProducesResponseType(typeof(ApplicationResponseList<VideoViewModel>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{serverId}/videos")]
        public ActionResult GetAllVideosByServerId(Guid serverId) => Ok(_videoservice.GetAllByServerId(serverId));

        /// <summary>
        /// Get video by id and server id
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>video by id and server id</returns>
        [ProducesResponseType(typeof(ApplicationResponseItem<VideoViewModel>), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{serverId}/videos/{videoId}")]
        public ActionResult GetVideoByIdAndServerId(Guid serverId, Guid videoId) => Ok(_videoservice.GetByIdAndServerId(serverId, videoId));

        /// <summary>
        /// Download video
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Download video</returns>
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{serverId}/videos/{videoId}/binary")]
        public FileContentResult Download(Guid serverId, Guid videoId)
        {
            DownloadVideoViewModel result = _videoservice.Download(serverId, videoId);
            return result != null ?  File(result.SizeInBytes, result.ContentType) : File(new byte[] { }, "video/mp4");
        }
    }
}
