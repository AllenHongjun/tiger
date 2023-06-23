<template>
  <div class="app-container">

    <el-row :gutter="10">
      <el-col :span="12">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>系统信息</span>
            </div>
            <el-table ref="dragTable" v-loading="listLoading" :data="list" row-key="id" border fit highlight-current-row style="width: 100%">
              <!-- <el-table-column prop="name" label="名称" width="180" />
              <el-table-column prop="value" label="信息" /> -->

              <el-table-column min-width="300px" label="名称" width="180">
                <template slot-scope="{row}">
                  <span>{{ row.author }}</span>
                </template>
              </el-table-column>

              <el-table-column align="center" label="信息">
                <template slot-scope="{row}">
                  <span>{{ row.content_short }}</span>
                </template>
              </el-table-column>
            </el-table>
          </el-card>
        </div>
      </el-col>

      <el-col :span="12">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>CLR信息</span>
            </div>
            <el-table :data="CLRData" style="width: 100%">
              <el-table-column prop="name" label="名称" width="180" />
              <el-table-column prop="value" label="信息" />
            </el-table>
          </el-card>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="10">
      <el-col :span="6">
        <div class="grid-content bg-purple-light">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>系统使用信息</span>
            </div>
            <el-table :data="systemUseData" style="width: 100%">
              <el-table-column prop="name" label="名称" width="180" />
              <el-table-column prop="value" label="信息" />
            </el-table>
          </el-card>
        </div>
      </el-col>

      <el-col :span="18">
        <div class="grid-content bg-purple-light">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>系统率</span>
            </div>
            此处改为使用占比的图表显示

          </el-card>
        </div>
      </el-col>

    </el-row>

    <el-row>
      <el-card class="box-card">
        <div slot="header" class="clearfix">
          <span>磁盘信息</span>
        </div>
        <el-table :data="diskData" style="width: 100%">
          <el-table-column prop="dirName" label="盘符路径" width="180" />
          <el-table-column prop="sysTypeName" label="文件系统" />
          <el-table-column prop="typeName" label="盘符类型" />
          <el-table-column prop="total" label="总大小" />
          <el-table-column prop="free" label="可用大小" />
          <el-table-column prop="used" label="已用大小" />
          <el-table-column prop="usage" label="已用百分比" />
        </el-table>
      </el-card>
    </el-row>
  </div>
</template>

<script>
// import { fetchList } from '@/api/article'
import { fetchList, getFeatures } from '@/api/sass/features'
import Sortable from 'sortablejs'

export default {
  name: 'DragTable',
  data() {
    return {
      blank: {

      },
      // 系统信息
      systemData: [{
        name: '服务器名称',
        value: 'iZwz90rjtrmd83mt2s0df8Z'
      },
      {
        name: '操作系统',
        value: 'Linux 5.4.0-131-generic #147-Ubuntu SMP Fri Oct 14 17:07:22 UTC 2022'
      },
      {
        name: '系统架构',
        value: 'Unix X64'
      },
      {
        name: 'CPU核数',
        value: '4 核'
      },
      {
        name: '总内存',
        value: '7.31G'
      },
      {
        name: '总磁盘',
        value: '69.31G'
      },
      {
        name: '运行时长',
        value: '185 天 17 小时 38 分 08 秒'
      },
      {
        name: '外网地址',
        value: '118.190.161.209 中国 山东 青岛 阿里云'
      },
      {
        name: '内网地址',
        value: '::ffff:127.0.0.1'
      },
      {
        name: '运行框架',
        value: '.NET 6.0.11'
      }
      ],
      // 系统使用星系
      systemUseData: [{
        name: '启动时间',
        value: '2023-06-22 03:00:03'
      },
      {
        name: '运行时长',
        value: '00 天 13 小时 47 分 43 秒'
      },
      {
        name: '网站目录',
        value: '/wwwroot/smart_prison_core/wwwroot'
      },
      {
        name: '开发环境',
        value: 'Production'
      }
      ],
      // dotnet运行时
      CLRData: [{
        name: '.NET名称',
        value: 'Java HotSpot(TM) 64-Bit Server VM'
      },
      {
        name: '.NET版本',
        value: '1.8.0_111'
      },
      {
        name: '启动时间',
        value: '2023-04-23 16:12:19'
      },
      {
        name: '运行时长',
        value: '60天0小时40分钟'
      },
      {
        name: '安装路径',
        value: '/usr/java/jdk1.8.0_111/jre'
      },
      {
        name: '项目路径',
        value: '/home/ruoyi/projects/ruoyi-vue'
      },
      {
        name: '运行参数',
        value: '[-Dname=target/ruoyi-vue.jar, -Duser.timezone=Asia/Shanghai, -Xms512m, -Xmx1024m, -XX:MetaspaceSize=128m, -XX:MaxMetaspaceSize=512m, -XX:+HeapDumpOnOutOfMemoryError, -XX:+PrintGCDateStamps, -XX:+PrintGCDetails, -XX:NewRatio=1, -XX:SurvivorRatio=30, -XX:+UseParallelGC, -XX:+UseParallelOldGC]'
      }
      ],
      // 磁盘信息
      diskData: [{
        dirName: '/',
        sysTypeName: 'ext4',
        typeName: '/',
        total: '39.2 GB',
        free: '25.1 GB',
        used: '14.1 GB',
        usage: 35.87
      },
      {
        dirName: 'C:',
        sysTypeName: 'ext4',
        typeName: '/',
        total: '45.2 GB',
        free: '33.1 GB',
        used: '5.1 GB',
        usage: 15.87
      }
      ],
      list: null,
      total: null,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10
      },
      sortable: null,
      oldList: [],
      newList: []
    }
  },
  created() {
    this.getList()
  },
  methods: {
    async getList() {
      this.listLoading = true
      const { data } = await getFeatures(this.listQuery)
      this.list = data.items
      this.total = data.total
      this.listLoading = false
      this.oldList = this.list.map(v => v.id)
      this.newList = this.oldList.slice()
      this.$nextTick(() => {
        this.setSort()
      })
    },
    setSort() {
      const el = this.$refs.dragTable.$el.querySelectorAll('.el-table__body-wrapper > table > tbody')[0]
      this.sortable = Sortable.create(el, {
        ghostClass: 'sortable-ghost', // Class name for the drop placeholder,
        setData: function(dataTransfer) {
          // to avoid Firefox bug
          // Detail see : https://github.com/RubaXa/Sortable/issues/1012
          dataTransfer.setData('Text', '')
        },
        onEnd: evt => {
          const targetRow = this.list.splice(evt.oldIndex, 1)[0]
          this.list.splice(evt.newIndex, 0, targetRow)

          // for show the changes, you can delete in you code
          const tempIndex = this.newList.splice(evt.oldIndex, 1)[0]
          this.newList.splice(evt.newIndex, 0, tempIndex)
        }
      })
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
// 卡片
.text {
    font-size: 14px;
}

.item {
    margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
    display: table;
    content: "";
}

.clearfix:after {
    clear: both
}

.box-card {
    width: 100%;
}

.line {
    text-align: center;
}

// 布局
.el-row {
    margin-bottom: 20px;

    &:last-child {
        margin-bottom: 0;
    }
}

.el-col {
    border-radius: 4px;
}

.bg-purple-dark {
    background: #99a9bf;
}

.bg-purple {
    background: #d3dce6;
}

.bg-purple-light {
    background: #e5e9f2;
}

.grid-content {
    border-radius: 4px;
    min-height: 36px;
}

.row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
}
</style>
