/* 
 * Selling Partner API for Direct Fulfillment Orders
 *
 * The Selling Partner API for Direct Fulfillment Orders provides programmatic access to a direct fulfillment vendor's order data.
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using FikaAmazonAPI.AmazonSpApiSDK.Models.Authorization;

namespace FikaAmazonAPI.AmazonSpApiSDK.Models.VendorDirectFulfillmentOrders
{
    /// <summary>
    /// The response schema for the getOrders operation.
    /// </summary>
    [DataContract]
    public partial class GetOrdersResponse :  IEquatable<GetOrdersResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOrdersResponse" /> class.
        /// </summary>
        /// <param name="payload">A list of purchase orders..</param>
        /// <param name="errors">errors.</param>
        public GetOrdersResponse(OrderList payload = default(OrderList), ErrorList errors = default(ErrorList))
        {
            this.Payload = payload;
            this.Errors = errors;
        }
        public GetOrdersResponse()
        {
            this.Payload = default(OrderList);
            this.Errors = default(ErrorList);
        }
        /// <summary>
        /// A list of purchase orders.
        /// </summary>
        /// <value>A list of purchase orders.</value>
        [DataMember(Name="payload", EmitDefaultValue=false)]
        public OrderList Payload { get; set; }

        /// <summary>
        /// Gets or Sets Errors
        /// </summary>
        [DataMember(Name="errors", EmitDefaultValue=false)]
        public ErrorList Errors { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetOrdersResponse {\n");
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
            return this.Equals(input as GetOrdersResponse);
        }

        /// <summary>
        /// Returns true if GetOrdersResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of GetOrdersResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetOrdersResponse input)
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