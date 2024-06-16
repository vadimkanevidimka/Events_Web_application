"use client";
import React, { useState } from 'react';
import axios from 'axios';
import { Form, Button, Container } from 'react-bootstrap';
import { NavLink, useNavigate } from 'react-router-dom';

const Register = () => {
  const navigate = useNavigate();
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const participant = {
      firstName,
      lastName,
      dateOfBirth,
      registrationDate: new Date().toISOString(),
      email,
    };
    const user = {
      email,
      password,
      role: 1, // Assuming '1' is a default role
      participant,
    };

    try {
      const response = await axios.post('http://localhost:5155/api/Reg/Registration', user);
      if(response.status == 200) alert('Registration successful!');
      navigate(-1);
    } catch (response) {
      alert(`Registration failed! ${response.response.data.ErrorMessage}`);
    }
  };

  return (
    <div class="container my-5 col-4">
      <h1>Register</h1>
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="firstName">
          <Form.Label>First Name</Form.Label>
          <Form.Control
            type="text"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            required
          />
        </Form.Group>
        <Form.Group controlId="lastName">
          <Form.Label>Last Name</Form.Label>
          <Form.Control
            type="text"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
          />
        </Form.Group>
        <Form.Group controlId="dateOfBirth">
          <Form.Label>Date of Birth</Form.Label>
          <Form.Control
            type="date"
            value={dateOfBirth}
            onChange={(e) => setDateOfBirth(e.target.value)}
            required
          />
        </Form.Group>
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
          <Button type="submit" to="/login" className="btn btn-primary rounded-pill mb-4">Sign In</Button>
        </div>
      </Form>
    </div>
  );
};

export default Register;