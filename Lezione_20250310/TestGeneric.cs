namespace Lezione_20250310 {
    public class TestGeneric<T> {

        T value;

        public void SetValue (T value) {
            this.value = value;
        }

        public T GetValue () {
            return value;
        }

        class NestedClass {
            T interlaValue;
        }


    }
}
