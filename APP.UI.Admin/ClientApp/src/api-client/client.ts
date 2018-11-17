﻿/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

export class BaseClient {
    protected transformOptions(options: RequestInit) {
        const token = window.localStorage.getItem('Token');
        let headers = new Headers(options.headers);
        headers.append("Authorization", "Bearer " + token); 
        options.headers = headers;
        return Promise.resolve(options);
    }
}

export class ArticleClient extends BaseClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: (key: string, value: any) => any = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl ? baseUrl : "http://localhost:56833";
    }

    addArticle(model: ArticleModel): Promise<string> {
        let url_ = this.baseUrl + "/api/Article/AddArticle";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ = <RequestInit>{
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json", 
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processAddArticle(_response);
        });
    }

    protected processAddArticle(response: Response): Promise<string> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<string>(<any>null);
    }

    getArticles(model: SearchArticleModel): Promise<ResultModelOfListOfArticleListModel> {
        let url_ = this.baseUrl + "/api/Article/GetArticles";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ = <RequestInit>{
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json", 
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processGetArticles(_response);
        });
    }

    protected processGetArticles(response: Response): Promise<ResultModelOfListOfArticleListModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 ? ResultModelOfListOfArticleListModel.fromJS(resultData200) : <any>null;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResultModelOfListOfArticleListModel>(<any>null);
    }
}

export class AuthClient extends BaseClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: (key: string, value: any) => any = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl ? baseUrl : "http://localhost:56833";
    }

    signIn(name: string, pwd: string): Promise<AuthModel> {
        let url_ = this.baseUrl + "/api/Auth?";
        if (name !== undefined)
            url_ += "name=" + encodeURIComponent("" + name) + "&"; 
        if (pwd !== undefined)
            url_ += "pwd=" + encodeURIComponent("" + pwd) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processSignIn(_response);
        });
    }

    protected processSignIn(response: Response): Promise<AuthModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 ? AuthModel.fromJS(resultData200) : <any>null;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<AuthModel>(<any>null);
    }
}

export class UserClient extends BaseClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: (key: string, value: any) => any = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl ? baseUrl : "http://localhost:56833";
    }

    get(): Promise<UserModel[]> {
        let url_ = this.baseUrl + "/api/User";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processGet(_response);
        });
    }

    protected processGet(response: Response): Promise<UserModel[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (resultData200 && resultData200.constructor === Array) {
                result200 = [];
                for (let item of resultData200)
                    result200.push(UserModel.fromJS(item));
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<UserModel[]>(<any>null);
    }
}

export class ArticleModel implements IArticleModel {
    id?: string;
    title?: string;
    subTitle?: string;
    userId?: string;
    ownerId?: string;
    channelId?: string;
    author?: string;
    context?: string;
    created: Date;
    updated: Date;
    state: number;

    constructor(data?: IArticleModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.title = data["title"];
            this.subTitle = data["subTitle"];
            this.userId = data["userId"];
            this.ownerId = data["ownerId"];
            this.channelId = data["channelId"];
            this.author = data["author"];
            this.context = data["context"];
            this.created = data["created"] ? new Date(data["created"].toString()) : <any>undefined;
            this.updated = data["updated"] ? new Date(data["updated"].toString()) : <any>undefined;
            this.state = data["state"];
        }
    }

    static fromJS(data: any): ArticleModel {
        data = typeof data === 'object' ? data : {};
        let result = new ArticleModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["subTitle"] = this.subTitle;
        data["userId"] = this.userId;
        data["ownerId"] = this.ownerId;
        data["channelId"] = this.channelId;
        data["author"] = this.author;
        data["context"] = this.context;
        data["created"] = this.created ? this.created.toISOString() : <any>undefined;
        data["updated"] = this.updated ? this.updated.toISOString() : <any>undefined;
        data["state"] = this.state;
        return data; 
    }
}

export interface IArticleModel {
    id?: string;
    title?: string;
    subTitle?: string;
    userId?: string;
    ownerId?: string;
    channelId?: string;
    author?: string;
    context?: string;
    created: Date;
    updated: Date;
    state: number;
}

export class ResultModelOfListOfArticleListModel implements IResultModelOfListOfArticleListModel {
    data?: ArticleListModel[];
    total: number;

    constructor(data?: IResultModelOfListOfArticleListModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            if (data["data"] && data["data"].constructor === Array) {
                this.data = [];
                for (let item of data["data"])
                    this.data.push(ArticleListModel.fromJS(item));
            }
            this.total = data["total"];
        }
    }

    static fromJS(data: any): ResultModelOfListOfArticleListModel {
        data = typeof data === 'object' ? data : {};
        let result = new ResultModelOfListOfArticleListModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (this.data && this.data.constructor === Array) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        data["total"] = this.total;
        return data; 
    }
}

export interface IResultModelOfListOfArticleListModel {
    data?: ArticleListModel[];
    total: number;
}

export class ArticleListModel implements IArticleListModel {
    id?: string;
    title?: string;
    userName?: string;
    channelName?: string;
    created: Date;
    updated: Date;
    state: number;

