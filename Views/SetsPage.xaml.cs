using TCGPokellection.Models;
using DataLibrary;
using DataLibrary.Models;


namespace TCGPokellection.Views;

public partial class SetsPage : ContentPage
{
    #region FIELDS / PROPERTIES
    DataAccess _data = new DataAccess();
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
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public SetsPage()
    {
        InitializeComponent();
        BindingContext = this;

        LoadData();
    }

    // LOAD DATA
    public async void LoadData()
    {
        LoadingIndicator.IsVisible = true;

        await Task.Delay(250);

        string sql = "SELECT * FROM pokemon_sets ORDER BY release_date DESC";
        //Sets = await _data.LoadData<SetsModel, dynamic>(sql, new { });

        Sets = await Task.Run(() => _data.LoadData<SetsModel, dynamic>(sql, new { }));
        OnPropertyChanged(nameof(Sets));

        LoadingIndicator.IsVisible = false;

    }

    // ITEM SELECTED FROM COLLECTION VIEW
    private void CollectionView_Item_Selected()
    {
        AppController.SetSelected = SelectedItem;
        Shell.Current.GoToAsync("Expansions");
    }
    #endregion
}