import axios from 'axios'

var url = 'https://desafio-pontotel-backend.herokuapp.com/api/v1/'

export function prepareConexion () {
  return new Promise(resolve => {
    axios.defaults.baseURL = url
    axios.defaults.headers.post['Content-Type'] = 'application/json'
    axios.defaults.headers.put['Content-Type'] = 'application/json'
    axios.defaults.headers.patch['Content-Type'] = 'application/json'
    axios.defaults.headers.common.Authorization = 'Bearer ' + localStorage.getItem('token')
    resolve()
  })
}

export function Post (rota, objeto) {
  return new Promise((resolve, reject) => {
    prepareConexion().then(() => {
      var json = JSON.stringify(objeto)
      axios.post(url + rota, json).then((result) => {
        resolve(result)
      }).catch((err) => {
        reject(err)
      })
    })
  })
}

export function Put (rota, objeto) {
  return new Promise((resolve, reject) => {
    prepareConexion().then(() => {
      var json = JSON.stringify(objeto)
      axios.put(url + rota, json).then((result) => {
        resolve(result)
      }).catch((err) => {
        reject(err)
      })
    })
  })
}

export function Get (rota) {
  return new Promise((resolve, reject) => {
    prepareConexion().then(() => {
      axios.get(url + rota).then((result) => {
        resolve(result)
      }).catch((err) => {
        reject(err)
      })
    })
  })
}
