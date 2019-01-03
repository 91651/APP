
let interceptors: any[] = [];
window.fetch = ((fetch) => {
    return (url: string, request: RequestInit) => {
        debugger;
        return interceptor(fetch(url, request));
    };
})(window.fetch);

function interceptor(fetch: any) {
    const reversedInterceptors = interceptors.reduce((array, interceptor) => [interceptor].concat(array), []);


    return fetch;
}
