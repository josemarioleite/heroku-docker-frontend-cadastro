<template>
  <q-dialog maximized v-model="showDialog" persistent transition-show="scale" transition-hide="scale">
    <q-layout view="Lhh lpR fff" container class="bg-white">

      <q-header class="row bg-dark absolute" style="padding: 10px">
        <q-toolbar-title>{{titleDialog}}</q-toolbar-title>
        <q-btn flat dense icon="close" align="right" @click="close" />
      </q-header>

      <q-page-container>
        <q-page>
          <div class="q-pl-md q-pt-md q-pb-xs col-12 text-h5 text-grey-7">Dados Pessoais</div>
          <div class="col-12">
            <hr>
          </div>
          <div class="q-pl-xs q-pr-xs col-12 row">
            <q-input :error="errorName" v-model="name" bg-color="white" outlined label="Nome" class="inputClass col-md-6 col-xs-12"> <template v-slot:prepend> <q-icon name="person" color="dark" /> </template> </q-input>
            <q-input :error="errorBirthDate" v-model="birthDate" class="inputClass spaceField col-md-6 col-xs-12" label="Data de Nascimento" mask="##/##/####" outlined bg-color="white">
              <template v-slot:prepend>
                <q-icon name="calendar_today" class="cursor-pointer" color="dark">
                  <q-popup-proxy ref="qDateProxy" cover transition-show="scale" transition-hide="scale">
                    <q-date v-model="birthDate" subtitle="Data de" title="Nascimento" mask="DD-MM-YYYY">
                      <div class="row items-center justify-end">
                        <q-btn v-close-popup label="Ok" color="primary" flat />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </template>
            </q-input>
          </div>
          <div class="row col-12 q-pl-xs q-pr-xs">
            <q-input :error="errorEmail" v-model="email" bg-color="white" outlined label="E-mail" class="inputClass col-md-6 col-xs-12"> <template v-slot:prepend> <q-icon name="email" color="dark" /> </template> </q-input>
            <q-input :error="errorCpfPis" v-model="cpfPis" :rules="[ val => val.length <= 15 || 'Só é permitido 11 caracteres']" :mask="maskedCpfPis" bg-color="white" outlined label="CPF/PIS" class="inputClass spaceField col-md-6 col-xs-12">
              <template v-slot:prepend> <q-icon name="fingerprint" color="dark" /> </template>
              <template v-slot:append>
                <div class="row items-center">
                  <div style="font-size: 1rem">CPF</div>
                  <q-toggle v-model="isPis" color="green" />
                  <div style="font-size: 1rem">PIS</div>
                </div>
              </template>
            </q-input>
          </div>

          <div class="row col-12 q-pl-xs q-pr-xs">
            <Select
              label="País"
              v-model="countrySelected"
              class="col-md-6 col-xs-12 inputClass"
              @filter="filterCountry"
              :error="errorCountrySelected"
              :Icon="'public'"
              :Options="countryFilter"
            />

            <Select
              label="Estado"
              v-model="stateSelected"
              class="col-md-6 col-xs-12 text-uppercase inputClass spaceField"
              @filter="filterState"
              :error="errorStateSelected"
              :Icon="'location_city'"
              :Options="stateFilter"
              :OptionsLabel="'nome'"
              :OptionsValue="'sigla'"
            />
          </div>

          <div class="col-12 q-pl-xs q-pr-xs" v-show="isUpdate === false">
            <q-input :error="errorPassword" type="password" v-model="passwordUser" bg-color="white" outlined label="Senha" class="inputClass">
              <template v-slot:prepend>
                <q-icon name="lock" color="dark" />
              </template>
            </q-input>
          </div>

          <div class="q-mt-md q-pl-md col-12 text-h5 text-grey-7">Endereço</div>
          <div class="col-12">
            <hr>
          </div>

          <div class="row col-12 q-pl-xs q-pr-xs">
            <Select
              label="Cidade"
              v-model="citySelected"
              class="col-md-6 col-xs-12 inputClass"
              @filter="filterCity"
              :error="errorCitySelected"
              :Icon="'flag'"
              :Options="cityFilter"
              :OptionsLabel="'name'"
              :OptionsValue="'name'"
            />

            <q-input
              outlined
              bg-color="white"
              v-model="cepSelected"
              mask="##.###-###"
              class="inputClass spaceField col-md-6 col-xs-12"
              :error="errorCEP"
              label="CEP"
            >
              <template v-slot:prepend>
                <q-icon name="apartment" color="dark" />
              </template>

              <template v-slot:append>
                <q-btn color="primary" icon="search" @click="searchCep" size="10px">
                  <q-tooltip style="font-size: 12px">Buscar Endereços pelo CEP</q-tooltip>
                </q-btn>
              </template>
            </q-input>
          </div>

          <div class="row col-12 q-pl-xs q-pr-xs">
            <q-input :error="errorStreetSelected" v-model="streetSelected" bg-color="white" outlined label="Rua" class="inputClass col-md-6 col-xs-12"> <template v-slot:prepend> <q-icon name="add_road" color="dark" /> </template> </q-input>
            <q-input :error="errorNumberSelected" v-model="numberSelected" bg-color="white" outlined label="Número" class="inputClass spaceField col-md-6 col-xs-12"> <template v-slot:prepend> <q-icon name="pin" color="dark" /> </template> </q-input>
          </div>

          <div class="row col-12 q-pl-xs q-pr-xs">
            <q-input :error="errorDistrictSelected" v-model="districtSelected" bg-color="white" outlined label="Bairro" class="inputClass col-md-6 col-xs-12"> <template v-slot:prepend> <q-icon name="maps_home_work" color="dark" /> </template> </q-input>
            <q-input v-model="complementSelected" bg-color="white" outlined label="Complemento" class="inputClass spaceField col-md-6 col-xs-12"> <template v-slot:prepend> <q-icon name="description" color="dark" /> </template> </q-input>
          </div>

          <div class="row justify-center col-12 items-center q-mt-md q-pl-xs q-pr-xs q-mb-md" style="">
            <q-btn style="width: 140px;" color="red" icon="close" label="Cancelar" @click="close" :disable="isDisabled"  />
            <q-btn style="width: 140px; margin-left: 10px" color="green" icon="save" @click="saveUpdate" label="Salvar" />
          </div>
        </q-page>
      </q-page-container>
    </q-layout>
  </q-dialog>
