import { ArticleClient, AuthClient, FileClient, UserClient } from '@/api-client/client';

function loadConfig() {
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open('GET', process.env.BASE_URL + '/app-config.json', false);
    xmlHttp.send(null);

    let config: any = null;
    if (xmlHttp && xmlHttp.status === 200) {
        const response = xmlHttp.responseText;
        config = JSON.parse(response);
    }
    return config;
}
export const appConfig = loadConfig();
// const apiUrl = process.env.npm_package_devConfig_apiUrl;

export const _apiUrl = appConfig.apiUrl;

export const _Auth = new AuthClient(_apiUrl);
export const _Article = new ArticleClient(_apiUrl);
export const _File = new FileClient(_apiUrl);
export const _User = new UserClient(_apiUrl);
