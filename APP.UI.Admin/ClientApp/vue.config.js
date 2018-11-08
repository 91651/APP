var path = require('path');

var CopyWebpackPlugin = require('copy-webpack-plugin');


module.exports = {
  baseUrl: "/",
  chainWebpack: config => {
    config.plugins.delete('fork-ts-checker'); // Vue Cli 3.0后，ts分离到独立文件会报错。 https://github.com/vuejs/vue-cli/issues/1104
    config.module
      .rule('ts')
      .use('ts-loader')
  },
  devServer: {
    //open: process.platform === 'darwin',
    //host: '0.0.0.0',
    port: 8010,
    //https: false,
    //hotOnly: false,
    //proxy: null, // string | Object
    //before: app => {}
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