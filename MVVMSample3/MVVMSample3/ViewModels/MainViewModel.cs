using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMSample3.ViewModels
{
    public class MainViewModel : Notifier
    {
        private string _sendMessage;
        public string SendMessage
        {
            get => _sendMessage;
            set
            {
                _sendMessage = value;
                OnPropertyChanged("SendMessage");
            }
        }

        private string _receiveMessage;
        public string ReceiveMessage
        {
            get => _receiveMessage;
            set
            {
                 _receiveMessage = value;
                OnPropertyChanged("ReceiveMessage");
            }
        }

        private bool _isDisabled;
        public bool IsDisabled
        {
            get { return _isDisabled; }
            set
            {
                _isDisabled = value;
                OnPropertyChanged("Disabled");
            }
        }

        private ICommand pushButton;

        public MainViewModel()
        {
            
        }

        public ICommand PushButton
        {
            get { return (this.pushButton) ?? (this.pushButton = new DelegateCommand(PrintMessage)); }
        }


        private void PrintMessage()
        {
            _receiveMessage = "Received : " +_sendMessage;
            OnPropertyChanged("ReceiveMessage");
        }


        public class DelegateCommand : ICommand
        {
            private readonly Func<bool> canExecute;
            private readonly Action execute;

            public DelegateCommand(Func<bool> canExecute, Action execute)
            {
                this.canExecute = canExecute;
                this.execute = execute;
            }

            public DelegateCommand(Action execute) : this(execute, null)
            {

            }

            public DelegateCommand(Action execute, object p)
            {
                this.execute = execute;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                if (this.canExecute == null)
                    return true;

                return this.canExecute();
            }

            public void Execute(object parameter)
            {
                this.execute();
            }

            public void RaiseCanExecuteChanged()
            {
                if (this.CanExecuteChanged != null)
                    this.CanExecuteChanged(this, EventArgs.Empty);
            }

        }
    }
}
