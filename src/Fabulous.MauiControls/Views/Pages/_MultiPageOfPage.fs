namespace Fabulous.Maui

open Fabulous
open Microsoft.Maui.Controls
open System.Runtime.CompilerServices

type IFabMultiPageOfPage =
    inherit IFabPage

module MultiPageOfPage =
    let Children =
        Attributes.defineListWidgetCollection "MultiPageOfPage" (fun target -> (target :?> MultiPage<Page>).Children)

    let CurrentPageChanged =
        Attributes.defineEventNoArg "MultiPageOfPage_CurrentPageChanged" (fun target -> (target :?> MultiPage<Page>).CurrentPageChanged)

[<Extension>]
type MultiPageOfPageModifiers =
    /// <summary>Raised when the CurrentPage property changes.</summary>
    /// <param name="onCurrentPageChanged">The msg to invoke when the CurrentPage property changes.</param>
    [<Extension>]
    static member inline onCurrentPageChanged(this: WidgetBuilder<'msg, #IFabMultiPageOfPage>, onCurrentPageChanged: 'msg) =
        this.AddScalar(MultiPageOfPage.CurrentPageChanged.WithValue(onCurrentPageChanged))
