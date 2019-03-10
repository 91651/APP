<template>
        <Layout class="main">
            <Header>
                <Menu mode="horizontal" theme="dark" active-name="1">
                    <div class="layout-logo">遇见</div>
                </Menu>
            </Header>
            <Layout>
                <Sider breakpoint="md" collapsible :collapsed-width="78" :style="{background: '#fff'}">
                    <Menu active-name="1-2" theme="light" width="auto" :open-names="['1']">
                        <Submenu name="1">
                            <template slot="title">
                                <Icon type="ios-navigate"></Icon>
                                文章管理
                            </template>
                            <MenuItem name="1-1"><span>分类列表</span></MenuItem>
                            <MenuItem name="1-2" to="/article"><span>文章列表</span></MenuItem>
                        </Submenu>
                        <Submenu name="1">
                            <template slot="title">
                                <Icon type="ios-navigate"></Icon>
                                用户管理
                            </template>
                            <MenuItem name="2-1" to="/user"><span>用户列表</span></MenuItem>
                        </Submenu>
                    </Menu>
                </Sider>
                <Layout>
                    <Breadcrumb :style="{margin: '10px'}">
                        <BreadcrumbItem>Home</BreadcrumbItem>
                        <BreadcrumbItem>Components</BreadcrumbItem>
                        <BreadcrumbItem>Layout</BreadcrumbItem>
                    </Breadcrumb>
                    <Content class="content-wrapper">
                        <keep-alive>
                            <router-view v-if="$route.meta.keepAlive"/>
                        </keep-alive>
                    </Content>
                </Layout>
            </Layout>
            <Footer class="layout-footer-center">2011-2016 &copy; TalkingData</Footer>
        </Layout>
</template>

<script lang="ts" src="./main.ts"></script>

