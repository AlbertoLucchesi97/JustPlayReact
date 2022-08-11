import React from 'react';
import { Page } from './Page';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { getVideogame, deleteVideogame } from '../data/VideogamesData';
import {
  addVideogameToWishlist,
  removeVideogameToWishlist,
  addVideogameToOwned,
  removeVideogameToOwned,
} from '../data/UserData';
import { VideogamesList } from './VideogamesList';
import { useSelector, useDispatch } from 'react-redux';
import { useAuth } from './Auth';
import { AppState, VideogameData } from '../data/types';
import {
  gettingVideogameAction,
  gotVideogameAction,
  deletingVideogameAction,
  deletedVideogameAction,
  gettingSimilarGamesAction,
  gotSimilarGamesAction,
  addingVideogameToWishlistAction,
  addedVideogameToWishlistAction,
  removingVideogameFromWishlistAction,
  removedVideogameFromWishlistAction,
  addingVideogameToOwnedAction,
  addedVideogameToOwnedAction,
  removingVideogameFromOwnedAction,
  removedVideogameFromOwnedAction,
} from '../redux/Store';

export const VideogamePage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { isAuthenticated } = useAuth();
  const videogame = useSelector((state: AppState) => state.videogames.viewing);
  const videogames = useSelector(
    (state: AppState) => state.videogames.videogames,
  );
  const similarGames = useSelector(
    (state: AppState) => state.videogames.similarGames,
  );
  const user = useSelector((state: AppState) => state.user.user);
  const videogamesWishlist = useSelector(
    (state: AppState) => state.user.videogamesWishlist,
  );
  const videogamesOwned = useSelector(
    (state: AppState) => state.user.videogamesOwned,
  );
  const { id } = useParams();

  const isInWishlist = (videogame: VideogameData): boolean => {
    return videogamesWishlist.some((v) => v.id === videogame.id);
  };

  const isInOwned = (videogame: VideogameData): boolean => {
    return videogamesOwned.some((v) => v.id === videogame.id);
  };

  React.useEffect(() => {
    const doGetVideogame = async (id: number) => {
      dispatch(gettingVideogameAction());
      const foundVideogame = await getVideogame(id);
      dispatch(gotVideogameAction(foundVideogame));
      if (foundVideogame) {
        dispatch(gettingSimilarGamesAction());
        const similarVideogames = videogames.filter(
          (videogame) => videogame.genre === foundVideogame.genre,
        );
        dispatch(
          gotSimilarGamesAction(
            similarVideogames.filter(({ id }) => id !== foundVideogame.id),
          ),
        );
      }
    };
    if (id) {
      doGetVideogame(Number(id));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id, videogamesWishlist, videogamesOwned]);

  const submitForm = async () => {
    if (id) {
      dispatch(deletingVideogameAction());
      const result = await deleteVideogame(Number(id));
      if (result) {
        dispatch(deletedVideogameAction(Number(id)));
        navigate('/');
      } else {
        return;
      }
    }
  };

  const onRemoveVideogameToWishlist = async () => {
    dispatch(removingVideogameFromWishlistAction());
    const result = await removeVideogameToWishlist(Number(id));
    if (result) {
      dispatch(removedVideogameFromWishlistAction(Number(id)));
    } else {
      return;
    }
  };

  const onAddVideogameToWishlist = async () => {
    dispatch(addingVideogameToWishlistAction());
    const result = await addVideogameToWishlist(Number(id));
    if (result && videogame) {
      dispatch(addedVideogameToWishlistAction(videogame));
    } else {
      return;
    }
  };

  const onRemoveVideogameToOwned = async () => {
    dispatch(removingVideogameFromOwnedAction());
    const result = await removeVideogameToOwned(Number(id));
    if (result) {
      dispatch(removedVideogameFromOwnedAction(Number(id)));
    } else {
      return;
    }
  };

  const onAddVideogameToOwned = async () => {
    dispatch(addingVideogameToOwnedAction());
    const result = await addVideogameToOwned(Number(id));
    if (result && videogame) {
      dispatch(addedVideogameToOwnedAction(videogame));
    } else {
      return;
    }
  };

  return (
    <Page>
      {videogame !== null && (
        <React.Fragment>
          <div className="divCoverStyle">
            <img
              className="detailsCoverStyle"
              src={videogame.cover}
              alt="cover"
            />
          </div>
          <div className="divDetailsStyle">
            <div>
              <div className="labelDetailsStyle">Title</div>
              <span className="spanDetailsStyle">{videogame.title}</span>
            </div>
            <div>
              <div className="labelDetailsStyle">Year</div>

              <span className="spanDetailsStyle">{videogame.year}</span>
            </div>
            <div>
              <div className="labelDetailsStyle">Genre</div>
              <span className="spanDetailsStyle">{videogame.genre}</span>
            </div>
            <div>
              <div className="labelDetailsStyle">Software House</div>
              <span className="spanDetailsStyle">
                {videogame.softwareHouse}
              </span>
            </div>
            <div>
              <div className="labelDetailsStyle">Publisher</div>
              <span className="spanDetailsStyle">{videogame.publisher}</span>
            </div>
            <div className="labelDetailsStyle">Synopsis</div>
            <div>
              <textarea
                disabled={true}
                className="textareaDetailsStyle"
                value={videogame.synopsis}
              />
            </div>
          </div>
          {videogame.trailer && (
            <div className="trailerDiv">
              <iframe
                className="trailerStyle"
                title="trailerFrame"
                src={videogame.trailer}
              ></iframe>
            </div>
          )}
          <div className="detailsPageButtons">
            {isAuthenticated && (
              <div className="authButtons">
                {isInWishlist(videogame) ? (
                  <button
                    className="invisibleButtonDetails"
                    onClick={onRemoveVideogameToWishlist}
                  >
                    <span
                      className="material-icons"
                      title="Remove from wishlist"
                    >
                      remove_shopping_cart
                    </span>
                  </button>
                ) : (
                  <button
                    className="invisibleButtonDetails"
                    onClick={onAddVideogameToWishlist}
                  >
                    <span className="material-icons" title="Add in Wishlist">
                      add_shopping_cart
                    </span>
                  </button>
                )}
                {isInOwned(videogame) ? (
                  <button
                    className="invisibleButtonDetails"
                    onClick={onRemoveVideogameToOwned}
                  >
                    <span className="material-icons" title="Remove from owned">
                      cancel
                    </span>
                  </button>
                ) : (
                  <button
                    className="invisibleButtonDetails"
                    onClick={onAddVideogameToOwned}
                  >
                    <span className="material-icons" title="Add to owned">
                      check_circle
                    </span>
                  </button>
                )}
              </div>
            )}
            {user?.admin && (
              <div className="adminButtons">
                <Link to={`/edit/${id}`} className="linkStyle">
                  <span className="material-icons" title="Edit">
                    edit
                  </span>
                </Link>
                <Link to="" className="linkStyle" onClick={submitForm}>
                  <span className="material-icons" title="Delete">
                    delete
                  </span>
                </Link>
              </div>
            )}
          </div>
          <div className="similarGamesDiv">
            <span className="filterLabel">You could also like:</span>
            <VideogamesList data={similarGames} />
          </div>
        </React.Fragment>
      )}
    </Page>
  );
};
