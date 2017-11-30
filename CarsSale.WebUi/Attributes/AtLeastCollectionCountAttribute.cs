using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsSale.WebUi.Attributes
{
    public class AtLeastCollectionCountAttribute: RequiredAttribute
    {
        public int Count { get; set; }

        public override bool IsValid(object value)
        {
            if (!(value is ICollection collection))
            {
                return false;
            }

            return collection.Count >= Count;
        }
    }
}