import Vue from 'vue';
import Router from 'vue-router';
import Main from './app/main/main.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Main, // 使用require合并文件 require('./app/home/Home.vue')
    },
  ],
});
