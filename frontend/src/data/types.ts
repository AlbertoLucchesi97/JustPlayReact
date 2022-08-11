export interface VideogameData {
  id: number;
  title: string;
  year: number;
  genre: string;
  softwareHouse: string;
  publisher: string;
  synopsis: string;
  cover: string;
  trailer: string;
}

export interface PostVideogameData {
  title: string;
  year: number;
  genre: string;
  softwareHouse: string;
  publisher: string;
  synopsis: string;
  cover: string;
  trailer: string;
}

export interface UserData {
  id: number;
  email: string;
  auth?: string;
  admin: boolean;
  videogamesOwned: VideogameData[];
  videogamesWishlist: VideogameData[];
}

export interface Props {
  title?: string;
  children: React.ReactNode;
}

export interface SignInProps {
  action: SigninAction;
}

export interface SignOutProps {
  action: SignoutAction;
}

export type SignoutAction = 'signout' | 'signout-callback';

export type SigninAction = 'signin' | 'signin-callback';

export type FormData = {
  title: string;
  year: number;
  genre: string;
  softwareHouse: string;
  publisher: string;
  synopsis: string;
  cover: string;
  trailer: string;
};

export type SearchFormData = {
  search: string;
};

//#region State

export interface AppState {
  readonly videogames: VideogamesState;
  readonly user: UserState;
  readonly auth: AuthState;
}

export interface VideogamesState {
  readonly loading: boolean;
  readonly videogames: VideogameData[];
  readonly viewing: VideogameData | null;
  readonly searched: VideogameData[];
  readonly added: VideogameData | null;
  readonly submitting: boolean;
  readonly submitted: boolean;
  readonly deleted: boolean;
  readonly sort: string;
  readonly similarGames: VideogameData[];
}

export interface UserState {
  readonly loading: boolean;
  readonly user: UserData | null;
  readonly videogamesOwned: VideogameData[];
  readonly videogamesWishlist: VideogameData[];
}

//#endregion

//#region Auth
export interface Auth0User {
  name?: string;
  email?: string;
}

export interface IAuth0Context {
  isAuthenticated: boolean;
  user?: Auth0User;
  signIn: () => void;
  signOut: () => void;
  loading: boolean;
}

export interface AuthState {
  readonly user: Auth0User | undefined;
  readonly loading: boolean;
  readonly isAuthenticated: boolean;
}
//#endregion
