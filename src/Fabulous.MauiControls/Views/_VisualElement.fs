namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui
open Microsoft.Maui.Controls
open Microsoft.Maui.Graphics

type IFabVisualElement =
    inherit IFabNavigableElement

[<Struct>]
type TranslateToData =
    { X: float
      Y: float
      AnimationDuration: uint32
      Easing: Easing }

[<Struct>]
type ScaleToData =
    { Scale: float
      AnimationDuration: uint32
      Easing: Easing }

[<Struct>]
type FadeToData =
    { Opacity: float
      AnimationDuration: uint32
      Easing: Easing }

[<Struct>]
type RotateToData =
    { Rotation: float
      AnimationDuration: uint32
      Easing: Easing }

module VisualElementUpdaters =
    let updateVisualElementFocus oldValueOpt (newValueOpt: ValueEventData<bool, bool> voption) (node: IViewNode) =
        let target = node.Target :?> VisualElement

        let onEventName = $"Focus_On"
        let onEvent = target.Focused

        let offEventName = $"Focus_Off"
        let offEvent = target.Unfocused

        match newValueOpt with
        | ValueNone ->
            // The attribute is no longer applied, so we clean up the events
            match node.TryGetHandler(onEventName) with
            | ValueNone -> ()
            | ValueSome handler -> onEvent.RemoveHandler(handler)

            match node.TryGetHandler(offEventName) with
            | ValueNone -> ()
            | ValueSome handler -> offEvent.RemoveHandler(handler)

            // Only clear the property if a value was set before
            match oldValueOpt with
            | ValueNone -> ()
            | ValueSome _ -> target.Unfocus()

        | ValueSome curr ->
            // Clean up the old event handlers if any
            match node.TryGetHandler(onEventName) with
            | ValueNone -> ()
            | ValueSome handler -> onEvent.RemoveHandler(handler)

            match node.TryGetHandler(offEventName) with
            | ValueNone -> ()
            | ValueSome handler -> offEvent.RemoveHandler(handler)

            // Set the new value
            if curr.Value then
                target.Focus() |> ignore
            else
                target.Unfocus()

            // Set the new event handlers
            let onHandler =
                EventHandler<FocusEventArgs>(fun _ args ->
                    let r = curr.Event true
                    Dispatcher.dispatch node r)

            node.SetHandler(onEventName, ValueSome onHandler)
            onEvent.AddHandler(onHandler)

            let offHandler =
                EventHandler<FocusEventArgs>(fun _ args ->
                    let r = curr.Event false
                    Dispatcher.dispatch node r)

            node.SetHandler(offEventName, ValueSome offHandler)
            offEvent.AddHandler(offHandler)

