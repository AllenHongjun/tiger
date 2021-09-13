<template>
  <div class="createPost-container">

    <el-form ref="postForm" :model="postForm" :rules="rules" class="form-container">

      <div class="createPost-main-container">
        <div class="postInfo-container">
          <el-tabs v-model="activeName" @tab-click="tabClick">
            <el-tab-pane label="商品信息" name="1">
              <el-form-item label="分类" prop="" label-width="120px">
                <category-cascader ref="CategoryCascader" @handleCheckChange="handleCheckChange" />
              </el-form-item>

              <el-form-item label="商品标题:" label-width="120px" prop="name">
                <el-input v-model="postForm.name" class="form-input" placeholder="请输入商品标题" />
              </el-form-item>

              <el-form-item label="商品副标题:" label-width="120px" prop="subTitle">
                <el-input v-model="postForm.subTitle" class="form-input" placeholder="请输入商品副标题" />
              </el-form-item>

              <el-form-item label="商品简介:" style="margin-bottom: 40px;" label-width="120px" prop="detailTitle">
                <el-input v-model="postForm.detailTitle" :rows="3" type="textarea" class="form-input" placeholder="请输入商品简介" />
              </el-form-item>

              <el-row>
                <el-col :span="8">
                  <el-form-item label="商品货号:" label-width="120px" prop="productSn">
                    <el-input v-model="postForm.productSn" class="form-input" placeholder="请输入商品货号" />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="单位:" label-width="120px">
                    <el-input v-model="postForm.unit" class="form-input" placeholder="请输入订单" />
                  </el-form-item>
                </el-col>
                <!-- <el-col :span="8">
                  <el-form-item label="采购价" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.purchasePrice" />
                  </el-form-item>
                </el-col> -->
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item label="原价" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.oriPrice" type="number" />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="销售价" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.price" />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="采购价" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.purchasePrice" />
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item label="库存" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.stock" type="number" />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="预警库存" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.lowStock" />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="重量" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.weight" />
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item label="发布" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.publishStatus" :label="1">开启</el-radio>
                    <el-radio v-model="postForm.publishStatus" :label="0">关闭</el-radio>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="新品" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.newStatus" :label="1">是</el-radio>
                    <el-radio v-model="postForm.newStatus" :label="0">否</el-radio>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="推荐" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.recommandStatus" :label="1">开启</el-radio>
                    <el-radio v-model="postForm.recommandStatus" :label="0">关闭</el-radio>
                  </el-form-item>
                </el-col>
              </el-row>

              <el-form-item label="商品封面图:" label-width="120px" prop="picture" style="margin-bottom: 30px;">
                <Upload v-model="postForm.picture" />
              </el-form-item>

              <el-form-item label="多规格:" label-width="120px" prop="picture" style="margin-bottom: 30px;">
                <SkuForm
                  :source-attribute="sourceAttribute"
                  :attribute.sync="attribute"
                  :sku.sync="sku"
                  separator=","
                  :theme="2"
                />
                <el-row type="flex" :gutter="20">
                  <el-col>
                    <el-divider content-position="left">attribute 数据</el-divider>
                    <pre><code>{{ attribute }}</code></pre>
                  </el-col>
                  <el-col>
                    <el-divider content-position="left">sku 数据</el-divider>
                    <pre><code>{{ sku }}</code></pre>
                  </el-col>
                </el-row>
              </el-form-item>

              <!-- <el-form-item label="用户标签:" label-width="120px">
                <el-select v-model="userTagValue" placeholder="请选择">
                  <el-option
                    v-for="item in userTagOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
              </el-form-item> -->

              <el-form-item label="" label-width="120px">
                <el-button type="primary" @click="nextStep">
                  下一步
                </el-button>
              </el-form-item>

            </el-tab-pane>

            <el-tab-pane label="商品详情" name="2">
              <el-form-item label="商品明细" label-width="120px" prop="detailDesc" style="margin-bottom: 30px;">
                <Tinymce ref="editor" v-model="postForm.detailDesc" :height="600" />
              </el-form-item>

              <el-form-item label="" label-width="120px">
                <el-button type="primary" @click="preStep">
                  上一步
                </el-button>
                <el-button type="primary" @click="nextStep">
                  下一步
                </el-button>
              </el-form-item>
            </el-tab-pane>

            <el-tab-pane label="其他设置" name="3">
              <el-row>
                <el-col :span="8">
                  <el-form-item label="成长值" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.giftGrowth" />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="赠送积分" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.giftIntegration" />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="排序" label-width="120px" class="postInfo-container-item">
                    <el-input v-model="postForm.sort" />
                  </el-form-item>
                </el-col>
              </el-row>

              <el-form-item label="" label-width="120px">
                <el-button type="primary" @click="preStep">
                  上一步
                </el-button>
                <el-button type="primary" @click="submitForm">
                  保存
                </el-button>
              </el-form-item>

            </el-tab-pane>
          </el-tabs>
        </div>
      </div>
    </el-form>
  </div>
