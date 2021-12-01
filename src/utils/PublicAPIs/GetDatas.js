import axios from 'axios'
var protocolo = location.protocol

var url = protocolo + '//servicodados.ibge.gov.br'
if (protocolo === 'http:') {
  url = url + '/api/v1/'
} else if (protocolo === 'https:') {
  url = url + '/api/v1/'
}

export function prepareConexion () {
  return new Promise(resolve => {
    axios.defaults.baseURL = url
    //axios.defaults.headers.common.Authorization = 'Bearer ' + localStorage.getItem('token')
    resolve()
  })
}

export function GetDatas (rota) {
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
