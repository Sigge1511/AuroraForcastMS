using AuroraForcastMS.ViewModels;
using CommunityToolkit.Maui.Views;


namespace AuroraForcastMS
{

    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
