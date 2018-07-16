import { Component, Vue, Prop } from 'vue-property-decorator';
import SideMenu from './components/side-menu/side-menu.vue';
import minLogo from '@/assets/images/logo-min.jpg';
import maxLogo from '@/assets/images/logo.jpg';

@Component({
    components: {
        SideMenu,
    },
})
export default class MainComponent extends Vue {
    @Prop() private collapsed: boolean = false;
    @Prop() private minLogo: string = minLogo;
    @Prop() private maxLogo: string = maxLogo;
    @Prop() private isFullscreen: boolean = false;
}
