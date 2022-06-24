using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lab6
{
    public class Linear_mode
    {
        //Програма має вивести всі точки, що потрапляють в цей радіус

        public static Dictionary<float, float> FindingPointsLinearly(float latitude, float longitude, float radius)
        {
            /*
            string pathToFile = @"C:\Users\Kate\Desktop\University\Programming\dictionary.txt";
            string[] allLines = File.ReadAllLines(pathToFile);
            */
            
            string[] contents = System.IO.File.ReadAllLines("ukraine_poi.csv");
            Dictionary<float, float> appropriatePoints = new Dictionary<float, float>();
            CultureInfo point = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            point.NumberFormat.NumberDecimalSeparator = ",";
            point.NumberFormat.NumberGroupSeparator = "";
            float maxLatitude = 0;
            float minLatitude = 180;
            float maxLongtitude = 0;
            float minLongtitude = 180;
            foreach (var line in contents)
            {
                string[] lineSeparated = line.Split(';');
                if (lineSeparated.Length < 3)
                {
                    continue;
                }
                
                float latitudeFromFile = float.Parse(lineSeparated[0], point);
                float longitudeFromFile = float.Parse(lineSeparated[1], point);
                if (latitudeFromFile > maxLatitude)
                {
                    maxLatitude = latitudeFromFile;
                }

                if (latitudeFromFile < minLatitude)
                {
                    minLatitude = latitudeFromFile;
                }

                if (longitudeFromFile > maxLongtitude)
                {
                    maxLongtitude = longitudeFromFile;
                }

                if (longitudeFromFile < minLongtitude)
                {
                    minLongtitude = longitudeFromFile;
                }
                
                if (CountingDistance(latitude, latitudeFromFile, longitude, longitudeFromFile) < radius)
                {
                    appropriatePoints[latitudeFromFile] = longitudeFromFile;
                }
            }

            Console.WriteLine($"Max Lat: {maxLatitude}, max long: {maxLongtitude}, min lat: {minLatitude} min long: {minLongtitude}");
            return appropriatePoints;
        }

        public static float CountingDistance(float latitude, float latitudeFromFile, float longitude, float longitudeFromFile)
        {
            float RadiusOfEarth = 6371; 
            double φ1 = latitude * Math.PI/180; 
            double φ2 = latitudeFromFile * Math.PI/180;
            double Δφ = (latitudeFromFile-latitude) * Math.PI/180;
            double Δλ = (longitudeFromFile-longitude) * Math.PI/180;

            double a = Math.Sin(Δφ/2) * Math.Sin(Δφ/2) + Math.Cos(φ1) * Math.Cos(φ2) * Math.Sin(Δλ/2) * Math.Sin(Δλ/2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double d = RadiusOfEarth * c;
            return float.Parse(d.ToString());
        }
    }
}