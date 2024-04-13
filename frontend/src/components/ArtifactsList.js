import React, { useState, useEffect } from 'react';
import './Cards.css';

function ArtifactsList() {
  const [artifacts, setArtifacts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5015/api/artifacts')
      .then(response => {
        if (!response.ok) {
          throw new Error(`Server responded with status: ${response.status} - ${response.statusText}`);
        }
        return response.json();
      })
      .then(data => {
        setArtifacts(data);
        setLoading(false);
      })
      .catch(error => {
        setError(error.message);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <div className="cards__container">
      <video src='/videos/3795834-uhd_2160_4096_25fps.mp4' autoPlay loop muted className="video-background" />
      <h1>Artifacts</h1>
      
      <div className="cards__items">
        {artifacts.map(artifact => (
          <div key={artifact.id} className="cards__item">
            <div className="cards__item__link">
              <div className="cards__item__pic-wrap" data-category={artifact.name}>
                <img src={artifact.image} alt={artifact.name} className="cards__item__img" />
              </div>
              <div className="cards__item__info">
                <p>Artist: {artifact.artist}</p>
                <p>Description: {artifact.description}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ArtifactsList;
