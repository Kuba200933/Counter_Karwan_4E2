using System.Xml.Linq;

namespace Counter_Karwan_4E2
{
    public static class CounterStorage
    {
        private static string path = Path.Combine(FileSystem.AppDataDirectory, "counters.xml");

        public static void CreateFile()
        {
            if (!File.Exists(path))
            {
                XDocument doc = new XDocument(
                        new XElement("Counters")
                );

                doc.Save(path);
            }
        }

        public static void AddCounter(CounterData counter)
        {
            CreateFile();

            XDocument doc = XDocument.Load(path);

            doc.Root!.Add(
                new XElement("Counter",
                    new XElement("ID", counter.Id),
                    new XElement("Name", counter.Name),
                    new XElement("InitialValue", counter.InitialValue),
                    new XElement("CurrentValue", counter.CurrentValue),
                    new XElement("Color", counter.Color)
                )
            );

            doc.Save(path);
        }

        public static void UpdateCounter(Guid id, int value)
        {
            CreateFile();

            XDocument doc = XDocument.Load(path);

            foreach(XElement counter in doc.Root!.Elements("Counter"))
            {
                string counterId = counter.Element("ID").Value;

                if (counterId == id.ToString())
                {
                    counter.Element("CurrentValue").Value = value.ToString();
                    break;
                }
            }

            doc.Save(path);
        }

        public static void DeleteCounter(Guid id)
        {
            CreateFile();

            XDocument doc = XDocument.Load(path);

            foreach(XElement counter in doc.Root!.Elements("Counter"))
            {
                string counterId = counter.Element("ID").Value;

                if (counterId == id.ToString())
                {
                    counter.Remove();
                    break;
                }
            }

            doc.Save(path);
        }
    }
}
