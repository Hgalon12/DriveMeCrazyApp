﻿using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.ViewModels
{
    public class DataViewModel :ViewModelBase
    {
        private bool isDataExist;
        public bool IsDataExist
        {
            get
            {
                return isDataExist;
            }
            set
            {
                isDataExist = value;
                OnPropertyChanged();
            }
        }

        private bool isDataNotExist;
        public bool IsDataNotExist
        {
            get
            {
                return isDataNotExist;
            }
            set
            {
                isDataNotExist = value;
                OnPropertyChanged();
            }
        }

        private BarChart usageChart;
        public BarChart UsageChart
        {
            get
            {
                return usageChart;
            }
            set
            {
                usageChart = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<CarUseChart> chart;
        public ObservableCollection<CarUseChart> Chart
        {
            get => chart;
            set
            {
                chart = value;
                OnPropertyChanged();
            }
        }
        public async void ReadChart()
        {
            TableUser u = ((App)Application.Current).LoggedInUser;
            if (u != null)
            {


                List<CarUseChart> list;
                if (u.CarOwnerId == null)
                {
                    list = await proxy.GetCarUsage(u.Id, 7);
                }
                else
                {
                    list = await proxy.GetCarUsage(u.CarOwnerId, 7);
                }

                if (list != null)
                {
                    Chart = new ObservableCollection<CarUseChart>(list);
                }
                else
                    Chart = new ObservableCollection<CarUseChart>();

                IsDataExist = Chart.Count > 0;
                IsDataNotExist = Chart.Count == 0;

                // הוספת כל הערכים מ-Chart אל entries
                List<ChartEntry> entries = new List<ChartEntry>();
                entries.Clear();
                foreach (var carUse in Chart)
                {
                    entries.Add(new ChartEntry((float)carUse.Hours)
                    {
                        Color = SKColor.Parse("#3498db"),
                        Label = carUse.Name,
                        ValueLabel = carUse.Hours.ToString()
                    });
                }

                UsageChart = new BarChart()
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
        private DriveMeCrazyWebAPIProxy proxy;
        public DataViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;
            TableUser u = ((App)Application.Current).LoggedInUser;
            Chart = new ObservableCollection<CarUseChart>();
            ReadChart();

           

            
            //entries.Add(new ChartEntry(7)
            //{
            //    Color = SKColor.Parse("#3498db"),
            //    Label = "Hadas",
            //    ValueLabel = "7",

            //});
            //entries.Add(new ChartEntry(2)
            //{
            //    Color = SKColor.Parse("#3498db"),
            //    Label = "Adar",
            //    ValueLabel = "2"
            //});
            //entries.Add(new ChartEntry(5)
            //{
            //    Color = SKColor.Parse("#3498db"),
            //    Label = "Amit",
            //    ValueLabel = "5"
            //});
            
            


            
        }
    }
}
