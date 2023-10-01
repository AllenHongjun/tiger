using ClosedXML.Excel;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tiger.Infrastructure.ExportImport.Help;

namespace Tiger.Infrastructure.ExportImport
{
    /// <summary>
    /// Import manager
    /// </summary>
    public partial class ImportManager : IImportManager
    {
        #region Fields

        
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        

        #endregion

        #region Ctor

        public ImportManager(
            IHttpClientFactory httpClientFactory,
            
            IServiceScopeFactory serviceScopeFactory
            )
        {
            _serviceScopeFactory = serviceScopeFactory;
            
        }

        #endregion

        #region Utilities

        private static ExportedAttributeType GetTypeOfExportedAttribute(IXLWorksheet worksheet, PropertyManager<ExportProductAttribute> productAttributeManager, PropertyManager<ExportSpecificationAttribute> specificationAttributeManager, int iRow)
        {
            productAttributeManager.ReadFromXlsx(worksheet, iRow, ExportProductAttribute.ProducAttributeCellOffset);

            if (productAttributeManager.IsCaption)
            {
                return ExportedAttributeType.ProductAttribute;
            }

            specificationAttributeManager.ReadFromXlsx(worksheet, iRow, ExportProductAttribute.ProducAttributeCellOffset);

            if (specificationAttributeManager.IsCaption)
            {
                return ExportedAttributeType.SpecificationAttribute;
            }

            return ExportedAttributeType.NotSpecified;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        private static async Task SetOutLineForSpecificationAttributeRowAsync(object cellValue, IXLWorksheet worksheet, int endRow)
        {
            var attributeType = (cellValue ?? string.Empty).ToString();

            if (attributeType.Equals("AttributeType", StringComparison.InvariantCultureIgnoreCase))
            {
                worksheet.Row(endRow).OutlineLevel = 1;
            }
            else
            {
                if ((await SpecificationAttributeType.Option.ToSelectListAsync(useLocalization: false))
                    .Any(p => p.Text.Equals(attributeType, StringComparison.InvariantCultureIgnoreCase)))
                    worksheet.Row(endRow).OutlineLevel = 1;
                else if (int.TryParse(attributeType, out var attributeTypeId) && Enum.IsDefined(typeof(SpecificationAttributeType), attributeTypeId))
                    worksheet.Row(endRow).OutlineLevel = 1;
            }
        }

        private static void CopyDataToNewFile(ImportProductMetadata metadata, IXLWorksheet worksheet, string filePath, int startRow, int endRow, int endCell)
        {
            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            // ok, we can run the real code of the sample now
            using var workbook = new XLWorkbook(stream);
            // uncomment this line if you want the XML written out to the outputDir
            //xlPackage.DebugMode = true; 

            // get handles to the worksheets
            var outWorksheet = workbook.Worksheets.Add(typeof(Product).Name);
            metadata.Manager.WriteCaption(outWorksheet);
            var outRow = 2;
            for (var row = startRow; row <= endRow; row++)
            {
                outWorksheet.Row(outRow).OutlineLevel = worksheet.Row(row).OutlineLevel;
                for (var cell = 1; cell <= endCell; cell++)
                {
                    outWorksheet.Row(outRow).Cell(cell).Value = worksheet.Row(row).Cell(cell).Value;
                }

                outRow += 1;
            }

            workbook.Save();
        }

        protected virtual int GetColumnIndex(string[] properties, string columnName)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if (columnName == null)
                throw new ArgumentNullException(nameof(columnName));

            for (var i = 0; i < properties.Length; i++)
                if (properties[i].Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return i + 1; //excel indexes start from 1
            return 0;
        }

        protected virtual string GetMimeTypeFromFilePath(string filePath)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var mimeType);

