"use client";
import React, { useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, useHistory, NavLink } from 'react-router-dom';
import { Form, Button, Container } from 'react-bootstrap';

const NotFound = () => {
    return (
        <div className='container'>
            <h1>Not found!</h1>
            <h3>Put something else</h3>
        </div>
    );
}

export default NotFound;