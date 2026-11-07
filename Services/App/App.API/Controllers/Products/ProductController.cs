using App.Application.Features.Products.Commands.AddProduct;
using App.Application.Features.Products.Commands.DeleteProduct;
using App.Application.Features.Products.Commands.UpdateProduct;
using App.Application.Features.Products.Queries.GetAllProduct;
using App.Application.Features.Products.Queries.GetProductById;
using App.Infrastructure.Messages;
using Lib.Core.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductController : BaseController
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }


    #region insert
    [HttpPost]
    [Route("InsertProduct")]
    public async Task<ActionResult<ResponseModel>> InsertProduct([FromBody] AddProductCommand model)
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
    #endregion

    #region update
    [HttpPost]
    [Route("UpdateProduct")]
    public async Task<ActionResult<ResponseModel>> UpdateProduct([FromBody] UpdateProductCommand model)
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
    #endregion

    #region Delete
    [HttpDelete]
    [Route("deleteProduct")]
    public async Task<ActionResult<ResponseModel>> DeleteProduct(long Id)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }
            return await _mediator.Send(new DeleteProductCommand() { Id = Id });
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion

    #region getall
    [HttpPost]
    [Route("getallProduct")]
    public async Task<ActionResult<ResponseModel>> GetAllProduct([FromBody] GetAllProductQuery model)
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
    #endregion

    #region getbyid
    [HttpGet]
    [Route("GetbyidProduct")]
    public async Task<ActionResult<ResponseModel>> GetByIdProduct(long Id)
    {
        ResponseModel response = new();
        try
        {
            if (!ModelState.IsValid)
            {
                response.Message = ResponseMessages.InvalidParameters;
                return BadRequest(response);
            }

            return await _mediator.Send(new GetProductByIdQuery() { Id = Id });
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return BadRequest(response);
        }
    }
    #endregion



}
