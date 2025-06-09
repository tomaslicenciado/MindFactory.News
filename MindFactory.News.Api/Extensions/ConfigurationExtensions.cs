// <copyright file="ConfigurationExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static bool IsCORSEnabled(this IConfiguration configuration)
        {
            var corsSection = configuration.GetSection("EnableCORS");
            _ = bool.TryParse(corsSection.Value, out var corsValue);
            return corsValue;
        }

        public static bool IsSecurityEnabled(this IConfiguration configuration)
        {
            var sectionValue = configuration.GetSection("EnableSecurity");
            _ = bool.TryParse(sectionValue.Value, out var boolValue);
            return boolValue;
        }
    }
}