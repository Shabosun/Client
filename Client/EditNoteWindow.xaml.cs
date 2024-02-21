using Client.Model;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для EditNoteWindow.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        public Note Note { get; private set; }
        public EditNoteWindow(Note note)
        {
            InitializeComponent();
            Note = note;
            DataContext = Note;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
