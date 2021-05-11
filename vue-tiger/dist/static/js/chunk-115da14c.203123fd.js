(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-115da14c"],{"1c18":function(e,t,i){},2083:function(e,t,i){"use strict";i.r(t);var n=function(){var e=this,t=e.$createElement,i=e._self._c||t;return i("div",{staticClass:"app-container"},[i("el-row",{staticStyle:{"margin-bottom":"20px"}},[i("el-input",{staticClass:"filter-item",staticStyle:{width:"150px"},attrs:{placeholder:"关键词"},model:{value:e.listQuery.Filter,callback:function(t){e.$set(e.listQuery,"Filter",t)},expression:"listQuery.Filter"}}),i("el-button",{staticClass:"filter-item",attrs:{size:"small",type:"primary",icon:"el-icon-search"},on:{click:e.handleFilter}}),e._v(" "),i("router-link",{attrs:{to:"/setting/role/create"}},[i("el-button",{attrs:{type:"primary",size:"small",icon:"el-icon-edit"}},[e._v(" 添加 ")])],1)],1),i("el-table",{directives:[{name:"loading",rawName:"v-loading",value:e.listLoading,expression:"listLoading"}],attrs:{data:e.list,"element-loading-text":"Loading",border:"",fit:"","highlight-current-row":""}},[i("el-table-column",{attrs:{align:"center",label:"ID",width:"95"},scopedSlots:e._u([{key:"default",fn:function(t){return[e._v(" "+e._s(t.$index)+" ")]}}])}),i("el-table-column",{attrs:{label:"名称",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[e._v(" "+e._s(t.row.name)+" ")]}}])}),i("el-table-column",{attrs:{label:"是否默认",align:"center",width:"95"},scopedSlots:e._u([{key:"default",fn:function(t){return[e._v(" "+e._s(1==t.row.isDefault?"是":"否")+" ")]}}])}),i("el-table-column",{attrs:{label:"是否发布",align:"center",width:"95"},scopedSlots:e._u([{key:"default",fn:function(t){return[e._v(" "+e._s(1==t.row.isPublish?"是":"否")+" ")]}}])}),i("el-table-column",{attrs:{label:"是否静态",align:"center",width:"95"},scopedSlots:e._u([{key:"default",fn:function(t){return[e._v(" "+e._s(1==t.row.isStatic?"是":"否")+" ")]}}])}),i("el-table-column",{attrs:{align:"center",label:"操作",width:"400"},scopedSlots:e._u([{key:"default",fn:function(t){return[i("router-link",{attrs:{to:"/setting/role/edit/"+t.row.id}},[i("el-button",{attrs:{type:"primary",size:"mini",icon:"el-icon-edit"}},[e._v(" 编辑 ")])],1),e._v(" "),i("el-button",{attrs:{type:"primary",size:"mini"},on:{click:function(i){return e.handlePermission(t)}}},[e._v(" 授权 ")]),i("el-button",{attrs:{type:"danger",size:"mini",icon:"el-icon-delete"},on:{click:function(i){return e.deleteData(t.row.id)}}},[e._v(" 删除 ")])]}}])})],1),i("pagination",{directives:[{name:"show",rawName:"v-show",value:e.total>0,expression:"total > 0"}],attrs:{total:e.total,page:e.listQuery.page,limit:e.listQuery.limit},on:{"update:page":function(t){return e.$set(e.listQuery,"page",t)},"update:limit":function(t){return e.$set(e.listQuery,"limit",t)},pagination:e.fetchData}}),i("el-dialog",{attrs:{visible:e.dialogVisible,title:"角色授权"},on:{"update:visible":function(t){e.dialogVisible=t}}},[i("el-form",{attrs:{"label-width":"80px","label-position":"left"}},[i("el-tabs",{attrs:{"tab-position":"left"}},e._l(e.permissionData.groups,(function(t){return i("el-tab-pane",{key:t.name,attrs:{label:t.displayName}},[i("el-form-item",{attrs:{label:t.displayName}},[i("el-tree",{ref:"permissionTree",refInFor:!0,attrs:{data:e.transformPermissionTree(t.permissions),props:e.treeDefaultProps,"show-checkbox":"","check-strictly":"","node-key":"name","default-expand-all":""}})],1)],1)})),1)],1),i("div",{staticStyle:{"text-align":"right"}},[i("el-button",{attrs:{size:"mini",type:"danger"},on:{click:function(t){e.dialogVisible=!1}}},[e._v("取消")]),i("el-button",{attrs:{size:"mini",type:"primary"},on:{click:function(t){return e.updatePermissionData()}}},[e._v("确认")])],1)],1)],1)},r=[],a=(i("99af"),i("4de4"),i("c740"),i("d81d"),i("45fc"),i("b0c0"),i("c24f")),s=i("333d"),o={name:"Role",components:{Pagination:s["a"]},props:{providerName:{type:String,required:!1}},data:function(){return{list:null,listLoading:!0,total:0,listQuery:{page:1,limit:20,SkipCount:0,Filter:"",Sorting:"name desc"},dialogVisible:!1,permissionData:{groups:[]},treeDefaultProps:{children:"children",label:"label"},dialogPermissionFormVisible:!1,permissionsQuery:{providerKey:"",providerName:"R"}}},created:function(){this.fetchData(),this.permissionsQuery.providerName="R"},methods:{fetchData:function(){var e=this;this.listLoading=!0,Object(a["m"])(this.listQuery).then((function(t){e.list=t.items,e.total=t.totalCount,e.listLoading=!1}))},handleFilter:function(){this.listQuery.page=1,this.fetchData()},deleteData:function(e){var t=this;console.log("delete"),Object(a["d"])(e).then((function(e){t.$message({message:"删除成功",type:"success"})})).catch((function(e){console.log(e)}))},handlePermission:function(e){var t=this;this.dialogVisible=!0,"R"===this.permissionsQuery.providerName?this.permissionsQuery.providerKey=e.row.name:"U"===this.permissionsQuery.providerName&&(this.permissionsQuery.providerKey=e.row.id),Object(a["k"])(this.permissionsQuery).then((function(e){t.permissionData=e;var i=function(e){var i=[],n=t.permissionData.groups[e];for(var r in n.permissions)n.permissions[r].isGranted&&(console.log(n.permissions[r]),i.push(n.permissions[r].name));t.$nextTick((function(){t.$refs.permissionTree[e].setCheckedKeys(i)}))};for(var n in t.permissionData.groups)i(n)}))},transformPermissionTree:function(e){var t=arguments.length>1&&void 0!==arguments[1]?arguments[1]:null,i=[];if(!e||!e.some((function(e){return e.parentName===t})))return i;var n=e.filter((function(e){return e.parentName===t}));for(var r in n){var a="";"R"===this.permissionsQuery.providerName?a=n[r].displayName:"U"===this.permissionsQuery.providerName&&(a=n[r].displayName+" "+n[r].grantedProviders.map((function(e){return"".concat(e.providerName,": ").concat(e.providerKey)}))),i.push({name:n[r].name,label:a,disabled:this.isGrantedByOtherProviderName(n[r].grantedProviders),children:this.transformPermissionTree(e,n[r].name)})}return i},isGrantedByOtherProviderName:function(e){var t=this;return!!e.length&&e.findIndex((function(e){return e.providerName!==t.permissionsQuery.providerName}))>-1},updatePermissionData:function(){var e=this,t=[],i=function(i){var n=e.$refs.permissionTree[i].getCheckedKeys(),r=e.permissionData.groups[i],a=function(e){r.permissions[e].isGranted&&!n.some((function(t){return t===r.permissions[e].name}))?t.push({isGranted:!1,name:r.permissions[e].name}):!r.permissions[e].isGranted&&n.some((function(t){return t===r.permissions[e].name}))&&t.push({isGranted:!0,name:r.permissions[e].name})};for(var s in r.permissions)a(s)};for(var n in this.permissionData.groups)i(n);Object(a["t"])(this.permissionsQuery,{permissions:t}).then((function(){e.dialogPermissionFormVisible=!1,e.$message({message:"权限添加成功",type:"success"})}))}}},l=o,u=i("2877"),c=Object(u["a"])(l,n,r,!1,null,null,null);t["default"]=c.exports},"333d":function(e,t,i){"use strict";var n=function(){var e=this,t=e.$createElement,i=e._self._c||t;return i("div",{staticClass:"pagination-container",class:{hidden:e.hidden}},[i("el-pagination",e._b({attrs:{background:e.background,"current-page":e.currentPage,"page-size":e.pageSize,layout:e.layout,"page-sizes":e.pageSizes,total:e.total},on:{"update:currentPage":function(t){e.currentPage=t},"update:current-page":function(t){e.currentPage=t},"update:pageSize":function(t){e.pageSize=t},"update:page-size":function(t){e.pageSize=t},"size-change":e.handleSizeChange,"current-change":e.handleCurrentChange}},"el-pagination",e.$attrs,!1))],1)},r=[];i("a9e3");Math.easeInOutQuad=function(e,t,i,n){return e/=n/2,e<1?i/2*e*e+t:(e--,-i/2*(e*(e-2)-1)+t)};var a=function(){return window.requestAnimationFrame||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||function(e){window.setTimeout(e,1e3/60)}}();function s(e){document.documentElement.scrollTop=e,document.body.parentNode.scrollTop=e,document.body.scrollTop=e}function o(){return document.documentElement.scrollTop||document.body.parentNode.scrollTop||document.body.scrollTop}function l(e,t,i){var n=o(),r=e-n,l=20,u=0;t="undefined"===typeof t?500:t;var c=function e(){u+=l;var o=Math.easeInOutQuad(u,n,r,t);s(o),u<t?a(e):i&&"function"===typeof i&&i()};c()}var u={name:"Pagination",props:{total:{required:!0,type:Number},page:{type:Number,default:1},limit:{type:Number,default:20},pageSizes:{type:Array,default:function(){return[10,20,30,50]}},layout:{type:String,default:"total, sizes, prev, pager, next, jumper"},background:{type:Boolean,default:!0},autoScroll:{type:Boolean,default:!0},hidden:{type:Boolean,default:!1}},computed:{currentPage:{get:function(){return this.page},set:function(e){this.$emit("update:page",e)}},pageSize:{get:function(){return this.limit},set:function(e){this.$emit("update:limit",e)}}},methods:{handleSizeChange:function(e){this.$emit("pagination",{page:this.currentPage,limit:e}),this.autoScroll&&l(0,800)},handleCurrentChange:function(e){this.$emit("pagination",{page:e,limit:this.pageSize}),this.autoScroll&&l(0,800)}}},c=u,d=(i("e498"),i("2877")),p=Object(d["a"])(c,n,r,!1,null,"6af373ef",null);t["a"]=p.exports},c740:function(e,t,i){"use strict";var n=i("23e7"),r=i("b727").findIndex,a=i("44d2"),s=i("ae40"),o="findIndex",l=!0,u=s(o);o in[]&&Array(1)[o]((function(){l=!1})),n({target:"Array",proto:!0,forced:l||!u},{findIndex:function(e){return r(this,e,arguments.length>1?arguments[1]:void 0)}}),a(o)},e498:function(e,t,i){"use strict";var n=i("1c18"),r=i.n(n);r.a}}]);