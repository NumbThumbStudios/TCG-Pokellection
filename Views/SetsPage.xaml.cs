using TCGPokellection.Models;
using DataLibrary;
using DataLibrary.Models;
using System.Net.NetworkInformation;
using TCGPokellection.Controls;
using CommunityToolkit.Maui.Views;

namespace TCGPokellection.Views;

public partial class SetsPage : ContentPage
{
    #region FIELDS / PROPERTIES
    // FIELDS
    private bool is_busy = false;

    // PROPERTIES
    public List<SetsModel> Sets { get; set; }

    private SetsModel selectedItem;
    public SetsModel SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(selectedItem));

            if (SelectedItem != null)
            {
                CollectionView_Item_Selected();
                SetsCollectionView.SelectedItem = null;
            }
        }
    }

    public string TotalCollectionValue { get; set; } = $"{0:C}";
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public SetsPage()
    {
        InitializeComponent();
        BindingContext = this;

        SetBannerId();
        LoadData();
    }

    // LOAD DATA
    public async void LoadData()
    {
        // CUSTOM POP UP EXAMPLE
        /*var popup = new SimplePopup();
        var result = await this.ShowPopupAsync(popup);
        if(result is bool boolResult)
        {
            if(boolResult == true)
            {
                DisplayAlert($"TRUE!", $"", $"COOL!");
            }
            else
            {
                DisplayAlert($"FALSE!", $"", $"SIICK!");
            }
        }*/


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

        string sql = "SELECT DISTINCT ps.*, COUNT(ts.tcg_sets_series) AS total_expansions " +
            "FROM pokemon_sets ps LEFT JOIN tcg_sets ts ON ps.set_name = ts.tcg_sets_series " +
            "WHERE ts.tcg_sets_showInApp = 1 " +
            "GROUP BY ps.set_name " +
            "ORDER BY release_date DESC;";
        Sets = await Task.Run(() => _data.LoadData<SetsModel, dynamic>(sql, new { }));
        
        OnPropertyChanged(nameof(Sets));

        LoadingIndicator.IsVisible = false;

        UpdateCollectionValue();
    }

    // ITEM SELECTED FROM COLLECTION VIEW
    private void CollectionView_Item_Selected()
    {
        if (is_busy) { return; }
        AppController.SetSelected = SelectedItem;
        Shell.Current.GoToAsync("Expansions");
        is_busy = true;
    }

    // SET AD BANNER ID
    void SetBannerId()
    {
        #if __ANDROID__
                //myAds.AdsId = "ca-app-pub-6937310993044586/2243061553";
        #elif __IOS__
                //myAds.AdsId = "ca-app-pub-6937310993044586/1289072533";
        #endif

        // Android Banner ID: ca-app-pub-6937310993044586/2243061553
        // IOS Banner ID: ca-app-pub-6937310993044586/1289072533
    }

    // UPDATE COLLECTION VALUE
    private async void UpdateCollectionValue()
    {
        while(AppController.collection_loaded == false)
        {
            await Task.Delay(1000);
        }

        try
        {
            float total_collection_value = 0;
            foreach (var set in Sets)
            {
                var list = AppController.DeviceCollection.Where(x => x.card.CardSet == set.set_name && x.card.tcg_cards_is_product == 0).ToList();
                if(list.Any())
                {
                    float total_value = 0;
                    foreach (var card in list)
                    {
                        total_value += card.card.tcg_cards_price_market * card.Quantity;
                        total_collection_value += card.card.tcg_cards_price_market * card.Quantity;
                    }
                    set.Display_TotalCollectionValue = $"{total_value:C}";
                }
                else
                {
                    set.Display_TotalCollectionValue = $"{0:C}";
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

    // ON NAVIGATED TO
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        is_busy = false;
    }

    // RETRY BUTTON CLICKED
    private async void RetryButton_Clicked(object sender, EventArgs e)
    {
        RetrySection.IsVisible = false;
        LoadingIndicator.IsVisible = true;
        await Task.Delay(1000);
        LoadData();
    }

    // ON APPEARING
    protected override void OnAppearing()
    {
        base.OnAppearing();

        UpdateCollectionValue();
    }
    #endregion
}