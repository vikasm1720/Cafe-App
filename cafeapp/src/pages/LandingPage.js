import React from "react";
import { Link } from "react-router-dom";
import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";

const LandingPage = () => {
  return (
    <Box>
      {/* Main Content */}
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center",
          height: "80vh",
          textAlign: "center",
        }}
      >
        <Typography variant="h3" gutterBottom>
          Welcome to the Cafe App!
        </Typography>
        <Typography variant="h6" color="textSecondary" gutterBottom>
          Manage your cafes and employees efficiently and effortlessly.
        </Typography>
        <Box mt={3}>
          <Button
            variant="contained"
            color="primary"
            component={Link}
            to="/cafes"
            sx={{ marginRight: 2 }}
          >
            View Cafes
          </Button>
          <Button variant="contained" color="secondary" component={Link} to="/employees">
            View Employees
          </Button>
        </Box>
      </Box>
    </Box>
  );
};

export default LandingPage;
