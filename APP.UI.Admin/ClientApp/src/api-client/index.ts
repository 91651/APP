import { ArticleClient, AuthClient, FileClient } from '@/api-client/client';

export class AppConfig {
    public apiUrl?: string;
}

function loadConfig() {
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open('GET', '/app-config.json', false);
    xmlHttp.send(null);
    if (xmlHttp && xmlHttp.status === 200) {
        const response = xmlHttp.responseText;
        appConfig = JSON.parse(response) as AppConfig;
    }
}
export let appConfig = new AppConfig();
loadConfig();

export const _Auth = new AuthClient(appConfig.apiUrl);
export const _Article = new ArticleClient(appConfig.apiUrl);
export const _File = new FileClient(appConfig.apiUrl);
