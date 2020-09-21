import {defineComponent, ref} from 'vue';
import {Layout} from 'ant-design-vue';
import components from "./components";
import styles from './index.less';

const {Footer: ALayoutFooter} = Layout

export default defineComponent({
    name: 'PageFooter',
    components: {...components},
    props: {
    collapsed: {
      type: Boolean,
    }
  },
    setup() {
        return () => (
            <>
                <ALayoutHeader class="layout-header">
    <div class="left-options">
      <span onClick={() => $emit('update:collapsed', !collapsed)} class="menu-fold">
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
            </>
        );
    },
});
