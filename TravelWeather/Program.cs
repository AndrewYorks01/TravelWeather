using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;

namespace TravelWeather
{

    // Class for a beach
    public class BeachInfo
    {
        public BeachInfo()
        {
            MonthlyHighs = new double[12];
            MonthlyLows = new double[12];
            MonthlyPrecip = new double[12];
            MonthlyWind = new double[12];
            MonthlyDewPoint = new double[12];
            MonthlyWaterTemps = new double[12];
        }
        public string Name { get; set; }
        public double[] MonthlyHighs { get; set; }
        public double[] MonthlyLows { get; set; }
        public double[] MonthlyPrecip { get; set; }
        public double[] MonthlyWind { get; set; }
        public double[] MonthlyDewPoint { get; set; }
        public double[] MonthlyWaterTemps { get; set; }
        public int score;


    }

    // Class for a national park-type area
    public class ParkInfo
    {
        public ParkInfo()
        {
            MonthlyHighs = new double[12];
            MonthlyLows = new double[12];
            MonthlyPrecip = new double[12];
            MonthlyWind = new double[12];
            MonthlyDewPoint = new double[12];
        }
        public string Name { get; set; }
        public double[] MonthlyHighs { get; set; }
        public double[] MonthlyLows { get; set; }
        public double[] MonthlyPrecip { get; set; }
        public double[] MonthlyWind { get; set; }
        public double[] MonthlyDewPoint { get; set; }

        public int score;
    }

    public class AmuseInfo
    {
        public AmuseInfo()
        {
            MonthlyHighs = new double[12];
            MonthlyLows = new double[12];
            MonthlyPrecip = new double[12];
            MonthlyWind = new double[12];
            MonthlyDewPoint = new double[12];
        }
        public string Name { get; set; }
        public double[] MonthlyHighs { get; set; }
        public double[] MonthlyLows { get; set; }
        public double[] MonthlyPrecip { get; set; }
        public double[] MonthlyWind { get; set; }
        public double[] MonthlyDewPoint { get; set; }

        public int score;

    }


