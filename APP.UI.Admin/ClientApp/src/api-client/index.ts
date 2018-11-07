import { AccountClient, AuthClient } from '@/api-client/client';

const apiUrl = process.env.npm_package_devConfig_apiUrl;

export const _Auth = new AuthClient(apiUrl);
export const _Account = new AccountClient(apiUrl);