            //set to jpeg in case mime type cannot be found
            return mimeType ?? MimeTypes.ImageJpeg;
        }

        

        
        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task ImportSpecificationAttributeAsync(PropertyManager<ExportSpecificationAttribute> specificationAttributeManager, Product lastLoadedProduct)
        {
            if (!_catalogSettings.ExportImportProductSpecificationAttributes || lastLoadedProduct == null || specificationAttributeManager.IsCaption)
                return;

            var attributeTypeId = specificationAttributeManager.GetProperty("AttributeType").IntValue;
            var allowFiltering = specificationAttributeManager.GetProperty("AllowFiltering").BooleanValue;
            var specificationAttributeOptionId = specificationAttributeManager.GetProperty("SpecificationAttributeOptionId").IntValue;
            var productId = lastLoadedProduct.Id;
            var customValue = specificationAttributeManager.GetProperty("CustomValue").StringValue;
            var displayOrder = specificationAttributeManager.GetProperty("DisplayOrder").IntValue;
            var showOnProductPage = specificationAttributeManager.GetProperty("ShowOnProductPage").BooleanValue;

            //if specification attribute option isn't set, try to get first of possible specification attribute option for current specification attribute
            if (specificationAttributeOptionId == 0)
            {
                var specificationAttribute = specificationAttributeManager.GetProperty("SpecificationAttribute").IntValue;
                specificationAttributeOptionId =
                    (await _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttributeAsync(
                        specificationAttribute))
                    .FirstOrDefault()?.Id ?? specificationAttributeOptionId;
            }

            var productSpecificationAttribute = specificationAttributeOptionId == 0
                ? null
                : (await _specificationAttributeService.GetProductSpecificationAttributesAsync(productId, specificationAttributeOptionId)).FirstOrDefault();

            var isNew = productSpecificationAttribute == null;

            if (isNew)
                productSpecificationAttribute = new ProductSpecificationAttribute();

            if (attributeTypeId != (int)SpecificationAttributeType.Option)
                //we allow filtering only for "Option" attribute type
                allowFiltering = false;

            //we don't allow CustomValue for "Option" attribute type
            if (attributeTypeId == (int)SpecificationAttributeType.Option)
                customValue = null;

            productSpecificationAttribute.AttributeTypeId = attributeTypeId;
            productSpecificationAttribute.SpecificationAttributeOptionId = specificationAttributeOptionId;
            productSpecificationAttribute.ProductId = productId;
            productSpecificationAttribute.CustomValue = customValue;
            productSpecificationAttribute.AllowFiltering = allowFiltering;
            productSpecificationAttribute.ShowOnProductPage = showOnProductPage;
            productSpecificationAttribute.DisplayOrder = displayOrder;

            if (isNew)
                await _specificationAttributeService.InsertProductSpecificationAttributeAsync(productSpecificationAttribute);
            else
                await _specificationAttributeService.UpdateProductSpecificationAttributeAsync(productSpecificationAttribute);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task<string> DownloadFileAsync(string urlString, IList<string> downloadedFiles)
        {
            if (string.IsNullOrEmpty(urlString))
                return string.Empty;

            if (!Uri.IsWellFormedUriString(urlString, UriKind.Absolute))
                return urlString;

            if (!_catalogSettings.ExportImportAllowDownloadImages)
                return string.Empty;

            //ensure that temp directory is created
            var tempDirectory = _fileProvider.MapPath(ExportImportDefaults.UploadsTempPath);
            _fileProvider.CreateDirectory(tempDirectory);

            var fileName = _fileProvider.GetFileName(urlString);
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            var filePath = _fileProvider.Combine(tempDirectory, fileName);
            try
            {
                var client = _httpClientFactory.CreateClient(NopHttpDefaults.DefaultHttpClient);
                var fileData = await client.GetByteArrayAsync(urlString);
                await using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    fs.Write(fileData, 0, fileData.Length);

                downloadedFiles?.Add(filePath);
                return filePath;
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync("Download image failed", ex);
            }

            return string.Empty;
        }

        

        #endregion

        #region Nested classes

        protected class ProductPictureMetadata
        {
            public Product ProductItem { get; set; }

            public string Picture1Path { get; set; }

            public string Picture2Path { get; set; }

            public string Picture3Path { get; set; }

            public bool IsNew { get; set; }
        }

        public class CategoryKey
        {
            /// <returns>A task that represents the asynchronous operation</returns>
            public static async Task<CategoryKey> CreateCategoryKeyAsync(Category category, ICategoryService categoryService, IList<Category> allCategories, IStoreMappingService storeMappingService)
            {
                return new CategoryKey(await categoryService.GetFormattedBreadCrumbAsync(category, allCategories), category.LimitedToStores ? (await storeMappingService.GetStoresIdsWithAccessAsync(category)).ToList() : new List<int>())
                {
                    Category = category
                };
            }

            public CategoryKey(string key, List<int> storesIds = null)
            {
                Key = key.Trim();
                StoresIds = storesIds ?? new List<int>();
            }

            public List<int> StoresIds { get; }

            public Category Category { get; private set; }

            public string Key { get; }

            public bool Equals(CategoryKey y)
            {
                if (y == null)
                    return false;

                if (Category != null && y.Category != null)
                    return Category.Id == y.Category.Id;

                if ((StoresIds.Any() || y.StoresIds.Any())
                    && (StoresIds.All(id => !y.StoresIds.Contains(id)) || y.StoresIds.All(id => !StoresIds.Contains(id))))
                    return false;

                return Key.Equals(y.Key);
            }

            public override int GetHashCode()
            {
                if (!StoresIds.Any())
                    return Key.GetHashCode();

                var storesIds = StoresIds.Select(id => id.ToString())
                    .Aggregate(string.Empty, (all, current) => all + current);

                return $"{storesIds}_{Key}".GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var other = obj as CategoryKey;
                return other?.Equals(other) ?? false;
            }
        }

        #endregion
    }
}