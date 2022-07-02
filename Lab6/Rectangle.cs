using System.Collections.Generic;

namespace Lab6
{
    public class Rectangle
    {
        public Rectangle(List<Point> rectanglePoints)
        {
            LatMax = -90;
            LatMin = 90;
            LongMax = -180;
            LongMin = 180;
            foreach (var point in rectanglePoints)
            {
                LatMax = point.Latitude > LatMax ? point.Latitude : LatMax;

                LatMin = point.Latitude < LatMin ? point.Latitude : LatMin;

                LongMax = point.Longitude > LongMax ? point.Longitude : LongMax;

                LongMin = point.Longitude < LongMin ? point.Longitude : LongMin;
            }

            RectPoints = rectanglePoints;
        }
        public double LatMax { get; set; }
        public double LatMin { get; set; }
        public double LongMax { get; set; }
        public double LongMin { get; set; }
        public List<Point> RectPoints { get; set; }
    }
}   