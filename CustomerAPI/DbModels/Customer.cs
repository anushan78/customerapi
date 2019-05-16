using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.DbModels
{
    /// <summary>
    /// Defines customer object use to persist customer data.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer id. Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name. Required field
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name. Required field
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Date of Birth. Required field
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}