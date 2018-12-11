import '@babel/polyfill';
import 'whatwg-fetch';

import Vue from 'vue';
import router from './router';
import store from './store/index';

import 'iview/dist/styles/iview.css';
import './global.less';
import iView from 'iview';
import vueMoment from 'vue-moment';
import moment from 'moment';

import App from './app/app.vue';

Vue.use(iView);

moment.locale('zh-cn');

Vue.use(vueMoment, {
  moment
});


new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');


