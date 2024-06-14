// src/components/EventDetail.js
"use client";
import EditEvent from './EventDetailsForAdmin';
import EventDetail from './EventDetailsForUser';
import '../EventView.css';

const Event = () => {
  return (
      <div>
        {
          localStorage.getItem("UserRole") == '2' ? <EditEvent></EditEvent> : <EventDetail></EventDetail>
        }
      </div>
  );
};

export default Event;
