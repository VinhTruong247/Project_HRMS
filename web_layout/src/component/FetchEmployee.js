import jwt_decode from "jwt-decode";
import { useEffect } from 'react';

function FetchEmployee() {
    const token = localStorage.getItem('jwtToken');
    let decodedToken
    if (token) {
        decodedToken = jwt_decode(token);
    }

    const UserId = decodedToken.UserId
    const employee_url = `https://gearheadhrmsdb.azurewebsites.net/api/Employee/get/user/${UserId}/employee`;

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
                console.log(employee);
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }, [employee_url, token]);
}
export default FetchEmployee