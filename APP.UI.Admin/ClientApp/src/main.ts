import Vue from 'vue';
import router from './router';

import 'iview/dist/styles/iview.css';
import iView from 'iview';

import App from './app/app.vue';

Vue.use(iView);

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');


