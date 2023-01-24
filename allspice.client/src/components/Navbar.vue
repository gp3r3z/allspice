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
          <div class="input-group ">
            <input type="text" class="form-control" placeholder="Search Recipes" aria-label="Search Recipes"
              aria-describedby="basic-addon2">
          </div>
        </div>
        <!-- LOGIN COMPONENT HERE -->
        <Login />
      </div>
    </nav>
  </div>

</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
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
