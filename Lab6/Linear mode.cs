using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lab6
{
    public class Linear_mode
    {
        public static List<Point> FindingPointsLinearly(double latitude, double longitude, double radius)
        {
            string[] contents = System.IO.File.ReadAllLines("ukraine_poi.csv");
            List<Point> allPoints = new List<Point>();
            CultureInfo point = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            point.NumberFormat.NumberDecimalSeparator = ",";
            point.NumberFormat.NumberGroupSeparator = "";
            double maxLatitude = 0;
            double minLatitude = 180;
            double maxLongitude = 0;
            double minLongitude = 180;
            foreach (var line in contents)
            {
                string[] lineSeparated = line.Split(';');
                if (lineSeparated.Length < 3)
                {
                    continue;
                }
                
                double latitudeFromFile = double.Parse(lineSeparated[0], point);
                double longitudeFromFile = double.Parse(lineSeparated[1], point);
                if (latitudeFromFile > maxLatitude)
                {
                    maxLatitude = latitudeFromFile;
                }

                if (latitudeFromFile < minLatitude)
                {
                    minLatitude = latitudeFromFile;
                }

                if (longitudeFromFile > maxLongitude)
                {
                    maxLongitude = longitudeFromFile;
                }

                if (longitudeFromFile < minLongitude)
                {
                    minLongitude = longitudeFromFile;
                }
                allPoints.Add(new Point(latitudeFromFile, longitudeFromFile));
            }

            Point centre = new Point(latitude, longitude);
            List<Point> appropriatePoints = FindPointsFromCircle(allPoints, radius, centre);

            return appropriatePoints;
        }

        public static List<Point> FindPointsFromCircle(List<Point> allPoints, double radius, Point centre)
        {
            List<Point> appropriatePoints = new List<Point>();
            foreach (var pointFromFile in allPoints)
            {
                if (CountingDistance(centre.Latitude, centre.Longitude, 
                        pointFromFile.Latitude, pointFromFile.Longitude) < radius)
                {
                    appropriatePoints.Add(pointFromFile);
                }
            }

            return appropriatePoints;
        }

        public static double CountingDistance(double latitude, double longitude, double latitudeFromFile, double longitudeFromFile)
        {
            double RadiusOfEarth = 6371; 
            double φ1 = latitude * Math.PI/180; 
            double φ2 = latitudeFromFile * Math.PI/180;
            double deltaφ = (latitudeFromFile-latitude) * Math.PI/180;
            double deltaλ = (longitudeFromFile-longitude) * Math.PI/180;

            double a = Math.Sin(deltaφ/2) * Math.Sin(deltaφ/2) + Math.Cos(φ1) * Math.Cos(φ2) * Math.Sin(deltaλ/2) * Math.Sin(deltaλ/2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double d = RadiusOfEarth * c;
            return d;
        }
        
    }
}