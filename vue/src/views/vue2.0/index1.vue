<template>
<div class="app-container">

    <el-row>
        <el-col :span="8">
            这是一个故意的空白页面
            <h4>模板语法</h4>
            <span>Message: {{ msg }}</span> <br />

            <span v-once>这个将不会改变: {{ msg }}</span> <br />

            <p>Using mustaches: {{ rawHtml }}</p>
            <p>Using v-html directive: <span v-html="rawHtml"></span></p>

            <div v-bind:id="dynamicId"></div>

            <button v-bind:disabled="isButtonDisabled">Button</button> <br>

            {{ number + 1 }}
            {{ ok ? 'YES' : 'NO' }}
            {{ message.split('').reverse().join('') }}

            <!-- 这是语句，不是表达式 -->
            <!-- {{ var a = 1 }} -->
            <!-- 流控制也不会生效，请使用三元表达式 -->
            <!-- {{ if (ok) { return message } }} -->

            <h5>指令</h5>
            <p v-if="seen">现在你看到我了</p>
            <!-- v-bind 指令可以用于响应式地更新 HTML attribute -->
            <a v-bind:href="url" v-bind:title="url">{{ url }}</a> <br>

            <!-- v-on 指令，它用于监听 DOM 事件： -->
            <a v-on:click="doSomething">v-on 指令</a> <br>

            <!--注意，参数表达式的写法存在一些约束，如之后的“对动态参数表达式的约束”章节所述。-->
            <a v-bind:[attributeName]="url" target="_blank"> ... </a>

            <h5>修饰符</h5>
            <!-- 用于指出一个指令应该以特殊方式绑定。例如，.prevent 修饰符告诉 v-on 指令对于触发的事件调用 event.preventDefault()： -->
            <form v-on:submit.prevent="onSubmit">...</form>

            <h5>缩写</h5>
            <!-- 完整语法 -->
            <a v-bind:href="url">...</a>

            <!-- 缩写 -->
            <a :href="url">...</a>

            <!-- 动态参数的缩写 (2.6.0+) -->
            <a :[key]="url"> ... </a>

            <!-- 完整语法 -->
            <a v-on:click="doSomething">...</a>

            <!-- 缩写 -->
            <a @click="doSomething">...</a>

            <!-- 动态参数的缩写 (2.6.0+) -->
            <a @[event]="doSomething"> ... </a>

            <h4>计算属性</h4>
            <div id="example">
                <p>Original message: "{{ message }}"</p>
                <p>Computed reversed message: "{{ reversedMessage }}"</p>
            </div>

            <h4>Class 与 Style 绑定</h4>
            <div v-bind:class="{ active: isActive }"></div>

            <div class="static" v-bind:class="{ active: isActive, 'text-danger': hasError }">Class 与 Style 绑定</div>

            <div v-bind:class="classObject">通过 classObject 来绑定样式</div>

            <div v-bind:style="{ color: activeColor, fontSize: fontSize + 'px' }">绑定内联样式 对象语法</div>

            <div :style="{ display: ['-webkit-box', '-ms-flexbox', 'flex'] }"></div>

        </el-col>
        <el-col :span="8">
            <!-- 条件渲染 -->
            <h1 v-if="awesome">Vue is awesome!</h1>
            <h1 v-else>Oh no 😢</h1>

            <!-- 在 <template> 元素上使用 v-if 条件渲染分组 -->
            <template v-if="ok">
                <h1>Title</h1>
                <p>Paragraph 1</p>
                <p>Paragraph 2</p>
            </template>

            <div v-if="Math.random() > 0.5">
                Now you see me
            </div>
            <div v-else>
                Now you don't
            </div>

            <h1 v-show="ok">Hello!</h1>

            <!-- 列表渲染 -->
            <ul id="example-1">
                <li v-for="item in items" :key="item.message">
                    {{ item.message }}
                </li>
            </ul>

            <!-- <ul id="example-2">
                <li v-for="(item, index) in items">
                    {{ parentMessage }} - {{ index }} - {{ item.message }}
                </li>
            </ul> -->

            <!-- <ul id="v-for-object" class="demo">
                <li v-for="value in object" :key="value">
                    {{ value }}
                </li>
            </ul> -->

            <!-- <div v-for="(value, name) in object" :key="name">
                {{ name }}: {{ value }}
            </div> -->

            <div v-for="(value, name, index) in object" :key="name">
                {{ index }}. {{ name }}: {{ value }}
            </div>

            <!-- 显示过滤/排序后的结果 -->
            <li v-for="n in evenNumbers" :key="n">{{ n }}</li>

            <!-- 绑定的key是一个string 或 number 类型 一个对象会报错 -->
            <!--<ul v-for="set in sets" :key="set">
                <li v-for="n in even(set)" :key="n">{{ n }}</li>
            </ul> -->

            <div id="example-1">
                <button v-on:click="counter += 1">Add 1</button>
                <p>The button above has been clicked {{ counter }} times.</p>
            </div>

            <!-- <div id="example-2">
                <button v-on:click="greet">Greet</button>
            </div> -->

            <!-- 内联修饰符号 -->
            <!-- 阻止单击事件继续传播 -->
            <a v-on:click.stop="doThis"></a>

            <!-- 提交事件不再重载页面 -->
            <form v-on:submit.prevent="onSubmit"></form>

            <!-- 修饰符可以串联 -->
            <a v-on:click.stop.prevent="doThat"></a>

            <!-- 只有修饰符 -->
            <form v-on:submit.prevent></form>

            <!-- 添加事件监听器时使用事件捕获模式 -->
            <!-- 即内部元素触发的事件先在此处理，然后才交由内部元素进行处理 -->
            <div v-on:click.capture="doThis">...</div>

            <!-- 只当在 event.target 是当前元素自身时触发处理函数 -->
            <!-- 即事件不是从内部元素触发的 -->
            <div v-on:click.self="doThat">...</div>

            <!-- 只有在 `key` 是 `Enter` 时调用 `vm.submit()` -->
            <input v-on:keyup.enter="submit">
        </el-col>
        <el-col :span="8">
            <!-- 表单输入绑定 -->
            <input v-model="message" placeholder="edit me">
            <p>Message is: {{ message }}</p>

            <span>Multiline message is:</span>
            <p style="white-space: pre-line;">{{ message }}</p>
            <br>
            <textarea v-model="message" placeholder="add multiple lines"></textarea>

            <input type="checkbox" id="checkbox" v-model="checked">
            <label for="checkbox">{{ checked }}</label>

            <div>
                <input type="checkbox" id="jack" value="Jack" v-model="checkedNames">
                <label for="jack">Jack</label>
                <input type="checkbox" id="john" value="John" v-model="checkedNames">
                <label for="john">John</label>
                <input type="checkbox" id="mike" value="Mike" v-model="checkedNames">
                <label for="mike">Mike</label>
                <br>
                <span>Checked names: {{ checkedNames }}</span>
            </div>

            <div id="example-4">
                <input type="radio" id="one" value="One" v-model="picked">
                <label for="one">One</label>
                <br>
                <input type="radio" id="two" value="Two" v-model="picked">
                <label for="two">Two</label>
                <br>
                <span>Picked: {{ picked }}</span>
            </div>

            <div id="example-5">
                <select v-model="selected">
                    <option disabled value="">请选择</option>
                    <option>A</option>
                    <option>B</option>
                    <option>C</option>
                </select>
                <span>Selected: {{ selected }}</span>

                <select v-model="selected">
                    <option v-for="option in options" :key="option.value" v-bind:value="option.value">
                        {{ option.text }}
                    </option>
                </select>
                <span>Selected: {{ selected }}</span>
            </div>

            <div id="example-6">
                <!-- 当选中时，`picked` 为字符串 "a" -->
                <input type="radio" v-model="picked" value="a">

                <!-- `toggle` 为 true 或 false -->
                <input type="checkbox" v-model="toggle">

                <!-- 当选中第一个选项时，`selected` 为字符串 "abc" -->
                <select v-model="selected">
                    <option value="abc">ABC</option>
                </select>
            </div>
        </el-col>
    </el-row>

