using App.Domain.Entities.Company;
using App.Domain.Enums.Companies.GeneralInfo;
using App.Domain.ViewModels;
using App.Persistence.Repositories;
using Microsoft.OpenApi.Extensions;

namespace App.Infrastructure.Services.Common.Dropdown;

public interface IDropdownService
{
    Task<DropdownListModel> GetAllDropdown();
}

public class DropdownService : IDropdownService
{
    #region fields 
    private readonly IBaseRepository<Companies> _companyRepository;
    private readonly IBaseRepository<CompanyAddressInformation> _addressRepository;
    private readonly IBaseRepository<CompanyDetailsInformation> _detailsRepository;
    private readonly IBaseRepository<CompanyContactInformation> _contactsRepository;
    #endregion

    #region ctor
    public DropdownService(IBaseRepository<Companies> companyRepository,
        IBaseRepository<CompanyAddressInformation> addressRepository,
        IBaseRepository<CompanyDetailsInformation> detailsRepository,
        IBaseRepository<CompanyContactInformation> contactsRepository)

    {
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        _detailsRepository = detailsRepository ?? throw new ArgumentNullException(nameof(detailsRepository));
        _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
    }
    #endregion

    #region method
    public async Task<DropdownListModel> GetAllDropdown()
    {
        try
        {
            DropdownListModel dropdownModel = new();

            dropdownModel.CompanyDropdown = (await _companyRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.CompanyName, Id = a.Id }).ToList();
            dropdownModel.ContactDropdown = (await _companyRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.CompanyName, Id = a.Id }).ToList();
            dropdownModel.SalesPersonDropdown = (await _companyRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.SalesPersonId, Id = a.Id }).ToList();
            dropdownModel.AltSalesPersonDropdown = (await _companyRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.AltSalesPersonId, Id = a.Id }).ToList();
            dropdownModel.BuyerRepDropdown = (await _companyRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.BuyerRepId, Id = a.Id }).ToList();
            dropdownModel.TypeDropdown = (await _addressRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.AddressTypeId, Id = a.Id }).ToList();
            dropdownModel.IndustryDropdown = (await _detailsRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.IndustryId, Id = a.Id }).ToList();
            dropdownModel.ScopeDropdown = (await _detailsRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.ScopeId, Id = a.Id }).ToList();
            dropdownModel.CurrencyDropdown = (await _detailsRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.CurrencyId, Id = a.Id }).ToList();
            dropdownModel.DepartmentDropdown = (await _contactsRepository.GetAsync(x => !x.IsDeleted)).Select(a => new DropdownModel { Name = a.DepartmentId, Id = a.Id }).ToList();

            dropdownModel.SaleStageDropdown = Enum.GetValues(typeof(DefaultStage)).Cast<DefaultStage>()
                .Select(a => new DropdownModel { Name = EnumExtensions.GetDisplayName(a), Id = ((int)a).ToString() }).ToList();

            dropdownModel.TagsDropdown = Enum.GetValues(typeof(DefaultTags)).Cast<DefaultTags>()
                .Select(a => new DropdownModel { Name = EnumExtensions.GetDisplayName(a), Id = ((int)a).ToString() }).ToList();

            dropdownModel.TypesDropdown = Enum.GetValues(typeof(DefaultTypes)).Cast<DefaultTypes>()
                .Select(a => new DropdownModel { Name = EnumExtensions.GetDisplayName(a), Id = ((int)a).ToString() }).ToList();

            dropdownModel.TypesDropdown = Enum.GetValues(typeof(DefaultGender)).Cast<DefaultGender>()
                .Select(a => new DropdownModel { Name = EnumExtensions.GetDisplayName(a), Id = ((int)a).ToString() }).ToList();

            dropdownModel.TypesDropdown = Enum.GetValues(typeof(DefaultCountry)).Cast<DefaultCountry>()
                .Select(a => new DropdownModel { Name = EnumExtensions.GetDisplayName(a), Id = ((int)a).ToString() }).ToList();

            return dropdownModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

}
