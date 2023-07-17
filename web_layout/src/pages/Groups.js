import React, { useState } from 'react';

function Groups() {
  const [selectedMessage, setSelectedMessage] = useState(null);

  const handleSelectMessage = (message) => {
    setSelectedMessage(message);
  }

  const messages = [
    {
      id: 1,
      subject: 'Message Subject 1',
      sender: 'Sender Name 1',
      date: '09 July 2023',
      body: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla euismod urna ut nunc rutrum, a lobortis odio accumsan. Nam sodales, dolor et sollicitudin consectetur, mi enim lobortis tortor, vel rutrum tortor velit ut eros. Ut maximus nunc vel libero bibendum, non dapibus quam malesuada. Fusce sit amet orci euismod, molestie urna nec, lacinia ante. Aliquam erat volutpat.'
    },
    {
      id: 2,
      subject: 'Message Subject 2',
      sender: 'Sender Name 2',
      date: '08 July 2023',
      body: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla euismod urna ut nunc rutrum, a lobortis odio accumsan. Nam sodales, dolor et sollicitudin consectetur, mi enim lobortis tortor, vel rutrum tortor velit ut eros. Ut maximus nunc vel libero bibendum, non dapibus quam malesuada. Fusce sit amet orci euismod, molestie urna nec, lacinia ante. Aliquam erat volutpat.'
    }
  ];

  return (
    <div className="inbox">
      <div className="inbox-header">
      </div>
      <div className="inbox-messages">
        {messages.map(message => (
          <div 
            key={message.id} 
            className={`inbox-message ${selectedMessage === message.id ? 'selected' : ''}`}
            onClick={() => handleSelectMessage(message.id)}
          >
            <div className="message-header">
              <h2>{message.subject}</h2>
              <p>From: {message.sender}</p>
              <p>Received: {message.date}</p>
            </div>
            <div className="message-body">
              <p>{message.body}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Groups;