namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.Shapes

type IFabEllipse =
    inherit IFabShape


module Ellipse =

    let WidgetKey = Widgets.register<Ellipse>()

[<AutoOpen>]
module EllipseBuilders =

    type Fabulous.Maui.View with

        static member inline Ellipse<'msg>(strokeThickness: float, strokeLight: Brush, ?strokeDark: Brush) =
            WidgetBuilder<'msg, IFabEllipse>(
                Ellipse.WidgetKey,
                Shape.StrokeThickness.WithValue(strokeThickness),
                Shape.Stroke.WithValue(AppTheme.create strokeLight strokeDark)
            )

[<Extension>]
type EllipseModifiers =
    /// <summary>Link a ViewRef to access the direct Ellipse control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabEllipse>, value: ViewRef<Ellipse>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
