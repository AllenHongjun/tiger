﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Core.Http;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.ExportImport.Help;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Date;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;

namespace Nop.Services.ExportImport
{
    /// <summary>
    /// Import manager
    /// </summary>
    public partial class ImportManager : IImportManager
    {
        #region Fields

        private readonly CatalogSettings _catalogSettings;
        private readonly IAddressService _addressService;
        private readonly IAffiliateService _affiliateService;
        private readonly IBackInStockSubscriptionService _backInStockSubscriptionService;
        private readonly ICategoryService _categoryService;
        private readonly ICheckoutAttributeFormatter _checkoutAttributeFormatter;
        private readonly ICountryService _countryService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerService _customerService;
        private readonly ICustomNumberFormatter _customNumberFormatter;
        private readonly INopDataProvider _dataProvider;
        private readonly IDateRangeService _dateRangeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly IManufacturerService _manufacturerService;
        private readonly IMeasureService _measureService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly INopFileProvider _fileProvider;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductService _productService;
        private readonly IProductTagService _productTagService;
        private readonly IProductTemplateService _productTemplateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IShippingService _shippingService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;
        private readonly ITaxCategoryService _taxCategoryService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IVendorService _vendorService;
        private readonly IWorkContext _workContext;
        private readonly MediaSettings _mediaSettings;
        private readonly TaxSettings _taxSettings;
        private readonly VendorSettings _vendorSettings;

        #endregion

        #region Ctor

