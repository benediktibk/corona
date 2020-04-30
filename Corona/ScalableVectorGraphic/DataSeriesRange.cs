namespace ScalableVectorGraphic {
    public class DataSeriesRange {
        public DataSeriesRange(double minimumX, double maximumX, double minimumY, double maximumY) {
            MinimumX = minimumX;
            MaximumX = maximumX;
            MinimumY = minimumY;
            MaximumY = maximumY;
        }

        public double MinimumX { get; }
        public double MaximumX { get; }
        public double MinimumY { get; }
        public double MaximumY { get; }
    }
}
