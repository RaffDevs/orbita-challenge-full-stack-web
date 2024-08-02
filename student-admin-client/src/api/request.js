import axios from "axios";

const api = axios.create({
    baseURL: process.env.VUE_APP_API_URL,
});

export const getStudents = (search= '') => {
    console.log('URL', api.defaults.baseURL)
    return api.get('/api/students', { params: {search} })
}

export const createStudent = (studentData) => {
    return api.post('/api/students', studentData);
};

export const getStudentById = (id) => {
    return api.get(`/api/students/${id}`);
};

export const updateStudent = (id, studentData) => {
    return api.put(`/api/students/${id}`, studentData);
};

export const deleteStudent = (id) => {
    return api.delete(`/api/students/${id}`);
};
