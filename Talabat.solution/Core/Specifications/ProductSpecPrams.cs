﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecPrams
    {
        public string?Sort {  get; set; }
        public int? BrandId {  get; set; }
        public int? TypeId {  get; set; }
        public int PageIndex { get; set; } = 1;
        private int PageSize = 5;
      

        public int pagesize
        {
            get { return PageSize; }
            set { PageSize = value > 10 ? 10:value ; }
        }

        private string? Search;
        public string ?search
        {
            get { return Search; }
            set { Search = value.ToLower(); }
        }

    }
}
