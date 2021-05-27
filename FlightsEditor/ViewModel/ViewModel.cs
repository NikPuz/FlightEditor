using FlightsEditor.Model;
using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
            FillTypeTrain();
            FillStation();
        }

        private void FillTrain()
        {
            var q = (from a in ctx.Train
                     select a).ToList();
            this.Train = q;
        }

        private void FillTypeTrain()
        {
            var q = (from a in ctx.TypeTrain
                     .Select(a => a.Name)
                     select a).ToList();
            this.TypeTrain = q;
        }

        private void FillStation()
        {
            var q = (from a in ctx.Station
                     select a).ToList();
            this.Station = q;
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

        private List<string> _typeTrain;
        public List<string> TypeTrain
        {
            get { return _typeTrain; }
            set
            {
                _typeTrain = value;
                NotifyPropertyChanged();
            }
        }

        //---Вкладка Поезд

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

        //---Вкладка Станции

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
                this.SelectedIdStationAddStation = value.idStation;
                this.SelectedNameStationAddStation = value.Name;
                this.SelectedStationDistanceAddStation = value.Distance.ToList();

                _selectedStationAddStation = value;
                NotifyPropertyChanged();
            }
        }

        private List<Distance> _selectedStationDistanceAddStation;
        public List<Distance> SelectedStationDistanceAddStation
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
