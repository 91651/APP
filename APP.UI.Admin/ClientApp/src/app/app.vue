<template>
  <div id="app">
    <router-view/>
  </div>
</template>
<script lang="ts">

import { Component, Vue } from 'vue-property-decorator';
import Main from './main/main.vue';

@Component({
  components: {
    Main
  },
})
export default class App extends Vue {}
</script>

<style lang="less">
.size{
  width: 100%;
  height: 100%;
}
html,body{
  .size;
  margin: 0;
  padding: 0;
}
#app {
  .size;
}
</style>
