using System.Xml.Linq;

namespace Counter_Karwan_4E2;

public partial class AllCountersPage : ContentPage
{
    public string ColorPickerDescription { get; set; } = "Wybierz kolor licznika";
    public string AddNewCounterDescription { get; set; } = "Dodaj nowy licznik";
    
	public AllCountersPage()
	{
		InitializeComponent();
		BindingContext = this;

        CounterStorage.CreateFile();
        LoadCounters();
	}

    private void LoadCounters()
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, "counters.xml");

        System.Diagnostics.Debug.WriteLine(path);

        XDocument doc = XDocument.Load(path);

        foreach (XElement counter in doc.Root!.Elements("Counter"))
        {
            string id = counter.Element("ID")!.Value;
            string name = counter.Element("Name")!.Value;
            string initialValue = counter.Element("InitialValue")!.Value;
            string currentValue = counter.Element("CurrentValue")!.Value;
            string color = counter.Element("Color")!.Value;

            CounterData data = new CounterData(Guid.Parse(id), name, Int32.Parse(initialValue), Int32.Parse(currentValue), color);

            CounterView counterView = new CounterView(data);
            counterView.SetColor(Color.FromArgb(color));
            AllCountersContainer.Children.Add(counterView);
        }
    }

    private void OnAddCounterClicked(object sender, EventArgs e)
    {
		string name = CounterNameEntry.Text ?? "";
        string valueText = CounterStartingValueEntry.Text ?? "";
        string color = CounterColorPicker.SelectedItem?.ToString() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            ErrorLabel.Text = "B³¹d! Nazwa nie mo¿e byæ pusta.";
        }
        else if (string.IsNullOrWhiteSpace(valueText))
        {
            ErrorLabel.Text = "B³¹d! Wartoœæ pocz¹tkowa nie mo¿e byæ pusta.";
        }
        else if (!int.TryParse(valueText, out int value))
        {
            ErrorLabel.Text = "B³¹d! Wartoœæ pocz¹tkowa musi byæ liczb¹ ca³kowit¹.";
        } else if (string.IsNullOrWhiteSpace(color))
        {
            ErrorLabel.Text = "B³¹d! Musisz wybraæ jakiœ kolor!";
        }
        else
        {
            string colorText = "";

            switch (color)
            {
                case "Szary":
                    colorText = "#969696";
                    break;
                case "Czarny":
                    colorText = "#000000";
                    break;
                case "Bia³y":
                    colorText = "#FFFFFF";
                    break;
                case "Czerowny":
                    colorText = "#B81818";
                    break;
                case "Pomarañczowy":
                    colorText = "#B85A18";
                    break;
                case "Zó³ty":
                    colorText = "#B8AA18";
                    break;
                case "Zielony":
                    colorText = "#38B818";
                    break;
                case "Cyjan":
                    colorText = "#18B89D";
                    break;
                case "Niebieski":
                    colorText = "#1870B8";
                    break;
                case "Fioletowy":
                    colorText = "#5218B8";
                    break;
                case "Ró¿owy":
                    colorText = "#B818AD";
                    break;
                default:
                    colorText = "#969696";
                    break;
            }

            CounterData data = new CounterData()
            {
                Name = name,
                InitialValue = value,
                CurrentValue = value,
                Color = colorText
            };

            CounterStorage.AddCounter(data);

            CounterView counterView = new CounterView(data);
            counterView.SetColor(Color.FromArgb(colorText));
            AllCountersContainer.Children.Add(counterView);

            CounterNameEntry.Text = "";
            CounterStartingValueEntry.Text = "";
            ErrorLabel.Text = "";
            CounterColorPicker.SelectedItem = "";
        }
    }
}