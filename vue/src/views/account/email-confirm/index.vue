<template>
  <div class="app-container">
    <div class="title-container">
      <h3 class="title">
        {{ $t('AbpAccount.EmailConfirmation') }}
        <lang-select class="set-language" />
      </h3>
    </div>

    <el-form ref="dataForm" class="data-form" autocomplete="on" label-position="left">
      <p v-if="hasError">{{ $t('AbpAccount.InvalidToken') }}</p>
      <p v-else>{{ $t('AbpAccount.EmailConfirmSuccessMessage') }}</p>
      <el-button :loading="loading" type="primary" style="width:100%;margin-bottom:30px;" @click.native.prevent="navitoLogin()">
        {{ $t('AbpAccount.LoginApplication') }}
      </el-button>
    </el-form>

  </div>
</template>

<script>
import {
  confirmEmail
} from '@/api/profile'

import LangSelect from '@/components/LangSelect/index.vue'

export default {
  name: 'Register',
  components: {
    LangSelect
  },
  data() {
    return {
      dataForm: {
        userId: '',
        confirmToken: '',
        returnUrl: '',
        returnUrlHash: ''
      },

      loading: false,
      hasError: false
    }
  },
  mounted() {},
  destroyed() { },
  created() {
    var query = this.$route.query
    if (query) {
      this.userId = query.userId
      this.confirmToken = query.confirmToken
      this.returnUrl = query.returnUrl
      this.returnUrlHash = query.returnUrlHash
    }
    this.handleConfirmEmail()
  },
  methods: {
    handleConfirmEmail() {
      var req = {
        userId: this.userId,
        confirmToken: this.confirmToken,
        returnUrl: this.returnUrl,
        returnUrlHash: this.returnUrlHash
      }
      confirmEmail(req)
        .then(res => {
          console.log(res, 'res')
        })
        .catch((error) => {
          this.hasError = true
          console.log(error, 'error')
        })
    },
    navitoLogin() {
      this.$router.push({
        path: '/login'
      })
    }

  }
}
</script>

<style lang="scss">
@import "src/views/account/styles/account-global.scss";
</style>

<style lang="scss" scoped>
@import "src/views/account/styles/account.scss";
</style>
