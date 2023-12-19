export const TokenType = {
  Cookie: 0,
  Url: 1,
  NewUrl: 2
}

export const TokenTypeMap = {
  [TokenType.Cookie]: '旧版Cookie',
  [TokenType.Url]: '旧版Url',
  [TokenType.NewUrl]: '新版Url'
}
