import React from 'react';
import '../css/style.css';
import { Props } from '../data/types';

export const PageTitle = ({ children }: Props) => (
  <h2 className="pageTitleStyle">{children}</h2>
);
