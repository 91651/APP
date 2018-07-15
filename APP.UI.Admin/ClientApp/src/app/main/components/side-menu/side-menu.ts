import { Component, Vue, Prop } from 'vue-property-decorator';
import SideMenuItem from './side-menu-item.vue';

@Component({
    components: {
        SideMenuItem,
    }
})

export default class SideMenuComponent extends Vue {
    @Prop()
    menuList: {}

    @Prop()
    collapsed: Boolean;
}