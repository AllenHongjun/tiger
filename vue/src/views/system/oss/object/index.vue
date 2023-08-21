<template>
  <div class="app-container">
    <el-row :gutter="20">
      <el-col :span="6">
        <div class="grid-content bg-purple">
          <el-card>
            <h4>Oss容器</h4>
            <el-select v-model="currentBucket" :placeholder="$t('AbpOssManagement[\'Containers:Select\']')" :clearable="true" style="width:100%">
              <el-option
                v-for="item in bucketList"
                :key="item.name"
                :label="item.name"
                :value="item.name"
              />
            </el-select>
            <FolderTree :bucket="currentBucket" :get-list="handleFilter" @select="handlePathChange" />

          </el-card>

        </div>
      </el-col>
      <el-col :span="18">
        <div class="grid-content bg-purple-light">
          <el-card>
            <el-breadcrumb separator-class="el-icon-arrow-right">
              <el-breadcrumb-item :to="{ path: '/' }"> <i class="el-icon-s-home" style="padding-right: 8px;" />所有文件</el-breadcrumb-item>
              <el-breadcrumb-item>host</el-breadcrumb-item>
              <el-breadcrumb-item>test1</el-breadcrumb-item>
              <el-breadcrumb-item>test2</el-breadcrumb-item>
            </el-breadcrumb>
            <div class="filter-container">

              <el-row style="margin:15px 0px;">
                <el-col :span="12">
                  <el-input v-model="listQuery.Prefix" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
                  <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
                    {{ $t('AbpUi.Search') }}
                  </el-button>
                </el-col>
                <el-col :span="12">
                  <el-button-group style="float:right">
                    <el-button type="primary" icon="el-icon-upload" @click="handleUpload()">
                      {{ $t('AbpOssManagement[\'Objects:UploadFile\']') }}
                    </el-button>
                    <el-button type="reset" icon="el-icon-delete" @click="handleBulkDelete()">
                      {{ $t('AbpOssManagement[\'Objects:BulkDelete\']') }}
                    </el-button>
                  </el-button-group>
                </el-col>
              </el-row>
            </div>

            <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
              <el-table-column type="selection" width="55" />
              <el-table-column type="index" width="80" />

              <el-table-column :label="$t('AbpOssManagement[\'DisplayName:Name\']')" prop="name" sortable align="center">
                <template slot-scope="{ row }">
                  <span>{{ row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column :label="$t('AbpOssManagement[\'DisplayName:Size\']')" width="80" align="center">
                <template slot-scope="{ row }">
                  <span>{{ row.size }}</span>
                </template>
              </el-table-column>
              <el-table-column :label="$t('AbpOssManagement[\'DisplayName:CreationDate\']')" width="160" align="center">
                <template slot-scope="{ row }">
                  <span>{{ row.creationDate | moment }}</span>
                </template>
              </el-table-column>
              <el-table-column :label="$t('AbpOssManagement[\'DisplayName:LastModifiedDate\']')" width="160" align="center">
                <template slot-scope="{ row }">
                  <span>{{ row.lastModifiedDate | moment }}</span>
                </template>
              </el-table-column>

              <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="280" class-name="small-padding fixed-width">
                <template slot-scope="{ row, $index }">
                  <el-button v-if="checkPermission('AbpOssManagement.OssObject.Delete')" type="default" @click="handlePreview(row, $index)">
                    {{ $t("AbpOssManagement['Objects:Preview']") }}
                  </el-button>
                  <el-button v-if="checkPermission('AbpOssManagement.OssObject.Delete')" :loading="downloadLoading" type="primary" @click="handleDownload(row, $index)">
                    {{ $t("AbpOssManagement['Objects:Download']") }}
                  </el-button>
                  <el-button v-if="checkPermission('AbpOssManagement.OssObject.Delete')" type="danger" @click="handleDelete(row, $index)">
                    {{ $t("AbpUi['Delete']") }}
                  </el-button>

                </template>
              </el-table-column>
            </el-table>

            <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

          </el-card>

        </div>
      </el-col>
    </el-row>

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpOssManagement[\'Objects:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AbpOssManagement[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <!-- <el-form-item :label="$t('AbpOssManagement[\'DisplayName:DisplayName\']')" prop="displayName">
          <el-input v-model="temp.displayName" />
        </el-form-item>
        <el-form-item :label="$t('AbpOssManagement[\'DisplayName:Description\']')" prop="description">
          <el-input v-model="temp.description" />
        </el-form-item> -->

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpUi['Cancel']") }}
        </el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
          {{ $t("AbpUi['Save']") }}
        </el-button>
      </div>
    </el-dialog>

    <el-dialog :lock-scroll="false" draggable width="400px" :visible.sync="dialogUploadVisible">
      <template #header>
        <div style="color: #fff">
          <el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-UploadFilled /> </el-icon>
          <span> 上传文件 </span>
        </div>
      </template>
      <div>
        <el-upload ref="uploadRef" drag :auto-upload="false" :limit="1" :file-list="fileList" action="" :on-change="handleChange" accept=".jpg,.png,.bmp,.gif,.txt,.pdf,.xlsx,.docx">
          <el-icon class="el-icon--upload">
            <ele-UploadFilled />
          </el-icon>
          <div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
          <template #tip>
            <div class="el-upload__tip">请上传大小不超过 10MB 的文件</div>
          </template>
        </el-upload>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogUploadVisible = false">取消</el-button>
          <el-button type="primary" @click="uploadFile()">确定</el-button>
        </span>
      </template>
    </el-dialog>

    <oss-preview ref="OssPreview" />

  </div>
</template>

<script>
import {
  getContainers,
  getContainerObject

} from '@/api/system-manage/oss/container'
import {
  getObjects,
  getObject,
  createObject,
  updateObject,
  deleteObject,
  bulkDeleteObject,
  downloadObject

} from '@/api/system-manage/oss/object'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import { format } from '@/utils/strings'
import OssPreview from './components/OssPreviewModal'
import FolderTree from './components/FolderTree'

export default {
  name: 'Objects',
  components: {
    Pagination,
    FolderTree,
    OssPreview
  },
  data() {
    return {
      bucketList: [],
      currentBucket: undefined,
      currentPath: undefined,

      tableKey: 0,
      list: null,
      // TODO:查询阿里云的接口获取分页的总数量
      total: 10000,
      listLoading: true,
      listQuery: Object.assign({
        Bucket: 'tiger-blob',
        Prefix: undefined,
        Delimiter: undefined,
        Marker: undefined,
        EncodingType: undefined,
        MD5: undefined
      }, baseListQuery),
      temp: {
        id: undefined,
        name: '',
        size: 0,
        metadata: {
          additionalProp1: '',
          additionalProp2: '',
          additionalProp3: ''
        }
      },
      dialogFormVisible: false,
      dialogStatus: '',

      dialogUploadVisible: false,
      fileList: [],
      objectCreateInput: {
        Bucket: 'tiger-blob',
        Path: '',
        FileName: '',
        Overwrite: true,
        File: [],
        ExpirationTime: undefined
      },

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpOssManagement['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpOssManagement['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ]

      },
      downloadLoading: false,
      imagecropperShow: false,
      imagecropperKey: 0,
      image: 'https://wpimg.wallstcn.com/577965b9-bb9e-4e02-9f0c-095b41417191'
    }
  },
  // 钩子函数 通常在初始化页面完成后，对html的dom节点进行需要的操作。
  mounted() {
    this.fetchBuckets()
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
    // 获取buckets
    fetchBuckets() {
      getContainers({
        prefix: '',
        marker: '',
        sorting: '',
        skipCount: 0,
        maxResultCount: 1000
      }).then((res) => {
        this.bucketList = res.containers
      })
    },
    // buckets选中切换
    handleBucketChange(bucket) {
      this.currentBucket.value = bucket
    },
    // 路径选中切换
    handlePathChange(path) {
      this.currentPath.value = path
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      getContainerObject(this.listQuery).then(response => {
        this.list = response.objects
        this.listQuery.Marker = response.nextMarker
        this.listLoading = false
      })
    },
    handleFilter(firstPage = true, prefix = '', marker = '') {
      if (firstPage) {
        this.listQuery.page = 1
        this.listQuery.prefix = prefix
        this.listQuery.marker = marker
      }

      this.getList()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },

    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        name: '',
        size: 0,
        metadata: {
          additionalProp1: '',
          additionalProp2: '',
          additionalProp3: ''
        }
      }
    },
    cropSuccess(resData) {
      this.imagecropperShow = false
      this.imagecropperKey = this.imagecropperKey + 1
      this.image = resData.files.avatar
    },
    close() {
      this.imagecropperShow = false
    },

    // 点击创建按钮
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createObject(this.temp.name).then(() => {
            this.list.unshift(this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },

    // 更新按钮点击
    handleUpdate(row) {
      // this.temp = Object.assign({}, row) // copy obj
      getObject(row.name).then(response => {
        this.temp = response
      })
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateObject(this.temp.id, this.temp).then(() => {
            const index = this.list.findIndex((v) => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },

    // 删除
    handleDelete(row, index) {
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        var req = {
          'Bucket': 'tiger-blob',
          'Object': row.name,
          'path': 'host/tiger-blob/'
        }
        deleteObject(req).then(() => {
          const index = this.list.findIndex((v) => v.id === row.id)
          this.list.splice(index, 1)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    // 批量删除
    handleBulkDelete() {
      this.$confirm(
        // 消息
        '文件删除',
        // title
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        var req = {
          'Bucket': 'tiger-blob',
          'path': 'host/tiger-blob/',
          'Object': '' // 获取选中行的名称
        }
        bulkDeleteObject(req).then(() => {
          this.handleFilter()
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    handleUpload() {
      this.fileList = []
      this.dialogUploadVisible = true
    },
    handleChange(file, fileList) {
      this.fileList = fileList
    },
    uploadFile() {
      if (this.fileList.length < 1) return
      const formData = new FormData()
      // 下面数据是我自己设置的数据,可自行添加数据到formData(使用键值对方式存储)
      formData.append('Bucket', 'tiger-blob')
      formData.append('Path', 'tiger-blob')
      formData.append('FileName', this.fileList[0].name)
      // formData.append('ExpirationTime', undefined)
      formData.append('File', this.fileList[0].raw)// 拿到存在fileList的文件存放到formData中

      createObject(formData).then(() => {
        this.getList()
        this.dialogUploadVisible = false
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    // 预览
    handlePreview(row, index) {
      this.$refs['OssPreview'].handleDrawer(row, index)
    },

    // 下载
    handleDownload(row, index) {
      const link = document.createElement('a')
      link.style.display = 'none'
      link.href = 'https://tiger-blob.oss-cn-hangzhou.aliyuncs.com/host/' + row.name
      link.setAttribute('download', row.name)
      document.body.appendChild(link)
      link.click()
    },
    generateOssUrl(bucket, path, object) {
      // if (path) {
      //   // 对 Path部分的 URL 编码
      //   path = encodeURIComponent(path)
      //   if (path !== '.%2F' && path.endsWith('%2F')) {
      //     path = path.substring(0, path.length - 3)
      //   }
      // }
      // return format(downloadUrl, { bucket: bucket, path: path, name: object })
    }
  }
}
</script>
