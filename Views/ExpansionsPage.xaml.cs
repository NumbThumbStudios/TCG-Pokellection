using DataLibrary.Models;
using DataLibrary;
using TCGPokellection.Models;

namespace TCGPokellection.Views;

public partial class ExpansionsPage : ContentPage
{
    #region FIELDS / PROPERTIES
    DataAccess _data = new DataAccess();
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
        Title = AppController.SetSelected.set_name;
        HeaderImage = AppController.SetSelected.set_image;
        OnPropertyChanged(nameof(HeaderImage));

        LoadingIndicator.IsVisible = true;

        await Task.Delay(250);

        string sql = $"SELECT * " +
            $"FROM tcg_sets " +
            $"WHERE tcg_sets_series = '{AppController.SetSelected.set_name}' AND tcg_sets_showInApp = 1 " +
            $"ORDER BY tcg_sets_published DESC";

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
    }

    // ITEM SELECTED FROM COLLECTION VIEW
    private void CollectionView_Item_Selected()
    {
        AppController.ExpansionSelected = SelectedItem;
        Shell.Current.GoToAsync("Cards");
    }
    #endregion
}