namespace Lezione_20241127 {

    public enum CarType {
        _3_door,
        _5_door
    }

    class Car {

        private const int minHorsePower = 10;

        private string brand;
        private string model;
        private int horsePower;
        private Color color;
        private CarType type;

        public Car (int horsePower, string brand, string model, Color color, CarType type) {
            SetBrand(brand);
            SetModel(model);
            SetColor(color);
            SetCarType(type);
            SetHorsePower(horsePower);
        }

        public float GetKW () {
            return horsePower * 0.74f;
        }

        public string GetBrand () {
            return brand;
        }

        public string GetModel () {
            return model;
        }

        public int GetHorsePower () {
            return horsePower;
        }

        public Color GetColor () {
            return color;
        }

        public CarType GetCarType () {
            return type;
        }

        public void SetBrand (string brand) {
            if (string.IsNullOrWhiteSpace(brand)) {
                if (string.IsNullOrEmpty(this.brand)) {
                    this.brand = "No Brand";
                }
                return;
            }
            this.brand = brand;
        }

        public void SetModel (string model) {
            if (string.IsNullOrWhiteSpace(model)) {
                if (string.IsNullOrWhiteSpace(this.model)) {
                    this.model = "No Model";
                }
                return;
            }
            this.model = model;
        }

        public void SetHorsePower (int horsePower) {
            this.horsePower = horsePower;
            if (this.horsePower < minHorsePower) {
                this.horsePower = minHorsePower;
            }
        }

        public void SetColor (Color color) {
            this.color = color;
        }

        public void SetCarType (CarType type) {
            if (this.type == CarType._5_door && type == CarType._3_door) return;
            this.type = type;
        }

    }
}
