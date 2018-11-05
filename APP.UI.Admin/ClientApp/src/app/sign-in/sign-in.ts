import { Component, Vue, Prop } from 'vue-property-decorator';
import { State, Action, Getter } from 'vuex-class';
import './sign-in.less';

@Component
export default class SignInComponent extends Vue {
  @Action private signIn!: (data: {}) => Promise<string>;
  private signInForm: any = {
        userName: '',
        password: ''
      };
      private rules: any = {
        userName: [
            { required: true, message: '账号不能为空', trigger: 'blur' }
        ],
        password: [
            { required: true, message: '密码不能为空', trigger: 'blur' }
        ]
      };
      private handleSubmit(): void {
          const form: any = this.$refs.signInForm;
          form.validate((valid: any) => {
            if (valid) {
              this.signIn({name: this.signInForm.userName, pwd: this.signInForm.password}).then(() => {
                this.$router.replace('/');
              });
            }
          })
      }
}
