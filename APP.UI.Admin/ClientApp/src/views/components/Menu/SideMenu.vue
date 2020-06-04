    
<template>
  <a-layout-sider breakpoint="lg" collapsedWidth="80">
    <a-menu
      :defaultSelectedKeys="['1']"
      :defaultOpenKeys="['sub1']"
      mode="inline"
      theme="dark"
      :inlineCollapsed="true"
    >
    <template v-for="menu in menus">
      <template v-if="!menu.children || menu.children.length <= 0">
        <a-menu-item :key="`menu-${menu.name}`">
        <a-icon :type="`${menu.icon}`"/>
        <span>{{ menu.title }}</span>
        </a-menu-item>
        </template>
      <template v-else>
        <a-sub-menu :key="`menu-${menu.name}`">
        <span slot="title">
          <a-icon :type="`${menu.icon}`"/>
          <span>{{ menu.title }}</span>
        </span>
        <a-menu-item v-for="item in menu.children" :key="`menu-${item.name}`">{{ item.title }}</a-menu-item>
      </a-sub-menu>
        </template>
      </template>
    </a-menu>
  </a-layout-sider>
</template>

<script lang="ts" src="./SideMenu.ts"></script>
