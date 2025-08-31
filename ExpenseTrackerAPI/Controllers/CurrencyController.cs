using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Dto;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/Configs/Currency")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        protected APIResponse _response;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _response = new();
        }

        #region Get Currencies
        /// <summary>
        /// Get All Currencies
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet, Route("GetAllCurrencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetCurrencies(int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Currency> currencyList;
                currencyList = await _currencyService.GetAllAsync(pageSize, pageNumber);

                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.result = currencyList;
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

        /// <summary>
        /// Get One currency
        /// </summary>
        /// <param name="id"> Id of the curency</param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet, Route("GetCurrency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCurrency(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var expense = await _currencyService.GetAsync(d => d.Id == id);

                if (expense == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.result = expense;
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

        #region Create Currency
        [HttpPost, Route("CreateCurrency")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateCurrency([FromBody] Currency currency)
        {
            try
            {
                if (currency == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var existingCurrency = await _currencyService.GetAsync(e => e.Acronym == currency.Acronym);

                if (existingCurrency != null)
                {
                    _response.StatusCode = HttpStatusCode.Conflict; // 409 Conflict
                    _response.ErrorMessages = new List<string> { "currency already exists." };
                    return BadRequest(_response);
                }

                // Add the new expense
                await _currencyService.CreateAsync(currency);
                await _currencyService.SaveAsync();

                _response.result = currency;
                _response.StatusCode = HttpStatusCode.Created;
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
        #endregion

        #region Delete
        [HttpDelete, Route("DeleteCurrency/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteCurrency(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var currency = await _currencyService.GetAsync(d => d.Id == id);
                if (currency == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _currencyService.DeleteAsync(currency);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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

        #region Update
        [HttpPut, Route("UpdateCurrency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateCurrency(int id, [FromBody] Currency currency)
        {
            try
            {
                if (currency == null || id != currency.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _currencyService.UpdateAsync(id, currency) != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Currency not found." };
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
