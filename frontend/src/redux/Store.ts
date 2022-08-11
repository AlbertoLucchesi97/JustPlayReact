import { UserData, VideogameData, Auth0User } from '../data/types';
import { Store, combineReducers } from 'redux';
import { configureStore } from '@reduxjs/toolkit';
import { VideogamesState, UserState } from '../data/types';
import { AuthState, AppState } from '../data/types';

//#region AuthState
const initialAuthState: AuthState = {
  user: undefined,
  loading: false,
  isAuthenticated: false,
};

export const CreatingAuthState = 'CreatingAuthState';
export const creatingAuthStateAction = () =>
  ({
    type: CreatingAuthState,
  } as const);

export const CreatedAuthState = 'CreatedAuthState';
export const createdAuthStateAction = () =>
  ({
    type: CreatedAuthState,
  } as const);

export const GettingIsAuthenticated = 'GettingIsAuthenticated';
export const gettingIsAuthenticatedAction = () =>
  ({
    type: GettingIsAuthenticated,
  } as const);

export const GotIsAuthenticated = 'GotIsAuthenticated';
export const gotIsAuthenticatedAction = (isAuthenticated: boolean) =>
  ({
    type: GotIsAuthenticated,
    isAuthenticated,
  } as const);

export const GettingAuthUser = 'GettingAuthUser';
export const gettingAuthUserAction = () =>
  ({
    type: GettingAuthUser,
  } as const);

export const GotAuthUser = 'GotAuthUser';
export const gotAuthUserAction = (user: Auth0User | undefined) =>
  ({
    type: GotAuthUser,
    user,
  } as const);

type AuthAction =
  | ReturnType<typeof creatingAuthStateAction>
  | ReturnType<typeof createdAuthStateAction>
  | ReturnType<typeof gettingIsAuthenticatedAction>
  | ReturnType<typeof gotIsAuthenticatedAction>
  | ReturnType<typeof gettingAuthUserAction>
  | ReturnType<typeof gotAuthUserAction>;

const authReducer = (state = initialAuthState, action: AuthAction) => {
  switch (action.type) {
    case CreatingAuthState:
      return {
        ...state,
        loading: true,
      };
    case CreatedAuthState:
      return {
        ...state,
        loading: false,
      };
    case GettingIsAuthenticated:
      return {
        ...state,
        loading: true,
      };
    case GotIsAuthenticated:
      return {
        ...state,
        loading: false,
        isAuthenticated: action.isAuthenticated,
      };
    case GettingAuthUser:
      return {
        ...state,
        loading: true,
      };
    case GotAuthUser:
      return {
        ...state,
        loading: false,
        user: action.user,
      };
  }
  return state;
};

//#endregion State

//#region  VideogameState

const initialVideogameState: VideogamesState = {
  loading: false,
  videogames: [],
  viewing: null,
  searched: [],
  added: null,
  submitting: false,
  submitted: false,
  deleted: false,
  sort: '',
  similarGames: [],
};

export const GettingVideogames = 'GettingVideogames';
export const gettingVideogamesAction = () =>
  ({
    type: GettingVideogames,
  } as const);

export const GotVideogames = 'GotVideogames';
export const gotVideogamesAction = (videogames: VideogameData[]) =>
  ({
    type: GotVideogames,
    videogames: videogames,
  } as const);

export const GettingVideogame = 'GettingVideogame';
export const gettingVideogameAction = () =>
  ({
    type: GettingVideogame,
  } as const);

export const GotVideogame = 'GotVideogame';
export const gotVideogameAction = (videogame: VideogameData | null) =>
  ({
    type: GotVideogame,
    videogame: videogame,
  } as const);

export const SearchingVideogames = 'SearchingVideogames';
export const searchingVideogamesAction = () =>
  ({
    type: SearchingVideogames,
  } as const);

export const SearchedVideoGames = 'SearchedVideoGames';
export const searchedVideogamesAction = (videogames: VideogameData[]) =>
  ({
    type: SearchedVideoGames,
    videogames,
  } as const);

export const AddingVideogame = 'AddingVideogame';
export const addingVideogameAction = () =>
  ({
    type: AddingVideogame,
  } as const);

export const AddedVideogame = 'AddedVideogame';
export const addedVideogameAction = (videogame: VideogameData) =>
  ({
    type: AddedVideogame,
    videogame,
  } as const);

export const Submitting = 'Submitting';
export const submittingAction = () =>
  ({
    type: Submitting,
  } as const);

export const SuccessfullySubmitted = 'SuccessfullySubmitted';
export const successfullySubmittedAction = (videogame: VideogameData | null) =>
  ({
    type: SuccessfullySubmitted,
  } as const);

export const SubmittingFailed = 'SubmittingFailed';
export const submittingFailedAction = () =>
  ({
    type: SubmittingFailed,
  } as const);

