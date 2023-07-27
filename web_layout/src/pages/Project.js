import React, { useState, useEffect } from 'react';

function Project() {
  const [projects, setProjects] = useState([]);

  useEffect(() => {
    fetch('https://gearheadhrmsdb.azurewebsites.net/api/Project/get/projects')
      .then(response => response.json())
      .then(data => setProjects(data))
      .catch(error => console.log(error));
  }, []);

  return (
    <div className="projects">
      <div className="projects-header">
      </div>
      <div className="projects-list">
        {projects.map(project => (
          <div key={project.id} className="project-card">
            <h2>{project.name}</h2>
            <p>{project.description}</p>
            <div className="project-details">
              <div className="project-detail">
                <h3>Start Date</h3>
                <p>{project.startDate}</p>
              </div>
              <div className="project-detail">
                <h3>End Date</h3>
                <p>{project.endDate}</p>
              </div>
              <div className="project-detail">
                <h3>Status</h3>
                <p>{project.status}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Project;
