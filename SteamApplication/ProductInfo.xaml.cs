using DataAccessLibrary;
using DataAccessLibrary.EntityFramework;
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

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для ProductInfo.xaml
    /// </summary>
    public partial class ProductInfo : Page
    {
        private Frame MainFrame { get; set; }
        public List<CommentsView> productComments { get; private set; }
        public ProductCommentService Service { get; private set; }
        public double Rating { get; set; }
        public ProductInfo(Frame frame, product selectedProduct)
        {
            InitializeComponent();

            Rating = 50;

            MainFrame = frame;
            Service = new ProductCommentService();
            productComments = Service.SelectProductComments(selectedProduct).Select(p => new CommentsView()
            {
                Text = p.text,
                MarkId = p.comment_mark
            })
            .ToList();

            BitmapImage negativeImg = new BitmapImage(new Uri("C:\\neg.png"));
            BitmapImage positiveImg = new BitmapImage(new Uri("C:\\p.png"));
            BitmapImage neutralImg = new BitmapImage(new Uri("C:\\n.png"));

            foreach (var comment in productComments)
            {
                if (comment.MarkId == 1)
                {
                    Rating += 0.25;
                    comment.ImageMark = positiveImg;
                }
                else if (comment.MarkId == 2)
                {
                    Rating -= 0.35;
                    comment.ImageMark = negativeImg;
                }
                else
                {
                    comment.ImageMark = neutralImg;
                }
            }
            commentsGrid.ItemsSource = productComments;
            TitleDisplay.Text = selectedProduct.name;
            DescriptionDisplay.Text = selectedProduct.description;
            DeveloperDisplay.Text = selectedProduct.developer.name;
            PriceDisplay.Text = "$" + selectedProduct.price.ToString();
            RatignDisplay.Text = "Rating: " + Rating;

        }
    }

    public class CommentsView
    {
        public string Text { get; set; }
        public int MarkId { get; set; }
        public BitmapImage ImageMark { get; set; }
    }
}
