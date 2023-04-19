using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMSample1.View
{
    public class MainWindowViewModel : INotifier
    {
        private string _val1;
        private string _val2;
        private string _res;

        public string Val1
        {
            get => _val1;
            set
            {
                _val1 = value;
                OnPropertyChanged("Val1");
            }
        }
        public string Val2
        {
            get => _val2;
            set
            {
                _val2 = value;
                OnPropertyChanged("Val2");
            }
        }

        public string Res
        {
            get => _res;
            set
            {
                _res = value;
                OnPropertyChanged("Res");
            }
        }

        private ICommand pushBtn;
        public ICommand PushBtn
        {
            //?? 왼쪽이 null 이면 오른쪽 값으로 처리
            get { return (this.pushBtn) ?? (this.pushBtn = new DelegateCommand(Calc)); }
        }

        private void Calc()
        {
            if (string.IsNullOrEmpty(_val1) || string.IsNullOrEmpty(_val2))
                _res = "숫자를 입력하세요.";
            else
            {
                double intVal1 = -1, intVal2 = -1;
                if(double.TryParse(_val1,out intVal1) && double.TryParse(_val2, out intVal2))
                    _res = _val1 + " + " + _val2 + " = " + (double.Parse(_val1) + double.Parse(_val2));
                else
                    _res = "숫자를 입력하세요.";
            }
            
            OnPropertyChanged("Res");
        }

        public class DelegateCommand : ICommand
        {

            private readonly Func<bool> canExecute;
            private readonly Action execute;

            /// <summary>
            /// 인스턴스 초기화
            /// </summary>
            /// <param name="execute">indicate an execute function</param>
            public DelegateCommand(Action execute) : this(execute, null)
            {
            }

            /// <summary>
            /// 새 인스턴스 초기화
            /// </summary>
            /// <param name="execute">execute function </param>
            /// <param name="canExecute">can execute function</param>
            public DelegateCommand(Action execute, Func<bool> canExecute)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }
            /// <summary>
            /// 이벤트 핸들러를 실행
            /// </summary>
            public event EventHandler CanExecuteChanged;

            /// <summary>
            /// </summary>
            /// <param name="o">parameter by default of icomand interface</param>
            /// <returns>can execute or not</returns>
            public bool CanExecute(object o)
            {
                if (this.canExecute == null)
                {
                    return true;
                }
                return this.canExecute();
            }

            /// <summary>
            /// icommand 인터페이스 실행 메서드 구현
            /// </summary>
            /// <param name="o">parameter by default of icomand interface</param>
            public void Execute(object o)
            {
                this.execute();
            }

            /// <summary>
            /// 속성이 변경되면 raise can excute가 변경됨
            /// </summary>
            public void RaiseCanExecuteChanged()
            {
                if (this.CanExecuteChanged != null)
                {
                    this.CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

    }
}
