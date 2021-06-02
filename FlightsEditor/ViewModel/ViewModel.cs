using FlightsEditor.Model;
using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    class ViewModel : INotifyPropertyChanged
    {
        public TrainTicketEntities ctx = new TrainTicketEntities();

        public ViewModel()
        {
            FillTrain();
            FillTypeRailwayCarriageName();
            FillTypeTrainName();
            FillStation();
            FillStationName();
        }

        private void FillTrain()
        {
            var q = (from a in ctx.Train
                     select a).ToList();
            this.Train = q;
        }

        private void FillTypeRailwayCarriageName()
        {
            var q = (from a in ctx.TypeRailwayCarriage
                     .Select(a => a.Name)
                     select a).ToList();
            this.TypeRailwayCarriageName = q;
        }
        private void FillTypeTrainName()
        {
            var q = (from a in ctx.TypeTrain
                     .Select(a => a.Name)
                     select a).ToList();
            this.TypeTrainName = q;
        }

        private void FillStation()
        {
            var q = (from a in ctx.Station
                     select a).ToList();
            this.Station = q;
        }

        private void FillStationName()
        {
            var q = (from a in ctx.Station
                     .Select(a => a.Name)
                     select a).ToList();
            this.StationName = q;
        }

        private List<Station> _station;
        public List<Station> Station
        {
            get { return _station; }
            set
            {
                _station = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> _stationName;
        public List<string> StationName
        {
            get { return _stationName; }
            set
            {
                _stationName = value;
                NotifyPropertyChanged();
            }
        }

        private List<Train> _train;
        public List<Train> Train
        {
            get { return _train; }
            set
            {
                _train = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> _typeRailwayCarriageName;
        public List<string> TypeRailwayCarriageName
        {
            get { return _typeRailwayCarriageName; }
            set
            {
                _typeRailwayCarriageName = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> _typeTrainName;
        public List<string> TypeTrainName
        {
            get { return _typeTrainName; }
            set
            {
                _typeTrainName = value;
                NotifyPropertyChanged();
            }
        }

        //===Вкладка Добавить рейс

        private ObservableCollection<RailwayCarriage> _railwayCarriageAddFlight;
        public ObservableCollection<RailwayCarriage> RailwayCarriageAddFlight
        {
            get { return _railwayCarriageAddFlight; }
            set
            {
                _railwayCarriageAddFlight = value;
                int amoutSeat = 0;
                foreach (var item in RailwayCarriageAddFlight)
                {
                    var q = (from a in ctx.TypeRailwayCarriage
                    .Where(a => a.Name == item.Type)
                             select a.NumberOfSeats);
                    amoutSeat += q.FirstOrDefault();
                }
                TBlAmountSeatAddFlight = amoutSeat.ToString();
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<RouteDataGrid> _routeAddFlight;
        public ObservableCollection<RouteDataGrid> RouteAddFlight
        {
            get { return _routeAddFlight; }
            set
            {
                _routeAddFlight = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedTypeRailwayCarriageAddFlight;
        public string SelectedTypeRailwayCarriageAddFlight
        {
            get { return _selectedTypeRailwayCarriageAddFlight; }
            set
            {
                _selectedTypeRailwayCarriageAddFlight = value;
                NotifyPropertyChanged();
            }
        }

        private Train _selectedTrainAddFlight;
        public Train SelectedTrainAddFlight
        {
            get { return _selectedTrainAddFlight; }
            set
            {
                _selectedTrainAddFlight = value;
                TBlNumTrainAddFlight = value.NumTrain.ToString();
                NotifyPropertyChanged();
            }
        }

        private DateTime _departureDateTimeAddFlight;
        public DateTime DepartureDateTimeAddFlight
        {
            get { return _departureDateTimeAddFlight; }
            set
            {
                _departureDateTimeAddFlight = value;
                NotifyPropertyChanged();
            }
        }

        private string _cBAddRouteAddFlight;
        public string CBAddRouteAddFlight
        {
            get { return _cBAddRouteAddFlight; }
            set
            {
                _cBAddRouteAddFlight = value;
                NotifyPropertyChanged();
            }
        }

        private string _tBAddRouteAddFlight;
        public string TBAddRouteAddFlight
        {
            get { return _tBAddRouteAddFlight; }
            set
            {
                _tBAddRouteAddFlight = value;
                NotifyPropertyChanged();
            }

        }

        private string _tBFlightAddFlight;
        public string TBFlightAddFlight
        {
            get { return _tBFlightAddFlight; }
            set
            {
                _tBFlightAddFlight = value;
                NotifyPropertyChanged();
            }

        }

        private string _tBlNumTrainAddFlight = "Поезд №:";
        public string TBlNumTrainAddFlight
        {
            get { return _tBlNumTrainAddFlight; }
            set
            {
                _tBlNumTrainAddFlight = "Поезд №:" + value;
                NotifyPropertyChanged();
            }

        }

        private string _tBlAmountSeatAddFlight = "Мест:";
        public string TBlAmountSeatAddFlight
        {
            get { return _tBlAmountSeatAddFlight; }
            set
            {
                _tBlAmountSeatAddFlight = "Мест: " + value;
                NotifyPropertyChanged();
            }

        }

        private string _tBDateAddFlight;
        public string TBDateAddFlight
        {
            get { return _tBDateAddFlight; }
            set
            {
                if (DateTime.TryParse(value, out _))
                {
                    DateTime dt = DateTime.Parse(value, CultureInfo.GetCultureInfo("en-US"));
                    _tBDateAddFlight = dt.ToShortDateString();
                }
                DateTimeAddFlight = "";
                NotifyPropertyChanged();
            }

        }

        private string _tBTimeAddFlight = "00:00:00";
        public string TBTimeAddFlight
        {
            get { return _tBTimeAddFlight; }
            set
            {
                _tBTimeAddFlight = value;
                DateTimeAddFlight = "";
                NotifyPropertyChanged();
            }

        }

        private string _dAteTimeAddFlight;
        public string DateTimeAddFlight
        {
            get { return _dAteTimeAddFlight; }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(TBDateAddFlight + " " + TBTimeAddFlight, out dt))
                {
                    _dAteTimeAddFlight = TBDateAddFlight + " " + TBTimeAddFlight;
                }
                NotifyPropertyChanged();
            }

        }

        private RelayCommand _addRouteAddFlight;
        public RelayCommand AddRouteAddFlight
        {
            get
            {
                return _addRouteAddFlight ??
                (_addRouteAddFlight = new RelayCommand(obj =>
                {
                    if (RouteAddFlight == null) RouteAddFlight = new ObservableCollection<RouteDataGrid>() { new RouteDataGrid() { NameStation = CBAddRouteAddFlight, Number = 1, StopDuration = Convert.ToInt32(TBAddRouteAddFlight) } };
                    else
                    {
                        ObservableCollection<RouteDataGrid> collectionRoute = new ObservableCollection<RouteDataGrid>();
                        collectionRoute = RouteAddFlight;
                        collectionRoute.Add(new RouteDataGrid() { NameStation = CBAddRouteAddFlight, Number = RouteAddFlight.Count() + 1, StopDuration = Convert.ToInt32(TBAddRouteAddFlight) });
                        RouteAddFlight = collectionRoute;
                    }
                }));
            }
        }

        private RelayCommand _addRailwayCarriageAddFlight;
        public RelayCommand AddRailwayCarriageAddFlight
        {
            get
            {
                return _addRailwayCarriageAddFlight ??
                (_addRailwayCarriageAddFlight = new RelayCommand(obj =>
                {
                    if (RailwayCarriageAddFlight == null) RailwayCarriageAddFlight = new ObservableCollection<RailwayCarriage>() { new RailwayCarriage() { Type = SelectedTypeRailwayCarriageAddFlight, NumRailwayCarriage = 1 } };
                    else
                    {
                        ObservableCollection<RailwayCarriage> collectionRailwayCarriage = new ObservableCollection<RailwayCarriage>();
                        collectionRailwayCarriage = RailwayCarriageAddFlight;
                        collectionRailwayCarriage.Add(new RailwayCarriage() { Type = SelectedTypeRailwayCarriageAddFlight, NumRailwayCarriage = RailwayCarriageAddFlight.Count() + 1 });
                        RailwayCarriageAddFlight = collectionRailwayCarriage;
                    }
                }));
            }
        }

        private RelayCommand _removeRailwayCarriageAddFlight;
        public RelayCommand RemoveRailwayCarriageAddFlight
        {
            get
            {
                return _removeRailwayCarriageAddFlight ??
                (_removeRailwayCarriageAddFlight = new RelayCommand(obj =>
                {
                    ObservableCollection<RailwayCarriage> collectionRailwayCarriage = new ObservableCollection<RailwayCarriage>();
                    collectionRailwayCarriage = RailwayCarriageAddFlight;
                    collectionRailwayCarriage.Remove(collectionRailwayCarriage.Last());
                    RailwayCarriageAddFlight = collectionRailwayCarriage;
                }));
            }
        }

        private RelayCommand _removeRemoteAddFlight;
        public RelayCommand RemoveRouteAddFlight
        {
            get
            {
                return _removeRemoteAddFlight ??
                (_removeRemoteAddFlight = new RelayCommand(obj =>
                {
                    ObservableCollection<RouteDataGrid> collectionRoute = new ObservableCollection<RouteDataGrid>();
                    collectionRoute = RouteAddFlight;
                    collectionRoute.Remove(collectionRoute.Last());
                    RouteAddFlight = collectionRoute;
                }));
            }
        }

        //===Вкладка Поезд

        private string _selectedTypeTrainAddTrain;
        public string SelectedTypeTrainAddTrain
        {
            get { return _selectedTypeTrainAddTrain; }
            set
            {
                _selectedTypeTrainAddTrain = value;
                NotifyPropertyChanged();
            }
        }

        private int _selectedNumberTrainAddTrain;
        public int SelectedNumberTrainAddTrain
        {
            get { return _selectedNumberTrainAddTrain; }
            set
            {
                _selectedNumberTrainAddTrain = value;
                NotifyPropertyChanged();
            }
        }

        private string _tBAddTrain;
        public string TBAddTrain
        {
            get { return _tBAddTrain; }
            set
            {
                _tBAddTrain = value;
                NotifyPropertyChanged();
            }
        }

        private string _cBAddTrain;
        public string CBAddTrain
        {
            get { return _cBAddTrain; }
            set
            {
                _cBAddTrain = value;
                NotifyPropertyChanged();
            }
        }


        private Train _selectedTrainAddTrain;
        public Train SelectedTrainAddTrain
        {
            get { return _selectedTrainAddTrain; }
            set
            {
                if (value == null) return;
                this.SelectedTypeTrainAddTrain = value.Type;
                this.SelectedNumberTrainAddTrain = value.NumTrain;

                _selectedTrainAddTrain = value;
                NotifyPropertyChanged();
            }
        }

        private RelayCommand _addTrainCommand;
        public RelayCommand AddTrainCommand
        {
            get
            {
                return _addTrainCommand ??
                  (_addTrainCommand = new RelayCommand(obj =>
                  {
                      Train train = new Train();
                      train.Type = _cBAddTrain; 
                      train.NumTrain = Convert.ToInt32(_tBAddTrain);
                      ctx.Train.Add(train);
                      ctx.SaveChanges();
                      FillTrain();

                      NotifyPropertyChanged();
                  }));
            }
        }

        private RelayCommand _removeTrainCommand;
        public RelayCommand RemoveTrainCommand
        {
            get
            {
                return _removeTrainCommand ??
                  (_removeTrainCommand = new RelayCommand(obj =>
                  {
                      ctx.Train.Remove(_selectedTrainAddTrain);
                      ctx.SaveChanges();
                      FillTrain();

                      NotifyPropertyChanged();
                  }));
            }
        }

        private RelayCommand _editTrainCommand;
        public RelayCommand EditTrainCommand
        {
            get
            {
                return _editTrainCommand ??
                  (_editTrainCommand = new RelayCommand(obj =>
                  {
                      Train train = ctx.Train.First(a => a.NumTrain == _selectedTrainAddTrain.NumTrain);
                      train.Type = _selectedTypeTrainAddTrain;
                      ctx.SaveChanges();
                      FillTrain();

                      NotifyPropertyChanged();
                  }));
            }
        }

        //===Вкладка Станции
        //-Левая часть вкладки (станции)
        private int _selectedIdStationAddStation;
        public int SelectedIdStationAddStation
        {
            get { return _selectedIdStationAddStation; }
            set
            {
                _selectedIdStationAddStation = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedNameStationAddStation;
        public string SelectedNameStationAddStation
        {
            get { return _selectedNameStationAddStation; }
            set
            {
                _selectedNameStationAddStation = value;
                NotifyPropertyChanged();
            }
        }

        private string _tBAddStation;
        public string TBAddStation
        {
            get { return _tBAddStation; }
            set
            {
                _tBAddStation = value;
                NotifyPropertyChanged();
            }
        }

        private Station _selectedStationAddStation;
        public Station SelectedStationAddStation
        {
            get { return _selectedStationAddStation; }
            set
            {
                if (value == null) return;
                SelectedIdStationAddStation = value.idStation;
                SelectedNameStationAddStation = value.Name;

                List<Distance> distance = value.Distance.ToList().Union(value.Distance1.ToList()).ToList();
                List<DistanceDataGrid> listdistanceDataGrid = new List<DistanceDataGrid>();
                DistanceDataGrid distanceDataGrid = new DistanceDataGrid();
                foreach (var item in distance)
                {
                    if (item.idStation_1 != value.idStation) listdistanceDataGrid.Add(new DistanceDataGrid
                    {
                        idStation_1 = item.idStation_1,
                        idStation_2 = item.idStation_2,
                        Distance = item.Distance1,
                        NameStation = ctx.Station.First(a => a.idStation == item.idStation_1).Name
                    });
                    else if (item.idStation_2 != value.idStation) listdistanceDataGrid.Add(new DistanceDataGrid
                    {
                        idStation_1 = item.idStation_1,
                        idStation_2 = item.idStation_2,
                        Distance = item.Distance1,
                        NameStation = ctx.Station.First(a => a.idStation == item.idStation_2).Name
                    });

                }
                SelectedStationDistanceAddStation = listdistanceDataGrid;

                _selectedStationAddStation = value;
                NotifyPropertyChanged();
            }
        }

        private List<DistanceDataGrid> _selectedStationDistanceAddStation;
        public List<DistanceDataGrid> SelectedStationDistanceAddStation
        {
            get { return _selectedStationDistanceAddStation; }
            set
            {
                _selectedStationDistanceAddStation = value;
                NotifyPropertyChanged();
            }
        }

        private RelayCommand _addStationCommand;
        public RelayCommand AddStationCommand
        {
            get
            {
                return _addStationCommand ??
                  (_addStationCommand = new RelayCommand(obj =>
                  {
                      Station station = new Station();
                      station.Name = _tBAddStation;
                      ctx.Station.Add(station);
                      ctx.SaveChanges();
                      FillStation();
                      FillStationName();

                      NotifyPropertyChanged();
                  }));
            }
        }

        private RelayCommand _removeStationCommand;
        public RelayCommand RemoveStationCommand
        {
            get
            {
                return _removeStationCommand ??
                  (_removeStationCommand = new RelayCommand(obj =>
                  {
                      ctx.Station.Remove(_selectedStationAddStation);
                      ctx.SaveChanges();
                      FillStation();

                      NotifyPropertyChanged();
                  }));
            }
        }

        private RelayCommand _editStationCommand;
        public RelayCommand EditStationCommand
        {
            get
            {
                return _editStationCommand ??
                  (_editStationCommand = new RelayCommand(obj =>
                  {
                      Station station = ctx.Station.First(a => a.idStation == _selectedStationAddStation.idStation);
                      station.Name = _selectedNameStationAddStation;
                      ctx.SaveChanges();
                      FillStation();

                      NotifyPropertyChanged();
                  }));
            }
        }
        //-Правая часть вкладки (соседние станции)

        private string _cBAddDistatseStation;
        public string CBAddDistatseStation
        {
            get { return _cBAddDistatseStation; }
            set
            {
                _cBAddDistatseStation = value;
                NotifyPropertyChanged();
            }
        }

        private int _tBAddDistanseStation;
        public int TBAddDistatseStation
        {
            get { return _tBAddDistanseStation; }
            set
            {
                _tBAddDistanseStation = Convert.ToInt32(value);
                NotifyPropertyChanged();
            }
        }

        private string _tBEditDistanseStation;
        public string TBEditDistatseStation
        {
            get { return _tBEditDistanseStation; }
            set
            {
                _tBEditDistanseStation = value;
                NotifyPropertyChanged();
            }
        }

        private DistanceDataGrid _selectedDistanceAddStation;
        public DistanceDataGrid SelectedDistanceAddStation
        {
            get { return _selectedDistanceAddStation; }
            set
            {
                if (value == null) return;
                _selectedDistanceAddStation = value;
                TBEditDistatseStation = _selectedDistanceAddStation.Distance.ToString();
                NotifyPropertyChanged();
            }
        }

        private RelayCommand _addStationDistanceCommand;
        public RelayCommand AddStationDistanceCommand
        {
            get
            {
                return _addStationDistanceCommand ??
                  (_addStationDistanceCommand = new RelayCommand(obj =>
                  {
                      Distance distance = new Distance();
                      distance.idStation_1 = _selectedIdStationAddStation;
                      distance.idStation_2 = ctx.Station.First(a => a.Name == _cBAddDistatseStation).idStation;
                      distance.Distance1 = _tBAddDistanseStation;
                      ctx.Distance.Add(distance);
                      ctx.SaveChanges();
                      SelectedStationAddStation = SelectedStationAddStation;

                      NotifyPropertyChanged();
                  }));
            }
        }

        private RelayCommand _editStationDistanceCommand;
        public RelayCommand EditStationDistanceCommand
        {
            get
            {
                return _editStationDistanceCommand ??
                  (_editStationDistanceCommand = new RelayCommand(obj =>
                  {
                      Distance distance = ctx.Distance.First(a => a.idStation_1 == SelectedDistanceAddStation.idStation_1 && a.idStation_2 == SelectedDistanceAddStation.idStation_2);
                      distance.Distance1 = Convert.ToInt32(_tBEditDistanseStation);
                      ctx.SaveChanges();
                      SelectedStationAddStation = SelectedStationAddStation;

                      NotifyPropertyChanged();
                  }));
            }
        }

        private RelayCommand _removeStationDistanceCommand;
        public RelayCommand RemoveStationDistanceCommand
        {
            get
            {
                return _removeStationDistanceCommand ??
                  (_removeStationDistanceCommand = new RelayCommand(obj =>
                  {
                      Distance distance = ctx.Distance.First(a => a.idStation_1 == SelectedDistanceAddStation.idStation_1 && a.idStation_2 == SelectedDistanceAddStation.idStation_2);
                      ctx.Distance.Remove(distance);
                      ctx.SaveChanges();
                      SelectedStationAddStation = SelectedStationAddStation;

                      NotifyPropertyChanged();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
