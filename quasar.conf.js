const { configure } = require('quasar/wrappers');

module.exports = configure(function (ctx) {
  return {
    supportTS: true,
    boot: [
      'axios',
    ],
    css: [
      'app.css'
    ],
    extras: [
      'roboto-font', // optional, you are not bound to it
      'material-icons', // optional, you are not bound to it
    ],
    build: {
      vueRouterMode: 'history',
      chainWebpack () {},
    },
    devServer: {
      https: true,
      port: 3535,
      open: true,
      hot: false
    },
    framework: {
      config: {},
      lang: 'pt-BR',
      plugins: [
        'Notify', 'Loading', 'Dialog'
      ]
    },
    animations: [],
    ssr: {
      pwa: false,
      prodPort: 3000,
      maxAge: 1000 * 60 * 60 * 24 * 30,
      chainWebpackWebserver () {},
      middlewares: [
        ctx.prod ? 'compression' : '',
        'render' // keep this as last one
      ]
    },
    pwa: {
      workboxPluginMode: 'GenerateSW',
      workboxOptions: {},
      chainWebpackCustomSW () {},
      manifest: {
        name: `Cadastro-Usuario`,
        short_name: `Cadastro-Usuario`,
        description: `uma aplicação para cadastro de usuarios (desafio pontotel)`,
        display: 'standalone',
        orientation: 'portrait',
        background_color: '#ffffff',
        theme_color: '#027be3',
        icons: [
          {
            src: 'icons/icon-128x128.png',
            sizes: '128x128',
            type: 'image/png'
          },
          {
            src: 'icons/icon-192x192.png',
            sizes: '192x192',
            type: 'image/png'
          },
          {
            src: 'icons/icon-256x256.png',
            sizes: '256x256',
            type: 'image/png'
          },
          {
            src: 'icons/icon-384x384.png',
            sizes: '384x384',
            type: 'image/png'
          },
          {
            src: 'icons/icon-512x512.png',
            sizes: '512x512',
            type: 'image/png'
          }
        ]
      }
    },
    cordova: {
    },
    capacitor: {
      hideSplashscreen: true
    },
    electron: {
      bundler: 'packager', // 'packager' or 'builder'
      packager: {},
      builder: {
        appId: 'desafio-pontotel'
      },
      chainWebpack () {},
      chainWebpackPreload () {},
    }
  }
});
