using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HtmlAgilityPack;
using MaterialDesignThemes;

namespace Schedule_
{
    /// <summary>
    /// Логика взаимодействия для ScheduleWindows.xaml
    /// </summary>
    public partial class ScheduleWindows : Window
    {
        public ScheduleWindows()
        {
            InitializeComponent();
        }

        private void schedf_click(object sender, RoutedEventArgs e)
        {
            //для теста можно использовать группу 221201
            var html = @"http://schedule.tsu.tula.ru/?group=" + group.Text;

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            HtmlNodeCollection page = htmlDoc.DocumentNode.SelectNodes("//table[@class='tt']");

            if(page == null)
            {
                MessageBox.Show("Ошибочка, попробуй еще раз");
            }
            else
            {
                string table_schedule = page[1].InnerHtml;
                page[0].Remove();
                page[1].Remove();

                var doc = new HtmlDocument();
                doc.LoadHtml(table_schedule);

                HtmlNodeCollection a = doc.DocumentNode.SelectNodes("tr");
                foreach (var tag in a)
                {
                    if (tag.SelectSingleNode("td[@colspan='2']") != null)
                    {
                        raspis.Items.Add("\t\t" + tag.InnerText);
                    }
                    else if (tag.SelectSingleNode("td[@class='time']") != null)
                    {

                        raspis.Items.Add(tag.SelectSingleNode("td[@class='time']").InnerText + " " + tag.SelectSingleNode("td/div/span").InnerText + "\t" + tag.SelectSingleNode("td/div/span[@class='aud']/a").InnerText + "\n" + tag.SelectSingleNode("td/div/span[@class='aud']/div").InnerText);

                    }
                }
            }   
        }


        private void sched_exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
