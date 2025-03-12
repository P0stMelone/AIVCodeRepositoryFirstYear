using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250310 {
    public class CircularArray<T> {

        T[] array;

        public int Length {
            get { return array.Length; }
        }

        public CircularArray(int size) {
            array = new T[size];
        }

        public T this[int i] {
            get { return array[AdjustIndex(i)]; }
            set { array[AdjustIndex(i)] = value; }
        }

        private int AdjustIndex (int i) {
            return i % array.Length;
        }

    }
}
