import React from 'react';
import { PageTitle } from './PageTitle';
import { Props } from '../data/types';
import '../css/style.css';

export const Page = ({ title, children }: Props) => (
  <div className="pageStyle">
    {title && <PageTitle>{title}</PageTitle>}
    {children}
  </div>
);
