import Vue from 'vue';
import Router from 'vue-router';
import store from '@/store';
import { fetchInterceptor } from '../api-client/fetch-interceptor';
import Main from '../views/main/main.vue';

Vue.use(Router);

const router: Router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/signin',
      name: 'signin',
      component: () => import('../views/sign-in/sign-in.vue'),
    },
    {
      path: '/',
      name: 'home',
      component: Main, // 使用require合并文件 require('./app/home/Home.vue')
      meta: {
        keepAlive: true // 需要被缓存
      },
      children: [
        {
          path: '/user',
          name: 'user',
          component: () => import('../views/user/user.vue'),
          meta: {
            keepAlive: true
          }
        }
      ]
    }
  ]
});

router.beforeEach((to: any, from: any, next: any) => {
  const isAuth = store.getters.getUserStatus;
  if (isAuth) {
    next();
  } else {
    if (to.name === 'signin') {
      next();
    } else {
      next('signin');
    }
  }
});

fetchInterceptor.interceptors.push({
  request: (input: string, init: RequestInit) => {
    // 请求注入相关逻辑
    return { input, init };
  }
}, {
    response: (response: Response) => {
      if (response.status === 401) {
        router.replace('signin');
      }
      return response;
    }
  });

export default router;
