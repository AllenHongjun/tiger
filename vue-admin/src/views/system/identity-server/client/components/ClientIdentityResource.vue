<template>
  <div class="app-container" style="text-align: center">
    <!--穿梭框组件使用 https://element.eleme.cn/#/zh-CN/component/transfer -->
    <!-- <el-button type="primary" style="display:block;">Protected Resources</el-button> -->
    <div>
      <el-transfer
        v-model="value"
        style="text-align: left; display: inline-block"
        filterable
        :render-content="renderFunc"
        filter-placeholder="请输入"
        :titles="['Available', 'Assigned']"
        :button-texts="['到左边', '到右边']"
        :data="data"
        @change="handleChange"
      />
    </div>

  </div>
</template>

<script>
import {
  getIdentityResources
} from '@/api/system-manage/identity-server/identity-resource'

export default {
  name: 'ClientIdentityResource',
  props: {
    identityResources: {
      type: Array,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return []
      }
    }
  },
  data() {
    return {
      data: [],
      value: [],
      renderFunc(h, option) {
        return <span>{ option.label }</span>
      }

    }
  },
  created() {
    this.$nextTick(() => {
      this.fetchIdentityResources()
      // console.log('this.identityResources', this.identityResources)
      const data = this.identityResources
      const scopes = []
      for (const item of data) {
        scopes.push(item.scope)
      }
      // console.log(scopes)
      this.value = scopes
    })
  },
  methods: {
    fetchIdentityResources() {
      const input = {
        page: 1,
        limit: 999
      }
      getIdentityResources(input).then(response => {
        const data = []
        for (let i = 0; i < response.items.length; i++) {
          data.push({
            key: response.items[i].name,
            label: response.items[i].name,
            disabled: false
          })
        }
        this.data = data
      })
    },
    handleChange(value, direction, movedKeys) {
      const data = []
      for (let i = 0; i < value.length; i++) {
        data.push({
          scope: value[i]
        })
      }
      console.log('allowdScope', data)
      // 设置身份资源的事件
      this.$emit('set-identity-resources', data)
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

