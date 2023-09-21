import defaultSettings from '@/settings'
import i18n from '@/lang'

const title = defaultSettings.title || 'Tiger'

export default function getPageTitle(key) {
  const hasKey = i18n.te(key)
  if (hasKey) {
    const pageTitle = i18n.t(key)
    return `${pageTitle} - ${title}`
  }
  return `${title}`
}
