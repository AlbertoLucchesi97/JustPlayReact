import { VideogameData, PostVideogameData } from './types';
import { http } from './http';
import { getAccessToken } from '../components/Auth';

export const getVideogame = async (
  id: number,
): Promise<VideogameData | null> => {
  const result = await http<VideogameData>({
    path: `/videogames/${id}`,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return null;
  }
};

export const getVideogames = async (): Promise<VideogameData[]> => {
  const result = await http<VideogameData[]>({
    path: '/videogames',
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return [];
  }
};

export const getVideogamesSorted = async (
  criteria: string,
): Promise<VideogameData[]> => {
  const result = await http<VideogameData[]>({
    path: `/videogames?sort=${criteria}`,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return [];
  }
};

export const searchVideogames = async (
  criteria: string,
): Promise<VideogameData[]> => {
  const result = await http<VideogameData[]>({
    path: `/videogames?search=${criteria}`,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return [];
  }
};

export const postVideogame = async (
  videogame: PostVideogameData,
): Promise<VideogameData | undefined> => {
  const accessToken = await getAccessToken();
  const result = await http<VideogameData, PostVideogameData>({
    path: '/videogames',
    method: 'POST',
    body: videogame,
    accessToken,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return undefined;
  }
};

export const putVideogame = async (
  id: number,
  vgUpdated: PostVideogameData,
): Promise<VideogameData | undefined> => {
  const accessToken = await getAccessToken();
  const result = await http<VideogameData, PostVideogameData>({
    path: `/videogames/${id}`,
    method: 'PUT',
    body: vgUpdated,
    accessToken,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return undefined;
  }
};

export const deleteVideogame = async (id: number): Promise<boolean> => {
  const accessToken = await getAccessToken();
  const result = await http<void>({
    path: `/videogames/${id}`,
    method: 'DELETE',
    accessToken,
  });

  if (result.ok) {
    return true;
  } else {
    return false;
  }
};
