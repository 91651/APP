import { Component, Vue, Prop } from 'vue-property-decorator';
import './sign-in.less';

@Component
export default class SignInComponent extends Vue {
      public signInForm: any = {
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
          let form: any = this.$refs.signInForm;
          form.validate((valid: any) => {
            if (valid) {
            }
          })
      }
}
