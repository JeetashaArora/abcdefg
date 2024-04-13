import React, { useState, useEffect } from 'react';
import './Cards.css';

function ArtfactsList() {
  const [artfacts, setArtfacts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5015/api/artfacts')
      .then(response => {
        if (!response.ok) {
          throw new Error(`Server responded with status: ${response.status} - ${response.statusText}`);
        }
        return response.json();
      })
      .then(data => {
        setArtfacts(data);
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
      <h1>Artfacts</h1>
      
      <div className="cards__items">
        {artfacts.map(artfact => (
          <div key={artfact.id} className="cards__item">
            <div className="cards__item__link">
              <div className="cards__item__pic-wrap" data-category={artfact.name}>
                <img src={artfact.image} alt={artfact.name} className="cards__item__img" />
              </div>
              <div className="cards__item__info">
                <p>Year: {artfact.year}</p>
                <p>Description: {artfact.description}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ArtfactsList;
