// src/components/AdminEventManagement.js
"use client";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Container, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const AdminEventManagement = () => {
  const [events, setEvents] = useState([]);

  useEffect(() => {
    const fetchEvents = async () => {
      const response = await axios.get('/api/events');
      setEvents(response.data);
    };

    fetchEvents();
  }, []);

  const handleDelete = async (id) => {
    await axios.delete(`/api/events/${id}`);
    setEvents(events.filter(event => event.id !== id));
  };

  return (
    <Container>
      <h1>Event Management</h1>
      <Link to="/admin/events/create"><Button>Create Event</Button></Link>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Title</th>
            <th>Date</th>
            <th>Location</th>
            <th>Category</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {events.map(event => (
            <tr key={event.id}>
              <td>{event.title}</td>
              <td>{new Date(event.eventDateTime).toLocaleDateString()}</td>
              <td>{event.location}</td>
              <td>{event.category}</td>
              <td>
                <Link to={`/admin/events/edit/${event.id}`}><Button>Edit</Button></Link>
                <Button variant="danger" onClick={() => handleDelete(event.id)}>Delete</Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Container>
  );
};

export default AdminEventManagement;
