<template>
  <div class="app-container">
    <el-row v-show="operatorButtonsVisibilty" style="margin-bottom:5px;">

      <el-button icon="el-icon-search">查询</el-button>
      <el-button icon="el-icon-set-up" @click="swithBillContainer">切换</el-button>
      <el-button type="info" icon="el-icon-refresh" @click="handleRefresh">刷新</el-button>
      <el-button type="info" icon="el-icon-printer">打印</el-button>
      <el-button type="primary" icon="el-icon-plus" @click="handleCreate">添加</el-button>
      <el-button type="primary" icon="el-icon-edit" @click="handleUpdate">编辑</el-button>
      <el-button type="danger" icon="el-icon-delete" @click="handleDelete">删除</el-button>
      <el-button type="warning" icon="el-icon-check" @click="handleAudited()">审核</el-button>
      <el-dropdown trigger="click" style="margin:0 5px;" @command="handleOnDev">
        <el-button type="success" icon="el-icon-document-copy">
          生单<i class="el-icon-arrow-down el-icon--right" />
        </el-button>

        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item @click="handleOnDev">
            <el-link><i class="el-icon-document el-icon--left" />生成退货单 </el-link>
          </el-dropdown-item>
          <el-dropdown-item @click="handleOnDev">
            <el-link><i class="el-icon-document el-icon--left" />生成调拨单 </el-link>
          </el-dropdown-item>
          <el-dropdown-item @click="handleOnDev">
            <el-link><i class="el-icon-document el-icon--left" />生成入库单 </el-link>
          </el-dropdown-item>
          <el-dropdown-item @click="handleOnDev">
            <el-link><i class="el-icon-document el-icon--left" />生成出库单 </el-link>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <!-- <el-button type="danger" icon="el-icon-delete">作废</el-button> -->
      <el-button type="info" icon="el-icon-message">到货/入库</el-button>

      <el-dropdown trigger="click" style="margin:0 5px;" @command="handleBatch">
        <el-button type="warning" icon="el-icon-edit-outline">
          批处理<i class="el-icon-arrow-down el-icon--right" />
        </el-button>

        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item icon="el-icon-printer" divided command="batchPrint">批量打印</el-dropdown-item>
          <!-- <el-divider /> -->
          <el-dropdown-item icon="el-icon-delete" divided command="batchDelete">批量删除</el-dropdown-item>
          <el-dropdown-item icon="el-icon-check" divided command="batchAudit">批量审核</el-dropdown-item>
          <el-dropdown-item icon="el-icon-check" divided disabled>批量修改备注</el-dropdown-item>
          <el-dropdown-item icon="el-icon-message" divided command="batchArrive">批量到货</el-dropdown-item>
          <el-dropdown-item icon="el-icon-close" divided command="batchClose">批量订单关闭</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <el-dropdown trigger="click" style="margin:0 5px;" @command="handleCommand">
        <el-button type="info" icon="el-icon-more">
          更多<i class="el-icon-arrow-down el-icon--right" />
        </el-button>

        <el-dropdown-menu slot="dropdown">
          <!-- <el-dropdown-item icon="el-icon-plus" divided>复制新增</el-dropdown-item> -->
          <!-- <el-divider /> -->
          <!-- <el-dropdown-item icon="el-icon-circle-plus" divided>草稿新增</el-dropdown-item> -->
          <!-- <el-dropdown-item icon="el-icon-edit" divided @click="handleUpdate(scope.row)">标记已付</el-dropdown-item> -->
          <!-- <el-dropdown-item icon="el-icon-edit" divided disabled>修改备注</el-dropdown-item> -->
          <el-dropdown-item icon="el-icon-document" divided command="handleExport">导出excel</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </el-row>

    <el-row v-show="!operatorButtonsVisibilty" style="margin-bottom:5px;">
      <el-button type="primary" icon="el-icon-search" @click="detailFormStatus==='create'?createData():updateData()">保存</el-button>
      <el-button icon="el-icon-set-up" @click="swithBillContainer">存为草稿</el-button>
      <el-button type="info" icon="el-icon-refresh" @click="handleImport">导入明细</el-button>
      <el-button icon="el-icon-refresh" @click="onCancel">取消</el-button>

    </el-row>

    <div v-show="billContainerVisibilty" class="bill-container">
      <div class="filter-container">
        <el-input v-model="listQuery.title" placeholder="请输入分类名称" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        <!-- <el-select v-model="listQuery.importance" placeholder="条件" clearable style="width: 90px" class="filter-item">
        <el-option v-for="item in importanceOptions" :key="item" :label="item" :value="item" />
      </el-select> -->
        <!-- <el-select v-model="listQuery.status" placeholder="条件" clearable style="width: 90px" class="filter-item">
        <el-option v-for="item in statusOptions" :key="item" :label="item" :value="item" />
      </el-select> -->
        <el-select v-model="listQuery.type" placeholder="状态" clearable class="filter-item" style="width: 130px">
          <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
        </el-select>
        <el-cascader :props="listQuery.props" />
        <!-- <el-select v-model="listQuery.sort" style="width: 140px" class="filter-item" @change="handleFilter">
        <el-option v-for="item in sortOptions" :key="item.key" :label="item.label" :value="item.key" />
      </el-select> -->

        <div v-show="searchDivVisibilty" class="search-container">
          <el-form ref="searchForm" :model="listQuery" label-width="80px">
            <el-row>
              <el-col :span="6">
                <el-form-item v-model="listQuery.title" label="名称" placeholder="请输入名称" label-width="120px" class="postInfo-container-item">
                  <el-input v-model="listQuery.title" />
                </el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item label="重要性" label-width="120px" class="postInfo-container-item">
                  <el-select v-model="listQuery.importance" placeholder="重要性" clearable style="width: 90px" class="filter-item">
                    <el-option v-for="item in importanceOptions" :key="item" :label="item" :value="item" />
                  </el-select>
                </el-form-item>
              </el-col>

              <el-col :span="6">
                <el-form-item label="类型" label-width="120px" class="postInfo-container-item">
                  <el-select v-model="listQuery.type" placeholder="类型" clearable class="filter-item" style="width: 130px">
                    <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name+'('+item.key+')'" :value="item.key" />
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item label="排序" label-width="120px" class="postInfo-container-item">
                  <el-select v-model="listQuery.sort" style="width: 140px" class="filter-item" @change="handleFilter">
                    <el-option v-for="item in sortOptions" :key="item.key" :label="item.label" :value="item.key" />
                  </el-select>
                </el-form-item>
              </el-col>

            </el-row>

            <el-form-item label="" label-width="120px">
              <el-button type="primary" @click="handleFilter">
                搜索
              </el-button>
              <el-button type="primary" @click="resetSearchForm('searchForm')">
                重置
              </el-button>
              <el-button type="primary" @click="handleSearch">
                关闭
              </el-button>
            </el-form-item>
          </el-form>
        </div>
      </div>

      <el-table ref="billTable" v-loading="listLoading" :data="list" fit highlight-current-row style="width: 100%" @sort-change="sortChange" @current-change="handleCurrentChange">

        <el-table-column align="center" type="selection" width="55" />

        <el-table-column min-width="180px" label="单号">
          <template slot-scope="{row}">
            <span class="link-type" @click="swithBillContainer">{{ row.code }}</span>
          </template>
        </el-table-column>

        <!-- <el-table-column class-name="status-col" label="类型" width="110">
          <template slot-scope="{row}">
            <el-tag :type="row.purchaseType | statusFilter">
              {{ row.purchaseType | purchaseTypeFilter }}
            </el-tag>
          </template>
        </el-table-column> -->

        <el-table-column width="180px" align="center" label="创建日期" sortable>
          <template slot-scope="scope">
            <span>{{ scope.row.creationTime | formatDate }}</span>
          </template>
        </el-table-column>

        <!-- <el-table-column width="180px" align="center" label="到货时间" sortable>
          <template slot-scope="scope">
            <span>{{ scope.row.arriveDatetime | formatDate }}</span>
          </template>
        </el-table-column> -->

        <el-table-column min-width="100px" label="总数量">
          <template slot-scope="{row}">
            <span>{{ row.totalQty }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="100px" label="总金额">
          <template slot-scope="{row}">
            <span>{{ row.totalAmount }}</span>
          </template>
        </el-table-column>

        <!-- <el-table-column min-width="100px" label="总体积">
          <template slot-scope="{row}">
            <span>{{ row.totalVolume }}</span>
          </template>
        </el-table-column> -->

        <el-table-column min-width="100px" label="备注">
          <template slot-scope="{row}">
            <span>{{ row.note }}</span>
          </template>
        </el-table-column>

      </el-table>

      <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    </div>

    <div v-show="!billContainerVisibilty" class="bill-detail-container">
      <el-divider content-position="left" />
      <el-form ref="detailForm" :rules="rules" :model="temp" label-width="120px">
        <!-- {{ detailFormStatus }} -->
        <!-- <el-tag>进货</el-tag>
        <el-tag type="success">进货</el-tag>
        <el-tag type="info">打印</el-tag>
        <el-tag type="warning">审核</el-tag>
        <el-tag type="warning">待审</el-tag>
        <el-tag type="danger">删除</el-tag> -->
        <el-tag
          v-for="item in purchaseStatus"
          :key="item.label"
          :type="item.type"
          effect="plain"
        >
          {{ item.label }}
        </el-tag>
        <el-divider content-position="left">单据明细</el-divider>

        <el-row>
          <el-col :span="6">
            <el-form-item label="单号" prop="code">
              <el-input v-model="temp.code" placeholder="自动生成" :readonly="true" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <!-- <el-form-item label="入库仓库" prop="username">
              <el-input v-model="temp.warehouseId" :readonly="readonly" />

            </el-form-item> -->

            <el-form-item label="入库仓库" prop="warehouseId">
              <!-- <el-input v-model="temp.warehouseId" /> -->
              <el-select v-model="temp.warehouseId" filterable placeholder="请选择" :disabled="readonly" :clearable="true">
                <el-option
                  v-for="item in wareHouseOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="6">
            <el-form-item label="供应商" prop="username">
              <el-input v-model="temp.supplyName" :disabled="readonly" suffix-icon="el-icon-search" @click.native="selectSupplyDialogVisible = !readonly" />

            </el-form-item>
          </el-col>

          <!-- <el-col :span="6">
            <el-form-item label="类型" prop="purchaseType">
              <el-select v-model="temp.purchaseType" class="filter-item" placeholder="请选择" :readonly="readonly">
                <el-option v-for="item in purchaseTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
              </el-select>
            </el-form-item>
          </el-col> -->

        </el-row>

        <el-row>
          <el-col :span="6">
            <el-form-item label="总数量">
              <span>{{ temp.totalQty }}</span>
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="总金额" prop="username">
              <span>{{ temp.totalAmount }}</span>

            </el-form-item>
          </el-col>
          <!-- <el-col :span="6">
            <el-form-item label="总体积" prop="purchaseType">
              <span>{{ temp.totalVolume }}</span>
            </el-form-item>
          </el-col>

          <el-col :span="6">
            <el-form-item label="总箱数" prop="username">
              <span>{{ temp.totalCases }}</span>
            </el-form-item>
          </el-col> -->
        </el-row>

        <el-row>
          <el-col :span="6">
            <el-form-item label="备注">
              <el-input v-model="temp.note" :autosize="{ minRows: 2, maxRows: 4}" type="textarea" placeholder="请输入" :readonly="readonly" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-divider />
        <el-table v-loading="listLoading" :data="temp.purchaseDetails" fit highlight-current-row style="width: 100%" :show-summary="true" @sort-change="sortChange">
          <el-table-column align="center" type="selection" width="55" />

          <el-table-column v-if="!readonly" align="center" label="操作" width="150px">
            <template slot-scope="{row}">
              <el-button-group>
                <el-button type="success" icon="el-icon-plus" circle @click="handleCreateDetail(row)" />
                <!-- <el-button type="success" icon="el-icon-plus" @click="confirmEdit(row)" /> -->
                <el-button type="danger" icon="el-icon-delete" circle @click="handleDeleteDetail(row)" />
                <el-button v-if="row.edit" type="success" icon="el-icon-check" circle @click="confirmEditDetail(row)" />
                <el-button
                  v-else
                  type="primary"
                  icon="el-icon-edit"
                  circle
                  @click="row.edit=!row.edit"
                />
              </el-button-group>
            </template>
          </el-table-column>

          <el-table-column width="120px" label="货号" sortable>
            <template slot-scope="{row}">
              <template v-if="row.edit">
                <el-input v-model="row.productSn" class="edit-input" suffix-icon="el-icon-search" @click.native="selectProductDialogVisible = true" />
              </template>
              <span v-else>{{ row.productSn }}</span>
            </template>
          </el-table-column>

          <el-table-column width="100px" label="分类">
            <template slot-scope="{row}">
              <span>{{ row.product.categoryName }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="180px" label="商品名称">
            <template slot-scope="{row}">
              <span>{{ row.product.name }}</span>
            </template>
          </el-table-column>

          <el-table-column width="80px" label="规格">
            <template slot-scope="{row}">
              <span>{{ row.product.categoryName }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="80px" label="单位">
            <template slot-scope="{row}">
              <span>{{ row.product.unit }}</span>
            </template>
          </el-table-column>

          <el-table-column width="160px" label="批次" sortable>
            <template slot-scope="{row}">
              <template v-if="row.edit">
                <el-input v-model="row.batch" class="edit-input" />
              </template>
              <span v-else>{{ row.batch }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="80px" label="单价">
            <template slot-scope="{row}">
              <template v-if="row.edit">
                <el-input v-model="row.purchasePrice" class="edit-input" />
              </template>
              <span v-else>{{ row.purchasePrice }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="80px" label="数量">
            <template slot-scope="{row}">
              <template v-if="row.edit">
                <el-input v-model="row.totalQty" class="edit-input" />
              </template>
              <span v-else>{{ row.totalQty }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="80px" label="未收数量">
            <template slot-scope="{row}">
              <template v-if="row.edit">
                <el-input v-model="row.openQty" class="edit-input" />
              </template>
              <span v-else>{{ row.openQty }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="100px" label="处理标记">
            <template slot-scope="{row}">
              <template v-if="row.edit">
                <el-input v-model="row.processStamp" class="edit-input" />
              </template>
              <span v-else>{{ row.processStamp }}</span>
            </template>
          </el-table-column>

          <!-- <el-table-column width="180px" align="center" label="生产日期" sortable>
            <template slot-scope="scope">
              <template v-if="scope.row.edit">
                <el-date-picker v-model="scope.row.manufactureDate" type="date" placeholder="生产日期" style="width: 100%;" />
              </template>
              <span v-else>{{ scope.row.manufactureDate | formatDate }}</span>
            </template>
          </el-table-column>

          <el-table-column width="180px" align="center" label="入库日期" sortable>
            <template slot-scope="scope">
              <span>{{ scope.row.agingDate | formatDate }}</span>
            </template>
          </el-table-column> -->

        </el-table>

        <el-divider />

        <el-collapse accordion>
          <el-collapse-item title="更多信息" name="1">
            <el-row>
              <el-col :span="4">
                <el-form-item label="创建人">
                  <el-input v-model="temp.creatorId" :readonly="true" />
                </el-form-item>
              </el-col>

              <el-col :span="4">
                <el-form-item label="创建时间" prop="creationTime">
                  <el-date-picker v-model="temp.creationTime" type="date" placeholder="创建日期" style="width: 100%;" :readonly="true" />
                </el-form-item>
              </el-col>

              <el-col :span="4">
                <el-form-item label="删除人">
                  <el-input v-model="temp.deleterId" :readonly="true" />
                </el-form-item>
              </el-col>

              <el-col :span="4">
                <el-form-item label="删除时间" prop="deletionTime">
                  <el-date-picker v-model="temp.deletionTime" type="date" placeholder="删除时间" style="width: 100%;" :readonly="true" />
                </el-form-item>
              </el-col>

              <el-col :span="4">
                <el-form-item label="修改人">
                  <el-input v-model="temp.lastModifierId" :readonly="true" />
                </el-form-item>
              </el-col>

              <el-col :span="4">
                <el-form-item label="修改时间" prop="lastModificationTime">
                  <el-date-picker v-model="temp.lastModificationTime" type="date" placeholder="修改时间" style="width: 100%;" :readonly="true" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row />
          </el-collapse-item>

        </el-collapse>

      </el-form>

    </div>

    <GetSupplies :visible="selectSupplyDialogVisible" @selectSupply="selectSupply" @closeGetSupplies="selectSupplyDialogVisible = false" />

    <GetProducts :visible="selectProductDialogVisible" @selectProduct="selectProducts" @closeGetProducts="selectProductDialogVisible = false" />

    <!-- 导入excel -->
    <el-dialog title="导入" :visible.sync="importExcelDialogVisible" width="650px">
      <el-button v-waves :loading="downloadLoading" size="mini" icon="el-icon-download" @click="handleDownload">
        下载模板
      </el-button>
      <el-divider />
      <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
      <el-row style="margin-top:10px;">只能上传Excel文件,文件大小不能超过10M</el-row>
      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="importExcelDialogVisible = false">
          关闭
        </el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import { getPurchaseHeaders, getPurchaseHeaderById, createPurchaseHeader, updatePurchaseHeader, deletePurchaseHeader } from '@/api/purchase/purchase-header'
import { getAllWarehouses } from '@/api/basic/warehouse'
import moment from 'moment/moment'

import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import UploadExcelComponent from '@/components/UploadExcel/index.vue'
import GetSupplies from '@/components/GetData/GetSupplies.vue'
import GetProducts from '@/components/GetData/GetProducts.vue'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

// arr to obj, such as { CN : "China", US : "USA" }
const calendarTypeOptions = [
  { key: 'show', display_name: '显示' },
  { key: 'hidden', display_name: '隐藏' }
]
const calendarTypeKeyValue = calendarTypeOptions.reduce((acc, cur) => {
  acc[cur.key] = cur.display_name
  return acc
}, {})

const purchaseTypeOptions = [
  { key: 0, display_name: '其他入库' },
  { key: 1, display_name: '采购入库' },
  { key: 2, display_name: '退货入库' },
  { key: 3, display_name: '盘盈入库' }
]
const purchaseTypeKeyValue = purchaseTypeOptions.reduce((acc, cur) => {
  acc[cur.key] = cur.display_name
  return acc
}, {})

const purchaseStatus = [
  { type: '', label: '进货' },
  { type: 'success', label: '打印' },
  // { key: 'audited', type: 'info', label: '审核' },
  { key: 'waitAudited', type: 'danger', label: '待审' },
  { type: 'warning', label: '删除' }
]

export default {
  // 入库单
  name: 'PurchaseHeaderList',
  components: { Pagination, UploadExcelComponent, GetSupplies, GetProducts },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: 'success',
        draft: 'info',
        deleted: 'danger'
      }
      return statusMap[status]
    },
    typeFilter(type) {
      return calendarTypeKeyValue[type]
    },
    purchaseTypeFilter(type) {
      return purchaseTypeKeyValue[type]
    }
  },
  data() {
    return {
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        // status: undefined,
        name: undefined
        // sort: '+id'

      },
      currentRow: null,
      billContainerVisibilty: true,
      operatorButtonsVisibilty: true,
      detailFormStatus: '',
      purchaseTypeOptions,
      readonly: true,
      wareHouseOptions: [],
      importExcelDialogVisible: false,
      selectSupplyDialogVisible: false,
      selectProductDialogVisible: false,
      purchaseStatus,

      importanceOptions: [1, 2, 3],
      // statusOptions: [true, false],
      calendarTypeOptions,
      sortOptions: [{ label: '升序', key: '+id' }, { label: '降序', key: '-id' }],
      statusOptions: ['published', 'draft', 'deleted'],
      showReviewer: false,
      show: true,
      dialogFormVisible: false,
      textMap: {
        update: 'Edit',
        create: 'Create'
      },
      searchDivVisibilty: false,
      searchFilterDialogVisible: false,

      dialogPvVisible: false,
      pvData: [],
      downloadLoading: false,

      temp: {
        id: undefined,
        warehouseCode: '',
        code: '',
        purchasePrice: 0,
        status: 0,
        totalAmount: 0,
        totalQty: 0,
        auditedBy: '',
        purchaseBy: '',
        note: '',
        warehouseId: undefined,
        supplyId: '',
        supplyName: '',
        purchaseDetails: [

        ]
      },
      tempDetail: {
        productSn: '',
        unit: '',
        qty: 0,
        purchasePrice: 0,
        note: '',
        totalQty: 0,
        openQty: 0,
        processStamp: '',
        productId: '9CAC5265-21DC-C016-0374-39FEB4686D17',
        product: {
          name: '',
          productSn: '',
          unit: '',
          categoryName: ''
        },
        id: undefined,
        edit: false
        // id: ""
      },
      rules: {
        warehouseId: [{ required: true, message: '请选择仓库', trigger: 'change' }],
        timestamp: [{ type: 'date', required: true, message: 'timestamp is required', trigger: 'change' }],
        name: [{ required: true, message: 'title is required', trigger: 'blur' }]
      }

    }
  },
  created() {
    this.getList()
    this.getWarehouseList()
  },
  methods: {
    getWarehouseList() {
      getAllWarehouses().then((response) => {
        var obj = response
        console.log('obj', obj)
        var tempArr = []
        if (obj.length > 0) {
          Object.keys(obj).forEach(function(key) {
            // console.log('key', key, 'obj', obj)
            tempArr.push({ value: obj[key].id, label: obj[key].name })
          })
        }

        this.wareHouseOptions = tempArr
      })
    },
    getList() {
      this.listLoading = true
      getPurchaseHeaders(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false

        // 默认选中首行
        this.setCurrent(this.list[0])
      })
    },
    handleFilter() {
      this.searchDivVisibilty = false
      this.listQuery.page = 1
      this.getList()
    },
    // 刷新
    handleRefresh() {
      this.list = null
      this.handleFilter()
    },
    // 选中当前
    setCurrent(row) {
      console.log('row', row)
      this.$nextTick(() => {
        // this.$refs.multipleTable.toggleRowSelection(row, true)
        this.$refs.billTable.setCurrentRow(row)
      })
    },
    handleCurrentChange(val) {
      console.log('this.currentRow-val', val)
      this.currentRow = val
    },
    // 排序
    sortChange(data) {
      const { prop, order } = data
      if (prop === 'id') {
        this.sortByID(order)
      }
    },
    sortByID(order) {
      if (order === 'ascending') {
        this.listQuery.sort = '+id'
      } else {
        this.listQuery.sort = '-id'
      }
      this.handleFilter()
    },
    resetSearchForm(formName) {
      console.log('formName', formName)
      this.$refs[formName].resetFields()
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        warehouseCode: '',
        code: '',
        purchasePrice: 0,
        status: 0,
        totalAmount: 0,
        totalQty: 0,
        auditedBy: '',
        purchaseBy: '',
        note: '',
        warehouseId: undefined,
        supplyId: '',
        supplyName: '',
        purchaseDetails: [

        ]
      }
    },
    resetTempDetail() {
      this.tempDetail = {
        productSn: '',
        unit: '',
        qty: 0,
        purchasePrice: 0,
        note: '',
        totalQty: 0,
        openQty: 0,
        processStamp: '',
        productId: '9CAC5265-21DC-C016-0374-39FEB4686D17',
        product: {
          name: '',
          productSn: '',
          unit: '',
          categoryName: ''
        },
        id: undefined,
        edit: false

      }
    },
    // 搜索
    handleSearch() {
      this.searchDivVisibilty = !this.searchDivVisibilty
      console.log('handleSearch', this.searchDivVisibilty)
    },
    // 切换
    swithBillContainer() {
      this.billContainerVisibilty = !this.billContainerVisibilty
      if (!this.billContainerVisibilty) {
        // console.log('swithBillContainer', this.billContainerVisibilty)
        this.getDetail(this.currentRow.id)
      }
    },
    // 保存
    handleSave() {
      var isUpdate = this.detailFormStatus === 'update'
      if (isUpdate) {
        this.createData()
      } else {
        this.updateData()
      }
    },
    // 取消
    onCancel() {
      // this.billContainerVisibilty =
      this.operatorButtonsVisibilty = !this.operatorButtonsVisibilty
      this.readonly = true
    },
    handleCreate() {
      this.resetTemp()
      this.readonly = false
      this.detailFormStatus = 'create'
      this.billContainerVisibilty = this.operatorButtonsVisibilty = false
      this.temp.purchaseDetails.splice(0, 0, this.tempDetail)
      // this.tempDetail = this.temp.purchaseDetails;
      this.$nextTick(() => {
        this.$refs['detailForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['detailForm'].validate((valid) => {
        if (valid) {
          // this.temp.id = parseInt(Math.random() * 100) + 1024 // mock a id
          // this.temp.author = 'vue-element-admin'
          createPurchaseHeader(this.temp).then((res) => {
            // 添加成功后列表数据操作
            this.temp.code = res.code
            this.temp.creationTime = res.creationTime
            this.temp.id = res.id
            this.list.unshift(this.temp)
            this.setCurrent(this.list[0])

            this.billContainerVisibilty = this.operatorButtonsVisibilty = true
            this.readonly = true
            this.$notify({
              title: '成功',
              message: '创建成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    getDetail(id) {
      // if (this.temp.purchaseDetails !== null) {
      //   return
      // }
      getPurchaseHeaderById(id).then((res) => {
        console.log('getPurchaseHeaderById', res)
        this.temp = Object.assign({}, this.currentRow) // copy obj

        const items = res.purchaseDetails
        this.temp.purchaseDetails = items.map(v => {
          this.$set(v, 'edit', false) // https://vuejs.org/v2/guide/reactivity.html
          // v.originalTitle = v.title //  will be used when user click the cancel botton
          return v
        })
        // this.temp.purchaseDetails = res.purchaseDetails // copy obj
        console.log('temp', this.temp)
      })
    },
    handleUpdate() {
      console.log('currentRow', this.currentRow.id)
      // 避免点击按钮重复请求

      this.getDetail(this.currentRow.id)
      this.readonly = false

      this.billContainerVisibilty = this.operatorButtonsVisibilty = false
      this.detailFormStatus = 'update'

      this.$nextTick(() => {
        this.$refs['detailForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['detailForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)

          updatePurchaseHeader(tempData).then(() => {
            for (const v of this.list) {
              if (v.id === this.temp.id) {
                const index = this.list.indexOf(v)
                this.list.splice(index, 1, this.temp)
                this.setCurrent(this.list[index])
                break
              }
            }
            this.billContainerVisibilty = this.operatorButtonsVisibilty = true
            this.readonly = true

            this.$notify({
              title: '成功',
              message: '修改成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleDelete() {
      this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deletePurchaseHeader(this.currentRow.id)
          .then((response) => {
            const index = this.list.findIndex((v) => v.id === this.currentRow.id)
            this.list.splice(index, 1)
            this.$notify({
              title: '成功',
              message: '删除成功',
              type: 'success',
              duration: 2000
            })
          }).catch((err) => {
            console.log(err)
          })
      }).catch((err) => {
        console.log(err)
      })
    },

    handleAudited() {
      console.log('audit')
      var obj = { key: 'audited', type: 'info', label: '审核' }
      for (const v of this.purchaseStatus) {
        if (v.key === 'waitAudited') {
          console.log('v.key', v.key)
          const index = this.purchaseStatus.indexOf(v)
          this.purchaseStatus.splice(index, 1, obj)
          break
        }
      }
    },
    handleBatch(command) {
      var selection = this.$refs.billTable.selection
      this.billContainerVisibilty = true
      if (selection.length === 0) {
        this.$message({
          message: '请选中要处理的数据',
          type: 'success'
        })
        return
      }
      console.log('selection', selection)
      switch (command) {
        case 'batchDelete':
          this.batchDelete()
          break
        default:
          break
      }
    },
    batchDelete() {
      this.$message({
        message: '批量删除成功',
        type: 'success'
      })
    },
    handleCommand(command) {
      switch (command) {
        case 'handleExport':
          this.handleExport()
          break
        default:
          break
      }
      this.$message('click on item ' + command)
    },

    selectSupply(val) {
      console.log('val', val)
      this.temp.supplyId = val.id
      this.temp.supplyName = val.name
    },

    selectProducts(rows) {
      console.log('rows', rows)
      var index = this.temp.purchaseDetails.length
      // const index = this.temp.purchaseDetails.indexOf(row)
      rows.forEach(row => {
        // this.$refs.productTable.toggleRowSelection(row)
        var tempDetail = {
          productSn: row.productSn,
          unit: row.unit,
          processStamp: '',
          productId: row.id,
          product: {
            name: row.name,
            productSn: row.productSn,
            unit: row.name,
            categoryName: row.categoryName
          },
          id: undefined,
          edit: false

        }

        index++
        this.temp.purchaseDetails.splice(index, 0, tempDetail)
      })
      // this.temp.supplyId = val.id
      // this.temp.supplyName = val.name
    },

    // // 添加明细
    handleCreateDetail(row) {
      // this.temp.purchaseDetails
      const index = this.temp.purchaseDetails.indexOf(row)
      // 拼接函数(索引位置, 要删除元素的数量, 元素)
      // this.temp.purchaseDetails.unshift(this.temp.purchaseDetails[0])
      console.log('this.temp.purchaseDetails[0]', this.temp.purchaseDetails[0])
      console.log('this.temp.purchaseDetails', this.temp.purchaseDetails)
      this.resetTempDetail()
      this.temp.purchaseDetails.splice(index + 1, 0, this.tempDetail)
    },
    // 删除明细
    handleDeleteDetail(row) {
      const index = this.temp.purchaseDetails.indexOf(row)
      this.temp.purchaseDetails.splice(index, 1)
    },
    // 确认编辑明细
    confirmEditDetail(row) {
      row.edit = false
      // row.originalTitle = row.title
      this.$message({
        message: '当前行已修改',
        type: 'success'
      })
    },
    // 导出模板
    handleDownload() {
      this.downloadLoading = true
      console.log('this.temp.purchaseDetails', this.temp.purchaseDetails)
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['货号', '分类', '商品名称', '单位', '数量', '处理标记']
        const filterVal = ['productSn', 'product.categoryName', 'product.name', 'unit', 'totalQty', 'processStamp']
        //  this.temp.purchaseDetails
        const data = this.formatJson(filterVal, [])
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: '采购单模板-' + (moment(new Date()).format('YYYYMMDD-HHmmss'))
        })
        this.downloadLoading = false
      })
    },
    // 导入
    handleImport() {
      this.importExcelDialogVisible = true
      console.log('导入数据')
    },
    beforeUpload(file) {
      const isLt10M = file.size / 1024 / 1024 < 10

      if (isLt10M) {
        return true
      }

      this.$message({
        message: '请不要上传大于10MB的文件',
        type: 'warning'
      })
      return false
    },
    // 导出Excel
    handleExport() {
      // console.log('handleExport')
      this.getDetail(this.currentRow.id)
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['货号', '分类', '商品名称', '单位', '数量', '处理标记']
        const filterVal = ['productSn', 'product.categoryName', 'product.name', 'product.unit', 'totalQty', 'processStamp']
        const data = this.formatJson(filterVal, this.temp.purchaseDetails)
        // console.log('data', data)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: '采购单明细-' + (moment(new Date()).format('YYYYMMDD-HHmmss'))
        })
        this.downloadLoading = false
      })
    },
    // 导出Excel 成功处理
    handleSuccess({ results, header }) {
      this.$message({
        message: '文件上传成功',
        type: 'success'
      })
      // console.log('results', results)
      // console.log('header', header)

      // this.temp.purchaseDetails
      // const index = this.temp.purchaseDetails.indexOf(row)
      var index = 0
      // 拼接函数(索引位置, 要删除元素的数量, 元素)
      // this.temp.purchaseDetails.unshift(this.temp.purchaseDetails[0])
      // console.log('this.temp.purchaseDetails[0]', this.temp.purchaseDetails[0])
      // console.log('this.temp.purchaseDetails', this.temp.purchaseDetails)
      // this.resetTempDetail()

      // var tempDetail = Object.assign({}, this.tempDetail)
      // var tempDetail = {}
      results.forEach(v => {
        index++
        // console.log('v["单位"]', v['单位'], '-----v', v)
        // 导入数据的对象每次都要重置  原因？？
        this.resetTempDetail()
        this.tempDetail.unit = v['单位']
        this.tempDetail.productSn = v['货号']
        this.tempDetail.product.name = v['商品名称']
        this.tempDetail.totalQty = v['数量']
        // console.log('this.tempDetail.product.name', this.tempDetail.product.name)
        // console.log('this.tempDetail.productSn', this.tempDetail.productSn)
        // console.log('this.tempDetail', this.tempDetail)
        // console.log('this.temp.purchaseDetails', this.temp.purchaseDetails)
        // this.temp.purchaseDetails.push(tempDetail)
        this.temp.purchaseDetails.splice(index + 1, 0, this.tempDetail)
      })

      // this.temp.purchaseDetails = results
      // this.tableHeader = header
    },

    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        // console.log('v', v)
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else if (j.indexOf('product.') !== -1) {
          j = j.replace(/product./, '')
          return v.product[j]
        } else {
          return v[j]
        }
      }))
    },
    handleOnDev() {
      this.$message({
        message: '开发中。。。',
        type: 'success'
      })
    }

  }
}
</script>

<style scoped>
.cancel-btn {
  position: absolute;
  right: 15px;
  top: 10px;
}
.filter-container{
  margin-bottom: 5px;
  position:relative;

}
.search-container{
  position:absolute;
  border:rgb(230, 217, 217) solid 1px;
  padding:30px 20px;
  left:0px;
  top:40px;
  width:100%;
  background-color:rgb(255, 255, 255);
  z-index:99;
  display:block;
}

.bill-detail-container{
  width:100%;
  min-height:620px;
}
</style>

