"use client";
import React, { useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, useHistory, NavLink } from 'react-router-dom';
import { Form, Button, Container } from 'react-bootstrap';

const NotFoundPage = ()=>{
    return (
        <div className='container'>
            <h2>Not found!</h2>
            <h1>Put something else</h1>
        </div>
    );
}

export default NotFound