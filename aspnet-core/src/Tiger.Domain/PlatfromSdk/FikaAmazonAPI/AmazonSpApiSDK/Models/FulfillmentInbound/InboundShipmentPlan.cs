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
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace FikaAmazonAPI.AmazonSpApiSDK.Models.FulfillmentInbound
{
    /// <summary>
    /// Inbound shipment information used to create an inbound shipment. Returned by the createInboundShipmentPlan operation.
    /// </summary>
    [DataContract]
    public partial class InboundShipmentPlan : IEquatable<InboundShipmentPlan>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets LabelPrepType
        /// </summary>
        [DataMember(Name = "LabelPrepType", EmitDefaultValue = false)]
        public LabelPrepType LabelPrepType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="InboundShipmentPlan" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public InboundShipmentPlan() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="InboundShipmentPlan" /> class.
        /// </summary>
        /// <param name="ShipmentId">A shipment identifier originally returned by the createInboundShipmentPlan operation. (required).</param>
        /// <param name="DestinationFulfillmentCenterId">An Amazon fulfillment center identifier created by Amazon. (required).</param>
        /// <param name="ShipToAddress">The address of the Amazon fulfillment center to which to ship the items. (required).</param>
        /// <param name="LabelPrepType">LabelPrepType (required).</param>
        /// <param name="Items">SKU and quantity information for the items in the shipment. (required).</param>
        /// <param name="EstimatedBoxContentsFee">EstimatedBoxContentsFee.</param>
        public InboundShipmentPlan(string ShipmentId = default(string), string DestinationFulfillmentCenterId = default(string), Address ShipToAddress = default(Address), LabelPrepType LabelPrepType = default(LabelPrepType), InboundShipmentPlanItemList Items = default(InboundShipmentPlanItemList), BoxContentsFeeDetails EstimatedBoxContentsFee = default(BoxContentsFeeDetails))
        {
            // to ensure "ShipmentId" is required (not null)
            if (ShipmentId == null)
            {
                throw new InvalidDataException("ShipmentId is a required property for InboundShipmentPlan and cannot be null");
            }
            else
            {
                this.ShipmentId = ShipmentId;
            }
            // to ensure "DestinationFulfillmentCenterId" is required (not null)
            if (DestinationFulfillmentCenterId == null)
            {
                throw new InvalidDataException("DestinationFulfillmentCenterId is a required property for InboundShipmentPlan and cannot be null");
            }
            else
            {
                this.DestinationFulfillmentCenterId = DestinationFulfillmentCenterId;
            }
            // to ensure "ShipToAddress" is required (not null)
            if (ShipToAddress == null)
            {
                throw new InvalidDataException("ShipToAddress is a required property for InboundShipmentPlan and cannot be null");
            }
            else
            {
                this.ShipToAddress = ShipToAddress;
            }
            // to ensure "LabelPrepType" is required (not null)
            if (LabelPrepType == null)
            {
                throw new InvalidDataException("LabelPrepType is a required property for InboundShipmentPlan and cannot be null");
            }
            else
            {
                this.LabelPrepType = LabelPrepType;
            }
            // to ensure "Items" is required (not null)
            if (Items == null)
            {
                throw new InvalidDataException("Items is a required property for InboundShipmentPlan and cannot be null");
            }
            else
            {
                this.Items = Items;
            }
            this.EstimatedBoxContentsFee = EstimatedBoxContentsFee;
        }

        /// <summary>
        /// A shipment identifier originally returned by the createInboundShipmentPlan operation.
        /// </summary>
        /// <value>A shipment identifier originally returned by the createInboundShipmentPlan operation.</value>
        [DataMember(Name = "ShipmentId", EmitDefaultValue = false)]
        public string ShipmentId { get; set; }

        /// <summary>
        /// An Amazon fulfillment center identifier created by Amazon.
        /// </summary>
        /// <value>An Amazon fulfillment center identifier created by Amazon.</value>
        [DataMember(Name = "DestinationFulfillmentCenterId", EmitDefaultValue = false)]
        public string DestinationFulfillmentCenterId { get; set; }

        /// <summary>
        /// The address of the Amazon fulfillment center to which to ship the items.
        /// </summary>
        /// <value>The address of the Amazon fulfillment center to which to ship the items.</value>
        [DataMember(Name = "ShipToAddress", EmitDefaultValue = false)]
        public Address ShipToAddress { get; set; }


        /// <summary>
        /// SKU and quantity information for the items in the shipment.
        /// </summary>
        /// <value>SKU and quantity information for the items in the shipment.</value>
        [DataMember(Name = "Items", EmitDefaultValue = false)]
        public InboundShipmentPlanItemList Items { get; set; }

        /// <summary>
        /// Gets or Sets EstimatedBoxContentsFee
        /// </summary>
        [DataMember(Name = "EstimatedBoxContentsFee", EmitDefaultValue = false)]
        public BoxContentsFeeDetails EstimatedBoxContentsFee { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InboundShipmentPlan {\n");
            sb.Append("  ShipmentId: ").Append(ShipmentId).Append("\n");
            sb.Append("  DestinationFulfillmentCenterId: ").Append(DestinationFulfillmentCenterId).Append("\n");
            sb.Append("  ShipToAddress: ").Append(ShipToAddress).Append("\n");
            sb.Append("  LabelPrepType: ").Append(LabelPrepType).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");
            sb.Append("  EstimatedBoxContentsFee: ").Append(EstimatedBoxContentsFee).Append("\n");
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
            return this.Equals(input as InboundShipmentPlan);
        }

        /// <summary>
        /// Returns true if InboundShipmentPlan instances are equal
        /// </summary>
        /// <param name="input">Instance of InboundShipmentPlan to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InboundShipmentPlan input)
        {
            if (input == null)
                return false;

            return
                (
                    this.ShipmentId == input.ShipmentId ||
                    (this.ShipmentId != null &&
                    this.ShipmentId.Equals(input.ShipmentId))
                ) &&
                (
                    this.DestinationFulfillmentCenterId == input.DestinationFulfillmentCenterId ||
                    (this.DestinationFulfillmentCenterId != null &&
                    this.DestinationFulfillmentCenterId.Equals(input.DestinationFulfillmentCenterId))
                ) &&
                (
                    this.ShipToAddress == input.ShipToAddress ||
                    (this.ShipToAddress != null &&
                    this.ShipToAddress.Equals(input.ShipToAddress))
                ) &&
                (
                    this.LabelPrepType == input.LabelPrepType ||
                    (this.LabelPrepType != null &&
                    this.LabelPrepType.Equals(input.LabelPrepType))
                ) &&
                (
                    this.Items == input.Items ||
                    (this.Items != null &&
                    this.Items.Equals(input.Items))
                ) &&
                (
                    this.EstimatedBoxContentsFee == input.EstimatedBoxContentsFee ||
                    (this.EstimatedBoxContentsFee != null &&
                    this.EstimatedBoxContentsFee.Equals(input.EstimatedBoxContentsFee))
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
                if (this.ShipmentId != null)
                    hashCode = hashCode * 59 + this.ShipmentId.GetHashCode();
                if (this.DestinationFulfillmentCenterId != null)
                    hashCode = hashCode * 59 + this.DestinationFulfillmentCenterId.GetHashCode();
                if (this.ShipToAddress != null)
                    hashCode = hashCode * 59 + this.ShipToAddress.GetHashCode();
                if (this.LabelPrepType != null)
                    hashCode = hashCode * 59 + this.LabelPrepType.GetHashCode();
                if (this.Items != null)
                    hashCode = hashCode * 59 + this.Items.GetHashCode();
                if (this.EstimatedBoxContentsFee != null)
                    hashCode = hashCode * 59 + this.EstimatedBoxContentsFee.GetHashCode();
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
