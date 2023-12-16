<template>
  <div class="audit-log-container">
    <el-dialog :title="logData.entityTypeFullName " :visible.sync="dialogVisible" top="6vh">
      <!-- <row>{{ logData.id }}</row> -->
      <el-collapse v-model="activeNames">
        <el-collapse-item name="entity-change-detail" :title=" '时间:  ' + moment(logData.changeTime).format('YYYY-MM-DD HH:mm:ss') + '通过' + username">
          <table class="logInfo">
            <thead>
              <tr>
                <th> {{ $t("AbpAuditLogging['PropertyName']") }} </th>
                <th> {{ $t("AbpAuditLogging['OriginalValue']") }} </th>
                <th> {{ $t("AbpAuditLogging['NewValue']") }} </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="propertyChange in logData.propertyChanges" :key="propertyChange.id">
                <td>{{ propertyChange.propertyName }}</td>

                <td v-if="propertyChange.propertyName == 'ExtraProperties'">
                  <json-viewer :value="JSON.parse(propertyChange.originalValue)" :expand-depth="25" boxed sort :expanded="true" copyable />
                </td>
                <td v-else>{{ propertyChange.originalValue }}</td>

                <td v-if="propertyChange.propertyName == 'ExtraProperties'">
                  <json-viewer :value="JSON.parse(propertyChange.newValue)" :expand-depth="25" boxed sort :expanded="true" copyable />
                </td>
                <td v-else>{{ propertyChange.newValue }}</td>
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
  // getAuditLogEntityChange,
  // getAuditLogEntityChangeListWithUserName,
  getAuditLogEntityChangeWithUserName
} from '@/api/system-manage/auditing/auditlog'

import clip from '@/utils/clipboard' // 引入复制组件
import JsonViewer from 'vue-json-viewer'
import moment from 'moment'
export default {
  name: 'ChangeDetail',
  components: {
    JsonViewer
  },
  data() {
    return {
      activeNames: ['entity-change-detail'],
      dialogVisible: false,
      logData: {},
      username: ''
    }
  },
  created() {
    // console.log('this.logData', this.logData)
  },
  methods: {
    moment,
    getDetails() {
      getAuditLogEntityChangeWithUserName(this.logData.id).then(response => {
        this.logData = response.entityChange
        this.username = response.username
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
