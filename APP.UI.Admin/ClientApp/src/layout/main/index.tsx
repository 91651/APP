import {defineComponent, ref, h, computed} from 'vue';
import {Layout, message} from 'ant-design-vue';
import Header from './Header';
import Footer from './Footer';
import Menu from './Menu';
import Tabs from './Tabs';

export default defineComponent({
  components: { Header, Footer, Menu, Tabs },
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
    const collapsed = ref<boolean>(false)

    const testMsg = () => {
      message.success(h('span', '啥子'), 2)
    }

    const asiderWidth = computed(() => collapsed.value ? '80px' : '256px')

    return <>
      <a-layout class="layout">
    <a-layout-sider v-model-collapsed={collapsed} trigger={null} collapsible class="layout-sider">
      <sider-menu collapsed={collapsed}/>
    </a-layout-sider>
    <a-layout>
      <page-header v-model-collapsed="collapsed" />
      <a-layout-content class="layout-content">
        <tabs-view/>
      </a-layout-content>
    </a-layout>
  </a-layout>
    </>
  }
});
