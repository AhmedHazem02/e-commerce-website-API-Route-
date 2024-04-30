using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductBrandTypeSpecification:BaseSpecification<Product>
    {
        public ProductBrandTypeSpecification(ProductSpecPrams prams) : 
            base(p=>
            (string.IsNullOrEmpty(prams.search)||p.Name.ToLower().Contains(prams.search))
            &&
            (!prams.TypeId.HasValue || p.ProductTypeId==prams.TypeId)
            &&
            (!prams.BrandId.HasValue || p.ProductBrandId == prams.BrandId)
            )
        {
            Include.Add(p => p.ProductBrand); 
            Include.Add(p => p.ProductType);
            
            if (!string.IsNullOrEmpty(prams.Sort))
            {
                switch (prams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
                
            }

            ApplyPangination(prams.pagesize*(prams.PageIndex-1), prams.pagesize);
        }

        public ProductBrandTypeSpecification(int id) : base(p=>p.Id==id)
        {
            Include.Add(p => p.ProductBrand);
            Include.Add(p => p.ProductType);
        }
    }
}
