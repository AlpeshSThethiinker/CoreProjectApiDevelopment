using App.Domain.Entities.Company;
using App.Infrastructure.Services.RequestModels;
using App.Persistence.DbContexts;
using App.Persistence.Repositories;

namespace App.Infrastructure.Services.Company_Contact;

#region fields
public interface ICompanyContactService
{
    Task<CompanyContactInformation> InsertCompanyContact(CompanyContactInformation model);
    Task<CompanyContactInformation> UpdateCompanyContact(CompanyContactInformation model);
    Task<List<CompanyContactInformation>> DeleteCompanyContactById(DeleteCompanyContactRequestModel request);
    Task<CompanyContactInformation> UpdateCompanyContactsData(CompanyContactInformation request);
    Task<CompanyContactInformation> InsertCompanyContactsData(CompanyContactInformation request);
    Task<List<CompanyContactInformation>> GetContactsByCompanyId(string id);
}
#endregion 

public class CompanyContactService : ICompanyContactService
{
    #region ctor


    private readonly IBaseRepository<CompanyContactInformation> _contactRepository;
    private readonly AppDbContext _appDbContext;

    public CompanyContactService(AppDbContext appDbContext, IBaseRepository<CompanyContactInformation> contactRepository)
    {
        this._appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        this._contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
    }
    #endregion

    #region deletecompanycontactbyid
    public async Task<List<CompanyContactInformation>> DeleteCompanyContactById(DeleteCompanyContactRequestModel request)
    {
        try
        {
            var checkId = await GetContactsByCompanyId(request.Id!);
            if (checkId is not null)
            {
                await _contactRepository.DeleteRangeAsync(x => x.CompanyId == request.Id && !x.IsDeleted);
                return checkId;
            }
            else
            {
                throw new Exception("Company contact data not found");
            }

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region getcompanycontactbyid
    public async Task<List<CompanyContactInformation>> GetContactsByCompanyId(string Companyid)
    {
        try
        {
            List<CompanyContactInformation>? entity = (await _contactRepository.GetAsync(x => x.CompanyId == Companyid && !x.IsDeleted)).ToList();
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

    #region insertcompanycontact
    public async Task<CompanyContactInformation> InsertCompanyContact(CompanyContactInformation model)
    {
        try
        {
            CompanyContactInformation entity = new();
            entity = await _contactRepository.AddAsync(model);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region updatecompanycontact
    public async Task<CompanyContactInformation> UpdateCompanyContact(CompanyContactInformation request)
    {
        CompanyContactInformation? entity = null;
        try
        {
            if (string.IsNullOrEmpty(request.CompanyId))
            {
                entity = new();
                entity = await InsertCompanyContact(new()
                {
                    CompanyId = request.CompanyId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Title = request.Title,
                    Gender = request.Gender,
                    DepartmentId = request.DepartmentId,
                    Location = request.Location,
                    TimeZoneId = request.TimeZoneId,
                    Email = request.Email,
                    DirectionNumber = request.DirectionNumber,
                    MainOfficePhone = request.MainOfficePhone,
                    MobileNumber = request.MobileNumber,
                    Fax = request.Fax,
                    Xing = request.Xing,
                    Linkedin = request.Linkedin,
                    PrimaryAccounting = request.PrimaryAccounting,
                    PrimaryContact = request.PrimaryContact,
                    PrimaryPurchasing = request.PrimaryPurchasing,
                    DoNotCall = request.DoNotCall,
                    DoNotEmail = request.DoNotEmail,
                    Skype = request.Skype
                });
            }
            else if (!string.IsNullOrEmpty(request.CompanyId))
            {
                entity = await _contactRepository.GetByIdAsync(request.Id);
                entity.CompanyId = request.CompanyId;
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.Title = request.Title;
                entity.Gender = request.Gender;
                entity.DepartmentId = request.DepartmentId;
                entity.Location = request.Location;
                entity.TimeZoneId = request.TimeZoneId;
                entity.Email = request.Email;
                entity.DirectionNumber = request.DirectionNumber;
                entity.MainOfficePhone = request.MainOfficePhone;
                entity.MobileNumber = request.MobileNumber;
                entity.Fax = request.Fax;
                entity.Xing = request.Xing;
                entity.Linkedin = request.Linkedin;
                entity.PrimaryAccounting = request.PrimaryAccounting;
                entity.PrimaryContact = request.PrimaryContact;
                entity.PrimaryPurchasing = request.PrimaryPurchasing;
                entity.DoNotCall = request.DoNotCall;
                entity.DoNotEmail = request.DoNotEmail;
                entity.Skype = request.Skype;
                await _contactRepository.UpdateAsync(entity);
            }

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region updateCompanyContactsData
    public async Task<CompanyContactInformation> UpdateCompanyContactsData(CompanyContactInformation request)
    {
        try
        {
            var entity = await _contactRepository.Update(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region insertCompanyContactsData
    public async Task<CompanyContactInformation> InsertCompanyContactsData(CompanyContactInformation request)
    {
        try
        {
            var entity = await _contactRepository.AddAsync(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion



}
