using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Soroban.ViewModels
{
    public struct RunItem
    {
        public Operator Operator { get; private set; }
        public int Operand { get; private set; }

        public RunItem(Operator op, int operand)
        {
            Operator = op;
            Operand = operand;
        }
    }

    public class RunViewModel : ViewModelBase
    {
        private readonly Random random = new Random(Guid.NewGuid().GetHashCode());
        private readonly OperatorsViewModel availableOperators;

        private ObservableCollection<RunItem> items = new ObservableCollection<RunItem>();

        public ReadOnlyObservableCollection<RunItem> Items { get; private set; }

        private bool isComputed;
        public bool IsComputed
        {
            get { return isComputed; }
            private set { SetValue(ref isComputed, value); }
        }

        private int result;
        public int Result
        {
            get { return result; }
            private set { SetValue(ref result, value); }
        }

        public RunViewModel(OperatorsViewModel availableOperators)
        {
            this.availableOperators = availableOperators;

            Items = new ReadOnlyObservableCollection<RunItem>(items);
            
            RestartCommand = new AnonymousCommand(OnRestart);
            NextItemCommand = new AnonymousCommand(OnNextItem);
            ComputeResultCommand = new AnonymousCommand(OnComputeResult);
        }

        public ICommand RestartCommand { get; private set; }
        public ICommand NextItemCommand { get; private set; }
        public ICommand ComputeResultCommand { get; private set; }

        private void OnRestart()
        {
            items.Clear();
            Result = 0;
            IsComputed = false;
        }

        private void OnNextItem()
        {
            RunItem runItem = CreateRandomItem(items.Count > 0);
            items.Add(runItem);
            Result = 0;
            IsComputed = false;
        }

        private void OnComputeResult()
        {
            int result = 0;

            foreach (RunItem item in items)
            {
                switch (item.Operator)
                {
                    case Operator.None:
                    case Operator.Plus: result += item.Operand; break;
                    case Operator.Minus: result -= item.Operand; break;
                    case Operator.Multiply: result *= item.Operand; break;
                    case Operator.Divide: result /= item.Operand; break;
                }
            }

            Result = result;
            IsComputed = true;
        }

        private RunItem CreateRandomItem(bool hasOperator = true)
        {
            Operator op = Operator.None;

            int rnd = random.Next(availableOperators.ActiveOperarors.Length);

            if (hasOperator)
                op = availableOperators.ActiveOperarors[rnd].Operator;

            int maximumValue = availableOperators.ActiveOperarors[rnd].MaximumValue;

            return new RunItem(op, random.Next(maximumValue) + 1);
        }
    }
}
