import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Header from "./components/Header"; // Import the Header component
import LandingPage from "./pages/LandingPage";
import Cafes from "./pages/Cafe/Cafes";
import Employees from "./pages/Employee/Employees";
import AddEditCafe from "./pages/Cafe/AddEditCafe";
import AddEditEmployee from "./pages/Employee/AddEditEmployee";

const App = () => {
  return (
    <Router>
      <Header /> {/* Include Header component here */}
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/cafes" element={<Cafes />} />
        <Route path="/employees" element={<Employees />} />
        <Route path="/addcafe" element={<AddEditCafe />} />
        <Route path="/editcafe/:cafeId" element={<AddEditCafe />} />
        <Route path="/addemployee" element={<AddEditEmployee />} /> 
        <Route path="/editemployee/:employeeId" element={<AddEditEmployee />} />
      </Routes>
    </Router>
  );
};

export default App;