module VisualElement =
    let AnchorX = Attributes.defineBindableFloat VisualElement.AnchorXProperty

    let AnchorY = Attributes.defineBindableFloat VisualElement.AnchorYProperty

    let BackgroundColor =
        Attributes.defineBindableAppThemeColor VisualElement.BackgroundColorProperty

    let Background =
        Attributes.defineBindableWithEquality VisualElement.BackgroundProperty

    let BackgroundWidget =
        Attributes.defineBindableWidget VisualElement.BackgroundProperty

    let Clip = Attributes.defineBindableWidget VisualElement.ClipProperty

    let FlowDirection =
        Attributes.defineBindableEnum<FlowDirection> VisualElement.FlowDirectionProperty

    let HeightRequest =
        Attributes.defineBindableFloat VisualElement.HeightRequestProperty

    let WidthRequest = Attributes.defineBindableFloat VisualElement.WidthRequestProperty

    let InputTransparent =
        Attributes.defineBindableBool VisualElement.InputTransparentProperty

    let IsEnabled = Attributes.defineBindableBool VisualElement.IsEnabledProperty

    let IsVisible = Attributes.defineBindableBool VisualElement.IsVisibleProperty

    let MinimumHeightRequest =
        Attributes.defineBindableFloat VisualElement.MinimumHeightRequestProperty

    let MinimumWidthRequest =
        Attributes.defineBindableFloat VisualElement.MinimumWidthRequestProperty

    let MaximumHeightRequest =
        Attributes.defineBindableFloat VisualElement.MaximumHeightRequestProperty

    let MaximumWidthRequest =
        Attributes.defineBindableFloat VisualElement.MaximumHeightRequestProperty

    let Opacity = Attributes.defineBindableFloat VisualElement.OpacityProperty

    let Visual =
        Attributes.defineBindableWithEquality<IVisual> VisualElement.VisualProperty

    let FocusWithEvent =
        Attributes.defineSimpleScalar "VisualElement_FocusWithEvent" ScalarAttributeComparers.noCompare VisualElementUpdaters.updateVisualElementFocus

    let TranslateTo =
        Attributes.defineSimpleScalarWithEquality<TranslateToData> "View_TranslateTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.TranslateTo(0., 0., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.TranslateTo(data.X, data.Y, data.AnimationDuration, data.Easing) |> ignore)

    let TranslationX = Attributes.defineBindableFloat VisualElement.TranslationXProperty

    let TranslationY = Attributes.defineBindableFloat VisualElement.TranslationYProperty

    let Shadow = Attributes.defineBindableWidget VisualElement.ShadowProperty

    let ScaleX = Attributes.defineBindableFloat VisualElement.ScaleXProperty

    let ScaleTo =
        Attributes.defineSimpleScalarWithEquality<ScaleToData> "View_ScaleTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.ScaleTo(1., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.ScaleTo(data.Scale, data.AnimationDuration, data.Easing) |> ignore)

    let ScaleXTo =
        Attributes.defineSimpleScalarWithEquality<ScaleToData> "View_ScaleXTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.ScaleXTo(1., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.ScaleXTo(data.Scale, data.AnimationDuration, data.Easing) |> ignore)

    let ScaleY = Attributes.defineBindableFloat VisualElement.ScaleYProperty

    let ZIndex = Attributes.defineBindableInt VisualElement.ZIndexProperty

    let ScaleYTo =
        Attributes.defineSimpleScalarWithEquality<ScaleToData> "View_ScaleYTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.ScaleYTo(1., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.ScaleYTo(data.Scale, data.AnimationDuration, data.Easing) |> ignore)

    let FadeTo =
        Attributes.defineSimpleScalarWithEquality<FadeToData> "View_FadeTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.FadeTo(0., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.FadeTo(data.Opacity, data.AnimationDuration, data.Easing) |> ignore)

    let RotateTo =
        Attributes.defineSimpleScalarWithEquality<RotateToData> "View_RotateTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.RotateTo(0., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.RotateTo(data.Rotation, data.AnimationDuration, data.Easing) |> ignore)

    let RotateXTo =
        Attributes.defineSimpleScalarWithEquality<RotateToData> "View_RotateXTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.RotateXTo(0., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.RotateXTo(data.Rotation, data.AnimationDuration, data.Easing) |> ignore)

    let RotateYTo =
        Attributes.defineSimpleScalarWithEquality<RotateToData> "View_RotateYTo" (fun _ newValueOpt node ->
            let view = node.Target :?> View

            match newValueOpt with
            | ValueNone -> view.RotateYTo(0., uint 0, Easing.Linear) |> ignore
            | ValueSome data -> view.RotateYTo(data.Rotation, data.AnimationDuration, data.Easing) |> ignore)

