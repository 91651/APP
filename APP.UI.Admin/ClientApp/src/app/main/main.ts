import { Component, Vue, Prop } from 'vue-property-decorator';
import SideMenu from './components/side-menu/side-menu.vue';

@Component({
    components: {
        SideMenu,
    },
})
export default class MainComponent extends Vue {
}
