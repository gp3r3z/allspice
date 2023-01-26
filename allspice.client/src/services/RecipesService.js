import axios from "axios";
import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";
import { api, cSharpBE } from "./AxiosService.js";

class RecipesService {
    async getRecipes() {
        const res = await api.get('api/recipes');
        logger.log("FE Getting recipes", res.data)

        AppState.recipes = res.data;
    }
    async getMyRecipes(accountId) {
        const currentRecipes = await api.get('api/recipes');
        let filtered = currentRecipes.data.filter(r => r.creatorId == accountId)
        logger.log("FE here are my recipes ", filtered)
        AppState.recipes = filtered
    }
    async createRecipe(recipeData) {
        logger.log("FE Creating Recipe", recipeData)
        const res = await api.post('api/recipes', recipeData);

        AppState.recipes.push(res.data)

        logger.log("FE Creating Recipe Appstate", AppState.recipes, '\n response ', res.data)

        AppState.activeRecipe = res.data
    }
    async getActiveRecipe(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}`);
        logger.log("FE Getting recipe ByID", res.data)

        AppState.activeRecipe = res.data;

        this.getActiveRecipeIngredients(AppState.activeRecipe.id)
    }
    async getActiveRecipeIngredients(recipeId) {
        logger.log('FE getting ingredients by recipeId', recipeId)

        const res = await api.get(`api/recipes/${recipeId}/ingredients`)

        AppState.activeIngredients = res.data

        logger.log('Ingredients res.data ', res.data)
    }
    async removeRecipe(recipeId) {
        logger.log('FE getting ingredients by recipeId', recipeId)

        const res = await api.delete(`api/recipes/${recipeId}`)

        AppState.recipes = AppState.recipes.filter(item => item.id != recipeId)

        logger.log('Removed and updated Recipes ',)
    }
    async favoriteRecipe(recipeId) {
        const res = await api.post('api/favorites', { recipeId })

        logger.log('Liked Recipe ', res.data)

        AppState.unalterFav.push(res.data)

    }
    async unfavoriteRecipe(recipeId) {
        debugger
        let foundFav = AppState.unalterFav.find(uf => uf.recipeId == recipeId)
        logger.log('Found fav ', foundFav)
        const res = await api.delete(`api/favorites/${foundFav.id}`,)
        AppState.unalterFav = AppState.unalterFav.filter(uf => uf.recipeId != recipeId)

        logger.log('Unliked Here is the appstate ', AppState.unalterFav)
    }
    // TODO work on getting recipes to work 
    async searchRecipe(searchInput) {
        logger.log('searching with the param ', searchInput.param)
        const res = await api.get(`api/recipes?id=180`)

        logger.log("[Searching Recipes]", res.data)
    }
    async updateRecipe(updateData, recipeId) {
        logger.log('Here is the update data ', updateData)
        const res = await api.put(`api/recipes/${recipeId}`, updateData)
        AppState.activeRecipe = res.data
        logger.log("[Recipe Up  dated ]", res.data)
    }
    async addIngredient(ingredientData, recipeId) {
        ingredientData.recipeId = recipeId
        logger.log('Here is the ingredient data ', ingredientData)
        const res = await api.post(`api/ingredients`, ingredientData)
        AppState.activeIngredients.push(res.data)
        logger.log("[Ingredient Updated ]", res.data)
    }
    async removeIngredient(ingredientId) {
        const res = await api.delete(`api/ingredients/${ingredientId}`)

        logger.log('Removed ingre res', res.data)

        AppState.activeIngredients = AppState.activeIngredients.filter(aI => aI.id != ingredientId)

    }



}

export const recipesService = new RecipesService();