import React, { useState, useEffect } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { deleteEmployee, fetchEmployees } from "../../services/endpoint";
import { Button, Box, Typography } from "@mui/material";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const location = useLocation();
  const cafeName = new URLSearchParams(location.search).get("cafe");

  const fetchData = async () => {
    try {
      const response = await fetchEmployees(cafeName);
      setEmployees(response.data);
    } catch (error) {
      console.error("Error fetching employees:", error);
    }
  };

  useEffect(() => {
    fetchData();
  }, [cafeName]);


  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this Employee?')) {
      await deleteEmployee(id);
      fetchData();
    }
  };

  const columns = [
    { headerName: "ID", field: "id", flex: 1,  sortable: true, filter: true },
    { headerName: "Name", field: "name", flex: 2,  sortable: true, filter: true },
    { headerName: "Email", field: "emailAddress", flex: 2,  sortable: true, filter: true },
    { headerName: "Phone", field: "phoneNumber", flex: 1,  sortable: true, filter: true },
    { headerName: "Days Worked", field: "daysWorked", flex: 1,  sortable: true, filter: true },
    { headerName: "Cafe", field: "cafe", flex: 2,  sortable: true, filter: true },
    {
      headerName: "Actions",
      field: "id",
      cellRenderer: (params) => (
        <div>
          <Button
            variant="contained"
            color="primary"
            component={Link}
            to={`/editEmployee/${params.value}`}
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
    <Box sx={{ padding: "20px" }}>
      {cafeName && (
        <Typography variant="h4" gutterBottom>
          Employees under {cafeName}
        </Typography>
      )}

      <Button variant="contained" color="primary" component={Link} to="/addemployee" sx={{ marginBottom: '20px' }}>
        Add New Employee
      </Button>
      <div className="ag-theme-alpine" style={{ height: 600, width: "100%" }}>
        <AgGridReact
          rowData={employees}
          columnDefs={columns}
          pagination={true}
          domLayout="autoHeight"
          enableSorting={true}
          enableFilter={true}
        />
      </div>
    </Box>
  );
};

export default Employees;
