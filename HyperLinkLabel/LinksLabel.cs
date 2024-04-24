using System.Windows.Input;
//[assembly: XmlnsDefinition("http://www.pankaj_singh_apps.de/maui/controls", "HyperLinkLabel")]
namespace HyperLinkLabel
{
    // All the code in this file is included in all platforms.
    public class LinksLabel : Label
    {
        public static BindableProperty LinksTextProperty = BindableProperty.Create(nameof(LinksText), typeof(string), typeof(LinksLabel), propertyChanged: OnLinksTextPropertyChanged);
        [Obsolete]
        private readonly ICommand _linkTapGesture = new Command<string>((url) =>
        {
            Uri uri = new Uri(url);
            Task.Run(async () =>
            {
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            });
        });

        public string LinksText
        {
            get => GetValue(LinksTextProperty) as string;
            set => SetValue(LinksTextProperty, value);
        }

        private void SetFormattedText()
        {
            var formattedString = new FormattedString();

            if (!string.IsNullOrEmpty(LinksText))
            {
                var splitText = LinksText.Split(' ');

                foreach (string textPart in splitText)
                {
                    var span = new Span { Text = $"{textPart} " };

                    if (IsUrl(textPart)) // a link  
                    {
                        span.TextColor = Microsoft.Maui.Graphics.Colors.DeepSkyBlue;
                        span.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = _linkTapGesture,
                            CommandParameter = textPart
                        });
                    }

                    formattedString.Spans.Add(span);
                }
            }

            this.FormattedText = formattedString;
        }

        private bool IsUrl(string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
              (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private static void OnLinksTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var linksLabel = bindable as LinksLabel;
            linksLabel.SetFormattedText();
        }
    }
}
