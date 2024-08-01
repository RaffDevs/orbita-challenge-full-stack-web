<template>
  <v-container>
    <v-row>
      <StudentGrid/>
    </v-row>
  </v-container>
</template>

<script>
import StudentGrid from "@/components/StudentGridComponent.vue";

export default {
  name: "MainPage",
  components: {StudentGrid}
}
</script>

<style scoped>

</style>