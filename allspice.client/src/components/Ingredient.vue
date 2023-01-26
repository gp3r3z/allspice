<template>
    <div class="row mt-1">
        <div class="col-4 ">
            {{ ingredients.quantity }}
        </div>
        <div class="col-5">
            {{ ingredients.name }}
        </div>
        <div class="col-1" v-if="edit">
            <button @click="removeIngredient(ingredients.id)" class="btn text-white"><i
                    class="mdi mdi-delete"></i></button>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import Pop from '../utils/Pop.js';
export default {
    props: {
        ingredients: { type: Object, required: true }
    },
    setup() {
        return {
            edit: computed(() => AppState.edit),
            account: computed(() => AppState.account),
            async removeIngredient(ingredientId) {
                try {
                    recipesService.removeIngredient(ingredientId)
                    Pop.success('Removed ingredient')
                } catch (error) {
                    Pop.error(error)
                    logger.log(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>

</style>