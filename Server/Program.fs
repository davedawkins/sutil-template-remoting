module Program

open SharedTypes

let getStudents() =
  async {
    return [
        { Name = "Mike";  Age = 23; }
        { Name = "John";  Age = 22; }
        { Name = "Diana"; Age = 22; }
    ]
  }

let findStudentByName name =
  async {
    let! students = getStudents()
    let student = List.tryFind (fun student -> student.Name = name) students
    return student
  }

let studentApi : IStudentApi = {
    studentByName = findStudentByName
    allStudents = getStudents
}

open Suave
open Fable.Remoting.Server
open Fable.Remoting.Suave

let webApp : WebPart =
    Remoting.createApi()
    |> Remoting.fromValue studentApi
    |> Remoting.buildWebPart

// start the web server
startWebServer defaultConfig webApp