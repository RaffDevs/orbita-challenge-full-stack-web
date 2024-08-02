<template>
  <v-container>
    <v-row>
      <v-col cols="9">
        <v-text-field
            v-model="search"
            label="Buscar Aluno"
            append-icon="mdi-magnify"
            class="mb-4"
        />
      </v-col>
      <v-col cols="3">
        <v-btn @click="getStudents">
          Procurar
        </v-btn>
      </v-col>
    </v-row>

    <!-- Toolbar com abas -->
    <v-tabs v-model="tab" centered>
      <v-tab>Ativos</v-tab>
      <v-tab>Inativos</v-tab>
    </v-tabs>

    <!-- Conteúdo das abas -->
    <v-tabs-items v-model="tab" class="mt-3">
      <v-tab-item class="ma-3">
        <v-row v-if="students.length === 0">
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
            <v-text-field
                v-model="newStudent.ra"
                label="RA"
                :rules="raRules"
            />
            <v-text-field
                v-model="newStudent.firstName"
                label="Nome"
                :rules="nameRules"
            />
            <v-text-field
                v-model="newStudent.lastName"
                label="Sobrenome"
                :rules="nameRules"
            />
            <v-text-field
                v-model="newStudent.email"
                label="Email"
                :rules="emailRules"
            />
            <v-text-field
                v-model="newStudent.cpf"
                label="CPF"
                :rules="cpfRules"
            />
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
        <v-card-text>Tem certeza de que deseja excluir o aluno "{{ studentToDelete?.fullName }}"?</v-card-text>
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
                v-model="studentDetails.ra"
                label="RA"
                disabled
            />
            <v-text-field
                v-model="studentDetails.cpf"
                label="RA"
                disabled
            />

            <v-text-field
                v-model="studentDetails.firstName"
                label="Nome"
                :disabled="!isEditing"
            />
            <v-text-field
                v-model="studentDetails.lastName"
                label="Sobrenome"
                :disabled="!isEditing"
            />
            <v-text-field
                v-model="studentDetails.email"
                label="Email"
                :disabled="!isEditing"
            />

            <v-switch
                v-model="studentDetails.active"
                label="Ativo"
                :disabled="!isEditing"
            />
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn
              text
              @click="closeDetailsDialog"
          >Fechar
          </v-btn>
          <v-btn
              color="primary"
              v-if="!isEditing"
              @click="enableEditing"
          >Editar
          </v-btn>
          <v-btn
              color="primary"
              v-if="isEditing"
              @click="saveDetails"
          >Salvar
          </v-btn>
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
    <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="3000">
      {{ snackbar.message }}
      <v-btn color="white" text @click="snackbar.show = false">Fechar</v-btn>
    </v-snackbar>
  </v-container>
</template>

<script>
import StudentCard from './StudentCardComponent.vue';
import { getStudents, getStudentById, updateStudent, createStudent, deleteStudent} from '@/api/request';

export default {
  name: 'StudentGrid',
  components: {
    StudentCard,
  },
  created() {
    this.getStudents()
  },
  data() {
    return {
      dialog: false,
      deleteDialog: false,
      detailsDialog: false,
      studentToDelete: null,
      studentDetails: {
        firstName: "",
        lastName: "",
        ra: "",
        email: "",
        cpf: "",
        active: false
      },
      isEditing: false,
      tab: 0,
      search: '',
      newStudent: {
        firstName: "",
        lastName: "",
        ra: "",
        email: "",
        cpf: "",
      },
      snackbar: {
        show: false,
        message: '',
        color: ''
      },
      students: [],
      nameRules: [
        v => !!v || 'Nome é obrigatório',
        v => (v && v.length <= 50) || 'Nome deve ter no máximo 50 caracteres'
      ],
      emailRules: [
        v => !!v || 'Email é obrigatório',
        v => /.+@.+\..+/.test(v) || 'E-mail deve ser válido'
      ],
      raRules: [
        v => !!v || 'RA é obrigatório',
        v => (v && v.length >= 6) || 'RA deve ter no minimo 6 caracteres',
        v => (v && v.length <= 15) || 'RA deve ter no máximo 15 caracteres',
        v => /^\d+$/.test(v) || 'RA deve conter apenas números'

      ],
      cpfRules: [
        v => !!v || 'CPF é obrigatório',
      ]
    };
  },
  computed: {
    filteredActiveStudents() {
      return this.students.filter(student => student.isActive);
    },
    filteredInactiveStudents() {
      return this.students.filter(student => !student.isActive);
    },
  },
  methods: {
    async getStudents() {
      try {
        const res = await getStudents(this.search);
        this.students = res.data;
      } catch (error) {
        console.log(error);
      }
    },
    openDialog() {
      this.dialog = true;
    },
    closeDialog() {
      this.dialog = false;
    },
    async addStudent() {
      try {
        const res = await createStudent(this.newStudent);
        if (res.status === 201) {
          await this.getStudents();
          this.snackbar.message = 'Aluno adicionado com sucesso!';
          this.snackbar.color = 'success';
          this.snackbar.show = true;
          this.newStudent = { firstName: "", lastName: "", ra: "", email: "", cpf: "" };
        }
      } catch (error) {
        this.snackbar.message = `Erro ao adicionar aluno: ${error.response.data.join(",") || error.message}`;
        this.snackbar.color = 'error';
        this.snackbar.show = true;
      }
      this.closeDialog();
    },
    async showStudentDetails(student) {
      const studentData = await getStudentById(student.ra)
      this.studentDetails.firstName = studentData.data.fullName.split(' ')[0]
      this.studentDetails.lastName = studentData.data.fullName.split(' ')[1]
      this.studentDetails.email = studentData.data.email
      this.studentDetails.cpf = studentData.data.cpf
      this.studentDetails.ra = studentData.data.ra
      this.studentDetails.active = studentData.data.isActive
      this.detailsDialog = true;
      this.isEditing = false;
    },
    closeDetailsDialog() {
      this.detailsDialog = false;
    },
    enableEditing() {
      this.isEditing = true;
    },
    async saveDetails() {
      try {
        const updateData = {
          firstName: this.studentDetails.firstName,
          lastName: this.studentDetails.lastName,
          email: this.studentDetails.email,
          isActive: this.studentDetails.active,
        };
        await updateStudent(this.studentDetails.ra, updateData);
        this.snackbar.message = 'Detalhes do aluno atualizados com sucesso!';
        this.snackbar.color = 'success';
        this.snackbar.show = true;
        this.closeDetailsDialog();
        await this.getStudents();
      } catch (error) {
        this.snackbar.message = `Erro ao atualizar detalhes do aluno: ${error.response.data.join(",") || error.message}`;
        this.snackbar.color = 'error';
        this.snackbar.show = true;
      }
    },
    requestDeleteStudent(student) {
      this.studentToDelete = student;
      this.deleteDialog = true;
    },
    cancelDelete() {
      this.deleteDialog = false;
      this.studentToDelete = null;
    },
    async confirmDelete() {
      try {
        if (this.studentToDelete) {
          const res = await deleteStudent(this.studentToDelete.ra);
          if (res.status === 204) {
            await this.getStudents();
            this.snackbar.message = 'Aluno excluído com sucesso!';
            this.snackbar.color = 'success';
            this.snackbar.show = true;
          }
        }
      } catch (error) {
        this.snackbar.message = `Erro ao excluir aluno: ${error.response.data.join(",") || error.message}`;
        this.snackbar.color = 'error';
        this.snackbar.show = true;
      }
      this.deleteDialog = false;
      this.studentToDelete = null;
    },
  },
}
</script>

<style scoped>
</style>
