import { _Auth } from '@/api-client';

export const auth = {
  state: {
    name: '',
    token: '',
    status: 1
  },
  mutations: {
    SET_SIGNIN: (state: any, data: any) => {
      auth.state.name = data.userName;
      auth.state.token = data.token;
      auth.state.status = data.status;
    }
  },
  actions: {
  },
  getters: {
    getUserStatus: (state: any) => {
      if (state.status) {
        return true;
      }
      return false;
    }
  }
}
