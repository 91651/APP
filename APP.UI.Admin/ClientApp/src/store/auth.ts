import { AccountClient } from '../api-client/client';

let account = new AccountClient(process.env.API_URL);

export const auth = {
  actions: {
    signIn() {
      debugger;
      account.signIn();
    }
  }
};
