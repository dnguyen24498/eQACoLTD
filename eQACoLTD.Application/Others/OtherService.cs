using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Other.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace eQACoLTD.Application.Others
{
    public class OtherService : IOtherService
    {
        private readonly AppIdentityDbContext _context;
        public OtherService(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BrandsDto>> GetBrandsAsync()
        {
            var brands = await (from b in _context.Brands
                         select new BrandsDto()
                         {
                             Id = b.Id,
                             Name=b.Name
                         }).ToListAsync();
            return (IEnumerable<BrandsDto>)brands;
        }
    }
}
