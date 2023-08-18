<template>
  <el-dialog :title="$t('AbpFeatureManagement[\'Features\']')" :visible.sync="dialogFormVisible" top="5vh">
    <div>
      <el-tabs tab-position="left">
        <el-tab-pane v-for="(group,sIndex) in featureData" :key="group.groupName" :label="group.groupDisplayName">
          <el-tabs tab-position="top" style="min-height:480px;max-height:750px;overflow-y:scroll;">

            <el-card class="box-card">
              <div slot="header" class="clearfix">
                <span>{{ group.groupDisplayName }}</span>
              </div>
              <!-- 溢出隐藏优化 -->
              <div class="text item">
                <el-form :ref="group.name" label-position="top">
                  <el-form-item v-for="info in group.featureInfos" :key="info.name" :label="info.displayName">
                    <div v-if="info.valueType.name==='FreeTextStringValueType'">
                      <el-input :id="info.name" v-model="info.value" :name="info.formName" />
                    </div>
                    <div v-if="info.valueType.name==='ToggleStringValueType'">
                      <el-checkbox :id="info.name" v-model="info.value" :name="info.formName" :checked="info.value==='true' || info.value==='True'" />
                      <span style="margin-left:5px;">{{ info.description }}</span>
                    </div>
                    <div v-if="info.valueType.name==='SelectionStringValueType'">
                      <el-select :id="info.name" v-model="info.value" :name="info.formName">
                        <el-option v-for="item in info.valueType.itemSource.items" :key="item.value" :label="item.displayText.name" :value="item.value" />
                      </el-select>
                      <p>{{ info.description }}</p>
                    </div>
                  </el-form-item>
                  <el-button type="primary" @click="updateFeatureValues(sIndex)">{{ $t('AbpUi.Save') }}</el-button>
                  <el-button @click="dialogFormVisible = false">
                    {{ $t("AbpFeatureManagement['Cancel']") }}
                  </el-button>
                </el-form>
              </div>
            </el-card>
          </el-tabs>
        </el-tab-pane>
      </el-tabs>
      <div slot="footer" class="dialog-footer" />

    </div>

  </el-dialog>
</template>

<script>
import {
  getFeatures,
  updateFeatures
} from '@/api/sass/features'

import {
  getSettingValues,
  setSettingValues,
  resetSettingValues
} from '@/api/system-manage/setting'

export default {
  name: 'FeatureDialog',
  props: {
    providerName: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      features: '',
      temp: {},
      dialogFormVisible: false,
      rules: {},
      featuresQuery: {
        providerKey: '',
        providerName: ''
      },
      featureData: [],
      settingData: [],
      formRefs: []
    }
  },
  created() {
    this.featuresQuery.providerName = this.providerName
  },
  methods: {
    resetTemp() {
      this.temp = {}
    },
    handleUpdate(row) {
      this.resetTemp()
      this.featuresQuery.providerKey = row.id
      this.getGroupFeatures()
      this.dialogFormVisible = true
      // 获取到数据。想数据展示为需要的格式
    },
    getGroupFeatures() {
      getFeatures(this.featuresQuery).then(response => {
        // push 之前先重置一下数据
        this.featureData = []
        for (const featureGroup of response.groups) {
          const featureInfo = []
          for (const info of featureGroup.features) {
            if (!info.valueType) continue// 后端数据格式错误,没有valueType不赋值

            const formRefsKey = featureGroup.name
            info.formName = 'Feature_' + info.name.replace(/\./g, '_')
            featureInfo.push(info)
            if (this.formRefs.indexOf(formRefsKey) <= 0) {
              this.formRefs.push(formRefsKey)
            }
          }

          this.featureData.push({
            groupName: featureGroup.name,
            groupDisplayName: featureGroup.displayName,
            featureInfos: {
              ...featureInfo
            }
          })
        }
      })
    },
    updateFeatureValues(index) {
      var featureInfos = this.featureData[index].featureInfos
      var features = []
      for (const key in featureInfos) {
        var item = featureInfos[key]
        var feature = { 'name': item.name, 'value': item.value }
        features.push(feature)
      }
      console.log('features', features)
      var req = { 'features': features }

      updateFeatures(this.featuresQuery, req).then(() => {
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          const tempData = {
            features: []
          }
          this.features.map(feature => {
            if (feature.valueType.name === 'ToggleStringValueType') {
              tempData.features.push({
                name: feature.name,
                value: !!this.temp[feature.name]
              })
            } else if (feature.valueType.name === 'FreeTextStringValueType') {
              tempData.features.push({
                name: feature.name,
                value: this.temp[feature.name]
              })
            }
          })

          updateFeatures(this.featuresQuery, tempData).then(() => {
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("HelloAbp['Success']"),
              message: this.$i18n.t("HelloAbp['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    getGroupSettingDefinitions() {
      getSettingValues().then(setting => {
        for (const s of setting) {
          const settingInfo = []
          for (const info of s.settingInfos) {
            const group2 = info.properties.Group2
            const formRefsKey = s.groupName + '.' + group2
            settingInfo[group2] = settingInfo[group2] || []
            info.formName = 'Setting_' + info.name.replace(/\./g, '_')
            settingInfo[group2].push(info)
            if (this.formRefs.indexOf(formRefsKey) <= 0) {
              this.formRefs.push(formRefsKey)
            }
          }

          this.settingData.push({
            groupName: s.groupName,
            groupDisplayName: s.groupDisplayName,
            settingInfos: {
              ...settingInfo
            }
          })
        }
      })
    },
    updateSettingValues(formName, index) {
      const obj = {}
      for (const from of this.settingData[index].settingInfos[formName]) {
        const {
          formName,
          value
        } = from
        obj[formName] = value
      }
      setSettingValues(obj).then(() => {
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    }
  }
}
</script>
