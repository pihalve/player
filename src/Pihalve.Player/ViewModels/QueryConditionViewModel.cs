using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Pihalve.Player.Annotations;
using Pihalve.Player.Library.Model;

namespace Pihalve.Player.ViewModels
{
    public class QueryConditionViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FieldViewModel> _fields;
        private ObservableCollection<OperatorViewModel> _operators;

        private FieldViewModel _field;
        private OperatorViewModel _operator;
        private string _value;

        public QueryConditionViewModel()
        {
            _field = Fields.First();
            _operator = Operators.First();
            _value = string.Empty;
        }

        public ObservableCollection<FieldViewModel> Fields
        {
            get
            {
                return _fields ?? (_fields = new ObservableCollection<FieldViewModel>(
                    FieldViewModel.Translations.Select(translation => new FieldViewModel(translation.Key)).OrderBy(x => x.DisplayName)));
            }
        }

        public ObservableCollection<OperatorViewModel> Operators
        {
            get { return _operators ?? (_operators = new ObservableCollection<OperatorViewModel>(
                Enum.GetValues(typeof(Operator)).Cast<Operator>().Select(opr => new OperatorViewModel(opr)))); }
        }

        public FieldViewModel Field
        {
            get { return _field; }
            set
            {
                if (value != null && !_field.Equals(value))
                {
                    _field = value;
                    OnPropertyChanged(nameof(Field));
                }
            }
        }

        public OperatorViewModel Operator
        {
            get { return _operator; }
            set
            {
                if (value != null && !_operator.Equals(value))
                {
                    _operator = value;
                    OnPropertyChanged(nameof(Operator));
                }
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                if (value != null && !_value.Equals(value))
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
