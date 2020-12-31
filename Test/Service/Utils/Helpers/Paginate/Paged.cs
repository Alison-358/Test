using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Utils.Helpers.Paginate
{
    public static class Paged<T>
    {
        public static object Pagination<T>(List<T> listDto, int maxRows, object objParam, int? pageSize, int? page)
        {
            var skipRows = 0;

            if (listDto == null || listDto.Count == 0)
            {
                return new object()
                {

                };
            }

            var paramValues = new Dictionary<object, object>();

            paramValues.Add(objParam, 1);

            var convertedDictionary = paramValues.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString());

            if (pageSize.HasValue && page.HasValue)
            {
                maxRows = pageSize.Value * page.Value;
                skipRows = maxRows - pageSize.Value;

                var skipedList = listDto.Skip(skipRows).Take(pageSize.Value).ToList();

                return new
                {
                    CurrentPage = page,
                    Items = skipedList,
                    PageSize = pageSize,
                    TotalRows = listDto.Count,
                    Filters = convertedDictionary
                };
            }
            else
            {
                return new
                {
                    CurrentPage = page,
                    Items = listDto,
                    PageSize = pageSize,
                    TotalRows = listDto.Count,
                    Filters = convertedDictionary
                };
            }
        }
    }
}
