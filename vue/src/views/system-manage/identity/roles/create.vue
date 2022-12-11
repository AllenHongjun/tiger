<template>
  <role-detail :is-edit="false" />
</template>

<script>
import RoleDetail from './components/role_detail'

export default {
  name: 'CreateRole',
  components: { RoleDetail }
}
</script>

