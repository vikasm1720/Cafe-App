import React from "react";
import { Link } from "react-router-dom";
import { AppBar, Toolbar, Typography, Button } from "@mui/material";

const Header = () => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Cafe App
        </Typography>
        <Button color="inherit" component={Link} to="/cafes">
          Cafes
        </Button>
        <Button color="inherit" component={Link} to="/employees">
          Employees
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default Header;
