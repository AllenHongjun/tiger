<template>
  <div class="app-container">
    <p style="text-align: center; margin: 0 0 20px">User claim types to be included in tokens</p>
    <div style="text-align: center">
      <el-transfer
        v-model="value"
        style="text-align: left; display: inline-block"
        filterable
        :render-content="renderFunc"
        :titles="['Source', 'Target']"
        :button-texts="['到左边', '到右边']"
        :format="{
          noChecked: '${total}',
          hasChecked: '${checked}/${total}'
        }"
        :data="data"
        @change="handleChange"
      >
        <el-button slot="left-footer" class="transfer-footer" size="small">操作</el-button>
        <el-button slot="right-footer" class="transfer-footer" size="small">操作</el-button>
      </el-transfer>
    </div>
  </div>
</template>

<script>
import {
  getClaimTypes

} from '@/api/system-manage/identity/claim-type'

export default {
  name: 'UserClaim',
  data() {
    return {
      data: [],
      value: [1],
      value4: [1],
      renderFunc(h, option) {
        return <span>{ option.label }</span>
      }
    }
  },
  created() {
    this.$nextTick(() => {
      this.fetchClaimTypes()
    })
  },
  methods: {
    fetchClaimTypes() {
      const input = {
        page: 1,
        limit: 999
      }
      getClaimTypes(input).then(response => {
        const data = []
        for (let i = 0; i < response.items.length; i++) {
          data.push({
            key: response.items[i].id,
            label: response.items[i].name,
            disabled: false
          })
        }
        this.data = data
      })
    },
    handleChange(value, direction, movedKeys) {
      // console.log('value', value, 'direction', direction, 'movedKeys', movedKeys)
      // TODO: 调用接口保存数据
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

<style lang="scss" scoped>
.line{
  text-align: center;
}

/* /deep/ 语法不可用 https://juejin.cn/post/7085915259541667847*/
::v-deep .el-transfer-panel{
  width: 275px;
}
 .transfer-footer {
  margin-left: 20px;
  padding: 6px 5px;
}
</style>

