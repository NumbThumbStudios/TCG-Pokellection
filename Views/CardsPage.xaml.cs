using DataLibrary.Models;
using DataLibrary;
using TCGPokellection.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.MauiMTAdmob;

namespace TCGPokellection.Views;

public partial class CardsPage : ContentPage
{
    #region FIELDS / PROPERTIES
    // FIELDS
    private bool isLoadingData = true;
    public List<String> list_sort_options { get; set; } = new List<string> 
    {
        "Card Number: Low to High",
        "Card Number: High to Low",
        "Product Name: A to Z",
        "Product Name: Z to A",
        "Price: Low to High",
        "Price: High to Low"
    };
    public List<String> list_filter_options { get; set; } = new List<string>
    {
        "None",
        "In My Collection",
        "Cards I Still Need"
    };
    

    // PROPERTIES
    public string HeaderTitle { get; set; }
    public string HeaderImage { get; set; }

    private string expansion_ReleaseDate;
    public string Expansion_ReleaseDate
    {
        get => expansion_ReleaseDate;
        set
        {
            expansion_ReleaseDate = value;
            OnPropertyChanged(nameof(Expansion_ReleaseDate));
        }
    }

    private string expansion_Collection_Completion = "0.00%";
    public string Expansion_Collection_Completion
    {
        get => expansion_Collection_Completion;
        set
        {
            expansion_Collection_Completion = value;
            OnPropertyChanged(nameof(Expansion_Collection_Completion));
        }
    }

    private string expansion_Collection_Value = "$0.00";
    public string Expansion_Collection_Value
    {
        get => expansion_Collection_Value;
        set
        {
            expansion_Collection_Value = value;
            OnPropertyChanged(nameof(Expansion_Collection_Value));
        }
    }

    private string expansion_CardCount;
    public string Expansion_CardCount
    {
        get => expansion_CardCount;
        set
        {
            expansion_CardCount = value;
            OnPropertyChanged(nameof(Expansion_CardCount));
        }
    }

    private string variants_Enabled;
    public string Variants_Enabled
    {
        get => variants_Enabled;
        set
        {
            variants_Enabled = value;
            OnPropertyChanged(nameof(Variants_Enabled));
        }
    }

    private bool variantsToggled;
    public bool VariantsToggled
    {
        get => variantsToggled;
        set
        {
            variantsToggled = value;
            OnPropertyChanged(nameof(VariantsToggled));
        }
    }

    private ObservableCollection<CardsModel> cards;
    public ObservableCollection<CardsModel> Cards
    {
        get => cards;
        set
        {
            cards = value;
            OnPropertyChanged(nameof(Cards));
        }
    }

    public ObservableCollection<CardsModel> page_cards { get; set; } = new ObservableCollection<CardsModel>();

