import Vue from 'vue';
import Router from 'vue-router';
import Home from './app/home/Home.vue';
import About from './app/About.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home, // 使用require合并文件 require('./app/home/Home.vue')
    },
    {
      path: '/about',
      name: 'about',
      component: About,
    },
  ],
});
