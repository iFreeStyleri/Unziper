using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unziper.Core.Abstractions;
using Unziper.Utils;

namespace Unziper.ViewModels
{
    public partial class MenuViewModel : ObservableObject, IFileDragDropTarget
    {
        private readonly IUnzipService _unzipService;
        private readonly IAsyncUnzipService _asyncUnzipService;
        private readonly IOptionService _optionService;

        
        public AsyncRelayCommand UnzipCommand { get; set; }
        public AsyncRelayCommand AddPasswordsCommand { get; set; }

        [ObservableProperty] private bool _isAsyncBruteForce;
        [ObservableProperty] private bool _hasUnzip;
        [ObservableProperty] private bool _hasVisibilityBar;
        [ObservableProperty] private int _allFiles;
        [ObservableProperty] private int _currentFile;
        [ObservableProperty] private string _unzipProgressText;
        [ObservableProperty] private bool _hasLoadedPassword;


        public MenuViewModel(IAsyncUnzipService asyncUnzipService, IUnzipService unzipService, IOptionService optionService)
        {
            _optionService = optionService;
            _unzipService = unzipService;
            _asyncUnzipService = asyncUnzipService;
            UnzipCommand = new AsyncRelayCommand(UnzipMethod);
            AddPasswordsCommand = new AsyncRelayCommand(AddPasswordsMethod);
            HasLoadedPassword = optionService.HasLoadedPassword;
        }

        private async Task UnzipMethod()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Archives(.7z, .rar, .zip)|*.7z;*.rar;*.zip";
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == true)
            {
                await Unzip(fileDialog.FileNames);
            }
        }

        public async void OnFileDrop(string[] filePaths)
            => await Unzip(filePaths);


        private async Task AddPasswordsMethod()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt|*.txt";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var passwords = await File.ReadAllLinesAsync(fileDialog.FileName);
                _optionService.SetPasswords(passwords);
                await _optionService.Save();
                HasLoadedPassword = _optionService.HasLoadedPassword;
                MessageBox.Show("Пароли сохранены!");
            }
        }

        private async Task Unzip(string[] filePaths)
        {
            filePaths = filePaths.Where(s => s.Contains(".zip") || s.Contains(".7z") || s.Contains(".rar")).ToArray();
            if (filePaths.Length == 0)
            {
                MessageBox.Show("Файлы не выбраны!");
                return;
            }
            ConfigureProgress(filePaths.Length);
            var notifiers = new List<string>();
            if (IsAsyncBruteForce)
                notifiers = await _asyncUnzipService.UnzipAll(filePaths, Path.GetDirectoryName(filePaths.First()), UnzipProgress);
            else
                notifiers = await _unzipService.UnzipAll(filePaths, Path.GetDirectoryName(filePaths.First()), UnzipProgress);

            var message = string.Join("\n", notifiers);

            if (string.IsNullOrWhiteSpace(message))
                MessageBox.Show("Файлы успешно разархивированы!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
                MessageBox.Show("Файлы не были разархивированы:\n" + message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            HasVisibilityBar = false;
        }

        private void ConfigureProgress(int countFiles)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                AllFiles = countFiles;
                CurrentFile = 0;
                HasVisibilityBar = AllFiles > 1;
            });
        }

        private void UnzipProgress()
        {
            if (AllFiles > 1)
            {
                if (CurrentFile < AllFiles)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        CurrentFile++;
                        UnzipProgressText = $" {CurrentFile} из {AllFiles}";
                    });
                }
            }
        }
    }
}
