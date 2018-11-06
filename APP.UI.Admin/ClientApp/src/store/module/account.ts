import { AccountClient } from '@/api-client/client';

let _account = new AccountClient(process.env.npm_package_devConfig_apiUrl);

export const account = {
  state: {
    token: ''
  },
  actions: {
    getAccount(vuex: any): Promise<void> {
      debugger;
      return new Promise((resolve, reject) => {
        _account.get().then((d) => {
          resolve();
        });
      });
  }
},
  getters : {
    }
}
