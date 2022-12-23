<template>
<div class="app-container">
    <div>
        <el-row :gutter="20">

            <el-col :span="6" :xs="24">
                <user-card :user="user" />
            </el-col>

            <el-col :span="18" :xs="24">
                <el-card>
                    <el-tabs v-model="activeTab">

                        <el-tab-pane label="管理个人资料" name="PersonalSettings">
                            <PersonalSettings :user="user" />
                        </el-tab-pane>

                        <el-tab-pane label="修改密码" name="ChangePassword">
                            <changePassword />
                        </el-tab-pane>
                    </el-tabs>
                </el-card>
            </el-col>

        </el-row>
    </div>
</div>
</template>

<script>
import {
    mapGetters
} from 'vuex'
import UserCard from './components/UserCard'
import ChangePassword from './components/ChangePassword'
import PersonalSettings from './components/PersonalSettings'

export default {
    name: 'Profile',
    components: {
        UserCard,
        PersonalSettings,
        ChangePassword
    },
    data() {
        return {
            user: {},
            activeTab: 'PersonalSettings'
        }
    },
    computed: {
        ...mapGetters([
            'name',
            'email',
            'avatar',
            'roles',
            'userName',
            'phoneNumber',
            'introduction'
        ])
    },
    created() {
        this.getUser()
    },
    methods: {
        getUser() {
            console.log(this)
            this.user = {
                name: this.name,
                role: this.roles.join(' | '),
                email: this.email,
                avatar: this.avatar,
                userName: this.userName,
                phoneNumber: this.phoneNumber,
                introduction: this.introduction
            }
        }
    }
}
</script>
