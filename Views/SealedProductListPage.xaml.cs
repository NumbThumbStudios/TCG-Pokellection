using TCGPokellection.Models;
using DataLibrary;
using DataLibrary.Models;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TCGPokellection.Views;

public partial class SealedProductListPage : ContentPage
{
    #region FIELDS / PROPERTIES
    // FIELDS
    private SetsModel setSelected;
    private CardsModel selectedItem;

    // PROPERTIES
    public string HeaderTitle { get; set; }
    public CardsModel SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));

            if(selectedItem != null)
            {
                ProductSelected();
                SelectedItem = null;
            }
        }
    }
    public ObservableCollection<CardsModel> Products { get; set; }
    public string TotalCollectionValue { get; set; } = $"{0:C}";
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public SealedProductListPage()
    {
        InitializeComponent();

        Init();
    }

    // CONSTRUCTOR: OVERLOADED
    public SealedProductListPage(SetsModel set)
    {
        InitializeComponent();

        setSelected = set;
        Init();
    }

    // INITIALIZE
    private void Init()
    {
        BindingContext = this;
        SetBannerId();

        HeaderTitle = setSelected.set_name;
        OnPropertyChanged(nameof(HeaderTitle));

        LoadData();
    }

    // LOAD DATA
    private async void LoadData()
    {
        // VERIFY INTERNET CONNECTION 
        if (AppController.IsConnectedToNetwork() == false)
        {
            LoadingIndicator.IsVisible = false;
            RetrySection.IsVisible = true;
            return;
        }

        DataAccess _data = new DataAccess();

        LoadingIndicator.IsVisible = true;

        await Task.Delay(250);

        string set_name_to_pass = AppController.SetSelected.set_name;
        if(set_name_to_pass == "Misc Products") { set_name_to_pass = ""; }

        string sql = $"SELECT c.*, s.tcg_sets_name_display as CardExpansion, s.tcg_sets_series AS CardSet " +
            $"FROM tcg_cards c LEFT JOIN tcg_sets s ON s.tcg_sets_group_id = c.tcg_cards_group_id " +
            $"WHERE c.tcg_cards_is_product = 1 " +
            $"AND c.tcg_cards_cardNumber NOT LIKE '%code card%' " +
            $"AND s.tcg_sets_series = '{set_name_to_pass}'";

        Products = new ObservableCollection<CardsModel>(await Task.Run(() => _data.LoadData<CardsModel, dynamic>(sql, new { })));
        OnPropertyChanged(nameof(Products));

        LoadingIndicator.IsVisible = false;

        LoadCollection_FromDevice();

        UpdateCollectionValue();
    }

    // PRODUCT CLICKED
    private void ProductSelected()
    {
        AppController.CardSelected = SelectedItem;
        AppController.Cards = Products.ToList();
        Shell.Current.Navigation.PushAsync(new CardDetailsPage());
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

    // LOAD COLLECTION CHECKMARKS
    public async void LoadCollection_FromDevice()
    {
        await Task.Delay(500);

        foreach (var prod in Products)
        {
            if (AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == prod.tcg_cards_id).Any())
            {
                prod.IsInCollection = true;
                prod.CollectionIconSource = "spr_checkmark_checked.png";
                prod.CollectionCount = $"{AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == prod.tcg_cards_id).ToList()[0].Quantity} in collection";
            }
            else
            {
                prod.IsInCollection = false;
                prod.CollectionIconSource = "spr_checkmark_unchecked.png";
                prod.CollectionCount = $"0 in collection";
            }
        }
    }

    // UPDATE COLLECTION VALUE
    private async void UpdateCollectionValue()
    {
        while (AppController.collection_loaded == false)
        {
            await Task.Delay(1000);
        }

        try
        {
            float total_collection_value = 0;
            foreach (var set in Products)
            {
                //var list = AppController.DeviceCollection.Where(x => x.card.CardSet == set.CardSet && x.card.CardExpansion == set.CardExpansion && x.card.tcg_cards_is_product == 1).ToList();
                var list = AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == set.tcg_cards_id).ToList();
                if (list.Any())
                {
                    foreach (var card in list)
                    {
                        total_collection_value += card.card.tcg_cards_price_market * card.Quantity;
                    }
                }
            }
            TotalCollectionValue = $"{total_collection_value:C}";

            OnPropertyChanged(nameof(TotalCollectionValue));
        }
        catch { }
    }
    #endregion




    #region EVENT HANDLERS
    // RETRY BUTTON CLICKED
    private async void RetryButton_Clicked(object sender, EventArgs e)
    {
        RetrySection.IsVisible = false;
        LoadingIndicator.IsVisible = true;
        await Task.Delay(1000);
        LoadData();
    }

    // UPDATE COLLECTION BUTTON TAPPED
    private void UpdateCollection_Tapped(object sender, TappedEventArgs e)
    {
        // VERIFY INTERNET CONNECTION 
        if (AppController.IsConnectedToNetwork() == false)
        {
            DisplayAlert($"Network Error", $"Please check your internet connection and try again.", "Okay");
            return;
        }

        CardsModel card = ((Border)sender).BindingContext as CardsModel;
        CardsModel found_card = Products.Where(x => x.tcg_cards_id == card.tcg_cards_id).First();

        if (found_card.IsInCollection)
        {
            found_card.IsInCollection = false;
            found_card.CollectionIconSource = "spr_checkmark_unchecked.png";
            found_card.CollectionCount = $"0 in collection";
            AppController.RemoveCardFromCollection_Device(found_card);
        }
        else
        {
            found_card.IsInCollection = true;
            found_card.CollectionIconSource = "spr_checkmark_checked.png";
            found_card.CollectionCount = $"1 in collection";
            AppController.AddCardToCollection_Device(found_card);
        }

        UpdateCollectionValue();

        AppController.AdActionClicked();
    }

    // ON APPEARING
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        UpdateCollectionValue();

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
    #endregion
}