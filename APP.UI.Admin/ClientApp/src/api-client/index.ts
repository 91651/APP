import { ArticleClient, AuthClient, FileClient, UserClient } from '@/api-client/client';

export class AppConfig {
    public apiUrl?: string;
}

function loadConfig() {
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open('GET', process.env.BASE_URL + '/app-config.json', true);
    xmlHttp.send(null);
    if (xmlHttp && xmlHttp.status === 200) {
        const response = xmlHttp.responseText;
        appConfig = JSON.parse(response) as AppConfig;
    }
}
export let appConfig = new AppConfig();

export const _Auth = new AuthClient(appConfig.apiUrl);
export const _Article = new ArticleClient(appConfig.apiUrl);
export const _File = new FileClient(appConfig.apiUrl);
export const _User = new UserClient(appConfig.apiUrl);
