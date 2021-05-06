<template>
  <role-detail :is-edit="true" />
</template>

<script>
import roleDetail from './components/role_detail'

export default {
  name: 'EditRole',
  components: { roleDetail }
}
</script>

