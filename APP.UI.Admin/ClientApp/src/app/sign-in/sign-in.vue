<template>
<div class="user-layout-container">
<div class="user-layout-content">
    <div class="user-layout-top"><div class="user-layout-header"><span class="user-layout-title">Ant Design</span></div><div class="user-layout-desc">Ant Design 是西湖区最具影响力的 Web 设计规范</div></div>
<div class="user-login-main">
<a-form :layout="'horizontal'">
<a-tabs defaultActiveKey="1">
    <a-tab-pane tab="账户密码登录" key="1">
        <a-form-item>
        <a-input size="large" placeholder="用户名"><a-icon slot="prefix" type="user" class="prefixIcon" /></a-input>
      </a-form-item>
      <a-form-item>
        <a-input size="large" placeholder="密码"><a-icon slot="prefix" type="lock" class="prefixIcon" /></a-input>
      </a-form-item>
    </a-tab-pane>
  </a-tabs>
</a-form>
</div>

</div>
<a-layout-footer>刘泽华 @2019</a-layout-footer>
</div>
</template>

<script lang="ts" src="./sign-in.ts"></script>