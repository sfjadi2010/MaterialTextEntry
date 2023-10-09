namespace MaterialTextEntry.TextControl.Controls;

public partial class MatrialEntry : ContentView
{
    private readonly int _yPos;
    private readonly int _xPos;
    private readonly Color _primary;
    public MatrialEntry()
    {
        InitializeComponent();

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            _xPos = -50;
            _yPos = -18;
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            _xPos = -45;
            _yPos = -18;
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS || DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        {

        }

        var rd = App.Current.Resources.MergedDictionaries.First();
        _primary = (Color)rd["Primary"];

        MEEntry.ZIndex = 2;
        MEBorder.ZIndex = 2;
        MELabel.ZIndex = 3;

        BindingContext = this;
    }

    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MatrialEntry), default(string));
    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MatrialEntry), default(string));
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    private void MEEntry_Focused(object sender, FocusEventArgs e)
    {
        // MELabel.IsVisible = false;
        MEBorder.Stroke = _primary;
        MELabel.TextColor = _primary;
        ScaleLabelDown();
    }

    private void MEEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(MEEntry.Text))
        {
            // MELabel.IsVisible = true;
            ScaleLabelUp();
        }
    }

    private void ScaleLabelDown()
    {
        MELabel.ScaleTo(0.8, 250, Easing.Linear);
        MELabel.TranslateTo(_xPos, _yPos, 250, Easing.Linear);
        MELabel.ZIndex = 3;
    }

    private void ScaleLabelUp()
    {
        MELabel.ScaleTo(1, 250, Easing.Linear);
        MELabel.TranslateTo(0, 0, 250, Easing.Linear);
        MELabel.ZIndex = 1;
    }
}