import { Component, Vue, Prop } from 'vue-property-decorator';
import './sign-in.less';
import { _Auth } from '@/api-client';
@Component
export default class SignInComponent extends Vue {
  private signInForm: any;
  private message?: string = '';

  private beforeCreate() {
    this.signInForm = this.$form.createForm(this);
  }

  private handleSubmit(e: Event): void {
    e.preventDefault();
    this.signInForm.validateFields((err: Error, values: any) => {
      if (!err) {
        _Auth.signIn(values.userName, values.password).then((d) => {
          if (d.status) {
            this.$router.replace('/');
          } else {
            this.message = d.message;
          }
        });
      }
    });
  }
}