</template>

<script>
import Select from 'src/components/SelectComponent.vue'
import { GetDatas } from 'src/utils/PublicAPIs/GetDatas.js'
import { Get, Put, Post } from 'src/utils/conexion.js'
import { ConvertStringToDatetime } from 'src/utils/dateUtils.js'

export default {
  name: 'User-Dialog',
  components: { Select },
  data () {
    return {
      passwordUser: '',
      id: '',
      name: '',
      email: '',
      birthDate: '',
      cpfPis: '',
      maskedCpfPis: '',
      countrySelected: '',
      stateSelected: '',
      citySelected: '',
      streetSelected: '',
      numberSelected: '',
      districtSelected: '',
      complementSelected: '',
      titleDialog: '',
      cepSelected: '',
      isUpdate: false,
      isDisabled: false,
      isPis: false,
      errorCEP: false,
      errorName: false,
      errorEmail: false,
      errorBirthDate: false,
      errorCpfPis: false,
      errorCountrySelected: false,
      errorStateSelected: false,
      errorCitySelected: false,
      errorStreetSelected: false,
      errorNumberSelected: false,
      errorDistrictSelected: false,
      errorPassword: false,
      showDialog: false,
      countryFilter: [],
      countryData: [],
      stateData: [],
      stateFilter: [],
      cityFilter: [],
      cityData: []
    }
  },
  methods: {
    validateData () {
      return new Promise(resolve => {
        var fieldEmpty = false

        if (this.email === '' || this.email === null) {
          fieldEmpty = true
          this.errorEmail = true
        } else {
          fieldEmpty = false
          this.errorEmail = false
        }

        if (this.cpfPis === '' || this.cpfPis === null) {
          fieldEmpty = true
          this.errorCpfPis = true
        } else {
          fieldEmpty = false
          this.errorCpfPis = false
        }

        if (this.name === '' || this.name === null) {
          fieldEmpty = true
          this.errorName = true
        } else {
          fieldEmpty = false
          this.errorName = false
        }

        if (this.birthDate === '' || this.birthDate === null) {
          fieldEmpty = true
          this.errorBirthDate = true
        } else {
          fieldEmpty = false
          this.errorBirthDate = false
        }

        if (this.countrySelected === '' || this.countrySelected === null) {
          fieldEmpty = true
          this.errorCountrySelected = true
        } else {
          fieldEmpty = false
          this.errorCountrySelected = false
        }

        if (this.stateSelected === '' || this.stateSelected === null) {
          fieldEmpty = true
          this.errorStateSelected = true
        } else {
          fieldEmpty = false
          this.errorStateSelected = false
        }

        if (this.citySelected === '' || this.citySelected === null) {
          fieldEmpty = true
          this.errorCitySelected = true
        } else {
          fieldEmpty = false
          this.errorCitySelected = false
        }

        if (this.streetSelected === '' || this.streetSelected === null) {
          fieldEmpty = true
          this.errorStreetSelected = true
        } else {
          fieldEmpty = false
          this.errorStreetSelected = false
        }

        if (this.numberSelected === '' || this.numberSelected === null) {
          fieldEmpty = true
          this.errorNumberSelected = true
        } else {
          fieldEmpty = false
          this.errorNumberSelected = false
        }

        if (this.districtSelected === '' || this.districtSelected === null) {
          fieldEmpty = true
          this.errorDistrictSelected = true
        } else {
          fieldEmpty = false
          this.errorDistrictSelected = false
        }

        if (this.complementSelected === '' || this.complementSelected === null) {
          fieldEmpty = true
        } else {
          fieldEmpty = false
        }

        if (this.cepSelected === '' || this.cepSelected === null) {
          fieldEmpty = true
          this.errorCEP = true
        } else {
          fieldEmpty = false
          this.errorCEP = false
        }

        if (this.isUpdate === false) {
          if (this.passwordUser === '' || this.passwordUser === null) {
            this.errorPassword = true
            fieldEmpty = true
          } else {
            fieldEmpty = false
            this.errorPassword = false
          }
        }

        if (fieldEmpty == false) {
          resolve(false)
        } else {
          this.$q.notify({
            message: 'Preencha todos os campos para salvar',
            type: 'negative'
          })
        }
      })
    },
    close () {
      this.showDialog = !this.showDialog
      this.cleanFields()
    },
    setNameState (state) {
      GetDatas('localidades/estados/' + state).then(res => {
        this.stateSelected = res.data
      }).catch(err => {
        console.log(err)
      })
    },
    insertDataField (user, title) {
      this.id = user.id
      this.cepSelected = user.cep
      this.streetSelected = user.street
      this.complementSelected = user.complement
      this.name = user.name
      this.email = user.email
      this.numberSelected = user.number
      this.birthDate = new Date(user.birthDate).toLocaleDateString('pt-BR')
      this.countrySelected = user.country.toUpperCase()
      this.setNameState(user.state)
      this.citySelected = user.city.toUpperCase()
      this.districtSelected = user.district
      this.isPis = user.isPis === 'S' ? true : false
      this.cpfPis = user.cpfPis
      this.titleDialog = title.toUpperCase()
      this.masked(this.isPis)
    },
    show (data, title, isUpdate) {
      this.showDialog = !this.showDialog
      if (data !== null) {
        this.isUpdate = isUpdate
        this.insertDataField(data, title)
      } else {
        this.titleDialog = title.toUpperCase()
        this.isUpdate = isUpdate
        this.isPis = true
      }
    },
    filterCountry (val, update) {
      if (val === '') {
        update(() => {
          this.countryFilter = this.countryData
        })
        return
      }

      update(() => {
        const needle = val.toUpperCase()
        this.countryFilter = this.countryData.filter(f => f.indexOf(needle) > -1)
      })
    },
    filterCity (val, update) {
      if (val === '') {
        update(() => {
          this.cityFilter = this.cityData
        })
        return
      }

      update(() => {
        const needle = val.toUpperCase()
        this.cityFilter = this.cityData.filter(f => f.indexOf(needle) > -1)
      })
    },
    filterState (val, update) {
      if (val === '') {
        update(() => {
          this.stateFilter = this.stateData
        })
        return
      }

      update(() => {
        const needle = val.toUpperCase()
        this.stateFilter = this.stateData.filter(f => f.nome.indexOf(needle) > -1)
      })
    },
    getCoutries () {
      var array = []
      GetDatas('paises').then(res => {
        for (var i = 0; i < res.data.length; i++) {
          this.countryData.push(res.data[i].nome.abreviado.toUpperCase())
        }
        this.countryData = this.countryData.sort(function (a, b) {
          if (a < b) {
            return -1;
          }
          if (a > b) {
            return 1;
          }
          return 0;
        })
      }).catch(err => {
        console.log('NÃO FOI POSSÍVEL OBTER PAÍSES')
        console.log(err)
      })
    },
    getStates () {
      GetDatas('localidades/estados').then(res => {
        for (var i = 0; i < res.data.length; i++) {
          var obj = Object()
          obj.sigla = res.data[i].sigla
          obj.nome = res.data[i].nome.toUpperCase()
          this.stateData.push(obj)
        }
        this.stateData = this.stateData.sort(function (a, b) {
          if (a.nome < b.nome) {
            return -1;
          }
          if (a.nome > b.nome) {
            return 1;
          }
          return 0;
        })
      }).catch(err => {
        console.log('NÃO FOI POSSÍVEL OBTER AS UFs')
        console.log(err)
      })
    },
    getCities (initials) {
      GetDatas(`localidades/estados/${initials}/municipios`).then(res => {
        for (var i = 0; i < res.data.length; i++) {
          this.cityData.push(res.data[i].nome.toUpperCase())
        }
        this.cityData = this.cityData.sort(function (a, b) {
          if (a < b) {
            return -1;
          }
          if (a > b) {
            return 1;
          }
          return 0;
        })
      }).catch(err => {
        console.log('NÃO FOI POSSÍVEL OBTER AS UFs')
        console.log(err)
      })
    },
    searchCep () {
      if (this.cepSelected.length === 10) {
        this.errorCEP = false
        var cep = this.cepSelected.replace('.', '').replace('-', '')

        Get('cep/' + cep).then(res => {
          if (res.status === 200 && res.data.cep !== (null || '')) {
            this.streetSelected = res.data.logradouro
            this.districtSelected = res.data.bairro
            this.complementSelected = res.data.complemento
          }
          if (res.status === 200 && res.data.erro === true) {
            console.log(res.data)
            this.$q.notify({
              message: 'CEP Inexistente, tente outro...',
              type: 'negative',
              icon: 'error',
              timeout: 2000
            })
          }
        }).catch(err => {
          console.log(err)
        })
      } else {
        this.errorCEP = true
        this.$q.notify({
          message: 'CEP Inválido',
          type: 'negative',
          icon: 'error',
          position: 'center',
          timeout: 2000
        })
      }
    },
    cleanFields () {
      this.id = ''
      this.name = ''
      this.email = ''
      this.birthDate = ''
      this.cpfPis = ''
      this.countrySelected = ''
      this.stateSelected = ''
      this.citySelected = ''
      this.streetSelected = ''
      this.numberSelected = ''
      this.districtSelected = ''
      this.complementSelected = ''
      this.cepSelected = ''
      this.isPis = false
      this.errorCEP = false
      this.errorName = false
      this.errorEmail = false
      this.errorBirthDate = false
      this.errorCpfPis = false
      this.errorCountrySelected = false
      this.errorStateSelected = false
      this.errorCitySelected = false
      this.errorStreetSelected = false
      this.errorNumberSelected = false
      this.errorDistrictSelected = false
      this.errorPassword = false
    },
    async saveUpdate () {
      this.validateData().then(async () => {
        var user = Object()

        user.Id = this.id
        user.CEP = this.cepSelected.replace('.', '').replace('-', '')
        user.City = this.citySelected.toUpperCase()
        user.State = this.stateSelected.sigla.toUpperCase()
        user.Country = this.countrySelected.toUpperCase()
        user.BirthDate = await ConvertStringToDatetime(this.birthDate)
        user.Email = this.email
        user.Complement = this.complementSelected
        user.IsPis = this.isPis
        user.CpfPis = this.cpfPis
        user.District = this.districtSelected
        user.Street = this.streetSelected
        user.Name = this.name
        user.Number = this.numberSelected

        this.isDisabled = true

        if (this.isUpdate === false) {
          user.PasswordString = this.passwordUser

          if (user.PasswordString === '' || user.PasswordString === null) {
            this.errorPassword = true
            this.$q.notify({
              message: 'Senha não inserida!!!',
              type: 'negative'
            })
          } else {
            this.postData(user)
          }
        } else {
          this.updateData(user)
        }

        this.isDisabled = false
      })
    },
    postData (user) {
      Post('user/new', user).then(res => {
        if (res.data.status === true) {
          this.$q.notify({
            message: res.data.msg,
            type: 'positive'
          })
        } else {
          this.$q.notify({
            message: res.data.msg,
            type: 'negative'
          })
        }
        this.showDialog = !this.showDialog
        this.cleanFields()
        this.$emit('reloadData')
      }).catch(err => {
        console.log(err)
        this.$q.notify({
          message: 'Erro ao criar usuário, tente novamente mais tarde',
          type: 'negative'
        })
      })
    },
    updateData (user) {
      Put('user/' + user.Id, user).then(res => {
        console.log(res.data)
        if (res.data.status === true) {
          this.$q.notify({
            message: res.data.msg,
            type: 'positive',
            timeout: 3000
          })
          this.showDialog = !this.showDialog
          this.cleanFields()
          this.$emit('reloadData')
        } else {
          this.$q.notify({
            message: res.data.msg,
            type: 'negative'
          })
        }
      }).catch(err => {
        console.log(err.message)
      })
    },
    masked (value) {
      if (value === true) {
        this.maskedCpfPis = '###.#####.##-#'
      } else {
        this.maskedCpfPis = '###.###.###-##'
      }
    }
  },
  watch: {
    countrySelected: function (val) {
      if (val.toLowerCase() === 'brasil') {
        this.getStates()
      }
    },
    stateSelected: function (val) {
      if (val.sigla !== '') {
        this.getCities(val.sigla)
      }
    },
    isPis: function (val) {
      this.masked(val)
    }
  },
  created () {
    this.getCoutries()
  },
}

</script>

<style>
  @media only screen and (min-width: 500px) {
    .inputClass {
      padding-left: 5px;
      padding-right: 5px;
    }
  }

  @media only screen and (max-width: 1023px) {
    .spaceField {
      margin-top: 17px;
    }
  }

  @media only screen and (max-width: 499px) {
    .inputClass {
      padding-left: 10px;
      padding-right: 10px;
    }
  }
</style>
