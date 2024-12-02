import axios from 'axios';

// Define the base URL for your API
const API_BASE_URL = "/api";

// Create an Axios instance
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
   "Access-Control-Allow-Origin": "*"
  },
  withCredentials: true // Include cookies if your API uses them
});

// Axios request interceptor (optional for logging or debugging)
axiosInstance.interceptors.request.use(
  (config) => {
    console.log("Request Sent:", config);
    return config;
  },
  (error) => Promise.reject(error)
);

// Axios response interceptor (optional for error handling)
axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error("Error Response:", error);
    return Promise.reject(error);
  }
);

// API methods
export const fetchCafes = (location) => 
  axiosInstance.get(`/cafes`, { params: { location } });

export const fetchEmployees = (cafe) => 
  axiosInstance.get(`/employees`, { params: { cafe } });
export const fetchCafeById = (id) => 
    axiosInstance.get(`/cafes`,{ params: { id } });
export const fetchEmployeeById = (id) =>
    axiosInstance.get(`/employees`,{ params: { id } });
export const addCafe = (data) => 
  axiosInstance.post(`/cafes`, data);

export const updateCafe = (data) => 
  axiosInstance.put(`/cafes`, data);

export const deleteCafe = (id) => 
  axiosInstance.delete(`/cafes/${id}`);

export const addEmployee = (data) => 
  axiosInstance.post(`/employees`, data);

export const updateEmployee = (data) => 
  axiosInstance.put(`/employees`, data);

export const deleteEmployee = (id) => 
  axiosInstance.delete(`/employees/${id}`);
