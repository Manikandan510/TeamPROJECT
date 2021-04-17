﻿using ProductMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Repository
{
    public interface IProductRepository
    {
        public List<ProductDto> GetAllProducts();
        public bool AddProductRating(ProductRating model);
    }
}
