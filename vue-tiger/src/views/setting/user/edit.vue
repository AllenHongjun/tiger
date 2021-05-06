<template>
  <div class="app-container">
    <el-form
      ref="ruleForm"
      :model="ruleForm"
      :rules="rules"
      label-width="100px"
      class="demo-ruleForm"
    >
      <el-row>
        <el-col :span="12">
          <div class="grid-content bg-purple">
            <el-form-item label="用户名" prop="userName">
              <el-input v-model="ruleForm.userName" />
            </el-form-item></div
        ></el-col>
        <el-col :span="12"
          ><div class="grid-content bg-purple-light">
            <el-form-item label="姓名" prop="name">
              <el-input v-model="ruleForm.name" />
            </el-form-item></div
        ></el-col>
      </el-row>

      <el-row>
        <el-col :span="12">
          <div class="grid-content bg-purple">
            <el-form-item label="邮箱" prop="email">
              <el-input v-model="ruleForm.email" />
            </el-form-item></div
        ></el-col>
        <el-col :span="12"
          ><div class="grid-content bg-purple-light">
            <el-form-item label="手机号" prop="phoneNumber">
              <el-input v-model="ruleForm.phoneNumber" />
            </el-form-item></div
        ></el-col>
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
            </el-form-item></div
        ></el-col>
        <el-col :span="12"
          ><div class="grid-content bg-purple-light">
            <el-form-item label="修改时间" prop="creationTime">
              <el-input v-model="ruleForm.creationTime" />
            </el-form-item></div
        ></el-col>
      </el-row>

      <!-- <el-form-item label="活动区域" prop="region">
        <el-select v-model="ruleForm.region" placeholder="请选择活动区域">
          <el-option label="区域一" value="shanghai"></el-option>
          <el-option label="区域二" value="beijing"></el-option>
        </el-select>
      </el-form-item> -->
      <!-- <el-form-item label="活动时间" required>
        <el-col :span="11">
          <el-form-item prop="date1">
            <el-date-picker
              type="date"
              placeholder="选择日期"
              v-model="ruleForm.date1"
              style="width: 100%"
            ></el-date-picker>
          </el-form-item>
        </el-col>
        <el-col class="line" :span="2">-</el-col>
        <el-col :span="11">
          <el-form-item prop="date2">
            <el-time-picker
              placeholder="选择时间"
              v-model="ruleForm.date2"
              style="width: 100%"
            ></el-time-picker>
          </el-form-item>
        </el-col>
      </el-form-item>
      <el-form-item label="即时配送" prop="delivery">
        <el-switch v-model="ruleForm.delivery"></el-switch>
      </el-form-item>
      <el-form-item label="活动性质" prop="type">
        <el-checkbox-group v-model="ruleForm.type">
          <el-checkbox label="美食/餐厅线上活动" name="type"></el-checkbox>
          <el-checkbox label="地推活动" name="type"></el-checkbox>
          <el-checkbox label="线下主题活动" name="type"></el-checkbox>
          <el-checkbox label="单纯品牌曝光" name="type"></el-checkbox>
        </el-checkbox-group>
      </el-form-item>
      <el-form-item label="特殊资源" prop="resource">
        <el-radio-group v-model="ruleForm.resource">
          <el-radio label="线上品牌商赞助"></el-radio>
          <el-radio label="线下场地免费"></el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="活动形式" prop="desc">
        <el-input type="textarea" v-model="ruleForm.desc"></el-input>
      </el-form-item> -->

      <el-form-item>
        <el-button type="primary" @click="submitForm('ruleForm')"
          >立即创建</el-button
        >
        <el-button @click="resetForm('ruleForm')">重置</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { getUser } from "@/api/user";
export default {
  props: {
    isEdit: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      ruleForm: {
        name: "",
        userName: "",
        email: "",
        phoneNumber: "",
        lockoutEnabled: false,
        isDeleted: false,
        deletionTime: "",
        lastModificationTime: "",
        creationTime: "",
        id: "",
      },
      rules: {
        name: [
          { required: true, message: "请输入姓名", trigger: "blur" },
          { min: 2, max: 8, message: "长度在 2 到 8 个字符", trigger: "blur" },
        ],
        // region: [
        //   { required: true, message: "请选择活动区域", trigger: "change" },
        // ],
        // date1: [
        //   {
        //     type: "date",
        //     required: true,
        //     message: "请选择日期",
        //     trigger: "change",
        //   },
        // ],
        // date2: [
        //   {
        //     type: "date",
        //     required: true,
        //     message: "请选择时间",
        //     trigger: "change",
        //   },
        // ],
        // type: [
        //   {
        //     type: "array",
        //     required: true,
        //     message: "请至少选择一个活动性质",
        //     trigger: "change",
        //   },
        // ],
        // resource: [
        //   { required: true, message: "请选择活动资源", trigger: "change" },
        // ],
        // desc: [{ required: true, message: "请填写活动形式", trigger: "blur" }],
      },
    };
  },
  created() {
    const id = this.$route.params && this.$route.params.id;
    this.fetchData(id);
    if (this.isEdit) {
    }

    // Why need to make a copy of this.$route here?
    // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
    // https://github.com/PanJiaChen/vue-element-admin/issues/1221
    this.tempRoute = Object.assign({}, this.$route);
  },
  methods: {
    fetchData(id) {
      // _self = this
      console.log(id);
      // this.$store.dispatch('user/login', id).then(() => {
      //   // console.log('this.redirect:' + this.redirect)

      //   // this.$router.push({ path: this.redirect || '/', query: this.otherQuery })
      //   // console.log('login')
      //   // this.loading = false
      // }).catch(() => {
      //   console.log('login err')
      //   // this.loading = false
      // })

      getUser(id)
        .then((response) => {
          console.log(response);
          console.log(response.userName);
          // _self.userName = response.userName
          // _self.email = response.email
          this.ruleForm.userName = response.userName;
          this.ruleForm.name = response.name;
          this.ruleForm.email = response.email;
          this.ruleForm.phoneNumber = response.phoneNumber;
          this.ruleForm.isDeleted = response.isDeleted;
          this.ruleForm.lockoutEnabled = response.lockoutEnabled;
          this.ruleForm.creationTime = response.creationTime;
          this.ruleForm.lastModificationTime = response.lastModificationTime;

          // // just for test
          // this.postForm.title += `   Article Id:${this.postForm.id}`
          // this.postForm.content_short += `   Article Id:${this.postForm.id}`

          // // set tagsview title
          // this.setTagsViewTitle()

          // // set page title
          // this.setPageTitle()
        })
        .catch((err) => {
          console.log(err);
        });
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          alert("submit!");
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
  },
};
</script>
