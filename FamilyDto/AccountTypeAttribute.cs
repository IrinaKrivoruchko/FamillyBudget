using DataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FamilyDto
{
    public class AccountTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string inputValue)
            {
                return Enum.TryParse(typeof(TypeAccount), inputValue, true, out var _);
            }
            return false;
        }
    }
}
