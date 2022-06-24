using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;


namespace Lab6
{
    class Program
    {
        // map_search.exe --db=ukraine_poi.csv --lat=30.212, --long=35.872 --size=20
        // 50.457703185031306, 30.40929970170323 -> 50.4582236329281, 30.430281850686008
        static void Main(string[] args)
        {
            float latitude = 0;
            float longitude = 0;
            float radius = 0;
            foreach (var arg in args)
            {
                string[] splitArg = arg.Split('=');
                if (splitArg[0] == "--lat")
                {
                    bool res = float.TryParse(splitArg[1], NumberStyles.Any, CultureInfo.InvariantCulture, out latitude);
                    if (!res)
                    {
                        throw new Exception("Wrong format");
                    }
                }
                if (splitArg[0] == "--long")
                {
                    bool res = float.TryParse(splitArg[1], NumberStyles.Any, CultureInfo.InvariantCulture, out longitude);
                    if (!res)
                    {
                        throw new Exception("Wrong format");
                    }
                }

                if (splitArg[0] == "--size")
                {
                    bool res = float.TryParse(splitArg[1], NumberStyles.Any, CultureInfo.InvariantCulture, out radius);
                    if (!res)
                    {
                        throw new Exception("Wrong format");
                    }
                }
            }
            
            Dictionary<float, float> appropriatePoints = Linear_mode.FindingPointsLinearly(latitude, longitude, radius);
            
            // Elapsed time: 00:00:00.6639001
            /*
            int i = 0;
            foreach (var latitudeKey in appropriatePoints.Keys)
            {
                if (i > 20)
                {
                    Console.WriteLine($"All amount of points: {appropriatePoints.Keys.Count}");
                    break;
                }
                Console.WriteLine($"{latitudeKey}, {appropriatePoints[latitudeKey]}");
                i += 1;
            }
            */
            Console.WriteLine(Linear_mode.CountingDistance(
                float.Parse("50.457703185031306", CultureInfo.InvariantCulture), 
                float.Parse("50.4582236329281", CultureInfo.InvariantCulture),
                float.Parse("30.40929970170323", CultureInfo.InvariantCulture),  
                float.Parse("30.430281850686008", CultureInfo.InvariantCulture)));
            
            
            
        }
    }
}


/*

        // знайдіть мінімальну та максимальну довготу та широту серед усіх точок
        // Max Lat: 56,17019, max long: 40,72657, min lat: 42,70549 min long: 22,11892
        float maxLatitude = float.Parse("56.17019", CultureInfo.InvariantCulture);
        float maxLongtitude = float.Parse("40,72657", CultureInfo.InvariantCulture);
        float minLatitude = float.Parse("42,70549", CultureInfo.InvariantCulture);
        float minLongtitude = float.Parse("22,11892", CultureInfo.InvariantCulture);
*/