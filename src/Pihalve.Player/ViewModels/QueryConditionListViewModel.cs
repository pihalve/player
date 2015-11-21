using System.Collections.ObjectModel;

namespace Pihalve.Player.ViewModels
{
    public class QueryConditionListViewModel
    {
        public ObservableCollection<QueryConditionViewModel> Conditions => new ObservableCollection<QueryConditionViewModel>();
    }
}
