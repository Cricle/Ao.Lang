using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.SampleFull
{
    public partial class TodoManager : ObservableObject
    {
        public ObservableCollection<TodoEntity> TodoList { get; } = new ObservableCollection<TodoEntity>();
    }
    public partial class TodoEntity : ObservableObject
    {
        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private bool ok;
    }
}
