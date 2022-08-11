import React from 'react';
import { Page } from './Page';
import { useAuth } from './Auth';
import { VideogamesList } from './VideogamesList';
import '../css/style.css';
import { AppState } from '../data/types';
import { useSelector } from 'react-redux';

export const ProfilePage = () => {
  const { user } = useAuth();
  const videogamesOwned = useSelector(
    (state: AppState) => state.user.videogamesOwned,
  );
  const videogamesWishlist = useSelector(
    (state: AppState) => state.user.videogamesWishlist,
  );

  return (
    <Page>
      <div>
        <h2 className="privatePageTitle">
          Welcome to your private page, {user?.name}
        </h2>
      </div>
      <div className="similarGamesDiv">
        <span className="filterLabel">You have those videogames:</span>
        <VideogamesList data={videogamesOwned} />
      </div>
      <div className="similarGamesDiv">
        <span className="filterLabel">Your wishlist:</span>
        <VideogamesList data={videogamesWishlist} />
      </div>
    </Page>
  );
};

export default ProfilePage;
