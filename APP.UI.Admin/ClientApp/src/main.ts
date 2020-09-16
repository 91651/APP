import { createApp } from 'vue'
import antd from 'ant-design-vue';
import 'ant-design-vue/components/style.js';
import App from './App.vue'
import router from './router'
import store from './store'

import './css/ant-design-pro.less';

createApp(App).use(antd).use(store).use(router).mount('#app')
