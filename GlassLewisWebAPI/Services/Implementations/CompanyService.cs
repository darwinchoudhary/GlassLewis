using System;
using GlassLewisWebAPI.Database;
using GlassLewisWebAPI.Models;
using GlassLewisWebAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisWebAPI.Services.Implementations;

public class CompanyService: ICompanyService
{
    private CompanyDbContext _context;

    public CompanyService(CompanyDbContext context){
        _context = context;
    }

    public async Task<Response> CreateCompany(Company company)
    {
        try
        {
            if (_context.Companies.Any(x => x.Isin.ToLower() == company.Isin.ToLower()))
            {
                return new Response(){Success = false, Error = "A company with the same ISIN already exists."};
            }

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new Response(){Success = false, Error = $"An error occured creating {company.Name}"};
        }        
        return new Response(){Success = true, Error = ""};
    }

    public async Task<List<Company>> GetAllCompanies() => 
             await _context.Companies.ToListAsync();
    

    public async Task<Company> GetCompanyById(int id) =>
             await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
    

    public async Task<Company> GetCompanyByIsin(string isin) =>
            await _context.Companies.FirstOrDefaultAsync(x => string.Equals(x.Isin, isin, StringComparison.OrdinalIgnoreCase));


    public async Task<Response> UpdateCompany(Company updatedCompany)
    {
        try
        {
            var company = await _context.Companies.FindAsync(updatedCompany.Id);
            if (company == null)
            {
                return new Response(){Success = false, Error = "A company with this ID could not be found."};
            }

            if (_context.Companies.Any(x => x.Isin.ToLower() == updatedCompany.Isin.ToLower() && x.Id != updatedCompany.Id))
            {
                return new Response(){Success = false, Error = "A company with this ISIN already exists."};
            }

            company.Name = updatedCompany.Name;
            company.Ticker = updatedCompany.Ticker;
            company.Exchange = updatedCompany.Exchange;
            company.Website = updatedCompany.Website;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex){
            return new Response(){Success = false, Error = $"An error occured updating {updatedCompany.Name}."};        
        }
        return new Response(){Success = true, Error = ""};
    }
}
