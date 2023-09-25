<template>
  <div class="audit-log-container">
    <el-dialog :title="logData.url+ '-' + $t('AbpAuditLogging[\'Detail\']')" :visible.sync="dialogVisible" top="6vh">
      <el-tabs type="border-card">
        <el-tab-pane :label="$t('AbpAuditLogging[\'RequsetInfo\']')">
          <table class="logInfo">
            <tbody>
              <tr>
                <th>{{ $t("AbpAuditLogging['HttpStatusCode']") }}</th>
                <td><el-tag type="info">{{ logData.httpStatusCode }}</el-tag></td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['HttpMethod']") }}</th>
                <td><el-tag type="danger">{{ logData.httpMethod }}</el-tag></td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['Url']") }}</th>
                <td><el-link type="primary" disabled>{{ logData.url }}</el-link></td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ClientIpAddress']") }}</th>
                <td>{{ logData.clientIpAddress }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ClientId']") }}</th>
                <td>{{ logData.clientId }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ClientName']") }}</th>
                <td>{{ logData.ClientName }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['UserName']") }}</th>
                <td>{{ logData.userName }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ExecutionTime']") }}</th>
                <td>{{ logData.executionTime | moment }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ExecutionDuration']") }}</th>
                <td>{{ logData.executionDuration }}ms</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['BrowserInfo']") }}</th>
                <td>{{ logData.browserInfo }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['Comments']") }}</th>
                <td>{{ logData.comments }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ExtraProperties']") }}</th>
                <td>
                  <pre>{{ logData.extraProperties }}</pre>
                </td>
              </tr>
            </tbody>
          </table>
        </el-tab-pane>
        <el-tab-pane :label="$t('AbpAuditLogging[\'Actions\']')">
          <el-collapse v-if="logData.actions && logData.actions.length>0">
            <el-collapse-item v-for="action in logData.actions" :key="action.id" :title="action.serviceName" :name="action.serviceName">
              <table class="logInfo">
                <tbody>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['MethodName']") }}</th>
                    <td>{{ action.methodName }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ExecutionTime']") }}</th>
                    <td>{{ action.executionTime | moment }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ExecutionDuration']") }}</th>
                    <td>{{ action.executionDuration }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['Parameters']") }}</th>
                    <td>
                      <!-- 展示JSON格式数据【vue-json-viewer】 -->
                      <json-viewer :value="JSON.parse(action.parameters)" :expand-depth="25" boxed sort :expanded="true" copyable />
                      <br>
                      <!-- <el-button type="primary" icon="el-icon-document" @click="handleCopyParameters(action.parameters,$event)">
                        {{ $t("table.copy") }}
                      </el-button> -->
                    </td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ExtraProperties']") }}</th>
                    <td>{{ action.extraProperties }}</td>
                  </tr>
                </tbody>
              </table>
            </el-collapse-item>
          </el-collapse>
          <p v-else>
            {{ $t('table.empty') }}
          </p>
        </el-tab-pane>
        <el-tab-pane v-if="logData.exceptions" :label="$t('AbpAuditLogging[\'Exceptions\']')">
          <table class="logInfo">
            <tbody>
              <tr>
                <th>{{ $t("AbpAuditLogging['Exceptions']") }}</th>
                <td style="max-height: 610px;overflow-y: scroll;display: block;">
                  <p>{{ logData.exceptions }}</p>
                  <br>
                  <el-button type="primary" icon="el-icon-document" @click="handleCopyParameters(logData.exceptions,$event)">
                    {{ $t("table.copy") }}
                  </el-button>
                </td>
              </tr>

            </tbody>
          </table>
        </el-tab-pane>
      </el-tabs>

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
  getAuditLog
} from '@/api/system-manage/auditing/auditlog'
import clip from '@/utils/clipboard' // 引入复制组件
import JsonViewer from 'vue-json-viewer'
export default {
  name: 'AuditLogDetails',
  components: {
    JsonViewer
  },
  data() {
    return {
      dialogVisible: false,
      logData: {}
    }
  },
  methods: {
    getDetails() {
      getAuditLog(this.logData.id).then(response => {
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
</style>
