using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyLAB2
{
   public class MyICommand:ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public MyICommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }
        public MyICommand(Action executeMethod,Func<bool> canExecute)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecute;
        }

        public event EventHandler CanExecuteChanged = delegate { };
       

        bool ICommand.CanExecute(object parameter)
        {
           if(_TargetCanExecuteMethod!=null)
            {
                return _TargetCanExecuteMethod();
            }
            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

         void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
