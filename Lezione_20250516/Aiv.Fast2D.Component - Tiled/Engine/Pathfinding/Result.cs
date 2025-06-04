using System;
using System.Collections.Generic;


namespace Aiv.Fast2D.Component {
    public struct Result<T> {
        public SearchNode<T> Solution;
        public List<T> Steps;
        public int Iterations;
    }
}
