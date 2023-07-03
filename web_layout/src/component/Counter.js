import React from "react";
import { useState,useEffect } from "react"; 

function Counter() {
    const [data, setData] = useState([]);
  
    useEffect(() => {
      fetch('https://localhost:7220/api/Employee/employees')
        .then(response => response.json())
        .then(data => setData(data))
        .catch(error => console.error(error));
    }, []);
  
    const count = data.length;
  
    return (
      <div>
        <p>{count}</p>
      </div>
    );
  }

export default Counter;