import { defineComponent, ref, unref, watch, SetupContext } from 'vue';
import { useRoute } from "vue-router";
import router from "@/router/";
import antIcons from './ant-icons';
import { message } from 'ant-design-vue';

export default defineComponent({
  name: 'PageFooter',
  components: { ...antIcons },
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
    const route = useRoute();
    const getOpenKeys = () => {
      return route.meta.isGroup ? route.matched.slice(1, 3).map(item => item.path) : [route.matched[1].path]
    };

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
    return <>
      <div class="logo">
        <img src="../../assets/logo.svg" />
        <h1 >黑匣子控制中心</h1>
      </div>

      <a-menu onClick={clickMenuItem} theme="dark" mode="inline" inlineCollapsed="false" v-model-selectedKeys={selectedKeys} v-model-openKeys={openKeys} openChange={onOpenChange}>
        {routes.map((group, i) => {
          if (!group.meta?.hidden) {
            <a-sub-menu key={group.path}>
              <template v-slot="title">
                <component is={group.meta?.icon} />
                <span>{group.meta?.title}</span>
              </template>
              {group.children?.map((menu, i) => {
                if (menu.meta?.isGroup) {
                  <a-sub-menu key={group.path + '/' + menu.path}>
                    <template v-slot="title">
                      <component is={menu.meta?.icon} />
                      <span class="nav-text">{menu.meta.title}</span>
                    </template>
                  </a-sub-menu>
                  {
                    menu.children?.map((item, i) => {
                      if (!item.meta?.hidden) {
                        <a-menu-item key={group.path + '/' + menu.path + '/' + item.path}>
                          <span class="nav-text">{item.meta?.title}</span>
                        </a-menu-item>
                      }
                    })
                  }

                }

                {
                  if (!menu.meta?.hidden && !menu.meta?.isGroup) {
                    <a-menu-item key={group.path + '/' + menu.path}>
                      <component is={menu.meta?.icon} />
                      <span class="nav-text">{menu.meta?.title}</span>
                    </a-menu-item>
                  }
                }
              })}




            </a-sub-menu>
          }
        })}
      </a-menu>
    </>
  }
});
