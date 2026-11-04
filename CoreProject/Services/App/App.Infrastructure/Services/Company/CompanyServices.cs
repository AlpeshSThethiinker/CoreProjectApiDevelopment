using App.Domain.Entities.Company;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company.RequestModel;
using App.Infrastructure.Services.RequestModels;
using App.Persistence.DbContexts;
using App.Persistence.Repositories;

namespace App.Infrastructure.Services.Company
{
    public class CompanyServices
    {
        #region fields
        public interface ICompanyService
        {
            Task<Companies> InsertCompany(Companies model);
            Task<Companies> UpdateCompany(UpdateCompanyRequestModel model);
            Task<Companies> DeleteCompanyById(DeleteCompanyRequestModel request);

            Task<Companies> GetCompanyId(string id);

            Task<List<Companies>> GetAllCompany();

        }
        #endregion


        public class CompanyService : ICompanyService
        {

            #region ctor
            private readonly IBaseRepository<Companies> _baseRepository;
            private readonly IBaseRepository<CompanyAddressInformation> _addressRepository;
            private readonly IBaseRepository<CompanyDetailsInformation> _detailsRepository;
            private readonly IBaseRepository<CompanyContactInformation> _contactRepository;
            private readonly AppDbContext _appDbContext;

            public CompanyService(IBaseRepository<Companies> baseRepository, AppDbContext appDbContext,
                                  IBaseRepository<CompanyAddressInformation> addressRepository,
                                  IBaseRepository<CompanyDetailsInformation> detailsRepository, IBaseRepository<CompanyContactInformation> contactRepository)
            {
                this._baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
                this._appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
                this._addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
                this._detailsRepository = detailsRepository ?? throw new ArgumentNullException(nameof(detailsRepository));
                this._contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            }
            #endregion

            #region delete company
            public async Task<Companies> DeleteCompanyById(DeleteCompanyRequestModel request)
            {
                Companies entity = new();
                try
                {
                    var checkId = await _baseRepository.GetByIdAsync(request.Id!);
                    //var checkaddressId = await _addressRepository.GetByIdAsync(request.Id!);
                    var checkaddressId = (await _addressRepository.GetAsync(x => x.CompanyId == request.Id)).ToList();
                    var checkdetailsId = (await _detailsRepository.GetAsync(x => x.CompanyId == request.Id)).ToList();
                    var checkcontactId = (await _contactRepository.GetAsync(x => x.CompanyId == request.Id)).ToList();
                    if (checkId is not null)
                    {
                        checkId.IsDeleted = true;
                        checkId.DeletedAt = DateTime.UtcNow;
                        await _baseRepository.UpdateAsync(checkId);
                        if (checkaddressId is not null)
                        {
                            foreach (var item in checkaddressId)
                            {
                                item.IsDeleted = true;
                                item.DeletedAt = DateTime.UtcNow;
                                await _addressRepository.UpdateAsync(item);
                            }
                            if (checkdetailsId is not null)
                            {
                                foreach (var item in checkdetailsId)
                                {
                                    item.IsDeleted = true;
                                    item.DeletedAt = DateTime.UtcNow;
                                    await _detailsRepository.UpdateAsync(item);
                                }
                                if (checkcontactId is not null)
                                {
                                    foreach (var item in checkcontactId)
                                    {
                                        item.IsDeleted = true;
                                        item.DeletedAt = DateTime.UtcNow;
                                        await _contactRepository.UpdateAsync(item);
                                    }
                                }
                            }
                        }
                        return entity;
                    }
                    else
                    {
                        throw new Exception(ResponseMessages.ExceptionOccured);
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            #endregion

            #region get all company
            public async Task<List<Companies>> GetAllCompany()
            {
                try
                {
                    var GetAll = await _baseRepository.GetAllAsync();
                    return GetAll.ToList();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            #endregion

            #region get company by id
            public async Task<Companies> GetCompanyId(string id)
            {
                try
                {
                    Companies? entity = await _baseRepository.GetByIdAsync(id);
                    if (entity is null)
                        return null!;

                    return entity;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion

            #region insert company
            public async Task<Companies> InsertCompany(Companies model)
            {
                try
                {
                    Companies entity = new();
                    entity = await _baseRepository.AddAsync(model);
                    return entity;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion

            #region update company
            public async Task<Companies> UpdateCompany(UpdateCompanyRequestModel request)
            {
                try
                {
                    var result = (await _baseRepository.GetAsync(x => x.CompanyName == request.CompanyName && x.IsDeleted == false && x.Id != request.Id)).ToList();
                    if (result.Count != 0)
                    {
                        throw new Exception(ResponseMessages.ExceptionOccured);
                    }
                    var entity = await _baseRepository.GetByIdAsync(request?.Id!);
                    if (entity is not null)
                    {
                        entity.Id = request!.Id!;
                        entity.CompanyName = request.CompanyName;
                        entity.CompanyEmail = request.CompanyEmail;
                        entity.CompanyPhone = request.CompanyPhone;
                        entity.CompanyFax = request.CompanyFax;
                        entity.StateId = request.StateId;
                        entity.CountryId = request.CountryId;
                        entity.Website = request.Website;
                        entity.TimeZoneId = request.TimeZoneId;
                        entity.SalesPersonId = request.SalesPersonId;
                        entity.AltSalesPersonId = request.AltSalesPersonId;
                        entity.BuyerRepId = request.BuyerRepId;
                        entity.ContactReminders = request.ContactReminders;
                        entity.DoNotCall = request.DoNotCall;
                        entity.MarkAsVendor = request.MarkAsVendor;
                        entity.ApprovedVendorList = request.ApprovedVendorList;
                        entity.OverAllRating = request.OverAllRating;
                        await _baseRepository.UpdateAsync(entity);
                        return entity;
                    }
                    else
                    {
                        throw new Exception("No Record Found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion
        }
    }
}
