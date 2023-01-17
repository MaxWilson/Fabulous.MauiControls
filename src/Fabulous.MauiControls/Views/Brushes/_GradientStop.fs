namespace Fabulous.Maui

open Microsoft.Maui.Controls

open Fabulous

type IFabGradientStop =
    inherit IFabElement

module GradientStop =

    let WidgetKey = Widgets.register<GradientStop>()

    let Color = Attributes.defineBindableAppThemeColor GradientStop.ColorProperty

    let Offset = Attributes.defineBindableFloat GradientStop.OffsetProperty

[<AutoOpen>]
module GradientStopBuilders =
    type Fabulous.Maui.View with

        /// <summary>GradientStop objects to the LinearGradientBrush.GradientStops collection, that specify the colors in the gradient and their positions.</summary>
        /// <param name="light">The color in light theme.</param>
        /// <param name="dark">The color in dark theme.</param>
        static member inline GradientStop(offset: float, light: FabColor, ?dark: FabColor) =
            WidgetBuilder<'msg, IFabGradientStop>(
                GradientStop.WidgetKey,
                GradientStop.Color.WithValue(AppTheme.create light dark),
                GradientStop.Offset.WithValue(offset)
            )
