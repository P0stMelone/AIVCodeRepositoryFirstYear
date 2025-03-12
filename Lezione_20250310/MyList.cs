using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_20250310 {
    public class MyList<T> : IEnumerable<T> where T : IEquatable<T>, IComparable<T> {

        private const int incrementFactor = 2;

        private int currentIndex;

        T[] array;

        #region Properties
        public int Capacity {
            get { return array.Length; }
        }
        public int Count {
            get { return currentIndex; }
        }
        public T this[int index] {
            get {
                if (index < 0  || index > Count) throw new IndexOutOfRangeException();
                return array[index];
            }
            set {
                if (index < 0 || index > Count) throw new IndexOutOfRangeException();
                array[index] = value;
            }
        }
        #endregion

        #region Constructor
        public MyList() {
            array = new T[incrementFactor];
        }
        public MyList(int capacity) {
            array = new T[capacity];
        }
        #endregion

        public bool Contains (T element) {
            for (int i = 0; i < Count; i++) {
                if (array[i].Equals(element)) return true;
            }
            return false;
        }
        public int IndexOf (T element) {
            for (int i = 0; i < Count; i++) {
                if (array[i].Equals(element)) return i;
            }
            return -1;
        }

        public void Add(T element) {
            if (currentIndex >= array.Length) {
                InternalIncrementArray();
            }
            array[currentIndex] = element;
            currentIndex++;
        }

        public void Insert (int index, T element) {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            if (currentIndex >= array.Length) {
                InternalIncrementArray();
            }
            for (int i = Count; i > index; i--) {
                array[i] = array[i - 1];
            }
            array[index] = element;
        }

        public void Remove (T element) {
            int index = IndexOf(element);
            if (index < 0) return;
            RemoveAt(index);
        }

        public void RemoveAt (int index) {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            for (int i = index; i < Count -1; i++) {
                array[i] = array[i + 1];
            }
            currentIndex--;
            if (currentIndex < array.Length / (incrementFactor * incrementFactor)) {
                DecreaseArraySize();
            }
        }

        public void RemoveRange (int index, int count) {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            if (index + count >= Count) throw new IndexOutOfRangeException();
            for (int i = 0; i < count; i++) {
                RemoveAt(index);
            }
        }

        public void Reverse () {
            Reverse(0, Count);
        }

        public void Reverse (int index, int count) {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            if (index + count > Count) throw new IndexOutOfRangeException();
            T tempElement;
            int lastIndex = index + (count/2);
            int elementCounter = (2*index) + count;
            for (int i = index; i < lastIndex; i++) {
                tempElement = array[elementCounter - 1 - i];
                array[elementCounter - 1 -i] = array[i];
                array[i] = tempElement;
            }
        }

        public void Clear () {
            currentIndex = 0; //non alloco e dealloco memoria
        }

        #region InternalModifyArraySize
        private void InternalIncrementArray () {
            T[] newArray = new T[array.Length * incrementFactor];
            for (int i = 0; i < array.Length; i++) {
                newArray[i] = array[i];
            }
            array = newArray;
        }

        private void DecreaseArraySize () {
            T[] newArray = new T[array.Length / incrementFactor];
            for (int i = 0; i < newArray.Length; i++) {
                newArray[i] = array[i];
            }
            array = newArray;
        }
        #endregion

        public IEnumerator<T> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
