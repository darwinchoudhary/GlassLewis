using System;
using GlassLewisWebAPI.Models;

namespace GlassLewisWebAPI.Services.Abstractions;

public interface ICompanyService
{
    Task<Response> CreateCompany(Company company);
    Task<Company> GetCompanyById(int id);
    Task<Company> GetCompanyByIsin(string isin);
    Task<List<Company>> GetAllCompanies();
    Task<Response> UpdateCompany(Company company);
}
