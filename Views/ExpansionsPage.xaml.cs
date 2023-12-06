using DataLibrary.Models;
using DataLibrary;
using TCGPokellection.Models;

namespace TCGPokellection.Views;

public partial class ExpansionsPage : ContentPage
{
    #region FIELDS / PROPERTIES
    // FIELDS
    private bool is_busy = false;

    // PROPERTIES
    public string HeaderTitle { get; set; }
    public string HeaderImage { get; set; }
    public List<ExpansionsModel> Expansions { get; set; }

    private ExpansionsModel selectedItem;
    public ExpansionsModel SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(selectedItem));

            if (SelectedItem != null)
            {
                CollectionView_Item_Selected();
                ExpansionsCollectionView.SelectedItem = null;
            }
        }
    }

    public string TotalCollectionValue { get; set; } = $"{0:C}";
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public ExpansionsPage()
    {
        InitializeComponent();
        BindingContext = this;

        LoadData();
    }

    // LOAD DATA
    public async void LoadData()
    {
        HeaderTitle = AppController.SetSelected.set_name;
        HeaderImage = AppController.SetSelected.set_image;
        OnPropertyChanged(nameof(HeaderTitle));
        OnPropertyChanged(nameof(HeaderImage));

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

        string sql = $"SELECT ts.*, COUNT(DISTINCT tc.tcg_cards_cardNumber) as total_cards " +
            $"FROM tcg_sets ts LEFT JOIN tcg_cards tc ON ts.tcg_sets_group_id = tc.tcg_cards_group_id " +
            $"WHERE tcg_sets_series = '{AppController.SetSelected.set_name}' AND tcg_sets_showInApp = 1 AND tc.tcg_cards_is_product = 0 " +
            $"GROUP BY ts.tcg_sets_group_id " +
            $"ORDER BY tcg_sets_published DESC;";
        //Expansions = await _data.LoadData<ExpansionsModel, dynamic>(sql, new { });
        Expansions = await Task.Run(() => _data.LoadData<ExpansionsModel, dynamic>(sql, new { }));

        foreach (var item in Expansions)
        {
            if (String.IsNullOrEmpty(item.tcg_sets_image_logo))
            {
                item.tcg_sets_image_logo = "https://cdn.freebiesupply.com/images/large/2x/pokemon-logo-black-transparent.png";
            }
        }
        OnPropertyChanged(nameof(Expansions));

        LoadingIndicator.IsVisible = false;

        UpdateCollectionValue();
    }

    // ITEM SELECTED FROM COLLECTION VIEW
    private void CollectionView_Item_Selected()
    {
        if (is_busy) { return; }
        AppController.ExpansionSelected = SelectedItem;
        Shell.Current.GoToAsync("Cards");
        is_busy = true;
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
            foreach (var expansion in Expansions)
            {
                var list = AppController.DeviceCollection.Where(x => x.card.CardExpansion == expansion.tcg_sets_name_display && x.card.CardSet == AppController.SetSelected.set_name && x.card.tcg_cards_is_product == 0).ToList();
                if (list.Any())
                {
                    float total_value = 0;
                    foreach (var card in list)
                    {
                        total_value += card.card.tcg_cards_price_market * card.Quantity;
                        total_collection_value += card.card.tcg_cards_price_market * card.Quantity;
                    }

                    expansion.Display_TotalCollectionValue = $"{total_value:C}";
                }
                else
                {
                    expansion.Display_TotalCollectionValue = $"{0:C}";
                }
            }
            TotalCollectionValue = $"{total_collection_value:C}";
            OnPropertyChanged(nameof(TotalCollectionValue));

            OnPropertyChanged(nameof(Expansions));
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

    // NO NETWORK - RETRY BUTTON CLICKED
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