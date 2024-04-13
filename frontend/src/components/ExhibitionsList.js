import React, { useState, useEffect } from 'react';
import './Cards.css';

function ExhibitionsList() {
    const [exhibitions, setExhibitions] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const imageUrls = [
        'https://plus.unsplash.com/premium_photo-1682088718214-63d88903430e?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fGFydCUyMGV4aGliaXRpb258ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1547296017-978c31e1c124?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fGFydCUyMGV4aGliaXRpb258ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1507643179773-3e975d7ac515?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGFydCUyMGV4aGliaXRpb258ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1603629242133-adaaa856147c?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fGFydCUyMGV4aGliaXRpb258ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1545987796-b199d6abb1b4?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fGFydCUyMGV4aGliaXRpb258ZW58MHx8MHx8fDA%3D',
        'https://images.unsplash.com/photo-1582555172866-f73bb12a2ab3?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8YXJ0JTIwZXhoaWJpdGlvbnxlbnwwfHwwfHx8MA%3D%3D',
        'https://images.unsplash.com/photo-1600903781679-7ea3cbc564c3?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8YXJ0JTIwZXhoaWJpdGlvbnxlbnwwfHwwfHx8MA%3D%3D',
        'https://images.unsplash.com/photo-1503293050619-6048ffad0dc5?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fGFydCUyMGV4aGliaXRpb258ZW58MHx8MHx8fDA%3D',
    ];

    useEffect(() => {
        fetch('http://localhost:5015/api/exhibitions')
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

                setExhibitions(exhibitionsWithImages);
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
            <video src='/videos/5827625-uhd_3840_2160_24fps.mp4' autoPlay loop muted className="video-background" />
            <h1>Exhibitions</h1>

            <div className="cards__items">
                {exhibitions.map(exhibition => (
                    <div key={exhibition.id} className="cards__item">
                        <div className="cards__item__link">
                            <div className="cards__item__pic-wrap" data-category={exhibition.name}>
                                <img src={exhibition.imageUrl} alt={exhibition.name} className="cards__item__img" />
                            </div>
                            <div className="cards__item__info">
                                <p>Place: {exhibition.place}</p>
                                <p>Date: {new Date(exhibition.date).toLocaleDateString()}</p>
                                <p>Description: {exhibition.description}</p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default ExhibitionsList;
