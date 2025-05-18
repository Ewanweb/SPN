using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Site.Domain._shared;

namespace Site.Domain.Agents.ValueObjects
{
    public class AgentPhoneNumber : ValueObject
    {
        public string Value { get; }

        public AgentPhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("شماره موبایل نمی‌تواند خالی باشد", nameof(value));

            if (value.Length != 11 || !value.All(char.IsDigit))
                throw new ArgumentException("شماره موبایل باید شامل 11 رقم عددی باشد", nameof(value));

            if (!value.StartsWith("09"))
                throw new ArgumentException("شماره موبایل باید با 09 شروع شود", nameof(value));


            if (!Regex.IsMatch(value, @"^09\d{9}$"))
                throw new ArgumentException("شماره موبایل باید با 09 شروع شده و فقط شامل 11 رقم عددی باشد");

            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
