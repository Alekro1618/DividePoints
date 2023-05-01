using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace DividePoints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush green = new SolidColorBrush();
        SolidColorBrush blue = new SolidColorBrush();

        List<Point> b_points = new List<Point>();
        List<Point> g_points = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
            green.Color = Color.FromArgb(255, 85, 177, 85);
            blue.Color = Color.FromArgb(255, 62, 132, 245);
        }

        public void AddPointGreen(object content, MouseButtonEventArgs e)
        {
            Ellipse p = new Ellipse();
            
            p.Fill = green;
            p.Width = 20;
            p.Height = 20;
            Point click = e.GetPosition(Field);
            p.RenderTransform = new TranslateTransform(click.X-10, click.Y-10);
            Field.Children.Add(p);
            e.Handled = true;
            g_points.Add(click);
            DrawPlane();
        }

        public void AddPointBlue(object content, MouseButtonEventArgs e)
        {
            Ellipse p = new Ellipse();

            p.Fill = blue;
            p.Width = 20;
            p.Height = 20;
            Point click = e.GetPosition(Field);
            p.RenderTransform = new TranslateTransform(click.X - 10, click.Y - 10);
            Field.Children.Add(p);
            e.Handled = true;
            b_points.Add(click);
            DrawPlane();
        }

        public void DrawPlane()
        {
            Back.Children.Clear();
            float frq = 20;
            Random n = new Random();
            for (int i = 0; i<800/frq; i++)
            {
                for (int j = 0; j<434/frq; j++)
                {
                    Rectangle pixel = new Rectangle();
                    pixel.Height = frq;
                    pixel.Width = frq;
                    SolidColorBrush grad = new SolidColorBrush();
                    grad.Color = FindClosest(new Point(i * frq, j * frq));
                    pixel.Fill = grad;
                    pixel.RenderTransform = new TranslateTransform(i * frq, j * frq);
                    Back.Children.Add(pixel);
                }
            }
        }

        // private double metric(Point a, Point b) => Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
        private double metric(Point a, Point b) => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y,2));

        public Color FindClosest(Point p)
        {
            double min_dist_g = double.PositiveInfinity;
            double min_dist_b = double.PositiveInfinity;
            foreach (Point g in g_points)
            {
                double dist = metric(g, p);
                if (dist < min_dist_g)
                {
                    min_dist_g = dist;
                }
            }
            foreach (Point b in b_points)
            {
                double dist = metric(b, p);
                if (dist < min_dist_b)
                {
                    min_dist_b = dist;
                }
            }
            return Color.FromArgb(255, 0, (byte)(min_dist_b / 800 * 255), (byte)(min_dist_g/800 *255));
        }
    }
}
 