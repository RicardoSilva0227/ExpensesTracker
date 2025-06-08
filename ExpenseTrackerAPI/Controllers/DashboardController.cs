using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/Dashboard")]
    [ApiController]
    public class DashboardController : Controller
    {
        protected APIResponse _response;
        private readonly IWalletService _walletService;

        public DashboardController(IWalletService walletService)
        {
            _walletService = walletService;
            _response = new();
        }


        #region Get Wallet Summary
        [HttpGet, Route("GetWalletSummary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<APIResponse>> getWalletSummary(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var wallet = await _walletService.GetWalletSummary(id);

                if (wallet == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.result = wallet;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        #endregion
    }
}
