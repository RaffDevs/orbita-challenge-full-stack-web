<template>
  <v-list dense>
    <v-list-item
        link
        @click="navigateToAlunos"
    >
      <v-list-item-icon>
        <v-icon>mdi-account-group</v-icon>
      </v-list-item-icon>
      <v-list-item-content >
        <v-list-item-title>Alunos</v-list-item-title>
      </v-list-item-content>
    </v-list-item>
  </v-list>
</template>


<script>
export default {
  name: "DrawerComponent",
  data: () => ({
    items: [
      {title: 'Dashboard', icon: 'mdi-view-dashboard'},
      {title: 'Photos', icon: 'mdi-image'},
      {title: 'About', icon: 'mdi-help-box'},
    ],
    right: null,
  })

}
</script>


<style scoped>

</style>