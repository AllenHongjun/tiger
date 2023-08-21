<template>
  <div>
    <el-button icon="el-icon-folder-add" type="primary" style="width:100%;margin:15px 0px;" @click="handleCreateFolder">创建文件夹</el-button>
    <el-tree :load="fetchChildren" :lazy="true" :props="defaultProps" @node-click="handleSelectChange">
      <span slot-scope="{ node, data }" class="custom-tree-node">
        <span :name="node.label">
          <i
            v-if="data.children.length>0"
            :class="node.expanded ? 'el-icon-folder-opened' : 'el-icon-folder'"
          />
          <i v-else class="el-icon-folder" />{{ node.label }}
        </span>
      </span>
    </el-tree>

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
  createObject

} from '@/api/system-manage/oss/object'

export default {
  name: 'FolderTree',
  props: {
    bucket: {
      type: String,
      default: 'tiger-blob'
    },
    getList: {
      type: Function,
      require: true,
      default: null
    }
  },
  data() {
    return {
      folders: [
        {
          label: './',
          value: './',
          title: 'Objects:Root',
          key: './',
          path: '',
          name: '',
          children: []
        }
      ],

      defaultProps: {
        children: 'children', // 将获取数组中的children作为子节点（children）的展示
        label: 'name' // 将获取数组中的name作为显示节点（label）进行展示
      }
    }
  },
  watch: {
    // bucket属性值变化的时候调用
    bucket() {
      if (this.bucket) {
        this.fetchFolders(this.bucket).then((fs) => {
          // this.folders[0].children = fs
        })
      }
    },
    immediate: true // watch侦听操作内的函数会立刻被执行
  },
  methods: {
    // 打开创建目录弹框
    handleCreateFolder() {
      this.$prompt('请输入目录名称', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        inputPattern: /[a-z]|[A-Z]:(\\[^\\/&?\n]+)\\?/,
        inputErrorMessage: '目录格式不正确，不能包含下面字符\/:*?"<>|  '
      }).then(({ value }) => {
        console.log(value)
        const folderName = value.substr(-1) === '/' ? value : value + '/'
        // TODO: 根据选中的节点来获取 path的值
        this.CreateFolder(this.bucket, 'test1/', folderName)
        this.$message({
          type: 'success',
          message: '你的目录是: ' + folderName
        })
      }).catch((res) => {
        this.$message({
          type: 'info',
          message: '取消输入' + res
        })
      })
    },
    // 创建目录
    CreateFolder(bucket, path, name) {
      const formData = new FormData()
      formData.append('Bucket', bucket)
      formData.append('Path', path)
      formData.append('FileName', name)
      // formData.append('ExpirationTime', undefined)
      formData.append('File', undefined)// 拿到存在fileList的文件存放到formData中
      createObject(formData).then(() => {
        // ToDo: 将对象添加到树节点中
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    fetchFolders(bucket, path) {
      return new Promise((resolve) => {
        var req = {
          bucket: bucket,
          prefix: path ?? '',
          delimiter: '/',
          marker: '',
          encodingType: '',
          sorting: '',
          skipCount: 0,
          maxResultCount: 1000
        }
        getContainerObject(req).then((res) => {
          const fs = res.objects
            .filter((item) => item.isFolder)
            .map((item) => {
              return {
                label: item.name,
                value: item.name,
                key: `${item.path ?? ''}${item.name}`,
                name: item.name,
                title: item.name,
                path: item.path,
                children: []
              }
            })
          return resolve(fs)
        }).catch(() => {
          return resolve([])
        })
      })
    },

    // 先加载树节点的文件夹，然后加载叶节点的文件。
    /**
       * 懒加载方法的两个参数
       * 当前节点（node）的信息（包括它的层级数据等）
       * 另一个是一个重新渲染当前节点下子节点的方法（resolve），它接收一个数组，该数组也会按照props中的映射关系
       * */
    fetchChildren(treeNode, resolve) {
      if (!this.bucket) {
        resolve([])
        return
      }

      if (treeNode.data?.children?.length > 0) {
        resolve([])
        return
      }
      let path = ''
      if (treeNode.data?.path) {
        path = path + treeNode.data?.path
      }
      if (treeNode.data?.name) {
        path = path + treeNode.data?.name
      }
      this.fetchFolders(this.bucket, path).then((fs) => {
        if (!treeNode.data) {
          treeNode.data = fs
        } else {
          treeNode.data.children = fs
        }

        this.folders = [...this.folders]
        resolve(fs)
      }).catch(() => {
        resolve()
      })
    },
    handleSelectChange(data, resolve) {
      console.log(data)
      // 子组件调用父组件方法
      this.getList(true, data.key)
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

<style scoped>
.line{
  text-align: center;
}
</style>

