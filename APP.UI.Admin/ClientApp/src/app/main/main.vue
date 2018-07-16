<template>
    <Layout style="height: 100%" class="main">
        <Sider hide-trigger collapsible :width="210" :collapsed-width="64" v-model="collapsed">
            <Header class="header-con">
                <header-bar :collapsed="collapsed" @on-coll-change="handleCollapsedChange">
                    <user :user-avator="userAvator" />
                    <language @on-lang-change="setLocal" style="margin-right: 10px;" :lang="local" />
                    <fullscreen v-model="isFullscreen" style="margin-right: 10px;" />
                </header-bar>
            </Header>
            <side-menu accordion :active-name="$route.name" :collapsed="collapsed" @on-select="turnToPage" :menu-list="menuList">
                <!-- 需要放在菜单上面的内容，如Logo，写在side-menu标签内部，如下 -->
                <div class="logo-con">
                    <img v-show="!collapsed" :src="maxLogo" key="max-logo" />
                    <img v-show="collapsed" :src="minLogo" key="min-logo" />
                </div>
            </side-menu>
        </Sider>
        <Layout>
            
            <Content>
                <Layout>
                    <div class="tag-nav-wrapper">
                        <tags-nav :value="$route" @input="handleClick" :list="tagNavList" @on-close="handleCloseTag" />
                    </div>
                    <Content class="content-wrapper">
                        <keep-alive :include="cacheList">
                            <router-view />
                        </keep-alive>
                    </Content>
                </Layout>
            </Content>
        </Layout>
    </Layout>
</template>

<script lang="ts" src="./main.ts"></script>
