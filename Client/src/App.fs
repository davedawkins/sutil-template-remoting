module App

open Sutil
open Sutil.Styling
open type Feliz.length

open Fable.Remoting.Client
open SharedTypes

type AppContext = {
    Api : IStudentApi
}

type Model = {
    Students : Student list
}

[<AutoOpen>]
module ModelHelpers =
    let students m = m.Students

type Message =
    | FetchStudents
    | SetStudents of Student list

let init() = { Students = [] }, Cmd.none

let update (appctx: AppContext) msg model =
    match msg with

    | FetchStudents ->
        model,
        Cmd.OfAsync.perform (appctx.Api.allStudents) () SetStudents

    | SetStudents students ->
        { model with Students = students },
        Cmd.none

let viewStudent student =
    Html.divc "student-row" [
        Html.span student.Name
        Html.span student.Age
    ]

let style = [
    rule ".container" [
        Css.padding (rem 2)
    ]
    rule ".student-grid" [
        Css.width (px 400)
        Css.displayGrid
        Css.custom( "grid-template-columns", "subgrid" )
        Css.marginTop (rem 1)
    ]
    rule ".student-row" [
        Css.displayGrid
        Css.custom( "grid-template-columns", "9fr 3fr" )
    ]
    rule ".student-row>span" [
        Css.padding (px 4)
    ]
    rule ".heading" [
        Css.fontWeightBold
    ]
]

let view model dispatch =

    Html.divc "container" [
        Html.button [
            Attr.className "button"
            Ev.onClick (fun _ -> dispatch FetchStudents)
            text "Fetch Students"
        ]

        Html.divc "student-grid" [
            Html.divc "student-row heading" [
                Html.span "Name"
                Html.span "Age"
            ]
            Bind.each( model .> students, viewStudent )
        ]
    ] |> withStyle style


let main() =

    let makeStudentApi() =
        Remoting.createApi()
        |> Remoting.buildProxy<IStudentApi>

    let app = {
        Api = makeStudentApi()
    }

    let model, dispatch = () |> Store.makeElmish init (update app) ignore

    view  model dispatch |> Program.mountElement "sutil-app"

main()