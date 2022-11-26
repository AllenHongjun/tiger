/* 
 * Selling Partner API for Listings Items
 *
 * The Selling Partner API for Listings Items (Listings Items API) provides programmatic access to selling partner listings on Amazon. Use this API in collaboration with the Selling Partner API for Product Type Definitions, which you use to retrieve the information about Amazon product types needed to use the Listings Items API.  For more information, see the [Listings Items API Use Case Guide](doc:listings-items-api-v2021-08-01-use-case-guide).
 *
 * OpenAPI spec version: 2021-08-01
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace FikaAmazonAPI.AmazonSpApiSDK.Models.ListingsItems
{
    /// <summary>
    /// Response containing the results of a submission to the Selling Partner API for Listings Items.
    /// </summary>
    [DataContract]
    public partial class ListingsItemSubmissionResponse :  IEquatable<ListingsItemSubmissionResponse>, IValidatableObject
    {
        /// <summary>
        /// The status of the listings item submission.
        /// </summary>
        /// <value>The status of the listings item submission.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum ACCEPTED for value: ACCEPTED
            /// </summary>
            [EnumMember(Value = "ACCEPTED")]
            ACCEPTED = 1,
            
            /// <summary>
            /// Enum INVALID for value: INVALID
            /// </summary>
            [EnumMember(Value = "INVALID")]
            INVALID = 2
        }

        /// <summary>
        /// The status of the listings item submission.
        /// </summary>
        /// <value>The status of the listings item submission.</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListingsItemSubmissionResponse" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public ListingsItemSubmissionResponse() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListingsItemSubmissionResponse" /> class.
        /// </summary>
        /// <param name="sku">A selling partner provided identifier for an Amazon listing. (required).</param>
        /// <param name="status">The status of the listings item submission. (required).</param>
        /// <param name="submissionId">The unique identifier of the listings item submission. (required).</param>
        /// <param name="issues">Listings item issues related to the listings item submission..</param>
        public ListingsItemSubmissionResponse(string sku = default(string), StatusEnum status = default(StatusEnum), string submissionId = default(string), List<Issue> issues = default(List<Issue>))
        {
            // to ensure "sku" is required (not null)
            if (sku == null)
            {
                throw new InvalidDataException("sku is a required property for ListingsItemSubmissionResponse and cannot be null");
            }
            else
            {
                this.Sku = sku;
            }
            // to ensure "status" is required (not null)
            if (status == null)
            {
                throw new InvalidDataException("status is a required property for ListingsItemSubmissionResponse and cannot be null");
            }
            else
            {
                this.Status = status;
            }
            // to ensure "submissionId" is required (not null)
            if (submissionId == null)
            {
                throw new InvalidDataException("submissionId is a required property for ListingsItemSubmissionResponse and cannot be null");
            }
            else
            {
                this.SubmissionId = submissionId;
            }
            this.Issues = issues;
        }
        
        /// <summary>
        /// A selling partner provided identifier for an Amazon listing.
        /// </summary>
        /// <value>A selling partner provided identifier for an Amazon listing.</value>
        [DataMember(Name="sku", EmitDefaultValue=false)]
        public string Sku { get; set; }


        /// <summary>
        /// The unique identifier of the listings item submission.
        /// </summary>
        /// <value>The unique identifier of the listings item submission.</value>
        [DataMember(Name="submissionId", EmitDefaultValue=false)]
        public string SubmissionId { get; set; }

        /// <summary>
        /// Listings item issues related to the listings item submission.
        /// </summary>
        /// <value>Listings item issues related to the listings item submission.</value>
        [DataMember(Name="issues", EmitDefaultValue=false)]
        public List<Issue> Issues { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ListingsItemSubmissionResponse {\n");
            sb.Append("  Sku: ").Append(Sku).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  SubmissionId: ").Append(SubmissionId).Append("\n");
            sb.Append("  Issues: ").Append(Issues).Append("\n");
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
            return this.Equals(input as ListingsItemSubmissionResponse);
        }

        /// <summary>
        /// Returns true if ListingsItemSubmissionResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of ListingsItemSubmissionResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ListingsItemSubmissionResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Sku == input.Sku ||
                    (this.Sku != null &&
                    this.Sku.Equals(input.Sku))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.SubmissionId == input.SubmissionId ||
                    (this.SubmissionId != null &&
                    this.SubmissionId.Equals(input.SubmissionId))
                ) && 
                (
                    this.Issues == input.Issues ||
                    this.Issues != null &&
                    this.Issues.SequenceEqual(input.Issues)
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
                if (this.Sku != null)
                    hashCode = hashCode * 59 + this.Sku.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.SubmissionId != null)
                    hashCode = hashCode * 59 + this.SubmissionId.GetHashCode();
                if (this.Issues != null)
                    hashCode = hashCode * 59 + this.Issues.GetHashCode();
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
