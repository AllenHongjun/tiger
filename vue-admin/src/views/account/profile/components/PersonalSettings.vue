<template>
  <el-form ref="aForm" :model="userInfo" :rules="aRules" label-width="120px">
    <el-row>
      <el-col :span="12">
        <el-form-item :label="$t('AbpIdentity[\'DisplayName:Surname\']')" prop="surname">
          <el-input v-model.trim="userInfo.surname" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item :label="$t('AbpIdentity[\'DisplayName:Name\']')" prop="name">
          <el-input v-model.trim="userInfo.name" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-form-item :label="$t('AbpIdentity.UserName')" prop="userName">
      <el-input v-model.trim="userInfo.userName" />
    </el-form-item>
    <el-form-item :label="$t('AbpIdentity.EmailAddress')" prop="email">
      <el-input v-model.trim="userInfo.email">
        <el-button slot="append" type="primary" icon="el-icon-postcard" @click="handleSendEmailConfirmLink()">验证</el-button>
      </el-input>

    </el-form-item>
    <el-form-item :label="$t('AbpIdentity.PhoneNumber')" prop="phoneNumber">
      <el-input v-model.trim="userInfo.phoneNumber">
        <el-button slot="append" type="primary" icon="el-icon-postcard" @click="handleSendChangePhoneNumberCode()">验证</el-button>
      </el-input>
    </el-form-item>
    <el-form-item :label="$t('AbpAccount[\'Introduction\']')">
      <el-input v-model.trim="userInfo.extraProperties.Introduction" type="textarea" :rows="3" />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit">{{ $t('AbpUi.Submit') }}</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import {
  sendChangePhoneNumberCode,
  changePhoneNumber,
  sendEmailConfirmLink,
  confirmEmail
} from '@/api/profile'

import { validPhone } from '@/utils/validate'

export default {
  props: {
    user: {
      type: Object,
      default: () => {
        return {
          surname: '',
          name: '',
          userName: '',
          email: '',
          avatar: '',
          role: '',
          phoneNumber: '',
          introduction: ''
        }
      }
    }
  },
  data() {
    var checkPhone = (rule, value, callback) => {
      if (!value) {
        // 默认设置手机号可以为空
        return callback()
      }
      setTimeout(() => {
        // Number.isInteger是es6验证数字是否为整数的方法,但是我实际用的时候输入的数字总是识别成字符串
        // 所以我就在前面加了一个+实现隐式转换
        if (!Number.isInteger(+value)) {
          callback(new Error('请输入数字值'))
        } else {
          if (validPhone(value)) {
            callback()
          } else {
            callback(new Error(this.$i18n.t("AbpIdentity['The {0} field is not a valid phone number.']", [this.$i18n.t("AbpIdentity['PhoneNumber']")])))
          }
        }
      }, 100)
    }
    return {
      aRules: {
        surname: [{
          max: 64,
          message: this.$i18n.t("AbpIdentity['The field {0} must be a string with a maximum length of {1}.']", [this.$i18n.t("AbpIdentity['DisplayName:Surname']"), '64']),
          trigger: 'blur'
        }],
        name: [{
          max: 64,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['DisplayName:Name']"), '64']
          ),
          trigger: 'blur'
        }],
        userName: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [this.$i18n.t("AbpIdentity['UserName']")]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t("AbpIdentity['The field {0} must be a string with a maximum length of {1}.']", [this.$i18n.t("AbpIdentity['UserName']"), '256']),
            trigger: 'blur'
          }
        ],
        email: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [this.$i18n.t("AbpIdentity['EmailAddress']")]),
            trigger: 'blur'
          },
          {
            type: 'email',
            message: this.$i18n.t("AbpIdentity['The {0} field is not a valid e-mail address.']", [this.$i18n.t("AbpIdentity['EmailAddress']")]),
            trigger: ['blur', 'change']
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['EmailAddress']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        phoneNumber: [
          {
            validator: checkPhone,
            message: this.$i18n.t("AbpIdentity['The {0} field is not a valid phone number.']", [this.$i18n.t("AbpIdentity['PhoneNumber']")]),
            trigger: 'blur'
          },
          {
            max: 16,
            message: this.$i18n.t("AbpIdentity['The field {0} must be a string with a maximum length of {1}.']", [this.$i18n.t("AbpIdentity['PhoneNumber']"), '16']),
            trigger: 'blur'
          }
        ]
      },
      loading: false,
      userInfo: {
        userName: this.user.userName,
        email: this.user.email,
        surname: this.user.surname,
        name: this.user.name,
        phoneNumber: this.user.phoneNumber,
        extraProperties: {
          Avatar: this.user.avatar,
          Introduction: this.user.introduction
        }
      }
    }
  },
  mounted() {
    console.log(this.user)
  },
  methods: {
    handleSendEmailConfirmLink() {
      var req = {
        email: this.userInfo.email,
        appName: 'FrontWeb',
        returnUrl: '',
        returnUrlHash: ''
      }
      sendEmailConfirmLink(req).then(() => {
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    handleSendChangePhoneNumberCode() {
      var req = {
        newPhoneNumber: this.userInfo.phoneNumber
      }

      sendChangePhoneNumberCode(req).then(() => {
        this.$prompt('验证码已发送到你的电话号码. <br />请输入验证码以验证你的电话号码:', '提示', {
          dangerouslyUseHTMLString: true, // 配置允许文字换行
          confirmButtonText: this.$i18n.t('AbpUi.Save'),
          cancelButtonText: this.$i18n.t('AbpUi.Cancel'),
          inputPattern: /\S+/,
          inputErrorMessage: '验证码不能为空'
        }).then(({ value }) => {
          changePhoneNumber({ newPhoneNumber: this.userInfo.phoneNumber, code: value }).then((res) => {
            // TODO: 触发重新加载一下用户信息
            console.log('res', res)
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '取消输入'
          })
        })
      })
    },
    submit() {
      this.$refs.aForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$store.dispatch('user/setUserInfo', this.userInfo)
            .then((res) => {
              this.loading = false
              this.$notify({
                title: this.$i18n.t("TigerUi['Success']"),
                message: this.$i18n.t("TigerUi['SuccessMessage']"),
                type: 'success',
                duration: 2000
              })
            }).catch(() => {})
        } else {
          return false
        }
      })
    }
  }
}
</script>
<style scoped>

.el-input-group__append button.el-button {
  background-color: #fff;
}
</style>
