using System.Data.Entity;
using System.Windows;

namespace BindingDataWPF
{
    public partial class MainWindow : Window
    {
        private StudentModelContainer db;
        public MainWindow()
        {
            InitializeComponent();
            db = new StudentModelContainer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.StudentSet.Load();
            db.GroupSet.Load();
            dataGridStudent.ItemsSource = db.StudentSet.Local.ToBindingList<Student>();
            comboBoxGroup.ItemsSource = db.GroupSet.Local.ToBindingList<Group>();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            db.StudentSet.Add(new Student()
            {
                Name = textBoxName.Text,
                Age = textBoxAge.Text,
                Group = comboBoxGroup.SelectedItem as Group
            });
            db.SaveChanges();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudent.SelectedItem != null)
            {
                var student = dataGridStudent.SelectedItem as Student;

                if (student != null)
                {
                    student.Name = textBoxName.Text;
                    student.Age = textBoxAge.Text;
                    student.Group = comboBoxGroup.SelectedItem as Group;
                }
            }
            db.SaveChanges();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudent.SelectedItem!= null)
            {
                var student = dataGridStudent.SelectedItem as Student;

                if (student != null)
                {
                    db.StudentSet.Remove(student);
                    db.SaveChanges();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
