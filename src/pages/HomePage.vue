<template>
  <q-layout view="hHh lpR fFf">
    <div class="container" style="background: linear-gradient(#261479, #443780)">
      <Header :User="user" :ButtonEnabled="buttonDisabled" :RealodData="teste" />
      <div class="text-white text-h5">Olá {{user.name}}</div>
    </div>
  </q-layout>
</template>

<script>
import { Get } from "../utils/conexion.js";
import Header from '../templates/Header.vue'

export default {
  name: 'Home',
  components: { Header },
  data () {
    return {
      user: {},
      buttonDisabled: true
    }
  },
  methods: {
    verifyToken () {
      var obj = localStorage.getItem('token')
      if (obj != null) {
        Get('auth/token').then( async (res) => {
          if (res.status != 200) {
            this.$router.push('/')
          } else if (res.status == 200) {
            var userToken = JSON.parse(localStorage.getItem('tokenContent'))
            Get('user/' + userToken.id).then( async (res) => {
              if (res.status == 200) {
                this.user = res.data
                this.buttonDisabled = !this.buttonDisabled
                console.log('Verificado...')
              }
            }).catch(err => {
              console.log(err)
            })
          }
        }).catch(err => {
          console.log(err)
          this.$router.push('/')
        })
      } else {
        this.$router.push('/')
      }
    },
    teste () {
      this.verifyToken()
      this.buttonDisabled = !this.buttonDisabled
    }
  },
  beforeMount () {
    this.verifyToken()
  }
}
</script>

<style>
  .container {
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100vh;
    justify-content: center;
    align-items: center;
  }
</style>
