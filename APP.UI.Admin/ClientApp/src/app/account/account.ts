import { Component, Vue, Prop } from 'vue-property-decorator';
import { State, Action, Getter } from 'vuex-class';
import './account.less';

@Component
export default class AccountComponent extends Vue {
  @Action private getAccount!: () => Promise<void>;
  private getAccountList(): void {
    debugger;
    this.getAccount();
  }
}
