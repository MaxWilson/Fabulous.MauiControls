namespace Fabulous.Maui

open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.Shapes

type IShape =
    inherit Fabulous.Maui.IView

module Shape =

    let Fill =
        Attributes.defineBindableAppTheme<Brush> Shape.FillProperty

    let FillWidget =
        Attributes.defineBindableWidget Shape.FillProperty

    let Stroke =
        Attributes.defineBindableAppTheme<Brush> Shape.StrokeProperty

    let StrokeThickness =
        Attributes.defineBindableFloat Shape.StrokeThicknessProperty

    let StrokeDashArrayString =
        Attributes.defineSimpleScalarWithEquality<string>
            "Shape_StrokeDashArrayString"
            (fun _ newValueOpt node ->
                let target = node.Target :?> BindableObject

                match newValueOpt with
                | ValueNone -> target.ClearValue(Shape.StrokeDashArrayProperty)
                | ValueSome string ->
                    target.SetValue(
                        Shape.StrokeDashArrayProperty,
                        DoubleCollectionConverter()
                            .ConvertFromInvariantString(string)
                    ))

    let StrokeDashArrayList =
        Attributes.defineSimpleScalarWithEquality<float list>
            "Shape_StrokeDashArrayList"
            (fun _ newValueOpt node ->
                let target = node.Target :?> BindableObject

                match newValueOpt with
                | ValueNone -> target.ClearValue(Shape.StrokeDashArrayProperty)
                | ValueSome points ->
                    let coll = DoubleCollection()
                    points |> List.iter coll.Add
                    target.SetValue(Shape.StrokeDashArrayProperty, coll))

    let StrokeDashOffset =
        Attributes.defineBindableFloat Shape.StrokeDashOffsetProperty

    let StrokeLineCap =
        Attributes.defineBindableWithEquality<PenLineCap> Shape.StrokeLineCapProperty

    let StrokeLineJoin =
        Attributes.defineBindableWithEquality<PenLineJoin> Shape.StrokeLineJoinProperty

    let StrokeMiterLimit =
        Attributes.defineBindableFloat Shape.StrokeMiterLimitProperty

    let Aspect =
        Attributes.defineBindableWithEquality<Stretch> Shape.AspectProperty


[<Extension>]
type ShapeModifiers =

    [<Extension>]
    static member inline fill(this: WidgetBuilder<'msg, #IShape>, light: Brush, ?dark: Brush) =
        this.AddScalar(Shape.Fill.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline fill<'msg, 'marker, 'contentMarker when 'marker :> IShape and 'contentMarker :> IBrush>
        (
            this: WidgetBuilder<'msg, 'marker>,
            content: WidgetBuilder<'msg, 'contentMarker>
        ) =
        this.AddWidget(Shape.FillWidget.WithValue(content.Compile()))

    [<Extension>]
    static member inline stroke(this: WidgetBuilder<'msg, #IShape>, light: Brush, ?dark: Brush) =
        this.AddScalar(Shape.Stroke.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline strokeThickness(this: WidgetBuilder<'msg, #IShape>, value: float) =
        this.AddScalar(Shape.StrokeThickness.WithValue(value))

    [<Extension>]
    static member inline strokeDashArray(this: WidgetBuilder<'msg, #IShape>, value: string) =
        this.AddScalar(Shape.StrokeDashArrayString.WithValue(value))

    [<Extension>]
    static member inline strokeDashArray(this: WidgetBuilder<'msg, #IShape>, value: float list) =
        this.AddScalar(Shape.StrokeDashArrayList.WithValue(value))

    [<Extension>]
    static member inline strokeDashOffset(this: WidgetBuilder<'msg, #IShape>, value: float) =
        this.AddScalar(Shape.StrokeDashOffset.WithValue(value))

    [<Extension>]
    static member inline strokeLineCap(this: WidgetBuilder<'msg, #IShape>, value: PenLineCap) =
        this.AddScalar(Shape.StrokeLineCap.WithValue(value))

    [<Extension>]
    static member inline strokeLineJoin(this: WidgetBuilder<'msg, #IShape>, value: PenLineJoin) =
        this.AddScalar(Shape.StrokeLineJoin.WithValue(value))

    [<Extension>]
    static member inline strokeMiterLimit(this: WidgetBuilder<'msg, #IShape>, value: float) =
        this.AddScalar(Shape.StrokeMiterLimit.WithValue(value))

    [<Extension>]
    static member inline aspect(this: WidgetBuilder<'msg, #IShape>, value: Stretch) =
        this.AddScalar(Shape.Aspect.WithValue(value))
