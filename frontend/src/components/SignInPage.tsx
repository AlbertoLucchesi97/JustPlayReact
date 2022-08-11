import React from 'react';
import { Page } from './Page';
import '../css/style.css';
import { useAuth } from './Auth';
import { SignInProps } from '../data/types';

export const SignInPage = ({ action }: SignInProps) => {
  const { signIn } = useAuth();
  if (action === 'signin') {
    signIn();
  }
  return (
    <Page title="Sign In">
      <h2>Signing in...</h2>
    </Page>
  );
};
