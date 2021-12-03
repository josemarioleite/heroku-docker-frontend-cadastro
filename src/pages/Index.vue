<template>
  <div style="background: linear-gradient(#261479, #443780)">
      <div class="banner transparent">
        <div class="borderFrame">
          <div class="textBanner q-mt-md q-mb-xs">Olá Visitante</div>
          <div class="q-pa-sm">

            <q-input v-model="email" :error="errorEmail" bg-color="white" color="black" outlined error-message="Este campo é obrigado a estar preenchido" label="E-mail">
              <template v-slot:prepend>
                <q-icon name="email" color="dark" />
              </template>
            </q-input>

            <q-input class="input" v-model="cpfPis" :error="errorCpfPis" bg-color="white" color="black" outlined error-message="Este campo é obrigado a estar preenchido" label="CPF ou PIS">
              <template v-slot:prepend>
                <q-icon name="person" color="dark" />
              </template>
            </q-input>

            <q-input type="password" class="input" v-model="password" :error="errorPassword" bg-color="white" color="black" outlined error-message="Este campo é obrigado a estar preenchido" label="Senha">
              <template v-slot:prepend>
                <q-icon name="lock" color="dark" />
              </template>
            </q-input>

            <q-btn @click="dialogRegister" size="12px" flat label="Cadastrar-se" class="text-white float-right" />
            <q-btn @click="authLogin" rounded class="button" label="Login" :disable="lockButton" />
            <DialogRegister ref="dialogRegister" />
          </div>
        </div>
      </div>
  </div>
</template>

<script>
import { Get, Post } from '../utils/conexion.js'
import { User } from 'src/models/user.js'
import { DecodeJWT } from '../utils/Jwt.js'
import DialogRegister from 'src/templates/dialogs/UserDialog.vue'

export default {
  name: 'Index',
  components: { DialogRegister },
  data () {
    return {
      email: '',
      cpfPis: '',
      password: '',
      lockButton: false,
      errorEmail: false,
      errorCpfPis: false,
      errorPassword: false,
    }
  },
  methods: {
    dialogRegister () {
      this.$refs.dialogRegister.show(null, 'Cadastrar novo usuário', false)
    },
    pageHome () {
      this.$router.push('home');
    },
    verifyToken () {
      var obj = localStorage.getItem('token')
      if (obj != null) {
        Get('auth/token').then(res => {
          if (res.status == 200) {
            this.pageHome()
          }
        }).catch(err => {})
      }
    },
    authLogin () {
      this.validateData().then(() => {
        this.confirmAction('Deseja realmente se logar ?').then(() => {
          this.lockButton = true
          var user = new User()

          user.CpfPis = this.cpfPis
          user.Email = this.email
          user.Password = this.password

          try {
            Post('auth/login', user).then(async (res) => {
              this.$q.loading.show({
                message: 'Aguarde enquanto verificamos...'
              })
              localStorage.setItem('token', res.data.token)
              localStorage.setItem('tokenContent', await DecodeJWT())
              setTimeout(() => {
                if (res.data.status === true) {
                  this.$q.notify({
                    message: res.data.msg,
                    color: 'green'
                  })
                  this.pageHome()
                } else {
                  this.$q.notify({
                    message: res.data.msg,
                    color: 'red'
                  })
                }
              }, 250)
            }).catch(err => {
              console.log(err)
              this.$q.notify({
                message: 'Erro ao fazer autenticação',
                color: 'red'
              })
              this.timer = setTimeout(() => {
                this.$q.loading.hide()
                this.timer = void 0
              }, 1000)
            })
          } catch (exc) {
            console.log(exc)
          } finally {
            this.lockButton = false          
          }
          this.timer = setTimeout(() => {
            this.$q.loading.hide()
            this.timer = void 0
          }, 1000)
        })
      })
    },
    confirmAction (message) {
      return new Promise((resolve, reject) => {
        this.$q.dialog({
          dark: true,
          title: message,
          cancel: 'Sair',
          persistent: true
        }).onOk(() => {
          resolve()
        })
      })
    },
    validateData () {
      return new Promise(resolve => {
        var fieldEmpty = false

        if (this.email == '' || this.email == null) {
          this.errorEmail = true
          fieldEmpty = true
        } else {
          this.errorEmail = false
          fieldEmpty = false
        }

        if (this.cpfPis == '' || this.cpfPis == null) {
          this.errorCpfPis = true
          fieldEmpty = true
        } else {
          this.errorCpfPis = false
          fieldEmpty = false
        }

        if (this.password == '' || this.password == null) {
          this.errorPassword = true
          fieldEmpty = true
        } else {
          this.errorPassword = false
          fieldEmpty = false
        }

        if (fieldEmpty == false) {
          resolve(false)
        }
      })
    },
  },
  beforeMount() {
    this.verifyToken()
  },
  beforeDestroy () {
    if (this.timer !== void 0) {
      clearTimeout(this.timer)
      this.$q.loading.hide()
    }
  }
};
</script>

<style>
  .input {
    padding-top: 5px;
  }

  .button {
    margin-top: 15px;
    width: 100%;
    background: #7860E0;
    color: #fff;
  }

  .banner {
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100vh;
    justify-content: center;
    align-items: center;
  }

  .textBanner {
    font-family: 'Poppins', sans-serif;
    text-align: center;
    color: white;
    font-size: 2rem;
  }

  .borderFrame {
    height: 420px;
    width: 320px;
    border: 1px solid #ccc;
    border-radius: 20px;
  }
</style>
