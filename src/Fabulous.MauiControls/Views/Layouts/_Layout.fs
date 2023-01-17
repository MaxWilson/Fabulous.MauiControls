namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabLayout =
    inherit IFabView

module Layout =
    let Padding =
        Attributes.defineBindableWithEquality<Thickness> Layout.PaddingProperty

    let CascadeInputTransparent =
        Attributes.defineBindableBool Layout.CascadeInputTransparentProperty

    let IsClippedToBounds =
        Attributes.defineBindableBool Layout.IsClippedToBoundsProperty

// let LayoutChanged =
//     Attributes.defineEventNoArg "Layout_LayoutChanged" (fun target -> (target :?> Layout).LayoutChanged)

[<Extension>]
type LayoutModifiers =
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabLayout>, value: Thickness) =
        this.AddScalar(Layout.Padding.WithValue(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabLayout>, value: float) =
        LayoutModifiers.padding(this, Thickness(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabLayout>, left: float, top: float, right: float, bottom: float) =
        LayoutModifiers.padding(this, Thickness(left, top, right, bottom))

    [<Extension>]
    static member inline cascadeInputTransparent(this: WidgetBuilder<'msg, #IFabLayout>, value: bool) =
        this.AddScalar(Layout.CascadeInputTransparent.WithValue(value))

    [<Extension>]
    static member inline isClippedToBounds(this: WidgetBuilder<'msg, #IFabLayout>, value: bool) =
        this.AddScalar(Layout.IsClippedToBounds.WithValue(value))

// [<Extension>]
// static member inline onLayoutChanged(this: WidgetBuilder<'msg, #ILayout>, value: 'msg) =
//     this.AddScalar(Layout.LayoutChanged.WithValue(value))
