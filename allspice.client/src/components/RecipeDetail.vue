<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">

            </div>
            <div class="modal-header">

                <h3 class="text-center mx-3">Recipe Details</h3>
                <button v-if="!edit" type="button" class="btn-close mx-3" data-bs-dismiss="modal"
                    aria-label="Close"></button>
                <button v-else @click="editRecipe(edit)" type="button" class="btn-close mx-3 bg-danger"
                    data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class=" col-3 p-0">
                <img :src="recipe.img" class="img-custom" />
            </div>
            <div class="col-9 bg-success">
                <div class="row">
                    <div class="col-8">
                        <h4> {{ recipe.title }}</h4>
                    </div>
                    <div class="col-3 text-center mt-2">
                        <button @click="removeRecipe(recipe.id)" v-show="recipe.creatorId == account.id" class="btn"><i
                                class="mdi mdi-delete "></i></button>
                        <div v-if="account.id" class=" card-blur rounded text-center">
                            <button v-if="!fav.find(f => f.recipeId == recipe.id)" @click="favoriteRecipe(recipe.id)"
                                class="btn text-white"><i class="mdi mdi-heart fs-4 "></i></button>
                            <button v-else @click="unfavoriteRecipe(recipe.id)" class="btn text-white"><i
                                    class="mdi mdi-heart fs-4 favorite"></i></button>

                        </div>
                    </div>

                </div>
                <div class="row card-details bg-dark justify-content-around ">
                    <div class="col-6 p-3">
                        <div class="row justify-content-center">
                            <span class="col-7">
                                <h3 class="border-bottom">Recipe Info</h3>
                            </span>
                            <span class="col-1" v-if="recipe.creatorId == account.id">
                                <button @click="editRecipe(edit)" class="btn"><i
                                        class="mdi mdi-pencil rounded text-white fs-3"
                                        :class="{ 'edited': edit == true }"></i></button>
                            </span>

                        </div>
                        <div>
                            <textarea v-model="input.instructions" v-if="edit" id="" cols="30"
                                rows="10">{{ recipe.instructions }}</textarea>
                            <p v-else> {{ recipe.instructions }}</p>
                        </div>
                        <div class="row" v-if="edit">
                            <button @click.prevent="updateRecipe(input, recipe.id)" class="btn btn-light">Update
                                Instructions</button>
                        </div>
                    </div>
                    <div class="col-6  ing-body p-3 ">
                        <div class="row text-center">
                            <h3 class=" border-bottom">Ingredients </h3>
                        </div>
                        <div class="row" v-for="i in ingredients">
                            <Ingredient :ingredients="i" />
                        </div>

                        <div class="row ing-bottom mb-3 " v-if="edit">
                            <div class="col-3">
                                <input v-model="inputIngredient.quantity" type="text" class="form-control"
                                    placeholder="Qty" aria-label="quantity" aria-describedby="basic-addon1">
                            </div>
                            <div class="col-8">
                                <input v-model="inputIngredient.name" type="text" class="form-control"
                                    placeholder="Add Ingredient" aria-label="add ingredient"
                                    aria-describedby="basic-addon1">
                            </div>
                            <div class="row justify-content-end mt-2">

                                <button @click="addIngredient(inputIngredient, recipe.id)"
                                    class="input-group-text btn btn-light col-3" id="basic-addon1"><i
                                        class=" mdi mdi-plus"></i></button>

                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>

</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, watchEffect } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipesService } from '../services/RecipesService.js';
import { Modal } from 'bootstrap'

export default {

    setup() {
        const input = ref({})
        const inputIngredient = ref({})
        watchEffect(() => {
            if (AppState.activeRecipe) {
                // logger.log('here is the active event in the watch effect', AppState.activeEvent[0])
                input.value = { ...AppState.activeRecipe }
            }
        });
        return {
            edit: computed(() => AppState.edit),
            input,
            inputIngredient,
            account: computed(() => AppState.account),
            recipe: computed(() => AppState.activeRecipe),
            ingredients: computed(() => AppState.activeIngredients),
            fav: computed(() => AppState.unalterFav),
            async editRecipe(edit) {
                try {
                    AppState.edit = !edit
                } catch (error) {
                    Pop.error(error)
                    logger.log(error)
                }
            },
            async removeRecipe(recipeId) {
                try {
                    await recipesService.removeRecipe(recipeId)
                    Modal.getOrCreateInstance('#recipeModal').hide()
                } catch (error) {
                    Pop.error(error)
                    logger.log(error)
                }
            },
            async favoriteRecipe(recipeId) {
                try {
                    await recipesService.favoriteRecipe(recipeId)
                } catch (error) {
                    logger.log(error)
                    Pop.error(error)
                }
            },
            async unfavoriteRecipe(recipeId) {
                try {

                    logger.log('Trying to unfavor ', recipeId)
                    await recipesService.unfavoriteRecipe(recipeId)
                } catch (error) {
                    logger.log(error)
                    Pop.error(error)
                }
            },

            async updateRecipe(recipeData, recipeId) {
                try {

                    logger.log('Updating Recipe ', recipeData)
                    await recipesService.updateRecipe(recipeData, recipeId)
                    AppState.edit = false
                    Pop.success('Updated Recipe')
                } catch (error) {
                    logger.log(error)
                    Pop.error(error)
                }
            },
            async addIngredient(ingredientData, recipeId) {
                try {

                    logger.log('Adding ingredient ', ingredientData)
                    await recipesService.addIngredient(ingredientData, recipeId)
                    inputIngredient.value = {}
                    Pop.success('Added ingredient')

                } catch (error) {
                    logger.log(error)
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" >
.ing {
    position: relative;
}

.ing-bottom {
    position: absolute;
    bottom: 0px;
}

.edited {
    color: red
}

.img-custom {
    max-width: 100%;
    height: 100%;
}

.favorite {
    color: red;
}

.card-details {
    min-height: 45vh;
}
</style>