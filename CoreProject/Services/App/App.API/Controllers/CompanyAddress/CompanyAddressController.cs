using App.Application.Features.CompanyAddress.Commands.AddCompanyAddress;
using App.Application.Features.CompanyAddress.Commands.DeleteCompanyAddress;
using App.Application.Features.CompanyAddress.Commands.UpdateCompanyAddress;
using App.Application.Features.CompanyAddress.Queries.GetCompanyAddressById;
using App.Infrastructure.Messages;
using Lib.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.CompanyAddress
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyAddressController : BaseController
    {
        public CompanyAddressController(IMediator mediator) : base(mediator)
        {
        }

        #region httppost insert company Address
        [HttpPost]
        [Route("insertcompanyAddress")]
        public async Task<ActionResult<ResponseModel>> InsertCompanyAddress([FromBody] AddCompanyAddressCommand command)
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


        #region httpput update company Address
        [HttpPost]
        [Route("updatecompanyAddress")]
        public async Task<ActionResult<ResponseModel>> UpdateCompanyAddress([FromBody] UpdateCompanyAddressCommand command)
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


        #region httpdelete delete company Address
        [HttpPost]
        [Route("deletecompanyAddress")]
        public async Task<ActionResult<ResponseModel>> DeleteCompanyAddress([FromBody] DeleteCompanyAddressCommand command)
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


        #region httpostbyid company Address
        [HttpPost]
        [Route("getcompanyAddressbyid")]
        public async Task<ActionResult<ResponseModel>> GetCompanyAddressByid([FromBody] GetCompanyAddressByIdQuery command)
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



    }
}
