/* 
 * Selling Partner API for Fulfillment Inbound
 *
 * The Selling Partner API for Fulfillment Inbound lets you create applications that create and update inbound shipments of inventory to Amazon's fulfillment network.
 *
 * OpenAPI spec version: v0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace FikaAmazonAPI.AmazonSpApiSDK.Models.FulfillmentInbound
{
    /// <summary>
    /// Labeling requirements and item preparation instructions to help you prepare items for shipment to Amazon&#39;s fulfillment network.
    /// </summary>
    [DataContract]
    public partial class SKUPrepInstructions : IEquatable<SKUPrepInstructions>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets BarcodeInstruction
        /// </summary>
        [DataMember(Name = "BarcodeInstruction", EmitDefaultValue = false)]
        public BarcodeInstruction? BarcodeInstruction { get; set; }
        /// <summary>
        /// Gets or Sets PrepGuidance
        /// </summary>
        [DataMember(Name = "PrepGuidance", EmitDefaultValue = false)]
        public PrepGuidance? PrepGuidance { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SKUPrepInstructions" /> class.
        /// </summary>
        /// <param name="SellerSKU">The seller SKU of the item..</param>
        /// <param name="ASIN">The Amazon Standard Identification Number (ASIN) of the item..</param>
        /// <param name="BarcodeInstruction">BarcodeInstruction.</param>
        /// <param name="PrepGuidance">PrepGuidance.</param>
        /// <param name="PrepInstructionList">PrepInstructionList.</param>
        /// <param name="AmazonPrepFeesDetailsList">AmazonPrepFeesDetailsList.</param>
        public SKUPrepInstructions(string SellerSKU = default(string), string ASIN = default(string), BarcodeInstruction? BarcodeInstruction = default(BarcodeInstruction?), PrepGuidance? PrepGuidance = default(PrepGuidance?), PrepInstructionList PrepInstructionList = default(PrepInstructionList), AmazonPrepFeesDetailsList AmazonPrepFeesDetailsList = default(AmazonPrepFeesDetailsList))
        {
            this.SellerSKU = SellerSKU;
            this.ASIN = ASIN;
            this.BarcodeInstruction = BarcodeInstruction;
            this.PrepGuidance = PrepGuidance;
            this.PrepInstructionList = PrepInstructionList;
            this.AmazonPrepFeesDetailsList = AmazonPrepFeesDetailsList;
        }

        /// <summary>
        /// The seller SKU of the item.
        /// </summary>
        /// <value>The seller SKU of the item.</value>
        [DataMember(Name = "SellerSKU", EmitDefaultValue = false)]
        public string SellerSKU { get; set; }

        /// <summary>
        /// The Amazon Standard Identification Number (ASIN) of the item.
        /// </summary>
        /// <value>The Amazon Standard Identification Number (ASIN) of the item.</value>
        [DataMember(Name = "ASIN", EmitDefaultValue = false)]
        public string ASIN { get; set; }



        /// <summary>
        /// Gets or Sets PrepInstructionList
        /// </summary>
        [DataMember(Name = "PrepInstructionList", EmitDefaultValue = false)]
        public PrepInstructionList PrepInstructionList { get; set; }

        /// <summary>
        /// Gets or Sets AmazonPrepFeesDetailsList
        /// </summary>
        [DataMember(Name = "AmazonPrepFeesDetailsList", EmitDefaultValue = false)]
        public AmazonPrepFeesDetailsList AmazonPrepFeesDetailsList { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SKUPrepInstructions {\n");
            sb.Append("  SellerSKU: ").Append(SellerSKU).Append("\n");
            sb.Append("  ASIN: ").Append(ASIN).Append("\n");
            sb.Append("  BarcodeInstruction: ").Append(BarcodeInstruction).Append("\n");
            sb.Append("  PrepGuidance: ").Append(PrepGuidance).Append("\n");
            sb.Append("  PrepInstructionList: ").Append(PrepInstructionList).Append("\n");
            sb.Append("  AmazonPrepFeesDetailsList: ").Append(AmazonPrepFeesDetailsList).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as SKUPrepInstructions);
        }

        /// <summary>
        /// Returns true if SKUPrepInstructions instances are equal
        /// </summary>
        /// <param name="input">Instance of SKUPrepInstructions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SKUPrepInstructions input)
        {
            if (input == null)
                return false;

            return
                (
                    this.SellerSKU == input.SellerSKU ||
                    (this.SellerSKU != null &&
                    this.SellerSKU.Equals(input.SellerSKU))
                ) &&
                (
                    this.ASIN == input.ASIN ||
                    (this.ASIN != null &&
                    this.ASIN.Equals(input.ASIN))
                ) &&
                (
                    this.BarcodeInstruction == input.BarcodeInstruction ||
                    (this.BarcodeInstruction != null &&
                    this.BarcodeInstruction.Equals(input.BarcodeInstruction))
                ) &&
                (
                    this.PrepGuidance == input.PrepGuidance ||
                    (this.PrepGuidance != null &&
                    this.PrepGuidance.Equals(input.PrepGuidance))
                ) &&
                (
                    this.PrepInstructionList == input.PrepInstructionList ||
                    (this.PrepInstructionList != null &&
                    this.PrepInstructionList.Equals(input.PrepInstructionList))
                ) &&
                (
                    this.AmazonPrepFeesDetailsList == input.AmazonPrepFeesDetailsList ||
                    (this.AmazonPrepFeesDetailsList != null &&
                    this.AmazonPrepFeesDetailsList.Equals(input.AmazonPrepFeesDetailsList))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.SellerSKU != null)
                    hashCode = hashCode * 59 + this.SellerSKU.GetHashCode();
                if (this.ASIN != null)
                    hashCode = hashCode * 59 + this.ASIN.GetHashCode();
                if (this.BarcodeInstruction != null)
                    hashCode = hashCode * 59 + this.BarcodeInstruction.GetHashCode();
                if (this.PrepGuidance != null)
                    hashCode = hashCode * 59 + this.PrepGuidance.GetHashCode();
                if (this.PrepInstructionList != null)
                    hashCode = hashCode * 59 + this.PrepInstructionList.GetHashCode();
                if (this.AmazonPrepFeesDetailsList != null)
                    hashCode = hashCode * 59 + this.AmazonPrepFeesDetailsList.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
