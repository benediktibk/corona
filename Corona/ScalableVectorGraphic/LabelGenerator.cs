namespace ScalableVectorGraphic {
    public class LabelGenerator<T> : ILabelGenerator<T> {
        public string CreateLabel(T value) {
            return value.ToString();
        }
    }
}
