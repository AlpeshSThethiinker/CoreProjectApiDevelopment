using App.Application.Features.CompanyAddress.Commands.AddCompanyAddress;
using App.Application.Features.CompanyContacts.Commands.DeleteCompanyContacts;
using App.Application.Features.CompanyContacts.Commands.UpdateCompanyContacts;
using App.Application.Features.CompanyDetails.Commands.AddDetailsAddress;
using App.Application.Features.CompanyDetails.Commands.DeleteDetailsAddress;
using App.Application.Features.CompanyDetails.Commands.UpdateDetailsAddress;
using App.Application.Features.CompanyDetails.Queries.GetCompanyDetailsById;
using App.Infrastructure.Messages;
using Lib.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.CompanyDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDetailsController : BaseController
    {
        public CompanyDetailsController(IMediator mediator) : base(mediator)
        {
        }

        #region httppost insert company Details
        [HttpPost]
        [Route("insertcompanyDetails")]
        public async Task<ActionResult<ResponseModel>> InsertCompanyDetails([FromBody] AddCompanyDetailsCommand command)
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


        #region httpput update company Details
        [HttpPost]
        [Route("updatecompanyDetails")]
        public async Task<ActionResult<ResponseModel>> UpdateCompanyDetails([FromBody] UpdateCompanyDetailsCommand command)
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


        #region httpdelete delete company Details
        [HttpPost]
        [Route("deletecompanyDetails")]
        public async Task<ActionResult<ResponseModel>> DeleteCompanyDetails([FromBody] DeleteCompanyDetailsCommand command)
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


        #region httpostbyid company Details
        [HttpPost]
        [Route("getcompanyDetailsbyid")]
        public async Task<ActionResult<ResponseModel>> GetCompanyDetailsByid([FromBody] GetCompanyDetailsByIdQuery command)
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
