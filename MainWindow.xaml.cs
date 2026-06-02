using System;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace API

{
    public partial class MainWindow : Window
    {

        private string snils = "";

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void GetDataButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    string url = "http://localhost:4444/TransferSimulator/snils";

                    string jsonAnswer = await client.GetStringAsync(url);




                    jsonAnswer = jsonAnswer.Replace("{", "");
                    jsonAnswer = jsonAnswer.Replace("}", "");
                    jsonAnswer = jsonAnswer.Replace("\"", "");
                    jsonAnswer = jsonAnswer.Replace("value :", "");

                    snils = jsonAnswer.Trim();


                    txtBoxFullNameText.Text = snils;


                    txtBoxResultText.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(snils))
            {
                txtBoxResultText.Text = "Сначала получите данные";


                return;
            }




            string forbiddenSymbols = "0123456789!@#$%^&*():;_-+=[]{}<>?/|\\&";



            if (snils.Intersect(forbiddenSymbols).Count() > 0)
            {
                txtBoxResultText.Text = "СНИЛС не корректен";
                return;
            }

            txtBoxResultText.Text = "СНИЛС корректен";
        }
    }
}