    constructor(data?: IArticleListModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.title = data["title"];
            this.userName = data["userName"];
            this.channelName = data["channelName"];
            this.created = data["created"] ? new Date(data["created"].toString()) : <any>undefined;
            this.updated = data["updated"] ? new Date(data["updated"].toString()) : <any>undefined;
            this.state = data["state"];
        }
    }

    static fromJS(data: any): ArticleListModel {
        data = typeof data === 'object' ? data : {};
        let result = new ArticleListModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["userName"] = this.userName;
        data["channelName"] = this.channelName;
        data["created"] = this.created ? this.created.toISOString() : <any>undefined;
        data["updated"] = this.updated ? this.updated.toISOString() : <any>undefined;
        data["state"] = this.state;
        return data; 
    }
}

export interface IArticleListModel {
    id?: string;
    title?: string;
    userName?: string;
    channelName?: string;
    created: Date;
    updated: Date;
    state: number;
}

export class Query implements IQuery {
    take: number;
    skip: number;
    sort?: Sort[];
    filter?: Filter[];

    constructor(data?: IQuery) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.take = data["take"];
            this.skip = data["skip"];
            if (data["sort"] && data["sort"].constructor === Array) {
                this.sort = [];
                for (let item of data["sort"])
                    this.sort.push(Sort.fromJS(item));
            }
            if (data["filter"] && data["filter"].constructor === Array) {
                this.filter = [];
                for (let item of data["filter"])
                    this.filter.push(Filter.fromJS(item));
            }
        }
    }

    static fromJS(data: any): Query {
        data = typeof data === 'object' ? data : {};
        let result = new Query();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["take"] = this.take;
        data["skip"] = this.skip;
        if (this.sort && this.sort.constructor === Array) {
            data["sort"] = [];
            for (let item of this.sort)
                data["sort"].push(item.toJSON());
        }
        if (this.filter && this.filter.constructor === Array) {
            data["filter"] = [];
            for (let item of this.filter)
                data["filter"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IQuery {
    take: number;
    skip: number;
    sort?: Sort[];
    filter?: Filter[];
}

export class SearchArticleModel extends Query implements ISearchArticleModel {
    id?: string;
    title?: string;
    userName?: string;
    createdDate?: Date;

    constructor(data?: ISearchArticleModel) {
        super(data);
    }

    init(data?: any) {
        super.init(data);
        if (data) {
            this.id = data["id"];
            this.title = data["title"];
            this.userName = data["userName"];
            this.createdDate = data["createdDate"] ? new Date(data["createdDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): SearchArticleModel {
        data = typeof data === 'object' ? data : {};
        let result = new SearchArticleModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["userName"] = this.userName;
        data["createdDate"] = this.createdDate ? this.createdDate.toISOString() : <any>undefined;
        super.toJSON(data);
        return data; 
    }
}

export interface ISearchArticleModel extends IQuery {
    id?: string;
    title?: string;
    userName?: string;
    createdDate?: Date;
}

export class Sort implements ISort {
    field?: string;
    desc: boolean;

    constructor(data?: ISort) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.field = data["field"];
            this.desc = data["desc"];
        }
    }

    static fromJS(data: any): Sort {
        data = typeof data === 'object' ? data : {};
        let result = new Sort();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["field"] = this.field;
        data["desc"] = this.desc;
        return data; 
    }
}

export interface ISort {
    field?: string;
    desc: boolean;
}

export class Filter implements IFilter {
    field?: string;
    value?: any;

    constructor(data?: IFilter) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.field = data["field"];
            this.value = data["value"];
        }
    }

    static fromJS(data: any): Filter {
        data = typeof data === 'object' ? data : {};
        let result = new Filter();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["field"] = this.field;
        data["value"] = this.value;
        return data; 
    }
}

export interface IFilter {
    field?: string;
    value?: any;
}

export class AuthModel implements IAuthModel {
    userName?: string;
    token?: string;
    tokenType?: string;
    status?: string;

    constructor(data?: IAuthModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.userName = data["userName"];
            this.token = data["token"];
            this.tokenType = data["tokenType"];
            this.status = data["status"];
        }
    }

    static fromJS(data: any): AuthModel {
        data = typeof data === 'object' ? data : {};
        let result = new AuthModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userName"] = this.userName;
        data["token"] = this.token;
        data["tokenType"] = this.tokenType;
        data["status"] = this.status;
        return data; 
    }
}

export interface IAuthModel {
    userName?: string;
    token?: string;
    tokenType?: string;
    status?: string;
}

export class UserModel implements IUserModel {
    id?: string;
    userName?: string;

    constructor(data?: IUserModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.userName = data["userName"];
        }
    }

    static fromJS(data: any): UserModel {
        data = typeof data === 'object' ? data : {};
        let result = new UserModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["userName"] = this.userName;
        return data; 
    }
}

export interface IUserModel {
    id?: string;
    userName?: string;
}

export class SwaggerException extends Error {
    message: string;
    status: number; 
    response: string; 
    headers: { [key: string]: any; };
    result: any; 

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if(result !== null && result !== undefined)
        throw result;
    else
        throw new SwaggerException(message, status, response, headers, null);
}

/* tslint:disable */

export class Result<T>{
    data?: T
    total?: number
}