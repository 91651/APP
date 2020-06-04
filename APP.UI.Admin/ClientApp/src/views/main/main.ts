import { Component, Vue } from 'vue-property-decorator';
import { mapMutations, mapActions } from 'vuex';
import SideMenu from '../components/Menu/SideMenu.vue';
import './main.less';
import { MenuModel } from '@/api-client/client';
import { _Common } from '@/api-client';

@Component({
    components: {
        SideMenu,
    },
})
export default class MainComponent extends Vue {

    private collapsed = false;
    private menus: MenuModel[] = new Array<MenuModel>();

    private mounted() {
        _Common.getMenus().then(r => {
            this.menus = r!;
            console.log(this.menus);
        });
    }
}
