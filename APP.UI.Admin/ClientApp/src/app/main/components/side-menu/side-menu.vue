<template>
    <div class="side-menu-wrapper">
        <slot></slot>
        <Menu ref="menu" v-show="!collapsed" :active-name="activeName" :open-names="openedNames" :accordion="accordion" :theme="theme" width="auto" @on-select="handleSelect">
            <template v-for="item in menuList">
                <template v-if="item.children && item.children.length === 1">
                    <side-menu-item v-if="showChildren(item)" :key="`menu-${item.name}`" :parent-item="item"></side-menu-item>
                    <menu-item v-else :name="`${item.children[0].name}`" :key="`menu-${item.children[0].name}`"><common-icon :type="item.children[0].icon || ''" /><span>{{ showTitle(item.children[0]) }}</span></menu-item>
                </template>
                <template v-else>
                    <side-menu-item v-if="showChildren(item)" :key="`menu-${item.name}`" :parent-item="item"></side-menu-item>
                    <menu-item v-else :name="`${item.name}`" :key="`menu-${item.name}`"><common-icon :type="item.icon || ''" /><span>{{ showTitle(item) }}</span></menu-item>
                </template>
            </template>
        </Menu>
        <div class="menu-collapsed" v-show="collapsed" :list="menuList">
            <template v-for="item in menuList">
                <collapsed-menu v-if="item.children && item.children.length > 1" @on-click="handleSelect" hide-title :root-icon-size="rootIconSize" :icon-size="iconSize" :theme="theme" :parent-item="item" :key="`drop-menu-${item.name}`"></collapsed-menu>
                <Tooltip v-else :content="item.children[0].meta.title" placement="right" :key="`drop-menu-${item.children[0].name}`">
                    <a @click="handleSelect(item.children[0].name)" class="drop-menu-a" :style="{textAlign: 'center'}"><Icon :size="rootIconSize" :color="textColor" :type="item.children[0].icon" /></a>
                </Tooltip>
            </template>
        </div>
    </div>
</template>
<script lang="ts" src="./side-menu.ts"></script>