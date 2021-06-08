using FlightsEditor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

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
            FillFlight();
        }

        private void FillTrain()
        {
            var q = (from a in ctx.Train
                     select a).ToList();
            Train = q;
        }
        private void FillFlight()
        {
            var q = (from a in ctx.Flight
                     select a).ToList();
            Flight = q;
        }

        private void FillTypeRailwayCarriageName()
        {
            var q = (from a in ctx.TypeRailwayCarriage
                     .Select(a => a.Name)
                     select a).ToList();
            TypeRailwayCarriageName = q;
        }
        private void FillTypeTrainName()
        {
            var q = (from a in ctx.TypeTrain
                     .Select(a => a.Name)
                     select a).ToList();
            TypeTrainName = q;
        }

        private void FillStation()
        {
            var q = (from a in ctx.Station
                     select a).ToList();
            Station = q;
        }

        private void FillStationName()
        {
            var q = (from a in ctx.Station
                     .Select(a => a.Name)
                     select a).ToList();
            StationName = q;
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

        private List<Flight> _flight;
        public List<Flight> Flight
        {
            get { return _flight; }
            set
            {
                _flight = value;
                var q = (from a in value
                         .Select(a => a.NumFlight)
                                      select a).ToList();
                CBFlightEditFlight = q;

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

        private string _tBAddRouteAddFlight = "0";
        public string TBAddRouteAddFlight
        {
            get { return _tBAddRouteAddFlight; }
            set
            {
                if (int.TryParse(value, out _))
                {
                    _tBAddRouteAddFlight = value;
                }
                NotifyPropertyChanged();
            }

        }

        private string _tBFlightAddFlight;
        public string TBFlightAddFlight
        {
            get { return _tBFlightAddFlight; }
            set
            {
                if (int.TryParse(value, out _) && value != "0")
                {
                    _tBFlightAddFlight = value;
                }
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

        private DateTime _tBDateAddFlight = DateTime.Now.Date;
        public DateTime TBDateAddFlight
        {
            get { return _tBDateAddFlight; }
            set
            {
                _tBDateAddFlight = value;
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
                if (DateTime.TryParse(value, out _))
                {
                    _tBTimeAddFlight = value;
                }
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
                if (DateTime.TryParse(TBDateAddFlight.ToShortDateString() + " " + TBTimeAddFlight, out dt))
                {
                    _dAteTimeAddFlight = TBDateAddFlight.ToShortDateString() + " " + TBTimeAddFlight;
                }
                NotifyPropertyChanged();
            }

        }

        private RelayCommand _addFlightAddFlight;
        public RelayCommand AddFlightAddFlight
        {
            get
            {
                return _addFlightAddFlight ??
                (_addFlightAddFlight = new RelayCommand(obj =>
                {
                    if (ctx.Flight.Any(a => a.NumFlight.ToString() == TBFlightAddFlight)) { MessageBox.Show("Рейс с этим номером уже существует.", "Ошибка!"); return; }
                    if (TBFlightAddFlight == null) { MessageBox.Show("Номер рейса не задан.", "Ошибка!"); return; }
                    if (TBDateAddFlight == null) { MessageBox.Show("Дата отправления не задана.", "Обшибка!"); return; }
                    if (RailwayCarriageAddFlight == null || RailwayCarriageAddFlight.Count == 0) { MessageBox.Show("Состав не задан.", "Обшибка!"); return; }
                    if (RouteAddFlight == null || RouteAddFlight.Count == 0) { MessageBox.Show("Маршрут не задан.", "Обшибка!"); return; }
                    if (SelectedTrainAddFlight == null) { MessageBox.Show("Поезд не выбран.", "Обшибка!"); return; }

                    Flight flight = new Flight();
                    Seat seat = new Seat();
                    Route route = new Route();
                    flight.NumFlight = Convert.ToInt32(TBFlightAddFlight);
                    flight.NumTrain = SelectedTrainAddFlight.NumTrain;
                    flight.DepartureDateTime = DateTime.Parse(DateTimeAddFlight);
                    ctx.Flight.Add(flight);

                    foreach (var carriage in RailwayCarriageAddFlight)
                    {
                        carriage.NumFlight = flight.NumFlight;
                        ctx.RailwayCarriage.Add(new RailwayCarriage { NumFlight = carriage.NumFlight, NumRailwayCarriage = carriage.NumRailwayCarriage, Type = carriage.Type });
                        switch (carriage.Type)
                        {
                            case "Плацкарт":
                                for (int num = 1; num < 54; num++)
                                {
                                    seat.Upper = false;
                                    seat.Side = false;
                                    seat.NumFlight = flight.NumFlight;
                                    seat.NumRailwayCarriage = carriage.NumRailwayCarriage;
                                    seat.NumSeat = num;
                                    if (num % 2 == 0) seat.Upper = true;
                                    if (num > 36) seat.Side = true;
                                    ctx.Seat.Add(new Seat { NumSeat = seat.NumSeat,
                                        NumRailwayCarriage = seat.NumRailwayCarriage,
                                        NumFlight = seat.NumFlight, 
                                        Upper = seat.Upper, 
                                        Side = seat.Side});
                                }
                                break;
                            case "Купе":
                                seat.Side = false;
                                for (int num = 1; num < 37; num++)
                                {
                                    seat.Upper = false;
                                    seat.NumFlight = flight.NumFlight;
                                    seat.NumRailwayCarriage = carriage.NumRailwayCarriage;
                                    seat.NumSeat = num;
                                    if (num % 2 == 0) seat.Upper = true;
                                    ctx.Seat.Add(new Seat
                                    {
                                        NumSeat = seat.NumSeat,
                                        NumRailwayCarriage = seat.NumRailwayCarriage,
                                        NumFlight = seat.NumFlight,
                                        Upper = seat.Upper,
                                        Side = seat.Side
                                    });
                                }
                                break;
                            case "Люкс":
                                seat.Upper = false;
                                seat.Side = false;
                                for (int num = 1; num < 19; num++)
                                {
                                    seat.NumFlight = flight.NumFlight;
                                    seat.NumRailwayCarriage = carriage.NumRailwayCarriage;
                                    seat.NumSeat = num;
                                    ctx.Seat.Add(new Seat
                                    {
                                        NumSeat = seat.NumSeat,
                                        NumRailwayCarriage = seat.NumRailwayCarriage,
                                        NumFlight = seat.NumFlight,
                                        Upper = seat.Upper,
                                        Side = seat.Side
                                    });
                                }
                                break;
                            case "Сидячий":
                                seat.Upper = false;
                                seat.Side = false;
                                for (int num = 1; num < 67; num++)
                                {
                                    seat.NumFlight = flight.NumFlight;
                                    seat.NumRailwayCarriage = carriage.NumRailwayCarriage;
                                    seat.NumSeat = num;
                                    ctx.Seat.Add(new Seat
                                    {
                                        NumSeat = seat.NumSeat,
                                        NumRailwayCarriage = seat.NumRailwayCarriage,
                                        NumFlight = seat.NumFlight,
                                        Upper = seat.Upper,
                                        Side = seat.Side
                                    });
                                }
                                break;
                        }
                    }
                    foreach (var routeAddFlight in RouteAddFlight)
                    {
                        route.NumFlight = flight.NumFlight;
                        route.NumberInRoute = routeAddFlight.Number;
                        route.StopDuration = TimeSpan.FromMinutes(routeAddFlight.StopDuration);
                        var q = (from a in ctx.Station
                                 .Where(a => a.Name == routeAddFlight.NameStation)
                                 select a.idStation);
                        route.idStation = q.FirstOrDefault();
                        ctx.Route.Add(new Route { idStation = route.idStation, NumFlight = route.NumFlight, NumberInRoute = route.NumberInRoute, StopDuration = route.StopDuration });
                    }
                    ctx.SaveChanges();
                    MessageBox.Show("Рейс успешно добавлен", "Выполнено!");
                    FillFlight();
                }));
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

        private RelayCommand _removeRouteAddFlight;
        public RelayCommand RemoveRouteAddFlight
        {
            get
            {
                return _removeRouteAddFlight ??
                (_removeRouteAddFlight = new RelayCommand(obj =>
                {
                    ObservableCollection<RouteDataGrid> collectionRoute = new ObservableCollection<RouteDataGrid>();
                    collectionRoute = RouteAddFlight;
                    collectionRoute.Remove(collectionRoute.Last());
                    RouteAddFlight = collectionRoute;
                }));
            }
        }

        //===Вкладка Изменить рейс

        private List<int> _cBFlightEditFlight;
        public List<int> CBFlightEditFlight
        {
            get { return _cBFlightEditFlight; }
            set
            {
                _cBFlightEditFlight = value;

                NotifyPropertyChanged();
            }
        }

        private int _cBSelectedFlightEditFlight;
        public int CBSelectedFlightEditFlight
        {
            get { return _cBSelectedFlightEditFlight; }
            set
            {
                _cBSelectedFlightEditFlight = value;
                if (value == 0) return;
                Flight flight = new Flight();
                flight = (from a in ctx.Flight
                          .Where(a => a.NumFlight == value)
                          select a).First();
                TBTimeEditFlight = flight.DepartureDateTime.ToShortTimeString();
                SelectedTrainEditFlight = flight.Train;
                RailwayCarriageEditFlight = new ObservableCollection<RailwayCarriage>(flight.RailwayCarriage);
                RouteEditFlight.Clear();
                foreach (var item in flight.Route)
                {
                    RouteEditFlight.Add(new RouteDataGrid
                    {
                        NameStation = (from a in ctx.Station
                                             .Where(a => a.idStation == item.idStation)
                                       select a.Name).First(),
                        Number = item.NumberInRoute,
                        StopDuration = item.StopDuration.Value.Minutes
                    });
                }
                int amoutSeat = 0;
                foreach (var item in RailwayCarriageEditFlight)
                {
                    var q = (from a in ctx.TypeRailwayCarriage
                    .Where(a => a.Name == item.Type)
                             select a.NumberOfSeats);
                    amoutSeat += q.FirstOrDefault();
                }
                TBlAmountSeatEditFlight = amoutSeat.ToString();
                TBDateEditFlight = flight.DepartureDateTime.Date;
                NotifyPropertyChanged();
            }
        }

        private string _cBSelectedStationNameEditFlight;
        public string CBSelectedStationNameEditFlight
        {
            get { return _cBSelectedStationNameEditFlight; }
            set
            {
                _cBSelectedStationNameEditFlight = value;

                NotifyPropertyChanged();
            }
        }

        private DateTime _tBDateEditFlight = DateTime.Now.Date;
        public DateTime TBDateEditFlight
        {
            get { return _tBDateEditFlight; }
            set
            {
                _tBDateEditFlight = value;
                DateTimeEditFlight = "";
                NotifyPropertyChanged();
            }

        }

        private string _tBTimeEditFlight = "00:00:00";
        public string TBTimeEditFlight
        {
            get { return _tBTimeEditFlight; }
            set
            {
                if (DateTime.TryParse(value, out _))
                {
                    _tBTimeEditFlight = value;
                    DateTimeEditFlight = "";
                }
                NotifyPropertyChanged();
            }

        }

        private ObservableCollection<RailwayCarriage> _railwayCarriageEditFlight = new ObservableCollection<RailwayCarriage>();
        public ObservableCollection<RailwayCarriage> RailwayCarriageEditFlight
        {
            get { return _railwayCarriageEditFlight; }
            set
            {
                _railwayCarriageEditFlight = value;
                int amoutSeat = 0;
                foreach (var item in RailwayCarriageEditFlight)
                {
                    var q = (from a in ctx.TypeRailwayCarriage
                    .Where(a => a.Name == item.Type)
                             select a.NumberOfSeats);
                    amoutSeat += q.FirstOrDefault();
                }
                TBlAmountSeatEditFlight = amoutSeat.ToString();
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<RouteDataGrid> _routeEditFlight = new ObservableCollection<RouteDataGrid>();
        public ObservableCollection<RouteDataGrid> RouteEditFlight
        {
            get { return _routeEditFlight; }
            set
            {
                _routeEditFlight = value;
                NotifyPropertyChanged();
            }
        }

        private Train _selectedTrainEditFlight;
        public Train SelectedTrainEditFlight
        {
            get { return _selectedTrainEditFlight; }
            set
            {
                if (CBSelectedFlightEditFlight == 0) return;
                Flight flight = ctx.Flight.Where(a => a.NumFlight == CBSelectedFlightEditFlight).First();
                flight.Train = value;
                ctx.SaveChanges();
                _selectedTrainEditFlight = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _departureDateTimeEditFlight;
        public DateTime DepartureDateTimeEditFlight
        {
            get { return _departureDateTimeEditFlight; }
            set
            {
                _departureDateTimeEditFlight = value;
                NotifyPropertyChanged();
            }
        }

        private string _tBAddRouteEditFlight = "0";
        public string TBAddRouteEditFlight
        {
            get { return _tBAddRouteEditFlight; }
            set
            {
                if (int.TryParse(value, out _))
                {
                    _tBAddRouteEditFlight = value;
                }
                NotifyPropertyChanged();
            }

        }

        private string _tBFlightEditFlight;
        public string TBFlightEditFlight
        {
            get { return _tBFlightEditFlight; }
            set
            {
                if (int.TryParse(value, out _))
                {
                    _tBFlightEditFlight = value;
                }
                NotifyPropertyChanged();
            }

        }

        private string _tBlNumTrainEditFlight = "Поезд №:";
        public string TBlNumTrainEditFlight
        {
            get { return _tBlNumTrainEditFlight; }
            set
            {
                _tBlNumTrainEditFlight = "Поезд №:" + value;
                NotifyPropertyChanged();
            }

        }

        private string _tBlAmountSeatEditFlight = "Мест:";
        public string TBlAmountSeatEditFlight
        {
            get { return _tBlAmountSeatEditFlight; }
            set
            {
                _tBlAmountSeatEditFlight = "Мест: " + value;
                NotifyPropertyChanged();
            }

        }

        private string _DateTimeEditFlight;
        public string DateTimeEditFlight
        {
            get { return _DateTimeEditFlight; }
            set
            {
                if (DateTime.TryParse(TBDateEditFlight.ToShortDateString() + " " + TBTimeEditFlight, out _))
                {
                    _DateTimeEditFlight = TBDateEditFlight.ToShortDateString() + " " + TBTimeEditFlight;
                }
                NotifyPropertyChanged();
            }

        }

        private string _selectedTypeRailwayCarriageEditFlight;
        public string SelectedTypeRailwayCarriageEditFlight
        {
            get { return _selectedTypeRailwayCarriageEditFlight; }
            set
            {
                _selectedTypeRailwayCarriageEditFlight = value;
                NotifyPropertyChanged();
            }
        }

        private RelayCommand _editDateTimeEditFlight;
        public RelayCommand EditDateTimeEditFlight
        {
            get
            {
                return _editDateTimeEditFlight ??
                (_editDateTimeEditFlight = new RelayCommand(obj =>
                {
                    if (CBSelectedFlightEditFlight == 0) return; 
                    Flight flight = ctx.Flight.Where(a => a.NumFlight == CBSelectedFlightEditFlight).First();
                    flight.DepartureDateTime = DateTime.Parse(DateTimeEditFlight);
                    ctx.SaveChanges();
                }));
            }
        }

        private RelayCommand _removeFlightEditFlight;
        public RelayCommand RemoveFlightEditFlight
        {
            get
            {
                return _removeFlightEditFlight ??
                (_removeFlightEditFlight = new RelayCommand(obj =>
                {
                    if (!(CBSelectedFlightEditFlight > 0)) { MessageBox.Show("Номер рейса не задан.", "Ошибка!"); return; }

                    ctx.Route.RemoveRange(ctx.Route.Where(a => a.NumFlight == CBSelectedFlightEditFlight));
                    ctx.Seat.RemoveRange(ctx.Seat.Where(a => a.NumFlight == CBSelectedFlightEditFlight));
                    ctx.RailwayCarriage.RemoveRange(ctx.RailwayCarriage.Where(a => a.NumFlight == CBSelectedFlightEditFlight));
                    ctx.Flight.RemoveRange(ctx.Flight.Where(a => a.NumFlight == CBSelectedFlightEditFlight));

                    ctx.SaveChanges();
                    FillFlight();
                    CBSelectedFlightEditFlight = 0;
                    SelectedTrainEditFlight = new Train();
                    TBlAmountSeatEditFlight = "";
                    RailwayCarriageEditFlight.Clear();
                    RouteEditFlight.Clear();
                    TBTimeEditFlight = "00:00:00";
                    TBDateEditFlight = DateTime.Now.Date;
                }));
            }
        }

        private RelayCommand _addRailwayCarriageEditFlight;
        public RelayCommand AddRailwayCarriageEditFlight
        {
            get
            {
                return _addRailwayCarriageEditFlight ??
                (_addRailwayCarriageEditFlight = new RelayCommand(obj =>
                {
                    if (CBSelectedFlightEditFlight == 0 && SelectedTypeRailwayCarriageEditFlight != "") return;
                    Flight flight = ctx.Flight.Where(a => a.NumFlight == CBSelectedFlightEditFlight).First();
                    int numRailwayCarriage;

                    ObservableCollection<RailwayCarriage> collectionRailwayCarriage = new ObservableCollection<RailwayCarriage>();
                    numRailwayCarriage = RailwayCarriageEditFlight.Count() + 1;
                    collectionRailwayCarriage = RailwayCarriageEditFlight;
                    collectionRailwayCarriage.Add(new RailwayCarriage() { Type = SelectedTypeRailwayCarriageEditFlight, NumRailwayCarriage = numRailwayCarriage, NumFlight = CBSelectedFlightEditFlight });
                    flight.RailwayCarriage.Add(new RailwayCarriage() { Type = SelectedTypeRailwayCarriageEditFlight, NumRailwayCarriage = numRailwayCarriage, NumFlight = CBSelectedFlightEditFlight });
                    RailwayCarriageEditFlight = collectionRailwayCarriage;

                    Seat seat = new Seat();
                    switch (SelectedTypeRailwayCarriageEditFlight)
                    {
                        case "Плацкарт":
                            for (int num = 1; num < 54; num++)
                            {
                                seat.Upper = false;
                                seat.Side = false;
                                seat.NumFlight = flight.NumFlight;
                                seat.NumRailwayCarriage = numRailwayCarriage;
                                seat.NumSeat = num;
                                if (num % 2 == 0) seat.Upper = true;
                                if (num > 36) seat.Side = true;
                                ctx.Seat.Add(new Seat
                                {
                                    NumSeat = seat.NumSeat,
                                    NumRailwayCarriage = seat.NumRailwayCarriage,
                                    NumFlight = seat.NumFlight,
                                    Upper = seat.Upper,
                                    Side = seat.Side
                                });
                            }
                            break;
                        case "Купе":
                            seat.Side = false;
                            for (int num = 1; num < 37; num++)
                            {
                                seat.Upper = false;
                                seat.NumFlight = flight.NumFlight;
                                seat.NumRailwayCarriage = numRailwayCarriage;
                                seat.NumSeat = num;
                                if (num % 2 == 0) seat.Upper = true;
                                ctx.Seat.Add(new Seat
                                {
                                    NumSeat = seat.NumSeat,
                                    NumRailwayCarriage = seat.NumRailwayCarriage,
                                    NumFlight = seat.NumFlight,
                                    Upper = seat.Upper,
                                    Side = seat.Side
                                });
                            }
                            break;
                        case "Люкс":
                            seat.Upper = false;
                            seat.Side = false;
                            for (int num = 1; num < 19; num++)
                            {
                                seat.NumFlight = flight.NumFlight;
                                seat.NumRailwayCarriage = numRailwayCarriage;
                                seat.NumSeat = num;
                                ctx.Seat.Add(new Seat
                                {
                                    NumSeat = seat.NumSeat,
                                    NumRailwayCarriage = seat.NumRailwayCarriage,
                                    NumFlight = seat.NumFlight,
                                    Upper = seat.Upper,
                                    Side = seat.Side
                                });
                            }
                            break;
                        case "Сидячий":
                            seat.Upper = false;
                            seat.Side = false;
                            for (int num = 1; num < 67; num++)
                            {
                                seat.NumFlight = flight.NumFlight;
                                seat.NumRailwayCarriage = RailwayCarriageEditFlight.Count();
                                seat.NumSeat = num;
                                ctx.Seat.Add(new Seat
                                {
                                    NumSeat = seat.NumSeat,
                                    NumRailwayCarriage = seat.NumRailwayCarriage,
                                    NumFlight = seat.NumFlight,
                                    Upper = seat.Upper,
                                    Side = seat.Side
                                });
                            }
                            break;
                    }
                    ctx.SaveChanges();
                }));
            }
        }

        private RelayCommand _removeRailwayCarriageEditFlight;
        public RelayCommand RemoveRailwayCarriageEditFlight
        {
            get
            {
                return _removeRailwayCarriageEditFlight ??
                (_removeRailwayCarriageEditFlight = new RelayCommand(obj =>
                {
                    if (CBSelectedFlightEditFlight == 0 || RailwayCarriageEditFlight.Count() == 0) return;

                    Flight flight = ctx.Flight.Where(a => a.NumFlight == CBSelectedFlightEditFlight).First();
                    RailwayCarriage railwayCarriage = new RailwayCarriage();
                    ObservableCollection<RailwayCarriage> collectionRailwayCarriage = new ObservableCollection<RailwayCarriage>();

                    collectionRailwayCarriage = RailwayCarriageEditFlight;
                    railwayCarriage = collectionRailwayCarriage.Last();
                    collectionRailwayCarriage.Remove(railwayCarriage);
                    RailwayCarriageEditFlight = collectionRailwayCarriage;
                    ctx.Seat.RemoveRange(ctx.Seat.Where(a => a.NumRailwayCarriage == railwayCarriage.NumRailwayCarriage && a.NumFlight == railwayCarriage.NumFlight));
                    flight.RailwayCarriage.Remove(railwayCarriage);
                    ctx.SaveChanges();
                }));
            }
        }

        private RelayCommand _addRouteEditFlight;
        public RelayCommand AddRouteEditFlight
        {
            get
            {
                return _addRouteEditFlight ??
                (_addRouteEditFlight = new RelayCommand(obj =>
                {
                    if (CBSelectedFlightEditFlight == 0 || CBSelectedStationNameEditFlight == "") return;
                    Flight flight = ctx.Flight.Where(a => a.NumFlight == CBSelectedFlightEditFlight).First();

                    RouteEditFlight.Add(new RouteDataGrid() { NameStation = CBSelectedStationNameEditFlight, Number = RouteEditFlight.Count() + 1, StopDuration = Convert.ToInt32(TBAddRouteEditFlight) });

                    int idStation = ctx.Station.Where(a => a.Name == CBSelectedStationNameEditFlight).First().idStation;
                    flight.Route.Add(new Route() { idStation = idStation, NumFlight = CBSelectedFlightEditFlight, NumberInRoute = RouteEditFlight.Count() + 1, StopDuration = TimeSpan.FromMinutes(Convert.ToInt32(TBAddRouteEditFlight)) });
                    ctx.SaveChanges();
                }));
            }
        }

        private RelayCommand _removeRouteEditFlight;
        public RelayCommand RemoveRouteEditFlight
        {
            get
            {
                return _removeRouteEditFlight ??
                (_removeRouteEditFlight = new RelayCommand(obj =>
                {
                    if (CBSelectedFlightEditFlight == 0 || CBSelectedStationNameEditFlight == "") return;
                    Flight flight = ctx.Flight.Where(a => a.NumFlight == CBSelectedFlightEditFlight).First();

                    RouteDataGrid routeDataGrid = RouteEditFlight.Last();
                    RouteEditFlight.Remove(routeDataGrid);

                    int idStation = ctx.Station.Where(a => a.Name == routeDataGrid.NameStation).First().idStation;
                    Route route = ctx.Route.Where(a => a.idStation == idStation && a.NumFlight == CBSelectedFlightEditFlight).First();
                    flight.Route.Remove(route);
                    ctx.SaveChanges();
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

        private string _selectedNumberTrainAddTrain = "Номер:";
        public string SelectedNumberTrainAddTrain
        {
            get { return _selectedNumberTrainAddTrain; }
            set
            {
                _selectedNumberTrainAddTrain = "Номер: " + value;
                NotifyPropertyChanged();
            }
        }

        private string _tBAddTrain = "0";
        public string TBAddTrain
        {
            get { return _tBAddTrain; }
            set
            {
                if (int.TryParse(value, out _))
                {
                    
                    if (!(from a in ctx.Train
                     .Select(a => a.NumTrain)
                             select a).ToList()
                             .Contains(Convert.ToInt32(value)))
                    _tBAddTrain = value;
                }
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
                SelectedTypeTrainAddTrain = value.Type;
                SelectedNumberTrainAddTrain = value.NumTrain.ToString();

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

        private string _tBAddDistanceStation = "0";
        public string TBAddDistanceStation
        {
            get { return _tBAddDistanceStation; }
            set
            {
                if (int.TryParse(value, out _))
                {
                    _tBAddDistanceStation = value;
                }
                NotifyPropertyChanged();
            }
        }

        private string _tBEditDistanceStation = "0";
        public string TBEditDistanceStation
        {
            get { return _tBEditDistanceStation; }
            set
            {
                if (int.TryParse(value, out _))
                {
                    _tBEditDistanceStation = value;
                }
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
                TBEditDistanceStation = _selectedDistanceAddStation.Distance.ToString();
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
                      distance.idStation_1 = SelectedIdStationAddStation;
                      distance.idStation_2 = ctx.Station.First(a => a.Name == CBAddDistatseStation).idStation;
                      distance.Distance1 = Convert.ToInt32(TBAddDistanceStation);
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
                      distance.Distance1 = Convert.ToInt32(_tBEditDistanceStation);
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
