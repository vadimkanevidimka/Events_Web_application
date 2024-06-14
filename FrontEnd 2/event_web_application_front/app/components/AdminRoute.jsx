// src/components/PrivateRoute.js
import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import jwt_decode from 'jwt-decode';
import { Navigate } from 'react-router-dom';

const isAdminAuthenticated = () => {
  const token = localStorage.getItem('token');
  const role = localStorage.getItem('UserRole');
  if (!token || !role) return false;

  try {
    if (token == null || role != 2) {
      localStorage.removeItem('token');
      return false;
    }
  } catch (error) {
    return false;
  }

  return true;
};

const AdminRoute = ({ children }) => {
    const authed = isAdminAuthenticated(); // isauth() returns true or false based on localStorage
    return authed ? children : <Navigate to="/login" />;
  }

export default AdminRoute;