import React from 'react';
import { Page } from './Page';
import { useForm } from 'react-hook-form';
import { postVideogame } from '../data/VideogamesData';
import { useSelector, useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { FormData, AppState } from '../data/types';
import {
  submittingAction,
  successfullySubmittedAction,
  submittingFailedAction,
} from '../redux/Store';
import '../css/style.css';

export const AddVideogamePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const submitting = useSelector(
    (state: AppState) => state.videogames.submitting,
  );

  const {
    register,
    formState: { errors },
    handleSubmit,
    formState,
  } = useForm<FormData>({
    mode: 'onBlur',
  });

  const submitForm = async (data: FormData) => {
    dispatch(submittingAction());

    const result = await postVideogame({
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
      navigate('/');
    } else {
      dispatch(submittingFailedAction());
    }
  };

  return (
    <Page title="Add Videogame">
      <form onSubmit={handleSubmit(submitForm)}>
        <fieldset
          className="fieldset"
          disabled={formState.isSubmitting || formState.isSubmitted}
        >
          <div className="fieldContainer">
            <label htmlFor="title" className="fieldLabel">
              Title
            </label>
            <div>
              <input
                id="title"
                className="fieldInput"
                type="text"
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
          <div className="fieldContainer">
            <label htmlFor="year" className="fieldLabel">
              Year
            </label>
            <div>
              <input
                id="year"
                className="fieldInput"
                type="number"
                {...register('year', {
                  required: true,
                })}
              />
              {errors.year && errors.year.type === 'required' && (
                <span className="fieldError">You must enter the year</span>
              )}
            </div>
          </div>

          <div className="fieldContainer">
            <label htmlFor="genre" className="fieldLabel">
              Genre
            </label>
            <div>
              <input
                id="genre"
                className="fieldInput"
                type="text"
                {...register('genre', {
                  required: true,
                })}
              />
              {errors.genre && errors.genre.type === 'required' && (
                <span className="fieldError">You must enter the genre</span>
              )}
            </div>
          </div>

          <div className="fieldContainer">
            <label htmlFor="softwareHouse" className="fieldLabel">
              Software House
            </label>
            <div>
              <input
                id="softwareHouse"
                className="fieldInput"
                type="text"
                {...register('softwareHouse', { required: true, minLength: 1 })}
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
          <div className="fieldContainer">
            <label htmlFor="publisher" className="fieldLabel">
              Publisher
            </label>
            <div>
              <input
                id="publisher"
                className="fieldInput"
                type="text"
                {...register('publisher', { required: true, minLength: 1 })}
              />
              {errors.publisher && errors.publisher.type === 'required' && (
                <span className="fieldError">You must enter the publisher</span>
              )}
              {errors.publisher && errors.publisher.type === 'minLength' && (
                <span className="fieldError">
                  The publisher must be at least 1 character
                </span>
              )}
            </div>
          </div>
          <div className="fieldContainer">
            <label htmlFor="synopsis" className="fieldLabel">
              Synopsis
            </label>
            <div>
              <textarea
                id="synopsis"
                className="fieldTextArea"
                {...register('synopsis', { required: true, minLength: 20 })}
              />
              {errors.synopsis && errors.synopsis.type === 'required' && (
                <span className="fieldError">You must enter the synopsis</span>
              )}
              {errors.synopsis && errors.synopsis.type === 'minLength' && (
                <span className="fieldError">
                  The synopsis must be at least 20 characters
                </span>
              )}
            </div>
          </div>
          <div className="fieldContainer">
            <label htmlFor="cover" className="fieldLabel">
              Cover
            </label>
            <div>
              <input
                id="cover"
                className="fieldInput"
                type="text"
                {...register('cover', { required: true })}
              />
              {/* <input
                id="cover"
                className="fieldInput"
                type="file"
                accept=".jpg, .jpeg, .png"
                {...register('cover', { required: true })}
              /> */}
              {errors.cover && errors.cover.type === 'required' && (
                <span className="fieldError">You must enter the cover</span>
              )}
            </div>
          </div>
          <div className="fieldContainer">
            <label htmlFor="trailer" className="fieldLabel">
              Trailer
            </label>
            <div>
              <input
                id="trailer"
                className="fieldInput"
                type="text"
                {...register('trailer')}
              />
            </div>
          </div>
          <div className="fieldButton">
            <button className="invisibleButtonEdit" type="submit">
              <span className="material-icons" title="Save">
                save
              </span>
            </button>
            {submitting && <span>Submitting...</span>}
          </div>
        </fieldset>
      </form>
    </Page>
  );
};
export default AddVideogamePage;