export const DeletingVideogame = 'DeletingVideogame';
export const deletingVideogameAction = () =>
  ({
    type: DeletingVideogame,
  } as const);

export const DeletedVideogame = 'DeletedVideogame';
export const deletedVideogameAction = (id: number) =>
  ({
    type: DeletedVideogame,
    id,
  } as const);

export const ChangingSort = 'ChangingSort';
export const changingSortAction = (sort: string) =>
  ({
    type: ChangingSort,
    sort,
  } as const);

export const GettingSimilarGames = 'GettingSimilarGames';
export const gettingSimilarGamesAction = () =>
  ({
    type: GettingSimilarGames,
  } as const);

export const GotSimilarGames = 'GotSimilarGames';
export const gotSimilarGamesAction = (games: VideogameData[]) =>
  ({
    type: GotSimilarGames,
    games,
  } as const);

type VideogamesAction =
  | ReturnType<typeof gettingVideogamesAction>
  | ReturnType<typeof gotVideogamesAction>
  | ReturnType<typeof gettingVideogameAction>
  | ReturnType<typeof gotVideogameAction>
  | ReturnType<typeof searchingVideogamesAction>
  | ReturnType<typeof searchedVideogamesAction>
  | ReturnType<typeof addingVideogameAction>
  | ReturnType<typeof addedVideogameAction>
  | ReturnType<typeof submittingAction>
  | ReturnType<typeof successfullySubmittedAction>
  | ReturnType<typeof submittingFailedAction>
  | ReturnType<typeof deletingVideogameAction>
  | ReturnType<typeof deletedVideogameAction>
  | ReturnType<typeof changingSortAction>
  | ReturnType<typeof gettingSimilarGamesAction>
  | ReturnType<typeof gotSimilarGamesAction>;

const videogamesReducer = (
  state = initialVideogameState,
  action: VideogamesAction,
) => {
  switch (action.type) {
    case GettingVideogames:
      return {
        ...state,
        videogames: [],
        loading: true,
      };
    case GotVideogames:
      return {
        ...state,
        loading: false,
        videogames: action.videogames,
      };
    case GettingVideogame:
      return {
        ...state,
        viewing: null,
        loading: true,
      };
    case GotVideogame:
      return {
        ...state,
        loading: false,
        viewing: action.videogame,
      };
    case SearchingVideogames:
      return {
        ...state,
        searched: [],
        loading: true,
      };
    case SearchedVideoGames:
      return {
        ...state,
        loading: false,
        searched: action.videogames,
      };
    case AddingVideogame:
      return {
        ...state,
        added: null,
        loading: true,
      };
    case AddedVideogame:
      return {
        ...state,
        loading: false,
        videogames: [...state.videogames, action.videogame],
      };
    case Submitting:
      return {
        ...state,
        submitting: true,
      };
    case SuccessfullySubmitted:
      return {
        ...state,
        submitting: false,
        submitted: true,
      };
    case SubmittingFailed:
      return {
        ...state,
        submitting: false,
        submitted: false,
      };
    case DeletingVideogame:
      return {
        ...state,
        loading: true,
      };
    case DeletedVideogame:
      return {
        ...state,
        loading: false,
        deleted: true,
        videogames: state.videogames.filter((vg) => vg.id !== action.id),
      };
    case ChangingSort:
      return {
        ...state,
        sort: action.sort,
      };
    case GettingSimilarGames:
      return {
        ...state,
        loading: true,
      };
    case GotSimilarGames:
      return {
        ...state,
        loading: false,
        similarGames: action.games,
      };
  }
  return state;
};

//#endregion

//#region  UserState
const initialUserState: UserState = {
  loading: false,
  user: null,
  videogamesOwned: [],
  videogamesWishlist: [],
};

export const GettingUser = 'GettingUser';
export const gettingUserAction = () =>
  ({
    type: GettingUser,
  } as const);

export const GotUser = 'GotUser';
export const gotUserAction = (user: UserData | null) =>
  ({
    type: GotUser,
    user,
  } as const);

export const GettingVideogamesOwned = 'GettingVideogamesOwned';
export const gettingVideogamesOwnedAction = () =>
  ({
    type: GettingVideogamesOwned,
  } as const);

export const GotVideogamesOwned = 'GotVideogamesOwned';
export const gotVideogamesOwnedAction = (videogames: VideogameData[]) =>
  ({
    type: GotVideogamesOwned,
    videogames,
  } as const);

export const GettingVideogamesWishlist = 'GettingVideogamesWishlist';
export const gettingVideogamesWishlistAction = () =>
  ({
    type: GettingVideogamesWishlist,
  } as const);

export const GotVideogamesWishlist = 'GotVideogamesWishlist';
export const gotVideogamesWishlistAction = (videogames: VideogameData[]) =>
  ({
    type: GotVideogamesWishlist,
    videogames,
  } as const);

