
using App.Application.Features.AccountLedgers.Commands.AddAccountLedger;
using App.Application.Features.AccountLedgers.Commands.DeleteAccountLedger;
using App.Application.Features.AccountLedgers.Commands.UpdateAccountLedger;
using App.Application.Features.AccountLedgers.Queries.GetAllAccountLedger;
using App.Application.Features.AccountLedgers.Queries.GetAccountLedgerById;
using App.Infrastructure.Messages;
using Lib.Core.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace App.API.Controllers.AccountLedger;
    [Route("api/[controller]")]
    [ApiController]
public class AccountLedgerController : BaseController
{
    public AccountLedgerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("InsertAccountLedger")]
    public async Task<ActionResult<ResponseModel>> InsertAccountLedger([FromBody] AddAccountLedgerCommand model)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            return await _mediator.Send(model);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }

    [HttpPost]
    [Route("UpdateAccountLedger")]
    public async Task<ActionResult<ResponseModel>> UpdateAccountLedger([FromBody] UpdateAccountLedgerCommand model)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            return await _mediator.Send(model);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }

    [HttpDelete]
    [Route("DeleteAccountLedger")]
    public async Task<ActionResult<ResponseModel>> DeleteAccountLedger(long Id)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            return await _mediator.Send(new DeleteAccountLedgerCommand() { Id = Id });
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }

    [HttpPost]
    [Route("GetAllAccountLedger")]
    public async Task<ActionResult<ResponseModel>> GetAllAccountLedger([FromBody] GetAllAccountLedgerQuery model)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            return await _mediator.Send(model);
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }

    [HttpGet]
    [Route("GetAccountLedgerById")]
    public async Task<ActionResult<ResponseModel>> GetAccountLedgerById(long Id)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            return await _mediator.Send(new GetAccountLedgerByIdQuery() { AccountGroupId = Id });
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
}
