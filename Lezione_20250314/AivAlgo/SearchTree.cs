using System;
using System.Collections.Generic;
using Aiv.Collections.Generic;

namespace Aiv
{
    public struct SearchTreeAction<T>
    {
        public T NewState;
        public float Cost;
    }

    public interface ISearchGraphSimple<T>
    {
        List<SearchTreeAction<T>> GetActions(T State);
    }

    public interface ISearchGraph<T> : ISearchGraphSimple<T>
    {
        float Heuristic(T From, T To);
    }

    public static class SearchTree
    {
        public class SearchNode<T> : IComparable<SearchNode<T>>
        {
            public SearchNode<T> Parent;

            public T State;
            public float Cost = 0;
            public float Heuritic = 0;

            public float Priority { get { return Cost + Heuritic; } }

            public SearchNode(T state, SearchNode<T> parent)
            {
                State = state;
                Parent = parent;
            }

            public SearchNode(T state, SearchNode<T> parent, float cost, float heuristic)
            {
                State = state;
                Parent = parent;
                Cost = cost;
                Heuritic = heuristic;
            }

            public int CompareTo(SearchNode<T> other)
            {
                return Math.Sign(Priority - other.Priority);
            }
        }

        public struct Result<T>
        {
            public SearchNode<T> Solution;
            public List<T> Steps;
            public int Iterations;
        }

        private static Result<T> MakeResult<T>(SearchNode<T> solution, int iterations = 0)
        {
            Result<T> result = new Result<T>();

            result.Steps = new List<T>();
            result.Solution = solution;
            result.Iterations = iterations;

            while (solution != null)
            {
                result.Steps.Add(solution.State);
                solution = solution.Parent;
            }

            result.Steps.Reverse();

            return result;
        }

        public static Result<T> BreadthFirstSearch<G, T>(G graph, T from, T to) where G : ISearchGraphSimple<T> where T : IEquatable<T>
        {
            Queue<SearchNode<T>> openList = new Queue<SearchNode<T>>();
            HashSet<T> closedList = new HashSet<T>();

            int iterations = 0;
            openList.Enqueue(new SearchNode<T>(from, null));
            while (openList.Count > 0)
            {
                var current = openList.Dequeue();
                ++iterations;

                if (current.State.Equals(to)) return MakeResult(current, iterations);

                closedList.Add(current.State);

                foreach (var action in graph.GetActions(current.State))
                {
                    if (closedList.Contains(action.NewState)) continue;
                    openList.Enqueue(new SearchNode<T>(action.NewState, current));
                }
            }
            return MakeResult<T>(null);
        }

        public static Result<T> AStarSearch<T>(ISearchGraph<T> graph, T from, T to) where T : IEquatable<T>
        {
            Result<T> result = new Result<T>();

            PriorityQueue<SearchNode<T>> openList = new PriorityQueue<SearchNode<T>>();
            HashSet<T> closedList = new HashSet<T>();

            int iterations = 0;
            openList.Enqueue(new SearchNode<T>(from, null));
            while (openList.Count > 0)
            {
                var current = openList.Dequeue();
                ++iterations;

                if (closedList.Contains(current.State)) continue;
                closedList.Add(current.State);

                if (current.State.Equals(to)) return MakeResult(current, iterations);

                foreach (var action in graph.GetActions(current.State))
                {
                    openList.Enqueue(new SearchNode<T>(action.NewState, current, current.Cost + action.Cost, graph.Heuristic(action.NewState, to)));
                }
            }

            return result;
        }
    }
}
