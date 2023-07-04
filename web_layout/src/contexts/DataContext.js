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

    const UserId = decodedToken.UserId
    const employee_url = `https://localhost:7220/api/Employee/get/user/${UserId}/employee`;

    useEffect(() => {
        fetch(employee_url, {
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
            .then(employee => {
                setData(employee)
                console.log(employee);
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }, [employee_url, token]);

    useEffect(() => {
        fetch(employee_url, {
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
    }, [employee_url, token]);

    return (
        <DataContext.Provider value={data}>{children}</DataContext.Provider>
    )
}

export default DataProvider

//"username": "Ho",
//"password": "a123!",