import React from "react";
import { TextField } from "@mui/material";

const ReusableTextbox = ({ label, name, value, onChange, validation, errorText, required = false }) => {
  const isValid = !validation || validation(value);

  return (
    <TextField
      label={label}
      name={name}
      value={value}
      onChange={onChange}
      variant="outlined"
      fullWidth
      margin="normal"
      error={!isValid}
      helperText={!isValid ? errorText : ""}
      required={required}
    />
  );
};

export default ReusableTextbox;
