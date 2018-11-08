import { _User } from '@/api-client';

export const user = {
  state: {
    token: ''
  },
  actions: {
    getUsers(vuex: any): Promise<void> {
      return new Promise((resolve, reject) => {
        _User.get().then((d) => {
          resolve();
        });
      });
    }
  },
  getters: {
  }
}
