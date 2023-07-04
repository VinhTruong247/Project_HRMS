import { useEffect, createContext } from 'react';
import React from 'react'
import jwt_decode from 'jwt-decode';

export const DataContext = createContext()

const DataProvider = ({ children }) => {
    const [data, setData] = React.useState([])
    const token = localStorage.getItem('jwtToken');
    let decodedToken
    if (token) {
        decodedToken = jwt_decode(token);
    }

    const DepartmentId = decodedToken.DepartmentId
    const department_url = `https://localhost:7220/api/Department/get/${DepartmentId}/department`;

    useEffect(() => {
        fetch(department_url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Api response was not ok.');
                }
            })
            .then(department => {
                setData(department)
                console.log(department);
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }, [department_url, token]);

    return (
        <DataContext.Provider value={data}>{children}</DataContext.Provider>
    )
}

export default DataProvider

//"username": "Ho",
//"password": "a123!",