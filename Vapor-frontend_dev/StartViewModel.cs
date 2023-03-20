using System.Collections.ObjectModel;

namespace Vapor
{
    public class StartViewModel
    {
        public ObservableCollection<HexagonViewModel> Hexagons { get; } = new ObservableCollection<HexagonViewModel>();

        public StartViewModel()
        {
            // Beispielcode zum Hinzufügen von HexagonViewModels
            Hexagons.Add(new HexagonViewModel(0, 0));
            Hexagons.Add(new HexagonViewModel(0, 1));
            Hexagons.Add(new HexagonViewModel(1, 0));
            Hexagons.Add(new HexagonViewModel(1, 1));
        }
    }

    public class HexagonViewModel
    {
        public int Row { get; }
        public int Column { get; }

        public HexagonViewModel(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
