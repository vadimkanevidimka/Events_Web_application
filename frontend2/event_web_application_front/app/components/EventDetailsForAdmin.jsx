// src/components/EditEvent.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom';
import { Form, Button, Container } from 'react-bootstrap';
import '../eventview.css';

const EditEvent = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [Base64URL, setbase64url] = useState('');
  const [eventDateTime, setEventDateTime] = useState('');
  const eventImage = {
    Base64URL,
  };
  const [event, setEvent] = useState({
    title: '',
    description: '',
    eventDateTime: "",
    location: '',
    category: '',
    maxParticipants: 0,
    eventImage,
  });

  useEffect(() => {
    // Fetch event data by ID
    const fetchEvent = async () => {
      try {
        const response = await axios.get(`http://localhost:5155/api/Event/GetById?id=${id}`);
        setEvent(response.data);
        setEventDateTime(response.data.eventDateTime);
        setbase64url(response.data.eventImage.base64URL);
      } catch (error) {
        console.error('Error fetching event data', error);
      }
    };

    fetchEvent();
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEvent({ ...event, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.patch(`http://localhost:5155/api/Event/Update`, event);
      alert('Event updated successfully!');
    } catch (error) {
      navigate(-1);
      console.error('Error updating event', error);
    }
  };

  const handleDelete = async () => {
    const response = await axios.delete(`http://localhost:5155/api/Event/Delete?id=${id}`); // Implement this endpoint
    if(response.status == 200){
      alert("Delete completed");
      navigate(-1);
    }
  };

  const handleImageChange=(e)=>{console.log(e.target.files);
    const data = new FileReader();
    data.addEventListener('load' , ()=>{setbase64url(data.result)});
        data.readAsDataURL(e.target.files[0]);
    };

  return (
    <Container className='my-4'>
      <h1>Event details</h1>
        <div class="row ">
            <div class="col-md-4">
                <label class="form-label"><strong>Event image:</strong></label>
                <img src={Base64URL} name="CollectionImage" alt="Person using a laptop" class="img-fluid rounded-3 shadow"></img>
            </div>
            <div class="col-lg-8 shadow rounded">
              <div class="pin-details">
        <Form onSubmit={handleSubmit}>
            <Form.Group controlId="title">
            <Form.Label>Title</Form.Label>
            <Form.Control
                type="text"
                name="title"
                value={event.title}
                onChange={handleChange}
                required
            />
            </Form.Group>
            <Form.Group controlId="description">
            <Form.Label>Description</Form.Label>
            <Form.Control
                as="textarea"
                name="description"
                value={event.description}
                onChange={handleChange}
                required
            />
            </Form.Group>
            <Form.Group controlId="eventDateTime">
            <Form.Label>Date and Time</Form.Label>
            <Form.Control
                type="datetime-local"
                name="eventDateTime"
                value={eventDateTime}
                onChange={(e) => setEventDateTime(e.target.value)}
                required
            />
            </Form.Group>
            <Form.Group controlId="location">
            <Form.Label>Location</Form.Label>
            <Form.Control
                type="text"
                name="location"
                value={event.location}
                onChange={handleChange}
                required
            />
            </Form.Group>
            <Form.Group controlId="category">
            <Form.Label>Category</Form.Label>
            <Form.Control
                type="text"
                name="category"
                value={event.category.name}
                onChange={handleChange}
                required
            />
            </Form.Group>
            <Form.Group controlId="maxParticipants">
            <Form.Label>Max Participants</Form.Label>
            <Form.Control
                type="number"
                name="maxParticipants"
                value={event.maxParticipants}
                onChange={handleChange}
                required
            />
            </Form.Group>
            <Form.Group controlId="imageUrl">
            <Form.Label>Image</Form.Label>
            <br/>
            <img class="pin-image img shadow rounded mb-3" style={{width: '100px'}} src={Base64URL}></img>
            <Form.Control
            className='btn'
                type="file"
                name="imageUrl"
                onChange={handleImageChange}
            />
            </Form.Group>
            <Button className='btn btn-success rounded-pill' variant="primary" type="submit">
            Save Changes
            </Button>
            <button class="btn btn-outline-warning rounded-pill m-3" onClick={() => navigate(-1)}>Back</button>
            <Button className="btn btn-danger rounded-pill" onClick={handleDelete}  to="/">Delete</Button>
        </Form>
        </div>
        </div>
      </div>
    </Container>
  );
};

export default EditEvent;
