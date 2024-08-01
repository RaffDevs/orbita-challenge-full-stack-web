<template>
  <v-container>
    <v-text-field
        v-model="search"
        label="Buscar Aluno"
        append-icon="mdi-magnify"
        class="mb-4"
    />

    <!-- Toolbar com abas -->
    <v-tabs v-model="tab" centered>
      <v-tab>Ativos</v-tab>
      <v-tab>Inativos</v-tab>
    </v-tabs>

    <!-- Conteúdo das abas -->
    <v-tabs-items v-model="tab" class="mt-3">
      <v-tab-item class="ma-3">
        <v-row v-if="filteredActiveStudents.length === 0">
          <v-col cols="12">
            <v-alert type="info" border="left" colored-border>
              Nenhum aluno ativo cadastrado.
            </v-alert>
          </v-col>
        </v-row>
        <v-row v-else>
          <v-col v-for="(student, index) in filteredActiveStudents" :key="student.ra" cols="12" md="4">
            <StudentCard
                :student="student"
                :index="index"
                @show-details="showStudentDetails"
                @request-delete="requestDeleteStudent"
            />
          </v-col>
        </v-row>
      </v-tab-item>

      <v-tab-item>
        <v-row v-if="filteredInactiveStudents.length === 0">
          <v-col cols="12">
            <v-alert type="info" border="left" colored-border>
              Nenhum aluno inativo cadastrado.
            </v-alert>
          </v-col>
        </v-row>
        <v-row v-else>
          <v-col v-for="(student, index) in filteredInactiveStudents" :key="student.ra" cols="12" md="4">
            <StudentCard
                :student="student"
                :index="index"
                @show-details="showStudentDetails"
                @request-delete="requestDeleteStudent"
            />
          </v-col>
        </v-row>
      </v-tab-item>
    </v-tabs-items>

    <!-- Dialog para Cadastrar Novo Aluno -->
    <v-dialog v-model="dialog" max-width="500px" @click:outside="closeDialog">
      <v-card>
        <v-card-title>
          <span class="headline">Cadastrar Novo Aluno</span>
        </v-card-title>
        <v-card-text>
          <v-form>
            <v-text-field v-model="newStudent" label="Nome" />
            <v-text-field v-model="newStudent" label="RA" />
            <v-switch v-model="newStudent" label="Ativo" />
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn text @click="closeDialog">Cancelar</v-btn>
          <v-btn color="primary" @click="addStudent">Salvar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Dialog de confirmação de exclusão -->
    <v-dialog v-model="deleteDialog" max-width="290">
      <v-card>
        <v-card-title class="headline">Confirmar Exclusão</v-card-title>
        <v-card-text>Tem certeza de que deseja excluir o aluno "{{ studentToDelete }}"?</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="grey" @click="cancelDelete">Cancelar</v-btn>
          <v-btn color="red" @click="confirmDelete">Excluir</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Dialog para Mostrar e Editar Detalhes do Aluno -->
    <v-dialog v-model="detailsDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ isEditing ? 'Editar' : 'Detalhes' }} do Aluno</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="detailsForm">
            <v-text-field
                v-model="selectedStudent"
                label="Nome"
                :disabled="!isEditing"
            />
            <v-text-field
                v-model="selectedStudent"
                label="RA"
                :disabled="!isEditing"
            />
            <v-switch
                v-model="selectedStudent"
                label="Ativo"
                :disabled="!isEditing"
            />
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn
              text
              @click="closeDetailsDialog"
          >Fechar</v-btn>
          <v-btn
              color="primary"
              v-if="!isEditing"
              @click="enableEditing"
          >Editar</v-btn>
          <v-btn
              color="primary"
              v-if="isEditing"
              @click="saveDetails"
          >Salvar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Floating Action Button -->
    <v-btn
        fab
        bottom
        right
        fixed
        color="primary"
        dark
        @click="openDialog"
    >
      <v-icon>mdi-plus</v-icon>
    </v-btn>
  </v-container>
