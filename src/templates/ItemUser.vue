<template>
  <q-btn :disable="ButtonEnabled" class="float-right" icon="exit_to_app" size="13px" style="background: #080808">
    <q-menu>
      <div class="row no-wrap q-pa-md">
        <div class="column justify-center">
          <div class="text-h6 q-mb-md" style="font-family: 'Ruthie', cursive; font-size: 40px">Pontotel</div>
          <div style="margin-top: 30px">
            <div class="text-center">Versão: {{versaoApp}}</div>
            <div class="text-center q-mb-md">Data: {{dataVersaoApp}}</div>
            <q-btn
              @click="openDialogUser"
              class="q-ml-xs"
              color="red"
              label="Editar Dados"
              push
              size="sm"
              v-close-popup
            />
          </div>
        </div>

        <q-separator vertical inset class="q-mx-lg" />

        <div class="column items-center">
          <q-avatar size="72px" class="bg-dark">
            <div class="text-white">{{User.name.substr(0,1)}}</div>
          </q-avatar>

          <div class="text-subtitle2 ellipsis q-mt-md">{{User.name}}</div>
          <div class="text-subtitle2 q-mb-xs">{{User.email}}</div>

          <q-btn
            @click="removeItemsLocalStorage"
            color="primary"
            label="Sair"
            push
            size="sm"
            v-close-popup
          />
        </div>
      </div>
    </q-menu>
    <UserDialog ref="userDialog" @reloadData="reloadData" />
  </q-btn>
</template>

<script>
import UserDialog from './dialogs/UserDialog.vue'

export default {
  name: 'Item-Menu',
  components: {
    UserDialog
  },
  props: {
    User: Object,
    ReloadData: Function,
    ButtonEnabled: {
      type: Boolean,
      default: true
    }
  },
  data () {
    return {
      versaoApp: '',
      dataVersaoApp: ''
    }
  },
  methods: {
    openDialogUser () {
      this.$refs.userDialog.show(this.User, 'Editar Dados', true)
    },
    reloadData () {
      this.ReloadData()
    },
    removeItemsLocalStorage() {
      localStorage.removeItem('token')
      localStorage.removeItem('tokenContent')
      this.$router.push('/')
      window.history.forward()
    }
  },
  mounted () {
    this.versaoApp = '1.0.0'
    this.dataVersaoApp = '07/11/2021'
  }
}
</script>
