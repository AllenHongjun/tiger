<template>
  <div class="app-container">
    <p style="text-align: center; margin: 0 0 20px">User claim types to be included in tokens</p>
    <div style="text-align: center">
      <el-transfer
        v-model="value"
        style="text-align: left; display: inline-block"
        filterable
        :left-default-checked="[2, 3]"
        :right-default-checked="[1]"
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
export default {
  name: 'UserClaim',
  data() {
    const generateData = _ => {
      const data = []
      for (let i = 1; i <= 15; i++) {
        data.push({
          key: i,
          label: `备选项 ${i}`,
          disabled: i % 4 === 0
        })
      }
      return data
    }
    return {
      data: generateData(),
      value: [1],
      value4: [1],
      renderFunc(h, option) {
        return <span>{ option.key } - { option.label }</span>
      }
    }
  },
  methods: {
    handleChange(value, direction, movedKeys) {
      console.log(value, direction, movedKeys)
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
.transfer-footer {
  margin-left: 20px;
  padding: 6px 5px;
}
</style>

