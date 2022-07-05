using TreeQueryAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TreeQueryAPI.Helpers
{
    /// <summary>
    /// Responsible for loading and saving the nodes json.
    /// </summary>
    public class DataManager
    {
        private const string FileName = "Data\\nodes.json";

        /// <summary>
        /// Saves a list of nodes to a json file.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="fileName"></param>
        public void Save(List<Node> nodes, string fileName = FileName)
        {
            var nodePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            var nodeJson = JsonSerializer.Serialize(nodes);
            File.WriteAllText(nodePath, nodeJson);
        }

        /// <summary>
        /// Loads a list of nodes from a json file.
        /// </summary>
        /// <returns></returns>
        public List<Node> Load()
        {
            var nodePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
            if (File.Exists(nodePath))
            {
                var nodeJson = File.ReadAllText(nodePath);
                var nodes = JsonSerializer.Deserialize<List<Node>>(nodeJson);
                return nodes;
            }
            else
            {
                Console.WriteLine("Node JSON file not found.");
                Console.ReadLine();
            }
            return null;
        }
    }
}
