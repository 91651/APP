import { Component, Vue, Prop } from 'vue-property-decorator';
import { mapMutations, mapActions } from 'vuex';
import SideMenu from '../components/Menu/SideMenu.vue';
import minLogo from '@/assets/images/logo-min.jpg';
import maxLogo from '@/assets/images/logo.jpg';
import './main.less';
import { MenuModel } from '@/api-client/client';
import { _Common } from '@/api-client';

@Component({
    components: {
        SideMenu,
    },
})
export default class MainComponent extends Vue {

    private collapsed: boolean = false;
    private minLogo: string = minLogo;
    private maxLogo: string = maxLogo;
    private menus: MenuModel[] = new Array<MenuModel>();

    private mounted() {
        _Common.getMenus().then(r => {
            this.menus = r!;
            debugger;
        });
    }
}
