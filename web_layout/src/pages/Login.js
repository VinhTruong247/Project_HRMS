import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../css/login.css';

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [remember, setRemember] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  
  const handleSubmit = (event) => {
    event.preventDefault();
    console.log("Username:", username);
    console.log("Password:", password);
    console.log("Remember:", remember);

    // Here you can make an API call to authenticate the user.
    // For demonstration purposes, I'm going to assume the user is authenticated.
    setIsLoggedIn(true);
  };
  
  if (isLoggedIn) {
    // If the user is authenticated, redirect to the home page.
    return <Redirect to="/" />;
  } else {
    return (
      <div className="login">
        <div className="login-container">
          <div className="row">
            <div className="col-lg-3"></div>
  
            <div className="col-lg-6">
              <h2>Login</h2>
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
                <div className="mb-3">
                  <label htmlFor="password">Password:</label>
                  <input
                    type="password"
                    name="password"
                    placeholder="Enter your password here"
                    value={password}
                    onChange={(event) => setPassword(event.target.value)}
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
  
            <div className="col-lg-3"></div>
          </div>
        </div>
      </div>
    );
  }
}

export default Login;