</div>
</template>

<script>
export default {
    data() {
        return {
            blank: {

            },
            a: 1,
            msg: "这是一条用模板语法展示的消息",
            rawHtml: '<span style="color: red">This should be red.</span>',

            dynamicId: 12,
            isButtonDisabled: true, // 绑定属性

            number: 98,
            ok: 'YES',
            message: "这是一条表达式展示的消息111",

            awesome: true,
            seen: true,
            url: "http://www.bing.com",
            attributeName: 'href',
            key: 'href',
            event: 'doSomething',

            isActive: true,
            hasError: true,
            error: null,

            activeColor: 'red',
            fontSize: 30,

            parentMessage: 'Parent',
            items: [{
                    message: 'Foo'
                },
                {
                    message: 'Bar'
                }
            ],

            object: {
                title: 'How to do lists in Vue',
                author: 'Jane Doe',
                publishedAt: '2016-04-10'
            },

            numbers: [1, 2, 3, 4, 5],
            sets: [
                [1, 2, 3, 4, 5],
                [6, 7, 8, 9, 10]
            ],

            counter: 0,

            name: 'Vue.js',
            checked: false,
            checkedNames: [],
            picked: '',
            selected: '',
            options: [{
                    text: 'One',
                    value: 'A'
                },
                {
                    text: 'Two',
                    value: 'B'
                },
                {
                    text: 'Three',
                    value: 'C'
                }
            ]

        }
    },

    // 计算属性也是一个属性 就是通过的属性而已
    computed: {
        // 计算属性是基于它们的响应式依赖进行缓存的。

        // 计算属性的 getter
        reversedMessage: function () {
            // `this` 指向 vm 实例
            return this.message.split('').reverse().join('')
        },

        classObject: function () {
            return {
                active: this.isActive && !this.error,
                'text-danger': this.error && this.error.type === 'fatal'
            }
        },

        // 定义一个计算属性 将计算属性变更后的值显示
        evenNumbers: function () {
            return this.numbers.filter(function (number) {
                return number % 2 === 0
            })
        },

        greet: function (event) {
            // `this` 在方法里指向当前 Vue 实例
            alert('Hello ' + this.name + '!')
            // `event` 是原生 DOM 事件
            if (event) {
                alert(event.target.tagName)
            }
        }
    },

    // created 钩子可以用来在一个实例被创建之后执行代码
    created: function () {
        // `this` 指向 vm 实例
        console.log('a is: ' + this.a)
    },
    mounted() {
        // 各种生命周期的函数
    },
    updated() {

    },
    destroyed() {

    },
    methods: {
        onSubmit() {
            this.$message('submit!')
        },
        onCancel() {
            this.$message({
                message: 'cancel!',
                type: 'warning',

            })
        },
        doSomething() {
            this.$message('这是用 v-on指令绑定监听事件执行的方法')
        },

        even: function (numbers) {
            return numbers.filter(function (number) {
                return number % 2 === 0
            })
        }
    }
}
</script>

<style scoped>
.line {
    text-align: center;
}
</style>
