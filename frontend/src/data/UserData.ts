import { UserData, VideogameData } from './types';
import { http } from './http';
import { getAccessToken } from '../components/Auth';

export const getUser = async (): Promise<UserData | null> => {
  const accessToken = await getAccessToken();
  const result = await http<UserData>({
    path: `/users/`,
    accessToken,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return null;
  }
};

export const getVideogamesOwned = async (): Promise<VideogameData[]> => {
  const accessToken = await getAccessToken();
  const result = await http<VideogameData[]>({
    path: `/users/videogamesOwned`,
    accessToken,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return [];
  }
};

export const getVideogamesWishlist = async (): Promise<VideogameData[]> => {
  const accessToken = await getAccessToken();
  const result = await http<VideogameData[]>({
    path: `/users/videogamesWishlist`,
    accessToken,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return [];
  }
};

export const addVideogameToOwned = async (
  videogameId: number,
): Promise<boolean> => {
  const accessToken = await getAccessToken();
  const result = await http<number>({
    path: `/users/videogamesOwned/add/${videogameId}`,
    method: 'POST',
    accessToken,
  });
  if (result.ok && result.body) {
    return true;
  } else {
    return false;
  }
};

export const removeVideogameToOwned = async (id: number): Promise<boolean> => {
  const accessToken = await getAccessToken();
  const result = await http<number>({
    path: `/users/videogamesOwned/remove/${id}`,
    method: 'DELETE',
    accessToken,
  });
  if (result.ok && result.body) {
    return true;
  } else {
    return false;
  }
};

export const addVideogameToWishlist = async (
  videogameId: number,
): Promise<boolean> => {
  const accessToken = await getAccessToken();
  const result = await http<number>({
    path: `/users/videogamesWishlist/add/${videogameId}`,
    method: 'POST',
    accessToken,
  });
  if (result.ok && result.body) {
    return true;
  } else {
    return false;
  }
};

export const removeVideogameToWishlist = async (
  id: number,
): Promise<boolean> => {
  const accessToken = await getAccessToken();
  const result = await http<number>({
    path: `/users/videogamesWishlist/remove/${id}`,
    method: 'DELETE',
    accessToken,
  });
  if (result.ok && result.body) {
    return true;
  } else {
    return false;
  }
};
