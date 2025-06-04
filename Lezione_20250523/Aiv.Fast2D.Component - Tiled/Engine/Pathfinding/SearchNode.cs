using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public class SearchNode<T> : IComparable<SearchNode<T>> {
        public SearchNode<T> Parent;

        public T State;
        public float Cost = 0;
        public float Heuritic = 0;

        public float Priority { get { return Cost + Heuritic; } }

        public SearchNode(T state, SearchNode<T> parent) {
            State = state;
            Parent = parent;
        }

        public SearchNode(T state, SearchNode<T> parent, float cost, float heuristic) {
            State = state;
            Parent = parent;
            Cost = cost;
            Heuritic = heuristic;
        }

        public int CompareTo(SearchNode<T> other) {
            return Math.Sign(Priority - other.Priority);
        }
    }
}
