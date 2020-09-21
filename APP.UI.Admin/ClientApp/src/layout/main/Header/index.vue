<template>
  <ALayoutHeader class="layout-header">
    <div class="left-options">
      <span @click="() => $emit('update:collapsed', !collapsed)" class="menu-fold">
            <menu-unfold-outlined v-if="collapsed"/>
            <menu-fold-outlined v-else/>
    </span>
      <a-breadcrumb>
        <a-breadcrumb-item v-for="routeItem in route.matched" :key="routeItem.name">
          <a>{{ routeItem.meta.title }}</a>
          <template v-slot:overlay>
            <a-menu v-if="routeItem.children.length">
              <template v-for="childItem in routeItem.children">
                <a-menu-item v-if="!childItem.meta.hidden" :key="childItem.path">
                  <router-link :to="routeItem.path == '/' ? childItem.path : (routeItem.path + '/' + childItem.path)">
                    {{ childItem.meta.title }}
                  </router-link>
                </a-menu-item>
              </template>
            </a-menu>
          </template>
        </a-breadcrumb-item>
      </a-breadcrumb>
    </div>
    <div class="right-options">
      <SettingOutlined/>
      <SearchOutlined/>
      <Dropdown>
        <a-avatar>{{ username }}</a-avatar>
        <template v-slot:overlay>
          <a-menu>
            <a-menu-item>
              <a href="javascript:;">个人中心</a>
            </a-menu-item>
            <a-menu-divider/>
            <a-menu-item>
              <a @click.prevent="doLogout">退出登录</a>
            </a-menu-item>
          </a-menu>
        </template>
      </Dropdown>
    </div>
  </ALayoutHeader>
</template>

<script lang="ts">
import {defineComponent, ref} from 'vue'
import {useRouter, useRoute} from 'vue-router'
import components from "@/layout/header/components";
import {logout} from "@/api/sys/user";

export default defineComponent({
  name: "PageHeader",
  components: {...components},
  props: {
    collapsed: {
      type: Boolean,
    }
  },
  setup() {
    const username = ref(localStorage.getItem('username') || '')

    const router = useRouter()
    const route = useRoute()
    console.log(route.matched)
    console.log(router.getRoutes(), 'currentRoute')

    const doLogout = () => {
      console.log(router, '退出登录')
      logout({})
      router.replace({
        name: 'login',
        query: {
          redirect: route.fullPath
        }
      })
    }

    return {
      doLogout,
      username,
      route
    }
  }
})
</script>

<style lang="scss" scoped>
.layout-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: sticky;
  top: 0;
  z-index: 10;
  background-color: #fff;
  padding: 0;
  height: $header-height;

  .left-options {
    display: flex;
    align-items: center;

    .menu-fold {
      padding: 0 24px;
      cursor: pointer;
    }
  }

  .right-options {
    display: flex;
    align-items: center;

    > * {
      margin-right: 20px;
      cursor: pointer;
    }
  }
}
</style>
