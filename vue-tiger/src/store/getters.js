const getters = {
  sidebar: state => state.app.sidebar,
  device: state => state.app.device,
  token: state => state.user.token,
  avatar: state => state.user.avatar,
  name: state => state.user.name,
  language: state => state.app.language,
  abpConfig: state => state.app.abpConfig,
}
export default getters
