import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Button, TextField, Box } from '@mui/material';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { fetchCafes, deleteCafe } from '../../services/endpoint';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

const Cafes = () => {
    const [cafes, setCafes] = useState([]);
    const [location, setLocation] = useState('');

    // Fetch cafes from the API
    const fetchData = async () => {
        try {
            const response = await fetchCafes(location);
            setCafes(response.data);
        } catch (error) {
            console.error('Error fetching cafes:', error);
        }
    };

    useEffect(() => {
        fetchData();
    }, [location]);

    // Handle deleting a cafe
    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this café?')) {
            await deleteCafe(id);
            fetchData();
        }
    };

    // Column definitions for Ag-Grid
    const columns = [
        {
            headerName: "Logo",
            field: "logo",
            cellRenderer: (params) => (
                <img src={`data:image/jpeg;base64,${params.value}`} alt="Cafe Logo" width="50" height="50" />
            ),
            sortable: true,
            filter: true,
        },
        {
            headerName: "Name",
            field: "name",
            sortable: true,
            filter: true,
        },
        {
            headerName: "Description",
            field: "description",
            sortable: true,
            filter: true,
        },
        {
            headerName: "Employees",
            field: "employees",
            cellRenderer: (params) => (
                <Link
                    to={`/employees?cafe=${params.data.name}`}
                    style={{ textDecoration: "none", color: "#1976d2", fontWeight: "bold" }}
                >
                    {params.value}
                </Link>
            ),
            sortable: true,
            filter: true,
        },
        {
            headerName: "Location",
            field: "location",
            sortable: true,
            filter: true,
        },
        {
            headerName: "Actions",
            field: "id",
            cellRenderer: (params) => (
                <div>
                    <Button
                        variant="contained"
                        color="primary"
                        component={Link}
                        to={`/editcafe/${params.value}`}
                    >
                        <EditIcon />
                    </Button>
                    <Button
                        variant="contained"
                        color="secondary"
                        onClick={() => handleDelete(params.value)}
                    >
                        <DeleteIcon />
                    </Button>
                </div>
            ),
        },
    ];

    return (
        <div>
            <Box sx={{ margin: '20px' }}>
                <TextField
                    label="Filter by Location"
                    variant="outlined"
                    fullWidth
                    value={location}
                    onChange={(e) => setLocation(e.target.value)}
                />
            </Box>

            <Box sx={{ padding: '20px' }}>
                <Button variant="contained" color="primary" component={Link} to="/addcafe" sx={{ marginBottom: '20px' }}>
                    Add New Cafe
                </Button>

                <div className="ag-theme-alpine" style={{ height: 600, width: '100%' }}>
                    <AgGridReact
                        rowData={cafes}
                        columnDefs={columns}
                        pagination={true}
                        domLayout='autoHeight'
                        enableFilter={true}
                        enableSorting={true}
                    />
                </div>
            </Box>
        </div>
    );
};

export default Cafes;
