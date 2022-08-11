export const server = 'http://localhost:8080';
export const webAPIUrl = `${server}/api`;

export const authSettings = {
  domain: 'dev-stdir6nx.us.auth0.com',
  client_id: 'Ome3ZDTOBECrWTEClREafL6hW0gyyZa8',
  redirect_uri: window.location.origin + '/signin-callback',
  scope: 'openid profile JustPlayAPI email',
  audience: 'https://justplay',
};