[<Extension>]
type VisualElementModifiers =

    [<Extension>]
    static member inline anchorX(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.AnchorX.WithValue(value))

    [<Extension>]
    static member inline anchorY(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.AnchorY.WithValue(value))

    [<Extension>]
    static member inline backgroundColor(this: WidgetBuilder<'msg, #IFabVisualElement>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(VisualElement.BackgroundColor.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline background(this: WidgetBuilder<'msg, #IFabVisualElement>, value: Brush) =
        this.AddScalar(VisualElement.Background.WithValue(value))

    [<Extension>]
    static member inline background(this: WidgetBuilder<'msg, #IFabVisualElement>, content: WidgetBuilder<'msg, #IFabBrush>) =
        this.AddWidget(VisualElement.BackgroundWidget.WithValue(content.Compile()))

    [<Extension>]
    static member inline clip<'msg, 'marker, 'contentMarker when 'marker :> IFabVisualElement and 'contentMarker :> IFabGeometry>
        (
            this: WidgetBuilder<'msg, 'marker>,
            content: WidgetBuilder<'msg, 'contentMarker>
        ) =
        this.AddWidget(VisualElement.Clip.WithValue(content.Compile()))

    [<Extension>]
    static member inline flowDirection(this: WidgetBuilder<'msg, #IFabVisualElement>, value: FlowDirection) =
        this.AddScalar(VisualElement.FlowDirection.WithValue(value))

    [<Extension>]
    static member inline size(this: WidgetBuilder<'msg, #IFabVisualElement>, ?width: float, ?height: float) =
        match width, height with
        | None, None -> this
        | Some w, None -> this.AddScalar(VisualElement.WidthRequest.WithValue(w))
        | None, Some h -> this.AddScalar(VisualElement.HeightRequest.WithValue(h))
        | Some w, Some h ->
            this
                .AddScalar(VisualElement.WidthRequest.WithValue(w))
                .AddScalar(VisualElement.HeightRequest.WithValue(h))

    [<Extension>]
    static member inline height(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.HeightRequest.WithValue(value))

    [<Extension>]
    static member inline width(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.WidthRequest.WithValue(value))

    [<Extension>]
    static member inline inputTransparent(this: WidgetBuilder<'msg, #IFabVisualElement>, value: bool) =
        this.AddScalar(VisualElement.InputTransparent.WithValue(value))

    [<Extension>]
    static member inline isEnabled(this: WidgetBuilder<'msg, #IFabVisualElement>, value: bool) =
        this.AddScalar(VisualElement.IsEnabled.WithValue(value))

    [<Extension>]
    static member inline isVisible(this: WidgetBuilder<'msg, #IFabVisualElement>, value: bool) =
        this.AddScalar(VisualElement.IsVisible.WithValue(value))

    [<Extension>]
    static member inline minimumHeight(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.MinimumHeightRequest.WithValue(value))

    [<Extension>]
    static member inline minimumWidth(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.MinimumWidthRequest.WithValue(value))

    [<Extension>]
    static member inline maximumHeight(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.MaximumHeightRequest.WithValue(value))

    [<Extension>]
    static member inline maximumWidth(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.MaximumWidthRequest.WithValue(value))

    [<Extension>]
    static member inline opacity(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.Opacity.WithValue(value))

    [<Extension>]
    static member inline visual(this: WidgetBuilder<'msg, #IFabVisualElement>, value: IVisual) =
        this.AddScalar(VisualElement.Visual.WithValue(value))

    [<Extension>]
    static member inline focus(this: WidgetBuilder<'msg, #IFabVisualElement>, value: bool, onFocusChanged: bool -> 'msg) =
        this.AddScalar(VisualElement.FocusWithEvent.WithValue(ValueEventData.create value (fun args -> onFocusChanged args |> box)))

    /// <summary>Animates an elements TranslationX and TranslationY properties from their current values to the new values. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="x">The x component of the final translation vector.</param>
    /// <param name="y">The y component of the final translation vector.</param>
    /// <param name="duration">The duration of the animation in milliseconds.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline translateTo(this: WidgetBuilder<'msg, #IFabVisualElement>, x: float, y: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.TranslateTo.WithValue(
                { X = x
                  Y = y
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

    /// <summary>Translates the X position of the element.</summary>
    /// <param name="x">The x component of the final translation vector.</param>
    [<Extension>]
    static member inline translationX(this: WidgetBuilder<'msg, #IFabVisualElement>, x: float) =
        this.AddScalar(VisualElement.TranslationX.WithValue(x))

    /// <summary>Translates the Y position of the element.</summary>
    /// <param name="y">The y component of the final translation vector.</param>
    [<Extension>]
    static member inline translationY(this: WidgetBuilder<'msg, #IFabVisualElement>, y: float) =
        this.AddScalar(VisualElement.TranslationY.WithValue(y))

    /// <summary>Create a Shadow widget, that enables a shadow to be added to any layout or view.</summary>
    [<Extension>]
    static member inline shadow<'msg, 'marker, 'contentMarker when 'marker :> IFabVisualElement and 'contentMarker :> IFabShadow>
        (
            this: WidgetBuilder<'msg, 'marker>,
            content: WidgetBuilder<'msg, 'contentMarker>
        ) =
        this.AddWidget(VisualElement.Shadow.WithValue(content.Compile()))

    /// <summary>Animates elements Scale property from their current values to the new values. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="scale">The value of the final scale vector.</param>
    /// <param name="duration">The time, in milliseconds, over which to animate the transition. The default is 250.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline scaleTo(this: WidgetBuilder<'msg, #IFabVisualElement>, scale: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.ScaleTo.WithValue(
                { Scale = scale
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

    /// <summary>ScaleX property from their current value to the new value. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="value">The value of the final scale vector.</param>
    [<Extension>]
    static member inline scaleX(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.ScaleX.WithValue(value))

    /// <summary>Animates elements ScaleX property from their current value to the new value. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="scale">The value of the final scale vector.</param>
    /// <param name="duration">The time, in milliseconds, over which to animate the transition. The default is 250.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline scaleXTo(this: WidgetBuilder<'msg, #IFabVisualElement>, scale: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.ScaleXTo.WithValue(
                { Scale = scale
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

    /// <summary>ScaleY property from their current value to the new value. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="value">The value of the final scale vector.</param>
    [<Extension>]
    static member inline scaleY(this: WidgetBuilder<'msg, #IFabVisualElement>, value: float) =
        this.AddScalar(VisualElement.ScaleY.WithValue(value))

    /// <summary>Sets the z-index of the element.</summary>
    /// <param name="value">The z-index of the element.</param>
    [<Extension>]
    static member inline zIndex(this: WidgetBuilder<'msg, #IFabVisualElement>, value: int) =
        this.AddScalar(VisualElement.ZIndex.WithValue(value))

    /// <summary>Animates elements ScaleY property from their current value to the new value. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="scale">The value of the final scale vector.</param>
    /// <param name="duration">The time, in milliseconds, over which to animate the transition. The default is 250.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline scaleYTo(this: WidgetBuilder<'msg, #IFabVisualElement>, scale: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.ScaleYTo.WithValue(
                { Scale = scale
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

    /// <summary>Animates elements Opacity property from their current values to the new values. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="opacity">The value of the final opacity value.</param>
    /// <param name="duration">The time, in milliseconds, over which to animate the transition. The default is 250.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline fadeTo(this: WidgetBuilder<'msg, #IFabVisualElement>, opacity: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.FadeTo.WithValue(
                { Opacity = opacity
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

    /// <summary>Animates an elements Rotation property from its current value to the new value. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="rotation">The value of the final rotation value.</param>
    /// <param name="duration">The time, in milliseconds, over which to animate the transition. The default is 250.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline rotateTo(this: WidgetBuilder<'msg, #IFabVisualElement>, rotation: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.RotateTo.WithValue(
                { Rotation = rotation
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

    /// <summary>Animates an elements RotationX property from its current value to the new value. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="rotation">The value of the final rotationX value.</param>
    /// <param name="duration">The time, in milliseconds, over which to animate the transition. The default is 250.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline rotateXTo(this: WidgetBuilder<'msg, #IFabVisualElement>, rotation: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.RotateXTo.WithValue(
                { Rotation = rotation
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

    /// <summary>Animates an elements RotationY property from its current value to the new value. This ensures that the input layout is in the same position as the visual layout.</summary>
    /// <param name="rotation">The value of the final rotationY value.</param>
    /// <param name="duration">The time, in milliseconds, over which to animate the transition. The default is 250.</param>
    /// <param name="easing">The easing of the animation.</param>
    [<Extension>]
    static member inline rotateYTo(this: WidgetBuilder<'msg, #IFabVisualElement>, rotation: float, duration: int, easing: Easing) =
        this.AddScalar(
            VisualElement.RotateYTo.WithValue(
                { Rotation = rotation
                  AnimationDuration = uint duration
                  Easing = easing }
            )
        )

[<Extension>]
type VisualElementExtraModifiers =
    [<Extension>]
    static member inline background(this: WidgetBuilder<'msg, #IFabVisualElement>, value: Color) = this.background(SolidColorBrush(value))

    [<Extension>]
    static member inline background(this: WidgetBuilder<'msg, #IFabVisualElement>, value: FabColor) = this.background(value.ToMauiColor())
