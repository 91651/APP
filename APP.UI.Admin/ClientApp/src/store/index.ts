import Vue from 'vue';
import Vuex from 'vuex';
import {auth} from './module/auth';
import {account} from './module/account';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {

  },
  mutations: {

  },
  actions: {

  },
  modules: {
    auth,
    account
  }
});
