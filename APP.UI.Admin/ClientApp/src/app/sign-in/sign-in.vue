<template>
  <div class="user-layout-container">
    <div class="user-layout-content">
      <div class="user-layout-top">
        <div class="user-layout-header">
          <span class="user-layout-title">
            <a-icon type="smile"/>遇见时光
          </span>
        </div>
        <div class="user-layout-desc">总是热衷于代码的折腾，追求简洁与优雅！</div>
      </div>
      <div class="user-login-main">
        <a-form :layout="'horizontal'" :form="signInForm" @submit="handleSubmit">
          <a-tabs defaultActiveKey="1">
            <a-tab-pane tab="账户密码登录" key="1">
              <a-form-item>
                <a-input
                  size="large"
                  placeholder="用户名"
                  v-decorator="['userName',{ rules: [{ required: true, message: '请输入用户账户!' }] }]"
                >
                  <a-icon slot="prefix" type="user" class="prefixIcon"/>
                </a-input>
              </a-form-item>
              <a-form-item>
                <a-input
                  size="large"
                  placeholder="密码"
                  type="password"
                  v-decorator="['password', { rules: [{ required: true, message: '请输入密码!' }] }]"
                >
                  <a-icon slot="prefix" type="lock" class="prefixIcon"/>
                </a-input>
              </a-form-item>
              <a-alert message="Error" type="error" showIcon/>
            </a-tab-pane>
          </a-tabs>
          <a-form-item>
            <a-checkbox>自动登录</a-checkbox>
            <a-button type="primary" block html-type="submit" class="login-form-button">登录</a-button>
          </a-form-item>
        </a-form>
      </div>
    </div>
    <a-layout-footer>刘泽华 @2019</a-layout-footer>
  </div>
</template>

<script lang="ts" src="./sign-in.ts"></script>