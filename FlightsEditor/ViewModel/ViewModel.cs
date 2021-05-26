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

        private List<Train> _train;
        public List<Train> Train
        {
            get
            { return _train; }
            set
            {
                _train = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> _typeTrain;
        public List<string> TypeTrain
        {
            get
            { return _typeTrain; }
            set
            {
                _typeTrain = value;
                NotifyPropertyChanged();
            }
        }


        private Train _selectedTrain;
        public Train SelectedTrain
        {
            get
            { return _selectedTrain; }
            set
            {
                if (value == null) return;
                this.SelectedTypeTrain = value.Type;
                this.SelectedNumberTrain = value.NumTrain;

                _selectedTrain = value;
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
                      ctx.Train.Remove(_selectedTrain);
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
                      Train train = ctx.Train.First(a => a.NumTrain == _selectedTrain.NumTrain);
                      train.Type = _selectedTypeTrain;
                      ctx.SaveChanges();
                      FillTrain();

                      NotifyPropertyChanged();
                  }));
            }
        }

        private string _selectedTypeTrain;
        public string SelectedTypeTrain
        {
            get
            { return _selectedTypeTrain; }
            set
            {
                _selectedTypeTrain = value;
                NotifyPropertyChanged();
            }
        }

        private int _selectedNumberTrain;
        public int SelectedNumberTrain
        {
            get
            { return _selectedNumberTrain; }
            set
            {
                _selectedNumberTrain = value;
                NotifyPropertyChanged();
            }
        }

        private string _tBAddTrain;
        public string TBAddTrain
        {
            get
            { return _tBAddTrain; }
            set
            {
                _tBAddTrain = value;
                NotifyPropertyChanged();
            }
        }

        private string _cBAddTrain;
        public string CBAddTrain
        {
            get
            { return _cBAddTrain; }
            set
            {
                _cBAddTrain = value;
                NotifyPropertyChanged();
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
