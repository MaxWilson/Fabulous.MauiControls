namespace Fabulous.Maui

open System
open System.Runtime.CompilerServices
open Fabulous
open Microsoft.Maui.Controls
open Microsoft.Maui.Controls.PlatformConfiguration

type IFabDatePicker =
    inherit IFabView

module DatePicker =
    let WidgetKey = Widgets.register<DatePicker>()

    let CharacterSpacing =
        Attributes.defineBindableFloat DatePicker.CharacterSpacingProperty

    let FontAttributes =
        Attributes.defineBindableWithEquality<FontAttributes> DatePicker.FontAttributesProperty

    let FontFamily =
        Attributes.defineBindableWithEquality<string> DatePicker.FontFamilyProperty

    let FontSize = Attributes.defineBindableFloat DatePicker.FontSizeProperty

    let Format = Attributes.defineBindableWithEquality<string> DatePicker.FormatProperty

    let MaximumDate =
        Attributes.defineBindableWithEquality<DateTime> DatePicker.MaximumDateProperty

    let MinimumDate =
        Attributes.defineBindableWithEquality<DateTime> DatePicker.MinimumDateProperty

    let TextColor = Attributes.defineBindableAppThemeColor DatePicker.TextColorProperty

    let FontAutoScalingEnabled =
        Attributes.defineBindableBool DatePicker.FontAutoScalingEnabledProperty

    let DateWithEvent =
        Attributes.defineBindableWithEvent "DatePicker_DateSelected" DatePicker.DateProperty (fun target -> (target :?> DatePicker).DateSelected)

    let UpdateMode =
        Attributes.defineSimpleScalarWithEquality<iOSSpecific.UpdateMode> "DatePicker_UpdateMode" (fun _ newValueOpt node ->
            let datePicker = node.Target :?> DatePicker

            let value =
                match newValueOpt with
                | ValueNone -> iOSSpecific.UpdateMode.Immediately
                | ValueSome v -> v

            iOSSpecific.DatePicker.SetUpdateMode(datePicker, value))

[<AutoOpen>]
module DatePickerBuilders =
    type Fabulous.Maui.View with

        static member inline DatePicker<'msg>(date: DateTime, onDateSelected: DateTime -> 'msg) =
            WidgetBuilder<'msg, IFabDatePicker>(
                DatePicker.WidgetKey,
                DatePicker.DateWithEvent.WithValue(ValueEventData.create date (fun args -> onDateSelected args.NewDate |> box))
            )

[<Extension>]
type DatePickerModifiers =
    /// <summary>CharacterSpacing, of type float, is the spacing between characters of the DatePicker text.</summary>
    [<Extension>]
    static member inline characterSpacing(this: WidgetBuilder<'msg, #IFabDatePicker>, value: float) =
        this.AddScalar(DatePicker.CharacterSpacing.WithValue(value))

    [<Extension>]
    static member inline font
        (
            this: WidgetBuilder<'msg, #IFabDatePicker>,
            ?size: float,
            ?attributes: FontAttributes,
            ?fontFamily: string,
            ?fontAutoScalingEnabled: bool
        ) =

        let mutable res = this

        match size with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontSize.WithValue(v))

        match attributes with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontAttributes.WithValue(v))

        match fontFamily with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontFamily.WithValue(v))

        match fontAutoScalingEnabled with
        | None -> ()
        | Some v -> res <- res.AddScalar(DatePicker.FontAutoScalingEnabled.WithValue(v))

        res

    /// <summary>Format of type string, a standard or custom .NET formatting string, which defaults to "D", the long date pattern.</summary>
    [<Extension>]
    static member inline format(this: WidgetBuilder<'msg, #IFabDatePicker>, value: string) =
        this.AddScalar(DatePicker.Format.WithValue(value))

    /// <summary>Date of type DateTime, which defaults to the first day of the year 1900.</summary>
    [<Extension>]
    static member inline minimumDate(this: WidgetBuilder<'msg, #IFabDatePicker>, value: DateTime) =
        this.AddScalar(DatePicker.MinimumDate.WithValue(value))

    /// <summary>Date of type DateTime, which defaults to the last day of the year 2100.</summary>
    [<Extension>]
    static member inline maximumDate(this: WidgetBuilder<'msg, #IFabDatePicker>, value: DateTime) =
        this.AddScalar(DatePicker.MaximumDate.WithValue(value))

    /// <summary>TextColor of type FabColor, the color used to display the selected date.</summary>
    [<Extension>]
    static member inline textColor(this: WidgetBuilder<'msg, #IFabDatePicker>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(DatePicker.TextColor.WithValue(AppTheme.create light dark))

    /// <summary>Link a ViewRef to access the direct DatePicker control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabDatePicker>, value: ViewRef<DatePicker>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type DatePickerPlatformModifiers =
    /// <summary>iOS platform specific. Sets a value that controls whether elements in the date picker are continuously updated while scrolling or updated once after scrolling has completed.</summary>
    /// <param name="mode">The new property value to assign.</param>
    [<Extension>]
    static member inline updateMode(this: WidgetBuilder<'msg, #IFabDatePicker>, mode: iOSSpecific.UpdateMode) =
        this.AddScalar(DatePicker.UpdateMode.WithValue(mode))
