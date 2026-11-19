using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Services
{
    public interface IStoreService
    {
        IEnumerable<Category> GetCategories();
        void AddCategory(Category category);
        IEnumerable<Product> GetProducts();
        IEnumerable<ProductDto> GetProductsApi();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        void CreateOrder(Order order);
        User? LoginUser(string username, string password);
        bool RegisterUser(User user);


    }

}
