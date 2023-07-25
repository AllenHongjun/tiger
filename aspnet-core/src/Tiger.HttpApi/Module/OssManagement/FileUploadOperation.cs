using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiger.Module.OssManagement
{
    /// <summary>
    /// Add extra parameters for uploading files in swagger.
    /// 
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/50172268/how-can-i-add-an-upload-button-to-swagger-ui-in-net-core-web-api?rq=3
    /// </remarks>
    public class FileUploadOperation : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            var isFileUploadOperation =
                context.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(FileContentType));

            if (!isFileUploadOperation) return;

            operation.Parameters.Clear();

            //var uploadFileMediaType = new OpenApiMediaType()
            //{
            //    Schema = new OpenApiSchema()
            //    {
            //        Type = "object",
            //        Properties =
            //    {
            //        ["uploadedFile"] = new OpenApiSchema()
            //        {
            //            Description = "Upload File",
            //            Type = "file",
            //            Format = "formData"
            //        }
            //    },
            //        Required = new HashSet<string>() { "uploadedFile" }
            //    }
            //};

            //operation.RequestBody = new OpenApiRequestBody
            //{
            //    Content = { ["multipart/form-data"] = uploadFileMediaType }
            //};


            Dictionary<string, OpenApiSchema> schema = new Dictionary<string, OpenApiSchema>();
            schema["fileName"] = new OpenApiSchema { Description = "Select file", Type = "string", Format = "binary" };
            Dictionary<string, OpenApiMediaType> content = new Dictionary<string, OpenApiMediaType>();
            content["multipart/form-data"] = new OpenApiMediaType { Schema = new OpenApiSchema { Type = "object", Properties = schema } };
            operation.RequestBody = new OpenApiRequestBody() { Content = content };
        }



        /// <summary>
        /// Indicates swashbuckle should consider the parameter as a file upload
        /// 指示 swashbuckle 应将参数视为文件上传
        /// </summary>
        [AttributeUsage(AttributeTargets.Method)]
        public class FileContentType : Attribute
        {

        }
    }
}
