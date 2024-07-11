// src/App.js
"use client";
import React, { Children, Component } from 'react';
import { BrowserRouter as Router, Route, Routes, Link, NavLink, Navigate, useHref } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Register from './components/register.jsx';
import Login from './components/login.jsx';
import EventList from './components/eventlist.jsx';
import EventDetail from './components/eventdetail.jsx';
import UserEvents from './components/userevents.jsx';
import AdminEventManagement from './components/admineventmanagment.jsx';
import CreateEvent from './components/createevent.jsx';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import PrivateRoute from './components/privateroute.jsx';
import isAuthenticated from './components/isauthenticated.jsx';
import AdminRoute from './components/adminroute.jsx';
import isAdminAuthenticated from './components/adminroute.jsx'
import { Button } from 'react-bootstrap';
import { Content } from 'next/font/google';


function App() {
  return (
    <Router>
        <header>
            <nav className="navbar navbar-expand-lg navbar-light bg-light shadow p-4">
                <ul className="navbar-nav me-auto mb-2 mb-lg-0 ms-lg-4">
                    <li className="nav-item">
                        <NavLink className="nav-link" to='/'>Home</NavLink>
                    </li>
                    {
                        localStorage.getItem("UserRole") == '2' ? 
                        (<li>
                            <NavLink className="nav-link" to="/CreateEvent">Create Event</NavLink>
                        </li>) : (<></>)
                    }
                    <li>
                        <NavLink className="nav-link" to="/myevents">My events</NavLink>
                    </li>
                </ul>
                { 
                    isAuthenticated() ? 
                    ( <NavLink className="btn btn-danger rounded-pill" to="/login" onClick={() => {
                        localStorage.removeItem("UserId");
                    localStorage.removeItem("token");
                            }}>
                                LogOut
                    </NavLink>) : 
                    (
                    <NavLink className="btn rounded-pill" to="/login" onClick={() => {
                        localStorage.removeItem("UserId");
                    localStorage.removeItem("token");
                            }}>
                                Log In
                    </NavLink>)
                }
            </nav>
        </header>
            <Container className='my-4'>
            <Routes>
                <Route path="/register" Component={Register}/>
                <Route path="/login" Component={Login} />
                <Route
                    path="/"
                    element={
                        <EventList />
                    }
                />
                <Route
                    path="/events/:id"
                    element={
                        <PrivateRoute>
                        <EventDetail />
                        </PrivateRoute>
                    }
                />
                <Route
                    path="/events"
                    element={
                        <PrivateRoute>
                        <EventList />
                        </PrivateRoute>
                    }
                />
                <Route
                    path="/CreateEvent"
                    element={
                        <AdminRoute>
                        <CreateEvent />
                        </AdminRoute>
                    }
                />
                <Route
                    path="/myevents"
                    element={
                        <PrivateRoute>
                        <UserEvents />
                        </PrivateRoute>
                    }
                />
                <Route
                    path="/admin"
                    element={
                        <PrivateRoute>
                        <AdminEventManagement />
                        </PrivateRoute>
                    }
                />
            </Routes>
            </Container>
            <footer className='footer backgroung-light shadow p-4'>
                <p className='text-muted'>Vadim Degtyaruk proj<br/>2024</p>
            </footer>
  </Router> 
  );
}

export default App;
