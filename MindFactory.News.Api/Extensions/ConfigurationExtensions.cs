using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindFactory.News.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static bool IsCORSEnabled(this IConfiguration configuration)
        {
            var corsSection = configuration.GetSection("EnableCORS");
            bool.TryParse(corsSection.Value, out var corsValue);
            return corsValue;
        }

        public static bool IsSecurityEnabled(this IConfiguration configuration)
        {
            var sectionValue = configuration.GetSection("EnableSecurity");
            bool.TryParse(sectionValue.Value, out var boolValue);
            return boolValue;
        }
    }
}