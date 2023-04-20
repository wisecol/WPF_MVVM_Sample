using MVVMSample4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMSample4.ViewModels
{
    public class MainViewModel : Notifier
    {
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

        private ICommand pushSend;
        public ICommand PushSend
        {
            get { return (this.pushSend) ?? (this.pushSend = new DelegateCommand(PushInfo)); }
        }

        private void PushInfo()
        {
            Message msg = new Message { Num = 123, str = DateTime.Now.ToString() };
            _receiveMessage = $"Num = {msg.Num}, Str = {msg.str}";
            OnPropertyChanged("ReceiveMessage");
        }

        public class DelegateCommand : ICommand
        {
            private readonly Action execute;
            private readonly Func<bool> canExecute;
            
            public DelegateCommand(Action execute) : this(execute,null)
            {                

            }

            public DelegateCommand(Action _execute, Func<bool> _canExecute)
            {
                this.execute = _execute;
                this.canExecute = _canExecute;
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
