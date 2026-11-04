using App.Application.Features.Common.Dropdown.SaleStage;
using App.Infrastructure.Messages;
using Lib.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Dropdown;

[Route("api/[controller]")]
[ApiController]
public class DropdownController : BaseController
{
    #region ctor
    public DropdownController(IMediator mediator) : base(mediator)
    {
    }
    #endregion

    #region httpPost get all dropdown
    [HttpGet]
    [Route("getAllCompany")]
    public async Task<ActionResult<ResponseModel>> GetDropdown()
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            response = await _mediator.Send(new SaleStageQuery());
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion
}