<template>
  <div class="container-fluid">
    <!-- SECTION filters -->
    <section class="row justify-content-center">
      <div class="col-5 bg-dark rounded p-1 filter-row">
        <section class="row  text-center">
          <div class="col-4  ">
            <button class="btn filter-btn ">MyRecipes</button>
          </div>
          <div class="col-4  ">
            <button class="btn filter-btn activeFilter" style="width:100px">Home</button>
          </div>
          <div class="col-4  ">
            <button class="btn filter-btn ">Favorites</button>
          </div>
        </section>
      </div>

    </section>
    <!-- SECTION Card -->
    <section class="row mx-3">
      <div v-for="r in recipes" class="col-4">
        <RecipeCard :recipe="r" class="selectable" />
      </div>
    </section>


    <section class="row">
      <button href="#" class="float-right btn rounded-circle bg-dark d-flex justify-content-center align-items-center"
        data-bs-toggle="modal" data-bs-target="#createModal" title="Create Group">
        <i class="mdi mdi-plus fs-1"></i>
      </button>
    </section>

  </div>
</template>

<script>
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipesService } from '../services/RecipesService.js'
import { AppState } from "../AppState.js";

export default {
  setup() {

    async function getRecipes() {
      try {
        await recipesService.getRecipes()

      } catch (error) {
        Pop.error(error);
        logger.log(error)
      }
    }
    onMounted(() => {
      getRecipes();
    })
    return {
      recipes: computed(() => AppState.recipes),

    };
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.filter-row {
  transform: translateY(-3vh);
  background-color: rgb(255, 255, 255);
  box-shadow: 10px 5px 5px black;
}

.float-right {
  position: fixed;
  width: 60px;
  height: 60px;
  bottom: 40px;
  right: 40px;
  background-color: #0C9;
  color: #AECBCF;
  border-radius: 50px;
  text-align: center;
  box-shadow: 2px 2px 3px #AECBCF;
}

.filter-btn {
  color: white;
}

.filter-btn:hover {
  background-color: #21314d;
}

.activeFilter {
  border-bottom: 3px solid #0C9;

}
</style>
