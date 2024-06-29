// src/components/PrivateRoute.js
import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import jwt_decode from 'jwt-decode';
import { Navigate } from 'react-router-dom';

const isAuthenticated = () => {
    const token = localStorage.getItem('token');
    if (!token) return false;
  
    try {
      if (token == null) {
        localStorage.removeItem('token');
        localStorage.removeItem('UserRole');
        localStorage.removeItem('UserId');
        return false;
      }
    } catch (error) {
      return false;
    }
  
    return true;
  };
  
  export default isAuthenticated;