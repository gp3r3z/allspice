<template>
    <section class="row justify-content-center p-3 mt-3">
        <div class="row bg-white  addpost-card">
            <div class="col-12 text-center bg-success">
                <h3>New Recipe</h3>
            </div>
            <div class="row">
                <form @submit.prevent="createRecipe" id="create-post">
                    <div class="row justify-content-between">
                        <div class="col-6">
                            <label for="title">Title</label>
                            <input v-model="input.title" placeholder="Recipe Title" class=" rounded form-control"
                                minlength="3">
                        </div>
                        <div class="col-6">
                            <label for="category">Category</label>
                            <select v-model="input.category" id="category" class="btn ">
                                <option value="mexican">Mexican</option>
                                <option value="italian">Italian</option>
                                <option value="speciality coffee">Specialitiy Coffee</option>
                                <option value="soup">Soup</option>
                                <option value="cheese">Cheese</option>
                            </select>
                        </div>
                        <div class="col-12">
                            <label for="title">Image Url</label>
                            <input v-model="input.img" placeholder="Image url" class="rounded form-control"
                                minlength="10">
                        </div>
                    </div>

                    <div class="row mt-3 justify-content-center">
                        <div class="col-12">
                            <textarea rows="4" cols="48" class="rounded form-control" v-model="input.instructions"
                                placeholder="Recipe info..." minlength="25"></textarea>
                        </div>
                    </div>
                </form>
                <section class="row justify-content-between text-center mx-1 mt-3">

                    <button form="create-post" data-bs-dismiss="modal" type="reset" class="col-5 btn fs-4 btn-danger"><i
                            class="mdi mdi-cancel"></i>
                        Cancel</button>
                    <button form="create-post" type="submit" class="col-5 btn fs-4 btn-success "><i
                            class="mdi mdi-send"></i>
                        Create</button>
                </section>
            </div>
        </div>



    </section>

</template>


<script>
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipesService } from '../services/RecipesService.js'
import { AppState } from "../AppState.js";
import { Modal } from 'bootstrap'


export default {
    setup() {
        const input = ref({})

        return {
            input,
            recipe: computed(() => AppState.activeRecipe),
            async createRecipe() {
                try {
                    await recipesService.createRecipe(input.value)
                    Pop.toast('Recipe created!', 'success')
                    Modal.getOrCreateInstance('#createModal').hide()
                    Modal.getOrCreateInstance('#recipeModal').show()

                } catch (error) {
                    Pop.error(error);
                    logger.log(error)
                }
            }
        }
    }
};
</script>

<style lang="scss" scoped>

</style>