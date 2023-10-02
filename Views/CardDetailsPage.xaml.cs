using DataLibrary.Models;
using System.Security.Policy;
using TCGPokellection.Models;

namespace TCGPokellection.Views;

public partial class CardDetailsPage : ContentPage
{
    #region FIELDS / PROPERTIES
    private List<CardsModel> cards;
    public List<CardsModel> Cards
    {
        get => cards;
        set
        {
            cards = value;
            OnPropertyChanged(nameof(Cards));
        }
    }
    private CardsModel cardSelected;
    public CardsModel CardSelected
    {
        get => cardSelected;
        set
        {
            cardSelected = value;
            OnPropertyChanged(nameof(CardSelected));
        }
    }
    private int position;
    public int Position
    {
        get => position;
        set
        {
            position = value;
            OnPropertyChanged(nameof(Position));
        }
    }
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public CardDetailsPage()
	{
		InitializeComponent();
        BindingContext = this;

        LoadData();
    }

    // LOAD DATA
    public void LoadData()
    {
        CardDetailsCarouselView.IsVisible = false;
        CardDetailsCarouselView.IsScrollAnimated = false;
        CardDetailsCarouselView.CurrentItem = null;
        CardDetailsCarouselView.Position = 0;

        Cards = AppController.Cards;

        for (var i = 0; i < Cards.Count; i++)
        {
            if (Cards[i].tcg_cards_id == AppController.CardSelected.tcg_cards_id)
            {
                CardSelected = Cards[i];
                Position = i;
                break;
            }
        }

        ShowData();
    }

    // SHOW DATA
    public async void ShowData()
    {
        await Task.Delay(250);

        CardDetailsCarouselView.Position = Position;
        CardDetailsCarouselView.CurrentItem = CardSelected;

        //await Task.Delay(1000);
        CardDetailsCarouselView.IsVisible = true;
        CardDetailsCarouselView.Scale = 0;
        CardDetailsCarouselView.Opacity = 0;
        CardDetailsCarouselView.ScaleTo(1, 250, Easing.Linear);
        CardDetailsCarouselView.FadeTo(1, 250, Easing.Linear);

        //await DisplayAlert("Title", $"{Position}", "Eh");
    }

    // (OVERRIDE) ON DEVICE BACK BUTTON PRESSED
    protected override bool OnBackButtonPressed()
    {
        CardDetailsCarouselView.ScaleTo(0, 250, Easing.Linear);
        CardDetailsCarouselView.FadeTo(0, 250, Easing.Linear);

        return base.OnBackButtonPressed();
    }

    // SHOP NOW BUTTON PRESSED
    private async void ShopNow_Tapped(object sender, TappedEventArgs e)
    {
        Uri uri = new Uri(CardSelected.tcg_cards_buy_url);
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
    #endregion
}