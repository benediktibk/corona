namespace ScalableVectorGraphic {
    public interface ILabelGenerator<T> {
        string CreateLabel(T value);
    }
}