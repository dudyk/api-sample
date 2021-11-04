using System;
using System.Collections.Generic;
using System.Linq;

namespace CarryLoad.Application.Extensions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<string> GetAllMessages(this Exception source)
        {
            return source
                .FromHierarchy(r => r.InnerException)
                .Select(r => r.Message);
        }
    }
}
