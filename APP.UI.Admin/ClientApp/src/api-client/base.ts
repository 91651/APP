/* tslint:disable */
export class BaseClient {
    protected transformOptions(options: RequestInit) {
        const token = window.localStorage.getItem('Token');
        let headers = new Headers(options.headers);
        headers.append("Authorization", "Bearer " + token); 
        options.headers = headers;
        return Promise.resolve(options);
    }
}

export class Result<T>{
    data?: T
    total?: number
}