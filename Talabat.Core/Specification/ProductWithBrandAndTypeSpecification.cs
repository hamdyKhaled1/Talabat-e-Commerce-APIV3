using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductParameter parameter)
            : base(P =>
            (string.IsNullOrEmpty(parameter.Search) || P.Name.ToLower().Contains(parameter.Search))&&
            (!parameter.BrandId.HasValue||P.ProductBrandId==parameter.BrandId.Value)&&
            (!parameter.TypeId.HasValue||P.ProductTypeId==parameter.TypeId.Value))
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(parameter.Sort))
            {
                switch(parameter.Sort)
                {
                    case "PriceAsc":
                        OrderBy = P => P.Price;
                        break;
                    case "PriceDesc":
                        OrderByDesc = P => P.Price;
                        break;
                    default:
                        OrderBy=P => P.Name;
                        break;
                }
            }

            ApplyPagination(parameter.PageIndex*(parameter.PageIndex-1),parameter.PageSize);
        }
        public ProductWithBrandAndTypeSpecification(int id):base(P=>P.Id==id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
