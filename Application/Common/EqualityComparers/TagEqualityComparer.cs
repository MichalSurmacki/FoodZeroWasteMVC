using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Common.EqualityComparers
{
    public class TagEqualityComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            var common = x.Product.Tags.Intersect(y.Product.Tags);
            return common.Any() ? true : false;
                
        }

        public int GetHashCode(Tag obj)
        {
            return obj.GetHashCode();
        }
    }
}
