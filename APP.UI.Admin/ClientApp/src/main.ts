import Vue from 'vue';
import router from './router';
import store from './store/index';

import 'iview/dist/styles/iview.css';
import iView from 'iView';

import App from './app/app.vue';

Vue.use(iView);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');


