<template>
  <div class="row">
    <nav class="navbar navbar-expand-lg  p-3  mt-3">
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarText">
        <ul class="navbar-nav me-auto">

        </ul>
        <div class="row">
          <form @submit.prevent="searchRecipe" class="input-group ">
            <input v-model="input.param" type="text" class="form-control" placeholder="Search Recipes"
              aria-label="Search Recipes" aria-describedby="basic-addon2">
          </form>
        </div>
        <!-- LOGIN COMPONENT HERE -->
        <Login />
      </div>
    </nav>
  </div>

</template>

<script>
import { ref, computed } from 'vue';

import Login from './Login.vue'
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipesService } from '../services/RecipesService.js';
export default {
  setup() {
    const input = ref({})

    return {
      input,
      async searchRecipe() {
        try {
          await recipesService.searchRecipe(input.value)

          input.value = {}
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      },
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
