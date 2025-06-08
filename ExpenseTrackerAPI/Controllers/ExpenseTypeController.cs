using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Dto;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/ExpensesTypes")]
    [ApiController]
    public class ExpenseTypeController : Controller
    {
        private readonly IExpenseTypeService _expensesTypeService;
        protected APIResponse _response;

        public ExpenseTypeController(IExpenseTypeService expenseTypeService)
        {
            _expensesTypeService = expenseTypeService;
            _response = new();
        }

        #region Get ExpensesTypes
        /// <summary>
        /// Get All ExpensesTypes
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet, Route("GetAllExpenseTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetExpenseTypes(int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<ExpenseType> expenseTypeList;
                expenseTypeList = await _expensesTypeService.GetAllAsync();

                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.result = expenseTypeList;
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
        /// Get One ExpenseType
        /// </summary>
        /// <param name="id"> Id of the expensesType</param>
        /// <returns></returns>
        [HttpGet, Route("GetExpenseType")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetExpenseType(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var expensesType = await _expensesTypeService.GetAsync(d => d.Id == id);

                if (expensesType == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.result = expensesType;
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

        #region Create ExpenseType
        [HttpPost, Route("CreateExpenseType")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateExpenseType([FromBody] ExpenseType model)
        {
            try
            {
                if (model == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var existingExpense = await _expensesTypeService.GetAsync(e => e.Code == model.Code);
                if (existingExpense != null)
                {
                    _response.StatusCode = HttpStatusCode.Conflict; // 409 Conflict
                    _response.ErrorMessages = new List<string> { "ExpenseType with the same Code already exists." };
                    return Conflict(_response);
                }

                // Add the new expensesType
                await _expensesTypeService.CreateAsync(model);
                await _expensesTypeService.SaveAsync();

                _response.result = model;
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
        [HttpDelete, Route("DeleteExpenseTypes/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteExpenseType(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var expensesType = await _expensesTypeService.GetAsync(d => d.Id == id);
                if (expensesType == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                 await _expensesTypeService.DeleteAsync(expensesType);
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
        [HttpPut, Route("UpdateExpenseType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateExpenseType(int id, [FromBody] ExpenseType expensesType)
        {
            try
            {
                if (expensesType == null || id != expensesType.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _expensesTypeService.UpdateAsync(id, expensesType) != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Expense not found." };
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
