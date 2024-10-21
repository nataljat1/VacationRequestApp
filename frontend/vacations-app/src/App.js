import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Button, Container } from '@mui/material';
import VacationList from './components/VacationList';
import VacationForm from './components/VacationForm';
import './App.css';

function App() {
  return (
    <Router>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Vacation Requests
          </Typography>
          <Button color="inherit" component={Link} to="/vacation_requests">
            View Requests
          </Button>
          <Button color="inherit" component={Link} to="/submit_request">
            Submit Request
          </Button>
        </Toolbar>
      </AppBar>

      <Container sx={{ mt: 4 }}>
        <Routes>
          <Route path="/" element={<VacationList />} />
          <Route path="/vacation_requests" element={<VacationList />} />
          <Route path="/submit_request" element={<VacationForm />} />
        </Routes>
      </Container>
    </Router>
  );
}

export default App;
