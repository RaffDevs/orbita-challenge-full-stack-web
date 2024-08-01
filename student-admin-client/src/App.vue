<template>
  <v-app>
    <v-navigation-drawer
        app
        permanent
        v-model="drawer"
        clipped
    >
      <DrawerComponent/>
    </v-navigation-drawer>
    <v-app-bar app elevation="0" clipped-left>
      <v-toolbar-title>Student Admin</v-toolbar-title>
    </v-app-bar>
    <v-main>
      <MainPage/>
    </v-main>
  </v-app>
</template>

<script>
import MainPage from "@/pages/MainPage.vue";
import DrawerComponent from "@/components/DrawerComponent.vue";

export default {
  name: 'App',

  components: {
    MainPage,
    DrawerComponent
  },

  data: () => ({
    drawer: false
  }),
};
</script>
