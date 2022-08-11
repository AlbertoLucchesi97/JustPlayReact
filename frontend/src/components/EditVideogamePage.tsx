import React from 'react';
import { Page } from './Page';
import { useForm } from 'react-hook-form';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router';
import { putVideogame, getVideogame } from '../data/VideogamesData';
import { useSelector, useDispatch } from 'react-redux';
import { AppState, FormData } from '../data/types';
import {
  gettingVideogameAction,
  gotVideogameAction,
  submittingAction,
  successfullySubmittedAction,
  submittingFailedAction,
} from '../redux/Store';
import '../css/style.css';

export const EditVideogamePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const videogameToUpdate = useSelector(
    (state: AppState) => state.videogames.viewing,
  );
  const { id } = useParams();

  React.useEffect(() => {
    const doGetVideogame = async (id: number) => {
      dispatch(gettingVideogameAction());
      const foundVideogame = await getVideogame(id);
      dispatch(gotVideogameAction(foundVideogame));
    };
    if (id) {
      doGetVideogame(Number(id));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const {
    register,
    formState: { errors },
    handleSubmit,
  } = useForm<FormData>({
    mode: 'onBlur',
  });

  const submitForm = async (data: FormData) => {
    dispatch(submittingAction());
    const result = await putVideogame(Number(id), {
      title: data.title,
      year: data.year,
      genre: data.genre,
      softwareHouse: data.softwareHouse,
      publisher: data.publisher,
      synopsis: data.synopsis,
      cover: data.cover,
      trailer: data.trailer,
    });
    if (result) {
      dispatch(successfullySubmittedAction(result));
      navigate(`/videogames/${id}`);
    } else {
      dispatch(submittingFailedAction());
    }
  };

  return (
    videogameToUpdate && (
      <Page title="Edit Videogame">
        <form onSubmit={handleSubmit(submitForm)}>
          <fieldset className="fieldset">
            <div className="fieldContainer">
              <div>
                <label htmlFor="title" className="fieldLabel">
                  Title
                </label>
                <div>
                  <input
                    id="title"
                    className="fieldInput"
                    type="text"
                    defaultValue={videogameToUpdate.title}
                    {...register('title', { required: true, minLength: 1 })}
                  />
                  {errors.title && errors.title.type === 'required' && (
                    <span className="fieldError">You must enter the title</span>
                  )}
                  {errors.title && errors.title.type === 'minLength' && (
                    <span className="fieldError">
                      The title must be at least 1 character
                    </span>
                  )}
                </div>
              </div>
              <div>
                <label htmlFor="year" className="fieldLabel">
                  Year
                </label>
                <div>
                  <input
                    id="year"
                    className="fieldInput"
                    type="number"
                    defaultValue={videogameToUpdate.year}
                    {...register('year', {
                      required: true,
                    })}
                  />
                  {errors.year && errors.year.type === 'required' && (
                    <span className="fieldError">You must enter the year</span>
                  )}
                </div>
              </div>

              <div>
                <label htmlFor="genre" className="fieldLabel">
                  Genre
                </label>
                <div>
                  <input
                    id="genre"
                    className="fieldInput"
                    type="text"
                    defaultValue={videogameToUpdate.genre}
                    {...register('genre', {
                      required: true,
                      minLength: 1,
                    })}
                  />
                  {errors.genre && errors.genre.type === 'required' && (
                    <span className="fieldError">You must enter the genre</span>
                  )}
                  {errors.genre && errors.genre.type === 'minLength' && (
                    <span className="fieldError">
                      The genre must be at least 1 character
                    </span>
                  )}
                </div>
              </div>

              <div>
                <label htmlFor="softwareHouse" className="fieldLabel">
                  Software House
                </label>
                <div>
                  <input
                    id="softwareHouse"
                    className="fieldInput"
                    type="text"
                    defaultValue={videogameToUpdate.softwareHouse}
                    {...register('softwareHouse', {
                      required: true,
                      minLength: 1,
                    })}
                  />
                  {errors.softwareHouse &&
                    errors.softwareHouse.type === 'required' && (
                      <span className="fieldError">
                        You must enter the software house
                      </span>
                    )}
                  {errors.softwareHouse &&
                    errors.softwareHouse.type === 'minLength' && (
                      <span className="fieldError">
                        The software house must be at least 1 character
                      </span>
                    )}
                </div>
              </div>
              <div>
                <label htmlFor="publisher" className="fieldLabel">
                  Publisher
                </label>
                <div>
                  <input
                    id="publisher"
                    className="fieldInput"
                    type="text"
                    defaultValue={videogameToUpdate.publisher}
                    {...register('publisher', { required: true, minLength: 1 })}
                  />
                  {errors.publisher && errors.publisher.type === 'required' && (
                    <span className="fieldError">
                      You must enter the publisher
                    </span>
                  )}
                  {errors.publisher &&
                    errors.publisher.type === 'minLength' && (
                      <span className="fieldError">
                        The publisher must be at least 1 character
                      </span>
                    )}
                </div>
              </div>
              <div>
                <label htmlFor="synopsis" className="fieldLabel">
                  Synopsis
                </label>
                <div>
                  <textarea
                    id="synopsis"
                    className="fieldTextArea"
                    defaultValue={videogameToUpdate.synopsis}
                    {...register('synopsis', { required: true, minLength: 20 })}
                  />
                  {errors.synopsis && errors.synopsis.type === 'required' && (
                    <span className="fieldError">
                      You must enter the synopsis
                    </span>
                  )}
                  {errors.synopsis && errors.synopsis.type === 'minLength' && (
                    <span className="fieldError">
                      The synopsis must be at least 20 characters
                    </span>
                  )}
                </div>
              </div>
              <div>
                <label htmlFor="cover" className="fieldLabel">
                  Cover
                </label>
                <div>
                  <input
                    id="cover"
                    className="fieldInput"
                    type="text"
                    defaultValue={videogameToUpdate.cover}
                    {...register('cover', { required: true })}
                  />
                  {errors.cover && errors.cover.type === 'required' && (
                    <span className="fieldError">You must enter the cover</span>
                  )}
                </div>
              </div>
              <div>
                <label htmlFor="trailer" className="fieldLabel">
                  Trailer
                </label>
                <div>
                  <input
                    id="trailer"
                    className="fieldInput"
                    type="text"
                    defaultValue={videogameToUpdate.trailer}
                    {...register('trailer')}
                  />
                </div>
              </div>
            </div>
            <div className="fieldButton">
              <button className="invisibleButtonEdit" type="submit">
                <span className="material-icons" title="Save">
                  save
                </span>
              </button>
            </div>
          </fieldset>
        </form>
      </Page>
    )
  );
};
export default EditVideogamePage;
