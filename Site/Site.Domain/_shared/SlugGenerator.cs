using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Site.Domain._shared
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return string.Empty;

            // Normalize to remove diacritics
            string normalized = phrase.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            string clean = stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC)
                .ToLowerInvariant();

            // Replace Persian/Arabic letters with Latin approximations (optional)
            clean = clean
                .Replace("‌", "") // zero-width non-joiner
                .Replace("ي", "ی")
                .Replace("ك", "ک");

            // Remove invalid chars and convert spaces to dashes
            clean = Regex.Replace(clean, @"[^a-z0-9\u0600-\u06FF\s-]", ""); // فارسی و انگلیسی
            clean = Regex.Replace(clean, @"[\s\-]+", "-").Trim('-');

            return clean;
        }
    }
}
