(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-54106c44"],{"1c18":function(t,e,i){},"333d":function(t,e,i){"use strict";var a=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"pagination-container",class:{hidden:t.hidden}},[i("el-pagination",t._b({attrs:{background:t.background,"current-page":t.currentPage,"page-size":t.pageSize,layout:t.layout,"page-sizes":t.pageSizes,total:t.total},on:{"update:currentPage":function(e){t.currentPage=e},"update:current-page":function(e){t.currentPage=e},"update:pageSize":function(e){t.pageSize=e},"update:page-size":function(e){t.pageSize=e},"size-change":t.handleSizeChange,"current-change":t.handleCurrentChange}},"el-pagination",t.$attrs,!1))],1)},n=[];i("a9e3");Math.easeInOutQuad=function(t,e,i,a){return t/=a/2,t<1?i/2*t*t+e:(t--,-i/2*(t*(t-2)-1)+e)};var r=function(){return window.requestAnimationFrame||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||function(t){window.setTimeout(t,1e3/60)}}();function o(t){document.documentElement.scrollTop=t,document.body.parentNode.scrollTop=t,document.body.scrollTop=t}function l(){return document.documentElement.scrollTop||document.body.parentNode.scrollTop||document.body.scrollTop}function s(t,e,i){var a=l(),n=t-a,s=20,u=0;e="undefined"===typeof e?500:e;var c=function t(){u+=s;var l=Math.easeInOutQuad(u,a,n,e);o(l),u<e?r(t):i&&"function"===typeof i&&i()};c()}var u={name:"Pagination",props:{total:{required:!0,type:Number},page:{type:Number,default:1},limit:{type:Number,default:20},pageSizes:{type:Array,default:function(){return[10,20,30,50]}},layout:{type:String,default:"total, sizes, prev, pager, next, jumper"},background:{type:Boolean,default:!0},autoScroll:{type:Boolean,default:!0},hidden:{type:Boolean,default:!1}},computed:{currentPage:{get:function(){return this.page},set:function(t){this.$emit("update:page",t)}},pageSize:{get:function(){return this.limit},set:function(t){this.$emit("update:limit",t)}}},methods:{handleSizeChange:function(t){this.$emit("pagination",{page:this.currentPage,limit:t}),this.autoScroll&&s(0,800)},handleCurrentChange:function(t){this.$emit("pagination",{page:t,limit:this.pageSize}),this.autoScroll&&s(0,800)}}},c=u,d=(i("e498"),i("2877")),m=Object(d["a"])(c,a,n,!1,null,"6af373ef",null);e["a"]=m.exports},"6d51":function(t,e,i){"use strict";i.r(e);var a=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"app-container"},[i("el-row",{staticStyle:{"margin-bottom":"20px"}},[i("el-input",{staticClass:"filter-item",staticStyle:{width:"150px"},attrs:{placeholder:"关键词"},model:{value:t.listQuery.Filter,callback:function(e){t.$set(t.listQuery,"Filter",e)},expression:"listQuery.Filter"}}),i("el-button",{staticClass:"filter-item",attrs:{size:"small",type:"primary",icon:"el-icon-search"},on:{click:t.handleFilter}},[t._v(" 搜索 ")]),i("el-button",{attrs:{type:"primary",size:"small",icon:"el-icon-edit"},on:{click:t.handleCreate}},[t._v(" 添加 ")])],1),i("el-table",{directives:[{name:"loading",rawName:"v-loading",value:t.listLoading,expression:"listLoading"}],attrs:{data:t.list,"element-loading-text":"Loading",border:"",fit:"","highlight-current-row":""}},[i("el-table-column",{attrs:{label:"名称",align:"left"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(" "+t._s(e.row.name)+" ")]}}])}),i("el-table-column",{attrs:{align:"center",label:"操作",width:"200"},scopedSlots:t._u([{key:"default",fn:function(e){return[i("el-button",{attrs:{type:"primary",size:"mini"},on:{click:function(i){return t.handleUpdate(e.row)}}},[t._v(" 编辑 ")]),i("el-button",{attrs:{size:"mini",type:"danger"},on:{click:function(i){return t.deleteData(e.row.id)}}},[t._v(" 删除 ")])]}}])})],1),i("pagination",{directives:[{name:"show",rawName:"v-show",value:t.total>0,expression:"total > 0"}],attrs:{total:t.total,page:t.listQuery.page,limit:t.listQuery.limit},on:{"update:page":function(e){return t.$set(t.listQuery,"page",e)},"update:limit":function(e){return t.$set(t.listQuery,"limit",e)},pagination:t.fetchData}}),i("el-dialog",{attrs:{title:t.textMap[t.dialogStatus],visible:t.dialogFormVisible},on:{"update:visible":function(e){t.dialogFormVisible=e}}},[i("el-form",{ref:"dataForm",attrs:{rules:t.rules,model:t.temp,"label-width":"180px","label-position":"left"}},[i("el-form-item",{attrs:{label:"租户名称",prop:"name"}},[i("el-input",{model:{value:t.temp.name,callback:function(e){t.$set(t.temp,"name",e)},expression:"temp.name"}})],1),"create"===t.dialogStatus?i("el-form-item",{attrs:{label:"租户管理员邮箱地址",prop:"adminEmailAddress"}},[i("el-input",{model:{value:t.temp.adminEmailAddress,callback:function(e){t.$set(t.temp,"adminEmailAddress",e)},expression:"temp.adminEmailAddress"}})],1):t._e(),"create"===t.dialogStatus?i("el-form-item",{attrs:{label:"租户管理员密码",prop:"adminPassword"}},[i("el-input",{model:{value:t.temp.adminPassword,callback:function(e){t.$set(t.temp,"adminPassword",e)},expression:"temp.adminPassword"}})],1):t._e()],1),i("div",{staticStyle:{"text-align":"right"}},[i("el-button",{attrs:{type:"danger"},on:{click:function(e){t.dialogFormVisible=!1}}},[t._v("取消")]),i("el-button",{attrs:{type:"primary"},on:{click:function(e){"create"===t.dialogStatus?t.createData():t.updateData()}}},[t._v("确认")])],1)],1)],1)},n=[],r=(i("c740"),i("a434"),i("c24f")),o=i("333d"),l={name:"Tenant",components:{Pagination:o["a"]},props:{providerName:{type:String,required:!1}},data:function(){return{list:null,listLoading:!0,total:0,listQuery:{page:1,limit:20,SkipCount:0,Sorting:"name desc",Filter:""},checked:!0,temp:{id:"",name:"",adminPassword:"",adminEmailAddress:""},dialogFormVisible:!1,dialogStatus:"",textMap:{update:"租户编辑",create:"租户添加"},rules:{name:[{required:!0,message:"请输入名称",trigger:"blur"}],adminEmailAddress:[{required:!0,message:"请输入邮箱",trigger:"blur"}],adminPassword:[{required:!0,message:"请输入密码",trigger:"blur"}]}}},created:function(){this.fetchData()},methods:{fetchData:function(){var t=this;this.listLoading=!0,Object(r["n"])(this.listQuery).then((function(e){t.list=e.items,t.total=e.totalCount,t.listLoading=!1}))},handleFilter:function(){this.listQuery.page=1,this.fetchData()},resetTemp:function(){this.temp={name:"",adminEmailAddress:"",adminPassword:""}},handleCreate:function(){var t=this;this.resetTemp(),this.dialogStatus="create",this.dialogFormVisible=!0,this.$nextTick((function(){t.$refs["dataForm"].clearValidate()}))},createData:function(){var t=this;this.$refs["dataForm"].validate((function(e){e&&Object(r["b"])(t.temp).then((function(){t.list.unshift(t.temp),t.dialogFormVisible=!1,t.$notify({title:"成功",message:"租户添加成功",type:"success",duration:2e3})}))}))},handleUpdate:function(t){var e=this;this.temp=Object.assign({},t),this.dialogStatus="update",this.dialogFormVisible=!0,this.$nextTick((function(){e.$refs["dataForm"].clearValidate()}))},updateData:function(){var t=this;this.$refs["dataForm"].validate((function(e){if(e){var i=Object.assign({},t.temp);Object(r["u"])(i.id,i).then((function(){var e=t.list.findIndex((function(e){return e.id===t.temp.id}));t.list.splice(e,1,t.temp),t.dialogFormVisible=!1,t.$notify({title:"成功",message:"修改成功",type:"success",duration:2e3})}))}}))},deleteData:function(t){var e=this;console.log("delete"),this.$confirm("此操作将永久删除数据, 是否继续?","提示",{confirmButtonText:"确定",cancelButtonText:"取消",type:"warning"}).then((function(){Object(r["e"])(t).then((function(i){var a=e.list.findIndex((function(e){return e.id===t}));e.list.splice(a,1),e.$message({message:"删除成功",type:"success"})})).catch((function(t){console.log(t)}))})).catch((function(){e.$message({type:"info",message:"已取消删除"})}))}}},s=l,u=i("2877"),c=Object(u["a"])(s,a,n,!1,null,null,null);e["default"]=c.exports},a434:function(t,e,i){"use strict";var a=i("23e7"),n=i("23cb"),r=i("a691"),o=i("50c4"),l=i("7b0b"),s=i("65f0"),u=i("8418"),c=i("1dde"),d=i("ae40"),m=c("splice"),p=d("splice",{ACCESSORS:!0,0:0,1:2}),f=Math.max,g=Math.min,h=9007199254740991,b="Maximum allowed length exceeded";a({target:"Array",proto:!0,forced:!m||!p},{splice:function(t,e){var i,a,c,d,m,p,y=l(this),v=o(y.length),w=n(t,v),S=arguments.length;if(0===S?i=a=0:1===S?(i=0,a=v-w):(i=S-2,a=g(f(r(e),0),v-w)),v+i-a>h)throw TypeError(b);for(c=s(y,a),d=0;d<a;d++)m=w+d,m in y&&u(c,d,y[m]);if(c.length=a,i<a){for(d=w;d<v-a;d++)m=d+a,p=d+i,m in y?y[p]=y[m]:delete y[p];for(d=v;d>v-a+i;d--)delete y[d-1]}else if(i>a)for(d=v-a;d>w;d--)m=d+a-1,p=d+i-1,m in y?y[p]=y[m]:delete y[p];for(d=0;d<i;d++)y[d+w]=arguments[d+2];return y.length=v-a+i,c}})},c740:function(t,e,i){"use strict";var a=i("23e7"),n=i("b727").findIndex,r=i("44d2"),o=i("ae40"),l="findIndex",s=!0,u=o(l);l in[]&&Array(1)[l]((function(){s=!1})),a({target:"Array",proto:!0,forced:s||!u},{findIndex:function(t){return n(this,t,arguments.length>1?arguments[1]:void 0)}}),r(l)},e498:function(t,e,i){"use strict";var a=i("1c18"),n=i.n(a);n.a}}]);