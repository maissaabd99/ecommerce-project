using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Product>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(Product data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,        
                ProductCategory = data.ProductCategory,
            };
            await _context.Movies.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            
            return null;
        }

        public async Task UpdateMovieAsync(NewProductVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                //dbMovie.ImageURL = data.ImageURL;
                dbMovie.ProductCategory = data.Category;
                await _context.SaveChangesAsync();
            }    
        }

        public async Task DeleteProduct(int id)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == id);
            var items =  _context.OrderItems.Where(n => n.Movie.Id == id).ToList();
            var shops = _context.ShoppingCartItems.Where(n => n.Movie.Id == id).ToList();
            foreach (var item in items)
            {
                _context.Remove(item);
            }
            await _context.SaveChangesAsync();
            foreach (var item in shops)
            {
                _context.Remove(item);
            }
            await _context.SaveChangesAsync();

            if (dbMovie != null)
            {
                 _context.Remove(dbMovie);
                await _context.SaveChangesAsync();  
            } 
        }
    }
}
