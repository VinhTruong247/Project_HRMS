import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

function Protected (props) {
    const { children } = props
    const token = localStorage.getItem('jwtToken');
    const navigate = useNavigate();

    useEffect(() =>{
        if(!token){
        
            navigate("/login");
        }
    }, [token])

    return children
}

export default Protected;