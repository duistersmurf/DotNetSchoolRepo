using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows;
using System.Data;
using WPFConsole.StoreService;

namespace WPFConsole
{
    class ClientController
    {

       public static Person loginperson { set; get; }
        
        public static NavigationWindow GetWindow(DependencyObject dobj)
        {
            var parent = VisualTreeHelper.GetParent(dobj);
            while (!(parent is NavigationWindow))
                parent = VisualTreeHelper.GetParent(parent);
            if (parent is NavigationWindow)
                return (parent as NavigationWindow);
            else
                return null;
        }

    }
}
