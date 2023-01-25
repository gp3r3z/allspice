<template>
    <div class="row mt-1">
        <div class="col-4 ">
            {{ ingredients.quantity }}
        </div>
        <div class="col-5">
            {{ ingredients.name }}
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    props: {
        ingredients: { type: Object, required: true }
    },
    setup() {
        return {}
    }
};
</script>


<style lang="scss" scoped>

</style>