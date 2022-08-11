import React from 'react';
import '../css/style.css';
import { Link, useSearchParams, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { useAuth } from './Auth';
import { SearchFormData } from '../data/types';

export const Header = () => {
  const navigate = useNavigate();
  const { register, handleSubmit } = useForm<SearchFormData>();
  const [searchParams] = useSearchParams();
  const criteria = searchParams.get('criteria') || '';
  const { isAuthenticated, loading } = useAuth();

  const submitForm = ({ search }: SearchFormData) => {
    navigate(`videogames/search?criteria=${search}`);
  };

  return (
    <div className="headerStyle">
      <Link to="/" className="linkStyle">
        <span className="titleStyle">JUSTPLAY</span>
      </Link>
      <form onSubmit={handleSubmit(submitForm)}>
        <input
          {...register('search')}
          className="searchInputStyle"
          type="text"
          placeholder="&#xF002; Search"
          defaultValue={criteria}
        />
      </form>
      {!loading &&
        (isAuthenticated ? (
          <div>
            <Link to="/profile" className="linkStyle">
              <span className="material-icons" title="Profile">
                account_circle
              </span>
            </Link>
            <Link to="signout" className="linkStyle">
              <span title="Logout" className="material-icons">
                logout
              </span>
            </Link>
          </div>
        ) : (
          <Link to="signin" className="linkStyle">
            <span className="material-icons" title="Login">
              login
            </span>
          </Link>
        ))}
    </div>
  );
};
