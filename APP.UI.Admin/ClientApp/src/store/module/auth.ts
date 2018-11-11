import { _Auth } from '@/api-client';

export const auth = {
  state: {
    token: ''
  },
  actions: {
    signIn(vuex: any, data: any): Promise<any> {
      return new Promise((resolve, reject) => {
        _Auth.signIn(data.name, data.pwd).then((d) => {
          if (d.token) {
            auth.state.token = d.token;
            window.localStorage.setItem('Token', d.token);
          }
          resolve(d);
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
