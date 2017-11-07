using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soroban.ViewModels
{
    public enum Operator
    {
        None,
        Plus,
        Minus,
        Multiply,
        Divide
    }

    public class OperatorViewModel : ViewModelBase
    {
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (SetValue(ref isActive, value))
                    activeChanged();
            }
        }

        public Operator Operator { get; private set; }

        private int maximumValue;
        public int MaximumValue
        {
            get { return maximumValue; }
            set { SetValue(ref maximumValue, value); }
        }

        private readonly Action activeChanged;

        public OperatorViewModel(bool isActive, Operator op, int maximumValue, Action activeChanged)
        {
            this.isActive = isActive;
            this.maximumValue = maximumValue;
            Operator = op;
            this.activeChanged = activeChanged;
        }
    }

    public class OperatorsViewModel : ViewModelBase
    {
        public OperatorViewModel[] AvailableOperators { get; private set; }

        public OperatorsViewModel()
        {
            Action activeChanged = OnOperatorActiveChanged;

            AvailableOperators = new OperatorViewModel[]
            {
                new OperatorViewModel(true, Operator.Plus, 99, activeChanged),
                new OperatorViewModel(false, Operator.Minus, 99, activeChanged),
                new OperatorViewModel(false, Operator.Multiply, 99, activeChanged),
                new OperatorViewModel(false, Operator.Divide, 99, activeChanged),
            };

            OnOperatorActiveChanged();
        }

        public OperatorViewModel[] ActiveOperarors { get; private set; }

        private void OnOperatorActiveChanged()
        {
            ActiveOperarors = AvailableOperators.Where(x => x.IsActive).ToArray();
        }
    }
}
