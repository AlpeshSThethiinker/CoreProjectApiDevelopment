using App.Application.Features.Masters.MultiLevelList.Commission.Commands.DeleteCommission;
using App.Application.Features.Masters.MultiLevelList.Commission.Commands.InsertCommission;
using App.Application.Features.Masters.MultiLevelList.Commission.Commands.UpdateCommission;
using App.Application.Features.Masters.MultiLevelList.Commission.Commands.UpdateCommissionStatus;
using App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetAllCommission;
using App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetByCommissionType;
using App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetCommissionById;
using App.Infrastructure.Messages;
using Lib.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Masters.MultiLevelList.Commissions;

[Route("api/Commission/[controller]")]
[ApiController]
public class CommissionController : BaseController
{
    #region ctor
    public CommissionController(IMediator mediator) : base(mediator)
    {
    }
    #endregion

    #region httppost insert Commission
    [HttpPost]
    [Route("insertCommission")]
    public async Task<ActionResult<ResponseModel>> InsertCommission([FromBody] AddCommissionCommand command)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            response = await _mediator.Send(command);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion

    #region httpput update Commission
    [HttpPost]
    [Route("updateCommission")]
    public async Task<ActionResult<ResponseModel>> UpdateCommission([FromBody] UpdateCommissionCommand command)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            response = await _mediator.Send(command);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion

    #region httpdelete delete Commission
    [HttpPost]
    [Route("deleteCommission")]
    public async Task<ActionResult<ResponseModel>> DeleteCommission([FromBody] DeleteCommissionCommand command)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            response = await _mediator.Send(command);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion

    #region httpPost get All Commission
    [HttpGet]
    [Route("getAllCommission")]
    public async Task<ActionResult<ResponseModel>> GetAllCommission()
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            response = await _mediator.Send(new GetAllCommissionQuery());
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion

    #region httget byid commission
    [HttpGet]
    [Route("getCommissionById")]
    public async Task<ActionResult<ResponseModel>> GetByIdCommission(string id)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            response = await _mediator.Send(new GetCommissionByIdQuery() { MappingId = id });
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion

    #region httppost Commission Status update
    [HttpPost]
    [Route("CommissionStatusUpdate")]
    public async Task<ActionResult<ResponseModel>> UpdateCommissionStatus([FromBody] UpdateCommissionStatusCommand command)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            response = await _mediator.Send(command);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion

    #region httpget by Commission type
    [HttpPost]
    [Route("getbycommissiontype")]
    public async Task<ActionResult<ResponseModel>> GetByCommissionType([FromBody] GetByCommissionTypeQuery request)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            response = await _mediator.Send(request);
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