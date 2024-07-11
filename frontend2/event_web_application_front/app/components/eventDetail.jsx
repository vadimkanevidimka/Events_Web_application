// src/components/EventDetail.js
"use client";
import EditEvent from './eventdetailsforadmin.jsx';
import EventDetail from './eventdetailsforuser.jsx';
import '../eventview.css';

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
