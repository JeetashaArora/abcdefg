import React from 'react';
import './Cards.css';
import CardItem from './CardItem';

function Cards() {
  return (
    <div className='cards'>
      <h1>Great artifacts and artists: timeless echoes of humanity</h1>
      <div className='cards__container'>
        <div className='cards__wrapper'>
          <ul className='cards__items'>
            <CardItem
              src='/images/img1.webp'
              text='Timeless wonders that echo the whispers of history, meticulously crafted by human hands'
              label='Artifacts'
              path='/artifact-card'
            />
            <CardItem
              src='images/img2.jpg'
              text='The visionary souls who breathe life into canvases, capturing the essence of our world′s beauty and complexity'
              label='Artists'
              path='/artifact-card'
            />
            <CardItem
              src='images/img-3.jpeg'
              text='Art is the expression of the soul′s deepest emotions, shaped by the hands of the artist and the spirit of their time'
              label='Art Styles'
              path='/art-style'
            />
            <CardItem
              src='images/img-4.webp'
              text='Art is not what you see, but what you make others see'
              label='Exhibitions'
              path='/exhibition'
            />
          </ul>
        </div>
      </div>
    </div>
  );
}

export default Cards;
