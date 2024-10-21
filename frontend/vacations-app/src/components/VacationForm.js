import React, { useState } from 'react';
import { submitVacationRequest } from '../services/vacationRequestsService'; 
import { TextField, Button, Typography, Box, Container } from '@mui/material';
import Stack from '@mui/material/Stack';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import dayjs from 'dayjs';
import { useNavigate } from 'react-router-dom';
import './VacationForm.css';

const VacationForm = () => {
  const navigate = useNavigate();

  // State for form inputs
  const [startDate, setStartDate] = useState(null);
  const [endDate, setEndDate] = useState(null);
  const [vacationDays, setVacationDays] = useState('');
  const [comment, setComment] = useState('');
  const [error, setError] = useState('');

  // Function to update dates and vacation days
  const updateDatesAndVacationDays = (field, value) => {
    if (field === 'startDate') {
      setStartDate(value);
      // Check if both startDate and endDate are valid
      if (value && endDate) {
        const daysDiff = endDate.diff(value, 'day') + 1;
        setVacationDays(daysDiff > 0 ? daysDiff : '');
        setEndDate(endDate);
      // Check if startDate is valid
      } else if (value && vacationDays) { 
        const calculatedEndDate = dayjs(value).add(vacationDays - 1, 'day');
        setEndDate(calculatedEndDate);
        setVacationDays(vacationDays);
      }
    } else if (field === 'endDate') {
      setEndDate(value);
      // Check if both endDate and startDate are valid
      if (value && startDate) {
        const daysDiff = value.diff(startDate, 'day') + 1;
        setVacationDays(daysDiff > 0 ? daysDiff : '');
        setStartDate(startDate);
      }
      // Check if endDate and vacationDays are valid
      if (value && vacationDays) {
        const calculatedStartDate = dayjs(value).subtract(vacationDays - 1, 'day');
        setStartDate(calculatedStartDate);
        setVacationDays(vacationDays);
      }
    } else if (field === 'vacationDays') {
      setVacationDays(value > 0 ? value : '');
      // Check if vacationDays and startDate are valid
      if (value && startDate) {
        const calculatedEndDate = dayjs(startDate).add(value - 1, 'day');
        setEndDate(calculatedEndDate);
        setStartDate(startDate);
      }
      // Check if vacationDays and endDate are valid
      if (value && endDate) {
        const calculatedStartDate = dayjs(endDate).subtract(value - 1, 'day');
        setStartDate(calculatedStartDate);
        setEndDate(endDate);
      }
    }
    setError('');
  };

  // Form submission handler
  const handleSubmit = async (event) => {
    event.preventDefault();

    const validationError = validateForm();
    if (validationError) {
      setError(validationError);
      return;
    }

    const vacationRequestData = {
      startDate: startDate.format('YYYY-MM-DD'),
      endDate: endDate.format('YYYY-MM-DD'),
      vacationDays,
      comment,
    };

    try {
      const response = await submitVacationRequest(vacationRequestData);
      console.log('Vacation request submitted:', response);
      navigate('/vacation_requests');
    } catch (err) {
      console.error('Error submitting vacation request:', err);
      setError('Failed to submit vacation request. Please try again.');
    }
  };

  const validateForm = () => {
    if (!startDate) {
      return 'Please select a start date.';
    }
    if (!endDate) {
      return 'Please select an end date.';
    }
    if (startDate.isAfter(endDate)) {
      return 'Start date cannot be after the end date.';
    }
    if (!vacationDays || vacationDays <= 0) {
      return 'Please enter a valid number of vacation days.';
    }
    return '';
  };

  // Cancel button handler to redirect to the requests list page
  const handleCancel = () => {
    navigate('/vacation_requests');
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Container maxWidth="sm" className="form-container">
        <Typography variant="h4" gutterBottom className="form-title">
          Submit a Vacation Request
        </Typography>

        <form onSubmit={handleSubmit}>
          <Box display="flex" justifyContent="space-between" marginBottom={2}>
            <DatePicker
              label="Start Date"
              value={startDate}
              onChange={(newValue) => updateDatesAndVacationDays('startDate', newValue)}
              renderInput={(params) => <TextField {...params} fullWidth className="date-picker-field" variant="outlined" />}
            />

            <DatePicker
              label="End Date"
              value={endDate}
              onChange={(newValue) => updateDatesAndVacationDays('endDate', newValue)}
              renderInput={(params) => <TextField {...params} fullWidth className="date-picker-field" variant="outlined" />}
            />
          </Box>

          <Box marginBottom={2}>
            <TextField
              label="Vacation Days"
              type="number"
              value={vacationDays}
              onChange={(e) => updateDatesAndVacationDays('vacationDays', parseInt(e.target.value, 10))}
              variant="outlined"
              className="vacation-days-input"
            />
          </Box>

          <Box marginBottom={2}>
            <TextField
              label="Comment"
              fullWidth
              multiline
              rows={4}
              value={comment}
              onChange={(e) => setComment(e.target.value)}
              variant="outlined"
              className="comment-input"
            />
          </Box>

          {error && (
            <Typography color="error" marginBottom={2} className="error-message">
              {error}
            </Typography>
          )}

          <Stack direction="row" spacing={2} className="button-group">
            <Button variant="contained" type="submit">Submit</Button>
            <Button variant="outlined" onClick={handleCancel}>Cancel</Button>
          </Stack>
        </form>
      </Container>
    </LocalizationProvider>
  );
};

export default VacationForm;
