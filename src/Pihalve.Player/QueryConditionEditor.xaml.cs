using System;
using System.Windows;
using System.Windows.Controls;
using Pihalve.Player.ViewModels;

namespace Pihalve.Player
{
    /// <summary>
    /// Interaction logic for QueryConditionEditor.xaml
    /// </summary>
    public partial class QueryConditionEditor : UserControl
    {
        public event EventHandler ConditionRemove;
        public event EventHandler ConditionAdd;

        public QueryConditionEditor()
        {
            InitializeComponent();

            DataContext = new QueryConditionViewModel();
        }

        public QueryConditionViewModel Model => (QueryConditionViewModel)DataContext;

        private void RemoveCondition_OnClick(object sender, RoutedEventArgs e)
        {
            OnConditionRemove();
        }

        private void AddCondition_OnClick(object sender, RoutedEventArgs e)
        {
            OnConditionAdd();
        }

        protected virtual void OnConditionRemove()
        {
            ConditionRemove?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnConditionAdd()
        {
            ConditionAdd?.Invoke(this, EventArgs.Empty);
        }
    }
}
