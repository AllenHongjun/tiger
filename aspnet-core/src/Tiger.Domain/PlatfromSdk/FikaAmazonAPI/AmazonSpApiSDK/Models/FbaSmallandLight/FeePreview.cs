/* 
 * Selling Partner API for FBA Small And Light
 *
 * The Selling Partner API for FBA Small and Light lets you help sellers manage their listings in the Small and Light program. The program reduces the cost of fulfilling orders for small and lightweight FBA inventory. You can enroll or remove items from the program and check item eligibility and enrollment status. You can also preview the estimated program fees charged to a seller for items sold while enrolled in the program.
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FikaAmazonAPI.AmazonSpApiSDK.Models.FbaSmallandLight
{
    /// <summary>
    /// The fee estimate for a specific item.
    /// </summary>
    [DataContract]
    public partial class FeePreview : IEquatable<FeePreview>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeePreview" /> class.
        /// </summary>
        /// <param name="Asin">The Amazon Standard Identification Number (ASIN) value used to identify the item..</param>
        /// <param name="Price">The price that the seller plans to charge for the item..</param>
        /// <param name="FeeBreakdown">A list of the Small and Light fees for the item..</param>
        /// <param name="TotalFees">The total fees charged if the item participated in the Small and Light program..</param>
        /// <param name="Errors">One or more unexpected errors occurred during the getSmallAndLightFeePreview operation..</param>
        public FeePreview(string Asin = default(string), MoneyType Price = default(MoneyType), List<FeeLineItem> FeeBreakdown = default(List<FeeLineItem>), MoneyType TotalFees = default(MoneyType), ErrorList Errors = default(ErrorList))
        {
            this.Asin = Asin;
            this.Price = Price;
            this.FeeBreakdown = FeeBreakdown;
            this.TotalFees = TotalFees;
            this.Errors = Errors;
        }

        /// <summary>
        /// The Amazon Standard Identification Number (ASIN) value used to identify the item.
        /// </summary>
        /// <value>The Amazon Standard Identification Number (ASIN) value used to identify the item.</value>
        [DataMember(Name = "asin", EmitDefaultValue = false)]
        public string Asin { get; set; }

        /// <summary>
        /// The price that the seller plans to charge for the item.
        /// </summary>
        /// <value>The price that the seller plans to charge for the item.</value>
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public MoneyType Price { get; set; }

        /// <summary>
        /// A list of the Small and Light fees for the item.
        /// </summary>
        /// <value>A list of the Small and Light fees for the item.</value>
        [DataMember(Name = "feeBreakdown", EmitDefaultValue = false)]
        public List<FeeLineItem> FeeBreakdown { get; set; }

        /// <summary>
        /// The total fees charged if the item participated in the Small and Light program.
        /// </summary>
        /// <value>The total fees charged if the item participated in the Small and Light program.</value>
        [DataMember(Name = "totalFees", EmitDefaultValue = false)]
        public MoneyType TotalFees { get; set; }

        /// <summary>
        /// One or more unexpected errors occurred during the getSmallAndLightFeePreview operation.
        /// </summary>
        /// <value>One or more unexpected errors occurred during the getSmallAndLightFeePreview operation.</value>
        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public ErrorList Errors { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FeePreview {\n");
            sb.Append("  Asin: ").Append(Asin).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  FeeBreakdown: ").Append(FeeBreakdown).Append("\n");
            sb.Append("  TotalFees: ").Append(TotalFees).Append("\n");
            sb.Append("  Errors: ").Append(Errors).Append("\n");
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
            return this.Equals(input as FeePreview);
        }

        /// <summary>
        /// Returns true if FeePreview instances are equal
        /// </summary>
        /// <param name="input">Instance of FeePreview to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FeePreview input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Asin == input.Asin ||
                    (this.Asin != null &&
                    this.Asin.Equals(input.Asin))
                ) &&
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
                ) &&
                (
                    this.FeeBreakdown == input.FeeBreakdown ||
                    this.FeeBreakdown != null &&
                    this.FeeBreakdown.SequenceEqual(input.FeeBreakdown)
                ) &&
                (
                    this.TotalFees == input.TotalFees ||
                    (this.TotalFees != null &&
                    this.TotalFees.Equals(input.TotalFees))
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
                if (this.Asin != null)
                    hashCode = hashCode * 59 + this.Asin.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                if (this.FeeBreakdown != null)
                    hashCode = hashCode * 59 + this.FeeBreakdown.GetHashCode();
                if (this.TotalFees != null)
                    hashCode = hashCode * 59 + this.TotalFees.GetHashCode();
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
