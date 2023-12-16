<template>
  <div>
    <el-button icon="el-icon-folder-add" type="primary" style="width:100%;margin:15px 0px;" @click="handleCreateFolder">创建文件夹</el-button>
    <el-tree ref="tree" :data="folders" lazy :load="fetchChildren" node-key="name" :props="defaultProps" @node-click="handleNodeClick">
      <span slot-scope="{ node, data }" class="custom-tree-node">
        <span :name="node.label">
          <i v-if="data.children.length > 0" :class="node.expanded ? 'el-icon-folder-opened' : 'el-icon-folder'" />
          <i v-else class="el-icon-folder" />
          <b style="margin-left:5px;">{{ node.label }}</b>
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
      default: ''
    },
    parentBreadLabel: { // 面包屑文字  通过函数类型属性给父组件传值
      type: Function,
      require: true,
      default: null
    },
    // 父级获取列表方法
    getList: {
      type: Function,
      require: true,
      default: null
    }
  },
  data() {
    return {
      breadList: [], // 面包屑数组
      breadLabel: '',
      data: [],
      folders: [
        {
          label: '根目录',
          value: '根目录',
          key: './',
          name: '根目录',
          title: '根目录',
          path: '',
          children: []
        }
      ],
      defaultProps: {
        label: 'name', // 将获取数组中的name作为显示节点（label）进行展示
        // value: '根目录',
        // key: './',
        // name: '根目录',
        // title: '根目录',
        // path: '',
        children: 'children' // 将获取数组中的children作为子节点（children）的展示

      },
      // 解决懒加载刷新问题
      rootNode: undefined,
      rootResolve: undefined,
      selectKeys: '' // 保存选中的值
    }
  },
  watch: {
    // bucket属性值变化的时候调用
    bucket() {
      if (this.bucket) {
        // `
        this.rootNode.childNodes = []// 把存起来的node的子节点清空，不然会界面会出现重复树！
        this.fetchChildren(this.rootNode, this.rootResolve)// 再次执行懒加载的方法
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
        // console.log(value)
        const folderName = value.substr(-1) === '/' ? value : value + '/'
        // TODO: 根据选中的节点来获取 path的值
        var path = this.selectKeys
        this.CreateFolder(this.bucket, path, folderName)
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

    /**
       * 懒加载方法的两个参数
       * 当前节点（node）的信息（包括它的层级数据等）
       * 另一个是一个重新渲染当前节点下子节点的方法（resolve），它接收一个数组，该数组也会按照props中的映射关系
       * */
    fetchChildren(treeNode, resolve) {
      // console.log('treeNode', treeNode, 'resolve', resolve)

      // 点击不同的选项加载不同的树 https://blog.csdn.net/qq_34092675/article/details/100942064
      if (treeNode.level === 0) {
        this.rootNode = treeNode// 这里是关键！在data里面定义一个变量，将node.level == 0的node存起来
        this.rootResolve = resolve// 同上，把node.level == 0的resolve也存起来
      }
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
        // console.log('fs', fs)
        // if (!treeNode.data) {
        //   treeNode.data = fs
        // } else {
        //   treeNode.data.children = fs
        // }
        // 懒加载获取的节点数据没有挂载到 folders这个数组上
        resolve(fs)
      }).catch(() => {
        resolve([])
      })
    },

    // 选择节点
    handleNodeClick(data) {
      // 获取面包屑
      this.breadList = []
      // 异步请求的数据需要放到$nextTick方法里面调用，不然渲染不出来
      this.$nextTick(() => {
        this.getTreeNode(this.$refs['tree'].getNode(data.name))
        // 调用父级获取列表方法
        this.getList(true, data.key)
        this.parentBreadLabel(this.breadLabel)

        this.selectKeys = data.key
      })
    },
    // 获取当前树节点和其父级节点
    getTreeNode(node) {
      if (node && node.label) {
        this.breadList.unshift(node.label)
        this.getTreeNode(node.parent) // 递归
        this.breadLabel = this.breadList.join('>')
      }
    }

  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

