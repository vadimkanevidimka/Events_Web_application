// src/components/PrivateRoute.js
import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import jwt_decode from 'jwt-decode';
import { Navigate } from 'react-router-dom';
import isAuthenticated from './isauthenticated.jsx';


const PrivateRoute = ({ children }) => {
    const authed = isAuthenticated(); // isauth() returns true or false based on localStorage
    return authed ? children : <Navigate to="/login" />;
  }

export default PrivateRoute;