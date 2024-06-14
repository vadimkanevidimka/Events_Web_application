// src/components/UserEvents.js
"use client";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Container, Button } from 'react-bootstrap';
import { NavLink, Link } from 'react-router-dom';
import '../CardStyle.css'

const UserEvents = () => {
  const [events, setEvents] = useState([]);
  const [search, setSearch] = useState('');

  useEffect(() => {
    const fetchEvents = async () => {
      const userId = localStorage.getItem("UserId");
      const response = await axios.get(`http://localhost:5155/api/Event/GetUsersEvents?userid=${userId}`); // Implement this endpoint
      setEvents(response.data);
    };

    fetchEvents();
  }, []);

  return (
      <Container claccName="my-4">
      <h1>My events</h1>
      <div>
          <div class="container pb-4">
          <form class="d-flex pt-4">
              <input class="form-control me-2 rounded-pill shadow" type="search" name="SearchText" placeholder="Search" value={search} onChange={(e) => setSearch(e.target.value)}></input>
              <button class="btn btn-outline-success rounded-pill shadow" type="submit">Search</button>
          </form>
          </div>
          <div class="container my-4">
            <div class="card-columns">
          {events.map(event => (
                  <Link className='link text-decoration-none text-dark' to={`/events/${event.id}`}>
                    <div class="pinterest-card mb-4">
                        <img src={`${event.eventImage.base64URL}`} alt="Image" class="img-fluid"></img>
                        <div class="p-4 bg-transparent">
                            <h3 class="card-title">{event.title}</h3>
                            <p class="card-text text-truncate">Location: {event.location}</p>
                            <p class="card-text text-truncate">Category: {event.category}</p>
                            <p className='text-muted'>Date: {new Date(event.eventDateTime).toLocaleDateString()}</p>
                            <div class="d-inline">
                              <button to={`/events/${event.id}`} class="btn btn-success rounded-pill">View</button>
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

export default UserEvents;
