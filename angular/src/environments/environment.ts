import { Config } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'TigerAdmin',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44306',
    redirectUri: baseUrl,
    clientId: 'TigerAdmin_App',
    responseType: 'code',
    scope: 'offline_access TigerAdmin',
  },
  apis: {
    default: {
      url: 'https://localhost:44306',
      rootNamespace: 'TigerAdmin',
    },
  },
} as Config.Environment;
