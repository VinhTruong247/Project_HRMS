import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../css/login.css';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';


function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [remember, setRemember] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      const response = await fetch('https://localhost:7220/api/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          username: username,
          password: password
        })
      });

      if (response.ok) {
      const token = await response.text();
      console.log('ok')
      localStorage.setItem("jwtToken", token);
      navigate ("/");
      } else {
        setError('Invalid username or password');
      }
    } catch (error) {
      console.log(error);
      setError('Error occurred while logging in');
    }
  };

  const toggleShowPassword = () => {
    setShowPassword(!showPassword);
  };

    return (
      <div className="login">
        <div className="login-container">
          <div className="row">
            <div className="col-lg-4"></div>

            <div className="col-lg-4 login_form">
              <h2>Login</h2>
              {error && <div className="alert alert-danger">{error}</div>}
              <form onSubmit={handleSubmit}>
                <div className="mb-3 mt-3">
                  <label htmlFor="loginInput">Email or Username:</label>
                  <input
                    type="text"
                    name="username"
                    placeholder="Enter your email or username here"
                    value={username}
                    onChange={(event) => setUsername(event.target.value)}
                  />
                </div>
                <div className="mb-3 password-input-container">
                  <label htmlFor="password">Password:</label>
                  <input
                    type={showPassword ? 'text' : 'password'}
                    name="password"
                    placeholder="Enter your password here"
                    value={password}
                    onChange={(event) => setPassword(event.target.value)}
                  />
                  <FontAwesomeIcon
                    icon={showPassword ? faEyeSlash : faEye}
                    className="password-icon"
                    onClick={toggleShowPassword}
                  />
                </div>
                <div className="row">
                  <div className="mb-3">
                    <label htmlFor="remember" className="form-label">
                      Remember password:
                    </label>
                    <input
                      type="checkbox"
                      name="remember"
                      value="on"
                      checked={remember}
                      onChange={(event) => setRemember(event.target.checked)}
                    />
                  </div>
                  <button type="submit">Login</button>
                </div>
              </form>
            </div>

            <div className="col-lg-4"></div>
          </div>
        </div>
      </div>
    );
}

export default Login;
