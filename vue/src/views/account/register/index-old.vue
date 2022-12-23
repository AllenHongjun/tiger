<template>
<div class="login-container">
    <el-form ref="loginForm" :model="loginForm" :rules="loginRules" class="login-form" auto-complete="on" label-position="left">

        <div class="title-container">
            <h3 class="title">账号注册</h3>
        </div>

        <el-form-item prop="username">
            <span class="svg-container">
                <svg-icon icon-class="user" />
            </span>
            <el-input ref="username" v-model="loginForm.userName" placeholder="用户名" name="username" type="text" tabindex="1" auto-complete="on" />
        </el-form-item>

        <el-form-item prop="emailAddress">
            <span class="svg-container">
                <svg-icon icon-class="email" />
            </span>
            <el-input ref="emailAddress" v-model="loginForm.emailAddress" placeholder="邮箱" name="emailAddress" type="text" tabindex="1" auto-complete="on" />
        </el-form-item>

        <!-- <el-form-item prop="appName">
            <span class="svg-container">
                <svg-icon icon-class="international" />
            </span>
            <el-input ref="appName" v-model="loginForm.appName" placeholder="应用名称" name="appName" type="text" tabindex="1" auto-complete="on" />
        </el-form-item> -->

        <el-form-item prop="password">
            <span class="svg-container">
                <svg-icon icon-class="password" />
            </span>
            <el-input :key="passwordType" ref="password" v-model="loginForm.password" :type="passwordType" placeholder="Password" name="password" tabindex="2" auto-complete="on" @keyup.enter.native="handleLogin" />
            <span class="show-pwd" @click="showPwd">
                <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
            </span>
        </el-form-item>

        <el-button :loading="loading" type="primary" style="width:100%;margin-bottom:30px;" @click.native.prevent="handleLogin">注册</el-button>

        <el-row>
            <el-col :span="12">
                <el-link href="#/login" type="primary">登 录</el-link>
            </el-col>
        </el-row>
        <!-- <div class="tips">
        <span style="margin-right:20px;">用户名: admin</span>
        <span> 密码: 1q2w3E*</span>
      </div> -->

    </el-form>

    <el-dialog title="切换租户" :visible.sync="dialogVisible" width="30%">
        <el-form label-width="80px" label-position="top">
            <el-form-item label="名称">
                <el-input v-model="tenant"></el-input>
            </el-form-item>
            <span>将名称留空以切换到宿主端</span>
        </el-form>

        <span slot="footer" class="dialog-footer">
            <el-button @click="dialogVisible = false">取 消</el-button>
            <el-button type="primary" @click="handleSwichTenant">保 存</el-button>
        </span>
    </el-dialog>

</div>
</template>

<script>
import {
    validUsername
} from '@/utils/validate'
import {
    getApplicationConfiguration,
    getTenantByName
} from '@/api/user'

