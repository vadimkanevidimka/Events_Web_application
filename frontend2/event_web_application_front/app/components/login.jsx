// src/components/Login.js
"use client";
import React, { useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, useHistory, NavLink } from 'react-router-dom';
import { Form, Button, Container } from 'react-bootstrap';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const user = {
      email,
      password,
    };

    try {
      const response = await axios.post('http://localhost:5155/api/Account/Token', user);
      localStorage.setItem('token', response.data.access_token);
      localStorage.setItem('UserId',  response.data.id);
      localStorage.setItem('UserRole', response.data.userrole)
      if(response.status == 200){
        alert('Login successful!');
        navigate('/');
      }
      else{
        alert(`Error, something wrong!`);
      }
      
    } catch (error) {
      console.error('There was an error logging in!', error);
    }
  };

  return (
    <div class="container my-5 col-4">
      <h1>Login</h1>
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="email">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </Form.Group>
        <Form.Group controlId="password">
          <Form.Label>Password</Form.Label>
          <Form.Control
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </Form.Group>
        <div class="row p-2 pt-4">
          <button type="submit" value="Login" data-mdb-button-init data-mdb-ripple-init class="btn btn-primary rounded-pill mb-4">Login</button>
        </div>
        <div class="text-center">
          <p>Not a member? <NavLink to="/register" class="dark">Register</NavLink></p>
        </div>
      </Form>
    </div>
  );
};

export default Login;
