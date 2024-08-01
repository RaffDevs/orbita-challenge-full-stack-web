<template>
  <v-card :class="{'student-card': true, 'inactive': !student.active}" max-width="400px">
    <v-card-title>{{ student.name }}</v-card-title>
    <v-card-subtitle>RA: {{ student.ra }}</v-card-subtitle>
    <v-card-text>Status: {{ student.active ? 'Ativo' : 'Inativo' }}</v-card-text>
    <v-card-actions>
      <v-btn icon @click="showDetails" color="primary">
        <v-icon>mdi-eye-outline</v-icon>
      </v-btn>
      <v-btn icon @click="requestDelete" color="red" text>
        <v-icon>mdi-delete-outline</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: 'StudentCard',
  props: {
    student: Object,
    index: Number,
  },
  methods: {
    showDetails() {
      this.$emit('show-details', this.student);
    },
    requestDelete() {
      this.$emit('request-delete', this.index);
    },
  },
}
</script>

<style scoped>
.student-card {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  transition: box-shadow 0.1s ease, background-color 0.3s ease;
}

.student-card:hover {
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.3);
}

.inactive {
  background-color: #f5f5f5;
  color: #888;
}
</style>
