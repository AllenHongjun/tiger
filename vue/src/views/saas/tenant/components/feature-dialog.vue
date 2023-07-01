<template>
  <el-dialog :title="$t('AbpFeatureManagement[\'Features\']')" :visible.sync="dialogFormVisible">
    <div>
      <el-tabs tab-position="left">
        <el-tab-pane v-for="(group,sIndex) in settingData" :key="group.groupName" :label="group.groupDisplayName">
          <el-tabs tab-position="top">
            <el-tab-pane v-for="card in group.settingInfos" :key="card[0].properties.Group2" :label="$t(`AbpSettingUi.${card[0].properties.Group2}`)">
              <el-card class="box-card">
                <div slot="header" class="clearfix">
                  <span>{{ $t(`AbpSettingUi.${card[0].properties.Group2}`) }}</span>
                </div>
                <div class="text item">
                  <el-form :ref="card[0].properties.Group2" label-position="top">
                    <el-form-item v-for="info in card" :key="info.name" :label="info.displayName">
                      <div v-if="info.properties.Type==='number' || info.properties.Type==='text'">
                        <el-input :id="info.name" v-model="info.value" :type="info.properties.Type" :name="info.formName" />
                        <span>{{ info.description }}</span>
                      </div>
                      <div v-if="info.properties.Type==='checkbox'">
                        <el-checkbox :id="info.name" v-model="info.value" :name="info.formName" :checked="info.value==='true' || info.value==='True'" />
                        <span style="margin-left:5px;">{{ info.description }}</span>
                      </div>
                      <div v-if="info.properties.Type==='select'">
                        <el-select :id="info.name" v-model="info.value" :name="info.formName">
                          <el-option v-for="item in info.properties.Options.split('|')" :key="item" :label="item" :value="item" />
                        </el-select>
                        <p>{{ info.description }}</p>
                      </div>
                    </el-form-item>

                    <div slot="footer" class="dialog-footer">
                      <el-button @click="dialogFormVisible = false">
                        {{ $t("AbpFeatureManagement['Cancel']") }}
                      </el-button>
                      <el-button v-if="features && features.length != 0" type="primary" @click="updateData()">
                        {{ $t("AbpFeatureManagement['Save']") }}
                      </el-button>
                    </div>
                    <el-button type="primary" @click="updateSettingValues(card[0].properties.Group2,sIndex)">{{ $t('AbpUi.Save') }}</el-button>
                  </el-form>
                </div>
              </el-card>
            </el-tab-pane>
          </el-tabs>
        </el-tab-pane>
      </el-tabs>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpFeatureManagement['Cancel']") }}
        </el-button>
        <el-button v-if="features && features.length != 0" type="primary" @click="updateData()">
          {{ $t("AbpFeatureManagement['Save']") }}
        </el-button>
      </div>

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
    this.getGroupSettingDefinitions()
  },
  methods: {
    resetTemp() {
      this.temp = {}
    },
    handleUpdate(row) {
      this.resetTemp()
      this.featuresQuery.providerKey = row.id
      this.dialogFormVisible = true
      // 获取到数据。想数据展示为需要的格式
      getFeatures(this.featuresQuery).then(response => {
        for (const feature of response) {
          const featureInfo = []
          for (const info of feature.featureInfo) {
            const group2 = info.properties.Group2
            const formRefsKey = feature.groupName + '.' + group2
            featureInfo[group2] = featureInfo[group2] || []
            info.formName = 'Feature_' + info.name.replace(/\./g, '_')
            featureInfo[group2].push(info)
            if (this.formRefs.indexOf(formRefsKey) <= 0) {
              this.formRefs.push(formRefsKey)
            }
          }

          this.featureData.push({
            groupName: feature.groupName,
            groupDisplayName: feature.groupDisplayName,
            featureInfos: {
              ...featureInfo
            }
          })
        }
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
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
