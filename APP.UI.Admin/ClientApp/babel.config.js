module.exports = {
  presets: [
    [
      '@vue/app', {
        debug: true,
        polyfills: [
          'es6.promise',
          //iview兼容性铺垫
          'es6.array.find-index',
          'es7.array.includes',
          'es6.string.includes'
        ]
      }
    ]
  ]
}