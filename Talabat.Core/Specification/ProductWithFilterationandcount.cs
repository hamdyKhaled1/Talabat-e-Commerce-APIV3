using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
   public class ProductWithFilterationandcount:BaseSpecification<Product>
    {
        public ProductWithFilterationandcount(ProductParameter parameter) : base(P => (!parameter.BrandId.HasValue || P.ProductBrandId == parameter.BrandId.Value) &&
        (!parameter.TypeId.HasValue || P.ProductTypeId == parameter.TypeId.Value))
        {
            
        }
    }
}
