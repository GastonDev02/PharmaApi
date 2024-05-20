using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Si es necesario para ApplicationDbContext
using PharmaApi.Data;
using PharmaApi.Dto;
using PharmaApi.Models;
using PharmaApi.Services;
using PharmaApi.Utils;

namespace PharmaApi.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProductModel> GetProducts()
        {
            try
            {
                var products = _context.Products.ToList();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al encontrar todos los prodcutos", ex);
            }

        }

        public ProductModel GetProduct(int id)
        {
            try
            {
                var productById = _context.Products.Find(id);
                if (productById == null)
                {
                    throw new Exception("Product not found");
                }

                return productById;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al encontrar un producto", ex);
            }

;        }

        public List<ProductModel> SearchProduct(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return this.GetProducts();
                }

                key = key.ToLower();
                var products = _context.Products
                    .Where(p => p.nombre_producto.ToLower().Contains(key) || p.descripcion_producto.ToLower().Contains(key))
                    .ToList();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar productos por clave", ex);
            }
        }

        public string CreateProduct(ProductModel newProduct)
        {
            try
            {
                var allProducts = this.GetProducts();
                var verificationProduct = allProducts.Find(product => product.codigo_producto == newProduct.codigo_producto);
                if(verificationProduct != null)
                {
                    verificationProduct.stock++;
                    _context.SaveChanges();
                    return $"Se ha actualizado el stock para el producto {verificationProduct.nombre_producto}";
                }
                else
                {
                    newProduct.id_producto = RandomIdGenerator.GenerateRandomId();
                    _context.Products.Add(newProduct);
                    _context.SaveChanges();
                    return "Se ha creado un nuevo producto";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el producto", ex);
            }
        }

        public void UpdateProduct(int idProduct, UpdateProductDto updateProduct)
        {
            try
            {
                var product = GetProduct(idProduct);
                if (product == null)
                {
                    throw new Exception("Producto no encontrado");
                }

                product.nombre_producto = updateProduct.nombre_producto;
                product.descripcion_producto = updateProduct.descripcion_producto;
                product.presentacion = updateProduct.presentacion;
                product.laboratorio = updateProduct.laboratorio;
                product.stock = updateProduct.stock;
                product.precio = updateProduct.precio;
                product.categoria = updateProduct.categoria;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto", ex);
            }
        }

        public void DeleteProduct(int idProduct)
        {
            try
            {
                var product = GetProduct(idProduct);
                if (product == null)
                {
                    throw new Exception("Producto no encontrado");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();

            }catch
            {
                throw;
            }
        }

    }
}
