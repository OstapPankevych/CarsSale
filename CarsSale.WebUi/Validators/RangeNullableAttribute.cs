using System;
using System.ComponentModel.DataAnnotations;

namespace CarsSale.WebUi.Validators
{
    public class RangeNullableAttribute : RangeAttribute
    {
		public RangeNullableAttribute(int min, int max): base(min, max) { }
        public override bool IsValid(object value)
        {
            return value == null || base.IsValid(value);
        }
    }
}