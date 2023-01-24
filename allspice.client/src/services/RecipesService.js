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
    async createRecipe(recipeData) {
        logger.log("FE Creating Recipe", recipeData)
        const res = await api.post('api/recipes', recipeData);

        logger.log("FE Creating Recipe response", res.data)
        AppState.recipes.shift(res.data)
        AppState.activeRecipe = res.data
    }
    async getActiveRecipe(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}`);
        logger.log("FE Getting recipe ByID", res.data)

        AppState.activeRecipe = res.data;
    }
    async getActiveRecipeIngredients(recipeId) {
        logger.log('FE getting ingredients by recipeId', recipeId)
    }

}

export const recipesService = new RecipesService();