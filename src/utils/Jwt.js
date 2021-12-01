export function DecodeJWT () {
  return new Promise(resolve => {
    var token = localStorage.getItem('token')
    resolve(atob(token.split('.')[1]))
  })
}
  