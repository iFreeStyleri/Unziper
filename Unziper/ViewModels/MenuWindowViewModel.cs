using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
