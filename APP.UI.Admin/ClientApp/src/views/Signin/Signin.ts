import { Options, Vue } from 'vue-class-component';

export default class extends Vue {
    private signinForm = { userName : "", password: ""};

    private handleSubmit(): void{
        console.log(this.signinForm);
        this.signinForm.password = "13";
        debugger
    }
  
}
