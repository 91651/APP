
import { UserClient, AuthClient } from '@/api-client/client';

function loadConfig() {
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open('GET', '/app-config.json', false);
    xmlHttp.send(null);

    let config: any = null;
    if (xmlHttp && xmlHttp.status === 200) {
        const response = xmlHttp.responseText;
        config = JSON.parse(response);
    }
    return config;
}
const appConfig = loadConfig();
// const apiUrl = process.env.npm_package_devConfig_apiUrl;

const apiUrl = appConfig.apiUrl;

export const _Auth = new AuthClient(apiUrl);
export const _User = new UserClient(apiUrl);
