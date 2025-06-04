using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public interface ISearchGraph<T> : ISearchGraphSimple<T> {
        float Heuristic(T From, T To);
    }
}
