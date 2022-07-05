using Newtonsoft.Json;
using System.Collections.Generic;

namespace TreeQueryAPI.Models
{
    /// <summary>
    /// Node
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Unique Identity.
        /// </summary>
        [JsonProperty]
        public string Id { get; set; }
        
        /// <summary>
        /// Name of the node.
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }
        
        /// <summary>
        /// Parent Identity.
        /// </summary>
        [JsonProperty]
        public string ParentId { get; set; }
        
        /// <summary>
        /// List of children node identities.
        /// </summary>
        [JsonProperty]
        public List<string> Children { get; set; } = new List<string>();

    }
}
