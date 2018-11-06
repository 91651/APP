import { AuthClient } from '@/api-client/client';

let _auth = new AuthClient(process.env.npm_package_devConfig_apiUrl);

export const auth = {
  state: {
    token: ''
  },
  actions: {
    signIn(vuex: any, data: any): Promise<void> {
      return new Promise((resolve, reject) => {
        _auth.signIn(data.name, data.pwd).then((d) => {
          auth.state.token = d;
          window.localStorage.setItem('Token', d);
          resolve();
        });
      });
  }
},
  getters : {
    getToken: ( state: any ) => {
    if (!state.token) {
    return window.localStorage.getItem('Token');
    } else {
    return state.token;
    }
}}}
