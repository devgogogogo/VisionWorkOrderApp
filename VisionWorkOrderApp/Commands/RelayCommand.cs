using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VisionWorkOrderApp.Commands
{
     public class RelayCommand : ICommand
    {
        // Action = 리턴값 없는 메서드를 담는 타입
        private readonly Action _execute;
        // Func<bool> = bool을 리턴하는 메서드 타입
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute=null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
        
       
    }
}
