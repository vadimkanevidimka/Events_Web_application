// src/components/EventList.js
"use client";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Container, Button, Image } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import '../CardStyle.css'

const EventList = () => {
  const [events, setEvents] = useState([]);
  const [search, setSearch] = useState('');
  const [category, setCategory] = useState('');
  const [location, setLocation] = useState('');

  useEffect(() => {
    const fetchEvents = async () => {
      const response = await axios.get('http://localhost:5155/api/Event/GetBySearch'
      , { 
        params: {
          search,
          category,
          location,
        },
      }
      );
      setEvents(response.data);
    };

    fetchEvents();
  }, [search, category, location]);

  return (
    <Container className='my-4'>
      <h1>Events</h1>
      <div>
          <div class="container pb-4">
          <form class="d-flex pt-4">
            <input className='form-control rounded-pill shadow m-2' type="text" placeholder="Search" value={search} onChange={(e) => setSearch(e.target.value)} />
            <input className='form-control rounded-pill shadow m-2' type="text" placeholder="Category" value={category} onChange={(e) => setCategory(e.target.value)} />
            <input className='form-control rounded-pill shadow m-2' type="text" placeholder="Location" value={location} onChange={(e) => setLocation(e.target.value)} />
          </form>
          </div>
          <div class="container my-4">
            <div class="card-columns">
          {events.map(event => (
                  <Link className='link text-decoration-none text-dark' to={`/events/${event.id}`}>
                    <div class="pinterest-card mb-4">
                        <img src={`${event.eventImage.base64URL}`} alt="Image"></img>
                        <div class="p-4 bg-transparent">
                            <h3 class="card-title">{event.title}</h3>
                            <p class="card-text text-truncate">Location: {event.location}</p>
                            <p class="card-text text-truncate">Category: {event.category}</p>
                            <p className='text-muted'>Date: {new Date(event.eventDateTime).toLocaleDateString()}</p>
                            <div class="d-inline">
                            {event.maxParticipants - event.participants.length > 0 ? (
                                  <button to={`/events/${event.id}`} class="btn btn-success rounded-pill">View</button>
                                  ) : (
                                    <p>No seats available</p>
                                  )}
                            </div>
                        </div>
                    </div>
                  </Link>
      ))}
        </div>
      </div>
    </div>
    </Container>
  );
};

export default EventList;