"use client";
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState } from 'react';
import axios from 'axios';
import { Container } from 'react-bootstrap';

const CreateEvent = () => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [eventDateTime, setEventDateTime] = useState('');
  const [location, setLocation] = useState('');
  const [category, setCategory] = useState('');
  const [maxParticipants, setMaxParticipants] = useState('');
  const [Base64URL, setbase64url] = useState('');
  const [eventimage, setImage] = useState([]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const eventimage = {
      Base64URL
    };
    const event = {
      title,
      description,
      eventDateTime: new Date().toISOString(),
      location,
      category,
      maxParticipants,
      eventimage,
    };
    // const formData = new FormData();
    // formData.append('title', title);
    // formData.append('description', description);
    // formData.append('eventDateTime', eventDateTime);
    // formData.append('location', location);
    // formData.append('category', category);
    // formData.append('maxParticipants', maxParticipants);
    // formData.append('eventimage', eventimage);

    try {
      var response = await axios.post('http://localhost:5155/api/Event/Add', event)
      if(response.status == 200) alert('Event created successfully!');
    } catch (error) {
      console.error('There was an error creating the event!', error);
    }
  };

    const handleImageChange=(e)=>{console. log(e. target. files);
        const data = new FileReader();
        data.addEventListener('load' , ()=>{setbase64url(data.result)});
            data.readAsDataURL(e.target.files[0]);
        };

  return (
      <Container className='py-5'>

    <form onSubmit={handleSubmit}>
        <div class="row ">
          <div class="col-md-5">
            <label class="form-label"><strong>Event image:</strong></label>
            <img src={Base64URL} name="CollectionImage" alt="Person using a laptop" class="img-fluid rounded-3 shadow"></img>
          </div>
          <div class="col-md-7">

            <div class="mb-3">
              <label class="form-label">Title:</label>
              <input class="form-control" type="text" value={title} onChange={(e) => setTitle(e.target.value)} required />
            </div>
            <div class="mb-3">
              <label class="form-label">Description:</label>
              <textarea  class="form-control" value={description} onChange={(e) => setDescription(e.target.value)} />
            </div>
            <div class="mb-3">
              <label class="form-label">Date and Time:</label>
              <input class="form-control" type="datetime-local" value={eventDateTime} onChange={(e) => setEventDateTime(e.target.value)} required />
            </div>
            <div class="mb-3">
              <label class="form-label">Location:</label>
              <input class="form-control" type="text" value={location} onChange={(e) => setLocation(e.target.value)} required />
            </div>
            <div class="mb-3">
              <label class="form-label">Category:</label>
              <input class="form-control" type="text" value={category} onChange={(e) => setCategory(e.target.value)} required />
            </div>
            <div class="mb-3">
              <label class="form-label">Max Participants:</label>
              <input class="form-control" type="number" value={maxParticipants} onChange={(e) => setMaxParticipants(e.target.value)} required />
            </div>
            <div class="mb-3">
              <label class="form-label">Image:</label>
              <input class="form-control" type="file" onChange={handleImageChange} />
            </div>
            <button class="btn btn-success rounded-pill" type="submit">Create Event</button>
          </div>
        </div>
        </form>
      </Container>
  );
};

export default CreateEvent;
