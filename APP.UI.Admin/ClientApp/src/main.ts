// 浏览器使用fetch
import 'whatwg-fetch';

import Vue from 'vue';
import router from './router';
import store from './store/index';
import 'iview/dist/styles/iview.css';
import './global.less';
import iView from 'iview';

import App from './app/app.vue';

Vue.use(iView);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');


