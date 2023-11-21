<template>
  <div class="app-container">
    <el-row>
      <el-col :span="24">
        <div class="grid-content bg-purple-dark">
          <notice-bar />
        </div>
      </el-col>
    </el-row>

    <el-row :gutter="10">
      <el-col :span="8">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>缓存列表</span>
              <!-- <el-button style="float: right; padding: 3px 0" type="text">操作按钮</el-button> -->
            </div>
            <el-input v-model="filterText" placeholder="输入关键字进行过滤" />
            <!-- 节点过滤的树形组件 -->
            <el-tree ref="tree" class="filter-tree" :data="data" :props="defaultProps" default-expand-all :filter-node-method="filterNode" />
          </el-card>
        </div>
      </el-col>
      <el-col :span="16">
        <div class="grid-content bg-purple-light">
          <el-card class="box-card">
            <div slot="header" class="clearfix box-card-header">
              <span>缓存数据</span>
              <el-button icon="el-icon-delete" style="float: right; padding: 3px 0" type="text">删除缓存</el-button>
            </div>
            <!-- 展示JSON格式数据【vue-json-viewer】 -->
            <json-viewer :value="data" :expand-depth="10" boxed sort :expanded="false" />

          </el-card>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import NoticeBar from '@/components/Notice/NoticeBar'
import JsonViewer from 'vue-json-viewer'

export default {
  name: 'CacheManage',
  components: {
    NoticeBar,
    JsonViewer
  },
  data() {
    return {
      notice: '',
      filterText: '',
      data: [{
        id: 1,
        label: '一级 1',
        children: [{
          id: 4,
          label: '二级 1-1',
          children: [{
            id: 9,
            label: '三级 1-1-1'
          }, {
            id: 10,
            label: '三级 1-1-2'
          }]
        }]
      },
      {
        id: 2,
        label: '一级 2',
        children: [{
          id: 5,
          label: '二级 2-1'
        }, {
          id: 6,
          label: '二级 2-2'
        }]
      },
      {
        id: 3,
        label: '一级 3',
        children: [{
          id: 7,
          label: '二级 3-1'
        }, {
          id: 8,
          label: '二级 3-2'
        }]
      }
      ],
      defaultProps: {
        children: 'children',
        label: 'label'
      }
    }
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val)
    }
  },
  methods: {
    filterNode(value, data) {
      if (!value) return true
      return data.label.indexOf(value) !== -1
    },
    openTip() {},
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

<style lang="scss" scoped>
.el-row {
    margin-bottom: 20px;

    &:last-child {
        margin-bottom: 0;
    }
}

.el-col {
    border-radius: 4px;
}

.bg-purple-dark {
    background: #99a9bf;
}

.bg-purple {
    background: #d3dce6;
}

.bg-purple-light {
    background: #e5e9f2;
}

.grid-content {
    border-radius: 4px;
    min-height: 36px;
}

.row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
}

.text {
    font-size: 14px;
}
</style>
