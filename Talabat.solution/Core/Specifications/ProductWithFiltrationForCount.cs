using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCount:BaseSpecification<Product>
    {
        public ProductWithFiltrationForCount(ProductSpecPrams prams) :
            base(p =>
             (string.IsNullOrEmpty(prams.search) || p.Name.ToLower().Contains(prams.search))
            &&
            (!prams.TypeId.HasValue || p.ProductTypeId == prams.TypeId)
            &&
            (!prams.BrandId.HasValue || p.ProductBrandId == prams.BrandId)
            )
        {
            
        }
    }
}
