﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Talent.WpfClient
{
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unexpected Exception",
                MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = false;
        }
    }
}
