// 浏览器使用fetch
import 'whatwg-fetch';

import Vue from 'vue';
import router from './router';
import store from './store/index';
import Antd from 'ant-design-vue';
import 'ant-design-vue/dist/antd.css';
import './css/ant-design-pro.less';

Vue.config.productionTip = false;
import App from './app/app.vue';

Vue.use(Antd);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');


