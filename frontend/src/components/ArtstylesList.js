import React, { useState, useEffect } from 'react';
import './Cards.css';

function ArtStylesList() {
  const [artStyles, setArtStyles] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5015/api/artstyles')
      .then(response => {
        if (!response.ok) {
          throw new Error(`Server responded with status: ${response.status} - ${response.statusText}`);
        }
        return response.json();
      })
      .then(data => {
        setArtStyles(data);
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
      <video src='/videos/istockphoto-1368075329-640_adpp_is.mp4' autoPlay loop muted className="video-background" />
      <h1>Art Styles</h1>
      
      <div className="cards__items">
        {artStyles.map(artStyle => (
          <div key={artStyle.id} className="cards__item">
            <div className="cards__item__link">
              <div className="cards__item__pic-wrap" data-category={artStyle.name}>
                <img src={artStyle.image} alt={artStyle.name} className="cards__item__img" />
              </div>
              <div className="cards__item__info">
                <p>Name: {artStyle.name}</p>
                <p>Description: {artStyle.description}</p>
                <p>Creator: {artStyle.creator}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ArtStylesList;

