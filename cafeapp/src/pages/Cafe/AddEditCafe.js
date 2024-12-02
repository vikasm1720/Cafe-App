import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Box, Button, Typography } from "@mui/material";
import ReusableTextbox from "../../components/ReusableTextbox";
import { fetchCafeById, addCafe, updateCafe } from "../../services/endpoint";

const AddEditCafe = () => {
    const { cafeId } = useParams();
    const navigate = useNavigate();
    const [cafe, setCafe] = useState({
        id: "",
        name: "",
        description: "",
        logo: "",
        location: "",
        logoName: "",
    });
    const [isDirty, setIsDirty] = useState(false);

    // Fetch cafe data if editing
    useEffect(() => {
        if (cafeId) {
            const fetchData = async () => {
                try {
                    const response = await fetchCafeById(cafeId);
                    const matchedCafe = response.data.find(cafe => cafe.id === cafeId);

                    if (matchedCafe) {
                        setCafe(matchedCafe);
                    } else {
                        console.error(`Cafe with ID ${cafeId} not found.`);
                    }
                } catch (error) {
                    console.error("Error fetching café details:", error);
                }
            };
            fetchData();
        }
    }, [cafeId]);

    // Warn user about unsaved changes
    useEffect(() => {
        const handleBeforeUnload = (e) => {
            if (isDirty) {
                const message = "You have unsaved changes. Are you sure you want to leave?";
                e.returnValue = message; // Standard for most browsers
                alert(message);
                return message; // For some browsers like Chrome
            }
        };

        window.addEventListener("beforeunload", handleBeforeUnload);
        return () => window.removeEventListener("beforeunload", handleBeforeUnload);
    }, [isDirty]);

    // Handle input changes and set unsaved state
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setCafe({ ...cafe, [name]: value });
        setIsDirty(true);
    };

    // Handle file input for logo
    const handleFileChange = async (e) => {
        const file = e.target.files[0];
        if (file && file.size > 2 * 1024 * 1024) {
            alert("File size exceeds 2MB limit.");
        } else {
            setCafe({ ...cafe, logo: file, logoName: file ? file.name : "" });
            setIsDirty(true);
        }
    };

    const isImagePath = (str) => {
        const imagePattern = /^(.*\.(jpeg|jpg|gif|png|bmp|svg))$/i;        
        return imagePattern.test(str);
      };

    // Handle form submission
    const handleSubmit = async (e) => {
        e.preventDefault();
        let logoBase64 = cafe.logo;
        if (cafe.logo && isImagePath(cafe.logo.name)) {
            const reader = new FileReader();
            logoBase64 = await new Promise((resolve, reject) => {
                reader.onload = () => resolve(reader.result.split(",")[1]);
                reader.onerror = (error) => reject(error);
                reader.readAsDataURL(cafe.logo);
            });
        }

        // Create payload
        const payload = {
            id: cafe.id,
            name: cafe.name,
            description: cafe.description,
            location: cafe.location,
            logo: logoBase64,
        };

        try {
            if (cafeId) {
                await updateCafe(payload);
            } else {
                await addCafe(payload);
            }
            navigate("/cafes");
        } catch (error) {
            console.error("Error saving café:", error);
        }
    };

    return (
        <Box sx={{ padding: "20px", maxWidth: "600px", margin: "auto" }}>
            <Typography variant="h4" gutterBottom>
                {cafeId ? "Edit Cafe" : "Add New Cafe"}
            </Typography>
            <form onSubmit={handleSubmit}>
                <ReusableTextbox
                    label="Name"
                    name="name"
                    value={cafe.name}
                    onChange={handleInputChange}
                    validation={(value) => value.length >= 6 && value.length <= 10}
                    errorText="Name must be between 6 and 10 characters."
                    required
                />
                <ReusableTextbox
                    label="Description"
                    name="description"
                    value={cafe.description}
                    onChange={handleInputChange}
                    validation={(value) => value.length <= 256}
                    errorText="Description must not exceed 256 characters."
                />
                <ReusableTextbox
                    label="Location"
                    name="location"
                    value={cafe.location}
                    onChange={handleInputChange}
                    required
                />
                <Button variant="contained" component="label" sx={{ marginY: 2 }}>
                    Upload Logo
                    <input type="file" hidden accept="image/*" onChange={handleFileChange} />
                </Button>
                {cafe.logoName && (
                    <Typography variant="body2" color="textSecondary">
                        Selected File: {cafe.logoName}
                    </Typography>
                )}
                <Box sx={{ marginTop: "20px" }}>
                    <Button variant="contained" color="primary" type="submit" sx={{ marginRight: 2 }}>
                        Submit
                    </Button>
                    <Button variant="outlined" color="secondary" onClick={() => navigate("/cafes")}>
                        Cancel
                    </Button>
                </Box>
            </form>
        </Box>
    );
};

export default AddEditCafe;
