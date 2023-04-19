using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMSample2.ViewModels
{
    public class MainViewModel :Notifier
    {
        private bool _isFree;
        private string _status;
       
        public bool IsFree
        {
            get { return _isFree; }
            set
            {
                _isFree = value;
                OnPropertyChanged("IsFree");
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        
       

        private ICommand pushBtn;

        public ICommand PushBtn
        {
            get { return (this.pushBtn) ?? (this.pushBtn = new DelegateCommand(ExecAsync)); }
        }

        public MainViewModel()
        {
            IsFree = true;
            Status = "";
            pushBtn = new DelegateCommand(ExecAsync);
        }

        private async Task ExecAsync()
        {
            IsFree = false;
            Status = "Processing...";

            await Task.Delay(2000);

            IsFree = true;
            Status = "Complete";

            OnPropertyChanged("Status");
        }


        public class DelegateCommand : ICommand
        {
            private readonly Func<Task> _execute;
            private readonly Func<bool> _canExecute;
            public DelegateCommand(Func<Task> execute):this(execute, null)
            {
                this._execute = execute;
            }
            public DelegateCommand(Func<Task> execute, Func<bool> canExecute)
            {
                this._execute = execute;
                this._canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                if (this._canExecute == null)
                    return true;

                return this._canExecute();
            }

            public void Execute(object parameter)
            {
                this._execute();
            }

            public void RaiseCanExecuteChanged()
            {
                if (this.CanExecuteChanged != null)
                    this.CanExecuteChanged(this, EventArgs.Empty);
            }

        }

    }
}
