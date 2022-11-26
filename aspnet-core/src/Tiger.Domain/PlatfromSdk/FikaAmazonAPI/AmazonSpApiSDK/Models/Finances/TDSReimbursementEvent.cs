/* 
 * Selling Partner API for Finances
 *
 * The Selling Partner API for Finances helps you obtain financial information relevant to a seller's business. You can obtain financial events for a given order, financial event group, or date range without having to wait until a statement period closes. You can also obtain financial event groups for a given date range.
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

namespace FikaAmazonAPI.AmazonSpApiSDK.Models.Finances
{
    /// <summary>
    /// A tax deduction at source (TDS) claim reimbursement event on the seller&#39;s account.
    /// </summary>
    [DataContract]
    public partial class TDSReimbursementEvent : IEquatable<TDSReimbursementEvent>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TDSReimbursementEvent" /> class.
        /// </summary>
        /// <param name="PostedDate">The date and time when the financial event was posted..</param>
        /// <param name="TdsOrderId">A tax deduction at source (TDS) claim identifier..</param>
        /// <param name="ReimbursedAmount">The amount of the reimbursement..</param>
        public TDSReimbursementEvent(DateTime? PostedDate = default(DateTime?), string TdsOrderId = default(string), Currency ReimbursedAmount = default(Currency))
        {
            this.PostedDate = PostedDate;
            this.TdsOrderId = TdsOrderId;
            this.ReimbursedAmount = ReimbursedAmount;
        }

        /// <summary>
        /// The date and time when the financial event was posted.
        /// </summary>
        /// <value>The date and time when the financial event was posted.</value>
        [DataMember(Name = "PostedDate", EmitDefaultValue = false)]
        public DateTime? PostedDate { get; set; }

        /// <summary>
        /// A tax deduction at source (TDS) claim identifier.
        /// </summary>
        /// <value>A tax deduction at source (TDS) claim identifier.</value>
        [DataMember(Name = "TdsOrderId", EmitDefaultValue = false)]
        public string TdsOrderId { get; set; }

        /// <summary>
        /// The amount of the reimbursement.
        /// </summary>
        /// <value>The amount of the reimbursement.</value>
        [DataMember(Name = "ReimbursedAmount", EmitDefaultValue = false)]
        public Currency ReimbursedAmount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TDSReimbursementEvent {\n");
            sb.Append("  PostedDate: ").Append(PostedDate).Append("\n");
            sb.Append("  TdsOrderId: ").Append(TdsOrderId).Append("\n");
            sb.Append("  ReimbursedAmount: ").Append(ReimbursedAmount).Append("\n");
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
            return this.Equals(input as TDSReimbursementEvent);
        }

        /// <summary>
        /// Returns true if TDSReimbursementEvent instances are equal
        /// </summary>
        /// <param name="input">Instance of TDSReimbursementEvent to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TDSReimbursementEvent input)
        {
            if (input == null)
                return false;

            return
                (
                    this.PostedDate == input.PostedDate ||
                    (this.PostedDate != null &&
                    this.PostedDate.Equals(input.PostedDate))
                ) &&
                (
                    this.TdsOrderId == input.TdsOrderId ||
                    (this.TdsOrderId != null &&
                    this.TdsOrderId.Equals(input.TdsOrderId))
                ) &&
                (
                    this.ReimbursedAmount == input.ReimbursedAmount ||
                    (this.ReimbursedAmount != null &&
                    this.ReimbursedAmount.Equals(input.ReimbursedAmount))
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
                if (this.PostedDate != null)
                    hashCode = hashCode * 59 + this.PostedDate.GetHashCode();
                if (this.TdsOrderId != null)
                    hashCode = hashCode * 59 + this.TdsOrderId.GetHashCode();
                if (this.ReimbursedAmount != null)
                    hashCode = hashCode * 59 + this.ReimbursedAmount.GetHashCode();
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
