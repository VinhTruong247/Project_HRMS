import React from 'react'
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

function Protected(props) {
    const { children } = props
    const [loading, setLoading] = useState(true)
    const token = localStorage.getItem('jwtToken');
    const navigate = useNavigate();

    useEffect(() => {
        if (!token) {
            navigate("/login");
        }
        setLoading(false)
    }, [token])

    return !loading && token ? children : <React.Fragment />
}

export default Protected;