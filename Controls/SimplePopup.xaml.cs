using CommunityToolkit.Maui.Views;

namespace TCGPokellection.Controls;


public partial class SimplePopup : Popup
{
	public SimplePopup()
	{
		InitializeComponent();
	}

    private async void OnYesButton_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(true);
    }

    private async void OnNoButton_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(false);
    }
}