export const AddingVideogameToWishlist = 'AddingVideogameToWishlist';
export const addingVideogameToWishlistAction = () =>
  ({
    type: AddingVideogameToWishlist,
  } as const);

export const AddedVideogameToWishlist = 'AddedVideogameToWishlist';
export const addedVideogameToWishlistAction = (videogame: VideogameData) =>
  ({
    type: AddedVideogameToWishlist,
    videogame,
  } as const);

export const RemovingVideogameFromWishlist = 'RemovingVideogameFromWishlist';
export const removingVideogameFromWishlistAction = () =>
  ({
    type: RemovingVideogameFromWishlist,
  } as const);

export const RemovedVideogameFromWishlist = 'RemovedVideogameFromWishlist';
export const removedVideogameFromWishlistAction = (id: number) =>
  ({
    type: RemovedVideogameFromWishlist,
    id,
  } as const);

export const AddingVideogameToOwned = 'AddingVideogameToOwned';
export const addingVideogameToOwnedAction = () =>
  ({
    type: AddingVideogameToOwned,
  } as const);

export const AddedVideogameToOwned = 'AddedVideogameToOwned';
export const addedVideogameToOwnedAction = (videogame: VideogameData) =>
  ({
    type: AddedVideogameToOwned,
    videogame,
  } as const);

export const RemovingVideogameFromOwned = 'RemovingVideogameFromOwned';
export const removingVideogameFromOwnedAction = () =>
  ({
    type: RemovingVideogameFromOwned,
  } as const);

export const RemovedVideogameFromOwned = 'RemovedVideogameFromOwned';
export const removedVideogameFromOwnedAction = (id: number) =>
  ({
    type: RemovedVideogameFromOwned,
    id,
  } as const);

type UserAction =
  | ReturnType<typeof gettingUserAction>
  | ReturnType<typeof gotUserAction>
  | ReturnType<typeof gettingVideogamesOwnedAction>
  | ReturnType<typeof gotVideogamesOwnedAction>
  | ReturnType<typeof gettingVideogamesWishlistAction>
  | ReturnType<typeof gotVideogamesWishlistAction>
  | ReturnType<typeof addingVideogameToWishlistAction>
  | ReturnType<typeof addedVideogameToWishlistAction>
  | ReturnType<typeof removingVideogameFromWishlistAction>
  | ReturnType<typeof removedVideogameFromWishlistAction>
  | ReturnType<typeof addingVideogameToOwnedAction>
  | ReturnType<typeof addedVideogameToOwnedAction>
  | ReturnType<typeof removingVideogameFromOwnedAction>
  | ReturnType<typeof removedVideogameFromOwnedAction>;

const userReducer = (state = initialUserState, action: UserAction) => {
  switch (action.type) {
    case GettingUser:
      return {
        ...state,
        loading: true,
      };
    case GotUser:
      return {
        ...state,
        loading: false,
        user: action.user,
      };
    case GettingVideogamesOwned:
      return {
        ...state,
        loading: true,
      };
    case GotVideogamesOwned:
      return {
        ...state,
        loading: false,
        videogamesOwned: action.videogames,
      };
    case GettingVideogamesWishlist:
      return {
        ...state,
        loading: true,
      };
    case GotVideogamesWishlist:
      return {
        ...state,
        loading: false,
        videogamesWishlist: action.videogames,
      };
    case AddingVideogameToWishlist:
      return {
        ...state,
        loading: true,
      };
    case AddedVideogameToWishlist:
      return {
        ...state,
        loading: false,
        videogamesWishlist: [...state.videogamesWishlist, action.videogame],
      };
    case RemovingVideogameFromWishlist:
      return {
        ...state,
        loading: true,
      };
    case RemovedVideogameFromWishlist:
      return {
        ...state,
        loading: false,
        videogamesWishlist: state.videogamesWishlist.filter(
          (videogame) => videogame.id !== action.id,
        ),
      };
    case AddingVideogameToOwned:
      return {
        ...state,
        loading: true,
      };
    case AddedVideogameToOwned:
      return {
        ...state,
        loading: false,
        videogamesOwned: [...state.videogamesOwned, action.videogame],
      };
    case RemovingVideogameFromOwned:
      return {
        ...state,
        loading: true,
      };
    case RemovedVideogameFromOwned:
      return {
        ...state,
        loading: false,
        videogamesOwned: state.videogamesOwned.filter(
          (videogame) => videogame.id !== action.id,
        ),
      };
  }
  return state;
};
//#endregion

const rootReducer = combineReducers<AppState>({
  videogames: videogamesReducer,
  user: userReducer,
  auth: authReducer,
});

export function setupStore(): Store<AppState> {
  const store = configureStore({
    reducer: rootReducer,
  });
  return store;
}
