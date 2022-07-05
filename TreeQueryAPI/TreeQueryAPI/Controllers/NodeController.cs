using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreeQueryAPI.Helpers;
using TreeQueryAPI.Models;

namespace TreeQueryAPI.Controllers
{
    /// <summary>
    /// Manages nodes.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly ManageNodes _nodeManager;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NodeController()
        {
            _nodeManager = new ManageNodes();
        }

        /// <summary>
        /// Gets all the nodes
        /// </summary>
        /// <returns>A collection of Nodes.</returns>
        public async Task<ActionResult<IEnumerable<Node>>> Get()
        {
            var nodes = await _nodeManager.GetAllNodesAsync();
            return Ok(nodes);
        }

        /// <summary>
        /// Gets a node by the unique identity.
        /// </summary>
        /// <param name="id">Unique identity of a node.</param>
        /// <returns>A Node.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Node>> Get(string id)
        {
            var node = await _nodeManager.GetNodeByIdAsync(id);
            return Ok(node);
        }

        /// <summary>
        /// Gets a node by name.
        /// </summary>
        /// <param name="name">Name of the node.</param>
        /// <returns>A Node.</returns>
        [HttpGet("{name}")]
        [ActionName("GetByName")]
        public async Task<ActionResult<Node>> GetByName(string name)
        {
            var node = await _nodeManager.GetNodeByNameAsync(name);
            return Ok(node);
        }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="node">A Node.</param>
        [HttpPost]
        public async void Post([FromBody] Node node)
        {
            await _nodeManager.AddNodeAsync(node);
        }

        /// <summary>
        /// Updates an existing node.
        /// </summary>
        /// <param name="id">Unique identity of a node.</param>
        /// <param name="updatedNode">The data to update the node with.</param>
        /// <returns>A Node.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Node>> Put(string id, [FromBody] Node updatedNode)
        {
            var node = await _nodeManager.UpdateNodeByIdAsync(id, updatedNode);
            return Ok(node);
        }

        /// <summary>
        /// Deletes a node.
        /// </summary>
        /// <param name="id">Unique identity of a node.</param>
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            await _nodeManager.DeleteNodeByIdAsync(id);
        }

    }
}