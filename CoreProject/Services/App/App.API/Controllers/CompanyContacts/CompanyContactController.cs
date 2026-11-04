using App.Application.Features.CompanyAddress.Commands.AddCompanyAddress;
using App.Application.Features.CompanyContacts.Commands.DeleteCompanyContacts;
using App.Application.Features.CompanyContacts.Commands.UpdateCompanyContacts;
using App.Application.Features.CompanyContacts.Queries.GetCompanyContactsById;
using App.Infrastructure.Messages;
using Lib.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.CompanyContacts
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyContactController : BaseController
    {

        public CompanyContactController(IMediator mediator) : base(mediator)
        {
        }

        #region httppost insert company Contact
        [HttpPost]
        [Route("insertcompanyContact")]
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


        #region httpput update company Contact
        [HttpPost]
        [Route("updatecompanyContact")]
        public async Task<ActionResult<ResponseModel>> UpdateCompanyContact([FromBody] UpdateCompanyContactCommand command)
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


        #region httpdelete delete company Contact
        [HttpPost]
        [Route("deletecompanyContact")]
        public async Task<ActionResult<ResponseModel>> DeleteCompanyContact([FromBody] DeleteCompanyContactCommand command)
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


        #region httpostbyid company Contact
        [HttpPost]
        [Route("getcompanyContactbyid")]
        public async Task<ActionResult<ResponseModel>> GetCompanyContactByid([FromBody] GetCompanyContactByIdQuery command)
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
