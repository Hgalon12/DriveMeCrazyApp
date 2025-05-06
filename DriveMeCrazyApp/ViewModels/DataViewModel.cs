using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.ViewModels
{
    public class DataViewModel :ViewModelBase
    {
        private BarChart exampleChart;
        public BarChart ExampleChart
        {
            get
            {
                return exampleChart;
            }
            set
            {
                exampleChart = value;
                OnPropertyChanged();
            }
        }
        //public async ReadChart()
        //{

        //}
        public DataViewModel()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            entries.Add(new ChartEntry(7)
            {
                Color = SKColor.Parse("#3498db"),
                Label = "Hadas",
                ValueLabel = "7",
                
            });
            entries.Add(new ChartEntry(2)
            {
                Color = SKColor.Parse("#3498db"),
                Label = "Adar",
                ValueLabel = "2"
            });
            entries.Add(new ChartEntry(5)
            {
                Color = SKColor.Parse("#3498db"),
                Label = "Amit",
                ValueLabel = "5"
            });
            ExampleChart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 30,
                LabelOrientation = Orientation.Horizontal,
                SerieLabelTextSize = 30,
                ValueLabelOption = ValueLabelOption.OverElement,
                ValueLabelOrientation = Orientation.Horizontal,
                
            };
            


            
        }
    }
}
