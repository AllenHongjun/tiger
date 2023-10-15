import { onMounted, onUnmounted } from 'vue'
import {
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel
} from '@microsoft/signalr'
import { useUserStoreWithOut } from '/@/store/modules/user'

import mitt from '/@/utils/mitt'

export function useSignalR({
  serverUrl,
  autoStart = false,
  useAccessToken = true,
  automaticReconnect = true,
  nextRetryDelayInMilliseconds = 60000
}) {
  const emitter = mitt()
  let connection = null

  onMounted(() => {
    _initlizaConnection()
  })

  onUnmounted(() => {
    if (
      connection !== null &&
      connection.state === HubConnectionState.Connected
    ) {
      stop()
    }
  })

  function _initlizaConnection() {
    const httpOptions = {}
    if (useAccessToken) {
      const userStore = useUserStoreWithOut()
      const token = userStore.getToken
      httpOptions.accessTokenFactory = () =>
        token.startsWith('Bearer ') ? token.substring(7) : token
    }
    var connectionBuilder = new HubConnectionBuilder()
      .withUrl(serverUrl, httpOptions)
      .configureLogging(LogLevel.Warning)
    if (automaticReconnect) {
      connectionBuilder.withAutomaticReconnect({
        nextRetryDelayInMilliseconds: () => nextRetryDelayInMilliseconds
      })
    }
    connection = connectionBuilder.build()
    if (autoStart) {
      start()
    }
  }

  async function start() {
    if (connection == null) {
      return Promise.reject('unable to start, connection not initialized!')
    }
    emitter.emit('signalR:beforeStart')
    await connection.start()
    emitter.emit('signalR:onStart')
  }

  async function stop() {
    if (connection == null) {
      return Promise.reject('unable to stop, connection not initialized!')
    }
    emitter.emit('signalR:beforeStop')
    await connection.stop()
    emitter.emit('signalR:onStop')
  }

  function beforeStart(callback) {
    emitter.on('signalR:beforeStart', callback)
  }

  function onStart(callback) {
    emitter.on('signalR:onStart', callback)
  }

  function beforeStop(callback) {
    emitter.on('signalR:beforeStop', callback)
  }

  function onStop(callback) {
    emitter.on('signalR:onStop', callback)
  }

  function on(methodName, newMethod) {
    connection?.on(methodName, newMethod)
  }

  function off(methodName, method) {
    connection?.off(methodName, method)
  }

  function onclose(callback) {
    connection?.onclose(callback)
  }

  function send(methodName, ...args) {
    if (connection == null) {
      return Promise.reject(
        'unable to send message, connection not initialized!'
      )
    }
    return connection.send(methodName, ...args)
  }

  function invoke(methodName, ...args) {
    if (connection == null) {
      return Promise.reject(
        'unable to send message, connection not initialized!'
      )
    }
    return connection.invoke(methodName, ...args)
  }

  return {
    on,
    off,
    onclose,
    beforeStart,
    onStart,
    beforeStop,
    onStop,
    send,
    invoke,
    start,
    stop
  }
}
