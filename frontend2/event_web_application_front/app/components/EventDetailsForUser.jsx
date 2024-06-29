"use client";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Button } from 'react-bootstrap';
import EditEvent from './EventDetailsForAdmin';
import '../EventView.css';

const EventDetail = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [event, setEvent] = useState(null);

  useEffect(() => {
    const fetchEvent = async () => {
      const response = await axios.get(`http://localhost:5155/api/Event/GetById?id=${id}`);
      setEvent(response.data);
    };

    fetchEvent();
  }, [id]);

  const handleRegister = async () => {
      const userId = localStorage.getItem("UserId");
      const response = await axios.post(`http://localhost:5155/api/Event/AddParticipantToEvent?eventid=${id}&userid=${userId}`); // Implement this endpoint
      alert('Registration successful!');
      if(response.status == 200){
        var button = document.getElementById('RegButton');
        button.className = "btn btn-danger rounded-pill";
        button.onclick = {handleUnRegister};
        button.textContent = "Unregister";
      }
  };

  const handleUnRegister = async () => {
    const userId = localStorage.getItem("UserId");
    const response = await axios.post(`http://localhost:5155/api/Event/DeleteParticipantFromEvent?eventid=${id}&userid=${userId}`); // Implement this endpoint
    alert('You have been unreged successfully!');
    if(response.status == 200){
      var button = document.getElementById('RegButton');
      button.className = "btn btn-success rounded-pill";
      button.onclick = {handleRegister};
      button.textContent = "Register";
    }
};

const handleDelete = async () => {
  const response = await axios.delete(`http://localhost:5155/api/Event/Delete?id=${id}`); // Implement this endpoint
  if(response.status == 200){
    alert("Delete completed");
    navigate(-1);
  }
};

  if (!event) return <div>Loading...</div>;

  return (
      <div class="container mt-5 mb-5">
        <h1>Event details</h1>
          <div class="row">
          <div class="col-lg-4">
              <img src={`${event.eventImage.base64URL}`} alt="https://via.placeholder.com/500x700" class="pin-image img shadow rounded"></img>
          </div>
          <div class="col-lg-8 shadow rounded">
              <div class="pin-details">
                  <h1 class="pin-title">{event.title}</h1>
                  <p class="pin-description"><strong>Description: </strong>{event.description}</p>
                  <p><strong>Date:</strong> {new Date(event.eventDateTime).toLocaleString()}</p>
                  <p><strong>Location:</strong> {event.location}</p>
                  <p><strong>Category:</strong> {event.category}</p>
                  <p><strong>Available Seats:</strong> {event.maxParticipants - event.participants.length}</p>
                  { event.maxParticipants - event.participants.length > 0 ? (
                      (Boolean(event.participants.find(x => x.id == localStorage.getItem("UserID") != null)) ? 
                      (<Button id='RegButton' className="btn btn-danger rounded-pill" onClick={handleUnRegister}>Unregister</Button> )
                      : (<Button id='RegButton' className="btn btn-success rounded-pill" onClick={handleRegister}>Register</Button>)))
                      : (
                        <p>No seats available</p>
                      )
                  }
                  <button class="btn btn-outline-warning rounded-pill m-3" onClick={() => navigate(-1)}>Back</button>
                  {
                        localStorage.getItem("UserRole") == '2' ? 
                        (<Button className="btn btn-danger rounded-pill" onClick={handleDelete}  to="/">Delete</Button>) : (<></>)
                    }

              </div>
          </div>
          </div>
      </div>
  );
};

export default EventDetail;