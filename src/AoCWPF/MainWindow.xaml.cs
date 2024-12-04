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
            var solutionPart1 = new Day1(1, 1);
            var resultPart1 = solutionPart1.Part1();

            var solutionPart2 = new Day1(1, 2);
            var resultPart2 = solutionPart2.Part2();
            
            MessageBox.Show($"Day 1 \npart1: {resultPart1} \npart2: {resultPart2}");
        }

        private void btnDay2_Click(object sender, RoutedEventArgs e)
        {
            var solutionPart1 = new Day2(2, 1);
            var resultPart1 = solutionPart1.Part1();

            var solutionPart2 = new Day2(2, 2);
            var resultPart2 = solutionPart2.Part2();

            MessageBox.Show($"Day 2 \npart1: {resultPart1} \npart2: {resultPart2}");
        }

        private void btnDay3_Click(object sender, RoutedEventArgs e)
        {
            var solutionPart1 = new Day3(3, 1);
            var resultPart1 = solutionPart1.Part1();

            var solutionPart2 = new Day3(3, 2);
            var resultPart2 = solutionPart2.Part2();

            MessageBox.Show($"Day 3 \npart1: {resultPart1} \npart2: {resultPart2}");
        }

        private void btnDay4_Click(object sender, RoutedEventArgs e)
        {
            var solutionPart1 = new Day4(4, 1);
            var resultPart1 = solutionPart1.Part1();

            var solutionPart2 = new Day4(4, 2);
            var resultPart2 = solutionPart2.Part2();

            MessageBox.Show($"Day 4 \npart1: {resultPart1} \npart2: {resultPart2}");
        }
    }
}