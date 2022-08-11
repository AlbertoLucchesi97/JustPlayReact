import React, { useState } from 'react';
import { VideogameData } from '../data/types';
import { Link } from 'react-router-dom';
import '../css/style.css';

interface Props {
  data: VideogameData;
}

export const Videogame = ({ data }: Props) => {
  const [isHovering, setIsHovering] = useState(false);

  const handleMouseOver = () => setIsHovering(true);
  const handleMouseOut = () => setIsHovering(false);

  const hoverVideogameText = () => {
    return <div className="hoverVideogameStyle">{data.title}</div>;
  };

  return (
    <Link className="videogameLinkStyle" to={`/videogames/${data.id}`}>
      <div onMouseOver={handleMouseOver} onMouseOut={handleMouseOut}>
        <img className="coverStyle" src={data.cover} alt={data.title} />
        {isHovering && hoverVideogameText()}
      </div>
    </Link>
  );
};
