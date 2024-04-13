import React from 'react';
import '../App.css';
import './HeroSection.css';

function HeroSection() {
  return (
    <div className='hero-container'>
      <video src='/videos/1085985-hd_1920_1080_25fps.mp4' autoPlay loop muted />
      <h1>Artistry Unleashed</h1>
      <p>A work of art which did not begin in emotion is not art.</p>
    </div>
  );
}

export default HeroSection;
