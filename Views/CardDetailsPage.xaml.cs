using DataLibrary.Models;
using Plugin.MauiMTAdmob;
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

        SetBannerId();
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

        LoadingIndicator.IsVisible = false;
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

    // SET BANNER ID
    void SetBannerId()
    {
        #if __ANDROID__
                myAds.AdsId = "ca-app-pub-6937310993044586/2243061553";
        #elif __IOS__
                myAds.AdsId = "ca-app-pub-6937310993044586/1289072533";
        #endif

        // Android Banner ID: ca-app-pub-6937310993044586/2243061553
        // IOS Banner ID: ca-app-pub-6937310993044586/1289072533
    }
    #endregion




    #region EVENT HANDLERS
    // ON APPEARING
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Verify Ads are a thing...
        await AppController.RestoreIAP();

        if (AppController.IAP_AdsRemoved)
        {
            myAds.IsVisible = false;
            myAds.IsEnabled = false;
        }
        else
        {
            myAds.IsVisible = true;
            myAds.IsEnabled = true;
        }
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
        string aff_link = await AppController.Get_TCGPlayerAffiliateLink();
        Uri uri = new Uri(CardSelected.tcg_cards_buy_url + aff_link);
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }

    // PLUS BUTTON COLLECTION TAPPED
    private void PlusButtonCollection_Tapped(object sender, TappedEventArgs e)
    {
        CardsModel card = ((Border)sender).BindingContext as CardsModel;

        int in_collection = 1;
        if (AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).Any())
        {
            in_collection = AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).ToList()[0].Quantity + 1;
        }

        AppController.AddCardToCollection_Device(card, in_collection);
        card.CollectionCount = $"{in_collection} in collection";

        AppController.AdActionClicked();
        /*if (AppController.AdActionClicked()) { LoadingIndicator.IsVisible = true; }
        CrossMauiMTAdmob.Current.OnInterstitialClosed += (sender, args) => { LoadingIndicator.IsVisible = false; };
        CrossMauiMTAdmob.Current.OnInterstitialFailedToShow += (sender, args) => { LoadingIndicator.IsVisible = false; };
        CrossMauiMTAdmob.Current.OnInterstitialFailedToLoad += (sender, args) => { LoadingIndicator.IsVisible = false; };*/
    }

    // MINUS BUTTON COLLECTION TAPPED
    private void MinusButtonCollection_Tapped(object sender, TappedEventArgs e)
    {
        CardsModel card = ((Border)sender).BindingContext as CardsModel;

        int in_collection = 1;
        if (!AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).Any())
        {
            return;
        }
        else
        {
            in_collection = AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).ToList()[0].Quantity - 1;
        }

        AppController.AddCardToCollection_Device(card, in_collection);
        card.CollectionCount = $"{in_collection} in collection";

        AppController.AdActionClicked();
        /*if (AppController.AdActionClicked()) { LoadingIndicator.IsVisible = true; }
        CrossMauiMTAdmob.Current.OnInterstitialClosed += (sender, args) => { LoadingIndicator.IsVisible = false; };
        CrossMauiMTAdmob.Current.OnInterstitialFailedToShow += (sender, args) => { LoadingIndicator.IsVisible = false; };
        CrossMauiMTAdmob.Current.OnInterstitialFailedToLoad += (sender, args) => { LoadingIndicator.IsVisible = false; };*/
    }

    // POSITION CHANGED
    private void CarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
    {
        Position = CardDetailsCarouselView.Position;
    }
    #endregion
}