using System;
using System.Threading.Tasks;
using CTRLKEY_API.Data;
using CTRLKEY_API.Models.DTOs;
using CTRLKEY_API.Models.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTRLKEY_API.Service.Products;

public class ProductService
{
    private readonly DBContext _database;

    public ProductService(DBContext database)
    {
        _database = database;
    }

    //
    public async Task<Product> AddProduct(string name, string description, double price, int stock, string imageUrl, string typeProduct)
    {
        //
        if (await _database.Products.AnyAsync(p => p.Name == name))
        {
            throw new ArgumentException("Product with this name already exists");
        }
        //
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            Stock = stock,
            ImageUrl = imageUrl,
            TypeProduct = typeProduct,
        };
        _database.Products.Add(product);
        await _database.SaveChangesAsync();
        return product;
    }
    
    //
    public async Task<List<Product>> GetProducts()
    {
        return await _database.Products.ToListAsync();
    }
    
    //
    public async Task<Product> GetProductById(int id)
    {
        var product = await _database.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }
    
    //
    public async Task<Product?> DeleteProduct(int id)
    {
        var product = await _database.Products.FindAsync(id);
        if (product == null)
        {
            return null;
        }
        _database.Products.Remove(product);
        await _database.SaveChangesAsync();
        return product;
    }
    
    //
    public async Task<Product> UpdateProduct(int id, ProductDto dto)
    {
        var product = await _database.Products.FindAsync(id);
        if (product == null)
        {
            return null;
        }
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.ImageUrl = dto.ImageUrl;
        
        _database.Products.Update(product);
        await _database.SaveChangesAsync();
        return product; 
    }
    
    //
    public async Task<List<Product>> SearchProducts(string searchQuery)
    {
        var products = await _database.Products
            .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()) || 
                        p.Description.ToLower().Contains(searchQuery.ToLower()))
            .ToListAsync();
        return products;
    }

   
}