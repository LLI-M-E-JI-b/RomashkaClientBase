using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomashkaClientBase.Model
{
    public static class CompaniesManager
    {
        public static void AddCompany(Company company)
        {
            using (var context = new ClientBaseContext())
            {
                context.Companies.Add(company);
                context.SaveChanges();
            }
        }

        public static void DeleteCompany(Company company)
        {
            using (var context = new ClientBaseContext())
            {
                var companyEntity = context.Companies.First(c => c.Id == company.Id);

                context.Users.RemoveRange(companyEntity.Users);
                context.Companies.Remove(companyEntity);
                
                context.SaveChanges();
            }
        }

        public static void UpdateCompany(Company company)
        {
            using (var context = new ClientBaseContext())
            {
                var companyEntity = context.Companies.Find(company.Id);
                
                context.Entry(companyEntity).CurrentValues.SetValues(company);
                context.SaveChanges();
            }
        }

        public static IEnumerable<Company> GetAllCompanies()
        {
            IEnumerable<Company> companies = null;

            using (var context = new ClientBaseContext())
                companies = context.Companies.ToArray(); 
            
            return companies;
        }
    }
}
