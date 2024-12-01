using System.Windows;
using AoCWPF.Solutions;

namespace AoCWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDay1_Click(object sender, RoutedEventArgs e)
        {
            var solution = new Day1();
            var resultPart1 = solution.Part1();
            var resultPart2 = solution.Part2();
            
            MessageBox.Show($"Day 1 \npart1: {resultPart1} \npart2: {resultPart2}");
        }
    }
}