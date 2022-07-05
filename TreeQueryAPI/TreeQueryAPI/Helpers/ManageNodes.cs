using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeQueryAPI.Models;

namespace TreeQueryAPI.Helpers
{
    /// <summary>
    /// Manages node data.
    /// </summary>
    public class ManageNodes
    {
        private readonly DataManager _dataManager;
        private static List<Node> _nodes;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManageNodes()
        {
            _dataManager = new DataManager();
            _nodes = _dataManager.Load();
        }

        /// <summary>
        /// Gets a list of all the nodes.
        /// </summary>
        /// <returns></returns>
        public Task<List<Node>> GetAllNodesAsync()
        {
            return Task.FromResult(_nodes);
        }

        /// <summary>
        /// Gets a node by identity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Node> GetNodeByIdAsync(string id)
        {
            var node = _nodes.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(node);
        }

        /// <summary>
        /// Gets a node by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Node> GetNodeByNameAsync(string name)
        {
            var node = _nodes.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return Task.FromResult(node);
        }

        /// <summary>
        /// Adds a node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Task AddNodeAsync(Node node)
        {
            _nodes.Add(node);
            if (!string.IsNullOrEmpty(node.ParentId))
            {
                var parent = _nodes.FirstOrDefault(x => x.Id == node.ParentId);
                parent?.Children.Add(node.Id);
            }
            _dataManager.Save(_nodes);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates a node.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedNode"></param>
        /// <returns></returns>
        public Task<Node> UpdateNodeByIdAsync(string id, Node updatedNode)
        {
            var node = _nodes.FirstOrDefault(x => x.Id == id);
            if (node == null) return Task.FromResult((Node) null);

            node.Name = updatedNode.Name;

            if (node.ParentId != updatedNode.ParentId)
            {
                if (!string.IsNullOrEmpty(node.ParentId)) // updated the parent's children list.
                {
                    var parentNode = _nodes.FirstOrDefault(x => x.Id == node.ParentId);
                    parentNode?.Children.Remove(node.Id);
                }

                if (!string.IsNullOrEmpty(updatedNode.ParentId))
                {
                    var parentNode = _nodes.FirstOrDefault(x => x.Id == updatedNode.ParentId);
                    parentNode?.Children.Add(updatedNode.Id);
                }

                node.ParentId = updatedNode.ParentId;
            }
            

            _dataManager.Save(_nodes);
            return Task.FromResult(node);
        }

        /// <summary>
        /// Deletes a node.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteNodeByIdAsync(string id)
        {
            var nodeToDelete = _nodes.FirstOrDefault(x => x.Id == id);
            if (nodeToDelete == null) return Task.CompletedTask;

            if (!string.IsNullOrEmpty(nodeToDelete.ParentId)) // updated the parent's children list.
            {
                var parentNode = _nodes.FirstOrDefault(x => x.Id == nodeToDelete.ParentId);
                parentNode?.Children.Remove(nodeToDelete.Id);
            }

            if (nodeToDelete.Children.Count > 0)
            {
                Delete(nodeToDelete.Id);
            }
            else // has no children - so just delete it.
            {
                _nodes.Remove(nodeToDelete);
            }
            _dataManager.Save(_nodes);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Recursive delete.
        /// </summary>
        /// <param name="id"></param>
        private void Delete(string id)
        {
            var children = GetChildren(id);

            foreach (var child in children)
            {
                Delete(child.Id);
            }

            var nodeToDelete = _nodes.FirstOrDefault(x => x.Id == id);
            if(nodeToDelete!=null) _nodes.Remove(nodeToDelete);
        }

        /// <summary>
        /// Gets a list of children for the given node identity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IEnumerable<Node> GetChildren(string id)
        {
            var node = _nodes.FirstOrDefault(x => x.Id == id);
            var children = new List<Node>();
            if (node == null) return children;
            
            foreach (var child in node.Children)
            {
                children.Add(_nodes.FirstOrDefault(x => x.Id == child));
            }
            return children;
        }
    }
}
