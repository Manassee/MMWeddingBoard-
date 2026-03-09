using MudBlazor;
using static MudBlazor.CategoryTypes;
namespace MMWeddingBoard.Shared
{
    public class Theme
    {
        public static MudTheme Create() => new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = new MudBlazor.Utilities.MudColor("#d4af37"),
                PrimaryDarken = "#b8891a",
                PrimaryLighten = "#e8c84a",
                PrimaryContrastText = new MudBlazor.Utilities.MudColor("#3d2b0e"),

                Secondary = new MudBlazor.Utilities.MudColor("#6b4f2a"),
                SecondaryContrastText = new MudBlazor.Utilities.MudColor("#fdf9f2"),

                Background = new MudBlazor.Utilities.MudColor("#fdf9f2"),
                Surface = new MudBlazor.Utilities.MudColor("#ffffff"),
                DrawerBackground = new MudBlazor.Utilities.MudColor("#fdf9f2"),
                AppbarBackground = new MudBlazor.Utilities.MudColor("#fdf9f2"),
                AppbarText = new MudBlazor.Utilities.MudColor("#3d2b0e"),

                TextPrimary = new MudBlazor.Utilities.MudColor("#3d2b0e"),
                TextSecondary = new MudBlazor.Utilities.MudColor("#6b4f2a"),
                TextDisabled = new MudBlazor.Utilities.MudColor("#9c7a4a"),

                Divider = new MudBlazor.Utilities.MudColor("rgba(212,175,55,0.25)"),
                DividerLight = new MudBlazor.Utilities.MudColor("rgba(212,175,55,0.12)"),

                Success = new MudBlazor.Utilities.MudColor("#6aab6a"),
                Warning = new MudBlazor.Utilities.MudColor("#d4af37"),
                Error = new MudBlazor.Utilities.MudColor("#c0392b"),
                Info = new MudBlazor.Utilities.MudColor("#5b7fa6"),

                OverlayDark = (string)new MudBlazor.Utilities.MudColor("rgba(61,43,14,0.5)"),
                OverlayLight = (string)new MudBlazor.Utilities.MudColor("rgba(253,249,242,0.8)"),

                ActionDefault = new MudBlazor.Utilities.MudColor("#b8891a"),
                ActionDisabled = new MudBlazor.Utilities.MudColor("rgba(184,137,26,0.35)"),
                ActionDisabledBackground = new MudBlazor.Utilities.MudColor("rgba(212,175,55,0.12)"),

                HoverOpacity = 0.06,

                TableLines = new MudBlazor.Utilities.MudColor("rgba(212,175,55,0.18)"),
                TableStriped = new MudBlazor.Utilities.MudColor("rgba(245,234,216,0.4)"),
                TableHover = new MudBlazor.Utilities.MudColor("rgba(212,175,55,0.06)"),

                LinesDefault = new MudBlazor.Utilities.MudColor("rgba(212,175,55,0.22)"),
                LinesInputs = new MudBlazor.Utilities.MudColor("rgba(184,137,26,0.45)"),
            },

            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = new[] { "Cormorant Garamond", "Georgia", "serif" },
                    FontSize = "1rem",
                    FontWeight = "400",
                    LineHeight = "1.6",
                    LetterSpacing = "0.01em",
                },
                H1 = new H1Typography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "2.5rem",
                    FontWeight = "400",
                },
                H2 = new H2Typography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "2rem",
                    FontWeight = "400",
                },
                H3 = new H3Typography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "1.6rem",
                    FontWeight = "400",
                },
                H4 = new H4Typography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "1.3rem",
                    FontWeight = "400",
                },
                H5 = new H5Typography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "1.1rem",
                    FontWeight = "400",
                },
                H6 = new H6Typography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "0.95rem",
                    FontWeight = "400",
                },
                Subtitle1 = new Subtitle1Typography
                {
                    FontFamily = new[] { "Cormorant Garamond", "serif" },
                    FontSize = "1.1rem",
                    
                },
                Subtitle2 = new Subtitle2Typography
                {
                    FontFamily = new[] { "Cormorant Garamond", "serif" },
                    FontSize = "0.95rem",
                    
                },
                Body1 = new Body1Typography
                {
                    FontFamily = new[] { "Cormorant Garamond", "serif" },
                    FontSize = "1rem",
                },
                Body2 = new Body2Typography
                {
                    FontFamily = new[] { "Cormorant Garamond", "serif" },
                    FontSize = "0.9rem",
                },
                Button = new ButtonTypography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "0.78rem",
                    FontWeight = "400",
                    LetterSpacing = "0.12em",
                    TextTransform = "uppercase",
                },
                Caption = new CaptionTypography
                {
                    FontFamily = new[] { "Cormorant Garamond", "serif" },
                    FontSize = "0.75rem",
                },
                Overline = new OverlineTypography
                {
                    FontFamily = new[] { "Cinzel", "serif" },
                    FontSize = "0.7rem",
                    LetterSpacing = "0.2em",
                },
            },
        };
    }
}
