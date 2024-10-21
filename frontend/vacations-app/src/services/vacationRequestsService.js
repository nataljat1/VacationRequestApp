import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

// Submit a vacation request
export const submitVacationRequest = async (vacationRequestData) => {
  try {
    const response = await axios.post(`${API_URL}/vacation_requests/request`, vacationRequestData);
    return response.data; 
  } catch (error) {
    throw error; 
  }
};

// Get all vacation requests
export const getAllVacationRequests = async () => {
    try {
      const response = await axios.get(`${API_URL}/vacation_requests/all`);
      return response.data; 
    } catch (error) {
      throw error; 
    }
  };