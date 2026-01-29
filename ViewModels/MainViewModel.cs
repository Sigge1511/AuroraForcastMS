using AuroraForcastMS.Models;
using AuroraForcastMS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliJ.Lang.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraForcastMS.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly AuroraService _auroraService;

        [ObservableProperty]
        private KpIndexInfo _currentKp;

        [ObservableProperty]
        private bool _isBusy;

        public MainViewModel()
        {
            _auroraService = new AuroraService();
            // Hämtar data direkt när appen startar
            LoadDataCommand = new AsyncRelayCommand(RefreshDataAsync);
            RefreshDataAsync();
        }

        public IAsyncRelayCommand LoadDataCommand { get; }

        private async Task RefreshDataAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var data = await _auroraService.GetCurrentKpIndexAsync();
                if (data != null)
                {
                    CurrentKp = data;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
