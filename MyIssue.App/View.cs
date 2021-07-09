using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyIssue.App
{
    class View
    {
        public BitmapImage Image { get; private set; }
        public View(string path)
        {
            Image = new BitmapImage(new Uri(path));
        }
    }
}
