import { AccountClient } from '../api-client/client';

let account = new AccountClient(process.env.npm_package_devConfig_apiUrl);

export const auth = {
  state: {
    token: ''
  },
  actions: {
    signIn() {
      account.signIn().then((data)=>{
        auth.state.token = 'ddd';
        window.localStorage.setItem('Token', data);
      });
    }
  },
  getters : {
    getToken: ( _state: any ) => {
    if (!_state.token) {
    return window.localStorage.getItem('Token');
    } else {
    return _state.token;
    }
}}}
