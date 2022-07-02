using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;


namespace Lab6
{
    class Program
    {
        // map_search.exe --db=ukraine_poi.csv --lat=30.212, --long=35.872 --size=20
        static void Main(string[] args)
        {
            double latitude = 0;
            double longitude = 0;
            double radius = 0;
            foreach (var arg in args)
            {
                string[] splitArg = arg.Split('=');
                if (splitArg[0] == "--lat")
                {
                    bool res = double.TryParse(splitArg[1], NumberStyles.Number, CultureInfo.InvariantCulture,
                        out latitude);
                    if (!res)
                    {
                        throw new Exception("Wrong format");
                    }
                }
                if (splitArg[0] == "--long")
                {
                    bool res = double.TryParse(splitArg[1], NumberStyles.Number, CultureInfo.InvariantCulture, out longitude);
                    if (!res)
                    {
                        throw new Exception("Wrong format");
                    }
                }

                if (splitArg[0] == "--size")
                {
                    bool res = double.TryParse(splitArg[1], NumberStyles.Number, CultureInfo.InvariantCulture, out radius);
                    if (!res)
                    {
                        throw new Exception("Wrong format");
                    }
                }
            }
            
            List<Point> appropriatePoints = Linear_mode.FindingPointsLinearly(latitude, longitude, radius);
            // Elapsed time Linear mode: 00:00:01.0072136

            int index = 0;
            Console.WriteLine("Linear mode:");
            foreach (var point in appropriatePoints)
            {
                if (index > 20)
                {
                    Console.WriteLine($"All amount of points: {appropriatePoints.Count}");
                    break;
                }
                Console.WriteLine($"{point.Latitude},{point.Longitude}");
                index += 1;
            }

            
            Console.WriteLine("K-d tree:");
            
            string[] contents = System.IO.File.ReadAllLines("ukraine_poi.csv");
            List<Point> listOfAllPoints = new List<Point>();
            foreach (var line in contents)
            {
                string[] lineSeparated = line.Split(';');
                if (lineSeparated.Length < 3)
                {
                    continue;
                }
                double latitudeFromFile = double.Parse(lineSeparated[0]);
                double longitudeFromFile = double.Parse(lineSeparated[1]);
                Point point = new Point(latitudeFromFile, longitudeFromFile);
                listOfAllPoints.Add(point);
            }
            
            
            K_d_tree kDTree = new K_d_tree(listOfAllPoints);
            // Elapsed time building k-d tree: 00:00:01.5582248
            
            Point centre = new Point(latitude, longitude);
            double deltaLatitude = CalculateDeltaLatitude(centre, radius);
            double deltaLongitude = CalculateDeltaLongitude(centre, radius);

            List<Point> listPoints = new List<Point>();
            listPoints.Add(new Point(latitude + deltaLatitude, longitude - deltaLongitude));
            listPoints.Add(new Point(latitude - deltaLatitude, longitude + deltaLongitude));
            Rectangle areaOfLooking = new Rectangle(listPoints);
            
            List<Point> pointsFromRectangle = kDTree.FindInRectangle(areaOfLooking, kDTree.Root);
            List<Point> pointsFromCircle = Linear_mode.FindPointsFromCircle(pointsFromRectangle, radius, centre);
            // Elapsed time Find points in k-d tree: 00:00:00.0035865

            int i = 0;
            foreach (var point in pointsFromCircle)
            {
                if (i > 20)
                {
                    Console.WriteLine($"All amount of points: {pointsFromCircle.Count}");
                    break;
                }
                Console.WriteLine($"{point.Latitude},{point.Longitude}");
                i += 1;
            }
        }

        private static double CalculateDeltaLatitude(Point centre, double radius)
        {
            return radius/Linear_mode.CountingDistance(centre.Latitude, centre.Longitude, 
                centre.Latitude + 1, centre.Longitude);
        }
        private static double CalculateDeltaLongitude(Point centre, double radius)
        {
            double distOfOneDegree = Linear_mode.CountingDistance(centre.Latitude, centre.Longitude, 
                centre.Latitude, centre.Longitude + 1);
            double degreesOfDistFromCentre = radius / distOfOneDegree;
            double newDist = Linear_mode.CountingDistance(centre.Latitude, centre.Longitude,
                centre.Latitude, centre.Longitude + degreesOfDistFromCentre);
            return degreesOfDistFromCentre * radius / newDist;
        }
    }
}