</template>

<script>
import Tinymce from '@/components/Tinymce'
import Upload from '@/components/Upload/SingleImage3'
import MDinput from '@/components/MDinput'
import Sticky from '@/components/Sticky' // 粘性header组件
import { validURL } from '@/utils/validate'
import { searchUser } from '@/api/remote-search'
import Warning from './Warning'
import { CommentDropdown, PlatformDropdown, SourceUrlDropdown } from './Dropdown'
import { getProductById, createProduct, updateProduct } from '@/api/basic/product'
import CategoryCascader from '@/views/basic/components/category-cascader'
import SkuForm from 'vue-sku-form'

const defaultForm = {
  id: undefined,
  productCategoryId: '',
  productAttributeTypeId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
  name: '',
  productSn: '',
  publishStatus: 0,
  newStatus: 0,
  recommandStatus: 0,
  verifyStatus: 0,
  sort: 0,
  sale: 0,
  note: '',
  picture: '',
  albumPics: '',
  detailTitle: '',
  subTitle: '',
  unit: '',
  keywords: '',
  detailDesc: '',
  price: 0,
  oriPrice: 0,
  purchasePrice: 0,
  promotionPrice: 0,
  giftGrowth: 0,
  giftIntegration: 0,
  stock: 0,
  lowStock: 0,
  weight: 0,
  previewStatus: 0,
  promotionType: 0,
  promotionStartTime: '2021-08-29T03:52:34.314Z',
  promotionEndTime: '2021-08-29T03:52:34.314Z',
  promotionPerLimit: 0,
  categoryName: 'string'

}

