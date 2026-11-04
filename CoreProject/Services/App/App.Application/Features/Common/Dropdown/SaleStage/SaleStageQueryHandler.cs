using App.Domain.Enums.Companies.GeneralInfo;
using App.Domain.ViewModels;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Common.Dropdown;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Common.Dropdown.SaleStage;

public class SaleStageQueryHandler : IRequestHandler<SaleStageQuery, ResponseModel>
{
    private readonly IDropdownService _dropdownService;

    public SaleStageQueryHandler(IDropdownService dropdownService)
    {
        _dropdownService = dropdownService;
    }

    #region Method
    public async Task<ResponseModel> Handle(SaleStageQuery request, CancellationToken cancellationToken)
    {
        //ResponseModel response = new();
        ResponseModel response = new();
        SaleStageQueryResponse responseModel = new();
        try
        {
            var stageList = Enum.GetValues(typeof(DefaultStage)).Cast<DefaultStage>().ToList();
            var customOrder = new List<DefaultStage> { DefaultStage.Lead, DefaultStage.Prospect, DefaultStage.Vendor };
            var dropdownData = new List<DropdownModel>();
            int i = 1;
            foreach (var item in stageList.OrderBy(a => customOrder.IndexOf(a)))
            {
                DropdownModel model = new()
                {
                    Id = i.ToString(),
                    Name = item.ToString(),
                };
                dropdownData.Add(model);
                i++;
            }
            await _dropdownService.GetAllDropdown();
            responseModel.StageList = dropdownData;
            response.Response = responseModel;
            response.IsSuccess = true;
            return response;

            /*
            var result = await _dropdownService.GetAllDropdown();
            response.Response = result;
            response.IsSuccess = true;
            return response;
             */
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return response;
        }
    }
    #endregion

}
