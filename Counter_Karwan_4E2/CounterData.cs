namespace Counter_Karwan_4E2
{
    public class CounterData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public string Color { get; set; } = "#969696";

        public CounterData() { }

        public CounterData(Guid id, string name, int initialValue, int currentValue, string color) {
            Id = id;
            Name = name;
            InitialValue = initialValue;
            CurrentValue = currentValue;
            Color = color;
        }
    }
}
