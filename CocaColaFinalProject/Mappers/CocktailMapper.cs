﻿using System.Collections.Generic;
using System.Linq;
using CM.DTOs;
using CM.Models;
using CM.Web.Areas.Reviews.Models;
using CM.Web.Areas.Cocktails.Models;
using CM.Web.Areas.Search.Models;

namespace CM.Web.Mappers
{
    public static class CocktailMapper
    {
        public static CocktailViewModel MapToCocktailViewModel(this CocktailDto cocktailDto)
        {
            var cocktailVm = new CocktailViewModel();
            cocktailVm.Id = cocktailDto.Id;
            cocktailVm.Name = cocktailDto.Name;
            cocktailVm.Rating = cocktailDto.Rating;
            cocktailVm.Image = cocktailDto.Image;
            cocktailVm.DateDeleted = cocktailDto.DateDeleted;
            //cocktailVm.CocktailIngredients = cocktailDto.CocktailIngredients;
            cocktailVm.CocktailComponents = cocktailDto.Ingredients.Select(i => i.MapToCocktailComponentVM()).ToList(); 
            cocktailVm.BarCocktails = cocktailDto.BarCocktails;
            cocktailVm.LikeCount = cocktailDto.LikeCount;
            cocktailVm.LikeByUsers = cocktailDto.LikedByUsers;
            //cocktailVm.Ingredients = cocktailDto.Ingredients.Select(i => i.MapIngredientToViewModel());
            //Recepie .? mai nqma smisul ako samo moje da se dl
            return cocktailVm; 

        }
       
        public static CocktailDto MapToCocktailDTO(this CreateCocktailViewModel cocktailVM)
        {
            var cocktailDto = new CocktailDto();
            cocktailDto.Id = cocktailVM.Id;
            cocktailDto.Name = cocktailVM.Name;
            cocktailDto.Image = cocktailVM.Image;
            cocktailDto.CocktailImage = cocktailVM.CocktailImage;
            cocktailDto.Ingredients = cocktailVM.Ingredients
                .Where(i=>i.Ingredient!=null)
                .Select(
                i=>i.MapCockTailComponentVMToDTo()           
                ).ToList();
            cocktailDto.Recipe = cocktailVM.Recipe;
            return cocktailDto;

        }
        public static CocktailDto MapToCocktailDTO(this CocktailViewModel cocktailVM)
        {
            var cocktailDto = new CocktailDto();
            cocktailDto.Id = cocktailVM.Id;
            cocktailDto.Name = cocktailVM.Name;
            cocktailDto.Image = cocktailVM.Image;
            cocktailDto.CocktailImage = cocktailVM.CocktailImage;
            cocktailDto.Ingredients = cocktailVM.Ingredients
                .Where(i => i.Ingredient != null)
                .Select(
                i => i.MapCockTailComponentVMToDTo()
                ).ToList();
            cocktailDto.Recipe = cocktailVM.Recipe;
            return cocktailDto;

        }
      
        public static CocktailDto MapToCocktailDto(this CocktailReviewViewModel cocktailVm)
        {
            var cocktailDto = new CocktailDto();
            cocktailDto.Id = cocktailVm.Id;
            cocktailDto.Description = cocktailVm.Description;
            cocktailDto.Rating = cocktailVm.Rating;
            return cocktailDto;
        }
        public static CocktailReviewViewModel MapToCocktailReviewViewModel(this CocktailDto cocktailDto)
        {
            var cocktailVm = new CocktailReviewViewModel();
            cocktailVm.Id = cocktailDto.Id;
            cocktailVm.CocktailID = cocktailDto.CocktailId;
            cocktailVm.Name = cocktailDto.Name;
            cocktailVm.Rating = cocktailDto.Rating;
            cocktailVm.Description = cocktailDto.Description;
            cocktailVm.Image = cocktailDto.Image;
            cocktailVm.ReviewDate = cocktailDto.ReviewDate;
            //cocktailVm.Ingredients = cocktailDto.Ingredients;
            cocktailVm.LikeCount = cocktailDto.LikeCount;
            cocktailVm.LikedByUsers = cocktailDto.LikedByUsers;
            return cocktailVm;
        }

        public static CocktailSearchRestultViewModel MapCocktailSearchDTOToVM(this CocktailSearchResultDTO cocktail)
        {
            var newCocktailVM = new CocktailSearchRestultViewModel();
            newCocktailVM.Id = cocktail.Id;
            newCocktailVM.Name = cocktail.Name;
            newCocktailVM.Image = cocktail.Image;
            newCocktailVM.Ingredients = cocktail.Ingredients;
            newCocktailVM.Rating = cocktail.Rating;
            return newCocktailVM;
        }

    }
}
