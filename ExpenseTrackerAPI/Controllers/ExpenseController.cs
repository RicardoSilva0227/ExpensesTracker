using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Dto;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expensesService;
        protected APIResponse _response;


        public ExpenseController(IExpenseService expensesService)
        {
            _expensesService = expensesService;
            _response = new();
        }

        #region Get Expenses
        /// <summary>
        /// Get All Expenses
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetExpenses(int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Expense> expenseList;
                expenseList = await _expensesService.GetAllAsync();

                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.result = expenseList;
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
        /// Get One Expense
        /// </summary>
        /// <param name="id"> Id of the expense</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetExpense")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetExpense(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var expense = _expensesService.GetAsync(d => d.Id == id);

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

        #region Create Expense
        [HttpPost]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateExpense([FromBody] Expense expense)
        {
            try
            {
                if (expense == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var existingExpense = await _expensesService.GetAsync(e => e.Code == expense.Code);
                if (existingExpense != null)
                {
                    _response.StatusCode = HttpStatusCode.Conflict; // 409 Conflict
                    _response.ErrorMessages = new List<string> { "Expense with the same Code already exists." };
                    return Conflict(_response);
                }

                // Add the new expense
                await _expensesService.CreateAsync(expense);
                await _expensesService.SaveAsync();

                _response.result = expense;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetExpense", new { id = expense.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteExpense")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var expense = await _expensesService.GetAsync(d => d.Id == id);
                if (expense == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _expensesService.DeleteAsync(expense);
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
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] Expense expense)
        {
            try
            {
                if (expense == null || id != expense.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }                

                await _expensesService.UpdateAsync(expense);
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
