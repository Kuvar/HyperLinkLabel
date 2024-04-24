# You need to add the following namepsace to your xaml files:
xmlns:control="clr-namespace:HyperLinkLabel;assembly=HyperLinkLabel"

# Finally you are able to use the "LinksLabel" in your xaml files:
<control:LinksLabel HorizontalTextAlignment="Center"
                   LinksText="This MAUI Forums question can be visited at https://learn.microsoft.com/en-us/answers/tags/247/dotnet-maui but Google only at https://www.google.com/"
                   TextColor="DarkGray" />
