using System.Collections.Generic;
using Microsoft.FSharp.Collections;
using OxyPlot;
using Promillekoll.Library;
using System;

namespace Promillekoll.UI
{
    public class MainViewModel
    {
        public PlotModel Model { get; private set; }

        public MainViewModel()
        {
            Model = new PlotModel();

            var profile = new Profile(Gender.Male, 80);
            
            var start = DateTime.Now.AddHours(-5);

            var drinks = new List<DrinkEntry> {
                new DrinkEntry(DrinkType.Beer, start.AddMinutes(5), 330.0, 4.6),
                new DrinkEntry(DrinkType.Beer, start.AddMinutes(25), 330.0, 4.6),
                new DrinkEntry(DrinkType.Beer, start.AddMinutes(50), 330.0, 4.6),
                new DrinkEntry(DrinkType.Beer, start.AddMinutes(75), 330.0, 4.6),
                new DrinkEntry(DrinkType.Spirits, start.AddMinutes(90), 40.0, 40.0)
            };
           
            var result = Library.Promillekoll.calculateAlcoholLevelOverTime(profile, ListModule.OfSeq(drinks));
            var series = new LineSeries("Alcohol Level");

            foreach (var entry in result)
            {
                var minutesSinceFirstDrink = entry.Item1.Subtract(start).TotalMinutes;
                series.Points.Add(new DataPoint(minutesSinceFirstDrink, entry.Item2));
            }
            Model.Series.Add(series);
        }
    }
}
