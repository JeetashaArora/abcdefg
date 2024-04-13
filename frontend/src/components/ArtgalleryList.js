import React, { useState, useEffect } from 'react';
import './Cards.css';

function ArtgalleryList() {
    const [artgalleries, setArtgalleries] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const imageUrls = [
        'https://images.unsplash.com/photo-1518998053901-5348d3961a04?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8YXJ0JTIwZ2FsbGVyeXxlbnwwfHwwfHx8MA%3D%3D',
        'https://images.unsplash.com/photo-1564399580075-5dfe19c205f3?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8YXJ0JTIwZ2FsbGVyeXxlbnwwfHwwfHx8MA%3D%3D',
        'https://images.unsplash.com/photo-1514195037031-83d60ed3b448?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTR8fGFydCUyMGdhbGxlcnl8ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1569783721854-33a99b4c0bae?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTB8fGFydCUyMGdhbGxlcnl8ZW58MHx8MHx8fDA%3D',
        'https://plus.unsplash.com/premium_photo-1679690708758-19137dd17d0c?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTd8fGFydCUyMGdhbGxlcnl8ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1591163088186-367c5daff7b0?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzJ8fGFydCUyMGdhbGxlcnl8ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1507643179773-3e975d7ac515?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDN8fGFydCUyMGdhbGxlcnl8ZW58MHx8MHx8fDA%3D',
        'https://plus.unsplash.com/premium_photo-1709031620960-877745c2bc34?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDl8fGFydCUyMGdhbGxlcnl8ZW58MHx8MHx8fDA%3D'
    ];

    useEffect(() => {
        fetch('http://localhost:5015/api/artgallery')
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Server responded with status: ${response.status} - ${response.statusText}`);
                }
                return response.json();
            })
            .then(data => {
                const startIndex = Math.floor(Math.random() * imageUrls.length);
                const exhibitionsWithImages = data.map((exhibition, index) => ({
                    ...exhibition,
                    imageUrl: imageUrls[(startIndex + index) % imageUrls.length]
                }));

                setArtgalleries(exhibitionsWithImages);
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
            <video src='/videos/Art_gallery.mp4' autoPlay loop muted className="video-background" />
            <h1>Art Galleries</h1>

            <div className="cards__items">
                {artgalleries.map(artgallery => (
                    <div key={artgallery.id} className="cards__item">
                        <div className="cards__item__link">
                            <div className="cards__item__pic-wrap" data-category={artgallery.name}>
                                <img src={artgallery.imageUrl} alt={artgallery.name} className="cards__item__img" />
                            </div>
                            <div className="cards__item__info">
                                <p>Place: {artgallery.place}</p>
                                <p>Date: {new Date(artgallery.date).toLocaleDateString()}</p>
                                <p>Description: {artgallery.description}</p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default ArtgalleryList;
