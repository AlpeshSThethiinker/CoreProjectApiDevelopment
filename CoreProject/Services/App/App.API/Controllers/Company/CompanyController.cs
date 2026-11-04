using App.Application.Features.Company.Commands.AddCompany;
using App.Application.Features.Company.Commands.DeleteCompany;
using App.Application.Features.Company.Commands.UpdateCompany;
using App.Application.Features.Company.Queries.GetAllCompany;
using App.Application.Features.Company.Queries.GetCompanyById;
using App.Infrastructure.Messages;
using Lib.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {

        public CompanyController(IMediator mediator) : base(mediator)
        {
        }

        #region httppost insert company
        [HttpPost]
        [Route("insertcompany")]
        public async Task<ActionResult<ResponseModel>> InsertCompany([FromBody] AddCompanyCommand command)
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


        #region httpput update company
        [HttpPost]
        [Route("updatecompany")]
        public async Task<ActionResult<ResponseModel>> UpdateCompany([FromBody] UpdateCompanyCommand command)
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


        #region httpdelete delete company
        [HttpPost]
        [Route("deletecompany")]
        public async Task<ActionResult<ResponseModel>> DeleteCompany([FromBody] DeleteCompanyCommand command)
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


        #region httpPost get company
        [HttpGet]
        [Route("getAllCompany")]
        public async Task<ActionResult<ResponseModel>> GetCompany()
        {
            ResponseModel response = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Message = ResponseMessages.InvalidParameters;
                    return BadRequest(response);
                }
                response = await _mediator.Send(new GetAllCompanyQuery());
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Message = ResponseMessages.ExceptionOccured + ex.Message;
                return BadRequest(response);
            }
        }
        #endregion


        #region httpostbyid company
        [HttpGet]
        [Route("getcompanybyid")]
        public async Task<ActionResult<ResponseModel>> GetByIdCompany(string companyId)
        {
            ResponseModel response = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Message = ResponseMessages.InvalidParameters;
                    return BadRequest(response);
                }

                response = await _mediator.Send(new GetCompanyByIdQuery() { CompanyId = companyId });
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