    // Class for "baseline data"
    public class Baseline
    {
        public double High { get; set; }
        public double Low { get; set; }
        public double Precipitation { get; set; }
        public double Wind { get; set; }
        public double DewPoint { get; set; }
        public double WaterTemp { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Gets the current directory so the program can extract the proper XML files
            string direc = Directory.GetCurrentDirectory();
            string delete = "bin-Debug-netcoreapp3.1";
            direc = direc.Substring(0, direc.Length - (delete.Length));
            //Console.WriteLine(direc);

            // Define the three baselines; namely, Virginia Beach in June (for beaches and water parks), Chippokes in October (for camping), and Williamsburg in April (for dry parks).
            Baseline VirginiaBeach = new Baseline();
            VirginiaBeach.High = 83.03;
            VirginiaBeach.Low = 67.90;
            VirginiaBeach.Precipitation = 2.27;
            VirginiaBeach.Wind = 7.98;
            VirginiaBeach.DewPoint = 63.33;
            VirginiaBeach.WaterTemp = 72.50;

            Baseline Chippokes = new Baseline();
            Chippokes.High = 65.87;
            Chippokes.Low = 46.74;
            Chippokes.Precipitation = 4.84;
            Chippokes.Wind = 4.99;
            Chippokes.DewPoint = 49.96;

            Baseline Busch = new Baseline();
            Busch.High = 67.73;
            Busch.Low = 46.17;
            Busch.Precipitation = 3.27;
            Busch.Wind = 8.69;
            Busch.DewPoint = 44.84;


            // Ask the user if they want to look at data for beaches or parks
            Console.WriteLine("TRAVEL WEATHER");
            char travelType = '!';
            while ( (travelType != 'B') && (travelType != 'b') && (travelType != 'P') && (travelType != 'p') && (travelType != 'A') && (travelType != 'a') )
            {
                travelType = '!';
                Console.Write("\nDo you want to look at data for beaches (B), camping parks (P), or amusement parks (A)?: ");
                travelType = Console.ReadKey().KeyChar;
            }
            Console.WriteLine();

            char waterChoice = '!';
            bool waterRides = false;
            if ( (travelType == 'A') || (travelType == 'a') )
            {
                while ((waterChoice != 'Y') && (waterChoice != 'y') && (waterChoice != 'N') && (waterChoice != 'n'))
                {
                    waterChoice = '!';
                    Console.Write("\nWill you be riding water rides (Y/N)?: ");
                    waterChoice = Console.ReadKey().KeyChar;
                }
                if ( (waterChoice == 'Y') || (waterChoice == 'y') )
                        waterRides = true;
                Console.WriteLine();
            }

            // Ask the user the month they want to look at data for
            int month = 0;
            while ( (month < 1) || (month > 12))
            {
                Console.Write("\nWhich month (1-12) do you want to look at weather data for?: ");
                month = int.Parse(Console.ReadLine());
            }

            // Load the proper XML file
            string fileSearch = "abc";
            int locationType = 0; // if 0, beaches, if 1, camping, if 2, amusement parks
            if ((travelType == 'B') || (travelType == 'b'))
                fileSearch = direc + "XML\\Beaches.xml";
            else if ((travelType == 'P') || (travelType == 'p'))
            {
                    fileSearch = direc + "XML\\Parks.xml";
                    locationType = 1;
            }
            else
            {
                fileSearch = direc + "XML\\AmusementParks.xml";
                locationType = 2;
            }

            if (File.Exists(fileSearch)){
                Console.WriteLine("Here's your data:");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("One of the XML files is missing.");
                System.Environment.Exit(1);
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(fileSearch);

            List<BeachInfo> Beaches = new List<BeachInfo>();
            List<ParkInfo> Parks = new List<ParkInfo>();
            List<AmuseInfo> Amusements = new List<AmuseInfo>();

            XmlNodeList nodeList;

            // If the user wants to look at beaches
            if (locationType == 0)
            {
                nodeList = doc.SelectNodes("/BeachData/Beach");
                foreach (XmlNode node in nodeList)
                {
                    BeachInfo myBeach = new BeachInfo();
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        // Go through all the child nodes and add their data to myBeach.
                        if (child.Name == "Name")
                            myBeach.Name = child.InnerText;

                        // Max temperatures
                        else if (child.Name == "JanMax")
                            myBeach.MonthlyHighs[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebMax")
                            myBeach.MonthlyHighs[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarMax")
                            myBeach.MonthlyHighs[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprMax")
                            myBeach.MonthlyHighs[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayMax")
                            myBeach.MonthlyHighs[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunMax")
                            myBeach.MonthlyHighs[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulMax")
                            myBeach.MonthlyHighs[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugMax")
                            myBeach.MonthlyHighs[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepMax")
                            myBeach.MonthlyHighs[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctMax")
                            myBeach.MonthlyHighs[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovMax")
                            myBeach.MonthlyHighs[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecMax")
                            myBeach.MonthlyHighs[11] = Convert.ToDouble(child.InnerText);

                        // Min temperatures
                        else if (child.Name == "JanMin")
                            myBeach.MonthlyLows[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebMin")
                            myBeach.MonthlyLows[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarMin")
                            myBeach.MonthlyLows[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprMin")
                            myBeach.MonthlyLows[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayMin")
                            myBeach.MonthlyLows[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunMin")
                            myBeach.MonthlyLows[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulMin")
                            myBeach.MonthlyLows[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugMin")
                            myBeach.MonthlyLows[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepMin")
                            myBeach.MonthlyLows[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctMin")
                            myBeach.MonthlyLows[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovMin")
                            myBeach.MonthlyLows[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecMin")
                            myBeach.MonthlyLows[11] = Convert.ToDouble(child.InnerText);

                        // Precipitation
                        else if (child.Name == "JanRain")
                            myBeach.MonthlyPrecip[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebRain")
                            myBeach.MonthlyPrecip[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarRain")
                            myBeach.MonthlyPrecip[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprRain")
                            myBeach.MonthlyPrecip[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayRain")
                            myBeach.MonthlyPrecip[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunRain")
                            myBeach.MonthlyPrecip[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulRain")
                            myBeach.MonthlyPrecip[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugRain")
                            myBeach.MonthlyPrecip[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepRain")
                            myBeach.MonthlyPrecip[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctRain")
                            myBeach.MonthlyPrecip[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovRain")
                            myBeach.MonthlyPrecip[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecRain")
                            myBeach.MonthlyPrecip[11] = Convert.ToDouble(child.InnerText);

                        // Wind speed
                        else if (child.Name == "JanWind")
                            myBeach.MonthlyWind[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebWind")
                            myBeach.MonthlyWind[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarWind")
                            myBeach.MonthlyWind[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprWind")
                            myBeach.MonthlyWind[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayWind")
                            myBeach.MonthlyWind[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunWind")
                            myBeach.MonthlyWind[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulWind")
                            myBeach.MonthlyWind[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugWind")
                            myBeach.MonthlyWind[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepWind")
                            myBeach.MonthlyWind[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctWind")
                            myBeach.MonthlyWind[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovWind")
                            myBeach.MonthlyWind[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecWind")
                            myBeach.MonthlyWind[11] = Convert.ToDouble(child.InnerText);

                        // Dew point
                        else if (child.Name == "JanDew")
                            myBeach.MonthlyDewPoint[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebDew")
                            myBeach.MonthlyDewPoint[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarDew")
                            myBeach.MonthlyDewPoint[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprDew")
                            myBeach.MonthlyDewPoint[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayDew")
                            myBeach.MonthlyDewPoint[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunDew")
                            myBeach.MonthlyDewPoint[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulDew")
                            myBeach.MonthlyDewPoint[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugDew")
                            myBeach.MonthlyDewPoint[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepDew")
                            myBeach.MonthlyDewPoint[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctDew")
                            myBeach.MonthlyDewPoint[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovDew")
                            myBeach.MonthlyDewPoint[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecDew")
                            myBeach.MonthlyDewPoint[11] = Convert.ToDouble(child.InnerText);

                        // Sea temperature
                        else if (child.Name == "JanSea")
                            myBeach.MonthlyWaterTemps[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebSea")
                            myBeach.MonthlyWaterTemps[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarSea")
                            myBeach.MonthlyWaterTemps[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprSea")
                            myBeach.MonthlyWaterTemps[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MaySea")
                            myBeach.MonthlyWaterTemps[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunSea")
                            myBeach.MonthlyWaterTemps[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulSea")
                            myBeach.MonthlyWaterTemps[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugSea")
                            myBeach.MonthlyWaterTemps[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepSea")
                            myBeach.MonthlyWaterTemps[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctSea")
                            myBeach.MonthlyWaterTemps[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovSea")
                            myBeach.MonthlyWaterTemps[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecSea")
                            myBeach.MonthlyWaterTemps[11] = Convert.ToDouble(child.InnerText);

                        myBeach.score = 0;
                    } // end foreach for a single location

                    // Add myBeach to the main Beaches list.
                    Beaches.Add(myBeach);

                } // end foreach for the entire list

                Console.Write("{0, -35} {1,-6} {2,-6} {3, -6} {4, -6} {5, -10} {6, -10} {7, -5}\n", "Name", "High", "Low", "Rain", "Wind", "Dew Point", "Water Temp", "Score");

                //Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2} {6, -10:N2}", "Virginia Beach in June", VirginiaBeach.High,
                    //VirginiaBeach.Low, VirginiaBeach.Precipitation, VirginiaBeach.Wind, VirginiaBeach.DewPoint, VirginiaBeach.WaterTemp);

                foreach (var beaches in Beaches)
                {
                    double hiTempRange = Math.Abs(VirginiaBeach.High-beaches.MonthlyHighs[month - 1]);
                    double loTempRange = Math.Abs(VirginiaBeach.Low - beaches.MonthlyLows[month - 1]);

                    // If the max temp is within 5 degrees of optimal, 2 points are added to the score.
                    // If the max temp is between 5 and 10 degrees of optimal, 1 point is added to the score.
                    // If the max temp is more than 10 degrees from optimal, 0 points are added.
                    if (hiTempRange <= 5)
                        beaches.score += 2;
                    else if ((hiTempRange > 5) && (hiTempRange <= 10))
                        beaches.score += 1;
                    else
                        beaches.score += 0;

                    // The same logic used for the max temp applies to the min temp.
                    if (loTempRange <= 5)
                        beaches.score += 2;
                    else if ((loTempRange > 5) && (loTempRange <= 10))
                        beaches.score += 1;
                    else
                        beaches.score += 0;

                    // If less than 5 inches of rain, 2 points are added to the score.
                    // If between 5 and 7 inches of rain, 1 point is added.
                    // If more than 7 inches of rain, 0 points are added.
                    if (beaches.MonthlyPrecip[month - 1] <= 5)
                        beaches.score += 2;
                    else if ((beaches.MonthlyPrecip[month - 1] > 5) && (beaches.MonthlyPrecip[month - 1] <= 7))
                        beaches.score += 1;
                    else
                        beaches.score += 0;

                    // If wind speed is less than 8 mph, 2 points are added to the score.
                    // If wind speed is between 8 and 12 mph, 1 point is added to the score.
                    // If wind speed is more than 12 mph, 0 points are added to the score.
                    if (beaches.MonthlyWind[month - 1] <= 8)
                        beaches.score += 2;
                    else if ((beaches.MonthlyWind[month - 1] > 8) && (beaches.MonthlyWind[month - 1] <= 12))
                        beaches.score += 1;
                    else
                        beaches.score += 0;

                    // If dew point is less than 53 degrees, 2 points are added to the score.
                    // If dew point is between 53 and 65 degrees, 1 point is added to the score.
                    // If dew point is more than 65 degrees, 0 points are added to the score.
                    if (beaches.MonthlyDewPoint[month - 1] <= 53)
                        beaches.score += 2;
                    else if ((beaches.MonthlyDewPoint[month - 1] > 53) && (beaches.MonthlyDewPoint[month - 1] <= 65))
                        beaches.score += 1;
                    else
                        beaches.score += 0;

                    // If water temperature is greater than 68 degrees, 2 points are added to the score.
                    // If water temperature is between 63 and 68 degrees, 1 point is added to the score.
                    // If water temperature is less than 63 degrees, 0 points are added to the score.
                    if (beaches.MonthlyWaterTemps[month - 1] >= 68)
                        beaches.score += 2;
                    else if ((beaches.MonthlyWaterTemps[month - 1] < 68) && (beaches.MonthlyWaterTemps[month - 1] >= 63))
                        beaches.score += 1;
                    else
                        beaches.score += 0;

                    // If the low temperature is below 30, the final score is by default 0.
                    if (beaches.MonthlyLows[month - 1] < 30)
                        beaches.score = 0;


                    //Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2} {6, -10:N2} {7, -2:N0}", beaches.Name, beaches.MonthlyHighs[month-1],
                    //    beaches.MonthlyLows[month-1], beaches.MonthlyPrecip[month-1], beaches.MonthlyWind[month-1], beaches.MonthlyDewPoint[month-1],
                    //    beaches.MonthlyWaterTemps[month-1], beaches.score);
                }

                //Console.WriteLine("Next data: ");

                Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2} {6, -10:N2}", "Virginia Beach in June", VirginiaBeach.High,
                        VirginiaBeach.Low, VirginiaBeach.Precipitation, VirginiaBeach.Wind, VirginiaBeach.DewPoint, VirginiaBeach.WaterTemp);

                int scoreCheck = 12;

                while (scoreCheck >= 0)
                {
                    foreach (var lookBeach in Beaches)
                    {
                        if (lookBeach.score == scoreCheck)
                        {
                            Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2} {6, -10:N2} {7, -2:N0}", lookBeach.Name, lookBeach.MonthlyHighs[month - 1],
                                lookBeach.MonthlyLows[month - 1], lookBeach.MonthlyPrecip[month - 1], lookBeach.MonthlyWind[month - 1], lookBeach.MonthlyDewPoint[month - 1],
                                lookBeach.MonthlyWaterTemps[month - 1], lookBeach.score);
                        }
                    }
                    scoreCheck--;
                }

            } // end for looking at beaches

            // User is looking at park data
            else if (locationType == 1)
            {
                nodeList = doc.SelectNodes("/ParkData/Park");
                foreach (XmlNode node in nodeList)
                {
                    ParkInfo myPark = new ParkInfo();
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        // Go through all the child nodes and add their data to myPark.
                        if (child.Name == "Name")
                            myPark.Name = child.InnerText;

                        // Max temperatures
                        else if (child.Name == "JanMax")
                            myPark.MonthlyHighs[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebMax")
                            myPark.MonthlyHighs[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarMax")
                            myPark.MonthlyHighs[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprMax")
                            myPark.MonthlyHighs[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayMax")
                            myPark.MonthlyHighs[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunMax")
                            myPark.MonthlyHighs[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulMax")
                            myPark.MonthlyHighs[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugMax")
                            myPark.MonthlyHighs[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepMax")
                            myPark.MonthlyHighs[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctMax")
                            myPark.MonthlyHighs[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovMax")
                            myPark.MonthlyHighs[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecMax")
                            myPark.MonthlyHighs[11] = Convert.ToDouble(child.InnerText);

                        // Min temperatures
                        else if (child.Name == "JanMin")
                            myPark.MonthlyLows[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebMin")
                            myPark.MonthlyLows[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarMin")
                            myPark.MonthlyLows[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprMin")
                            myPark.MonthlyLows[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayMin")
                            myPark.MonthlyLows[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunMin")
                            myPark.MonthlyLows[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulMin")
                            myPark.MonthlyLows[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugMin")
                            myPark.MonthlyLows[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepMin")
                            myPark.MonthlyLows[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctMin")
                            myPark.MonthlyLows[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovMin")
                            myPark.MonthlyLows[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecMin")
                            myPark.MonthlyLows[11] = Convert.ToDouble(child.InnerText);

                        // Precipitation
                        else if (child.Name == "JanRain")
                            myPark.MonthlyPrecip[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebRain")
                            myPark.MonthlyPrecip[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarRain")
                            myPark.MonthlyPrecip[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprRain")
                            myPark.MonthlyPrecip[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayRain")
                            myPark.MonthlyPrecip[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunRain")
                            myPark.MonthlyPrecip[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulRain")
                            myPark.MonthlyPrecip[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugRain")
                            myPark.MonthlyPrecip[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepRain")
                            myPark.MonthlyPrecip[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctRain")
                            myPark.MonthlyPrecip[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovRain")
                            myPark.MonthlyPrecip[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecRain")
                            myPark.MonthlyPrecip[11] = Convert.ToDouble(child.InnerText);

                        // Wind speed
                        else if (child.Name == "JanWind")
                            myPark.MonthlyWind[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebWind")
                            myPark.MonthlyWind[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarWind")
                            myPark.MonthlyWind[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprWind")
                            myPark.MonthlyWind[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayWind")
                            myPark.MonthlyWind[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunWind")
                            myPark.MonthlyWind[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulWind")
                            myPark.MonthlyWind[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugWind")
                            myPark.MonthlyWind[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepWind")
                            myPark.MonthlyWind[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctWind")
                            myPark.MonthlyWind[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovWind")
                            myPark.MonthlyWind[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecWind")
                            myPark.MonthlyWind[11] = Convert.ToDouble(child.InnerText);

                        // Dew point
                        else if (child.Name == "JanDew")
                            myPark.MonthlyDewPoint[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebDew")
                            myPark.MonthlyDewPoint[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarDew")
                            myPark.MonthlyDewPoint[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprDew")
                            myPark.MonthlyDewPoint[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayDew")
                            myPark.MonthlyDewPoint[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunDew")
                            myPark.MonthlyDewPoint[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulDew")
                            myPark.MonthlyDewPoint[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugDew")
                            myPark.MonthlyDewPoint[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepDew")
                            myPark.MonthlyDewPoint[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctDew")
                            myPark.MonthlyDewPoint[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovDew")
                            myPark.MonthlyDewPoint[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecDew")
                            myPark.MonthlyDewPoint[11] = Convert.ToDouble(child.InnerText);

                        myPark.score = 0;

                    } // end foreach for a single location

                    // Add myPark to the main Parks list.
                    Parks.Add(myPark);

                } // end foreach for the entire list

                Console.Write("{0, -35} {1,-6} {2,-6} {3, -6} {4, -6} {5, -10} {6, -5}\n", "Name", "High", "Low", "Rain", "Wind", "Dew Point", "Score");
                Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2}", "Chippokes in October", Chippokes.High,
                    Chippokes.Low, Chippokes.Precipitation, Chippokes.Wind, Chippokes.DewPoint);
                foreach (var parks in Parks)
                {
                    double hiTempRange = Math.Abs(Chippokes.High - parks.MonthlyHighs[month - 1]);
                    double loTempRange = Math.Abs(Chippokes.Low - parks.MonthlyLows[month - 1]);

                    // If the max temp is within 5 degrees of optimal, 2 points are added to the score.
                    // If the max temp is between 5 and 10 degrees of optimal, 1 point is added to the score.
                    // If the max temp is more than 10 degrees from optimal, 0 points are added.
                    if (hiTempRange <= 5)
                        parks.score += 2;
                    else if ((hiTempRange > 5) && (hiTempRange <= 10))
                        parks.score += 1;
                    else
                        parks.score += 0;

                    // The same logic used for the max temp applies to the min temp.
                    if (loTempRange <= 5)
                        parks.score += 2;
                    else if ((loTempRange > 5) && (loTempRange <= 10))
                        parks.score += 1;
                    else
                        parks.score += 0;

                    // If less than 5 inches of rain, 2 points are added to the score.
                    // If between 5 and 7 inches of rain, 1 point is added.
                    // If more than 7 inches of rain, 0 points are added.
                    if (parks.MonthlyPrecip[month - 1] <= 5)
                        parks.score += 2;
                    else if ((parks.MonthlyPrecip[month - 1] > 5) && (parks.MonthlyPrecip[month - 1] <= 7))
                        parks.score += 1;
                    else
                        parks.score += 0;

                    // If wind speed is less than 8 mph, 2 points are added to the score.
                    // If wind speed is between 8 and 12 mph, 1 point is added to the score.
                    // If wind speed is more than 12 mph, 0 points are added to the score.
                    if (parks.MonthlyWind[month - 1] <= 8)
                        parks.score += 2;
                    else if ((parks.MonthlyWind[month - 1] > 8) && (parks.MonthlyWind[month - 1] <= 12))
                        parks.score += 1;
                    else
                        parks.score += 0;

                    // If dew point is less than 53 degrees, 2 points are added to the score.
                    // If dew point is between 53 and 65 degrees, 1 point is added to the score.
                    // If dew point is more than 65 degrees, 0 points are added to the score.
                    if (parks.MonthlyDewPoint[month - 1] <= 53)
                        parks.score += 2;
                    else if ((parks.MonthlyDewPoint[month - 1] > 53) && (parks.MonthlyDewPoint[month - 1] <= 65))
                        parks.score += 1;
                    else
                        parks.score += 0;

                    // If the low temperature is below 30, the final score is by default 0.
                    if (parks.MonthlyLows[month - 1] < 30)
                        parks.score = 0;

                    // If the high temperature is above 90, the final score is by default 0.
                    if (parks.MonthlyHighs[month - 1] > 90)
                        parks.score = 0;

                    //Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2} {6, -5:N0}", parks.Name, parks.MonthlyHighs[month - 1],
                    //    parks.MonthlyLows[month - 1], parks.MonthlyPrecip[month - 1], parks.MonthlyWind[month - 1], parks.MonthlyDewPoint[month - 1], parks.score);
                }

                int scoreCheck = 10;

                while (scoreCheck >= 0)
                {
                    foreach (var lookPark in Parks)
                    {
                        if (lookPark.score == scoreCheck)
                        {
                            Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2} {6, -5:N0}", lookPark.Name, lookPark.MonthlyHighs[month - 1],
                                lookPark.MonthlyLows[month - 1], lookPark.MonthlyPrecip[month - 1], lookPark.MonthlyWind[month - 1], lookPark.MonthlyDewPoint[month - 1],
                                lookPark.score);
                        }
                    }
                    scoreCheck--;
                }

            } // end for looking at parks

            else
            {
                //Console.WriteLine("Filler for park data");
                nodeList = doc.SelectNodes("/AmusementData/Park");
                foreach (XmlNode node in nodeList)
                {
                    AmuseInfo myPark = new AmuseInfo();
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        // Go through all the child nodes and add their data to myPark.
                        if (child.Name == "Name")
                            myPark.Name = child.InnerText;

                        // Max temperatures
                        else if (child.Name == "JanMax")
                            myPark.MonthlyHighs[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebMax")
                            myPark.MonthlyHighs[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarMax")
                            myPark.MonthlyHighs[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprMax")
                            myPark.MonthlyHighs[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayMax")
                            myPark.MonthlyHighs[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunMax")
                            myPark.MonthlyHighs[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulMax")
                            myPark.MonthlyHighs[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugMax")
                            myPark.MonthlyHighs[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepMax")
                            myPark.MonthlyHighs[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctMax")
                            myPark.MonthlyHighs[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovMax")
                            myPark.MonthlyHighs[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecMax")
                            myPark.MonthlyHighs[11] = Convert.ToDouble(child.InnerText);

                        // Min temperatures
                        else if (child.Name == "JanMin")
                            myPark.MonthlyLows[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebMin")
                            myPark.MonthlyLows[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarMin")
                            myPark.MonthlyLows[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprMin")
                            myPark.MonthlyLows[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayMin")
                            myPark.MonthlyLows[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunMin")
                            myPark.MonthlyLows[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulMin")
                            myPark.MonthlyLows[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugMin")
                            myPark.MonthlyLows[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepMin")
                            myPark.MonthlyLows[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctMin")
                            myPark.MonthlyLows[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovMin")
                            myPark.MonthlyLows[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecMin")
                            myPark.MonthlyLows[11] = Convert.ToDouble(child.InnerText);

                        // Precipitation
                        else if (child.Name == "JanRain")
                            myPark.MonthlyPrecip[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebRain")
                            myPark.MonthlyPrecip[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarRain")
                            myPark.MonthlyPrecip[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprRain")
                            myPark.MonthlyPrecip[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayRain")
                            myPark.MonthlyPrecip[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunRain")
                            myPark.MonthlyPrecip[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulRain")
                            myPark.MonthlyPrecip[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugRain")
                            myPark.MonthlyPrecip[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepRain")
                            myPark.MonthlyPrecip[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctRain")
                            myPark.MonthlyPrecip[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovRain")
                            myPark.MonthlyPrecip[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecRain")
                            myPark.MonthlyPrecip[11] = Convert.ToDouble(child.InnerText);

                        // Wind speed
                        else if (child.Name == "JanWind")
                            myPark.MonthlyWind[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebWind")
                            myPark.MonthlyWind[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarWind")
                            myPark.MonthlyWind[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprWind")
                            myPark.MonthlyWind[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayWind")
                            myPark.MonthlyWind[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunWind")
                            myPark.MonthlyWind[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulWind")
                            myPark.MonthlyWind[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugWind")
                            myPark.MonthlyWind[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepWind")
                            myPark.MonthlyWind[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctWind")
                            myPark.MonthlyWind[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovWind")
                            myPark.MonthlyWind[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecWind")
                            myPark.MonthlyWind[11] = Convert.ToDouble(child.InnerText);

                        // Dew point
                        else if (child.Name == "JanDew")
                            myPark.MonthlyDewPoint[0] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "FebDew")
                            myPark.MonthlyDewPoint[1] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MarDew")
                            myPark.MonthlyDewPoint[2] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AprDew")
                            myPark.MonthlyDewPoint[3] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "MayDew")
                            myPark.MonthlyDewPoint[4] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JunDew")
                            myPark.MonthlyDewPoint[5] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "JulDew")
                            myPark.MonthlyDewPoint[6] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "AugDew")
                            myPark.MonthlyDewPoint[7] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "SepDew")
                            myPark.MonthlyDewPoint[8] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "OctDew")
                            myPark.MonthlyDewPoint[9] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "NovDew")
                            myPark.MonthlyDewPoint[10] = Convert.ToDouble(child.InnerText);
                        else if (child.Name == "DecDew")
                            myPark.MonthlyDewPoint[11] = Convert.ToDouble(child.InnerText);

                        myPark.score = 0;

                    } // end foreach for a single location

                    // Add myPark to the main Amusements list.
                    Amusements.Add(myPark);
                } // end foreach for the entire list
                
                Console.Write("{0, -35} {1,-6} {2,-6} {3, -6} {4, -6} {5, -10} {6, -5}\n", "Name", "High", "Low", "Rain", "Wind", "Dew Point", "Score");
                Baseline parkBaseline = new Baseline();
                if (waterRides)
                {
                    parkBaseline.High = VirginiaBeach.High;
                    parkBaseline.Low = VirginiaBeach.Low;
                    parkBaseline.Precipitation = VirginiaBeach.Precipitation;
                    parkBaseline.Wind = VirginiaBeach.Wind;
                    parkBaseline.DewPoint = VirginiaBeach.DewPoint;
                    Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2}", "Virginia Beach in June", parkBaseline.High,
                    parkBaseline.Low, parkBaseline.Precipitation, parkBaseline.Wind, parkBaseline.DewPoint);
                }
                else
                {
                    parkBaseline.High = Busch.High;
                    parkBaseline.Low = Busch.Low;
                    parkBaseline.Precipitation = Busch.Precipitation;
                    parkBaseline.Wind = Busch.Wind;
                    parkBaseline.DewPoint = Busch.DewPoint;
                    Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2}", "Williamsburg in April", parkBaseline.High,
                    parkBaseline.Low, parkBaseline.Precipitation, parkBaseline.Wind, parkBaseline.DewPoint);
                }

                foreach (var amuse in Amusements)
                {
                    double hiTempRange = Math.Abs(parkBaseline.High - amuse.MonthlyHighs[month - 1]);
                    double loTempRange = Math.Abs(parkBaseline.Low - amuse.MonthlyLows[month - 1]);

                    // If the max temp is within 5 degrees of optimal, 2 points are added to the score.
                    // If the max temp is between 5 and 10 degrees of optimal, 1 point is added to the score.
                    // If the max temp is more than 10 degrees from optimal, 0 points are added.
                    if (hiTempRange <= 5)
                        amuse.score += 2;
                    else if ((hiTempRange > 5) && (hiTempRange <= 10))
                        amuse.score += 1;
                    else
                        amuse.score += 0;

                    // The same logic used for the max temp applies to the min temp.
                    if (loTempRange <= 5)
                        amuse.score += 2;
                    else if ((loTempRange > 5) && (loTempRange <= 10))
                        amuse.score += 1;
                    else
                        amuse.score += 0;

                    // If less than 5 inches of rain, 2 points are added to the score.
                    // If between 5 and 7 inches of rain, 1 point is added.
                    // If more than 7 inches of rain, 0 points are added.
                    if (amuse.MonthlyPrecip[month - 1] <= 5)
                        amuse.score += 2;
                    else if ((amuse.MonthlyPrecip[month - 1] > 5) && (amuse.MonthlyPrecip[month - 1] <= 7))
                        amuse.score += 1;
                    else
                        amuse.score += 0;

                    // If wind speed is less than 8 mph, 2 points are added to the score.
                    // If wind speed is between 8 and 12 mph, 1 point is added to the score.
                    // If wind speed is more than 12 mph, 0 points are added to the score.
                    if (amuse.MonthlyWind[month - 1] <= 8)
                        amuse.score += 2;
                    else if ((amuse.MonthlyWind[month - 1] > 8) && (amuse.MonthlyWind[month - 1] <= 12))
                        amuse.score += 1;
                    else
                        amuse.score += 0;

                    // If dew point is less than 53 degrees, 2 points are added to the score.
                    // If dew point is between 53 and 65 degrees, 1 point is added to the score.
                    // If dew point is more than 65 degrees, 0 points are added to the score.
                    if (amuse.MonthlyDewPoint[month - 1] <= 53)
                        amuse.score += 2;
                    else if ((amuse.MonthlyDewPoint[month - 1] > 53) && (amuse.MonthlyDewPoint[month - 1] <= 65))
                        amuse.score += 1;
                    else
                        amuse.score += 0;

                    // If the low temperature is below 30, the final score is by default 0.
                    if (amuse.MonthlyLows[month - 1] < 30)
                        amuse.score = 0;
                }

                int scoreCheck = 10;

                while (scoreCheck >= 0)
                {
                    foreach (var lookPark in Amusements)
                    {
                        if ((lookPark.score == scoreCheck) && ((lookPark.MonthlyHighs[month-1] > 0)) )
                        {
                            Console.WriteLine("{0,-35} {1, -6:N2} {2, -6:N2} {3, -6:N2} {4, -6:N2} {5, -10:N2} {6, -5:N0}", lookPark.Name, lookPark.MonthlyHighs[month - 1],
                                lookPark.MonthlyLows[month - 1], lookPark.MonthlyPrecip[month - 1], lookPark.MonthlyWind[month - 1], lookPark.MonthlyDewPoint[month - 1],
                                lookPark.score);
                        }
                    }
                    scoreCheck--;
                }

            } // end for looking at amusement parks

            Console.WriteLine();
            Console.WriteLine("Press any key to exit the program.");
            Console.ReadKey();

        } // end of program
    }
}
