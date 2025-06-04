using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public interface ISearchGraphSimple<T> {
        List<SearchTreeAction<T>> GetActions(T State);
    }
}
