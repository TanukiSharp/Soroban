using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soroban.ViewModels
{
    public class RootViewModel : ViewModelBase
    {
        public OperatorsViewModel Operators { get; private set; } = new OperatorsViewModel();

        public RunViewModel Run { get; private set; }

        public RootViewModel()
        {
            Run = new RunViewModel(Operators);
        }
    }
}