        public ImportManager(CatalogSettings catalogSettings,
            IAddressService addressService,
            IAffiliateService affiliateService,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            ICategoryService categoryService,
            ICheckoutAttributeFormatter checkoutAttributeFormatter,
            ICountryService countryService,
            ICustomerActivityService customerActivityService,
            ICustomerService customerService,
            ICustomNumberFormatter customNumberFormatter,
            INopDataProvider dataProvider,
            IDateRangeService dateRangeService,
            IGenericAttributeService genericAttributeService,
            IHttpClientFactory httpClientFactory,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILogger logger,
            IManufacturerService manufacturerService,
            IMeasureService measureService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            INopFileProvider fileProvider,
            IOrderService orderService,
            IPictureService pictureService,
            IProductAttributeService productAttributeService,
            IProductService productService,
            IProductTagService productTagService,
            IProductTemplateService productTemplateService,
            IServiceScopeFactory serviceScopeFactory,
            IShippingService shippingService,
            ISpecificationAttributeService specificationAttributeService,
            IStateProvinceService stateProvinceService,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            ITaxCategoryService taxCategoryService,
            IUrlRecordService urlRecordService,
            IVendorService vendorService,
            IWorkContext workContext,
            MediaSettings mediaSettings,
            TaxSettings taxSettings,
            VendorSettings vendorSettings)
        {
            _addressService = addressService;
            _affiliateService = affiliateService;
            _backInStockSubscriptionService = backInStockSubscriptionService;
            _catalogSettings = catalogSettings;
            _categoryService = categoryService;
            _checkoutAttributeFormatter = checkoutAttributeFormatter;
            _countryService = countryService;
            _customerActivityService = customerActivityService;
            _customerService = customerService;
            _customNumberFormatter = customNumberFormatter;
            _dataProvider = dataProvider;
            _dateRangeService = dateRangeService;
            _genericAttributeService = genericAttributeService;
            _httpClientFactory = httpClientFactory;
            _fileProvider = fileProvider;
            _languageService = languageService;
            _localizationService = localizationService;
            _logger = logger;
            _manufacturerService = manufacturerService;
            _measureService = measureService;
            _newsLetterSubscriptionService = newsLetterSubscriptionService;
            _orderService = orderService;
            _pictureService = pictureService;
            _productAttributeService = productAttributeService;
            _productService = productService;
            _productTagService = productTagService;
            _productTemplateService = productTemplateService;
            _serviceScopeFactory = serviceScopeFactory;
            _shippingService = shippingService;
            _specificationAttributeService = specificationAttributeService;
            _stateProvinceService = stateProvinceService;
            _storeContext = storeContext;
            _storeMappingService = storeMappingService;
            _storeService = storeService;
            _taxCategoryService = taxCategoryService;
            _urlRecordService = urlRecordService;
            _vendorService = vendorService;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _taxSettings = taxSettings;
            _vendorSettings = vendorSettings;
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

        /// <summary>
        /// Creates or loads the image
        /// </summary>
        /// <param name="picturePath">The path to the image file</param>
        /// <param name="name">The name of the object</param>
        /// <param name="picId">Image identifier, may be null</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the image or null if the image has not changed
        /// </returns>
        protected virtual async Task<Picture> LoadPictureAsync(string picturePath, string name, int? picId = null)
        {
            if (string.IsNullOrEmpty(picturePath) || !_fileProvider.FileExists(picturePath))
                return null;

            var mimeType = GetMimeTypeFromFilePath(picturePath);
            var newPictureBinary = await _fileProvider.ReadAllBytesAsync(picturePath);
            var pictureAlreadyExists = false;
            if (picId != null)
            {
                //compare with existing product pictures
                var existingPicture = await _pictureService.GetPictureByIdAsync(picId.Value);
                if (existingPicture != null)
                {
                    var existingBinary = await _pictureService.LoadPictureBinaryAsync(existingPicture);
                    //picture binary after validation (like in database)
                    var validatedPictureBinary = await _pictureService.ValidatePictureAsync(newPictureBinary, mimeType, name);
                    if (existingBinary.SequenceEqual(validatedPictureBinary) ||
                        existingBinary.SequenceEqual(newPictureBinary))
                    {
                        pictureAlreadyExists = true;
                    }
                }
            }

            if (pictureAlreadyExists)
                return null;

            var newPicture = await _pictureService.InsertPictureAsync(newPictureBinary, mimeType, await _pictureService.GetPictureSeNameAsync(name));
            return newPicture;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task LogPictureInsertErrorAsync(string picturePath, Exception ex)
        {
            var extension = _fileProvider.GetFileExtension(picturePath);
            var name = _fileProvider.GetFileNameWithoutExtension(picturePath);

            var point = string.IsNullOrEmpty(extension) ? string.Empty : ".";
            var fileName = _fileProvider.FileExists(picturePath) ? $"{name}{point}{extension}" : string.Empty;

            await _logger.ErrorAsync($"Insert picture failed (file name: {fileName})", ex);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task ImportProductImagesUsingServicesAsync(IList<ProductPictureMetadata> productPictureMetadata)
        {
            foreach (var product in productPictureMetadata)
            {
                foreach (var picturePath in new[] { product.Picture1Path, product.Picture2Path, product.Picture3Path })
                {
                    if (string.IsNullOrEmpty(picturePath))
                        continue;

                    var mimeType = GetMimeTypeFromFilePath(picturePath);
                    var newPictureBinary = await _fileProvider.ReadAllBytesAsync(picturePath);
                    var pictureAlreadyExists = false;
                    if (!product.IsNew)
                    {
                        //compare with existing product pictures
                        var existingPictures = await _pictureService.GetPicturesByProductIdAsync(product.ProductItem.Id);
                        foreach (var existingPicture in existingPictures)
                        {
                            var existingBinary = await _pictureService.LoadPictureBinaryAsync(existingPicture);
                            //picture binary after validation (like in database)
                            var validatedPictureBinary = await _pictureService.ValidatePictureAsync(newPictureBinary, mimeType, picturePath);
                            if (!existingBinary.SequenceEqual(validatedPictureBinary) &&
                                !existingBinary.SequenceEqual(newPictureBinary))
                                continue;
                            //the same picture content
                            pictureAlreadyExists = true;
                            break;
                        }
                    }

                    if (pictureAlreadyExists)
                        continue;

                    try
                    {
                        var newPicture = await _pictureService.InsertPictureAsync(newPictureBinary, mimeType, await _pictureService.GetPictureSeNameAsync(product.ProductItem.Name));
                        await _productService.InsertProductPictureAsync(new ProductPicture
                        {
                            //EF has some weird issue if we set "Picture = newPicture" instead of "PictureId = newPicture.Id"
                            //pictures are duplicated
                            //maybe because entity size is too large
                            PictureId = newPicture.Id,
                            DisplayOrder = 1,
                            ProductId = product.ProductItem.Id
                        });
                        await _productService.UpdateProductAsync(product.ProductItem);
                    }
                    catch (Exception ex)
                    {
                        await LogPictureInsertErrorAsync(picturePath, ex);
                    }
                }
            }
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task ImportProductImagesUsingHashAsync(IList<ProductPictureMetadata> productPictureMetadata, IList<Product> allProductsBySku)
        {
            //performance optimization, load all pictures hashes
            //it will only be used if the images are stored in the SQL Server database (not compact)
            var trimByteCount = _dataProvider.SupportedLengthOfBinaryHash - 1;
            var productsImagesIds = await _productService.GetProductsImagesIdsAsync(allProductsBySku.Select(p => p.Id).ToArray());

            var allProductPictureIds = productsImagesIds.SelectMany(p => p.Value);

            var allPicturesHashes = allProductPictureIds.Any() ? await _dataProvider.GetFieldHashesAsync<PictureBinary>(p => allProductPictureIds.Contains(p.PictureId),
                p => p.PictureId, p => p.BinaryData) : new Dictionary<int, string>();

            foreach (var product in productPictureMetadata)
            {
                foreach (var picturePath in new[] { product.Picture1Path, product.Picture2Path, product.Picture3Path })
                {
                    if (string.IsNullOrEmpty(picturePath))
                        continue;
                    try
                    {
                        var mimeType = GetMimeTypeFromFilePath(picturePath);
                        var newPictureBinary = await _fileProvider.ReadAllBytesAsync(picturePath);
                        var pictureAlreadyExists = false;
                        var seoFileName = await _pictureService.GetPictureSeNameAsync(product.ProductItem.Name);

                        if (!product.IsNew)
                        {
                            var newImageHash = HashHelper.CreateHash(
                                newPictureBinary,
                                ExportImportDefaults.ImageHashAlgorithm,
                                trimByteCount);

                            var newValidatedImageHash = HashHelper.CreateHash(
                                await _pictureService.ValidatePictureAsync(newPictureBinary, mimeType, seoFileName),
                                ExportImportDefaults.ImageHashAlgorithm,
                                trimByteCount);

                            var imagesIds = productsImagesIds.ContainsKey(product.ProductItem.Id)
                                ? productsImagesIds[product.ProductItem.Id]
                                : Array.Empty<int>();

                            pictureAlreadyExists = allPicturesHashes.Where(p => imagesIds.Contains(p.Key))
                                .Select(p => p.Value)
                                .Any(p =>
                                    p.Equals(newImageHash, StringComparison.OrdinalIgnoreCase) ||
                                    p.Equals(newValidatedImageHash, StringComparison.OrdinalIgnoreCase));
                        }

                        if (pictureAlreadyExists)
                            continue;

                        var newPicture = await _pictureService.InsertPictureAsync(newPictureBinary, mimeType, seoFileName);

                        await _productService.InsertProductPictureAsync(new ProductPicture
                        {
                            //EF has some weird issue if we set "Picture = newPicture" instead of "PictureId = newPicture.Id"
                            //pictures are duplicated
                            //maybe because entity size is too large
                            PictureId = newPicture.Id,
                            DisplayOrder = 1,
                            ProductId = product.ProductItem.Id
                        });

                        await _productService.UpdateProductAsync(product.ProductItem);
                    }
                    catch (Exception ex)
                    {
                        await LogPictureInsertErrorAsync(picturePath, ex);
                    }
                }
            }
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<(string seName, bool isParentCategoryExists)> UpdateCategoryByXlsxAsync(Category category, PropertyManager<Category> manager, Dictionary<string, ValueTask<Category>> allCategories, bool isNew)
        {
            var seName = string.Empty;
            var isParentCategoryExists = true;
            var isParentCategorySet = false;

            foreach (var property in manager.GetProperties)
            {
                switch (property.PropertyName)
                {
                    case "Name":
                        category.Name = property.StringValue.Split(new[] { ">>" }, StringSplitOptions.RemoveEmptyEntries).Last().Trim();
                        break;
                    case "Description":
                        category.Description = property.StringValue;
                        break;
                    case "CategoryTemplateId":
                        category.CategoryTemplateId = property.IntValue;
                        break;
                    case "MetaKeywords":
                        category.MetaKeywords = property.StringValue;
                        break;
                    case "MetaDescription":
                        category.MetaDescription = property.StringValue;
                        break;
                    case "MetaTitle":
                        category.MetaTitle = property.StringValue;
                        break;
                    case "ParentCategoryId":
                        if (!isParentCategorySet)
                        {
                            var parentCategory = await await allCategories.Values.FirstOrDefaultAwaitAsync(async c => (await c).Id == property.IntValue);
                            isParentCategorySet = parentCategory != null;

                            isParentCategoryExists = isParentCategorySet || property.IntValue == 0;

                            category.ParentCategoryId = parentCategory?.Id ?? property.IntValue;
                        }

                        break;
                    case "ParentCategoryName":
                        if (_catalogSettings.ExportImportCategoriesUsingCategoryName && !isParentCategorySet)
                        {
                            var categoryName = manager.GetProperty("ParentCategoryName").StringValue;
                            if (!string.IsNullOrEmpty(categoryName))
                            {
                                var parentCategory = allCategories.ContainsKey(categoryName)
                                    //try find category by full name with all parent category names
                                    ? await allCategories[categoryName]
                                    //try find category by name
                                    : await await allCategories.Values.FirstOrDefaultAwaitAsync(async c => (await c).Name.Equals(categoryName, StringComparison.InvariantCulture));

                                if (parentCategory != null)
                                {
                                    category.ParentCategoryId = parentCategory.Id;
                                    isParentCategorySet = true;
                                }
                                else
                                {
                                    isParentCategoryExists = false;
                                }
                            }
                        }

                        break;
                    case "Picture":
                        var picture = await LoadPictureAsync(manager.GetProperty("Picture").StringValue, category.Name, isNew ? null : (int?)category.PictureId);
                        if (picture != null)
                            category.PictureId = picture.Id;
                        break;
                    case "PageSize":
                        category.PageSize = property.IntValue;
                        break;
                    case "AllowCustomersToSelectPageSize":
                        category.AllowCustomersToSelectPageSize = property.BooleanValue;
                        break;
                    case "PageSizeOptions":
                        category.PageSizeOptions = property.StringValue;
                        break;
                    case "ShowOnHomepage":
                        category.ShowOnHomepage = property.BooleanValue;
                        break;
                    case "PriceRangeFiltering":
                        category.PriceRangeFiltering = property.BooleanValue;
                        break;
                    case "PriceFrom":
                        category.PriceFrom = property.DecimalValue;
                        break;
                    case "PriceTo":
                        category.PriceTo = property.DecimalValue;
                        break;
                    case "AutomaticallyCalculatePriceRange":
                        category.ManuallyPriceRange = property.BooleanValue;
                        break;
                    case "IncludeInTopMenu":
                        category.IncludeInTopMenu = property.BooleanValue;
                        break;
                    case "Published":
                        category.Published = property.BooleanValue;
                        break;
                    case "DisplayOrder":
                        category.DisplayOrder = property.IntValue;
                        break;
                    case "SeName":
                        seName = property.StringValue;
                        break;
                }
            }

            category.UpdatedOnUtc = DateTime.UtcNow;
            return (seName, isParentCategoryExists);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<(Category category, bool isNew, string curentCategoryBreadCrumb)> GetCategoryFromXlsxAsync(PropertyManager<Category> manager, IXLWorksheet worksheet, int iRow, Dictionary<string, ValueTask<Category>> allCategories)
        {
            manager.ReadFromXlsx(worksheet, iRow);

            //try get category from database by ID
            var category = await await allCategories.Values.FirstOrDefaultAwaitAsync(async c => (await c).Id == manager.GetProperty("Id")?.IntValue);

            if (_catalogSettings.ExportImportCategoriesUsingCategoryName && category == null)
            {
                var categoryName = manager.GetProperty("Name").StringValue;
                if (!string.IsNullOrEmpty(categoryName))
                {
                    category = allCategories.ContainsKey(categoryName)
                        //try find category by full name with all parent category names
                        ? await allCategories[categoryName]
                        //try find category by name
                        : await await allCategories.Values.FirstOrDefaultAwaitAsync(async c => (await c).Name.Equals(categoryName, StringComparison.InvariantCulture));
                }
            }

            var isNew = category == null;

            category ??= new Category();

            var curentCategoryBreadCrumb = string.Empty;

            if (isNew)
            {
                category.CreatedOnUtc = DateTime.UtcNow;
                //default values
                category.PageSize = _catalogSettings.DefaultCategoryPageSize;
                category.PageSizeOptions = _catalogSettings.DefaultCategoryPageSizeOptions;
                category.Published = true;
                category.IncludeInTopMenu = true;
                category.AllowCustomersToSelectPageSize = true;
            }
            else
                curentCategoryBreadCrumb = await _categoryService.GetFormattedBreadCrumbAsync(category);

            return (category, isNew, curentCategoryBreadCrumb);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task SaveCategoryAsync(bool isNew, Category category, Dictionary<string, ValueTask<Category>> allCategories, string curentCategoryBreadCrumb, bool setSeName, string seName)
        {
            if (isNew)
                await _categoryService.InsertCategoryAsync(category);
            else
                await _categoryService.UpdateCategoryAsync(category);

            var categoryBreadCrumb = await _categoryService.GetFormattedBreadCrumbAsync(category);
            if (!allCategories.ContainsKey(categoryBreadCrumb))
                allCategories.Add(categoryBreadCrumb, new ValueTask<Category>(category));
            if (!string.IsNullOrEmpty(curentCategoryBreadCrumb) && allCategories.ContainsKey(curentCategoryBreadCrumb) &&
                categoryBreadCrumb != curentCategoryBreadCrumb)
                allCategories.Remove(curentCategoryBreadCrumb);

            //search engine name
            if (setSeName)
                await _urlRecordService.SaveSlugAsync(category, await _urlRecordService.ValidateSeNameAsync(category, seName, category.Name, true), 0);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task SetOutLineForProductAttributeRowAsync(object cellValue, IXLWorksheet worksheet, int endRow)
        {
            try
            {
                var aid = Convert.ToInt32(cellValue ?? -1);

                var productAttribute = await _productAttributeService.GetProductAttributeByIdAsync(aid);

                if (productAttribute != null)
                    worksheet.Row(endRow).OutlineLevel = 1;
            }
            catch (FormatException)
            {
                if ((cellValue ?? string.Empty).ToString() == "AttributeId")
                    worksheet.Row(endRow).OutlineLevel = 1;
            }
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task ImportProductAttributeAsync(PropertyManager<ExportProductAttribute> productAttributeManager, Product lastLoadedProduct)
        {
            if (!_catalogSettings.ExportImportProductAttributes || lastLoadedProduct == null || productAttributeManager.IsCaption)
                return;

            var productAttributeId = productAttributeManager.GetProperty("AttributeId").IntValue;
            var attributeControlTypeId = productAttributeManager.GetProperty("AttributeControlType").IntValue;

            var productAttributeValueId = productAttributeManager.GetProperty("ProductAttributeValueId").IntValue;
            var associatedProductId = productAttributeManager.GetProperty("AssociatedProductId").IntValue;
            var valueName = productAttributeManager.GetProperty("ValueName").StringValue;
            var attributeValueTypeId = productAttributeManager.GetProperty("AttributeValueType").IntValue;
            var colorSquaresRgb = productAttributeManager.GetProperty("ColorSquaresRgb").StringValue;
            var imageSquaresPictureId = productAttributeManager.GetProperty("ImageSquaresPictureId").IntValue;
            var priceAdjustment = productAttributeManager.GetProperty("PriceAdjustment").DecimalValue;
            var priceAdjustmentUsePercentage = productAttributeManager.GetProperty("PriceAdjustmentUsePercentage").BooleanValue;
            var weightAdjustment = productAttributeManager.GetProperty("WeightAdjustment").DecimalValue;
            var cost = productAttributeManager.GetProperty("Cost").DecimalValue;
            var customerEntersQty = productAttributeManager.GetProperty("CustomerEntersQty").BooleanValue;
            var quantity = productAttributeManager.GetProperty("Quantity").IntValue;
            var isPreSelected = productAttributeManager.GetProperty("IsPreSelected").BooleanValue;
            var displayOrder = productAttributeManager.GetProperty("DisplayOrder").IntValue;
            var pictureId = productAttributeManager.GetProperty("PictureId").IntValue;
            var textPrompt = productAttributeManager.GetProperty("AttributeTextPrompt").StringValue;
            var isRequired = productAttributeManager.GetProperty("AttributeIsRequired").BooleanValue;
            var attributeDisplayOrder = productAttributeManager.GetProperty("AttributeDisplayOrder").IntValue;

            var productAttributeMapping = (await _productAttributeService.GetProductAttributeMappingsByProductIdAsync(lastLoadedProduct.Id))
                .FirstOrDefault(pam => pam.ProductAttributeId == productAttributeId);

            if (productAttributeMapping == null)
            {
                //insert mapping
                productAttributeMapping = new ProductAttributeMapping
                {
                    ProductId = lastLoadedProduct.Id,
                    ProductAttributeId = productAttributeId,
                    TextPrompt = textPrompt,
                    IsRequired = isRequired,
                    AttributeControlTypeId = attributeControlTypeId,
                    DisplayOrder = attributeDisplayOrder
                };
                await _productAttributeService.InsertProductAttributeMappingAsync(productAttributeMapping);
            }
            else
            {
                productAttributeMapping.AttributeControlTypeId = attributeControlTypeId;
                productAttributeMapping.TextPrompt = textPrompt;
                productAttributeMapping.IsRequired = isRequired;
                productAttributeMapping.DisplayOrder = attributeDisplayOrder;
                await _productAttributeService.UpdateProductAttributeMappingAsync(productAttributeMapping);
            }

            var pav = (await _productAttributeService.GetProductAttributeValuesAsync(productAttributeMapping.Id))
                .FirstOrDefault(p => p.Id == productAttributeValueId);

            //var pav = await _productAttributeService.GetProductAttributeValueByIdAsync(productAttributeValueId);

            var attributeControlType = (AttributeControlType)attributeControlTypeId;

            if (pav == null)
            {
                switch (attributeControlType)
                {
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    case AttributeControlType.MultilineTextbox:
                    case AttributeControlType.TextBox:
                        if (productAttributeMapping.ValidationRulesAllowed())
                        {
                            productAttributeMapping.ValidationMinLength = productAttributeManager.GetProperty("ValidationMinLength")?.IntValueNullable;
                            productAttributeMapping.ValidationMaxLength = productAttributeManager.GetProperty("ValidationMaxLength")?.IntValueNullable;
                            productAttributeMapping.ValidationFileMaximumSize = productAttributeManager.GetProperty("ValidationFileMaximumSize")?.IntValueNullable;
                            productAttributeMapping.ValidationFileAllowedExtensions = productAttributeManager.GetProperty("ValidationFileAllowedExtensions")?.StringValue;
                            productAttributeMapping.DefaultValue = productAttributeManager.GetProperty("DefaultValue")?.StringValue;

                            await _productAttributeService.UpdateProductAttributeMappingAsync(productAttributeMapping);
                        }

                        return;
                }

                pav = new ProductAttributeValue
                {
                    ProductAttributeMappingId = productAttributeMapping.Id,
                    AttributeValueType = (AttributeValueType)attributeValueTypeId,
                    AssociatedProductId = associatedProductId,
                    Name = valueName,
                    PriceAdjustment = priceAdjustment,
                    PriceAdjustmentUsePercentage = priceAdjustmentUsePercentage,
                    WeightAdjustment = weightAdjustment,
                    Cost = cost,
                    IsPreSelected = isPreSelected,
                    DisplayOrder = displayOrder,
                    ColorSquaresRgb = colorSquaresRgb,
                    ImageSquaresPictureId = imageSquaresPictureId,
                    CustomerEntersQty = customerEntersQty,
                    Quantity = quantity,
                    PictureId = pictureId
                };

                await _productAttributeService.InsertProductAttributeValueAsync(pav);
            }
            else
            {
                pav.AttributeValueTypeId = attributeValueTypeId;
                pav.AssociatedProductId = associatedProductId;
                pav.Name = valueName;
                pav.ColorSquaresRgb = colorSquaresRgb;
                pav.ImageSquaresPictureId = imageSquaresPictureId;
                pav.PriceAdjustment = priceAdjustment;
                pav.PriceAdjustmentUsePercentage = priceAdjustmentUsePercentage;
                pav.WeightAdjustment = weightAdjustment;
                pav.Cost = cost;
                pav.CustomerEntersQty = customerEntersQty;
                pav.Quantity = quantity;
                pav.IsPreSelected = isPreSelected;
                pav.DisplayOrder = displayOrder;
                pav.PictureId = pictureId;

                await _productAttributeService.UpdateProductAttributeValueAsync(pav);
            }
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

        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task<ImportProductMetadata> PrepareImportProductDataAsync(IXLWorksheet worksheet)
        {
            //the columns
            var properties = GetPropertiesByExcelCells<Product>(worksheet);

            var manager = new PropertyManager<Product>(properties, _catalogSettings);

            var productAttributeProperties = new[]
            {
                new PropertyByName<ExportProductAttribute>("AttributeId"),
                new PropertyByName<ExportProductAttribute>("AttributeName"),
                new PropertyByName<ExportProductAttribute>("DefaultValue"),
                new PropertyByName<ExportProductAttribute>("ValidationMinLength"),
                new PropertyByName<ExportProductAttribute>("ValidationMaxLength"),
                new PropertyByName<ExportProductAttribute>("ValidationFileAllowedExtensions"),
                new PropertyByName<ExportProductAttribute>("ValidationFileMaximumSize"),
                new PropertyByName<ExportProductAttribute>("AttributeTextPrompt"),
                new PropertyByName<ExportProductAttribute>("AttributeIsRequired"),
                new PropertyByName<ExportProductAttribute>("AttributeControlType"),
                new PropertyByName<ExportProductAttribute>("AttributeDisplayOrder"),
                new PropertyByName<ExportProductAttribute>("ProductAttributeValueId"),
                new PropertyByName<ExportProductAttribute>("ValueName"),
                new PropertyByName<ExportProductAttribute>("AttributeValueType"),
                new PropertyByName<ExportProductAttribute>("AssociatedProductId"),
                new PropertyByName<ExportProductAttribute>("ColorSquaresRgb"),
                new PropertyByName<ExportProductAttribute>("ImageSquaresPictureId"),
                new PropertyByName<ExportProductAttribute>("PriceAdjustment"),
                new PropertyByName<ExportProductAttribute>("PriceAdjustmentUsePercentage"),
                new PropertyByName<ExportProductAttribute>("WeightAdjustment"),
                new PropertyByName<ExportProductAttribute>("Cost"),
                new PropertyByName<ExportProductAttribute>("CustomerEntersQty"),
                new PropertyByName<ExportProductAttribute>("Quantity"),
                new PropertyByName<ExportProductAttribute>("IsPreSelected"),
                new PropertyByName<ExportProductAttribute>("DisplayOrder"),
                new PropertyByName<ExportProductAttribute>("PictureId")
            };

            var productAttributeManager = new PropertyManager<ExportProductAttribute>(productAttributeProperties, _catalogSettings);

            var specificationAttributeProperties = new[]
            {
                new PropertyByName<ExportSpecificationAttribute>("AttributeType", p => p.AttributeTypeId),
                new PropertyByName<ExportSpecificationAttribute>("SpecificationAttribute", p => p.SpecificationAttributeId),
                new PropertyByName<ExportSpecificationAttribute>("CustomValue", p => p.CustomValue),
                new PropertyByName<ExportSpecificationAttribute>("SpecificationAttributeOptionId", p => p.SpecificationAttributeOptionId),
                new PropertyByName<ExportSpecificationAttribute>("AllowFiltering", p => p.AllowFiltering),
                new PropertyByName<ExportSpecificationAttribute>("ShowOnProductPage", p => p.ShowOnProductPage),
                new PropertyByName<ExportSpecificationAttribute>("DisplayOrder", p => p.DisplayOrder)
            };

            var specificationAttributeManager = new PropertyManager<ExportSpecificationAttribute>(specificationAttributeProperties, _catalogSettings);

            var endRow = 2;
            var allCategories = new List<string>();
            var allSku = new List<string>();

            var tempProperty = manager.GetProperty("Categories");
            var categoryCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            tempProperty = manager.GetProperty("SKU");
            var skuCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            var allManufacturers = new List<string>();
            tempProperty = manager.GetProperty("Manufacturers");
            var manufacturerCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            var allStores = new List<string>();
            tempProperty = manager.GetProperty("LimitedToStores");
            var limitedToStoresCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            if (_catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities)
            {
                productAttributeManager.SetSelectList("AttributeControlType", await AttributeControlType.TextBox.ToSelectListAsync(useLocalization: false));
                productAttributeManager.SetSelectList("AttributeValueType", await AttributeValueType.Simple.ToSelectListAsync(useLocalization: false));

                specificationAttributeManager.SetSelectList("AttributeType", await SpecificationAttributeType.Option.ToSelectListAsync(useLocalization: false));
                specificationAttributeManager.SetSelectList("SpecificationAttribute", (await _specificationAttributeService
                    .GetSpecificationAttributesAsync())
                    .Select(sa => sa as BaseEntity)
                    .ToSelectList(p => (p as SpecificationAttribute)?.Name ?? string.Empty));

                manager.SetSelectList("ProductType", await ProductType.SimpleProduct.ToSelectListAsync(useLocalization: false));
                manager.SetSelectList("GiftCardType", await GiftCardType.Virtual.ToSelectListAsync(useLocalization: false));
                manager.SetSelectList("DownloadActivationType",
                    await DownloadActivationType.Manually.ToSelectListAsync(useLocalization: false));
                manager.SetSelectList("ManageInventoryMethod",
                    await ManageInventoryMethod.DontManageStock.ToSelectListAsync(useLocalization: false));
                manager.SetSelectList("LowStockActivity",
                    await LowStockActivity.Nothing.ToSelectListAsync(useLocalization: false));
                manager.SetSelectList("BackorderMode", await BackorderMode.NoBackorders.ToSelectListAsync(useLocalization: false));
                manager.SetSelectList("RecurringCyclePeriod",
                    await RecurringProductCyclePeriod.Days.ToSelectListAsync(useLocalization: false));
                manager.SetSelectList("RentalPricePeriod", await RentalPricePeriod.Days.ToSelectListAsync(useLocalization: false));

                manager.SetSelectList("Vendor",
                    (await _vendorService.GetAllVendorsAsync(showHidden: true)).Select(v => v as BaseEntity)
                        .ToSelectList(p => (p as Vendor)?.Name ?? string.Empty));
                manager.SetSelectList("ProductTemplate",
                    (await _productTemplateService.GetAllProductTemplatesAsync()).Select(pt => pt as BaseEntity)
                        .ToSelectList(p => (p as ProductTemplate)?.Name ?? string.Empty));
                manager.SetSelectList("DeliveryDate",
                    (await _dateRangeService.GetAllDeliveryDatesAsync()).Select(dd => dd as BaseEntity)
                        .ToSelectList(p => (p as DeliveryDate)?.Name ?? string.Empty));
                manager.SetSelectList("ProductAvailabilityRange",
                    (await _dateRangeService.GetAllProductAvailabilityRangesAsync()).Select(range => range as BaseEntity)
                        .ToSelectList(p => (p as ProductAvailabilityRange)?.Name ?? string.Empty));
                manager.SetSelectList("TaxCategory",
                    (await _taxCategoryService.GetAllTaxCategoriesAsync()).Select(tc => tc as BaseEntity)
                        .ToSelectList(p => (p as TaxCategory)?.Name ?? string.Empty));
                manager.SetSelectList("BasepriceUnit",
                    (await _measureService.GetAllMeasureWeightsAsync()).Select(mw => mw as BaseEntity)
                        .ToSelectList(p => (p as MeasureWeight)?.Name ?? string.Empty));
                manager.SetSelectList("BasepriceBaseUnit",
                    (await _measureService.GetAllMeasureWeightsAsync()).Select(mw => mw as BaseEntity)
                        .ToSelectList(p => (p as MeasureWeight)?.Name ?? string.Empty));
            }

            var allAttributeIds = new List<int>();
            var allSpecificationAttributeOptionIds = new List<int>();

            var attributeIdCellNum = 1 + ExportProductAttribute.ProducAttributeCellOffset;
            var specificationAttributeOptionIdCellNum =
                specificationAttributeManager.GetIndex("SpecificationAttributeOptionId") +
                ExportProductAttribute.ProducAttributeCellOffset;

            var productsInFile = new List<int>();

            //find end of data
            var typeOfExportedAttribute = ExportedAttributeType.NotSpecified;
            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(endRow).Cell(property.PropertyOrderPosition))
                    .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                if (allColumnsAreEmpty)
                    break;

                if (new[] { 1, 2 }.Select(cellNum => worksheet.Row(endRow).Cell(cellNum))
                        .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString())) &&
                    worksheet.Row(endRow).OutlineLevel == 0)
                {
                    var cellValue = worksheet.Row(endRow).Cell(attributeIdCellNum).Value;
                    await SetOutLineForProductAttributeRowAsync(cellValue, worksheet, endRow);
                    await SetOutLineForSpecificationAttributeRowAsync(cellValue, worksheet, endRow);
                }

                if (worksheet.Row(endRow).OutlineLevel != 0)
                {
                    var newTypeOfExportedAttribute = GetTypeOfExportedAttribute(worksheet, productAttributeManager, specificationAttributeManager, endRow);

                    //skip caption row
                    if (newTypeOfExportedAttribute != ExportedAttributeType.NotSpecified && newTypeOfExportedAttribute != typeOfExportedAttribute)
                    {
                        typeOfExportedAttribute = newTypeOfExportedAttribute;
                        endRow++;
                        continue;
                    }

                    switch (typeOfExportedAttribute)
                    {
                        case ExportedAttributeType.ProductAttribute:
                            productAttributeManager.ReadFromXlsx(worksheet, endRow,
                                ExportProductAttribute.ProducAttributeCellOffset);
                            if (int.TryParse((worksheet.Row(endRow).Cell(attributeIdCellNum).Value ?? string.Empty).ToString(), out var aid))
                            {
                                allAttributeIds.Add(aid);
                            }

                            break;
                        case ExportedAttributeType.SpecificationAttribute:
                            specificationAttributeManager.ReadFromXlsx(worksheet, endRow, ExportProductAttribute.ProducAttributeCellOffset);

                            if (int.TryParse((worksheet.Row(endRow).Cell(specificationAttributeOptionIdCellNum).Value ?? string.Empty).ToString(), out var saoid))
                            {
                                allSpecificationAttributeOptionIds.Add(saoid);
                            }

                            break;
                    }

                    endRow++;
                    continue;
                }

                if (categoryCellNum > 0)
                {
                    var categoryIds = worksheet.Row(endRow).Cell(categoryCellNum).Value?.ToString() ?? string.Empty;

                    if (!string.IsNullOrEmpty(categoryIds))
                        allCategories.AddRange(categoryIds
                            .Split(new[] { ";", ">>" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim())
                            .Distinct());
                }

                if (skuCellNum > 0)
                {
                    var sku = worksheet.Row(endRow).Cell(skuCellNum).Value?.ToString() ?? string.Empty;

                    if (!string.IsNullOrEmpty(sku))
                        allSku.Add(sku);
                }

                if (manufacturerCellNum > 0)
                {
                    var manufacturerIds = worksheet.Row(endRow).Cell(manufacturerCellNum).Value?.ToString() ??
                                          string.Empty;
                    if (!string.IsNullOrEmpty(manufacturerIds))
                        allManufacturers.AddRange(manufacturerIds
                            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                }

                if (limitedToStoresCellNum > 0)
                {
                    var storeIds = worksheet.Row(endRow).Cell(limitedToStoresCellNum).Value?.ToString() ??
                                          string.Empty;
                    if (!string.IsNullOrEmpty(storeIds))
                        allStores.AddRange(storeIds
                            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                }

                //counting the number of products
                productsInFile.Add(endRow);

                endRow++;
            }

            //performance optimization, the check for the existence of the categories in one SQL request
            var notExistingCategories = await _categoryService.GetNotExistingCategoriesAsync(allCategories.ToArray());
            if (notExistingCategories.Any())
            {
                throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.Import.CategoriesDontExist"), string.Join(", ", notExistingCategories)));
            }

            //performance optimization, the check for the existence of the manufacturers in one SQL request
            var notExistingManufacturers = await _manufacturerService.GetNotExistingManufacturersAsync(allManufacturers.ToArray());
            if (notExistingManufacturers.Any())
            {
                throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.Import.ManufacturersDontExist"), string.Join(", ", notExistingManufacturers)));
            }

            //performance optimization, the check for the existence of the product attributes in one SQL request
            var notExistingProductAttributes = await _productAttributeService.GetNotExistingAttributesAsync(allAttributeIds.ToArray());
            if (notExistingProductAttributes.Any())
            {
                throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.Import.ProductAttributesDontExist"), string.Join(", ", notExistingProductAttributes)));
            }

            //performance optimization, the check for the existence of the specification attribute options in one SQL request
            var notExistingSpecificationAttributeOptions = await _specificationAttributeService.GetNotExistingSpecificationAttributeOptionsAsync(allSpecificationAttributeOptionIds.Where(saoId => saoId != 0).ToArray());
            if (notExistingSpecificationAttributeOptions.Any())
            {
                throw new ArgumentException($"The following specification attribute option ID(s) don't exist - {string.Join(", ", notExistingSpecificationAttributeOptions)}");
            }

            //performance optimization, the check for the existence of the stores in one SQL request
            var notExistingStores = await _storeService.GetNotExistingStoresAsync(allStores.ToArray());
            if (notExistingStores.Any())
            {
                throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.Import.StoresDontExist"), string.Join(", ", notExistingStores)));
            }

            return new ImportProductMetadata
            {
                EndRow = endRow,
                Manager = manager,
                Properties = properties,
                ProductsInFile = productsInFile,
                ProductAttributeManager = productAttributeManager,
                SpecificationAttributeManager = specificationAttributeManager,
                SkuCellNum = skuCellNum,
                AllSku = allSku
            };
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task ImportProductsFromSplitedXlsxAsync(IXLWorksheet worksheet, ImportProductMetadata metadata)
        {
            foreach (var path in SplitProductFile(worksheet, metadata))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                // Resolve
                var importManager = EngineContext.Current.Resolve<IImportManager>(scope);

                using var sr = new StreamReader(path);
                await importManager.ImportProductsFromXlsxAsync(sr.BaseStream);

                try
                {
                    _fileProvider.DeleteFile(path);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private IList<string> SplitProductFile(IXLWorksheet worksheet, ImportProductMetadata metadata)
        {
            var fileIndex = 1;
            var fileName = Guid.NewGuid().ToString();
            var endCell = metadata.Properties.Max(p => p.PropertyOrderPosition);

            var filePaths = new List<string>();

            while (true)
            {
                var curIndex = fileIndex * _catalogSettings.ExportImportProductsCountInOneFile;

                var startRow = metadata.ProductsInFile[(fileIndex - 1) * _catalogSettings.ExportImportProductsCountInOneFile];

                var endRow = metadata.CountProductsInFile > curIndex + 1
                    ? metadata.ProductsInFile[curIndex - 1]
                    : metadata.EndRow;

                var filePath = $"{_fileProvider.MapPath(ExportImportDefaults.UploadsTempPath)}/{fileName}_part_{fileIndex}.xlsx";

                CopyDataToNewFile(metadata, worksheet, filePath, startRow, endRow, endCell);

                filePaths.Add(filePath);
                fileIndex += 1;

                if (endRow == metadata.EndRow)
                    break;
            }

            return filePaths;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task<ImportOrderMetadata> PrepareImportOrderDataAsync(IXLWorksheet worksheet)
        {
            //the columns
            var properties = GetPropertiesByExcelCells<Order>(worksheet);

            var manager = new PropertyManager<Order>(properties, _catalogSettings);

            var orderItemProperties = new[]
            {
                new PropertyByName<OrderItem>("OrderItemGuid"),
                new PropertyByName<OrderItem>("Name"),
                new PropertyByName<OrderItem>("Sku"),
                new PropertyByName<OrderItem>("PriceExclTax"),
                new PropertyByName<OrderItem>("PriceInclTax"),
                new PropertyByName<OrderItem>("Quantity"),
                new PropertyByName<OrderItem>("DiscountExclTax"),
                new PropertyByName<OrderItem>("DiscountInclTax"),
                new PropertyByName<OrderItem>("TotalExclTax"),
                new PropertyByName<OrderItem>("TotalInclTax")
            };

            var orderItemManager = new PropertyManager<OrderItem>(orderItemProperties, _catalogSettings);

            var endRow = 2;
            var allOrderGuids = new List<Guid>();

            var tempProperty = manager.GetProperty("OrderGuid");
            var orderGuidCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            tempProperty = manager.GetProperty("CustomerGuid");
            var customerGuidCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            manager.SetSelectList("OrderStatus", await OrderStatus.Cancelled.ToSelectListAsync(useLocalization: false));
            manager.SetSelectList("ShippingStatus", await ShippingStatus.Delivered.ToSelectListAsync(useLocalization: false));
            manager.SetSelectList("PaymentStatus", await PaymentStatus.Authorized.ToSelectListAsync(useLocalization: false));

            var allCustomerGuids = new List<Guid>();

            var allOrderItemSkus = new List<string>();

            var countOrdersInFile = 0;

            //find end of data
            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(endRow).Cell(property.PropertyOrderPosition))
                    .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                if (allColumnsAreEmpty)
                    break;

                if (worksheet.Row(endRow).OutlineLevel != 0)
                {
                    orderItemManager.ReadFromXlsx(worksheet, endRow, 2);

                    //skip caption row
                    if (!orderItemManager.IsCaption)
                    {
                        allOrderItemSkus.Add(orderItemManager.GetProperty("Sku").StringValue);
                    }

                    endRow++;
                    continue;
                }

                if (orderGuidCellNum > 0)
                {
                    var orderGuidString = worksheet.Row(endRow).Cell(orderGuidCellNum).Value?.ToString() ?? string.Empty;
                    if (!string.IsNullOrEmpty(orderGuidString) && Guid.TryParse(orderGuidString, out var orderGuid))
                        allOrderGuids.Add(orderGuid);
                }

                if (customerGuidCellNum > 0)
                {
                    var customerGuidString = worksheet.Row(endRow).Cell(customerGuidCellNum).Value?.ToString() ?? string.Empty;
                    if (!string.IsNullOrEmpty(customerGuidString) && Guid.TryParse(customerGuidString, out var customerGuid))
                        allCustomerGuids.Add(customerGuid);
                }

                //counting the number of orders
                countOrdersInFile++;

                endRow++;
            }

            //performance optimization, the check for the existence of the customers in one SQL request
            var notExistingCustomerGuids = await _customerService.GetNotExistingCustomersAsync(allCustomerGuids.ToArray());
            if (notExistingCustomerGuids.Any())
            {
                throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Orders.Import.CustomersDontExist"), string.Join(", ", notExistingCustomerGuids)));
            }

            //performance optimization, the check for the existence of the order items in one SQL request
            var notExistingProductSkus = await _productService.GetNotExistingProductsAsync(allOrderItemSkus.ToArray());
            if (notExistingProductSkus.Any())
            {
                throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Orders.Import.ProductsDontExist"), string.Join(", ", notExistingProductSkus)));
            }

            return new ImportOrderMetadata
            {
                EndRow = endRow,
                Manager = manager,
                Properties = properties,
                CountOrdersInFile = countOrdersInFile,
                OrderItemManager = orderItemManager,
                AllOrderGuids = allOrderGuids,
                AllCustomerGuids = allCustomerGuids
            };
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task ImportOrderItemAsync(PropertyManager<OrderItem> orderItemManager, Order lastLoadedOrder)
        {
            if (lastLoadedOrder == null || orderItemManager.IsCaption)
                return;

            var orderItemGuid = Guid.TryParse(orderItemManager.GetProperty("OrderItemGuid").StringValue, out var guidValue) ? guidValue : Guid.NewGuid();
            var sku = orderItemManager.GetProperty("Sku").StringValue;
            var priceExclTax = orderItemManager.GetProperty("PriceExclTax").DecimalValue;
            var priceInclTax = orderItemManager.GetProperty("PriceInclTax").DecimalValue;
            var quantity = orderItemManager.GetProperty("Quantity").IntValue;
            var discountExclTax = orderItemManager.GetProperty("DiscountExclTax").DecimalValue;
            var discountInclTax = orderItemManager.GetProperty("DiscountInclTax").DecimalValue;
            var totalExclTax = orderItemManager.GetProperty("TotalExclTax").DecimalValue;
            var totalInclTax = orderItemManager.GetProperty("TotalInclTax").DecimalValue;

            var orderItemProduct = await _productService.GetProductBySkuAsync(sku);
            var orderItem = (await _orderService.GetOrderItemsAsync(lastLoadedOrder.Id)).FirstOrDefault(f => f.OrderItemGuid == orderItemGuid);

            if (orderItem == null)
            {
                //insert order item
                orderItem = new OrderItem
                {
                    DiscountAmountExclTax = discountExclTax,
                    DiscountAmountInclTax = discountInclTax,
                    OrderId = lastLoadedOrder.Id,
                    OrderItemGuid = orderItemGuid,
                    PriceExclTax = totalExclTax,
                    PriceInclTax = totalInclTax,
                    ProductId = orderItemProduct.Id,
                    Quantity = quantity,
                    OriginalProductCost = orderItemProduct.ProductCost,
                    UnitPriceExclTax = priceExclTax,
                    UnitPriceInclTax = priceInclTax
                };
                await _orderService.InsertOrderItemAsync(orderItem);
            }
            else
            {
                //update order item
                orderItem.DiscountAmountExclTax = discountExclTax;
                orderItem.DiscountAmountInclTax = discountInclTax;
                orderItem.OrderId = lastLoadedOrder.Id;
                orderItem.PriceExclTax = totalExclTax;
                orderItem.PriceInclTax = totalInclTax;
                orderItem.Quantity = quantity;
                orderItem.UnitPriceExclTax = priceExclTax;
                orderItem.UnitPriceInclTax = priceInclTax;
                await _orderService.UpdateOrderItemAsync(orderItem);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get property list by excel cells
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="worksheet">Excel worksheet</param>
        /// <returns>Property list</returns>
        public static IList<PropertyByName<T>> GetPropertiesByExcelCells<T>(IXLWorksheet worksheet)
        {
            var properties = new List<PropertyByName<T>>();
            var poz = 1;
            while (true)
            {
                try
                {
                    var cell = worksheet.Row(1).Cell(poz);

                    if (string.IsNullOrEmpty(cell?.Value?.ToString()))
                        break;

                    poz += 1;
                    properties.Add(new PropertyByName<T>(cell.Value.ToString()));
                }
                catch
                {
                    break;
                }
            }

            return properties;
        }

        /// <summary>
        /// Import products from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task ImportProductsFromXlsxAsync(Stream stream)
        {
            using var workbook = new XLWorkbook(stream);
            // get the first worksheet in the workbook
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new NopException("No worksheet found");

            var downloadedFiles = new List<string>();

            var metadata = await PrepareImportProductDataAsync(worksheet);

            if (_catalogSettings.ExportImportSplitProductsFile && metadata.CountProductsInFile > _catalogSettings.ExportImportProductsCountInOneFile)
            {
                await ImportProductsFromSplitedXlsxAsync(worksheet, metadata);
                return;
            }

            //performance optimization, load all products by SKU in one SQL request
            var currentVendor = await _workContext.GetCurrentVendorAsync();
            var allProductsBySku = await _productService.GetProductsBySkuAsync(metadata.AllSku.ToArray(), currentVendor?.Id ?? 0);

            //validate maximum number of products per vendor
            if (_vendorSettings.MaximumProductNumber > 0 &&
                currentVendor != null)
            {
                var newProductsCount = metadata.CountProductsInFile - allProductsBySku.Count;
                if (await _productService.GetNumberOfProductsByVendorIdAsync(currentVendor.Id) + newProductsCount > _vendorSettings.MaximumProductNumber)
                    throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.ExceededMaximumNumber"), _vendorSettings.MaximumProductNumber));
            }

            //performance optimization, load all categories IDs for products in one SQL request
            var allProductsCategoryIds = await _categoryService.GetProductCategoryIdsAsync(allProductsBySku.Select(p => p.Id).ToArray());

            //performance optimization, load all categories in one SQL request
            Dictionary<CategoryKey, Category> allCategories;
            try
            {
                var allCategoryList = await _categoryService.GetAllCategoriesAsync(showHidden: true);

                allCategories = await allCategoryList
                    .ToDictionaryAwaitAsync(async c => await CategoryKey.CreateCategoryKeyAsync(c, _categoryService, allCategoryList, _storeMappingService), c => new ValueTask<Category>(c));
            }
            catch (ArgumentException)
            {
                //categories with the same name are not supported in the same category level
                throw new ArgumentException(await _localizationService.GetResourceAsync("Admin.Catalog.Products.Import.CategoriesWithSameNameNotSupported"));
            }

            //performance optimization, load all manufacturers IDs for products in one SQL request
            var allProductsManufacturerIds = await _manufacturerService.GetProductManufacturerIdsAsync(allProductsBySku.Select(p => p.Id).ToArray());

            //performance optimization, load all manufacturers in one SQL request
            var allManufacturers = await _manufacturerService.GetAllManufacturersAsync(showHidden: true);

            //performance optimization, load all stores in one SQL request
            var allStores = await _storeService.GetAllStoresAsync();

            //product to import images
            var productPictureMetadata = new List<ProductPictureMetadata>();

            Product lastLoadedProduct = null;
            var typeOfExportedAttribute = ExportedAttributeType.NotSpecified;

            for (var iRow = 2; iRow < metadata.EndRow; iRow++)
            {
                //imports product attributes
                if (worksheet.Row(iRow).OutlineLevel != 0)
                {
                    if (lastLoadedProduct == null)
                        continue;

                    var newTypeOfExportedAttribute = GetTypeOfExportedAttribute(worksheet, metadata.ProductAttributeManager, metadata.SpecificationAttributeManager, iRow);

                    //skip caption row
                    if (newTypeOfExportedAttribute != ExportedAttributeType.NotSpecified &&
                        newTypeOfExportedAttribute != typeOfExportedAttribute)
                    {
                        typeOfExportedAttribute = newTypeOfExportedAttribute;
                        continue;
                    }

                    switch (typeOfExportedAttribute)
                    {
                        case ExportedAttributeType.ProductAttribute:
                            await ImportProductAttributeAsync(metadata.ProductAttributeManager, lastLoadedProduct);
                            break;
                        case ExportedAttributeType.SpecificationAttribute:
                            await ImportSpecificationAttributeAsync(metadata.SpecificationAttributeManager, lastLoadedProduct);
                            break;
                        case ExportedAttributeType.NotSpecified:
                        default:
                            continue;
                    }

                    continue;
                }

                metadata.Manager.ReadFromXlsx(worksheet, iRow);

                var product = metadata.SkuCellNum > 0 ? allProductsBySku.FirstOrDefault(p => p.Sku == metadata.Manager.GetProperty("SKU").StringValue) : null;

                var isNew = product == null;

                product ??= new Product();

                //some of previous values
                var previousStockQuantity = product.StockQuantity;
                var previousWarehouseId = product.WarehouseId;
                var prevTotalStockQuantity = await _productService.GetTotalStockQuantityAsync(product);

                if (isNew)
                    product.CreatedOnUtc = DateTime.UtcNow;

                foreach (var property in metadata.Manager.GetProperties)
                {
                    switch (property.PropertyName)
                    {
                        case "ProductType":
                            product.ProductTypeId = property.IntValue;
                            break;
                        case "ParentGroupedProductId":
                            product.ParentGroupedProductId = property.IntValue;
                            break;
                        case "VisibleIndividually":
                            product.VisibleIndividually = property.BooleanValue;
                            break;
                        case "Name":
                            product.Name = property.StringValue;
                            break;
                        case "ShortDescription":
                            product.ShortDescription = property.StringValue;
                            break;
                        case "FullDescription":
                            product.FullDescription = property.StringValue;
                            break;
                        case "Vendor":
                            //vendor can't change this field
                            if (currentVendor == null)
                                product.VendorId = property.IntValue;
                            break;
                        case "ProductTemplate":
                            product.ProductTemplateId = property.IntValue;
                            break;
                        case "ShowOnHomepage":
                            //vendor can't change this field
                            if (currentVendor == null)
                                product.ShowOnHomepage = property.BooleanValue;
                            break;
                        case "DisplayOrder":
                            //vendor can't change this field
                            if (currentVendor == null)
                                product.DisplayOrder = property.IntValue;
                            break;
                        case "MetaKeywords":
                            product.MetaKeywords = property.StringValue;
                            break;
                        case "MetaDescription":
                            product.MetaDescription = property.StringValue;
                            break;
                        case "MetaTitle":
                            product.MetaTitle = property.StringValue;
                            break;
                        case "AllowCustomerReviews":
                            product.AllowCustomerReviews = property.BooleanValue;
                            break;
                        case "Published":
                            product.Published = property.BooleanValue;
                            break;
                        case "SKU":
                            product.Sku = property.StringValue;
                            break;
                        case "ManufacturerPartNumber":
                            product.ManufacturerPartNumber = property.StringValue;
                            break;
                        case "Gtin":
                            product.Gtin = property.StringValue;
                            break;
                        case "IsGiftCard":
                            product.IsGiftCard = property.BooleanValue;
                            break;
                        case "GiftCardType":
                            product.GiftCardTypeId = property.IntValue;
                            break;
                        case "OverriddenGiftCardAmount":
                            product.OverriddenGiftCardAmount = property.DecimalValue;
                            break;
                        case "RequireOtherProducts":
                            product.RequireOtherProducts = property.BooleanValue;
                            break;
                        case "RequiredProductIds":
                            product.RequiredProductIds = property.StringValue;
                            break;
                        case "AutomaticallyAddRequiredProducts":
                            product.AutomaticallyAddRequiredProducts = property.BooleanValue;
                            break;
                        case "IsDownload":
                            product.IsDownload = property.BooleanValue;
                            break;
                        case "DownloadId":
                            product.DownloadId = property.IntValue;
                            break;
                        case "UnlimitedDownloads":
                            product.UnlimitedDownloads = property.BooleanValue;
                            break;
                        case "MaxNumberOfDownloads":
                            product.MaxNumberOfDownloads = property.IntValue;
                            break;
                        case "DownloadActivationType":
                            product.DownloadActivationTypeId = property.IntValue;
                            break;
                        case "HasSampleDownload":
                            product.HasSampleDownload = property.BooleanValue;
                            break;
                        case "SampleDownloadId":
                            product.SampleDownloadId = property.IntValue;
                            break;
                        case "HasUserAgreement":
                            product.HasUserAgreement = property.BooleanValue;
                            break;
                        case "UserAgreementText":
                            product.UserAgreementText = property.StringValue;
                            break;
                        case "IsRecurring":
                            product.IsRecurring = property.BooleanValue;
                            break;
                        case "RecurringCycleLength":
                            product.RecurringCycleLength = property.IntValue;
                            break;
                        case "RecurringCyclePeriod":
                            product.RecurringCyclePeriodId = property.IntValue;
                            break;
                        case "RecurringTotalCycles":
                            product.RecurringTotalCycles = property.IntValue;
                            break;
                        case "IsRental":
                            product.IsRental = property.BooleanValue;
                            break;
                        case "RentalPriceLength":
                            product.RentalPriceLength = property.IntValue;
                            break;
                        case "RentalPricePeriod":
                            product.RentalPricePeriodId = property.IntValue;
                            break;
                        case "IsShipEnabled":
                            product.IsShipEnabled = property.BooleanValue;
                            break;
                        case "IsFreeShipping":
                            product.IsFreeShipping = property.BooleanValue;
                            break;
                        case "ShipSeparately":
                            product.ShipSeparately = property.BooleanValue;
                            break;
                        case "AdditionalShippingCharge":
                            product.AdditionalShippingCharge = property.DecimalValue;
                            break;
                        case "DeliveryDate":
                            product.DeliveryDateId = property.IntValue;
                            break;
                        case "IsTaxExempt":
                            product.IsTaxExempt = property.BooleanValue;
                            break;
                        case "TaxCategory":
                            product.TaxCategoryId = property.IntValue;
                            break;
                        case "IsTelecommunicationsOrBroadcastingOrElectronicServices":
                            product.IsTelecommunicationsOrBroadcastingOrElectronicServices = property.BooleanValue;
                            break;
                        case "ManageInventoryMethod":
                            product.ManageInventoryMethodId = property.IntValue;
                            break;
                        case "ProductAvailabilityRange":
                            product.ProductAvailabilityRangeId = property.IntValue;
                            break;
                        case "UseMultipleWarehouses":
                            product.UseMultipleWarehouses = property.BooleanValue;
                            break;
                        case "WarehouseId":
                            product.WarehouseId = property.IntValue;
                            break;
                        case "StockQuantity":
                            product.StockQuantity = property.IntValue;
                            break;
                        case "DisplayStockAvailability":
                            product.DisplayStockAvailability = property.BooleanValue;
                            break;
                        case "DisplayStockQuantity":
                            product.DisplayStockQuantity = property.BooleanValue;
                            break;
                        case "MinStockQuantity":
                            product.MinStockQuantity = property.IntValue;
                            break;
                        case "LowStockActivity":
                            product.LowStockActivityId = property.IntValue;
                            break;
                        case "NotifyAdminForQuantityBelow":
                            product.NotifyAdminForQuantityBelow = property.IntValue;
                            break;
                        case "BackorderMode":
                            product.BackorderModeId = property.IntValue;
                            break;
                        case "AllowBackInStockSubscriptions":
                            product.AllowBackInStockSubscriptions = property.BooleanValue;
                            break;
                        case "OrderMinimumQuantity":
                            product.OrderMinimumQuantity = property.IntValue;
                            break;
                        case "OrderMaximumQuantity":
                            product.OrderMaximumQuantity = property.IntValue;
                            break;
                        case "AllowedQuantities":
                            product.AllowedQuantities = property.StringValue;
                            break;
                        case "AllowAddingOnlyExistingAttributeCombinations":
                            product.AllowAddingOnlyExistingAttributeCombinations = property.BooleanValue;
                            break;
                        case "NotReturnable":
                            product.NotReturnable = property.BooleanValue;
                            break;
                        case "DisableBuyButton":
                            product.DisableBuyButton = property.BooleanValue;
                            break;
                        case "DisableWishlistButton":
                            product.DisableWishlistButton = property.BooleanValue;
                            break;
                        case "AvailableForPreOrder":
                            product.AvailableForPreOrder = property.BooleanValue;
                            break;
                        case "PreOrderAvailabilityStartDateTimeUtc":
                            product.PreOrderAvailabilityStartDateTimeUtc = property.DateTimeNullable;
                            break;
                        case "CallForPrice":
                            product.CallForPrice = property.BooleanValue;
                            break;
                        case "Price":
                            product.Price = property.DecimalValue;
                            break;
                        case "OldPrice":
                            product.OldPrice = property.DecimalValue;
                            break;
                        case "ProductCost":
                            product.ProductCost = property.DecimalValue;
                            break;
                        case "CustomerEntersPrice":
                            product.CustomerEntersPrice = property.BooleanValue;
                            break;
                        case "MinimumCustomerEnteredPrice":
                            product.MinimumCustomerEnteredPrice = property.DecimalValue;
                            break;
                        case "MaximumCustomerEnteredPrice":
                            product.MaximumCustomerEnteredPrice = property.DecimalValue;
                            break;
                        case "BasepriceEnabled":
                            product.BasepriceEnabled = property.BooleanValue;
                            break;
                        case "BasepriceAmount":
                            product.BasepriceAmount = property.DecimalValue;
                            break;
                        case "BasepriceUnit":
                            product.BasepriceUnitId = property.IntValue;
                            break;
                        case "BasepriceBaseAmount":
                            product.BasepriceBaseAmount = property.DecimalValue;
                            break;
                        case "BasepriceBaseUnit":
                            product.BasepriceBaseUnitId = property.IntValue;
                            break;
                        case "MarkAsNew":
                            product.MarkAsNew = property.BooleanValue;
                            break;
                        case "MarkAsNewStartDateTimeUtc":
                            product.MarkAsNewStartDateTimeUtc = property.DateTimeNullable;
                            break;
                        case "MarkAsNewEndDateTimeUtc":
                            product.MarkAsNewEndDateTimeUtc = property.DateTimeNullable;
                            break;
                        case "Weight":
                            product.Weight = property.DecimalValue;
                            break;
                        case "Length":
                            product.Length = property.DecimalValue;
                            break;
                        case "Width":
                            product.Width = property.DecimalValue;
                            break;
                        case "Height":
                            product.Height = property.DecimalValue;
                            break;
                        case "IsLimitedToStores":
                            product.LimitedToStores = property.BooleanValue;
                            break;
                    }
                }

                //set some default values if not specified
                if (isNew && metadata.Properties.All(p => p.PropertyName != "ProductType"))
                    product.ProductType = ProductType.SimpleProduct;
                if (isNew && metadata.Properties.All(p => p.PropertyName != "VisibleIndividually"))
                    product.VisibleIndividually = true;
                if (isNew && metadata.Properties.All(p => p.PropertyName != "Published"))
                    product.Published = true;

                //sets the current vendor for the new product
                if (isNew && currentVendor != null)
                    product.VendorId = currentVendor.Id;

                product.UpdatedOnUtc = DateTime.UtcNow;

                if (isNew)
                    await _productService.InsertProductAsync(product);
                else
                    await _productService.UpdateProductAsync(product);

                //quantity change history
                if (isNew || previousWarehouseId == product.WarehouseId)
                {
                    await _productService.AddStockQuantityHistoryEntryAsync(product, product.StockQuantity - previousStockQuantity, product.StockQuantity,
                        product.WarehouseId, await _localizationService.GetResourceAsync("Admin.StockQuantityHistory.Messages.ImportProduct.Edit"));
                }
                //warehouse is changed 
                else
                {
                    //compose a message
                    var oldWarehouseMessage = string.Empty;
                    if (previousWarehouseId > 0)
                    {
                        var oldWarehouse = await _shippingService.GetWarehouseByIdAsync(previousWarehouseId);
                        if (oldWarehouse != null)
                            oldWarehouseMessage = string.Format(await _localizationService.GetResourceAsync("Admin.StockQuantityHistory.Messages.EditWarehouse.Old"), oldWarehouse.Name);
                    }

                    var newWarehouseMessage = string.Empty;
                    if (product.WarehouseId > 0)
                    {
                        var newWarehouse = await _shippingService.GetWarehouseByIdAsync(product.WarehouseId);
                        if (newWarehouse != null)
                            newWarehouseMessage = string.Format(await _localizationService.GetResourceAsync("Admin.StockQuantityHistory.Messages.EditWarehouse.New"), newWarehouse.Name);
                    }

                    var message = string.Format(await _localizationService.GetResourceAsync("Admin.StockQuantityHistory.Messages.ImportProduct.EditWarehouse"), oldWarehouseMessage, newWarehouseMessage);

                    //record history
                    await _productService.AddStockQuantityHistoryEntryAsync(product, -previousStockQuantity, 0, previousWarehouseId, message);
                    await _productService.AddStockQuantityHistoryEntryAsync(product, product.StockQuantity, product.StockQuantity, product.WarehouseId, message);
                }

                if (!isNew &&
                    product.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
                    product.BackorderMode == BackorderMode.NoBackorders &&
                    product.AllowBackInStockSubscriptions &&
                    await _productService.GetTotalStockQuantityAsync(product) > 0 &&
                    prevTotalStockQuantity <= 0 &&
                    product.Published &&
                    !product.Deleted)
                {
                    await _backInStockSubscriptionService.SendNotificationsToSubscribersAsync(product);
                }

                var tempProperty = metadata.Manager.GetProperty("SeName");

                //search engine name
                var seName = tempProperty?.StringValue ?? (isNew ? string.Empty : await _urlRecordService.GetSeNameAsync(product, 0));
                await _urlRecordService.SaveSlugAsync(product, await _urlRecordService.ValidateSeNameAsync(product, seName, product.Name, true), 0);

                tempProperty = metadata.Manager.GetProperty("Categories");

                if (tempProperty != null)
                {
                    var categoryList = tempProperty.StringValue;

                    //category mappings
                    var categories = isNew || !allProductsCategoryIds.ContainsKey(product.Id) ? Array.Empty<int>() : allProductsCategoryIds[product.Id];

                    var storesIds = product.LimitedToStores
                        ? (await _storeMappingService.GetStoresIdsWithAccessAsync(product)).ToList()
                        : new List<int>();

                    var importedCategories = await categoryList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(categoryName => new CategoryKey(categoryName, storesIds))
                        .SelectAwait(async categoryKey =>
                        {
                            var rez = (allCategories.ContainsKey(categoryKey) ? allCategories[categoryKey].Id : allCategories.Values.FirstOrDefault(c => c.Name == categoryKey.Key)?.Id) ??
                                      allCategories.FirstOrDefault(p =>
                                    p.Key.Key.Equals(categoryKey.Key, StringComparison.InvariantCultureIgnoreCase))
                                .Value?.Id;

                            if (!rez.HasValue && int.TryParse(categoryKey.Key, out var id))
                                rez = id;

                            if (!rez.HasValue)
                                //database doesn't contain the imported category
                                throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.Import.DatabaseNotContainCategory"), categoryKey.Key));

                            return rez.Value;
                        }).ToListAsync();

                    foreach (var categoryId in importedCategories)
                    {
                        if (categories.Any(c => c == categoryId))
                            continue;

                        var productCategory = new ProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId,
                            IsFeaturedProduct = false,
                            DisplayOrder = 1
                        };
                        await _categoryService.InsertProductCategoryAsync(productCategory);
                    }

                    //delete product categories
                    var deletedProductCategories = await categories.Where(categoryId => !importedCategories.Contains(categoryId))
                        .SelectAwait(async categoryId => (await _categoryService.GetProductCategoriesByProductIdAsync(product.Id, true)).FirstOrDefault(pc => pc.CategoryId == categoryId)).Where(pc => pc != null).ToListAsync();

                    foreach (var deletedProductCategory in deletedProductCategories)
                        await _categoryService.DeleteProductCategoryAsync(deletedProductCategory);
                }

                tempProperty = metadata.Manager.GetProperty("Manufacturers");
                if (tempProperty != null)
                {
                    var manufacturerList = tempProperty.StringValue;

                    //manufacturer mappings
                    var manufacturers = isNew || !allProductsManufacturerIds.ContainsKey(product.Id) ? Array.Empty<int>() : allProductsManufacturerIds[product.Id];
                    var importedManufacturers = manufacturerList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => allManufacturers.FirstOrDefault(m => m.Name == x.Trim())?.Id ?? int.Parse(x.Trim())).ToList();
                    foreach (var manufacturerId in importedManufacturers)
                    {
                        if (manufacturers.Any(c => c == manufacturerId))
                            continue;

                        var productManufacturer = new ProductManufacturer
                        {
                            ProductId = product.Id,
                            ManufacturerId = manufacturerId,
                            IsFeaturedProduct = false,
                            DisplayOrder = 1
                        };
                        await _manufacturerService.InsertProductManufacturerAsync(productManufacturer);
                    }

                    //delete product manufacturers
                    var deletedProductsManufacturers = await manufacturers.Where(manufacturerId => !importedManufacturers.Contains(manufacturerId))
                        .SelectAwait(async manufacturerId => (await _manufacturerService.GetProductManufacturersByProductIdAsync(product.Id)).First(pc => pc.ManufacturerId == manufacturerId)).ToListAsync();
                    foreach (var deletedProductManufacturer in deletedProductsManufacturers)
                        await _manufacturerService.DeleteProductManufacturerAsync(deletedProductManufacturer);
                }

                tempProperty = metadata.Manager.GetProperty("ProductTags");
                if (tempProperty != null)
                {
                    var productTags = tempProperty.StringValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

                    //searching existing product tags by their id
                    var productTagIds = productTags.Where(pt => int.TryParse(pt, out var _)).Select(int.Parse);

                    var productTagsByIds = (await _productTagService.GetAllProductTagsByProductIdAsync(product.Id)).Where(pt => productTagIds.Contains(pt.Id)).ToList();

                    productTags.AddRange(productTagsByIds.Select(pt => pt.Name));
                    var filter = productTagsByIds.Select(pt => pt.Id.ToString()).ToList();

                    //product tag mappings
                    await _productTagService.UpdateProductTagsAsync(product, productTags.Where(pt => !filter.Contains(pt)).ToArray());
                }

                tempProperty = metadata.Manager.GetProperty("LimitedToStores");
                if (tempProperty != null)
                {
                    var limitedToStoresList = tempProperty.StringValue;

                    var importedStores = product.LimitedToStores ? limitedToStoresList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => allStores.FirstOrDefault(store => store.Name == x.Trim())?.Id ?? int.Parse(x.Trim())).ToList() : new List<int>();

                    await _productService.UpdateProductStoreMappingsAsync(product, importedStores);
                }

                var picture1 = await DownloadFileAsync(metadata.Manager.GetProperty("Picture1")?.StringValue, downloadedFiles);
                var picture2 = await DownloadFileAsync(metadata.Manager.GetProperty("Picture2")?.StringValue, downloadedFiles);
                var picture3 = await DownloadFileAsync(metadata.Manager.GetProperty("Picture3")?.StringValue, downloadedFiles);

                productPictureMetadata.Add(new ProductPictureMetadata
                {
                    ProductItem = product,
                    Picture1Path = picture1,
                    Picture2Path = picture2,
                    Picture3Path = picture3,
                    IsNew = isNew
                });

                lastLoadedProduct = product;

                //update "HasTierPrices" and "HasDiscountsApplied" properties
                //_productService.UpdateHasTierPricesProperty(product);
                //_productService.UpdateHasDiscountsApplied(product);
            }

            if (_mediaSettings.ImportProductImagesUsingHash && await _pictureService.IsStoreInDbAsync())
                await ImportProductImagesUsingHashAsync(productPictureMetadata, allProductsBySku);
            else
                await ImportProductImagesUsingServicesAsync(productPictureMetadata);

            foreach (var downloadedFile in downloadedFiles)
            {
                if (!_fileProvider.FileExists(downloadedFile))
                    continue;

                try
                {
                    _fileProvider.DeleteFile(downloadedFile);
                }
                catch
                {
                    // ignored
                }
            }

            //activity log
            await _customerActivityService.InsertActivityAsync("ImportProducts", string.Format(await _localizationService.GetResourceAsync("ActivityLog.ImportProducts"), metadata.CountProductsInFile));
        }

        /// <summary>
        /// Import newsletter subscribers from TXT file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of imported subscribers
        /// </returns>
        public virtual async Task<int> ImportNewsletterSubscribersFromTxtAsync(Stream stream)
        {
            var count = 0;
            using (var reader = new StreamReader(stream))
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    var tmp = line.Split(',');

                    if (tmp.Length > 3)
                        throw new NopException("Wrong file format");

                    var isActive = true;

                    var store = await _storeContext.GetCurrentStoreAsync();
                    var storeId = store.Id;

                    //"email" field specified
                    var email = tmp[0].Trim();

                    if (!CommonHelper.IsValidEmail(email))
                        continue;

                    //"active" field specified
                    if (tmp.Length >= 2)
                        isActive = bool.Parse(tmp[1].Trim());

                    //"storeId" field specified
                    if (tmp.Length == 3)
                        storeId = int.Parse(tmp[2].Trim());

                    //import
                    var subscription = await _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreIdAsync(email, storeId);
                    if (subscription != null)
                    {
                        subscription.Email = email;
                        subscription.Active = isActive;
                        await _newsLetterSubscriptionService.UpdateNewsLetterSubscriptionAsync(subscription);
                    }
                    else
                    {
                        subscription = new NewsLetterSubscription
                        {
                            Active = isActive,
                            CreatedOnUtc = DateTime.UtcNow,
                            Email = email,
                            StoreId = storeId,
                            NewsLetterSubscriptionGuid = Guid.NewGuid()
                        };
                        await _newsLetterSubscriptionService.InsertNewsLetterSubscriptionAsync(subscription);
                    }

                    count++;
                }

            await _customerActivityService.InsertActivityAsync("ImportNewsLetterSubscriptions",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.ImportNewsLetterSubscriptions"), count));

            return count;
        }

        /// <summary>
        /// Import states from TXT file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="writeLog">Indicates whether to add logging</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of imported states
        /// </returns>
        public virtual async Task<int> ImportStatesFromTxtAsync(Stream stream, bool writeLog = true)
        {
            var count = 0;
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    var tmp = line.Split(',');

                    if (tmp.Length != 5)
                        throw new NopException("Wrong file format");

                    //parse
                    var countryTwoLetterIsoCode = tmp[0].Trim();
                    var name = tmp[1].Trim();
                    var abbreviation = tmp[2].Trim();
                    var published = bool.Parse(tmp[3].Trim());
                    var displayOrder = int.Parse(tmp[4].Trim());

                    var country = await _countryService.GetCountryByTwoLetterIsoCodeAsync(countryTwoLetterIsoCode);
                    if (country == null)
                    {
                        //country cannot be loaded. skip
                        continue;
                    }

                    //import
                    var states = await _stateProvinceService.GetStateProvincesByCountryIdAsync(country.Id, showHidden: true);
                    var state = states.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

                    if (state != null)
                    {
                        state.Abbreviation = abbreviation;
                        state.Published = published;
                        state.DisplayOrder = displayOrder;
                        await _stateProvinceService.UpdateStateProvinceAsync(state);
                    }
                    else
                    {
                        state = new StateProvince
                        {
                            CountryId = country.Id,
                            Name = name,
                            Abbreviation = abbreviation,
                            Published = published,
                            DisplayOrder = displayOrder
                        };
                        await _stateProvinceService.InsertStateProvinceAsync(state);
                    }

                    count++;
                }
            }

            //activity log
            if (writeLog)
            {
                await _customerActivityService.InsertActivityAsync("ImportStates",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.ImportStates"), count));
            }

            return count;
        }

        /// <summary>
        /// Import manufacturers from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task ImportManufacturersFromXlsxAsync(Stream stream)
        {
            using var workbook = new XLWorkbook(stream);
            // get the first worksheet in the workbook
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new NopException("No worksheet found");

            //the columns
            var properties = GetPropertiesByExcelCells<Manufacturer>(worksheet);

            var manager = new PropertyManager<Manufacturer>(properties, _catalogSettings);

            var iRow = 2;
            var setSeName = properties.Any(p => p.PropertyName == "SeName");

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                    .All(cell => cell?.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                if (allColumnsAreEmpty)
                    break;

                manager.ReadFromXlsx(worksheet, iRow);

                var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(manager.GetProperty("Id").IntValue);

                var isNew = manufacturer == null;

                manufacturer ??= new Manufacturer();

                if (isNew)
                {
                    manufacturer.CreatedOnUtc = DateTime.UtcNow;

                    //default values
                    manufacturer.PageSize = _catalogSettings.DefaultManufacturerPageSize;
                    manufacturer.PageSizeOptions = _catalogSettings.DefaultManufacturerPageSizeOptions;
                    manufacturer.Published = true;
                    manufacturer.AllowCustomersToSelectPageSize = true;
                }

                var seName = string.Empty;

                foreach (var property in manager.GetProperties)
                {
                    switch (property.PropertyName)
                    {
                        case "Name":
                            manufacturer.Name = property.StringValue;
                            break;
                        case "Description":
                            manufacturer.Description = property.StringValue;
                            break;
                        case "ManufacturerTemplateId":
                            manufacturer.ManufacturerTemplateId = property.IntValue;
                            break;
                        case "MetaKeywords":
                            manufacturer.MetaKeywords = property.StringValue;
                            break;
                        case "MetaDescription":
                            manufacturer.MetaDescription = property.StringValue;
                            break;
                        case "MetaTitle":
                            manufacturer.MetaTitle = property.StringValue;
                            break;
                        case "Picture":
                            var picture = await LoadPictureAsync(manager.GetProperty("Picture").StringValue, manufacturer.Name, isNew ? null : (int?)manufacturer.PictureId);

                            if (picture != null)
                                manufacturer.PictureId = picture.Id;

                            break;
                        case "PageSize":
                            manufacturer.PageSize = property.IntValue;
                            break;
                        case "AllowCustomersToSelectPageSize":
                            manufacturer.AllowCustomersToSelectPageSize = property.BooleanValue;
                            break;
                        case "PageSizeOptions":
                            manufacturer.PageSizeOptions = property.StringValue;
                            break;
                        case "PriceRangeFiltering":
                            manufacturer.PriceRangeFiltering = property.BooleanValue;
                            break;
                        case "PriceFrom":
                            manufacturer.PriceFrom = property.DecimalValue;
                            break;
                        case "PriceTo":
                            manufacturer.PriceTo = property.DecimalValue;
                            break;
                        case "AutomaticallyCalculatePriceRange":
                            manufacturer.ManuallyPriceRange = property.BooleanValue;
                            break;
                        case "Published":
                            manufacturer.Published = property.BooleanValue;
                            break;
                        case "DisplayOrder":
                            manufacturer.DisplayOrder = property.IntValue;
                            break;
                        case "SeName":
                            seName = property.StringValue;
                            break;
                    }
                }

                manufacturer.UpdatedOnUtc = DateTime.UtcNow;

                if (isNew)
                    await _manufacturerService.InsertManufacturerAsync(manufacturer);
                else
                    await _manufacturerService.UpdateManufacturerAsync(manufacturer);

                //search engine name
                if (setSeName)
                    await _urlRecordService.SaveSlugAsync(manufacturer, await _urlRecordService.ValidateSeNameAsync(manufacturer, seName, manufacturer.Name, true), 0);

                iRow++;
            }

            //activity log
            await _customerActivityService.InsertActivityAsync("ImportManufacturers",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.ImportManufacturers"), iRow - 2));
        }

        /// <summary>
        /// Import categories from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task ImportCategoriesFromXlsxAsync(Stream stream)
        {
            using var workboox = new XLWorkbook(stream);
            // get the first worksheet in the workbook
            var worksheet = workboox.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new NopException("No worksheet found");

            //the columns
            var properties = GetPropertiesByExcelCells<Category>(worksheet);

            var manager = new PropertyManager<Category>(properties, _catalogSettings);

            var iRow = 2;
            var setSeName = properties.Any(p => p.PropertyName == "SeName");

            //performance optimization, load all categories in one SQL request
            var allCategories = await (await _categoryService
                .GetAllCategoriesAsync(showHidden: true))
                .GroupByAwait(async c => await _categoryService.GetFormattedBreadCrumbAsync(c))
                .ToDictionaryAsync(c => c.Key, c => c.FirstAsync());

            var saveNextTime = new List<int>();

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                    .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                if (allColumnsAreEmpty)
                    break;

                //get category by data in xlsx file if it possible, or create new category
                var (category, isNew, currentCategoryBreadCrumb) = await GetCategoryFromXlsxAsync(manager, worksheet, iRow, allCategories);

                //update category by data in xlsx file
                var (seName, isParentCategoryExists) = await UpdateCategoryByXlsxAsync(category, manager, allCategories, isNew);

                if (isParentCategoryExists)
                {
                    //if parent category exists in database then save category into database
                    await SaveCategoryAsync(isNew, category, allCategories, currentCategoryBreadCrumb, setSeName, seName);
                }
                else
                {
                    //if parent category doesn't exists in database then try save category into database next time
                    saveNextTime.Add(iRow);
                }

                iRow++;
            }

            var needSave = saveNextTime.Any();

            while (needSave)
            {
                var remove = new List<int>();

                //try to save unsaved categories
                foreach (var rowId in saveNextTime)
                {
                    //get category by data in xlsx file if it possible, or create new category
                    var (category, isNew, currentCategoryBreadCrumb) = await GetCategoryFromXlsxAsync(manager, worksheet, rowId, allCategories);
                    //update category by data in xlsx file
                    var (seName, isParentCategoryExists) = await UpdateCategoryByXlsxAsync(category, manager, allCategories, isNew);

                    if (!isParentCategoryExists)
                        continue;

                    //if parent category exists in database then save category into database
                    await SaveCategoryAsync(isNew, category, allCategories, currentCategoryBreadCrumb, setSeName, seName);
                    remove.Add(rowId);
                }

                saveNextTime.RemoveAll(item => remove.Contains(item));

                needSave = remove.Any() && saveNextTime.Any();
            }

            //activity log
            await _customerActivityService.InsertActivityAsync("ImportCategories",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.ImportCategories"), iRow - 2 - saveNextTime.Count));

            if (!saveNextTime.Any())
                return;

            var categoriesName = new List<string>();

            foreach (var rowId in saveNextTime)
            {
                manager.ReadFromXlsx(worksheet, rowId);
                categoriesName.Add(manager.GetProperty("Name").StringValue);
            }

            throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Categories.Import.CategoriesArentImported"), string.Join(", ", categoriesName)));
        }

        /// <summary>
        /// Import orders from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task ImportOrdersFromXlsxAsync(Stream stream)
        {
            using var workbook = new XLWorkbook(stream);
            // get the first worksheet in the workbook
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new NopException("No worksheet found");

            var downloadedFiles = new List<string>();

            var metadata = await PrepareImportOrderDataAsync(worksheet);

            //performance optimization, load all orders by guid in one SQL request
            var allOrdersByGuids = await _orderService.GetOrdersByGuidsAsync(metadata.AllOrderGuids.ToArray());

            //performance optimization, load all customers by guid in one SQL request
            var allCustomersByGuids = await _customerService.GetCustomersByGuidsAsync(metadata.AllCustomerGuids.ToArray());

            Order lastLoadedOrder = null;

            for (var iRow = 2; iRow < metadata.EndRow; iRow++)
            {
                //imports product attributes
                if (worksheet.Row(iRow).OutlineLevel != 0)
                {
                    if (lastLoadedOrder == null)
                        continue;

                    metadata.OrderItemManager.ReadFromXlsx(worksheet, iRow, 2);

                    //skip caption row
                    if (!metadata.OrderItemManager.IsCaption)
                    {
                        await ImportOrderItemAsync(metadata.OrderItemManager, lastLoadedOrder);
                    }
                    continue;
                }

                metadata.Manager.ReadFromXlsx(worksheet, iRow);

                var order = allOrdersByGuids.FirstOrDefault(p => p.OrderGuid == metadata.Manager.GetProperty("OrderGuid").GuidValue);

                var isNew = order == null;

                order ??= new Order();

                Address orderBillingAddress = null;
                Address orderAddress = null;

                if (isNew)
                    order.CreatedOnUtc = DateTime.UtcNow;
                else
                {
                    orderBillingAddress = await _addressService.GetAddressByIdAsync(order.BillingAddressId);
                    orderAddress = await _addressService.GetAddressByIdAsync((order.PickupInStore ? order.PickupAddressId : order.ShippingAddressId) ?? 0);
                }

                orderBillingAddress ??= new Address();
                orderAddress ??= new Address();

                var customer = allCustomersByGuids.FirstOrDefault(p => p.CustomerGuid.ToString() == metadata.Manager.GetProperty("CustomerGuid").StringValue);

                foreach (var property in metadata.Manager.GetProperties)
                {
                    switch (property.PropertyName)
                    {
                        case "StoreId":
                            if (await _storeService.GetStoreByIdAsync(property.IntValue) is Store orderStore)
                                order.StoreId = property.IntValue;
                            else
                                order.StoreId = (await _storeContext.GetCurrentStoreAsync())?.Id ?? 0;
                            break;
                        case "OrderGuid":
                            order.OrderGuid = property.GuidValue;
                            break;
                        case "CustomerId":
                            order.CustomerId = customer?.Id ?? 0;
                            break;
                        case "OrderStatus":
                            order.OrderStatus = (OrderStatus)property.PropertyValue;
                            break;
                        case "PaymentStatus":
                            order.PaymentStatus = (PaymentStatus)property.PropertyValue;
                            break;
                        case "ShippingStatus":
                            order.ShippingStatus = (ShippingStatus)property.PropertyValue;
                            break;
                        case "OrderSubtotalInclTax":
                            order.OrderSubtotalInclTax = property.DecimalValue;
                            break;
                        case "OrderSubtotalExclTax":
                            order.OrderSubtotalExclTax = property.DecimalValue;
                            break;
                        case "OrderSubTotalDiscountInclTax":
                            order.OrderSubTotalDiscountInclTax = property.DecimalValue;
                            break;
                        case "OrderSubTotalDiscountExclTax":
                            order.OrderSubTotalDiscountExclTax = property.DecimalValue;
                            break;
                        case "OrderShippingInclTax":
                            order.OrderShippingInclTax = property.DecimalValue;
                            break;
                        case "OrderShippingExclTax":
                            order.OrderShippingExclTax = property.DecimalValue;
                            break;
                        case "PaymentMethodAdditionalFeeInclTax":
                            order.PaymentMethodAdditionalFeeInclTax = property.DecimalValue;
                            break;
                        case "PaymentMethodAdditionalFeeExclTax":
                            order.PaymentMethodAdditionalFeeExclTax = property.DecimalValue;
                            break;
                        case "TaxRates":
                            order.TaxRates = property.StringValue;
                            break;
                        case "OrderTax":
                            order.OrderTax = property.DecimalValue;
                            break;
                        case "OrderTotal":
                            order.OrderTotal = property.DecimalValue;
                            break;
                        case "RefundedAmount":
                            order.RefundedAmount = property.DecimalValue;
                            break;
                        case "OrderDiscount":
                            order.OrderDiscount = property.DecimalValue;
                            break;
                        case "CurrencyRate":
                            order.CurrencyRate = property.DecimalValue;
                            break;
                        case "CustomerCurrencyCode":
                            order.CustomerCurrencyCode = property.StringValue;
                            break;
                        case "AffiliateId":
                            order.AffiliateId = property.IntValue;
                            break;
                        case "PaymentMethodSystemName":
                            order.PaymentMethodSystemName = property.StringValue;
                            break;
                        case "ShippingPickupInStore":
                            order.PickupInStore = property.BooleanValue;
                            break;
                        case "ShippingMethod":
                            order.ShippingMethod = property.StringValue;
                            break;
                        case "ShippingRateComputationMethodSystemName":
                            order.ShippingRateComputationMethodSystemName = property.StringValue;
                            break;
                        case "CustomValuesXml":
                            order.CustomValuesXml = property.StringValue;
                            break;
                        case "VatNumber":
                            order.VatNumber = property.StringValue;
                            break;
                        case "CreatedOnUtc":
                            order.CreatedOnUtc = DateTime.TryParse(property.StringValue, out var createdOnUtc) ? createdOnUtc : DateTime.UtcNow;
                            break;
                        case "BillingFirstName":
                            orderBillingAddress.FirstName = property.StringValue;
                            break;
                        case "BillingLastName":
                            orderBillingAddress.LastName = property.StringValue;
                            break;
                        case "BillingPhoneNumber":
                            orderBillingAddress.PhoneNumber = property.StringValue;
                            break;
                        case "BillingEmail":
                            orderBillingAddress.Email = property.StringValue;
                            break;
                        case "BillingFaxNumber":
                            orderBillingAddress.FaxNumber = property.StringValue;
                            break;
                        case "BillingCompany":
                            orderBillingAddress.Company = property.StringValue;
                            break;
                        case "BillingAddress1":
                            orderBillingAddress.Address1 = property.StringValue;
                            break;
                        case "BillingAddress2":
                            orderBillingAddress.Address2 = property.StringValue;
                            break;
                        case "BillingCity":
                            orderBillingAddress.City = property.StringValue;
                            break;
                        case "BillingCounty":
                            orderBillingAddress.County = property.StringValue;
                            break;
                        case "BillingStateProvinceAbbreviation":
                            if (await _stateProvinceService.GetStateProvinceByAbbreviationAsync(property.StringValue) is StateProvince billingState)
                                orderBillingAddress.StateProvinceId = billingState.Id;
                            break;
                        case "BillingZipPostalCode":
                            orderBillingAddress.ZipPostalCode = property.StringValue;
                            break;
                        case "BillingCountryCode":
                            if (await _countryService.GetCountryByTwoLetterIsoCodeAsync(property.StringValue) is Country billingCountry)
                                orderBillingAddress.CountryId = billingCountry.Id;
                            break;
                        case "ShippingFirstName":
                            orderAddress.FirstName = property.StringValue;
                            break;
                        case "ShippingLastName":
                            orderAddress.LastName = property.StringValue;
                            break;
                        case "ShippingPhoneNumber":
                            orderAddress.PhoneNumber = property.StringValue;
                            break;
                        case "ShippingEmail":
                            orderAddress.Email = property.StringValue;
                            break;
                        case "ShippingFaxNumber":
                            orderAddress.FaxNumber = property.StringValue;
                            break;
                        case "ShippingCompany":
                            orderAddress.Company = property.StringValue;
                            break;
                        case "ShippingAddress1":
                            orderAddress.Address1 = property.StringValue;
                            break;
                        case "ShippingAddress2":
                            orderAddress.Address2 = property.StringValue;
                            break;
                        case "ShippingCity":
                            orderAddress.City = property.StringValue;
                            break;
                        case "ShippingCounty":
                            orderAddress.County = property.StringValue;
                            break;
                        case "ShippingStateProvinceAbbreviation":
                            if (await _stateProvinceService.GetStateProvinceByAbbreviationAsync(property.StringValue) is StateProvince shippingState)
                                orderAddress.StateProvinceId = shippingState.Id;
                            break;
                        case "ShippingZipPostalCode":
                            orderAddress.ZipPostalCode = property.StringValue;
                            break;
                        case "ShippingCountryCode":
                            if (await _countryService.GetCountryByTwoLetterIsoCodeAsync(property.StringValue) is Country shippingCountry)
                                orderAddress.CountryId = shippingCountry.Id;
                            break;
                    }
                }

                //check order address field values from excel
                if (string.IsNullOrWhiteSpace(orderAddress.FirstName) && string.IsNullOrWhiteSpace(orderAddress.LastName) &&string.IsNullOrWhiteSpace(orderAddress.Email))
                    orderAddress = null;

                //insert or update billing address
                if (orderBillingAddress.Id == 0)
                {
                    await _addressService.InsertAddressAsync(orderBillingAddress);
                    order.BillingAddressId = orderBillingAddress.Id; 
                }
                else
                    await _addressService.UpdateAddressAsync(orderBillingAddress);

                //insert or update shipping/pickup address
                if (orderAddress != null)
                {
                    if (orderAddress.Id == 0)
                    {
                        await _addressService.InsertAddressAsync(orderAddress);

                        if (order.PickupInStore)
                            order.PickupAddressId = orderAddress.Id;
                        else
                            order.ShippingAddressId = orderAddress.Id;
                    }
                    else
                        await _addressService.UpdateAddressAsync(orderAddress);
                }
                else
                    order.ShippingAddressId = null;

                //set some default values if not specified
                if (isNew)
                {
                    //customer language
                    var customerLanguage = await _languageService.GetLanguageByIdAsync(customer?.LanguageId ?? 0);
                    if (customerLanguage == null || !customerLanguage.Published)
                        customerLanguage = await _workContext.GetWorkingLanguageAsync();
                    order.CustomerLanguageId = customerLanguage.Id;

                    //tax display type
                    order.CustomerTaxDisplayType = _taxSettings.TaxDisplayType;

                    //set other default values
                    order.AllowStoringCreditCardNumber = false;
                    order.AuthorizationTransactionCode = string.Empty;
                    order.AuthorizationTransactionId = string.Empty;
                    order.AuthorizationTransactionResult = string.Empty;
                    order.CaptureTransactionId = string.Empty;
                    order.CaptureTransactionResult = string.Empty;
                    order.CardCvv2 = string.Empty;
                    order.CardExpirationMonth = string.Empty;
                    order.CardExpirationYear = string.Empty;
                    order.CardName = string.Empty;
                    order.CardNumber = string.Empty;
                    order.CardType = string.Empty;
                    order.CustomerIp = string.Empty;
                    order.CustomOrderNumber = string.Empty;
                    order.MaskedCreditCardNumber = string.Empty;
                    order.RefundedAmount = decimal.Zero;
                    order.SubscriptionTransactionId = string.Empty;

                    await _orderService.InsertOrderAsync(order);

                    //generate and set custom order number
                    order.CustomOrderNumber = _customNumberFormatter.GenerateOrderCustomNumber(order);
                    await _orderService.UpdateOrderAsync(order);
                }
                else
                    await _orderService.UpdateOrderAsync(order);

                lastLoadedOrder = order;
            }

            //activity log
            await _customerActivityService.InsertActivityAsync("ImportOrders", string.Format(await _localizationService.GetResourceAsync("ActivityLog.ImportOrders"), metadata.CountOrdersInFile));
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