import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Box, Button, Typography, Radio, RadioGroup, FormControlLabel, TextField, FormControl, InputLabel, Select, MenuItem, FormHelperText } from "@mui/material";
import ReusableTextbox from "../../components/ReusableTextbox";
import { fetchEmployeeById, addEmployee, updateEmployee, fetchCafes } from "../../services/endpoint";

const AddEditEmployee = () => {
  const { employeeId } = useParams();
  const navigate = useNavigate();
  const [employee, setEmployee] = useState({
    id: "",
    name: "",
    emailAddress: "",
    phoneNumber: "",
    gender: "Male", 
    cafeId: "", 
    startDate: "",
  });
  const [cafes, setCafes] = useState([]);
  const [isDirty, setIsDirty] = useState(false); 

  // Fetch employee data if editing
  useEffect(() => {
    if (employeeId) {
      const fetchData = async () => {
        try {
          const response = await fetchEmployeeById(employeeId);
          const matchedEmp = response.data.find(emp => emp.id === employeeId);

                    if (matchedEmp) {
                        setEmployee({
                        ...matchedEmp,
                        gender: matchedEmp.gender || "Male", 
                        startDate: matchedEmp.startDate || "",
                      });
                    } else {
                        console.error(`Employee with ID ${employeeId} not found.`);
                    }
          
        } catch (error) {
          console.error("Error fetching employee details:", error);
        }
      };
      fetchData();
    }
  }, [employeeId]);

  // Fetch available cafes for the dropdown
  useEffect(() => {
    const fetchCafesData = async () => {
      try {
        const response = await fetchCafes();
        setCafes(response.data); 
      } catch (error) {
        console.error("Error fetching cafes:", error);
      }
    };
    fetchCafesData();
  }, []);

  // Handle input changes and set the form as dirty
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEmployee({ ...employee, [name]: value });
    setIsDirty(true);
  };

  // Handle gender change
  const handleGenderChange = (e) => {
    setEmployee({ ...employee, gender: e.target.value });
    setIsDirty(true);
  };

  // Handle the form submit (add or update)
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (employeeId) {
        await updateEmployee(employee); 
      } else {
        await addEmployee(employee); 
      }
      setIsDirty(false); 
      navigate("/employees"); 
    } catch (error) {
      console.error("Error saving employee:", error);
    }
  };

  // Handle unsaved changes warning
  useEffect(() => {
    const handleBeforeUnload = (e) => {
      if (isDirty) {
        const message = "You have unsaved changes. Are you sure you want to leave?";
        e.returnValue = message; 
        return message; 
      }
    };

    window.addEventListener("beforeunload", handleBeforeUnload);
    return () => window.removeEventListener("beforeunload", handleBeforeUnload);
  }, [isDirty]);

  return (
    <Box sx={{ padding: "20px", maxWidth: "600px", margin: "auto" }}>
      <Typography variant="h4" gutterBottom>
        {employeeId ? "Edit Employee" : "Add New Employee"}
      </Typography>
      <form onSubmit={handleSubmit}>
        <ReusableTextbox
          label="Name"
          name="name"
          value={employee.name}
          onChange={handleInputChange}
          validation={(value) => value.length >= 6 && value.length <= 10}
          errorText="Name must be between 6 and 10 characters."
          required
        />
        <ReusableTextbox
          label="Email"
          name="emailAddress"
          value={employee.emailAddress}
          onChange={handleInputChange}
          validation={(value) => /\S+@\S+\.\S+/.test(value)}
          errorText="Please enter a valid email address."
          required
        />
        <ReusableTextbox
          label="Phone Number"
          name="phoneNumber"
          value={employee.phoneNumber}
          onChange={handleInputChange}
          validation={(value) => /^(8|9)\d{7}$/.test(value)}
          errorText="Phone number must start with 8 or 9 and contain 8 digits."
          required
        />

        <FormControl component="fieldset" sx={{ marginTop: 2 }}>
          <Typography variant="subtitle1">Gender</Typography>
          <RadioGroup
            name="gender"
            value={employee.gender}
            onChange={handleGenderChange}
            row
          >
            <FormControlLabel value="Male" control={<Radio />} label="Male" />
            <FormControlLabel value="Female" control={<Radio />} label="Female" />
          </RadioGroup>
        </FormControl>

        <FormControl fullWidth sx={{ marginTop: 2 }}>
          <InputLabel>Assigned Cafe</InputLabel>
          <Select
            label="Assigned Cafe"
            name="cafeId"
            value={employee.cafeId}
            onChange={handleInputChange}
            required
          >
            {cafes.map((cafe) => (
              <MenuItem key={cafe.id} value={cafe.id}>
                {cafe.name}
              </MenuItem>
            ))}
          </Select>
          <FormHelperText>Select a cafe to assign the employee</FormHelperText>
        </FormControl>

        <TextField
          label="Start Date"
          name="startDate"
          type="date"
          value={employee.startDate}
          onChange={handleInputChange}
          required
          fullWidth
          sx={{ marginTop: 2 }}
          InputLabelProps={{
            shrink: true,
          }}
        />

        <Box sx={{ marginTop: "20px" }}>
          <Button variant="contained" color="primary" type="submit" sx={{ marginRight: 2 }}>
            Submit
          </Button>
          <Button variant="outlined" color="secondary" onClick={() => navigate("/employees")}>
            Cancel
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default AddEditEmployee;
