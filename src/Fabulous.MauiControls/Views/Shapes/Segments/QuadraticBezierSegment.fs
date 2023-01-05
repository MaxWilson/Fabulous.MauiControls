namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls.Shapes
open Microsoft.Maui.Graphics

type IQuadraticBezierSegment =
    inherit Fabulous.Maui.IPathSegment

module QuadraticBezierSegment =
    let WidgetKey = Widgets.register<QuadraticBezierSegment>()

    let Point1 =
        Attributes.defineBindableWithEquality<Point> QuadraticBezierSegment.Point1Property

    let Point2 =
        Attributes.defineBindableWithEquality<Point> QuadraticBezierSegment.Point2Property

[<AutoOpen>]
module QuadraticBezierSegmentBuilders =

    type Fabulous.Maui.View with

        static member inline QuadraticBezierSegment<'msg>(point1: Point, point2: Point) =
            WidgetBuilder<'msg, IQuadraticBezierSegment>(
                QuadraticBezierSegment.WidgetKey,
                QuadraticBezierSegment.Point1.WithValue(point1),
                QuadraticBezierSegment.Point2.WithValue(point2)
            )
