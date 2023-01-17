namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls

type IFabActivityIndicator =
    inherit IFabView

module ActivityIndicator =
    let WidgetKey = Widgets.register<ActivityIndicator>()

    let IsRunning = Attributes.defineBindableBool ActivityIndicator.IsRunningProperty

    let Color = Attributes.defineBindableAppThemeColor ActivityIndicator.ColorProperty

[<AutoOpen>]
module ActivityIndicatorBuilders =
    type Fabulous.Maui.View with

        static member inline ActivityIndicator<'msg>(isRunning: bool) =
            WidgetBuilder<'msg, IFabActivityIndicator>(ActivityIndicator.WidgetKey, ActivityIndicator.IsRunning.WithValue(isRunning))

[<Extension>]
type ActivityIndicatorModifiers =

    /// <summary>Sets the activity indicator color.</summary>
    /// <param name="light">The color of the activity indicator in the light theme.</param>
    /// <param name="dark">The color of the activity indicator in the dark theme.</param>
    [<Extension>]
    static member inline color(this: WidgetBuilder<'msg, #IFabActivityIndicator>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(ActivityIndicator.Color.WithValue(AppTheme.create light dark))

    /// <summary>Link a ViewRef to access the direct ActivityIndicator control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabActivityIndicator>, value: ViewRef<ActivityIndicator>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