export default {
    name: 'Register',
    data() {
        const validateUsername = (rule, value, callback) => {
            if (!validUsername(value)) {
                callback(new Error('Please enter the correct user name'))
            } else {
                callback()
            }
        }
        const validatePassword = (rule, value, callback) => {
            if (value.length < 6) {
                callback(new Error('The password can not be less than 6 digits'))
            } else {
                callback()
            }
        }
        return {
            dialogVisible: false,
            loginForm: {
                userName: '',
                emailAddress: '',
                password: '1q2w3E*',
                appName: '',
            },
            loginRules: {
                username: [{
                    required: true,
                    trigger: 'blur',
                    validator: validateUsername
                }],
                password: [{
                    required: true,
                    trigger: 'blur',
                    validator: validatePassword
                }]
            },
            loading: false,
            passwordType: 'password',
            redirect: undefined,
            tenant: '',
        }
    },
    watch: {
        $route: {
            handler: function (route) {
                this.redirect = route.query && route.query.redirect
            },
            immediate: true
        }
    },
    methods: {
        showPwd() {
            if (this.passwordType === 'password') {
                this.passwordType = ''
            } else {
                this.passwordType = 'password'
            }
            this.$nextTick(() => {
                this.$refs.password.focus()
            })
        },

        handleLogin() {
            return;
            this.$refs.loginForm.validate(valid => {
                if (valid) {
                    this.loading = true
                    getApplicationConfiguration(this.tenant).then((response) => {
                        console.log(response)

                    });
                    // return;

                    this.$store.dispatch('user/login', this.loginForm).then(() => {
                        console.log('this.redirect:' + this.redirect)

                        this.$router.push({
                            path: this.redirect || '/',
                            query: this.otherQuery
                        })
                        console.log('login')
                        this.loading = false
                    }).catch(() => {
                        console.log('login err')
                        this.loading = false
                    })

                } else {
                    console.log('error submit!!')
                    return false
                }
            })
        },

        handleClose(done) {
            this.$confirm('确认关闭？')
                .then(_ => {
                    done();
                })
                .catch(_ => {});
        },
        handleSwichTenant() {
            this.tenant = this.tenant;
            getTenantByName(this.tenant).then((response) => {
                if (response.success) {
                    this.dialogVisible = false;
                    this.$notify({
                        title: "成功",
                        message: "获取成功",
                        type: "success",
                        duration: 2000,
                    });
                } else {
                    this.dialogVisible = true;
                    this.$notify({
                        title: "失败",
                        message: "租户信息获取失败",
                        type: "fail",
                        duration: 2000,
                    });
                }

            });

        }
    }
}
</script>

<style lang="scss">
/* 修复input 背景不协调 和光标变色 */
/* Detail see https://github.com/PanJiaChen/vue-element-admin/pull/927 */

$bg:#283443;
$light_gray:#fff;
$cursor: #fff;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
    .login-container .login-form .el-input input {
        color: $cursor;
    }
}

/* reset element-ui css */
.login-form {
    .el-input {
        display: inline-block;
        height: 47px;
        width: 85%;

        input {
            background: transparent;
            border: 0px;
            -webkit-appearance: none;
            border-radius: 0px;
            padding: 12px 5px 12px 15px;
            color: $light_gray;
            height: 47px;
            caret-color: $cursor;

            &:-webkit-autofill {
                box-shadow: 0 0 0px 1000px $bg inset !important;
                -webkit-text-fill-color: $cursor !important;
            }
        }
    }

    .el-form-item {
        border: 1px solid rgba(255, 255, 255, 0.1);
        background: rgba(0, 0, 0, 0.1);
        border-radius: 5px;
        color: #454545;
    }
}
</style>

<style lang="scss" scoped>
$bg:#2d3a4b;
$dark_gray:#889aa4;
$light_gray:#eee;

.login-container {
    min-height: 100%;
    width: 100%;
    background-color: $bg;
    overflow: hidden;

    .login-form {
        position: relative;
        width: 520px;
        max-width: 100%;
        padding: 160px 35px 0;
        margin: 0 auto;
        overflow: hidden;
    }

    .tips {
        font-size: 14px;
        color: #fff;
        margin-bottom: 10px;

        span {
            &:first-of-type {
                margin-right: 16px;
            }
        }
    }

    .svg-container {
        padding: 6px 5px 6px 15px;
        color: $dark_gray;
        vertical-align: middle;
        width: 30px;
        display: inline-block;
    }

    .title-container {
        position: relative;

        .title {
            font-size: 26px;
            color: $light_gray;
            margin: 0px auto 40px auto;
            text-align: center;
            font-weight: bold;
        }
    }

    .show-pwd {
        position: absolute;
        right: 10px;
        top: 7px;
        font-size: 16px;
        color: $dark_gray;
        cursor: pointer;
        user-select: none;
    }

    .switchBth {
        position: absolute;
        right: 10px;
        top: 7px;
        font-size: 16px;
    }

    .tenantInput {
        background-color: #2d3a4b;
        line-height: 40px;
        // font-size:20px;
    }
}
</style>
