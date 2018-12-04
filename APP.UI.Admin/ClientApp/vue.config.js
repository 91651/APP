var appConfig = require('./src/app-config.json');
var path = require('path');
var CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
  baseUrl: '',
  productionSourceMap: false,
  chainWebpack: config => {
    config.plugins.delete('fork-ts-checker'); // Vue Cli 3.0后，ts分离到独立文件会报错。 https://github.com/vuejs/vue-cli/issues/1104
  },
  devServer: {
    //open: process.platform === 'darwin',
    //host: '0.0.0.0',
    port: 8010,
    //https: false,
    //hotOnly: false,
    proxy: {
      '^/static/': {
        target: appConfig.apiUrl
      }
      //before: app => {}
    }
  },
  configureWebpack: {
    plugins: [
      new CopyWebpackPlugin([{
        from: path.join(__dirname + '/src', 'app-config.json'),
        to: path.join(__dirname, 'dist/')
      }])
    ]
  }
}