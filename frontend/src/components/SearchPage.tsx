import React from 'react';
import { Page } from './Page';
import { useSearchParams } from 'react-router-dom';
import { VideogamesList } from './VideogamesList';
import { searchVideogames } from '../data/VideogamesData';
import { useSelector, useDispatch } from 'react-redux';
import { AppState } from '../data/types';
import {
  searchingVideogamesAction,
  searchedVideogamesAction,
} from '../redux/Store';
import '../css/style.css';

export const SearchPage = () => {
  const dispatch = useDispatch();
  const [searchParams] = useSearchParams();

  const videogames = useSelector(
    (state: AppState) => state.videogames.searched,
  );

  const search = searchParams.get('criteria') || '';

  React.useEffect(() => {
    const doSearch = async (criteria: string) => {
      dispatch(searchingVideogamesAction());
      const foundResults = await searchVideogames(criteria);
      dispatch(searchedVideogamesAction(foundResults));
    };
    doSearch(search);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [search]);

  return (
    <Page title="Search Results">
      {search && <p className="searchPStyle">for "{search}"</p>}
      <VideogamesList data={videogames} />
    </Page>
  );
};
