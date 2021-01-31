using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Schedule_
{
    /// <summary>
    /// Логика взаимодействия для TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        int id_holder;

        public TeachersWindow()
        {
            InitializeComponent();
            find_dialog.Visibility = Visibility.Hidden;
            add_teacher_dialog.Visibility = Visibility.Hidden;
            citates_portraits.Visibility = Visibility.Hidden;
        }

        private void teachers_list_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (teachers_list.SelectedItem != null && teachers_list.SelectedItem.ToString() != "пустовато")
            {
                citates_portraits.Visibility = Visibility.Visible;
                using (var context = new ContextDB())
                {
                    var teachers = context.Teachers;

                    var dat = teachers_list.Items[teachers_list.SelectedIndex].ToString();

                    foreach (Teacher teacher in teachers
                        .Where(t => t.Name.StartsWith(dat)))
                    {
                        id_holder = teacher.Id;
                        teacher_header.Text = teacher.Name;
                    }
                }
                using (var context = new ContextDB())
                {
                    var descriptions = context.Descriptions;
                    foreach(Description description in descriptions
                        .Where(t => t.TeacherId == id_holder))
                    {
                        if(description.Portrait != null)
                        {
                            portraits.Items.Add(description.Portrait);
                        }
                        if (description.Citate != null)
                        {
                            citates.Items.Add(description.Citate);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Станет кликабельным, когда вы добавите препода");
            }
        }

        private void add_teacher_Click(object sender, RoutedEventArgs e)
        {
            teachers_list.Items.Clear();
            teacher_name.Visibility = Visibility.Visible;
            teacher_rating.Visibility = Visibility.Visible;
            confirm_add.Visibility = Visibility.Visible;
            find_dialog.Visibility = Visibility.Hidden;
            add_teacher_dialog.Visibility = Visibility.Visible;
        }

        private void find_teacher_Click(object sender, RoutedEventArgs e)
        {
            find_dialog.Visibility = Visibility.Visible;
            add_teacher_dialog.Visibility = Visibility.Hidden;
        }

        private void view_teachers_Click(object sender, RoutedEventArgs e)
        {
            teachers_list.Items.Clear();
            find_dialog.Visibility = Visibility.Hidden;
            add_teacher_dialog.Visibility = Visibility.Hidden;
            teachers_list.Visibility = Visibility.Visible;
            using (var context = new ContextDB())
            {
                foreach (Teacher teacher in context.Teachers)
                {
                    teachers_list.Items.Add(teacher.Name);
                }
                if (teachers_list.Items.IsEmpty == true)
                {
                    teachers_list.Items.Add("пустовато");
                }
            }
        }

        private void confirm_add_Click(object sender, RoutedEventArgs e)
        {
            if(teacher_name.Text != "" && teacher_rating.Text != "")
            {
                using (var context = new ContextDB())
                {
                    var teacher = new Teacher()
                    {
                        Name = teacher_name.Text,
                        Rating = double.Parse(teacher_rating.Text)
                    };

                    teacher_name.Text = "";
                    teacher_rating.Text = "";

                    context.Teachers.Add(teacher);
                    context.SaveChanges();

                    MessageBox.Show($"id: {teacher.Id} name: {teacher.Name}");
                }
            }
            else
            {
                MessageBox.Show("Еще раз попробуй");
            }
            
        }

        private void confirm_find_Click(object sender, RoutedEventArgs e)
        {
            find_results.Items.Clear();
            add_teacher_dialog.Visibility = Visibility.Hidden;
            using (var context = new ContextDB())
            {
                var teachers = context.Teachers;
                

                foreach (Teacher teacher in teachers
                    .Where(t => t.Name.StartsWith(name_find.Text)))
                {
                    find_results.Items.Add(teacher.Name);
                    
                }
                if(find_results.Items.IsEmpty == true)
                {
                    find_results.Items.Add("пустовато");
                }
            }
        }

        private void find_results_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (find_results.SelectedItem != null && find_results.SelectedItem.ToString() != "пустовато")
            {
                citates_portraits.Visibility = Visibility.Visible;
                using (var context = new ContextDB())
                {
                    var teachers = context.Teachers;

                    var dat = find_results.Items[find_results.SelectedIndex].ToString();

                    foreach (Teacher teacher in teachers
                        .Where(t => t.Name.StartsWith(dat)))
                    {
                        id_holder = teacher.Id;
                        teacher_header.Text = teacher.Name;
                    }
                }
                using (var context = new ContextDB())
                {
                    var descriptions = context.Descriptions;
                    foreach (Description description in descriptions
                        .Where(t => t.TeacherId == id_holder))
                    {
                        if (description.Portrait != null)
                        {
                            portraits.Items.Add(description.Portrait);
                        }
                        if (description.Citate != null)
                        {
                            citates.Items.Add(description.Citate);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Станет кликабельным, когда вы добавите препода");
            }
        }

        private void send_portrait_Click(object sender, RoutedEventArgs e)
        {
            if(portrait_text.Text != "" && id_holder != null)
            {
                using (var context = new ContextDB())
                {
                    if (context.Teachers.Count() != 0)
                    {
                        var description = new Description()
                        {
                            TeacherId = id_holder,
                            Citate = portrait_text.Text
                        };
                        portrait_text.Text = "";
                        context.Descriptions.Add(description);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Странно, но преподаватели отсутствуют");
                    }

                }
            }
        }

        private void send_citate_Click(object sender, RoutedEventArgs e)
        {
            if (citate_text.Text != "")
            {
                using (var context = new ContextDB())
                {
                    if(context.Teachers.Count() != 0)
                    {
                        var description = new Description()
                        {
                            TeacherId = id_holder,
                            Citate = citate_text.Text
                        };
                        citate_text.Text = "";
                        context.Descriptions.Add(description);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Странно, но преподаватели отсутствуют");
                    }

                }
            }
        }

        private void exit_citates_portraits_Click(object sender, RoutedEventArgs e)
        {
            citates.Items.Clear();
            portraits.Items.Clear();
            citates_portraits.Visibility = Visibility.Hidden;
        }
    }
}
