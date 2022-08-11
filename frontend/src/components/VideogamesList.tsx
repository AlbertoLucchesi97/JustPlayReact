import React from 'react';
import { VideogameData } from '../data/types';
import { Videogame } from './Videogame';
import '../css/style.css';

interface Props {
  data: VideogameData[];
}

export const VideogamesList = ({ data }: Props) => (
  <ul>
    {data.map((videogame) => (
      <li className="videogamesListStyle" key={videogame.id}>
        <Videogame data={videogame} />
      </li>
    ))}
  </ul>
);
