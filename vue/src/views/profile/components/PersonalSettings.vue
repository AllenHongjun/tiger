<template>
<el-form ref="aForm" :model="userInfo" :rules="aRules">
    <el-form-item label="姓" prop="surname">
        <el-input v-model.trim="userInfo.surname" />
    </el-form-item>
    <el-form-item label="名" prop="name">
        <el-input v-model.trim="userInfo.name" />
    </el-form-item>
    <el-form-item label="邮箱" prop="email">
        <el-input v-model.trim="userInfo.email" />
    </el-form-item>
    <el-form-item label="用户名" prop="userName">
        <el-input v-model.trim="userInfo.userName" />
    </el-form-item>
    <el-form-item label="手机号" prop="phoneNumber">
        <el-input v-model.trim="userInfo.phoneNumber" />
    </el-form-item>
    <!-- <el-form-item label="个人介绍">
        <el-input v-model.trim="userInfo.extraProperties.Introduction" type="textarea" :rows="2" placeholder="请输入个人介绍" />
    </el-form-item> -->
    <el-form-item>
        <el-button type="primary" @click="submit">提交</el-button>
    </el-form-item>
</el-form>
</template>

<script>
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
        return {
            aRules: {
                email: [{
                    required: true,
                    message: '邮箱不能为空',
                    trigger: ['blur', 'change']
                }],
                userName: [{
                    required: true,
                    message: '用户名不能为空',
                    trigger: ['blur', 'change']
                }],
                phoneNumber: [{
                    required: true,
                    message: '手机号不能为空',
                    trigger: ['blur', 'change']
                }]
            },
            loading: false,
            userInfo: {
                userName: this.user.userName,
                email: this.user.email,
                surname:this.user.surname,
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
        submit() {
            this.$refs.aForm.validate(valid => {
                if (valid) {
                    this.loading = true
                    this.$store.dispatch('user/setUserInfo', this.userInfo)
                        .then((res) => {
                            this.loading = false
                            this.$notify({
                                title: '成功',
                                message: '修改成功',
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
