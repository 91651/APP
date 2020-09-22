import {defineComponent, reactive, nextTick, toRefs, unref, watch} from 'vue'
import {useRoute, useRouter} from "vue-router";
import components from "./components";
import {message} from 'ant-design-vue'

export default defineComponent({
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
    const route = useRoute()
    const router = useRouter()

    // 获取简易的路由对象
    const getSimpleRoute = (route: any): any => {
      const {fullPath, hash, meta, name, params, path, query} = route
      return {fullPath, hash, meta, name, params, path, query}
    }

    let routes: any[] = []

    try {
      const routesStr = localStorage.getItem('routes') as string | null | undefined
      routes = routesStr ? JSON.parse(routesStr) : [getSimpleRoute(route)]
    } catch (e) {
      routes = [getSimpleRoute(route)]
    }

    const state = reactive({
      pageList: routes,
      activeKey: route.fullPath,
    })

    // 保存页面
    const savePageList = () => {
      console.count('你执行了多少次')
      nextTick(() => localStorage.setItem('routes', JSON.stringify(state.pageList)))
    }

    const whiteList = ['Redirect', 'login']

    watch(() => route.fullPath, (to, from) => {
      if (whiteList.includes(route.name as string)) return
      state.activeKey = to
      if (!state.pageList.find((item: any) => unref(item).fullPath == to)) {
        state.pageList.push(getSimpleRoute(route))
      }
    }, {immediate: true})

    watch(() => state.pageList, savePageList, {deep: true})

    // 关闭页面
    const removeTab = (targetKey: any) => {
      if (state.pageList.length === 1) {
        return message.warning('这已经是最后一页，不能再关闭了！')
      }
      const index = state.pageList.findIndex(item => item.fullPath == targetKey)
      state.pageList.splice(index, 1)
      // 如果关闭的是当前页
      if (state.activeKey === targetKey) {
        const currentRoute = state.pageList[Math.max(0, state.pageList.length - 1)]
        state.activeKey = currentRoute.fullPath
        router.push(currentRoute)
      }
    }
    // tabs 编辑（remove || add）
    const editTabItem = (targetKey: any, action: string) => {
      if (action == 'remove') {
        removeTab(targetKey)
      }
    }
    // 切换页面
    const changePage = (key: any) => {
      state.activeKey = key
      router.push(key)
    }

    // 刷新页面
    const reloadPage = () => {
      router.push({
        path: '/redirect' + unref(route).fullPath,
      })
    }

    // 关闭左侧
    const closeLeft = (fullPath: any, index: any) => {
      state.pageList.splice(0, index)
      state.activeKey = fullPath
      router.replace(fullPath)
    }

    // 关闭右侧
    const closeRight = (fullPath: any, index: any) => {
      state.pageList.splice(index + 1)
      state.activeKey = fullPath
      router.replace(fullPath)
    }

    // 关闭其他
    const closeOther = (fullPath: any) => {
      state.pageList = state.pageList.filter(item => item.fullPath == fullPath)
      state.activeKey = fullPath
      router.replace(fullPath)
    }

    // 关闭全部
    const closeAll = () => {
      localStorage.removeItem('routes')
      state.pageList = []
      router.replace('/')
    }
    return <>
      <div class="tabs-view">
        <a-tabs v-model-activeKey={state.activeKey} onChange={changePage} hide-add type="editable-card" edit={editTabItem} class="tabs">
        {
          state.pageList.map((pageItem, index) => {
            <template key={pageItem.fullPath}>
<a-tab-pane closable={pageItem.closable}>
<template>

<a-dropdown trigger={['contextmenu']}>
              <div style="display: inline-block">
                { pageItem.meta.title }
              </div>
              <template v-slot="overlay">
                <a-menu style="user-select: none">
                  <a-menu-item onClick="reloadPage" disabled={state.activeKey !== pageItem.fullPath} key="1">
                    <reload-outlined/>
                    刷新
                  </a-menu-item>
                  <a-menu-item onClick={removeTab(pageItem.fullPath)} key="2">
                    <close-outlined/>
                    关闭
                  </a-menu-item>
                  <a-menu-divider/>
                  <a-menu-item onClick={closeLeft(pageItem.fullPath, index)} key="3">
                    <vertical-right-outlined/>
                    关闭左侧
                  </a-menu-item>
                  <a-menu-item onClick={closeRight(pageItem.fullPath, index)} key="4">
                    <vertical-left-outlined/>
                    关闭右侧
                  </a-menu-item>
                  <a-menu-divider/>
                  <a-menu-item onClick={closeOther(pageItem.fullPath)} key="5">
                    <column-width-outlined/>
                    关闭其他
                  </a-menu-item>
                  <a-menu-item onClick={closeAll} key="6">
                    <minus-outlined/>
                    关闭全部
                  </a-menu-item>
                </a-menu>
              </template>
            </a-dropdown>


</template>

</a-tab-pane>

              </template>
          })
        }
<template v-slot="tabBarExtraContent">
        <a-dropdown trigger={['click']}>
          <a class="ant-dropdown-link" onClick={e => e.preventDefault()}>
            <down-outlined style="{fontSize: '20px'}"/>
          </a>
          <template v-slot="overlay">
            <a-menu style="user-select: none">
              <a-menu-item onClick={reloadPage} disabled={state.activeKey !== route.fullPath} key="1">
                <reload-outlined/>
                刷新
              </a-menu-item>
              <a-menu-item onClick={removeTab(route.fullPath)} key="2">
                <close-outlined/>
                关闭
              </a-menu-item>
              <a-menu-divider/>
              <a-menu-item onClick={closeOther(route.fullPath)} key="5">
                <column-width-outlined/>
                关闭其他
              </a-menu-item>
              <a-menu-item onClick={closeAll} key="6">
                <minus-outlined/>
                关闭全部
              </a-menu-item>
            </a-menu>
          </template>
        </a-dropdown>
      </template>
          
        </a-tabs>
        <div class="tabs-view-content">
      <router-view v-slot="{ Component }">
        <transition name="zoom-fade" mode="out-in">
          <component is="Component"/>
        </transition>
      </router-view>
    </div>
      </div>
    </>
  }
});
