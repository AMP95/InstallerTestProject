using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Squirrel;
using System.IO;

namespace InstallerTestProject
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        Action _action;
        public Command(Action action)
        {
            _action = action;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        string _input;
        public string Input{
            get => _input;
            set {
                _input = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Input)));
            }
        }
        string dir;
        public VM()
        {
            dir = Directory.GetCurrentDirectory();
            if (!File.Exists(dir + "//1.txt"))
            {
                Write("default");
                Input = "default";
            }
            else {
                using (FileStream stream = File.OpenWrite(dir + "//1.txt"))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Input = reader.ReadToEnd();
                    }
                }
            }

        }
        public Command Click
        {
            get {
                return new Command(() => {
                    Write(Input);
                });
            }
        }
        private void Write(string input) {
            using (FileStream stream = File.OpenWrite(dir + "//1.txt"))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(Input);
                    writer.Flush();
                }
            }
        }
    }
}
