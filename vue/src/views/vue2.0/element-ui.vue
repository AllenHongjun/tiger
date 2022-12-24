<template>
<div class="dashboard-container">
    <!-- <div class="dashboard-text">{{ name }}</div> -->
    <h2>熟悉elemntUI的组件和布局</h2>

    <el-steps :active="active" finish-status="success">
        <el-step title="步骤 1">1</el-step>
        <el-step title="步骤 2">2</el-step>
        <el-step title="步骤 3">3</el-step>
    </el-steps>
    <el-button style="margin-top: 12px;" @click="next">下一步</el-button>

    <el-form ref="form" :model="form" label-width="80px">
        <el-form-item label="活动名称">
            <el-input v-model="form.name" />
        </el-form-item>
        <el-form-item label="活动区域">
            <el-select v-model="form.region" placeholder="请选择活动区域">
                <el-option label="区域一" value="shanghai" />
                <el-option label="区域二" value="beijing" />
            </el-select>
        </el-form-item>
        <el-form-item label="活动时间">
            <el-col :span="11">
                <el-date-picker v-model="form.date1" type="date" placeholder="选择日期" style="width: 100%;" />
            </el-col>
            <el-col class="line" :span="2">-</el-col>
            <el-col :span="11">
                <el-time-picker v-model="form.date2" placeholder="选择时间" style="width: 100%;" />
            </el-col>
        </el-form-item>
        <el-form-item label="即时配送">
            <el-switch v-model="form.delivery" />
        </el-form-item>
        <el-form-item label="活动性质">
            <el-checkbox-group v-model="form.type">
                <el-checkbox label="美食/餐厅线上活动" name="type" />
                <el-checkbox label="地推活动" name="type" />
                <el-checkbox label="线下主题活动" name="type" />
                <el-checkbox label="单纯品牌曝光" name="type" />
            </el-checkbox-group>
        </el-form-item>
        <el-form-item label="特殊资源">
            <el-radio-group v-model="form.resource">
                <el-radio label="线上品牌商赞助" />
                <el-radio label="线下场地免费" />
            </el-radio-group>
        </el-form-item>
        <el-form-item label="活动形式">
            <el-input v-model="form.desc" type="textarea" />
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="onSubmit">立即创建</el-button>
            <el-button>取消</el-button>
        </el-form-item>
    </el-form>

    <hr>
    基础布局
    <el-row>
        <el-col :span="22">
            <div class="grid-content bg-purple-dark">一行一列</div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="12">
            <div class="grid-content bg-purple">一行两列</div>
        </el-col>
        <el-col :span="12">
            <div class="grid-content bg-purple-light">一行两列</div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="8">
            <div class="grid-content bg-purple">一行三列</div>
        </el-col>
        <el-col :span="8">
            <div class="grid-content bg-purple-light">一行三列</div>
        </el-col>
        <el-col :span="8">
            <div class="grid-content bg-purple" />一行三列</el-col>
    </el-row>
    <el-row>
        <el-col :span="6">
            <div class="grid-content bg-purple">fffff</div>
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple-light">fffff</div>
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple">fffff</div>
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple-light">fffff</div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="4">
            <div class="grid-content bg-purple">一行六列</div>
        </el-col>
        <el-col :span="4">
            <div class="grid-content bg-purple-light">fffff</div>
        </el-col>
        <el-col :span="4">
            <div class="grid-content bg-purple">fffff</div>
        </el-col>
        <el-col :span="4">
            <div class="grid-content bg-purple-light">fffff</div>
        </el-col>
        <el-col :span="4">
            <div class="grid-content bg-purple">fffff</div>
        </el-col>
    </el-row>

    <hr>
    分栏布局
    <el-row :gutter="200">
        <el-col :span="6">
            <div class="grid-content bg-purple">fdsfd</div>
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple">fdsfds</div>
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple">fdsfds</div>
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple">fdsfd</div>
        </el-col>
    </el-row>

    <hr>
    混合布局 配置组件不要删除没有用的后置标签。
    <el-row :gutter="20">
        <el-col :span="16">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="8">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>
    <el-row :gutter="20">
        <el-col :span="8">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="8">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="4">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="4">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>
    <el-row :gutter="20">
        <el-col :span="4">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="16">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="4">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>

    <hr>
    分栏便宜
    分栏使指定 栏目便宜。
    <el-row :gutter="20">
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="6" :offset="6">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>
    <el-row :gutter="20">
        <el-col :span="6" :offset="6">
            <div class="grid-content bg-purple">使第一栏栏目偏移6列</div>
        </el-col>
        <el-col :span="6" :offset="6">
            <div class="grid-content bg-purple">是第二栏目向右偏移六列</div>
        </el-col>
    </el-row>
    <el-row :gutter="20">
        <el-col :span="12" :offset="6">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>

    <hr>
    flex 的对齐方式
    可以启用 flex 布局，并可通过 justify 属性来指定 start, center, end,
    space-between, space-around 其中的值来定义子元素的排版方式。
    <el-row type="flex" class="row-bg">
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple-light" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>
    <el-row type="flex" class="row-bg" justify="center">
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple-light" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>
    <el-row type="flex" class="row-bg" justify="end">
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple-light" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>
    <el-row type="flex" class="row-bg" justify="space-between">
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple-light" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>
    <el-row type="flex" class="row-bg" justify="space-around">
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple-light" />
        </el-col>
        <el-col :span="6">
            <div class="grid-content bg-purple" />
        </el-col>
    </el-row>

    <el-divider>分割线--链接文字</el-divider>

    <div>
        <el-link href="https://element.eleme.io" target="_blank">默认链接</el-link>
        <el-link type="primary">主要链接</el-link>
        <el-link type="success">成功链接</el-link>
        <el-link type="warning">警告链接</el-link>
        <el-link type="danger">危险链接</el-link>
        <el-link type="info">信息链接</el-link>
    </div>
    <!-- 按钮禁用 -->
    <div>
        <el-link disabled>默认链接</el-link>
        <el-link type="primary" disabled>主要链接</el-link>
        <el-link type="success" disabled>成功链接</el-link>
        <el-link type="warning" disabled>警告链接</el-link>
        <el-link type="danger" disabled>危险链接</el-link>
        <el-link type="info" disabled>信息链接</el-link>
    </div>

    <div>
        <el-link icon="el-icon-edit">编辑</el-link>
        <el-link>查看<i class="el-icon-view el-icon--right" /> </el-link>
    </div>
    <el-divider>按钮</el-divider>
    <el-row>
        <el-button>默认按钮</el-button>
        <el-button size="medium">中等按钮</el-button>
        <el-button size="small">小型按钮</el-button>
        <el-button size="mini">超小按钮</el-button>
    </el-row>
    <el-row>
        <el-button round>默认按钮</el-button>
        <el-button size="medium" round>中等按钮</el-button>
        <el-button size="small" round>小型按钮</el-button>
        <el-button size="mini" round>超小按钮</el-button>
    </el-row>
    <el-button type="primary" :loading="true">加载中</el-button>

    <el-button type="primary" icon="el-icon-edit" />
    <el-button type="primary" icon="el-icon-share" />
    <el-button type="primary" icon="el-icon-delete" />
    <el-button type="primary" icon="el-icon-search">搜索</el-button>
    <el-button type="primary">上传<i class="el-icon-upload el-icon--right" /></el-button>

    <el-row>
        <el-button>默认按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="success">成功按钮</el-button>
        <el-button type="info">信息按钮</el-button>
        <el-button type="warning">警告按钮</el-button>
        <el-button type="danger">危险按钮</el-button>
    </el-row>

    <el-row>
        <el-button plain>朴素按钮</el-button>
        <el-button type="primary" plain>主要按钮</el-button>
        <el-button type="success" plain>成功按钮</el-button>
        <el-button type="info" plain>信息按钮</el-button>
        <el-button type="warning" plain>警告按钮</el-button>
        <el-button type="danger" plain>危险按钮</el-button>
    </el-row>

    <el-row>
        <el-button round>圆角按钮</el-button>
        <el-button type="primary" round>主要按钮</el-button>
        <el-button type="success" round>成功按钮</el-button>
        <el-button type="info" round>信息按钮</el-button>
        <el-button type="warning" round>警告按钮</el-button>
        <el-button type="danger" round>危险按钮</el-button>
    </el-row>

    <el-row>
        <el-button icon="el-icon-search" circle />
        <el-button type="primary" icon="el-icon-edit" circle />
        <el-button type="success" icon="el-icon-check" circle />
        <el-button type="info" icon="el-icon-message" circle />
        <el-button type="warning" icon="el-icon-star-off" circle />
        <el-button type="danger" icon="el-icon-delete" circle />
    </el-row>
</div>
</template>

<script>
import {
    mapGetters
} from 'vuex'

export default {
    name: 'Dashboard',
    data() {
        return {
            input: '',
            active: 0,
            form: {
                name: '',
                region: '',
                date1: '',
                date2: '',
                delivery: false,
                type: [],
                resource: '',
                desc: ''
            }
        }
    },
    computed: {
        ...mapGetters([
            'name'
        ])
    },
    methods: {
        next() {
            console.log('next')
            console.log(this.active)
            if (this.active++ > 2) this.active = 0
        },
        onSubmit() {
            console.log('submit')
        }
    }
}
</script>

<style lang="scss" scoped>
.dashboard {
    &-container {
        margin: 30px;
    }

    &-text {
        font-size: 30px;
        line-height: 46px;
    }

}

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
