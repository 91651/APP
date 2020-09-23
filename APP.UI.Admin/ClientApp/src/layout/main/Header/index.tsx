import { defineComponent, ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { Layout, Avatar, Menu, Dropdown, Breadcrumb } from 'ant-design-vue'
import { SettingOutlined, SearchOutlined, MenuFoldOutlined, MenuUnfoldOutlined } from '@ant-design/icons-vue';
import components from "./components";
import styles from './index.less';

const { Footer: ALayoutFooter } = Layout

export default defineComponent({
  name: 'PageFooter',
  components: { ...components },
  props: {
    collapsed: {
      type: Boolean,
    }
  },
  methods: {
    emitClick(event: Event) {
      this.$emit('hello-vue', event)
    }
  },
  render() {
    const username = ref(localStorage.getItem('username') || '');
    const router = useRouter();
    const route = useRoute();
    const signout = () => {
      console.log(router, '退出登录')
      router.replace({
        name: 'login',
        query: {
          redirect: route.fullPath
        }
      })
    }
    return <>
      <Layout.Header class="layout-header">
        <div class="left-options">
          <span onClick={() => this.$emit('update:collapsed', !this.collapsed)} class="menu-fold">
            {this.collapsed  ? <menu-unfold-outlined/> : <menu-fold-outlined />}
          </span>
          <a-breadcrumb>
            {route.matched.map((routeItem, i) => {
              <a-breadcrumb-item key={routeItem.name}>
                <a>{routeItem.meta.title}</a>
                <template v-slot="overlay">
                  {routeItem.children.length &&
                    <a-menu v-if="routeItem.children.length">
                      {routeItem.children.map((childItem, i) => {
                        childItem.meta?.hidden && <a-menu-item key={childItem.path}>
                          <router-link to={routeItem.path == '/' ? childItem.path : (routeItem.path + '/' + childItem.path)}>
                            {childItem.meta.title}
                          </router-link>
                        </a-menu-item>
                      })}
                    </a-menu>
                  }

                </template>
              </a-breadcrumb-item>
            })}

          </a-breadcrumb>
        </div>
        <div class="right-options">
          <SettingOutlined />
          <SearchOutlined />
          <Dropdown>
            <a-avatar>{{ username }}</a-avatar>
            <template v-slot="overlay">
              <a-menu>
                <a-menu-item>
                  <a href="javascript:;">个人中心</a>
                </a-menu-item>
                <a-menu-divider />
                <a-menu-item>
                  <a onClick={signout}>退出登录</a>
                </a-menu-item>
              </a-menu>
            </template>
          </Dropdown>
        </div>
      </Layout.Header>
    </>
  }
});
