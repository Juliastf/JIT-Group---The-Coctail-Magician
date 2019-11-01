﻿using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly CMContext _context;
        private readonly ICocktailServices _cocktailServices;
        private readonly IAppUserServices _userServices;

        public ReviewServices(CMContext context,
                              ICocktailServices cocktailServices,
                              IAppUserServices userServices)
        {
            // this dependencies can be removed ... till now
            _context = context;
            _cocktailServices = cocktailServices;
            _userServices = userServices;
        }

        public async Task<bool> CheckIfUserCanReview(string userId, CocktailDto cocktailDto)
        => cocktailDto.CocktailReviews.Any(c => c.UserId == userId && c.CocktailId == cocktailDto.Id);

        public async Task CreateCocktailReview(string userId, CocktailDto cocktailDto)
        {
            //validations

            var cocktailReview = new Review
            {
                UserId = userId,
                CocktailId = cocktailDto.Id,
                Description = cocktailDto.Description,
                Rating = cocktailDto.Rating,
                ReviewDate = DateTime.Now.ToShortDateString()
            };
            _context.Reviews.Add(cocktailReview);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            await this.SetAverrageRating(cocktailDto.Id);

        }
        public async Task SetAverrageRating(string cocktailId)
        {
            var gradesForCocktail = _context.Reviews
                                        .Where(c => c.CocktailId == cocktailId).ToList();
            var avg = gradesForCocktail.Average(c => c.Rating);
            var cocktail = _context.Cocktails.First(c => c.Id == cocktailId);
            cocktail.Rating = avg;
            await _context.SaveChangesAsync();
        }
        public async Task<IDictionary<string, Tuple<string, decimal, string>>> GetReviewsDetailsForCocktial(string cocktailId)
        {
            var reviews = new Dictionary<string, Tuple<string, decimal, string>>();

            var reviewsForCocktail = await _context.Reviews
                                    .Where(r => r.CocktailId == cocktailId)
                                    .ToListAsync();

            foreach (var item in reviewsForCocktail)
            {
                var username = await _userServices.GetUsernameById(item.UserId);
                
                if (item.Description == null)
                    item.Description = "No description";

                var descriptionWithRating = new Tuple<string, decimal, string>(item.Description,item.Rating, item.ReviewDate);

                reviews.Add(username, descriptionWithRating);
            }

            return reviews;
        }
    }
}