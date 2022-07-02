using System.Collections.Generic;

namespace Lab6
{
    // float latitude, float longtitude, float radius
    public class K_d_tree
    {
        public Node Root { get; set; }
        
        public K_d_tree(List<Point> allPoints)
        {
            Rectangle rootRectangle = new Rectangle(allPoints);
            Root = new Node(rootRectangle, true);
        }

        public List<Point> FindInRectangle(Rectangle areaOfLooking, Node currentNode)
        {
            if (!IsCrossing(currentNode.Value, areaOfLooking))
            {
                return new List<Point>();
            }

            List<Point> pointsInArea = new List<Point>();
            if (currentNode.LeftNode == null)
            {
                pointsInArea.AddRange(FindPointsInArea(currentNode.Value, areaOfLooking));
            }

            else
            {
                pointsInArea.AddRange(FindInRectangle(areaOfLooking, currentNode.LeftNode));
                pointsInArea.AddRange(FindInRectangle(areaOfLooking, currentNode.RightNode));
            }

            return pointsInArea;
        }

        /*private List<Point> PointsInCircle(Rectangle currentNodeValue, Rectangle areaOfLooking)
        {
            List<Point> appropriatePoints = new List<Point>();
            float latitudeCentre = (areaOfLooking.LatMax + areaOfLooking.LatMin) / 2;
            float longitudeCentre = (areaOfLooking.LongMax + areaOfLooking.LongMin) / 2;
            float radius = (areaOfLooking.LatMax - areaOfLooking.LatMin) / 2;
            foreach (var point in currentNodeValue.RectPoints)
            {
                if (Linear_mode.CountingDistance(latitudeCentre, point.Latitude, 
                        longitudeCentre, point.Longitude) < radius)
                {
                    appropriatePoints.Add(point);
                }
            }

            return appropriatePoints;
        }*/

        private List<Point> FindPointsInArea(Rectangle currentNodeValue, Rectangle areaOfLooking)
        {
            List<Point> pointsInArea = new List<Point>();
            foreach (var point in currentNodeValue.RectPoints)
            {
                if (point.Latitude > areaOfLooking.LatMin && point.Latitude < areaOfLooking.LatMax
                    && point.Longitude > areaOfLooking.LongMin && point.Longitude < areaOfLooking.LongMax)
                {
                    pointsInArea.Add(point);
                }
            }

            return pointsInArea;
        }

        public bool IsCrossing(Rectangle rectangle1, Rectangle rectangle2)
        {
            List<Point> cornersRectangle1 = CornersOfRectangle(rectangle1);
            List<Point> cornersRectangle2 = CornersOfRectangle(rectangle2);
            int crosses = 0;
            foreach (var corner in cornersRectangle1)
            {
                bool isPointInsideRectangle = PointIn(rectangle2, corner);
                if (isPointInsideRectangle)
                {
                    crosses += 1;
                }
            }
            
            if (crosses > 0)
            {
                return true;
            }
            
            foreach (var corner in cornersRectangle2)
            {
                bool isPointInsideRectangle = PointIn(rectangle1, corner);
                if (isPointInsideRectangle)
                {
                    crosses += 1;
                }
            }

            if (crosses > 0)
            {
                return true;
            }
            
            return false;
        }

        private bool PointIn(Rectangle rectangle, Point corner)
        {
            if (corner.Latitude >= rectangle.LatMin && corner.Latitude <= rectangle.LatMax 
                && corner.Longitude >= rectangle.LongMin && corner.Longitude <= rectangle.LongMax)
            {
                return true;
            }
            return false;
        }

        private List<Point> CornersOfRectangle(Rectangle rectangle1)
        {
            List<Point> corners = new List<Point>();
            Point leftUpPoint = new Point(rectangle1.LatMax, rectangle1.LongMin);
            Point leftDownPoint = new Point(rectangle1.LatMin, rectangle1.LongMin);
            Point rightUpPoint = new Point(rectangle1.LatMax, rectangle1.LongMax);
            Point rightDownPoint = new Point(rectangle1.LatMin, rectangle1.LongMax);
            corners.Add(leftUpPoint);
            corners.Add(rightUpPoint);
            corners.Add(rightDownPoint);
            corners.Add(leftDownPoint);
            return corners;
        }
        
    }
}
