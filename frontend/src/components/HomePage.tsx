import React from 'react';
import { VideogamesList } from './VideogamesList';
import { getVideogamesSorted } from '../data/VideogamesData';
import {
  getUser,
  getVideogamesOwned,
  getVideogamesWishlist,
} from '../data/UserData';
import Select from 'react-select';
import { Page } from './Page';
import '../css/style.css';
import { useAuth } from './Auth';
import { Link } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { AppState } from '../data/types';
import {
  gettingVideogamesAction,
  gotVideogamesAction,
  changingSortAction,
  gettingUserAction,
  gotUserAction,
  gettingVideogamesOwnedAction,
  gotVideogamesOwnedAction,
  gettingVideogamesWishlistAction,
  gotVideogamesWishlistAction,
} from '../redux/Store';

export const HomePage = () => {
  const dispatch = useDispatch();
  const videogames = useSelector(
    (state: AppState) => state.videogames.videogames,
  );
  const videogamesLoading = useSelector(
    (state: AppState) => state.videogames.loading,
  );
  const { isAuthenticated } = useAuth();
  const sort = useSelector((state: AppState) => state.videogames.sort);
  const user = useSelector((state: AppState) => state.user.user);

  const changeSort = (sortValue: any) => {
    dispatch(changingSortAction(sortValue.value));
  };

  const options = [
    { value: 'title', label: 'Title' },
    { value: 'year', label: 'Year' },
  ];

  React.useEffect(() => {
    const doGetVideogames = async () => {
      dispatch(gettingVideogamesAction());
      const videogames = await getVideogamesSorted(sort);
      dispatch(gotVideogamesAction(videogames));
    };
    const doGetUser = async () => {
      dispatch(gettingUserAction());
      const user = await getUser();
      dispatch(gotUserAction(user));
    };
    const doGetUserWishlistAndOwned = async () => {
      dispatch(gettingVideogamesOwnedAction());
      const foundVideogamesOwned = await getVideogamesOwned();
      dispatch(gotVideogamesOwnedAction(foundVideogamesOwned));
      dispatch(gettingVideogamesWishlistAction());
      const foundVideogameWishlist = await getVideogamesWishlist();
      dispatch(gotVideogamesWishlistAction(foundVideogameWishlist));
    };
    if (isAuthenticated && !user) {
      doGetUser();
      doGetUserWishlistAndOwned();
    }
    doGetVideogames();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [sort, isAuthenticated]);

  return (
    <Page>
      {videogamesLoading ? (
        <div>Loading...</div>
      ) : (
        <React.Fragment>
          <div className="newVideogame">
            {user?.admin && (
              <div className="homePageDivStyle">
                <Link to="/videogames/add" className="linkStyle">
                  <span className="material-icons" title="Add Videogame">
                    add_circle_outline
                  </span>
                </Link>
              </div>
            )}
          </div>
          <div className="filterDiv">
            <span className="material-icons" title="Sort By">
              list
            </span>

            <Select
              className="filterSelect"
              defaultValue={options[0]}
              options={options}
              onChange={(option) => changeSort(option)}
            />
          </div>
          <VideogamesList data={videogames || []} />
        </React.Fragment>
      )}
    </Page>
  );
};
