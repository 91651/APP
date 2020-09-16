import { createApp } from 'vue'
import antd from 'ant-design-vue';
import 'ant-design-vue/components/style.js';
import App from './App.vue'
import router from './router'
import store from './store'

createApp(App).use(antd).use(store).use(router).mount('#app')
