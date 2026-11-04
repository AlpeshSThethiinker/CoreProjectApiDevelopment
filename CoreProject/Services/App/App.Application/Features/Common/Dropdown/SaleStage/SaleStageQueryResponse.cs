using App.Domain.ViewModels;

namespace App.Application.Features.Common.Dropdown.SaleStage;

public class SaleStageQueryResponse
{
    public List<DropdownModel>? StageList { get; set; }
}
