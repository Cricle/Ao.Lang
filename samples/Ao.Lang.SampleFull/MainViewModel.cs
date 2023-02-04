using Ao.Lang.Runtime;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Linq;

namespace Ao.Lang.SampleFull
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            LangItems = Directory.EnumerateDirectories(Path.Combine(AppContext.BaseDirectory, "Strings"))
                .Select(x =>
                {
                    var lang = Path.GetFileName(x);
                    return new LangItem(lang, LanguageManager.Instance.CreateLangBox($"main:langs:{lang}"));
                }).ToArray();
            TodoManager = new TodoManager();
        }


        public LangItem[] LangItems { get; }

        public TodoManager TodoManager { get; }

        [ObservableProperty]
        private string currentContent;

        [ObservableProperty]
        private TodoEntity currentTodoEntity;

        [RelayCommand]
        public void DeleteTodo()
        {
            if (CurrentTodoEntity!=null)
            {
                TodoManager.TodoList.Remove(CurrentTodoEntity);
                CurrentTodoEntity = null;
            }
        }
        [RelayCommand]
        public void CreateTodo()
        {
            TodoManager.TodoList.Add(new TodoEntity { Content = CurrentContent });
            CurrentContent = null;
        }
        [RelayCommand]
        public void ClearTodo()
        {
            TodoManager.TodoList.Clear();
        }
    }
}
