using System.Collections.Generic;

namespace Lab6
{
    public class Rectangle
    {
        // Max Lat: 56,17019, max long: 40,72657, min lat: 42,70549 min long: 22,11892
        public Rectangle(List<Point> rectanglePoints)
        {
            LatMax = -90;
            LatMin = 90;
            LatMin = -180;
            LatMax = 180;
            foreach (var point in rectanglePoints)
            {
                LatMax = point.Latitude > LatMax ? point.Latitude : LatMax;

                LatMin = point.Latitude < LatMin ? point.Latitude : LatMin;

                if (point.Longitude > LongMax)
                {
                    LongMax = point.Longitude;
                }

                if (point.Longitude < LongMin)
                {
                    LongMin = point.Longitude;
                }
            }

            RectPoints = rectanglePoints;
        }
        public float LatMax { get; set; }
        public float LatMin { get; set; }
        public float LongMax { get; set; }
        public float LongMin { get; set; }
        public List<Point> RectPoints { get; set; }

        private List<Point> SetRectPoints(float latMax, float latMin, float longMax, float longMin)
        {
            List<Point> RectPoints = new List<Point>();
            
            return RectPoints;
        }
    }
}   