namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls.Shapes
open Microsoft.Maui.Graphics

type IFabRoundRectangleGeometry =
    inherit IFabGeometryGroup

module RoundRectangleGeometry =

    let WidgetKey = Widgets.register<RoundRectangleGeometry>()

    let CornerRadius =
        Attributes.defineBindableWithEquality<CornerRadius> RoundRectangleGeometry.CornerRadiusProperty

    let Rect =
        Attributes.defineBindableWithEquality<Rect> RoundRectangleGeometry.RectProperty

    let FillRule =
        Attributes.defineBindableEnum<FillRule> RoundRectangleGeometry.FillRuleProperty

[<AutoOpen>]
module RoundRectangleGeometryBuilders =
    type Fabulous.Maui.View with

        [<Obsolete("Use RoundRectangleGeometry(cornerRadius: CornerRadius, rect: Rect) instead")>]
        static member inline RoundRectangleGeometry<'msg>(cornerRadius: float, rect: Rect) =
            WidgetBuilder<'msg, IFabRoundRectangleGeometry>(
                RoundRectangleGeometry.WidgetKey,
                RoundRectangleGeometry.CornerRadius.WithValue(CornerRadius(cornerRadius)),
                RoundRectangleGeometry.Rect.WithValue(rect)
            )

        static member inline RoundRectangleGeometry<'msg>(cornerRadius: CornerRadius, rect: Rect) =
            WidgetBuilder<'msg, IFabRoundRectangleGeometry>(
                RoundRectangleGeometry.WidgetKey,
                RoundRectangleGeometry.CornerRadius.WithValue(cornerRadius),
                RoundRectangleGeometry.Rect.WithValue(rect)
            )

[<Extension>]
type RoundRectangleGeometryModifiers =

    [<Extension>]
    static member inline fillRule(this: WidgetBuilder<'msg, #IFabRoundRectangleGeometry>, value: FillRule) =
        this.AddScalar(RoundRectangleGeometry.FillRule.WithValue(value))
