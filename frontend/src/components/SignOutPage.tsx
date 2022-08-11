import React from 'react';
import { Page } from './Page';
import '../css/style.css';
import { useAuth } from './Auth';
import { SignOutProps } from '../data/types';

export const SignOutPage = ({ action }: SignOutProps) => {
  let message = 'Signing out...';

  const { signOut } = useAuth();

  switch (action) {
    case 'signout':
      signOut();
      break;
    case 'signout-callback':
      message = 'You successfully signed out!';
      break;
  }
  return (
    <Page title="Sign Out">
      <h2>{message}</h2>
    </Page>
  );
};
