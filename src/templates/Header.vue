<template>
  <q-header elevated class="bg-indigo-7 text-white">
    <q-toolbar>
      <q-toolbar-title>
        <div class="row col-12 items-center">
          <div class="row col-11 items-center">
            <q-avatar class="avatarHeader q-mr-sm">
              <img src="../assets/avatarUser.png">
            </q-avatar>
            <div class="titleHeader">{{this.User.email}}</div>
          </div>
          <div class="row col-1 justify-end">
            <ItemUser
              :User="User"
              :ButtonEnabled="ButtonEnabled"
              :ReloadData="reloadData" />
          </div>
        </div>
      </q-toolbar-title>
    </q-toolbar>
  </q-header>
</template>

<script>
import ItemUser from './ItemUser.vue'

export default {
  name: 'Header',
  props: {
    User: Object,
    RealodData: Function,
    ButtonEnabled: {
      type: Boolean,
      default: true
    }
  },
  components: {
    ItemUser
  },
  methods: {
    reloadData () {
      this.RealodData()
    }
  }
}
</script>

<style>
  @media only screen and (max-width: 499px) {
    .titleHeader {
      align-self: center;
      margin-left: 25px;
    }
  }
</style>
