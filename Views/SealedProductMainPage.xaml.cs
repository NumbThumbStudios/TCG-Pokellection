using TCGPokellection.Models;
using DataLibrary;
using DataLibrary.Models;

namespace TCGPokellection.Views;

public partial class SealedProductMainPage : ContentPage
{
    #region FIELDS / PROPERTIES
    // FIELDS
    private bool is_busy = false;
    private SetsModel selectedItem;

    // PROPERTIES
    public List<SetsModel> Sets { get; set; }
    public SetsModel SelectedItem 
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));

            if(selectedItem != null)
            {
                SetSelected();
                SelectedItem = null;
            }
        }
    }
    public string TotalCollectionValue { get; set; } = $"{0:C}";
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public SealedProductMainPage()
    {
        InitializeComponent();
        BindingContext = this;

        SetBannerId();
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

        string sql = $"SELECT s.tcg_sets_series AS set_name, ps.set_image as set_image, COUNT(DISTINCT c.tcg_cards_id) AS set_number_of_products " +
            $"FROM tcg_sets s LEFT JOIN tcg_cards c ON s.tcg_sets_group_id = c.tcg_cards_group_id LEFT JOIN pokemon_sets ps ON ps.set_name = s.tcg_sets_series " +
            $"WHERE c.tcg_cards_cardNumber NOT LIKE '%Code Card%' AND c.tcg_cards_is_product = 1 " +
            $"GROUP BY s.tcg_sets_series " +
            $"ORDER BY ps.set_image IS NULL, s.tcg_sets_published DESC";

        Sets = await Task.Run(() => _data.LoadData<SetsModel, dynamic>(sql, new { }));

        foreach(SetsModel set in Sets)
        {
            if(String.IsNullOrEmpty(set.set_name))
            {
                set.set_name = "Misc Products";
            }
        }

        OnPropertyChanged(nameof(Sets));

        LoadingIndicator.IsVisible = false;

        UpdateCollectionValue();
    }

    // SET AD BANNER ID
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

    // SET SELECTED
    private void SetSelected()
    {
        AppController.SetSelected = SelectedItem;
        Shell.Current.Navigation.PushAsync(new SealedProductListPage(SelectedItem));
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
            foreach (var set in Sets)
            {
                var list = AppController.DeviceCollection.Where(x => x.card.CardSet == set.set_name && x.card.tcg_cards_is_product == 1).ToList();
                if (list.Any())
                {
                    float total_value = 0;
                    foreach (var card in list)
                    {
                        total_value += card.card.tcg_cards_price_market * card.Quantity;
                        total_collection_value += card.card.tcg_cards_price_market * card.Quantity;
                    }
                    set.Display_TotalCollectionValue = $"My Collection: {total_value:C}";
                }
                else
                {
                    set.Display_TotalCollectionValue = $"My Collection: {0:C}";
                }
            }
            TotalCollectionValue = $"{total_collection_value:C}";

            OnPropertyChanged(nameof(TotalCollectionValue));
            OnPropertyChanged(nameof(Sets));
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