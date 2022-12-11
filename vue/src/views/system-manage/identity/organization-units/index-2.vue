<template>
<div class="app-container">
    <el-row :gutter="20">
        <el-col :span="10">
            <div class="grid-content">

                <el-row>
                    <el-col :span="20">
                        <div class="grid-content">组织机构树</div>
                    </el-col>
                    <el-col :span="4">
                        <div class="grid-content">
                            <el-button type="primary">添加根机构</el-button>
                        </div>
                    </el-col>
                </el-row>

                <el-row>
                    <div class="block">
                        <!-- <p>使用 scoped slot</p> -->
                        <el-tree :data="data" show-checkbox node-key="id" default-expand-all :expand-on-click-node="false" @node-drag-start="handleDragStart" @node-drag-enter="handleDragEnter" @node-drag-leave="handleDragLeave" @node-drag-over="handleDragOver" @node-drag-end="handleDragEnd" @node-drop="handleDrop" draggable :allow-drop="allowDrop" :allow-drag="allowDrag">
                            <span class="custom-tree-node" slot-scope="{ node, data }">
                                <span>{{ node.label }}</span>
                                <span>
                                    <el-button type="text" @click="() => update(data)">
                                        编辑
                                    </el-button>
                                    <el-button type="text" @click="() => append(data)">
                                        添加子组织
                                    </el-button>
                                    <el-button type="text" @click="() => remove(node, data)">
                                        删除
                                    </el-button>
                                </span>
                            </span>
                        </el-tree>
                    </div>
                </el-row>

            </div>
        </el-col>
        <el-col :span="14">
            <div class="grid-content">

                <el-tabs type="border-card">
                    <el-tab-pane label="成员">
                        <el-row>
                            <el-col :span="20">
                                <h3 class="grid-content">组织机构名称</h3>
                            </el-col>
                            <el-col :span="4">
                                <el-button type="primary">添加成员</el-button>
                            </el-col>
                        </el-row>
                        <template>
                            <el-table :data="tableData" style="width: 100%" :row-class-name="tableRowClassName">
                                <el-table-column prop="date" label="日期" width="180">
                                </el-table-column>
                                <el-table-column prop="name" label="姓名" width="180">
                                </el-table-column>
                                <el-table-column prop="address" label="地址">
                                </el-table-column>
                            </el-table>
                        </template>
                    </el-tab-pane>

                    <el-tab-pane label="角色">
                        <el-row>
                            <el-col :span="20">
                                <h3 class="grid-content">组织机构名称</h3>
                            </el-col>
                            <el-col :span="4">
                                <el-button type="primary">添加角色</el-button>
                            </el-col>
                        </el-row>
                        <template>
                            <el-table :data="tableData" height="250" border style="width: 100%">
                                <el-table-column prop="date" label="日期" width="180">
                                </el-table-column>
                                <el-table-column prop="name" label="姓名" width="180">
                                </el-table-column>
                                <el-table-column prop="address" label="地址">
                                </el-table-column>
                            </el-table>
                        </template>
                    </el-tab-pane>

                </el-tabs>
            </div>
        </el-col>
    </el-row>
</div>
</template>

<script>
export default {
    data() {
        const data = [{
            id: 1,
            label: '一级 1',
            children: [{
                id: 4,
                label: '二级 1-1',
                children: [{
                    id: 9,
                    label: '三级 1-1-1'
                }, {
                    id: 10,
                    label: '三级 1-1-2'
                }]
            }]
        }, {
            id: 2,
            label: '一级 2',
            children: [{
                id: 5,
                label: '二级 2-1'
            }, {
                id: 6,
                label: '二级 2-2'
            }]
        }, {
            id: 3,
            label: '一级 3',
            children: [{
                id: 7,
                label: '二级 3-1'
            }, {
                id: 8,
                label: '二级 3-2'
            }]
        }];

        const tableData = [{
            date: '2016-05-02',
            name: '王小虎',
            address: '上海市普陀区金沙江路 1518 弄',
        }, {
            date: '2016-05-04',
            name: '王小虎',
            address: '上海市普陀区金沙江路 1518 弄'
        }, {
            date: '2016-05-01',
            name: '王小虎',
            address: '上海市普陀区金沙江路 1518 弄',
        }, {
            date: '2016-05-03',
            name: '王小虎',
            address: '上海市普陀区金沙江路 1518 弄'
        }];
        return {
            blank: {

            },
            data: JSON.parse(JSON.stringify(data)),
            tableData: tableData
        }
    },
    methods: {
        onSubmit() {
            this.$message('submit!')
        },
        onCancel() {
            this.$message({
                message: 'cancel!',
                type: 'warning'
            })
        },

        update(data) {
            const newChild = {
                id: id++,
                label: 'testtest',
                children: []
            };
            if (!data.children) {
                this.$set(data, 'children', []);
            }
            data.children.push(newChild);
        },
        append(data) {
            const newChild = {
                id: id++,
                label: 'testtest',
                children: []
            };
            if (!data.children) {
                this.$set(data, 'children', []);
            }
            data.children.push(newChild);
        },

        remove(node, data) {
            const parent = node.parent;
            const children = parent.data.children || parent.data;
            const index = children.findIndex(d => d.id === data.id);
            children.splice(index, 1);
        },

        // renderContent(h, { node, data, store }) {
        //   return (
        //     <span class="custom-tree-node">
        //       <span>{node.label}</span>
        //       <span>
        //         <el-button size="mini" type="text" on-click={ () => this.append(data) }>Append</el-button>
        //         <el-button size="mini" type="text" on-click={ () => this.remove(node, data) }>Delete</el-button>
        //       </span>
        //     </span>);
        //}
        handleDragStart(node, ev) {
            console.log('drag start', node);
        },
        handleDragEnter(draggingNode, dropNode, ev) {
            console.log('tree drag enter: ', dropNode.label);
        },
        handleDragLeave(draggingNode, dropNode, ev) {
            console.log('tree drag leave: ', dropNode.label);
        },
        handleDragOver(draggingNode, dropNode, ev) {
            console.log('tree drag over: ', dropNode.label);
        },
        handleDragEnd(draggingNode, dropNode, dropType, ev) {
            console.log('tree drag end: ', dropNode && dropNode.label, dropType);
        },
        handleDrop(draggingNode, dropNode, dropType, ev) {
            console.log('tree drop: ', dropNode.label, dropType);
        },
        allowDrop(draggingNode, dropNode, type) {
            if (dropNode.data.label === '二级 3-1') {
                return type !== 'inner';
            } else {
                return true;
            }
        },
        allowDrag(draggingNode) {
            return draggingNode.data.label.indexOf('三级 3-2-2') === -1;
        },

        // table
        tableRowClassName({
            row,
            rowIndex
        }) {
            if (rowIndex === 1) {
                return 'warning-row';
            } else if (rowIndex === 3) {
                return 'success-row';
            }
            return '';
        }
    }
}
</script>

<style lang="scss" scoped>
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

.el-table .warning-row {
    background: oldlace;
}

.el-table .success-row {
    background: #f0f9eb;
}
.el-tab-pane .h3{
  margin: 0;
}
</style>
