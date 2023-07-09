using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.CustomAttributes
{
    public class ListHasElementsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}
