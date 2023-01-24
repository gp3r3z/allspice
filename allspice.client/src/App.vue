<template>
  <header class="mx-3 hero-img mt-3 elevation-5">
    <Navbar />
    <div class="row justify-content-center">
      <div class="col-5 text-white text-center">
        <h3>All-Spice</h3>
        <h5>Cherish Your Family's Cooking</h5>
      </div>
    </div>
  </header>
  <main>
    <router-view />
  </main>

  <Modal id="createModal">
    <AddRecipeForm />
  </Modal>
  <Modal id="recipeModal">
    <RecipeDetail />
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.hero-img {
  background-image: url('https://images.unsplash.com/photo-1606787366850-de6330128bfc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2670&q=80');
  object-fit: contain;
  object-position: center;
  min-height: 35vh;

}
</style>
