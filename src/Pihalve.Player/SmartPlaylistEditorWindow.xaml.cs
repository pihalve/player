using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Pihalve.Player.Library.Model;
using Pihalve.Player.Utilities;

namespace Pihalve.Player
{
    /// <summary>
    /// Interaction logic for SmartPlaylistEditorWindow.xaml
    /// </summary>
    public partial class SmartPlaylistEditorWindow : Window
    {
        public ObservableCollection<QueryConditionEditor> ConditionEditors { get; set; }

        public SmartPlaylist SmartPlaylist { get; set; } = new SmartPlaylist { Id = Guid.NewGuid() };

        public SmartPlaylistEditorWindow()
        {
            ConditionEditors = new ObservableCollection<QueryConditionEditor>();
            ConditionEditors.Add(CreateConditionEditor());

            InitializeComponent();
        }

        private void ConditionEditor_ConditionRemove(object sender, EventArgs e)
        {
            if (ConditionEditors.Count > 1)
            {
                ConditionEditors.Remove((QueryConditionEditor)sender);
            }
        }

        private void ConditionEditor_ConditionAdd(object sender, EventArgs e)
        {
            ConditionEditors.Add(CreateConditionEditor());
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            SmartPlaylist.Name = SmartPlaylistName.Text;
            SmartPlaylist.Conditions.Clear();

            foreach (QueryConditionEditor conditionEditor in ConditionEditors)
            {
                SmartPlaylist.Conditions.Add(new QueryCondition
                {
                    Field = PathExpressionVisitor.GetPath(conditionEditor.Model.Field.Value).First(),
                    Operator = conditionEditor.Model.Operator.Value,
                    Value = conditionEditor.Model.Value
                });
            }

            DialogResult = true;

            Close();
        }

        private QueryConditionEditor CreateConditionEditor()
        {
            var conditionEditor = new QueryConditionEditor { Margin = new Thickness(0, 0, 0, 10) };
            conditionEditor.ConditionAdd += ConditionEditor_ConditionAdd;
            conditionEditor.ConditionRemove += ConditionEditor_ConditionRemove;
            return conditionEditor;
        }
    }
}
