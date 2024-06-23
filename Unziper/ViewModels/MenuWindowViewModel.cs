using CommunityToolkit.Mvvm.ComponentModel;

namespace Unziper.ViewModels
{
    public partial class MenuWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject _selectedViewModel;
        [ObservableProperty]
        private MenuViewModel _menuViewModel;
        public MenuWindowViewModel(MenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
            SelectedViewModel = MenuViewModel;
        }
    }
}
