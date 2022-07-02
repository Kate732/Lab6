using System.Collections.Generic;

namespace Lab6
{
    public class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public Rectangle Value { get; }

        public Node(Rectangle value, bool vertical)
        {
            Value = value;
            if (value.RectPoints.Count > 100)
            {
                double medianLat;
                double medianLong;
                bool checkLeftSide = false;
                bool checkUpSide = false;
                
                if (vertical)
                {
                    medianLong = (value.LongMax + value.LongMin) / 2;
                    double newMaxLong = medianLong;
                    List<Point> leftSidedRectanglePoints = SelectLeftPointOfNewRectangle(newMaxLong);
                    LeftNode = new Node(new Rectangle(leftSidedRectanglePoints), false);

                    double newMinLong = medianLong;
                    List<Point> rightSidedRectanglePoints = SelectRightPointOfNewRectangle(newMinLong);
                    RightNode = new Node(new Rectangle(rightSidedRectanglePoints), false);
                }
                else
                {
                    medianLat = (value.LatMax + value.LatMin) / 2;
                    double newMinLat = medianLat;
                    List<Point> upSidedRectanglePoints = SelectUpPointOfNewRectangle(newMinLat);
                    LeftNode = new Node(new Rectangle(upSidedRectanglePoints), true);
                    double newMaxLat = medianLat;
                    List<Point> rightSidedRectanglePoints = SelectDownPointOfNewRectangle(newMaxLat);
                    RightNode = new Node(new Rectangle(rightSidedRectanglePoints), true);
                }
            }
        }

        public List<Point> SelectDownPointOfNewRectangle(double newMaxLat)
        {
            List<Point> newPoints = new List<Point>();
            foreach (var point in Value.RectPoints)
            {
                if (point.Latitude < newMaxLat)
                {
                    newPoints.Add(point);
                }
            }

            return newPoints;
        }

        public List<Point> SelectUpPointOfNewRectangle(double newMinLat)
        {
            List<Point> newPoints = new List<Point>();
            foreach (var point in Value.RectPoints)
            {
                if (point.Latitude > newMinLat)
                {
                    newPoints.Add(point);
                }
            }

            return newPoints;
        }

        private List<Point> SelectRightPointOfNewRectangle(double newMinLong)
        {
            List<Point> newPoints = new List<Point>();
            foreach (var point in Value.RectPoints)
            {
                if (point.Longitude > newMinLong)
                {
                    newPoints.Add(point);
                }
            }

            return newPoints;
        }

        public List<Point> SelectLeftPointOfNewRectangle(double newMaxLong)
        {
            List<Point> newPoints = new List<Point>();
            foreach (var point in Value.RectPoints)
            {
                if (point.Longitude < newMaxLong)
                {
                    newPoints.Add(point);
                }
            }

            return newPoints;
        }
    }
}