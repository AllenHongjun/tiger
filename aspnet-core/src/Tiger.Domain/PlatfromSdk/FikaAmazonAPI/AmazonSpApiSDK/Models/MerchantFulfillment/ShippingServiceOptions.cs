/* 
 * Selling Partner API for Merchant Fulfillment
 *
 * The Selling Partner API for Merchant Fulfillment helps you build applications that let sellers purchase shipping for non-Prime and Prime orders using Amazon’s Buy Shipping Services.
 *
 * OpenAPI spec version: v0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;


namespace FikaAmazonAPI.AmazonSpApiSDK.Models.MerchantFulfillment
{
    /// <summary>
    /// Extra services provided by a carrier.
    /// </summary>
    [DataContract]
    public partial class ShippingServiceOptions :  IEquatable<ShippingServiceOptions>, IValidatableObject
    {
        /// <summary>
        /// The delivery confirmation level.
        /// </summary>
        /// <value>The delivery confirmation level.</value>
        [DataMember(Name="DeliveryExperience", EmitDefaultValue=false)]
        public DeliveryExperienceType DeliveryExperience { get; set; }
        /// <summary>
        /// Gets or Sets CarrierWillPickUpOption
        /// </summary>
        [DataMember(Name="CarrierWillPickUpOption", EmitDefaultValue=false)]
        public CarrierWillPickUpOption? CarrierWillPickUpOption { get; set; }
        /// <summary>
        /// The seller&#39;s preferred label format.
        /// </summary>
        /// <value>The seller&#39;s preferred label format.</value>
        [DataMember(Name="LabelFormat", EmitDefaultValue=false)]
        public LabelFormat? LabelFormat { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingServiceOptions" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public ShippingServiceOptions() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingServiceOptions" /> class.
        /// </summary>
        /// <param name="deliveryExperience">The delivery confirmation level. (required).</param>
        /// <param name="declaredValue">The declared value of the shipment. The carrier uses this value to determine the amount to use to insure the shipment. If DeclaredValue is greater than the carrier&#39;s minimum insurance amount, the seller is charged for the additional insurance as determined by the carrier. For information about optional insurance coverage, see the Seller Central Help [UK](https://sellercentral.amazon.co.uk/gp/help/200204080) [US](https://sellercentral.amazon.com/gp/help/200204080)..</param>
        /// <param name="carrierWillPickUp">When true, the carrier will pick up the package.  Note: Scheduled carrier pickup is available only using Dynamex (US), DPD (UK), and Royal Mail (UK). (required).</param>
        /// <param name="carrierWillPickUpOption">carrierWillPickUpOption.</param>
        /// <param name="labelFormat">The seller&#39;s preferred label format..</param>
        public ShippingServiceOptions(DeliveryExperienceType deliveryExperience = default(DeliveryExperienceType), CurrencyAmount declaredValue = default(CurrencyAmount), bool? carrierWillPickUp = default(bool?), CarrierWillPickUpOption? carrierWillPickUpOption = default(CarrierWillPickUpOption?), LabelFormat? labelFormat = default(LabelFormat?))
        {
            // to ensure "deliveryExperience" is required (not null)
            if (deliveryExperience == null)
            {
                throw new InvalidDataException("deliveryExperience is a required property for ShippingServiceOptions and cannot be null");
            }
            else
            {
                this.DeliveryExperience = deliveryExperience;
            }
            // to ensure "carrierWillPickUp" is required (not null)
            if (carrierWillPickUp == null)
            {
                throw new InvalidDataException("carrierWillPickUp is a required property for ShippingServiceOptions and cannot be null");
            }
            else
            {
                this.CarrierWillPickUp = carrierWillPickUp;
            }
            this.DeclaredValue = declaredValue;
            this.CarrierWillPickUpOption = carrierWillPickUpOption;
            this.LabelFormat = labelFormat;
        }
        

        /// <summary>
        /// The declared value of the shipment. The carrier uses this value to determine the amount to use to insure the shipment. If DeclaredValue is greater than the carrier&#39;s minimum insurance amount, the seller is charged for the additional insurance as determined by the carrier. For information about optional insurance coverage, see the Seller Central Help [UK](https://sellercentral.amazon.co.uk/gp/help/200204080) [US](https://sellercentral.amazon.com/gp/help/200204080).
        /// </summary>
        /// <value>The declared value of the shipment. The carrier uses this value to determine the amount to use to insure the shipment. If DeclaredValue is greater than the carrier&#39;s minimum insurance amount, the seller is charged for the additional insurance as determined by the carrier. For information about optional insurance coverage, see the Seller Central Help [UK](https://sellercentral.amazon.co.uk/gp/help/200204080) [US](https://sellercentral.amazon.com/gp/help/200204080).</value>
        [DataMember(Name="DeclaredValue", EmitDefaultValue=false)]
        public CurrencyAmount DeclaredValue { get; set; }

        /// <summary>
        /// When true, the carrier will pick up the package.  Note: Scheduled carrier pickup is available only using Dynamex (US), DPD (UK), and Royal Mail (UK).
        /// </summary>
        /// <value>When true, the carrier will pick up the package.  Note: Scheduled carrier pickup is available only using Dynamex (US), DPD (UK), and Royal Mail (UK).</value>
        [DataMember(Name="CarrierWillPickUp", EmitDefaultValue=false)]
        public bool? CarrierWillPickUp { get; set; }



        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ShippingServiceOptions {\n");
            sb.Append("  DeliveryExperience: ").Append(DeliveryExperience).Append("\n");
            sb.Append("  DeclaredValue: ").Append(DeclaredValue).Append("\n");
            sb.Append("  CarrierWillPickUp: ").Append(CarrierWillPickUp).Append("\n");
            sb.Append("  CarrierWillPickUpOption: ").Append(CarrierWillPickUpOption).Append("\n");
            sb.Append("  LabelFormat: ").Append(LabelFormat).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
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
            return this.Equals(input as ShippingServiceOptions);
        }

        /// <summary>
        /// Returns true if ShippingServiceOptions instances are equal
        /// </summary>
        /// <param name="input">Instance of ShippingServiceOptions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ShippingServiceOptions input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DeliveryExperience == input.DeliveryExperience ||
                    (this.DeliveryExperience != null &&
                    this.DeliveryExperience.Equals(input.DeliveryExperience))
                ) && 
                (
                    this.DeclaredValue == input.DeclaredValue ||
                    (this.DeclaredValue != null &&
                    this.DeclaredValue.Equals(input.DeclaredValue))
                ) && 
                (
                    this.CarrierWillPickUp == input.CarrierWillPickUp ||
                    (this.CarrierWillPickUp != null &&
                    this.CarrierWillPickUp.Equals(input.CarrierWillPickUp))
                ) && 
                (
                    this.CarrierWillPickUpOption == input.CarrierWillPickUpOption ||
                    (this.CarrierWillPickUpOption != null &&
                    this.CarrierWillPickUpOption.Equals(input.CarrierWillPickUpOption))
                ) && 
                (
                    this.LabelFormat == input.LabelFormat ||
                    (this.LabelFormat != null &&
                    this.LabelFormat.Equals(input.LabelFormat))
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
                if (this.DeliveryExperience != null)
                    hashCode = hashCode * 59 + this.DeliveryExperience.GetHashCode();
                if (this.DeclaredValue != null)
                    hashCode = hashCode * 59 + this.DeclaredValue.GetHashCode();
                if (this.CarrierWillPickUp != null)
                    hashCode = hashCode * 59 + this.CarrierWillPickUp.GetHashCode();
                if (this.CarrierWillPickUpOption != null)
                    hashCode = hashCode * 59 + this.CarrierWillPickUpOption.GetHashCode();
                if (this.LabelFormat != null)
                    hashCode = hashCode * 59 + this.LabelFormat.GetHashCode();
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