export default {
  name: 'ArticleDetail',
  components: { Tinymce, MDinput, Upload, Sticky, Warning, CommentDropdown, PlatformDropdown, SourceUrlDropdown, CategoryCascader, SkuForm },
  props: {
    isEdit: {
      type: Boolean,
      default: false
    }
  },
  data() {
    const validateRequire = (rule, value, callback) => {
      // console.log(rule)
      if (value === '') {
        this.$message({
          message: rule.field + '为必传项',
          type: 'error'
        })
        callback(new Error(rule.field + '为必传项'))
      } else {
        callback()
      }
    }
    const validateSourceUri = (rule, value, callback) => {
      if (value) {
        if (validURL(value)) {
          callback()
        } else {
          this.$message({
            message: '外链url填写不正确',
            type: 'error'
          })
          callback(new Error('外链url填写不正确'))
        }
      } else {
        callback()
      }
    }
    return {
      activeName: '1',
      tabs: ['商品信息', '商品详情', '其他设置'],
      postForm: Object.assign({}, defaultForm),
      loading: false,
      userListOptions: [],
      rules: {
        // image_uri: [{ validator: validateRequire }],
        name: [
          { required: true, message: '请输入商品标题', trigger: 'blur' },
          { min: 3, max: 300, message: '长度在 3 到 300 个字符', trigger: 'blur' },
          { validator: validateRequire }
        ],
        subTitle: [{ validator: validateRequire }],
        detailTitle: [
          { required: true, message: '请输入商品详情标题', trigger: 'blur' },
          { min: 3, max: 300, message: '长度在 3 到 300 个字符', trigger: 'blur' },
          { validator: validateRequire }
        ],
        detailDesc: [
          { validator: validateRequire },
          { required: true, message: '请输入商品详情', trigger: 'blur' }
          // { min: 3, max: 300, message: '长度在 3 到 300 个字符', trigger: 'blur' }
        ],
        productSn: [
          { validator: validateRequire },
          { required: true, message: '请输入商品货号', trigger: 'blur' },
          { min: 3, max: 300, message: '长度在 3 到 300 个字符', trigger: 'blur' }
        ],
        source_uri: [{ validator: validateSourceUri, trigger: 'blur' }]
      },
      tempRoute: {},
      userTagOptions: [
        {
          value: '选项1',
          label: '黄金糕'
        }, {
          value: '选项2',
          label: '双皮奶'
        }, {
          value: '选项3',
          label: '蚵仔煎'
        }, {
          value: '选项4',
          label: '龙须面'
        }, {
          value: '选项5',
          label: '北京烤鸭'
        }
      ],
      userTagValue: '',
      sourceAttribute: [
        {
          name: '颜色',
          item: ['黑', '金', '白']
        },
        {
          name: '内存',
          item: ['16G', '32G']
        },
        {
          name: '尺寸',
          item: ['3.7寸', '4.7寸', '6.3寸']
        },
        {
          name: '像素',
          item: ['1200万', '2000万', '4000万']
        }
      ],
      structure: [
        {
          name: 'price',
          type: 'input',
          label: '价格',
          required: true
        },
        {
          name: 'stock',
          type: 'input',
          label: '库存',
          required: true
        }
      ],
      attribute: [],
      sku: []
    }
  },
  computed: {
    contentShortLength() {
      return this.postForm.content_short.length
    },
    displayTime: {
      // set and get is useful when the data
      // returned by the back end api is different from the front end
      // back end return => "2013-06-25 06:59:25"
      // front end need timestamp => 1372114765000
      get() {
        return (+new Date(this.postForm.display_time))
      },
      set(val) {
        this.postForm.display_time = new Date(val)
      }
    }
  },
  created() {
    if (this.isEdit) {
      const id = this.$route.params && this.$route.params.id
      this.fetchData(id)
    } else {
      this.postForm = Object.assign({}, defaultForm)
    }

    // Why need to make a copy of this.$route here?
    // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
    // https://github.com/PanJiaChen/vue-element-admin/issues/1221
    this.tempRoute = Object.assign({}, this.$route)
  },
  methods: {
    fetchData(id) {
      getProductById(id).then(response => {
        this.postForm = response
        // 设置分类组件的值
        this.$nextTick(() => {
          // console.log('this.postForm.productCategoryId', this.postForm.productCategoryId)
          this.$refs.CategoryCascader.value = this.postForm.productCategoryId
        })
        // console.log('this.postForm', this.postForm)
      }).catch(err => {
        console.log(err)
      })
    },
    handleCheckChange(categoryId) {
      // singleChecked
      // console.log('handleCheckChange', categoryId)
      this.postForm.productCategoryId = categoryId
    },
    tabClick(tab, event) {
      console.log(tab, event)
    },
    preStep() {
      let num = Number(this.activeName)
      num > 0 && num--
      this.activeName = num.toString()
    },
    nextStep() {
      let num = Number(this.activeName)
      num < this.tabs.length && num++
      // console.log('num', num, 'tab-length', this.tabs.length)
      this.activeName = num.toString()
    },
    setTagsViewTitle() {
      const title = 'Edit Article'
      const route = Object.assign({}, this.tempRoute, { title: `${title}-${this.postForm.id}` })
      this.$store.dispatch('tagsView/updateVisitedView', route)
    },
    setPageTitle() {
      const title = 'Edit Article'
      document.title = `${title} - ${this.postForm.id}`
    },
    submitForm() {
      // console.log(this.postForm)
      this.$refs.postForm.validate(valid => {
        if (valid) {
          this.loading = true
          // process.env.VUE_IMG_URL
          // console.log('this.postForm', this.postForm)
          if (this.isEdit) {
            const tempData = Object.assign({}, this.temp)

            updateProduct(this.postForm).then((res) => {
              // console.log('edit-product-res', res)
              // for (const v of this.list) {
              //   if (v.id === this.temp.id) {
              //     const index = this.list.indexOf(v)
              //     this.list.splice(index, 1, this.temp)
              //     break
              //   }
              // }
              this.$notify({
                title: '成功',
                message: '修改成功',
                type: 'success',
                duration: 2000
              })
            })
          } else {
            createProduct(this.postForm).then(() => {
            // this.list.unshift(this.postForm)
            // this.dialogFormVisible = false
              this.$notify({
                title: '成功',
                message: '发布文章成功',
                type: 'success',
                duration: 2000
              })
            })
          }

          this.postForm.status = 'published'
          this.loading = false
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    draftForm() {
      if (this.postForm.content.length === 0 || this.postForm.title.length === 0) {
        this.$message({
          message: '请填写必要的标题和内容',
          type: 'warning'
        })
        return
      }
      this.$message({
        message: '保存成功',
        type: 'success',
        showClose: true,
        duration: 1000
      })
      this.postForm.status = 'draft'
    },
    getRemoteUserList(query) {
      searchUser(query).then(response => {
        if (!response.data.items) return
        this.userListOptions = response.data.items.map(v => v.name)
      })
    }
  }
}
</script>

<style lang="scss" scoped>
@import "~@/styles/mixin.scss";

.createPost-container {
  position: relative;

  .createPost-main-container {
    padding: 40px 45px 20px 50px;

    .postInfo-container {
      position: relative;
      @include clearfix;
      margin-bottom: 10px;

      .postInfo-container-item {
        float: left;
      }
    }
  }

  .word-counter {
    width: 40px;
    position: absolute;
    right: 10px;
    top: 0px;
  }
}

.form-input{
  width:60%;
}

// .article-textarea /deep/ {
//   textarea {
//     padding-right: 40px;
//     resize: none;
//     border: none;
//     border-radius: 0px;
//     border-bottom: 1px solid #bfcbd9;
//   }
// }
</style>
