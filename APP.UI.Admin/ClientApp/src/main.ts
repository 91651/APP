import Vue from 'vue';
import router from './router';
import store from './store/index';

import 'iview/dist/styles/iview.css';
import iView from 'iview';
import moment from 'vue-moment';


import App from './app/app.vue';

Vue.use(iView);
Vue.use(moment);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');


