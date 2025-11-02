using System;

namespace Aiv.Fast2D.Component {

    public enum ComparerType {
        Less,
        LessOrEqual,
        Equal,
        Greater,
        GreaterOrEqual
    }

    public static class ComparerUtility {

        public static bool Compare<T1,T2>  (T1 a, T2 b, ComparerType comparerType) where T1 : IComparable<T2> {
            switch (comparerType) {
                case ComparerType.LessOrEqual:
                    return a.CompareTo(b) <= 0;
                case ComparerType.Less:
                    return a.CompareTo(b) < 0;
                case ComparerType.Equal:
                    return a.CompareTo(b) == 0;
                case ComparerType.GreaterOrEqual:
                    return a.CompareTo(b) >= 0;
                case ComparerType.Greater:
                    return a.CompareTo(b) > 0;
            }
            return false;
        }

    }
}
