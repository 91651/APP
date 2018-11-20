/* tslint:disable */
import { BaseClient } from './base'

export class FileClient extends BaseClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    constructor(baseUrl: string) {
        super();
        this.http = <any>window;
        this.baseUrl = baseUrl;
    }
    uploadImg(form: FormData): Promise<any> {
        let url_ = this.baseUrl + "/api/File/UploadImg";
        let options_ = <RequestInit>{
            body: form,
            method: "POST",
            headers: {
                "Content-Type": "multipart/form-data",
                "Accept": "application/json"
            }
        };
        this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            debugger;
            return _response;
        });
        return Promise.resolve<any>(<any>null);;
    }
}