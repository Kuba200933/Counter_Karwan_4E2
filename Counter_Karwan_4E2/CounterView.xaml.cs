namespace Counter_Karwan_4E2;

public partial class CounterView : ContentView
{
    int count = 0;
    int initialCount = 0;

    public Guid Id { get; }
    public string CounterName { get; set; } = "";
    public string CounterLabelText { get; set; } = "0";
    public string Color { get; set; }

    public string AddDescription { get; set; } = "Zwiększ wartość licznika";
    public string SubDescription { get; set; } = "Zmniejsz wartość licznika";
    public string ResetDescription { get; set; } = "Przywróć wartość początkową licznika";
    public string DeleteDescription { get; set; } = "";

    public CounterView(CounterData data)
    {
        InitializeComponent();

        Id = data.Id;
        CounterName = data.Name;
        initialCount = data.InitialValue;
        count = data.CurrentValue;
        Color = data.Color;
        
        CounterLabelText = count.ToString();

        DeleteDescription = $"Usuń licznik {CounterName}";

        BindingContext = this;
    }

    private void OnCounterAddClicked(object sender, EventArgs e)
    {
        SetValue(count + 1);
    }

    private void OnCounterSubClicked(object sender, EventArgs e)
    {
        SetValue(count - 1);
    }

    private void OnCounterResetClicked(object sender, EventArgs e)
    {
        SetValue(initialCount);
    }

    private void OnCounterDeleteClicked(object sender, EventArgs e)
    {
        CounterStorage.DeleteCounter(Id);

        if (Parent is FlexLayout container)
        {
            container.Children.Remove(this);
        }
    }

    private void SetValue(int value)
    {
        count = value;
        CounterLabelText = count.ToString();
        OnPropertyChanged(nameof(CounterLabelText));

        CounterStorage.UpdateCounter(Id, count);
    }

    public void SetColor(Color color)
    {
        CounterColorLable.BackgroundColor = color;
    }
}