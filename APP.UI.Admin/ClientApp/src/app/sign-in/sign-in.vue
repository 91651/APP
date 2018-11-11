<template>
  <div class="signin">
    <div class="signin-con">
      <Card icon="log-in" title="欢迎登录" :bordered="false">
        <div class="form-con">
          <Form ref="signInForm" :model="signInForm" :rules="rules">
            <FormItem prop="userName">
              <Input v-model="signInForm.userName" placeholder="请输入用户名">
                <span slot="prepend">
                  <Icon :size="16" type="ios-person"></Icon>
                </span>
              </Input>
            </FormItem>
            <FormItem prop="password">
              <Input type="password" v-model="signInForm.password" placeholder="请输入密码">
                <span slot="prepend">
                  <Icon :size="16" type="md-lock"></Icon>
                </span>
              </Input>
            </FormItem>
            <FormItem>
              <label class="ivu-form-item-error-tip">{{signInForm.message}}</label>
            </FormItem>
            <FormItem>
              <Button @click="handleSubmit" type="primary" long>登录</Button>
            </FormItem>
        </Form>
        </div>
      </Card>
    </div>
  </div>
</template>

<script lang="ts" src="./sign-in.ts"></script>