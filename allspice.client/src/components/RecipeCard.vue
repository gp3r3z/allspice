<template>
    <div class="food-card mt-3 elevation-3 rounded p-3 img-fluid" :style="`background-image: url(${recipe.img})`"
        data-bs-target="#recipeModal" data-bs-toggle="modal" @click="getActiveRecipe(recipe.id)">
        <!-- SECTION Card Header -->
        <section class=" card-header row align-content-stretch  justify-content-between p-2 text-white">
            <div class="col-3 card-blur rounded">
                <span class="mt-3">{{ recipe.category }}</span>
            </div>
            <div v-if="account.id" class="col-3  rounded text-center">
                <i v-if="!fav.find(f => f.recipeId == recipe.id)" class="mdi mdi-heart fs-4 "></i>
                <i v-else class="mdi mdi-heart fs-4 favorite"></i>
            </div>
        </section>
        <!-- SECTION Card Footer -->
        <div class="card-footer mb-3">
            <section class="row   p-2 mx-1 text-white card-blur rounded">
                <div class="col-12">
                    <span class="mt-3">{{ recipe.title }}</span>
                </div>
            </section>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipesService } from '../services/RecipesService.js';

export default {
    props: {
        recipe: { type: Object, required: true }
    },
    setup() {
        return {
            account: computed(() => AppState.account),
            fav: computed(() => AppState.unalterFav),

            async getActiveRecipe(recipeId) {
                try {
                    await recipesService.getActiveRecipe(recipeId)
                } catch (error) {
                    logger.log(error)
                    Pop.error(error)
                }
            },



        }
    }
};
</script>


<style lang="scss" scoped>
.card-blur {
    background-color: rgba(0, 0, 0, 0.448);
}

.food-card {
    position: relative;
    object-fit: contain;
    object-position: center;
    min-height: 35vh;
}

.card-footer {
    position: absolute;
    bottom: 0px;
}

.favorite {
    color: red;
}
</style>