using System;
using System.Collections.Generic;

namespace FullTextSearch.Db
{
    static class LanguageExtensions
    {
        public static bool In<T>(this T source, params T[] list)
        {
            return (list as IList<T>).Contains(source);
        }
    }
}