<template>
    <div class="row">
        <div class="col-12">

        </div>
        <div class="modal-header">
            <h3 class="text-center mx-3">Recipe Details</h3>
            <button type="button" class="btn-close mx-3" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="col-3 bg-danger">
            <img :src="recipe.img" class="img-fluid" />
        </div>
        <div class="col-9 bg-success">
            <div class="row">
                <h1>{{ recipe.title }}</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

            </div>
            <div class="row justify-content-around ">
                <div class="col-6 bg-dark p-3">
                    <div class="row justify-content-center">
                        <span class="col-7">
                            <h3>Recipe Info</h3>
                        </span>
                        <span class="col-1">
                            <i class="mdi mdi-pencil rounded bg-info fs-3"></i>
                        </span>

                    </div>
                    <textarea name="" id="" cols="30" rows="10">{{ recipe.instructions }}</textarea>
                </div>
                <div class="col-6 bg-dark ing-body p-3 ">
                    <div class="row text-center">
                        <h3>Ingredients </h3>
                    </div>

                    <div class="row ing-bottom mb-3 ">
                        <div class="col-10">
                            <input type="text" class="form-control" placeholder="Add Ingredient"
                                aria-label="add ingredient" aria-describedby="basic-addon1">
                        </div>
                        <button class="input-group-text btn btn-light col-2" id="basic-addon1"><i
                                class=" mdi mdi-plus"></i></button>
                    </div>

                </div>
            </div>
        </div>

    </div>

</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipesService } from '../services/RecipesService.js';
export default {

    setup() {
        const input = ref({})
        async function getActiveRecipeIngredients() {
            try {
                await recipesService.getActiveRecipeIngredients();
            }
            catch (error) {
                Pop.error(error);
                logger.log(error);
            }
        }
        // TODO working on getting add ingredients to load
        // onMounted(() => {
        //     getActiveRecipeIngredients()
        // });
        return {
            input,
            recipe: computed(() => AppState.activeRecipe),
            ingredients: computed(() => AppState.ingredients)
        }
    }
};
</script>


<style lang="scss" scoped>
.ing {
    position: relative;
}

.ing-bottom {
    position: absolute;
    bottom: 0px;
}
</style>