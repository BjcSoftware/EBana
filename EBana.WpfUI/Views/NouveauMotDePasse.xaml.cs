using EBana.WpfUI.ViewModels;
using System.Windows.Controls;

namespace EBana.WpfUI.Views
{
    public partial class NouveauMotDePasse : Page
    {
        public NouveauMotDePasse(NouveauMotDePasseViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
