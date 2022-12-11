/* 
 * Selling Partner API for Fulfillment Outbound
 *
 * The Selling Partner API for Fulfillment Outbound lets you create applications that help a seller fulfill Multi-Channel Fulfillment orders using their inventory in Amazon's fulfillment network. You can get information on both potential and existing fulfillment orders.
 *
 * OpenAPI spec version: v0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace FikaAmazonAPI.AmazonSpApiSDK.Models.FulfillmentOutbound
{
    /// <summary>
    /// GetFulfillmentOrderResult
    /// </summary>
    [DataContract]
    public partial class GetFulfillmentOrderResult : IEquatable<GetFulfillmentOrderResult>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFulfillmentOrderResult" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public GetFulfillmentOrderResult() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFulfillmentOrderResult" /> class.
        /// </summary>
        /// <param name="FulfillmentOrder">FulfillmentOrder (required).</param>
        /// <param name="FulfillmentOrderItem">FulfillmentOrderItem (required).</param>
        /// <param name="FulfillmentShipment">FulfillmentShipment.</param>
        /// <param name="ReturnItemList">ReturnItemList (required).</param>
        /// <param name="ReturnAuthorizationList">ReturnAuthorizationList (required).</param>
        public GetFulfillmentOrderResult(FulfillmentOrder FulfillmentOrder = default(FulfillmentOrder), FulfillmentOrderItemList FulfillmentOrderItems = default(FulfillmentOrderItemList), FulfillmentShipmentList FulfillmentShipments = default(FulfillmentShipmentList), ReturnItemList ReturnItems = default(ReturnItemList), ReturnAuthorizationList ReturnAuthorizations = default(ReturnAuthorizationList))
        {
            // to ensure "FulfillmentOrder" is required (not null)
            if (FulfillmentOrder == null)
            {
                throw new InvalidDataException("FulfillmentOrder is a required property for GetFulfillmentOrderResult and cannot be null");
            }
            else
            {
                this.FulfillmentOrder = FulfillmentOrder;
            }
            // to ensure "FulfillmentOrderItem" is required (not null)
            if (FulfillmentOrderItems == null)
            {
                throw new InvalidDataException("FulfillmentOrderItems is a required property for GetFulfillmentOrderResult and cannot be null");
            }
            else
            {
                this.FulfillmentOrderItems = FulfillmentOrderItems;
            }
            // to ensure "ReturnItemList" is required (not null)
            if (ReturnItems == null)
            {
                throw new InvalidDataException("ReturnItems is a required property for GetFulfillmentOrderResult and cannot be null");
            }
            else
            {
                this.ReturnItems = ReturnItems;
            }
            // to ensure "ReturnAuthorizationList" is required (not null)
            if (ReturnAuthorizations == null)
            {
                throw new InvalidDataException("ReturnAuthorizations is a required property for GetFulfillmentOrderResult and cannot be null");
            }
            else
            {
                this.ReturnAuthorizations = ReturnAuthorizations;
            }
            this.FulfillmentShipments = FulfillmentShipments;
        }

        /// <summary>
        /// Gets or Sets FulfillmentOrder
        /// </summary>
        [DataMember(Name = "FulfillmentOrder", EmitDefaultValue = false)]
        public FulfillmentOrder FulfillmentOrder { get; set; }

        /// <summary>
        /// Gets or Sets FulfillmentOrderItems
        /// </summary>
        [DataMember(Name = "FulfillmentOrderItems", EmitDefaultValue = false)]
        public FulfillmentOrderItemList FulfillmentOrderItems { get; set; }

        /// <summary>
        /// Gets or Sets FulfillmentShipments
        /// </summary>
        [DataMember(Name = "FulfillmentShipments", EmitDefaultValue = false)]
        public FulfillmentShipmentList FulfillmentShipments { get; set; }

        /// <summary>
        /// Gets or Sets ReturnItems
        /// </summary>
        [DataMember(Name = "ReturnItems", EmitDefaultValue = false)]
        public ReturnItemList ReturnItems { get; set; }

        /// <summary>
        /// Gets or Sets ReturnAuthorizations
        /// </summary>
        [DataMember(Name = "ReturnAuthorizations", EmitDefaultValue = false)]
        public ReturnAuthorizationList ReturnAuthorizations { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetFulfillmentOrderResult {\n");
            sb.Append("  FulfillmentOrder: ").Append(FulfillmentOrder).Append("\n");
            sb.Append("  FulfillmentOrderItems: ").Append(FulfillmentOrderItems).Append("\n");
            sb.Append("  FulfillmentShipments: ").Append(FulfillmentShipments).Append("\n");
            sb.Append("  ReturnItems: ").Append(ReturnItems).Append("\n");
            sb.Append("  ReturnAuthorizations: ").Append(ReturnAuthorizations).Append("\n");
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
            return this.Equals(input as GetFulfillmentOrderResult);
        }

        /// <summary>
        /// Returns true if GetFulfillmentOrderResult instances are equal
        /// </summary>
        /// <param name="input">Instance of GetFulfillmentOrderResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetFulfillmentOrderResult input)
        {
            if (input == null)
                return false;

            return
                (
                    this.FulfillmentOrder == input.FulfillmentOrder ||
                    (this.FulfillmentOrder != null &&
                    this.FulfillmentOrder.Equals(input.FulfillmentOrder))
                ) &&
                (
                    this.FulfillmentOrderItems == input.FulfillmentOrderItems ||
                    (this.FulfillmentOrderItems != null &&
                    this.FulfillmentOrderItems.Equals(input.FulfillmentOrderItems))
                ) &&
                (
                    this.FulfillmentShipments == input.FulfillmentShipments ||
                    (this.FulfillmentShipments != null &&
                    this.FulfillmentShipments.Equals(input.FulfillmentShipments))
                ) &&
                (
                    this.ReturnItems == input.ReturnItems ||
                    (this.ReturnItems != null &&
                    this.ReturnItems.Equals(input.ReturnItems))
                ) &&
                (
                    this.ReturnAuthorizations == input.ReturnAuthorizations ||
                    (this.ReturnAuthorizations != null &&
                    this.ReturnAuthorizations.Equals(input.ReturnAuthorizations))
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
                if (this.FulfillmentOrder != null)
                    hashCode = hashCode * 59 + this.FulfillmentOrder.GetHashCode();
                if (this.FulfillmentOrderItems != null)
                    hashCode = hashCode * 59 + this.FulfillmentOrderItems.GetHashCode();
                if (this.FulfillmentShipments != null)
                    hashCode = hashCode * 59 + this.FulfillmentShipments.GetHashCode();
                if (this.ReturnItems != null)
                    hashCode = hashCode * 59 + this.ReturnItems.GetHashCode();
                if (this.ReturnAuthorizations != null)
                    hashCode = hashCode * 59 + this.ReturnAuthorizations.GetHashCode();
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