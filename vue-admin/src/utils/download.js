import moment from 'moment'

/**
 * 下载导出Excel
 * @param {*} blobParts
 * @param {*} name
 */
export function downloadByData(blobParts, name) {
  const fileName = moment(new Date().getTime()).format('YYYYMMDDHHmmss') + '-' + name
  // const blob = new Blob([blobParts], { type: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document;charset=utf-8' })
  const blob = new Blob([blobParts])
  // for IE
  if (window.navigator && window.navigator.msSaveOrOpenBlob) {
    window.navigator.msSaveOrOpenBlob(blob, fileName)
  } else {
    console.log('chrome go here ')
    const downloadElement = document.createElement('a')
    const href = window.URL.createObjectURL(blob) // 创建下载的链接
    downloadElement.href = href
    downloadElement.download = fileName // 下载后文件名
    document.body.appendChild(downloadElement)
    downloadElement.click() // 点击下载
    document.body.removeChild(downloadElement) // 下载完成移除元素
    window.URL.revokeObjectURL(href) // 释放掉blob对象
  }
}

export function getFileName(headers) {
  var fileName = headers['content-disposition'].split(';')[1].split('filename=')[1]
  var fileNameUnicode = headers['content-disposition'].split('filename*=')[1]
  if (fileNameUnicode) {
    // 当存在 filename* 时，取filename* 并进行解码（为了解决中文乱码问题）
    fileName = decodeURIComponent(fileNameUnicode.split("''")[1])
  }
  return fileName
}