</template>

<script>
import StudentCard from './StudentCardComponent.vue';

export default {
  name: 'StudentGrid',
  components: {
    StudentCard,
  },
  data() {
    return {
      dialog: false,
      deleteDialog: false,
      detailsDialog: false,
      studentToDelete: null,
      selectedStudent: null,
      isEditing: false,
      tab: 0,
      search: '',
      newStudent: {
        name: '',
        ra: '',
        active: false,
      },
      students: [
        { name: 'João Silva', ra: '12345f', active: true },
        { name: 'Maria Oliveira', ra: '678v90', active: false },
        { name: 'João Silva', ra: '12qwe345', active: true },
        { name: 'Maria Oliveira', ra: '6789vb0', active: false },
        {name: 'João Silva', ra: '123yukj45', active: true},
        {name: 'Maria Oliveira', ra: '67casc890', active: false},
        { name: 'João Silva', ra: '123cz45', active: true },
        { name: 'Maria Oliveira', ra: '67jyt890', active: false },
        { name: 'João Silva', ra: '123yk45', active: true },
        { name: 'Maria Oliveira', ra: '67u890', active: false },
        {name: 'João Silva', ra: '12ewq345', active: true},
        {name: 'Maria Oliveira', ra: '67890xc', active: false},
        { name: 'João Silva', ra: '12345f', active: true },
        { name: 'Maria Oliveira', ra: '678v90', active: false },
        { name: 'João Silva', ra: '12qwe345', active: true },
        { name: 'Maria Oliveira', ra: '6789vb0', active: false },
        {name: 'João Silva', ra: '123yukj45', active: true},
        {name: 'Maria Oliveira', ra: '67casc890', active: false},
        { name: 'João Silva', ra: '123cz45', active: true },
        { name: 'Maria Oliveira', ra: '67jyt890', active: false },
        { name: 'João Silva', ra: '123yk45', active: true },
        { name: 'Maria Oliveira', ra: '67u890', active: false },
        {name: 'João Silva', ra: '12ewq345', active: true},
        {name: 'Maria Oliveira', ra: '67890xc', active: false},
      ],
    };
  },
  computed: {
    filteredStudents() {
      const searchLower = this.search.toLowerCase();
      return this.students.filter(student =>
          student.name.toLowerCase().includes(searchLower) ||
          student.ra.includes(searchLower)
      );
    },
    filteredActiveStudents() {
      return this.filteredStudents.filter(student => student.active);
    },
    filteredInactiveStudents() {
      return this.filteredStudents.filter(student => !student.active);
    },
  },
  methods: {
    openDialog() {
      this.dialog = true;
    },
    closeDialog() {
      this.dialog = false;
    },
    addStudent() {
      if (this.newStudent.name && this.newStudent.ra) {
        this.students.push({...this.newStudent});
        this.newStudent = {name: '', ra: '', active: false};
        this.closeDialog();
      }
    },
    showStudentDetails(student) {
      this.selectedStudent = {...student};
      this.detailsDialog = true;
      this.isEditing = false;
    },
    closeDetailsDialog() {
      this.detailsDialog = false;
      this.selectedStudent = null;
    },
    enableEditing() {
      this.isEditing = true;
    },
    saveDetails() {
      const index = this.students.findIndex(student => student.ra === this.selectedStudent.ra);
      if (index !== -1) {
        this.$set(this.students, index, {...this.selectedStudent});
      }
      this.closeDetailsDialog();
    },
    requestDeleteStudent(index) {
      this.studentToDelete = this.students[index];
      this.deleteDialog = true;
    },
    cancelDelete() {
      this.deleteDialog = false;
      this.studentToDelete = null;
    },
    confirmDelete() {
      if (this.studentToDelete) {
        const index = this.students.indexOf(this.studentToDelete);
        if (index !== -1) {
          this.students.splice(index, 1);
        }
        this.deleteDialog = false;
        this.studentToDelete = null;
      }
    },
  },
}
</script>

<style scoped>
</style>
