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

}

export const recipesService = new RecipesService();