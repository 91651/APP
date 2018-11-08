import Vue from 'vue';
import Vuex from 'vuex';
import { auth } from './module/auth';
import { user } from './module/user';

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
    user
  }
});
