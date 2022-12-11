<template>
<div class="app-container">
    <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="100px" class="demo-ruleForm">
        <el-row>
            <el-col :span="12">
                <div class="grid-content bg-purple">
                    <el-form-item label="用户名" prop="userName">
                        <el-input v-model="ruleForm.userName" />
                    </el-form-item>
                </div>
            </el-col>
            <el-col :span="12">
                <div class="grid-content bg-purple-light">
                    <el-form-item label="姓名" prop="name">
                        <el-input v-model="ruleForm.name" />
                    </el-form-item>
                </div>
            </el-col>
        </el-row>

        <el-row>
            <el-col :span="12">
                <div class="grid-content bg-purple">
                    <el-form-item label="密码" prop="password">
                        <el-input v-model="ruleForm.password" show-password />
                    </el-form-item>
                </div>
            </el-col>

        </el-row>

        <el-row>
            <el-col :span="12">
                <div class="grid-content bg-purple">
                    <el-form-item label="邮箱" prop="email">
                        <el-input v-model="ruleForm.email" />
                    </el-form-item>
                </div>
            </el-col>
            <el-col :span="12">
                <div class="grid-content bg-purple-light">
                    <el-form-item label="手机号" prop="phoneNumber">
                        <el-input v-model="ruleForm.phoneNumber" />
                    </el-form-item>
                </div>
            </el-col>
        </el-row>
        <el-form-item label="是否锁定" prop="resource">
            <el-radio-group v-model="ruleForm.lockoutEnabled">
                <el-radio :label="true">是</el-radio>
                <el-radio :label="false">否</el-radio>
            </el-radio-group>
        </el-form-item>
        <el-form-item label="是否删除" prop="resource">
            <el-radio-group v-model="ruleForm.isDeleted">
                <el-radio :label="true">是</el-radio>
                <el-radio :label="false">否</el-radio>
            </el-radio-group>
        </el-form-item>
        <el-row>
            <el-col :span="12">
                <div class="grid-content bg-purple">
                    <el-form-item label="创建时间" prop="lastModificationTime">
                        <el-input v-model="ruleForm.lastModificationTime" />
                    </el-form-item>
                </div>
            </el-col>
            <el-col :span="12">
                <div class="grid-content bg-purple-light">
                    <el-form-item label="修改时间" prop="creationTime">
                        <el-input v-model="ruleForm.creationTime" />
                    </el-form-item>
                </div>
            </el-col>
        </el-row>

        <el-form-item>
            <el-button type="primary" @click="submitForm('ruleForm')">立即创建</el-button>
            <el-button @click="resetForm('ruleForm')">重置</el-button>
        </el-form-item>
    </el-form>
</div>
</template>

<script>
import {
    getUser,
    createUser
} from '@/api/user'

export default {
    props: {
        isEdit: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            ruleForm: {
                name: '',
                userName: '',
                password: '123qwE*',
                email: '',
                phoneNumber: '',
                lockoutEnabled: false,
                isDeleted: false,
                deletionTime: '',
                lastModificationTime: '',
                creationTime: '',
                id: '',
                roleNames: ['admin']
            },
            rules: {
                name: [{
                        required: true,
                        message: '请输入姓名',
                        trigger: 'blur'
                    },
                    {
                        min: 2,
                        max: 8,
                        message: '长度在 2 到 8 个字符',
                        trigger: 'blur'
                    }
                ]
            }
        }
    },
    created() {
        // Why need to make a copy of this.$route here?
        // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
        // https://github.com/PanJiaChen/vue-element-admin/issues/1221
        this.tempRoute = Object.assign({}, this.$route)
    },
    methods: {
        fetchData(id) {
            // _self = this
            // this.$store.dispatch('user/login', id).then(() => {
            //   // console.log('this.redirect:' + this.redirect)

            //   // this.$router.push({ path: this.redirect || '/', query: this.otherQuery })
            //   // console.log('login')
            //   // this.loading = false
            // }).catch(() => {
            //   console.log('login err')
            //   // this.loading = false
            // })

            getUser(id).then(response => {
                // _self.userName = response.userName
                // _self.email = response.email
                this.ruleForm.userName = response.userName
                this.ruleForm.name = response.name
                this.ruleForm.email = response.email
                this.ruleForm.phoneNumber = response.phoneNumber
                this.ruleForm.isDeleted = response.isDeleted
                this.ruleForm.lockoutEnabled = response.lockoutEnabled
                this.ruleForm.creationTime = response.creationTime
                this.ruleForm.lastModificationTime = response.lastModificationTime

                // // just for test
                // this.postForm.title += `   Article Id:${this.postForm.id}`
                // this.postForm.content_short += `   Article Id:${this.postForm.id}`

                // // set tagsview title
                // this.setTagsViewTitle()

                // // set page title
                // this.setPageTitle()
            }).catch(err => {
                console.log(err)
            })
        },
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    createUser(this.ruleForm).then((response) => {
                        console.log(response)
                        alert('添加成功')
                        // this.handleFilter()
                        // this.dialogFormVisible = false
                        // this.$notify({
                        //   title: this.$i18n.t("HelloAbp['Success']"),
                        //   message: this.$i18n.t("HelloAbp['SuccessMessage']"),
                        //   type: 'success',
                        //   duration: 2000
                        // })
                    })

                    alert('submit!')
                } else {
                    console.log('error submit!!')
                    return false
                }
            })
        },
        resetForm(formName) {
            this.$refs[formName].resetFields()
        }
    }
}
</script>
