<template>
  <div class="createPost-container">

    <el-form ref="postForm" :model="postForm" :rules="rules" class="form-container">

      <div class="createPost-main-container">
        <div class="postInfo-container">
          <el-tabs v-model="activeName" @tab-click="tabClick">
            <el-tab-pane label="商品信息" name="1">

              <el-form-item label="商品标题:" label-width="120px">
                <el-input class="form-input" placeholder="请输入商品标题" />
              </el-form-item>
              <el-form-item label="单位:" label-width="120px">
                <el-input class="form-input" placeholder="请输入订单" />
              </el-form-item>

              <el-form-item label="商品简介:" style="margin-bottom: 40px;" label-width="120px">
                <el-input v-model="postForm.content_short" :rows="3" type="textarea" class="form-input" placeholder="请输入商品简介" />
                <!-- <span v-show="contentShortLength" class="word-counter">{{ contentShortLength }}words</span> -->
              </el-form-item>

              <el-form-item label="商品封面图:" label-width="120px" prop="image_uri" style="margin-bottom: 30px;">
                <Upload v-model="postForm.image_uri" />
              </el-form-item>

              <el-form-item label="用户标签:" label-width="120px">
                <el-select v-model="userTagValue" placeholder="请选择">
                  <el-option
                    v-for="item in userTagOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
              </el-form-item>

              <el-form-item label="" label-width="120px">
                <el-button type="primary" @click="nextStep">
                  下一步
                </el-button>
              </el-form-item>

            </el-tab-pane>

            <el-tab-pane label="商品详情" name="2">
              <el-form-item label="商品明细" label-width="120px" prop="content" style="margin-bottom: 30px;">
                <Tinymce ref="editor" v-model="postForm.content" :height="400" />
              </el-form-item>

              <!-- <el-form-item label="虚拟销量" label-width="120px" prop="image_uri" style="margin-bottom: 30px;">
                <Upload v-model="postForm.image_uri" />
              </el-form-item> -->

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
                  <el-form-item label="虚拟销量" label-width="120px" class="postInfo-container-item">
                    <el-input />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="额外赠送积分" label-width="120px" class="postInfo-container-item">
                    <el-input />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="排序" label-width="120px" class="postInfo-container-item">
                    <el-input />
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item label="商品状态" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.productStatus" label="1">上架</el-radio>
                    <el-radio v-model="postForm.productStatus" label="2">下架</el-radio>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="热卖单品" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.selling" label="1">开启</el-radio>
                    <el-radio v-model="postForm.selling" label="2">关闭</el-radio>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="促销单品" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.promotion" label="1">开启</el-radio>
                    <el-radio v-model="postForm.promotion" label="2">关闭</el-radio>
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-col :span="8">
                  <el-form-item label="精品推荐" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.recommend" label="1">上架</el-radio>
                    <el-radio v-model="postForm.recommend" label="2">下架</el-radio>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label="首发新品" label-width="120px" class="postInfo-container-item">
                    <el-radio v-model="postForm.new" label="1">开启</el-radio>
                    <el-radio v-model="postForm.new" label="2">关闭</el-radio>
                  </el-form-item>
                </el-col>

              </el-row>

              <el-form-item label="商品口令:" label-width="120px">
                <el-input class="form-input" placeholder="请输入订单" />
              </el-form-item>

              <el-form-item label="" label-width="120px">
                <el-button type="primary" @click="preStep">
                  上一步
                </el-button>
                <el-button type="primary">
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
import { fetchArticle } from '@/api/article'
import { searchUser } from '@/api/remote-search'
import Warning from './Warning'
import { CommentDropdown, PlatformDropdown, SourceUrlDropdown } from './Dropdown'

const defaultForm = {
  status: 'draft',
  title: '', // 商品标题
  content: '', // 商品详情
  content_short: '', // 文章摘要
  source_uri: '', // 文章外链
  image_uri: '', // 文章图片
  display_time: undefined, // 前台展示时间
  id: undefined,
  platforms: ['a-platform'],
  comment_disabled: false,
  importance: 0,
  productStatus: '1',
  selling: '1',
  promotion: '1',
  recommend: '1',
  new: '1'

}

export default {
  name: 'ArticleDetail',
  components: { Tinymce, MDinput, Upload, Sticky, Warning, CommentDropdown, PlatformDropdown, SourceUrlDropdown },
  props: {
    isEdit: {
      type: Boolean,
      default: false
    }
  },
  data() {
    const validateRequire = (rule, value, callback) => {
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
        image_uri: [{ validator: validateRequire }],
        title: [{ validator: validateRequire }],
        content: [{ validator: validateRequire }],
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
      userTagValue: ''
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
      fetchArticle(id).then(response => {
        this.postForm = response.data

        // just for test
        this.postForm.title += `   Article Id:${this.postForm.id}`
        this.postForm.content_short += `   Article Id:${this.postForm.id}`

        // set tagsview title
        this.setTagsViewTitle()

        // set page title
        this.setPageTitle()
      }).catch(err => {
        console.log(err)
      })
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
      console.log('num', num, 'tab-length', this.tabs.length)
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
      console.log(this.postForm)
      this.$refs.postForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$notify({
            title: '成功',
            message: '发布文章成功',
            type: 'success',
            duration: 2000
          })
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
