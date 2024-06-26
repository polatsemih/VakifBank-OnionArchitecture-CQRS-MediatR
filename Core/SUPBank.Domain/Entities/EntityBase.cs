﻿namespace SUPBank.Domain.Entities
{
    public class EntityBase
    {
        /// <summary>
        /// Unique Identifier number for each all entities
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Indicates whether the entity is active/visible
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The date and time when the entity was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The date and time when the entity was last modified
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }
    }
}
