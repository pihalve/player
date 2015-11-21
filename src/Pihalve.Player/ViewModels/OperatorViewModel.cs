using System.Collections.Generic;
using Pihalve.Player.Library.Model;

namespace Pihalve.Player.ViewModels
{
    public class OperatorViewModel
    {
        public static readonly Dictionary<Operator, string> Translations = new Dictionary<Operator, string>
        {
            { Operator.Contains, "contains" },
            { Operator.DoesNotContain, "does not contain" },
            { Operator.Is, "is" },
            { Operator.IsNot, "is not" },
            { Operator.StartsWith, "starts with" },
            { Operator.EndsWith, "ends with" }
        };

        public OperatorViewModel(Operator value)
        {
            Value = value;
        }

        public Operator Value { get; }

        public string DisplayName => Translations[Value];
    }
}
