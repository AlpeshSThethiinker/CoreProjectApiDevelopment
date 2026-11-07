
using App.Application.Features.Addresses.Commands.AddAddress;
using App.Application.Features.Addresses.Commands.DeleteAddress;
using App.Application.Features.Addresses.Commands.UpdateAddress;
using App.Application.Features.Addresses.Queries.GetAllAddress;
using App.Application.Features.Addresses.Queries.GetAddressById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Lib.Core.Models.Response;
using App.Infrastructure.Messages;

namespace App.API.Controllers.Addresses;

[Route("api/[controller]")]
[ApiController]
public class AddressController : BaseController
{
    public AddressController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("InsertAddress")]
    public async Task<ActionResult<ResponseModel>> InsertAddress(
        [FromBody] AddAddressCommand model)
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
    [Route("UpdateAddress")]
    public async Task<ActionResult<ResponseModel>> UpdateAddress(
        [FromBody] UpdateAddressCommand model)
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
    [Route("DeleteAddress")]
    public async Task<ActionResult<ResponseModel>> DeleteAddress(long Id)
    {
        ResponseModel response = new();

        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            return await _mediator.Send(
                new DeleteAddressCommand { Id = Id }
            );
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }

    [HttpPost]
    [Route("GetAllAddresses")]
    public async Task<ActionResult<ResponseModel>> GetAllAddresses(
        [FromBody] GetAllAddressQuery model)
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
    [Route("GetAddressById")]
    public async Task<ActionResult<ResponseModel>> GetAddressById(long Id)
    {
        ResponseModel response = new();

        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            return await _mediator.Send(
                new GetAddressByIdQuery { Id = Id }
            );
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
}
