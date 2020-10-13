using eQACoLTD.ViewModel.Other.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Others
{
    public interface IOtherService
    {
        Task<IEnumerable<BrandsForSelectionDto>> GetBrandsAsync();
        Task<IEnumerable<CategoriesForSelectionDto>> GetCategoriesAsync();
        Task<IEnumerable<CustomerTypesDto>> GetCustomertypesAsync();
        Task<IEnumerable<EmployeesForSelection>> GetEmployeesAsync();
    }
}
