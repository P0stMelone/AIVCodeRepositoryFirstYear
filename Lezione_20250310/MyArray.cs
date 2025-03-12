namespace Lezione_20250310 {
    public class MyArray<T> {

        T[] array;

        public MyArray(int size) {
            array = new T[size];
        }

        public T this[int i ] {
            get { return array[i]; }
            set { array[i] = value; }
        }

    }
}
