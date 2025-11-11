using Lbr;

namespace maui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnJustStringClicked(object? sender, EventArgs e)
	{
		lblJustString.Text = Some.DoThingyC();
	}

	private async void OnBestGrilClicked(object? sender, EventArgs e)
	{
		using var stream = await FileSystem.OpenAppPackageFileAsync("grils.json");
		using var reader = new StreamReader(stream);
		var jsonFileContents = reader.ReadToEnd();

		lblBestGril.Text = Some.WhoHasTheBestBoobsC(jsonFileContents);
	}
}
