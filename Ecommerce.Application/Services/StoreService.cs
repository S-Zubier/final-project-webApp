using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IEnumerable<Category> GetCategories() => _unitOfWork.Categories.GetAll();   
        public void AddCategory(Category category) { _unitOfWork.Categories.Add(category); _unitOfWork.Save(); }

        public IEnumerable<Product> GetProducts() => _unitOfWork.Products.GetAll(includeProperties: "Category");  
        public Product? GetProductById(int id) => _unitOfWork.Products.GetById(id);  
        public void AddProduct(Product product) { _unitOfWork.Products.Add(product); _unitOfWork.Save(); }

        public IEnumerable<ProductDto> GetProductsApi()
        {
            return _unitOfWork.Products.GetAll(includeProperties: "Category").Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category?.Name
            });
        }

        public void CreateOrder(Order order) { _unitOfWork.Orders.Add(order); _unitOfWork.Save(); }
        public void UpdateProduct(Product product)
        {
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
        }

        public void DeleteProduct(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product);
                _unitOfWork.Save();
            }
        }

        public User? LoginUser(string username, string password)
        {
            return _unitOfWork.Users.GetAll()
                .FirstOrDefault(u => u.Name == username && u.Password == password);
        }

        public bool RegisterUser(User user)
        {
             var exists = _unitOfWork.Users.GetAll().Any(u => u.Name == user.Name);
            if (exists)
            {
                return false;
            }

            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
            return true;
        }


    }
}
