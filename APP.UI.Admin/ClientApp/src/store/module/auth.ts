import { _Auth } from '@/api-client';

export const auth = {
  state: {
    token: ''
  },
  actions: {
    signIn(vuex: any, data: any): Promise<void> {
      return new Promise((resolve, reject) => {
        _Auth.signIn(data.name, data.pwd).then((d) => {
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
