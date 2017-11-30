using System.Collections.Generic;
using System.Linq;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class CurrencyRepository: Repository, ICurrencyRepository
    {
        public CurrencyRepository(string connectionString)
            : base(connectionString)
        { }

        public IEnumerable<Currency> GetCurrencies()
        {
            using (var context = CreateContext())
            {
                return context.CURRENCies
                    .AsEnumerable()
                    .Select(x => new Currency(x))
                    .ToList();
            }
        }
    }
}