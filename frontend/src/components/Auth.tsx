import React from 'react';
import createAuth0Client, { Auth0Client } from '@auth0/auth0-spa-js';
import { authSettings } from '../data/AppSettings';
import { IAuth0Context } from '../data/types';
import { AppState, Props } from '../data/types';
import { useSelector, useDispatch } from 'react-redux';
import {
  creatingAuthStateAction,
  createdAuthStateAction,
  gettingIsAuthenticatedAction,
  gotIsAuthenticatedAction,
  gettingAuthUserAction,
  gotAuthUserAction,
} from '../redux/Store';

export const Auth0Context = React.createContext<IAuth0Context>({
  isAuthenticated: false,
  signIn: () => {},
  signOut: () => {},
  loading: true,
});

export const useAuth = () => React.useContext(Auth0Context);

export const getAccessToken = async () => {
  const auth0FromHook = await createAuth0Client(authSettings);
  const accessToken = await auth0FromHook.getTokenSilently();
  return accessToken;
};

export const AuthProvider: React.FC<Props> = ({ children }) => {
  const dispatch = useDispatch();
  const [auth0Client, setAuth0Client] = React.useState<Auth0Client>();
  const isAuthenticated = useSelector(
    (state: AppState) => state.auth.isAuthenticated,
  );
  const user = useSelector((state: AppState) => state.auth.user);
  const loading = useSelector((state: AppState) => state.auth.loading);

  const getAuth0ClientFromState = () => {
    if (auth0Client === undefined) {
      throw new Error('Auth0 client not set');
    }
    return auth0Client;
  };

  React.useEffect(() => {
    const initAuth0 = async () => {
      dispatch(creatingAuthStateAction());
      const auth0FromHook = await createAuth0Client(authSettings);
      setAuth0Client(auth0FromHook);
      dispatch(createdAuthStateAction());

      if (
        window.location.pathname === '/signin-callback' &&
        window.location.search.indexOf('code=') > -1
      ) {
        await auth0FromHook.handleRedirectCallback();
        window.location.replace(window.location.origin);
      }

      dispatch(gettingIsAuthenticatedAction());
      const isAuthenticatedFromHook = await auth0FromHook.isAuthenticated();
      if (isAuthenticatedFromHook) {
        dispatch(gettingAuthUserAction());
        const user = await auth0FromHook.getUser();
        dispatch(gotAuthUserAction(user));
      }
      dispatch(gotIsAuthenticatedAction(isAuthenticatedFromHook));
    };
    initAuth0();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Auth0Context.Provider
      value={{
        isAuthenticated,
        user,
        signIn: () => getAuth0ClientFromState().loginWithRedirect(),
        signOut: () =>
          getAuth0ClientFromState().logout({
            client_id: authSettings.client_id,
            returnTo: window.location.origin + '/signout-callback',
          }),
        loading,
      }}
    >
      {children}
    </Auth0Context.Provider>
  );
};
export default AuthProvider;
