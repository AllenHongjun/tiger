<template>
  <div class="app-container" style="text-align: center">
    <!--穿梭框组件使用 https://element.eleme.cn/#/zh-CN/component/transfer -->
    <!-- <el-button type="primary" style="display:block;">Protected Resources</el-button> -->
    <div>
      <el-transfer
        v-model="value"
        style="text-align: left; display: inline-block"
        filterable
        :filter-method="filterMethod"
        filter-placeholder="请输入城市拼音"
        :titles="['Available', 'Assigned']"
        :button-texts="['到左边', '到右边']"
        :data="data"
      />
    </div>

  </div>
</template>

<script>
export default {
  name: 'ClientIdentityResource',
  data() {
    const generateData = _ => {
      const data = []
      const cities = ['上海', '北京', '广州', '深圳', '南京', '西安', '成都']
      const pinyin = ['shanghai', 'beijing', 'guangzhou', 'shenzhen', 'nanjing', 'xian', 'chengdu']
      cities.forEach((city, index) => {
        data.push({
          label: city,
          key: index,
          pinyin: pinyin[index]
        })
      })
      return data
    }
    return {
      data: generateData(),
      value: [],
      filterMethod(query, item) {
        return item.pinyin.indexOf(query) > -1
      }
    }
  },
  methods: {
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

