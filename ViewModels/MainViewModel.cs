using AuroraForcastMS.Models;
using AuroraForcastMS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuroraForcastMS.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly AuroraService _auroraService;

        // Här använder vi den klassiska stilen: attribut på privat fält.
        // Detta genererar automatiskt egenskapen 'CurrentKp' (stor bokstav).
        [ObservableProperty]
        private KpIndexInfo _currentKp;

        [ObservableProperty]
        private bool _isBusy;

        public MainViewModel(AuroraService auroraService)
        {
            _auroraService = auroraService;
            LoadDataCommand = new AsyncRelayCommand(RefreshDataAsync);
            _ = RefreshDataAsync();
        }

        public IAsyncRelayCommand LoadDataCommand { get; }

        private async Task RefreshDataAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                // Använd stor bokstav här då den genereras av toolkitet
                CurrentKp = await _auroraService.GetCurrentKpIndexAsync();
            }
            catch
            {
                // Tyst catch för att inte krascha under bygget
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}