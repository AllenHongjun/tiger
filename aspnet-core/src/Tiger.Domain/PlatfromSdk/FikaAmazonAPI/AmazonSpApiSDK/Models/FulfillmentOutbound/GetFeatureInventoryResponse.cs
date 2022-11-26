/* 
 * Selling Partner APIs for Fulfillment Outbound
 *
 * The Selling Partner API for Fulfillment Outbound lets you create applications that help a seller fulfill Multi-Channel Fulfillment orders using their inventory in Amazon's fulfillment network. You can get information on both potential and existing fulfillment orders.
 *
 * OpenAPI spec version: 2020-07-01
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace FikaAmazonAPI.AmazonSpApiSDK.Models.FulfillmentOutbound
{
    /// <summary>
    /// The breakdown of eligibility inventory by feature.
    /// </summary>
    [DataContract]
    public partial class GetFeatureInventoryResponse :  IEquatable<GetFeatureInventoryResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeatureInventoryResponse" /> class.
        /// </summary>
        /// <param name="payload">payload.</param>
        /// <param name="errors">One or more unexpected errors occurred during the getFeatureInventory operation..</param>
        public GetFeatureInventoryResponse(GetFeatureInventoryResult payload = default(GetFeatureInventoryResult), ErrorList errors = default(ErrorList))
        {
            this.Payload = payload;
            this.Errors = errors;
        }
        public GetFeatureInventoryResponse()
        {
            this.Payload = default(GetFeatureInventoryResult);
            this.Errors = default(ErrorList);
        }

        /// <summary>
        /// Gets or Sets Payload
        /// </summary>
        [DataMember(Name="payload", EmitDefaultValue=false)]
        public GetFeatureInventoryResult Payload { get; set; }

        /// <summary>
        /// One or more unexpected errors occurred during the getFeatureInventory operation.
        /// </summary>
        /// <value>One or more unexpected errors occurred during the getFeatureInventory operation.</value>
        [DataMember(Name="errors", EmitDefaultValue=false)]
        public ErrorList Errors { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetFeatureInventoryResponse {\n");
            sb.Append("  Payload: ").Append(Payload).Append("\n");
            sb.Append("  Errors: ").Append(Errors).Append("\n");
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
            return this.Equals(input as GetFeatureInventoryResponse);
        }

        /// <summary>
        /// Returns true if GetFeatureInventoryResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of GetFeatureInventoryResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetFeatureInventoryResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Payload == input.Payload ||
                    (this.Payload != null &&
                    this.Payload.Equals(input.Payload))
                ) && 
                (
                    this.Errors == input.Errors ||
                    (this.Errors != null &&
                    this.Errors.Equals(input.Errors))
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
                if (this.Payload != null)
                    hashCode = hashCode * 59 + this.Payload.GetHashCode();
                if (this.Errors != null)
                    hashCode = hashCode * 59 + this.Errors.GetHashCode();
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
