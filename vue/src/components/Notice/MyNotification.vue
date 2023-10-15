<template>
  <div>
    <el-badge :is-dot="true" style="line-height: 25px;margin-top: -5px;">
      <!-- todo 移除padding -->
      <el-popover v-model="popoverVisible" placement="bottom" width="500" trigger="click" class="my-notification-box" style="padding: 0;">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span style="padding-right: 10px;">我的消息</span>
            <router-link to="/notice/my-notification/list">
              <el-link style="float: right; padding: 3px 0" type="primary" @click="popoverVisible = false">查看全部</el-link>
            </router-link>

          </div>
          <div class="text item">
            <el-table :data="myNotificationData">
              <el-table-column property="NotificationName" label="NotificationName" />
              <el-table-column width="100" property="CreationTime" label="CreationTime" />
              <el-table-column width="150" property="ReadStatus" label="ReadStatus" />
            </el-table>
          </div>
        </el-card>

        <el-button slot="reference" style="padding: 8px 10px;" size="small">
          <svg-icon icon-class="message" />
        </el-button>
      </el-popover>

    </el-badge>

  </div>
</template>

<script>
export default {
  name: 'MyNotification',
  data() {
    return {
      dialogTableVisible: false,
      popoverVisible: false,
      myNotificationData: [
        {
          NotificationName: '这是一条消息',
          CreationTime: '2016-05-02',
          ReadStatus: '已读'
        },
        {
          NotificationName: '这是一条消息',
          CreationTime: '2016-05-02',
          ReadStatus: '已读'
        },
        {
          NotificationName: '这是一条消息',
          CreationTime: '2016-05-02',
          ReadStatus: '已读'
        },
        {
          NotificationName: '这是一条消息',
          CreationTime: '2016-05-02',
          ReadStatus: '已读'
        },
        {
          NotificationName: '这是一条消息',
          CreationTime: '2016-05-02',
          ReadStatus: '已读'
        },
        {
          NotificationName: '这是一条消息',
          CreationTime: '2016-05-02',
          ReadStatus: '已读'
        }

      ]
    }
  },
  computed: {
    errorLogs() {
      return this.$store.getters.errorLogs
    }
  },
  methods: {
    clearAll() {
      this.dialogTableVisible = false
      this.$store.dispatch('errorLog/clearErrorLog')
    }
  }
}
</script>

<style lang="scss" scoped>
.my-notification-box{
  .el-popover{
    padding: 0;
  }

}

.message-title {
  font-size: 16px;
  color: #333;
  font-weight: bold;
  padding-right: 8px;
}
</style>
