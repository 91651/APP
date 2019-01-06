
let interceptors: any[] = [];
window.fetch = ((fetch) => {
    return (input?: string | Request | undefined, init?: RequestInit | undefined) => {
        debugger;
        return interceptor(fetch, {input, init});
    };
})(window.fetch);

function interceptor(fetch: (input?: string | Request | undefined, init?: RequestInit | undefined) => Promise<Response>,
                     args: {input?: string | Request | undefined, init?: RequestInit | undefined}) {
    interceptors = []; 
    interceptors.push({response: (_response: Response) => { 
        debugger;
        return _response;
    }});
    interceptors.push({request: (input: string, init: RequestInit) => { 
        let headers = new Headers(init.headers);
        headers.append("qqq", "bbb"); 
        init.headers = headers;
        return {input, init};
    }});
    const reversedInterceptors = interceptors.reduce((array, interceptor) => [...[interceptor], array]);
    let promise = Promise.resolve(args);
    reversedInterceptors.forEach(({ request, requestError }: any) => {
        if (request || requestError) {
          promise = promise.then(args => request(args.input, args.init), requestError);
        }
      });
    let responseP = promise.then(args => fetch(args.input, args.init) );
    reversedInterceptors.forEach(({ response, responseError }: any) => {
        if (response || responseError) {
            responseP = responseP.then((_response: Response) => {
            return response(_response);
        });
        }
      });
    
    return responseP;
}
