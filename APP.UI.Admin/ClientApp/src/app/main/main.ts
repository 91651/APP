import { Component, Vue, Prop } from 'vue-property-decorator';
import { mapMutations, mapActions } from 'vuex';
import SideMenu from './components/side-menu/side-menu.vue';
import minLogo from '@/assets/images/logo-min.jpg';
import maxLogo from '@/assets/images/logo.jpg';
import './main.less';

@Component({
    components: {
        SideMenu,
    },
})
export default class MainComponent extends Vue {

    private collapsed: boolean = false;
    private minLogo: string = minLogo;
    private maxLogo: string = maxLogo;
    private isFullscreen: boolean = false;

    private handleCollapsedChange(state: any) {
        this.collapsed = state;
    }
    // get tagNavList() {
    //     return this.$store.state.app.tagNavList;
    // }
    // get gettagRouter() {
    //     return this.$store.state.app.tagRouter;
    // }
    get userAvator() {
        debugger;
        return this.$store.state.user.avatorImgPath;
    }
    // get cacheList() {
    //     return this.tagNavList.length ?
    //         this.tagNavList.filter(item => !(item.meta && item.meta.notCache)).map(item => item.name) : [];
    // }
    // get menuList() {
    //     return this.$store.getters.menuList;
    // }
    // get local() {
    //     return this.$store.state.app.local;
    // }
}
