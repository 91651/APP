import Vue from 'vue';
import Router from 'vue-router';
import store from '@/store';
import Main from './app/main/main.vue';

Vue.use(Router);

const router: Router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/signin',
      name: 'signin',
      component: () => import('./app/sign-in/sign-in.vue'),
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
          path: '/account',
          name: 'account',
          component:  () => import('./app/account/account.vue'),
          meta: {
            keepAlive: true
          }
        }
      ]
    }
  ]
});

router.beforeEach(( to: any, from: any, next: any ) => {
  const isAuth = store.getters.getToken;
  if (isAuth) {
      next();
  } else {
      if (to.path === '/signIn') { // 这就是跳出死循环的关键
          next();
      } else {
          next('/signIn');
      }
  }
})

export default router;
