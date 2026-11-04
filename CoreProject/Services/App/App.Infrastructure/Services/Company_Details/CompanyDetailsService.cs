using App.Domain.Entities.Company;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.RequestModels;
using App.Persistence.DbContexts;
using App.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Services.Company_Details;


#region fields
public interface ICompanyDetailsService
{
    Task<CompanyDetailsInformation> InsertCompanyDetails(CompanyDetailsInformation model);
    Task<CompanyDetailsInformation> UpdateCompanyDetails(CompanyDetailsInformation model);
    Task<CompanyDetailsInformation> DeleteCompanyDetailsById(DeleteCompanyDetailsRequestModel command);
    Task<List<CompanyDetailsInformation>> GetAsync(Expression<Func<CompanyDetailsInformation, bool>> predicate);
    Task<CompanyDetailsInformation> UpdateCompanyDetailsData(CompanyDetailsInformation request);
    Task<CompanyDetailsInformation> InsertCompanyDetailsData(CompanyDetailsInformation request);
    Task<CompanyDetailsInformation> GetCompanyDetailsByCompanyId(string id);
}
#endregion
public class CompanyDetailsService : ICompanyDetailsService
{
    #region ctor

    private readonly IBaseRepository<Companies> _baseRepository;
    private readonly IBaseRepository<CompanyAddressInformation> _addressRepository;
    private readonly IBaseRepository<CompanyDetailsInformation> _detailsRepository;
    private readonly IBaseRepository<CompanyContactInformation> _contactRepository;
    private readonly AppDbContext _appDbContext;

    public CompanyDetailsService(IBaseRepository<Companies> baseRepository, AppDbContext appDbContext,
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

    #region deletecompanybyid
    public async Task<CompanyDetailsInformation> DeleteCompanyDetailsById(DeleteCompanyDetailsRequestModel request)
    {

        CompanyDetailsInformation entity = new();
        try
        {
            var getData = await _detailsRepository.GetByIdAsync(request.Id!);
            if (getData is not null)
            {
                getData.IsDeleted = true;
                getData.DeletedAt = DateTime.UtcNow;
                await _detailsRepository.UpdateAsync(getData);
                return entity;
            }
            else
                throw new Exception(ResponseMessages.ExceptionOccured + ResponseMessages.NotFound);

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region getcompanydetailsbyid
    public async Task<CompanyDetailsInformation> GetCompanyDetailsByCompanyId(string Companyid)
    {
        try
        {
            CompanyDetailsInformation? entity = (await _detailsRepository.GetAsync(x => x.CompanyId == Companyid && !x.IsDeleted)).FirstOrDefault();
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

    #region insertcompanyDetails
    public async Task<CompanyDetailsInformation> InsertCompanyDetails(CompanyDetailsInformation model)
    {
        try
        {
            CompanyDetailsInformation entity = new();
            entity = await _detailsRepository.AddAsync(model);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region updatecompanydetails
    public async Task<CompanyDetailsInformation> UpdateCompanyDetails(CompanyDetailsInformation request)
    {
        CompanyDetailsInformation? entity = null;
        try
        {
            if (string.IsNullOrEmpty(request.CompanyId))
            {
                entity = new();
                entity = await InsertCompanyDetails(new()
                {
                    CompanyId = request.CompanyId,
                    DandBNumber = request.DandBNumber,
                    IndustryId = request.IndustryId,
                    SICCode = request.SICCode,
                    NAICSCode = request.NAICSCode,
                    ScopeId = request.ScopeId,
                    EntityType = request.EntityType,
                    Employees = request.Employees,
                    Revenue = request.Revenue,
                    ParentCompany = request.ParentCompany,
                    CurrencyId = request.CurrencyId,
                    TaxId = request.TaxId,
                    VAT = request.VAT,
                    ResellerId = request.ResellerId,
                    ResellerIdExpDate = request.ResellerIdExpDate,
                    Notes = request.Notes
                });
            }
            if (!string.IsNullOrEmpty(request.CompanyId))
            {
                entity = await _detailsRepository.GetByIdAsync(request.CompanyId);
                entity!.CompanyId = request.CompanyId;
                entity.DandBNumber = request.DandBNumber;
                entity.IndustryId = request.IndustryId;
                entity.SICCode = request.SICCode;
                entity.NAICSCode = request.NAICSCode;
                entity.ScopeId = request.ScopeId;
                entity.EntityType = request.EntityType;
                entity.Employees = request.Employees;
                entity.Revenue = request.Revenue;
                entity.ParentCompany = request.ParentCompany;
                entity.CurrencyId = request.CurrencyId;
                entity.TaxId = request.TaxId;
                entity.VAT = request.VAT;
                entity.ResellerId = request.ResellerId;
                entity.ResellerIdExpDate = request.ResellerIdExpDate;
                entity.Notes = request.Notes;
                await _detailsRepository.UpdateAsync(entity);
            }
            return entity!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region updateCompanyDetailsData
    public async Task<CompanyDetailsInformation> UpdateCompanyDetailsData(CompanyDetailsInformation request)
    {
        try
        {
            var entity = await _detailsRepository.Update(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region insertCompanyDetailsData
    public async Task<CompanyDetailsInformation> InsertCompanyDetailsData(CompanyDetailsInformation request)
    {
        try
        {
            var entity = await _detailsRepository.AddAsync(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region getAsyncCompanyDetails
    public async Task<List<CompanyDetailsInformation>> GetAsync(Expression<Func<CompanyDetailsInformation, bool>> predicate)
    {
        return await _appDbContext.Set<CompanyDetailsInformation>().Where(predicate).ToListAsync();
    }
    #endregion
}

