<template>
  <v-list dense>
    <v-list-item class="v-list-item--active"
        @click="setActive"
    >
      <v-list-item-icon>
        <v-icon>mdi-account-group</v-icon>
      </v-list-item-icon>
      <v-list-item-content>
        <v-list-item-title>Alunos</v-list-item-title>
      </v-list-item-content>
    </v-list-item>
  </v-list>
</template>

<script>
export default {
  name: "DrawerComponent",
  data() {
    return {
      isActive: true, // Inicialmente, o item não está ativo
    };
  },
  methods: {
    setActive() {
      this.isActive = !this.isActive; // Alterna o estado ativo ao clicar
    }
  }
}
</script>

<style scoped>
.v-list-item--active {
  background-color: #f5f5f5; 
}
</style>
