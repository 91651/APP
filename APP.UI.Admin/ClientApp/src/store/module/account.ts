import { _Account } from '@/api-client';

export const account = {
  state: {
    token: ''
  },
  actions: {
    getAccount(vuex: any): Promise<void> {
      return new Promise((resolve, reject) => {
        _Account.get().then((d) => {
          resolve();
        });
      });
  }
},
  getters : {
    }
}
