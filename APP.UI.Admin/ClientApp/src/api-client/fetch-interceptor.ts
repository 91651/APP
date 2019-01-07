export class FetchInterceptor {
    public interceptors: any[] = [];
    public interceptor(fetch: (input: RequestInfo, init?: RequestInit | undefined) => Promise<Response>,
        options: { input: RequestInfo, init?: RequestInit | undefined }) {
        const reversedInterceptors = this.interceptors.reduce((array, interceptor) => [...[interceptor], array]);
        let promise = Promise.resolve(options);
        reversedInterceptors.forEach(({ request, requestError }: any) => {
            if (request || requestError) {
                promise = promise.then(args => request(args.input, args.init), requestError);
            }
        });
        let responseP = promise.then(args => fetch(args.input, args.init));
        reversedInterceptors.forEach(({ response, responseError }: any) => {
            if (response || responseError) {
                responseP = responseP.then((_response: Response) => {
                    return response(_response);
                });
            }
        });
        return responseP;
    }
}

window.fetch = ((fetch) => {
    return (input: RequestInfo, init?: RequestInit | undefined) => {
        return fetchInterceptor.interceptor(fetch, { input, init });
    };
})(window.fetch);

export const fetchInterceptor = new FetchInterceptor();
