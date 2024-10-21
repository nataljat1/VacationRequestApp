import React, { useState, useEffect } from 'react';
import { getAllVacationRequests } from '../services/vacationRequestsService';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  TablePagination,
  Typography,
  TableSortLabel,
  TextField,
  Box,
  CircularProgress,
  Alert,
} from '@mui/material';
import dayjs from 'dayjs';

const VacationList = () => {
  const [requests, setRequests] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Sorting state
  const [order, setOrder] = useState('asc');
  const [orderBy, setOrderBy] = useState('startDate');

  // Pagination state
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  // Filtering state
  const [filterComment, setFilterComment] = useState('');

  // Fetch vacation requests
  useEffect(() => {
    const fetchRequests = async () => {
      try {
        const response = await getAllVacationRequests();
        setRequests(response); 
      } catch (err) {
        setError('Failed to fetch vacation requests. Please try again.');
      } finally {
        setLoading(false);
      }
    };

    fetchRequests();
  }, []);

  // Handle sorting
  const handleRequestSort = (property) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(property);
  };

  // Handle change of page
  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  // Handle change of rows per page
  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  // Handle comment filter change
  const handleFilterChange = (event) => {
    setFilterComment(event.target.value);
  };

  // Comparator function for sorting
  const descendingComparator = (a, b, orderBy) => {
    if (dayjs(a[orderBy]).isValid() && dayjs(b[orderBy]).isValid()) {
      return dayjs(b[orderBy]).isAfter(dayjs(a[orderBy])) ? 1 : -1;
    }
    return b[orderBy] < a[orderBy] ? -1 : 1;
  };

  const getComparator = (order, orderBy) => {
    return order === 'desc'
      ? (a, b) => descendingComparator(a, b, orderBy)
      : (a, b) => -descendingComparator(a, b, orderBy);
  };

  // Apply sorting to the requests
  const sortedRequests = [...requests].sort(getComparator(order, orderBy));

  // Apply filtering based on the comment field
  const filteredRequests = sortedRequests.filter((request) =>
    request.comment.toLowerCase().includes(filterComment.toLowerCase())
  );

  // Paginate the filtered and sorted requests
  const paginatedRequests = filteredRequests.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage);

  if (loading) {
    return <CircularProgress />;
  }

  if (error) {
    return <Alert severity="error">{error}</Alert>;
  }

  return (
    <div>
      <Typography variant="h4" gutterBottom>
        My Vacation Requests
      </Typography>

      {/* Filter by Comment */}
      <Box sx={{ mb: 2 }}>
        <TextField
          label="Filter by Comment"
          variant="outlined"
          fullWidth
          value={filterComment}
          onChange={handleFilterChange}
        />
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              {/* Sortable column headers */}
              <TableCell>
                <TableSortLabel
                  active={orderBy === 'startDate'}
                  direction={orderBy === 'startDate' ? order : 'asc'}
                  onClick={() => handleRequestSort('startDate')}
                >
                  Start Date
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={orderBy === 'endDate'}
                  direction={orderBy === 'endDate' ? order : 'asc'}
                  onClick={() => handleRequestSort('endDate')}
                >
                  End Date
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={orderBy === 'vacationDays'}
                  direction={orderBy === 'vacationDays' ? order : 'asc'}
                  onClick={() => handleRequestSort('vacationDays')}
                >
                  Vacation Days
                </TableSortLabel>
              </TableCell>
              <TableCell>Comment</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {paginatedRequests.map((request) => (
              <TableRow key={request.id}>
                <TableCell>{dayjs(request.startDate).format('YYYY-MM-DD')}</TableCell>
                <TableCell>{dayjs(request.endDate).format('YYYY-MM-DD')}</TableCell>
                <TableCell>{request.vacationDays}</TableCell>
                <TableCell>{request.comment}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>

        {/* Pagination */}
        <TablePagination
          component="div"
          count={filteredRequests.length}
          page={page}
          onPageChange={handleChangePage}
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={handleChangeRowsPerPage}
          rowsPerPageOptions={[5, 10, 25]}
        />
      </TableContainer>
    </div>
  );
};

export default VacationList;
