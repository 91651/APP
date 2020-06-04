import { Component, Vue, Prop } from 'vue-property-decorator';
import SideMenuItem from './side-menu-item.vue';

@Component

export default class SideMenuComponent extends Vue {

    @Prop()
    private menus?: any[];


}