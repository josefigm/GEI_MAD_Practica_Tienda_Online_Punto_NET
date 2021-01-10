using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.Cache
{
    public class CacheUtils
    {
        public static string FormatSearch(string keyWord, long categoryId, int startIndex, int count)
        {
            if (categoryId != -1)
            {
                return keyWord + "From" + categoryId + "-" + startIndex + "-" + count;
            }
            else
            {
                return keyWord + "-" + startIndex + "-" + count;
            }
        }
    }
}
