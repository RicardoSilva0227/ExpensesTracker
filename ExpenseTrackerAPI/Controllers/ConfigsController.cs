using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConfigTrackerAPI.Controllers
{
    [Route("api/Configs")]
    [ApiController]
    public class ConfigsController : Controller
    {
        private readonly IConfigService _configService;
        protected APIResponse _response;

        public ConfigsController(IConfigService configService)
        {
            _configService = configService;
            _response = new();
        }


        /// <summary>
        /// Get config
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet, Route("GetConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetConfig()
        {
            try
            {
                var config = await _configService.GetFirstOrDefault();
                if (config == null)
                {
                    config = new Configs
                    {
                        UseFtp = false,
                        EnableMultiCurrency = true,
                        EnableDiscounts = false,
                        Timezone = "UTC",
                        DateFormat = "dd/MM/yyyy"
                    };

                    await _configService.CreateAsync(config);
                    await _configService.SaveAsync();
                }

                _response.result = config;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpPut, Route("UpdateConfig")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateConfig([FromBody] Configs config)
        {
            try
            {
                if (config == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                var existing = await _configService.GetFirstOrDefault();
                if (existing == null)
                {
                    await _configService.CreateAsync(config);
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    await _configService.UpdateAsync(existing.Id, config);
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
    }
}
