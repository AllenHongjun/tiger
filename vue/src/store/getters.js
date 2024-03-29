const getters = {
  sidebar: state => state.app.sidebar,
  size: state => state.app.size,
  visitedViews: state => state.tagsView.visitedViews,
  cachedViews: state => state.tagsView.cachedViews,
  device: state => state.app.device,
  token: state => state.user.token,
  avatar: state => state.user.avatar,
  surname: state => state.user.surname,
  name: state => state.user.name,
  userName: state => state.user.userName,
  introduction: state => state.user.introduction,
  roles: state => state.user.roles,
  email: state => state.user.email,
  phoneNumber: state => state.user.phoneNumber,
  language: state => state.app.language,
  permission_routes: state => state.permission.routes,
  errorLogs: state => state.errorLog.logs,
  abpConfig: state => state.app.abpConfig,
  tenant: state => state.app.tenant

}
export default getters
