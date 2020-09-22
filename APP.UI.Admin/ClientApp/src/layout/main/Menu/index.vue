<template>
    <div class="logo">
      <img src="../../assets/logo.svg" alt="">
      <h1 v-show="!collapsed">黑匣子控制中心</h1>
    </div>
    <a-menu @click="clickMenuItem" theme="dark" mode="inline" :inlineCollapsed="false" v-model:selectedKeys="selectedKeys" v-model:openKeys="openKeys"  @openChange="onOpenChange">
      <template v-for="group in routes">
        <a-sub-menu v-if="!group.meta.hidden" :key="group.path">
          <template v-slot:title>
            <component :is="group.meta?.icon"/>
            <span>{{ group.meta.title }}</span>
          </template>
          <template v-for="menu in group.children">
            <a-sub-menu v-if="menu.meta.isGroup" :key="group.path + '/' + menu.path">
              <template v-slot:title>
                <component :is="menu.meta?.icon"/>
                <span class="nav-text">{{ menu.meta.title }}</span>
              </template>
              <template v-for="item in menu.children">
                <a-menu-item v-if="!item.meta.hidden" :key="group.path + '/' + menu.path + '/' + item.path">
                  <span class="nav-text">{{ item.meta.title }}</span>
                </a-menu-item>
              </template>
            </a-sub-menu>
            <a-menu-item v-if="!menu.meta.hidden && !menu.meta.isGroup" :key="group.path + '/' + menu.path">
              <component :is="menu.meta?.icon"/>
              <span class="nav-text">{{ menu.meta.title }}</span>
            </a-menu-item>
          </template>
        </a-sub-menu>
      </template>
    </a-menu>
</template>

<script lang="ts">
import {defineComponent, ref, unref, watch, SetupContext } from 'vue'
import antIcons from './ant-icons'
import {message} from 'ant-design-vue'

import router from "@/router/";

import {useRoute} from "vue-router";

interface Props {
  collapsed: boolean;
}

export default defineComponent({
  name: 'menu',
  props: {
    collapsed: {
      type: Boolean,
    }
  },
  components: {
    ...antIcons
  },
  setup(props: Props, context: SetupContext ) {
    const route = useRoute()
    const getOpenKeys = () => {
      return route.meta.isGroup ? route.matched.slice(1, 3).map(item => item.path) : [route.matched[1].path]
    }

    const routes = router.getRoutes().find(item => item.path === '/')?.children || []
    const openKeys = ref(getOpenKeys())
    const preOpenKeys = ref(unref(openKeys))
    const selectedKeys = ref([route.path])

    const clickMenuItem = (menuItem: any) => {
      router.push(menuItem.key)
    }
    const onOpenChange = (openKey: Array<string>) => {
      // openKeys.value = getOpenKeys()
    }

    watch(() => props.collapsed, (newVal) => {
      console.log(newVal, preOpenKeys)
      openKeys.value = newVal ? [] : unref(preOpenKeys)
    })

    watch(() => route.fullPath, () => {
      if (route.name == 'login') return
      openKeys.value = getOpenKeys()
      selectedKeys.value = [route.path]
    })

    watch(() => openKeys.value, (newVal, oldVal) => {
      preOpenKeys.value = oldVal
    })

    return {
      routes,
      openKeys,
      selectedKeys,
      clickMenuItem,
      onOpenChange
    }
  },
});

</script>

<style lang="scss" scoped>

</style>
