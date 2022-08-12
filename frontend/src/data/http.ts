import { webAPIUrl } from './AppSettings';
import settings from '../config/settings.json';

export interface HttpRequest<RequestBody> {
  path: string;
  method?: string;
  body?: RequestBody;
  accessToken?: string;
}

export interface HttpResponse<ResponseBody> {
  ok: boolean;
  body?: ResponseBody;
}

export const http = async <ResponseBody, RequestBody = undefined>(
  config: HttpRequest<RequestBody>,
): Promise<HttpResponse<ResponseBody>> => {
  // load a json file

  // for Docker use
  const url = settings.apiUrl;

  //for local use
  //const url = webAPIUrl;

  const request = new Request(`${url}${config.path}`, {
    method: config.method || 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
    body: config.body ? JSON.stringify(config.body) : undefined,
  });
  if (config.accessToken) {
    request.headers.set('authorization', `bearer ${config.accessToken}`);
  }
  const response = await fetch(request);

  if (response.ok && response.status === 204) {
    return { ok: response.ok };
  }

  if (response.ok) {
    const body = await response.json();
    return { ok: response.ok, body };
  } else {
    logError(request, response);
    return { ok: response.ok };
  }
};

export const logError = async (request: Request, response: Response) => {
  const contentType = response.headers.get('Content-Type');
  let body: any;
  if (contentType && contentType.indexOf('application/json') !== -1) {
    body = await response.json();
  } else {
    body = await response.text();
  }

  console.error(`Error requesting ${request.method} ${request.url}`, body);
};
