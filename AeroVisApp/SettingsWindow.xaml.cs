using System.Windows;
using Wpf.Ui.Controls;

namespace AeroVisApp;

public partial class SettingsWindow : FluentWindow
{
    public SettingsWindow()
    {
        InitializeComponent();
        ApplySettingsIconTheme(ThemeManager.IsDark);
        ThemeManager.ThemeChanged += ApplySettingsIconTheme;
    }

    private void ApplySettingsIconTheme(bool isDark)
    {
        var p = isDark ? "pack://application:,,,/res/settings/dark/" : "pack://application:,,,/res/settings/";
        Uri U(string name) => new($"{p}{name}");
        SettingsNavGeneral.Source = U("settings.svg");
        SettingsNavDevice.Source  = U("fan.svg");
        SettingsNavData.Source    = U("chart-line.svg");
        SettingsNavAbout.Source   = U("info.svg");

        var wp = isDark ? "pack://application:,,,/res/window/dark/" : "pack://application:,,,/res/window/";
        DeviceDropletIcon.Source = new Uri($"{wp}droplet.svg");
    }

    private void SettingsNavButton_Checked(object sender, RoutedEventArgs e)
    {
        if (PageGeneral == null) return;
        PageGeneral.Visibility  = sender == NavGeneral  ? Visibility.Visible : Visibility.Collapsed;
        PageDevice.Visibility   = sender == NavDevice   ? Visibility.Visible : Visibility.Collapsed;
        PageData.Visibility     = sender == NavData     ? Visibility.Visible : Visibility.Collapsed;
        PageAbout.Visibility    = sender == NavAbout    ? Visibility.Visible : Visibility.Collapsed;
    }

    private void DarkModeToggle_Changed(object sender, RoutedEventArgs e)
    {
        ThemeManager.Apply(DarkModeToggle.IsChecked == true);
    }

    private void BottomBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        e.Handled = true;
    }

    private void OpenWebsiteButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        e.Handled = true;
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "https://www.aerovis.org/",
            UseShellExecute = true
        });
    }

    private void CloseButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        e.Handled = true;
        Close();
    }
}
