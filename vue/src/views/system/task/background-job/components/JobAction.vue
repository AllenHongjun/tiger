<template>
  <div class="app-container">
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>作业行为</span>
        <el-button style="float: right; padding: 3px 0" type="text">操作按钮</el-button>
      </div>
      <div>
        <el-dropdown split-button type="primary" @click="handleClick">
          更多菜单
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item>黄金糕</el-dropdown-item>
            <el-dropdown-item>狮子头</el-dropdown-item>
            <el-dropdown-item>螺蛳粉</el-dropdown-item>
            <el-dropdown-item>双皮奶</el-dropdown-item>
            <el-dropdown-item>蚵仔煎</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </div>
    </el-card>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>卡片名称</span>
        <el-button style="float: right; padding: 3px 0" type="text">操作按钮</el-button>
      </div>
      <div>
        <el-button class="el-icon-delete" type="danger">删除</el-button>
        <el-form :label-position="labelPosition" label-width="80px" :model="formLabelAlign">
          <el-form-item label="名称">
            <el-input v-model="formLabelAlign.name" />
          </el-form-item>
          <el-form-item label="活动区域">
            <el-input v-model="formLabelAlign.region" />
          </el-form-item>
          <el-form-item label="活动形式">
            <el-input v-model="formLabelAlign.type" />
          </el-form-item>
        </el-form>
      </div>
    </el-card>
  </div>
</template>

<script>
import {
  getBackgroundJobActions,
  getBackgroundJobAction,
  createBackgroundJobAction,
  updateBackgroundJobAction,
  deleteBackgroundJobAction,
  getBackgroundJobActionDefinitions
} from '@/api/system-manage/task/background-job-action'

export default {
  name: 'JobAction',
  props: {
    jobId: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      blank: {

      },
      actionDefinitions: {},
      labelPosition: 'right',
      formLabelAlign: {
        name: '',
        region: '',
        type: ''
      }
    }
  },
  // watch() {
  //   this.fetchJobActionDefinitions(0)
  // },
  created() {
    this.fetchJobActionDefinitions()
  },
  methods: {
    fetchJobActionDefinitions(type) {
      getBackgroundJobActionDefinitions({ type: type }).then((res) => {
        this.actionDefinitions = res.items
        // actionFormsRef.value = []
        this.fetchJobActions()
      })
    },
    fetchJobActions() {
      if (this.jobId) {
        getBackgroundJobAction(this.jobId).then((res) => {
          res.items.forEach((action) => {
            const actionDefinition = this.actionDefinitions.find(ad => ad.name === action.name)
            // actionFormsRef.value.push({
            //   key: buildUUID(),
            //   submiting: false,
            //   model: {
            //     id: action.id,
            //     jobId: action.jobId,
            //     name: action.name,
            //     isEnabled: action.isEnabled,
            //     paramters: action.paramters,
            //     displayName: actionDefinition?.displayName
            //   },
            //   schemas: getFormSchemas(actionDefinition)
            // })
          })
        })
      }
    },
    handleClick() {
      this.$alert('button click')
    },
    onSubmit() {
      this.$message('submit!')
    },
    onCancel() {
      this.$message({
        message: 'cancel!',
        type: 'warning'
      })
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

