<template>
  <div class="audit-log-container">
    <el-dialog :title="logData.entityChange.entityTypeFullName + logData.entityChange.id" :visible.sync="dialogVisible" top="6vh">
      <el-collapse v-model="activeNames">
        <el-collapse-item name="action.serviceName" :title="logData.entityChange.changeTime + '通过' + logData.username">
          <table class="logInfo">
            <thead>
              <tr>
                <th> {{ $t("AbpAuditLogging['PropertyName']") }} </th>
                <th> {{ $t("AbpAuditLogging['OriginalValue']") }} </th>
                <th> {{ $t("AbpAuditLogging['NewValue']") }} </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="propertyChange in logData.entityChange.propertyChanges" :key="propertyChange.id">
                <td>{{ propertyChange.propertyName }}</td>
                <td>{{ propertyChange.originalValue }}</td>
                <!-- 如果是额外属性使用json插件展示 -->
                <td>{{ propertyChange.newValue }}</td>
              </tr>

            </tbody>
          </table>
        </el-collapse-item>
      </el-collapse>

      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">
          {{ $t("AbpAuditLogging['Close']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getAuditLogEntityChange,
  getAuditLogEntityChangeListWithUserName,
  getAuditLogEntityChangeWithUserName
} from '@/api/system-manage/auditing/auditlog'

import clip from '@/utils/clipboard' // 引入复制组件
import JsonViewer from 'vue-json-viewer'
export default {
  name: 'ChangeDetail',
  components: {
    JsonViewer
  },
  data() {
    return {
      activeNames: ['1'],
      dialogVisible: false,
      logData: {}
    }
  },
  methods: {
    getDetails() {
      getAuditLogEntityChangeWithUserName(this.logData.id).then(response => {
        console.log('response', response)
        this.logData = response
      })
    },
    createLogInfo(row) {
      this.dialogVisible = true
      this.logData = row
      this.getDetails()
    },
    handleCopyParameters(text, event) {
      clip(text, event)
    }
  }
}
</script>

<style lang="scss" scoped>
.audit-log-container {
    .logInfo {
        border-collapse: collapse;
        border-spacing: 2px;
        width:100%;

        tr {
            border: 1px solid #f0f0f0;

            th {
                background-color: #fafafa;
                width: 120px;
            }

            th,
            td {
                text-align: left;
                text-overflow: ellipsis;
                vertical-align: middle;
                box-sizing: border-box;
                border-right: #f0f0f0;
                height: inherit;

                padding: 8px 16px;
            }
        }
        .jv-container{
          overflow-y:scroll;max-height:300px;
        }
    }
}
.el-collapse-item__header{
  background-color: #f3ffed;
}
</style>
