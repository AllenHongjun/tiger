/**
 * 生成指定长度的随机密码
 * @param {密码长度} length
 */
export function generateRandomPassword(length = 8) {
  length = Number(length)
  // Limit length
  if (length < 6) {
    length = 6
  } else if (length > 16) {
    length = 16
  }
  const passwordArray = ['ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz', '1234567890', '!@#$%&*()']
  var password = []
  let n = 0
  for (let i = 0; i < length; i++) {
    // If password length less than 9, all value random
    if (password.length < (length - 4)) {
      // Get random passwordArray index
      const arrayRandom = Math.floor(Math.random() * 4)
      // Get password array value
      const passwordItem = passwordArray[arrayRandom]
      // Get password array value random index
      // Get random real value
      const item = passwordItem[Math.floor(Math.random() * passwordItem.length)]
      password.push(item)
    } else {
      // If password large then 9, lastest 4 password will push in according to the random password index
      // Get the array values sequentially
      const newItem = passwordArray[n]
      const lastItem = newItem[Math.floor(Math.random() * newItem.length)]
      // Get array splice index
      const spliceIndex = Math.floor(Math.random() * password.length)
      password.splice(spliceIndex, 0, lastItem)
      n++
    }
  }
  this.changePasswordForm.password = password.join('')
}
