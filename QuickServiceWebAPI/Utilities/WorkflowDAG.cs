using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Utilities
{
    public class WorkflowDAG
    {
        private readonly Dictionary<string, List<string>> _graph;

        public WorkflowDAG(List<WorkflowTransition> transitions)
        {
            _graph = new Dictionary<string, List<string>>();

            foreach (var transition in transitions)
            {
                if (!_graph.ContainsKey(transition.FromWorkflowTask))
                {
                    _graph[transition.FromWorkflowTask] = new List<string>();
                }

                _graph[transition.FromWorkflowTask].Add(transition.ToWorkflowTask);
            }
        }

        public List<string> GetPreviousNodes(string node)
        {
            var previousNodes = new List<string>();
            var visited = new HashSet<string>();
            var stack = new Stack<string>();

            stack.Push(node);
            visited.Add(node);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                foreach (var parent in _graph.Keys.Where(key => _graph[key].Contains(current)))
                {
                    if (!visited.Contains(parent))
                    {
                        stack.Push(parent);
                        visited.Add(parent);
                        previousNodes.Add(parent);
                    }
                }
            }

            return previousNodes;
        }

        public int GetSize()
        {
            return _graph.Count;
        }

        public List<string> GetSources()
        {
            var sources = new List<string>();
            var allNodes = _graph.Keys;

            foreach (var node in allNodes)
            {
                if (!_graph.Any(entry => entry.Value.Contains(node)))
                {
                    sources.Add(node);
                }
            }

            return sources;
        }
        public List<string> GetAdjacentNodes(string node)
        {
            if (_graph.ContainsKey(node))
            {
                return _graph[node];
            }

            return new List<string>();
        }

        public List<string> GetNextNodes(string node)
        {
            var nextNodes = new List<string>();
            var visited = new HashSet<string>();
            var stack = new Stack<string>();

            stack.Push(node);
            visited.Add(node);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                foreach (var child in _graph.GetValueOrDefault(current, new List<string>()))
                {
                    if (!visited.Contains(child))
                    {
                        stack.Push(child);
                        visited.Add(child);
                        nextNodes.Add(child);
                    }
                }
            }

            return nextNodes;
        }

        public bool HasNode(string node)
        {
            return _graph.ContainsKey(node);
        }
    }
}
