import React from 'react';
import { Provider } from 'react-redux';
import { setupStore } from './redux/Store';
import './css/App.css';
import './css/style.css';
import { Header } from './components/Header';
import { HomePage } from './components/HomePage';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { SearchPage } from './components/SearchPage';
import { SignInPage } from './components/SignInPage';
import { SignOutPage } from './components/SignOutPage';
import { NotFoundPage } from './components/NotFoundPage';
import { VideogamePage } from './components/VideogamePage';
import { AuthorizedPage } from './components/AuthorizedPage';
import { AuthProvider } from './components/Auth';
const EditVideogamePage = React.lazy(
  () => import('./components/EditVideogamePage'),
);
const AddVideogamePage = React.lazy(
  () => import('./components/AddVideogamePage'),
);
const ProfilePage = React.lazy(() => import('./components/ProfilePage'));

const store = setupStore();

function App() {
  return (
    <Provider store={store}>
      <AuthProvider>
        <BrowserRouter>
          <div className="container">
            <Header />
            <Routes>
              <Route path="" element={<HomePage />} />
              <Route path="videogames/search" element={<SearchPage />} />
              <Route
                path="videogames/add"
                element={
                  <React.Suspense
                    fallback={<div className="fallbackStyle">Loading...</div>}
                  >
                    <AuthorizedPage>
                      <AddVideogamePage />
                    </AuthorizedPage>
                  </React.Suspense>
                }
              />
              <Route
                path="profile"
                element={
                  <React.Suspense
                    fallback={<div className="fallbackStyle">Loading...</div>}
                  >
                    <AuthorizedPage>
                      <ProfilePage />
                    </AuthorizedPage>
                  </React.Suspense>
                }
              />
              <Route path="signin" element={<SignInPage action="signin" />} />
              <Route
                path="/signin-callback"
                element={<SignInPage action="signin-callback" />}
              />
              <Route
                path="signout"
                element={<SignOutPage action="signout" />}
              />
              <Route
                path="/signout-callback"
                element={<SignOutPage action="signout-callback" />}
              />
              <Route path="videogames/:id" element={<VideogamePage />} />
              <Route
                path="edit/:id"
                element={
                  <React.Suspense
                    fallback={<div className="fallbackStyle">Loading...</div>}
                  >
                    <AuthorizedPage>
                      <EditVideogamePage />
                    </AuthorizedPage>
                  </React.Suspense>
                }
              />
              <Route path="*" element={<NotFoundPage />} />
            </Routes>
          </div>
        </BrowserRouter>
      </AuthProvider>
    </Provider>
  );
}

export default App;
