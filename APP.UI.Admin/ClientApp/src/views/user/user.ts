import { Component, Vue, Prop } from 'vue-property-decorator';
import { State, Action, Getter } from 'vuex-class';
import './user.less';

@Component
export default class UserComponent extends Vue {
  @Action private getUsers!: () => Promise<void>;
  private getUsertList(): void {
    this.getUsers();
  }
}
