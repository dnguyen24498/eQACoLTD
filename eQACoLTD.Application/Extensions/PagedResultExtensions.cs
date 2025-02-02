﻿using eQACoLTD.Application.Configurations;
using eQACoLTD.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Extensions
{
    public static class PagedResultExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
        public static PagedResult<T> MapPage<T>(this IEnumerable<T> source,int page,int pageSize)
        {
            var paged = new PagedResult<T>
            {
                CurrentPage = page, PageSize = pageSize, RowCount = source.Count()
            };

            var pageCount = (double)paged.RowCount / pageSize;
            paged.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            paged.Results = source.Skip(skip).Take(pageSize).ToList();
            return paged;
        }
        // public static async Task<PagedResult<U>> GetPagedAsync<T, U>(this IQueryable<T> query, int page, int pageSize) where U : class
        // {
        //     var result = new PagedResult<U>()
        //     {
        //         CurrentPage = page,
        //         PageSize = pageSize,
        //         RowCount = await query.CountAsync()
        //     };
        //     // result.CurrentPage = page;
        //     // result.PageSize = pageSize;
        //     // result.RowCount = await query.CountAsync();
        //     var pageCount = (double)result.RowCount / pageSize;
        //     result.PageCount = (int)Math.Ceiling(pageCount);
        //
        //     var skip = (page - 1) * pageSize;
        //
        //     var queryResult = await query.Skip(skip)
        //                                 .Take(pageSize)
        //                                 .ToListAsync();
        //
        //     result.Results = ObjectMapper.Mapper.Map<List<T>, List<U>>(queryResult);
        //     return result;
        // }
        
    }
}