    private CardsModel selectedItem;
    public CardsModel SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));

            if (SelectedItem != null)
            {
                Card_Selected();
                CardsCollectionView.SelectedItem = null;
            }
        }
    }

    private int sort_SelectedItem;
    public int Sort_SelectedItem
    {
        get => sort_SelectedItem;
        set
        {
            sort_SelectedItem = value;
            OnPropertyChanged(nameof(Sort_SelectedItem));

            if (Sort_SelectedItem != -1)
            {
                Sort();
            }
        }
    }

    private int filter_SelectedItem;
    public int Filter_SelectedItem
    {
        get => filter_SelectedItem;
        set
        {
            filter_SelectedItem = value;
            OnPropertyChanged(nameof(Filter_SelectedItem));

            Filter();
        }
    }
    #endregion




    #region CONSTRUCTORS
    // CONSTRUCTOR
    public CardsPage()
    {
        InitializeComponent();
        BindingContext = this;

        Expansion_ReleaseDate = AppController.ExpansionSelected.tcg_sets_published.ToShortDateString();
        Expansion_CardCount = $"{AppController.ExpansionSelected.tcg_sets_overall_total} Cards";

        CardsSortPicker.ItemsSource = list_sort_options;
        CardsFilterPicker.ItemsSource = list_filter_options;

        LoadData();
    }
    #endregion




    #region METHODS
    // LOAD DATA
    public async void LoadData()
    {
        Title = AppController.ExpansionSelected.tcg_sets_name_display;
        HeaderImage = AppController.ExpansionSelected.tcg_sets_image_logo;
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
        isLoadingData = true;

        await Task.Delay(250);

        try
        {
            string sql = $"SELECT c.*, s.tcg_sets_name_display AS CardExpansion, s.tcg_sets_series AS CardSet " +
                $"FROM tcg_cards c, tcg_sets s " +
                $"WHERE s.tcg_sets_group_id = '{AppController.ExpansionSelected.tcg_sets_group_id}' AND c.tcg_cards_group_id = '{AppController.ExpansionSelected.tcg_sets_group_id}' AND tcg_cards_is_product=0";

            ObservableCollection<CardsModel> list = new ObservableCollection<CardsModel>(await Task.Run(() => _data.LoadData<CardsModel, dynamic>(sql, new { })));
            Cards = list;

            // LOOP THROUGH CARDS
            foreach (var card in Cards)
            {
                // TRY TO PARSE THE CARD NUMBER 
                try
                {
                    string[] s1 = card.tcg_cards_cardNumber.Split("/");
                    card.CardNumber = int.Parse(s1[0]);
                }
                catch
                {
                    card.CardNumber = -1;
                }
            }

            LoadingIndicator.IsVisible = false;
            isLoadingData = false;

            page_cards = Cards;
            Filter();
            //OnPropertyChanged(nameof(page_cards));

            Sort_SelectedItem = 0;
            //Sort();
        }
        catch (Exception ex)
        {
            //CardsCollectionView.EmptyView = "Something went wrong. Please try again.";
            //LoadingIndicator.IsVisible = false;
        }
    }

    // SORT CARDS
    private async void Sort()
    {

        if (isLoadingData) { return; }

        LoadingIndicator.IsVisible = true;
        await Task.Delay(100);

        switch (Sort_SelectedItem)
        {
            case 0: // Card Number Low to High
                page_cards = new ObservableCollection<CardsModel>(page_cards.OrderBy(x => x.CardNumber).ThenByDescending(x => x.tcg_cards_subType).ToList());
                break;

            case 1: // Card Number High to Low
                page_cards = new ObservableCollection<CardsModel>(page_cards.OrderByDescending(x => x.CardNumber).ThenByDescending(x => x.tcg_cards_subType).ToList());
                break;

            case 2: // A to Z
                page_cards = new ObservableCollection<CardsModel>(page_cards.OrderBy(x => x.tcg_cards_clean_name).ThenByDescending(x => x.tcg_cards_subType).ToList());
                break;

            case 3: // Z to A
                page_cards = new ObservableCollection<CardsModel>(page_cards.OrderByDescending(x => x.tcg_cards_clean_name).ThenByDescending(x => x.tcg_cards_subType).ToList());
                break;

            case 4: // Price Low to High
                page_cards = new ObservableCollection<CardsModel>(page_cards.OrderBy(x => x.tcg_cards_price_market).ThenByDescending(x => x.tcg_cards_subType).ToList());
                break;

            case 5: // Price High to Low
                page_cards = new ObservableCollection<CardsModel>(page_cards.OrderByDescending(x => x.tcg_cards_price_market).ThenByDescending(x => x.tcg_cards_subType).ToList());
                break;
        }

        Expansion_CardCount = $"{page_cards.Count} Cards being shown below";
        UpdateCollectionCompletionPercentage();

        OnPropertyChanged(nameof(page_cards));
        LoadingIndicator.IsVisible = false;
    }

    // FILTER CARDS
    private async void Filter()
    {
        if (isLoadingData) { return; }

        LoadingIndicator.IsVisible = true;
        await Task.Delay(100);

        page_cards = Cards;

        if (AppController.ShowVariants == false)
        {
            var filtered_list = page_cards.GroupBy(card => card.tcg_cards_cardNumber).SelectMany(group => group.OrderBy(card => card.tcg_cards_subType.Length).Take(1)).ToList();
            page_cards = new ObservableCollection<CardsModel>(filtered_list);

            Variants_Enabled = "spr_variants_unchecked.png";
        }
        else
        {
            var groups = page_cards.GroupBy(x => x.tcg_cards_cardNumber);

            foreach(var group in groups)
            {
                if(group.Count() > 1)
                {
                    var max_length = group.Max(item => item.tcg_cards_subType.Length);
                    var longest_item = group.Where(item => item.tcg_cards_subType.Length == max_length);

                    foreach (var item in group)
                    {
                        item.ShowSubType = longest_item.Contains(item);
                    }
                }
            }

            Variants_Enabled = "spr_variants_checked.png";
        }

        switch (Filter_SelectedItem)
        {
            case 0: // No Filter
                
                break;

            case 1: // My Collection
                var temp_cards_list = new ObservableCollection<CardsModel>();

                foreach(var card in page_cards)
                {
                    if(card.IsInCollection == true)
                    {
                        temp_cards_list.Add(card);
                    }
                }

                page_cards = temp_cards_list;

                if(page_cards.Count == 0) { CardsCollectionView.EmptyView = "Nothing in your collection"; }
                break;

            case 2: // Needed Cards
                temp_cards_list = new ObservableCollection<CardsModel>();

                foreach (var card in page_cards)
                {
                    if (card.IsInCollection == false)
                    {
                        temp_cards_list.Add(card);
                    }
                }

                page_cards = temp_cards_list;

                if (page_cards.Count == 0) { CardsCollectionView.EmptyView = "You have the entire collection!"; }
                break;
        }

        Sort();
    }

    // ITEM SELECTED FROM COLLECTION VIEW
    private void Card_Selected()
    {
        if(isLoadingData) { return; }

        CardsSortPicker.IsEnabled = false;
        CardsFilterPicker.IsEnabled = false;

        AppController.Cards = page_cards.ToList();

        AppController.CardSelected = SelectedItem;
        //Navigation.PushAsync(new CardDetailsPage());
        Shell.Current.Navigation.PushAsync(new CardDetailsPage());
    }

    // UPDATE COLLECTION COMPLETION PERCENTAGE
    private void UpdateCollectionCompletionPercentage()
    {
        float collection_count = 0;
        float total_cards = 0;

        if (AppController.ShowVariants == false)
        {
            //var filtered_list = page_cards.GroupBy(card => card.tcg_cards_cardNumber).SelectMany(group => group.OrderBy(card => card.tcg_cards_subType.Length).Take(1)).ToList();
            var filtered_list = Cards.GroupBy(card => card.tcg_cards_cardNumber).SelectMany(group => group.OrderBy(card => card.tcg_cards_subType.Length).Take(1)).ToList();
            total_cards = filtered_list.Count;

            foreach(var card in filtered_list)
            {
                if (card.IsInCollection == true)
                {
                    collection_count++;
                }
            }
        }
        else
        {
            total_cards = Cards.Count;

            foreach (var card in Cards)
            {
                if(card.IsInCollection == true)
                {
                    collection_count++;
                }
            }
        }

        float percentage = (collection_count / total_cards);
        Expansion_Collection_Completion = $"{percentage:P2}";

        OnPropertyChanged(nameof(Expansion_Collection_Completion));
    }

    // UPDATE COLLECTION VALUE
    private void UpdateCollectionValue()
    {
        float total_value = 0;

        foreach(var card in Cards)
        {
            if(card.IsInCollection == true)
            {
                var found_card = AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).First();
                total_value += found_card.card.tcg_cards_price_market * found_card.Quantity;
            }
        }

        Expansion_Collection_Value = $"{total_value:C2}";
    }

    // LOAD COLLECTION CHECKMARKS
    public async void LoadCollection_FromDevice()
    {
        await Task.Delay(500);

        foreach(var card in Cards)
        {
            if(AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).Any())
            {
                card.IsInCollection = true;
                card.CollectionIconSource = "spr_checkmark_checked.png";
                card.CollectionCount = $"{AppController.DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).ToList()[0].Quantity} in collection";
            }
            else
            {
                card.IsInCollection = false;
                card.CollectionIconSource = "spr_checkmark_unchecked.png";
                card.CollectionCount = $"0 in collection";
            }
        }

        UpdateCollectionValue();
        UpdateCollectionCompletionPercentage();
    }

    // TOGGLE MENU
    public async void ToggleMenu()
    {
        if (isLoadingData) { return; }
        //PopupMenu.IsVisible = !PopupMenu.IsVisible;
        if (PopupMenu.IsVisible == false)
        {
            CardsCollectionView.InputTransparent = true;
            PopupMenu.IsVisible = true;
            isLoadingData = true;
            await PopupMenu.FadeTo(1, 100, Easing.Linear);
            isLoadingData = false;
        }
        else
        {
            isLoadingData = true;
            await PopupMenu.FadeTo(0, 100, Easing.Linear);
            PopupMenu.IsVisible = false;
            isLoadingData = false;
            CardsCollectionView.InputTransparent = false;
        }
    }

    // TOGGLE VARIANTS (OFF/ON)
    public void ToggleVariants()
    {
        LoadingIndicator.IsVisible = true;

        AppController.ShowVariants = !AppController.ShowVariants;
        AppController.Set_ShowVariants();

        VariantsToggled = AppController.ShowVariants;
        OnPropertyChanged(nameof(VariantsToggled));

        Filter();
        UpdateCollectionCompletionPercentage();
    }

    // ADD ALL CARDS ON SCREEN TO COLLECTION
    public async void AddAllCardsToCollection()
    {
        if(page_cards.Count <= 0)
        {
            await DisplayAlert($"", $"There aren't any cards displayed to add to your collection.", $"Okay");
            return;
        }

        bool response = await DisplayAlert($"Add To Collection", $"Are you sure you want to add all visible cards to your collection? If a card is already in your collection, it will not be affected.", $"Yes", $"No");
        
        if(response == true)
        {
            LoadingIndicator.IsVisible = true;
            await Task.Delay(500);

            var new_list = page_cards.Where(x => x.IsInCollection == false).ToList();
            foreach (var card in new_list)
            {
                card.IsInCollection = true;
                card.CollectionIconSource = "spr_checkmark_checked.png";
                card.CollectionCount = $"1 in collection";
                AppController.AddCardToCollection_Device(card, 1);
            }

            UpdateCollectionValue();
            UpdateCollectionCompletionPercentage();
            OnPropertyChanged(nameof(page_cards));

            LoadingIndicator.IsVisible = false;

            AppController.AdActionClicked(100);
            /*if (AppController.AdActionClicked(100)) { LoadingIndicator.IsVisible = true; }
            CrossMauiMTAdmob.Current.OnInterstitialClosed += (sender, args) => { LoadingIndicator.IsVisible = false; };
            CrossMauiMTAdmob.Current.OnInterstitialFailedToShow += (sender, args) => { LoadingIndicator.IsVisible = false; };
            CrossMauiMTAdmob.Current.OnInterstitialFailedToLoad += (sender, args) => { LoadingIndicator.IsVisible = false; };*/
        }
    }

    // REMOVE ALL CARDS ON SCREEN FROM COLLECTION
    public async void RemoveAllCardsFromCollection()
    {
        bool response = await DisplayAlert($"Remove From Collection", $"Are you sure you want to remove all visible cards from your collection?", $"Yes", $"No");

        if(response == true)
        {
            LoadingIndicator.IsVisible = true;
            await Task.Delay(500);

            var new_list = page_cards.Where(x => x.IsInCollection == true).ToList();
            foreach(var card in new_list)
            {
                card.IsInCollection = false;
                card.CollectionIconSource = "spr_checkmark_unchecked.png";
                card.CollectionCount = $"0 in collection";
                AppController.RemoveCardFromCollection_Device(card);
            }

            UpdateCollectionValue();
            UpdateCollectionCompletionPercentage();
            OnPropertyChanged(nameof(page_cards));

            LoadingIndicator.IsVisible = false;

            AppController.AdActionClicked(100);
            /*if (AppController.AdActionClicked(100)) { LoadingIndicator.IsVisible = true; }
            CrossMauiMTAdmob.Current.OnInterstitialClosed += (sender, args) => { LoadingIndicator.IsVisible = false; };
            CrossMauiMTAdmob.Current.OnInterstitialFailedToShow += (sender, args) => { LoadingIndicator.IsVisible = false; };
            CrossMauiMTAdmob.Current.OnInterstitialFailedToLoad += (sender, args) => { LoadingIndicator.IsVisible = false; };*/
        }
    }
    #endregion




    #region EVENT HANDLERS
    // ON APPEARING
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(500);
        VariantsToggled = AppController.ShowVariants;
        OnPropertyChanged(nameof(VariantsToggled));
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
        CardsModel found_card = Cards.Where(x => x.tcg_cards_id == card.tcg_cards_id).First();

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
        UpdateCollectionCompletionPercentage();
        OnPropertyChanged(nameof(page_cards));

        AppController.AdActionClicked();
        /*if (AppController.AdActionClicked()) { LoadingIndicator.IsVisible = true; }
        CrossMauiMTAdmob.Current.OnInterstitialClosed += (sender, args) => { LoadingIndicator.IsVisible = false; };
        CrossMauiMTAdmob.Current.OnInterstitialFailedToShow += (sender, args) => { LoadingIndicator.IsVisible = false; };
        CrossMauiMTAdmob.Current.OnInterstitialFailedToLoad += (sender, args) => { LoadingIndicator.IsVisible = false; };*/
    }

    // ON NAVIGATED TO
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        CardsSortPicker.IsEnabled = true;
        CardsFilterPicker.IsEnabled = true;

        while (isLoadingData)
        {
            await Task.Delay(100);
        }

        try
        {
            await Task.Run(() => LoadCollection_FromDevice());
        }
        catch
        {
            await DisplayAlert($"Uh-Oh", $"Something seems to have went terribly wrong.", $"Duh!");
        }
        //UpdateCollectionCompletionPercentage();
    }

    // TOGGLE VARIANTS TAPPED
    private void ToggleShowVariants_Tapped(object sender, TappedEventArgs e)
    {
        //ToggleVariants();
    }

    // RETRY NETWORK CONNECTION BUTTON CLICKED
    private async void RetryButton_Clicked(object sender, EventArgs e)
    {
        RetrySection.IsVisible = false;
        LoadingIndicator.IsVisible = true;
        await Task.Delay(1000);
        LoadData();
    }

    // MENU BUTTON TAPPED
    public void Menu_Tapped(object sender, TappedEventArgs e)
    {
        ToggleMenu();
    }
    #endregion
}