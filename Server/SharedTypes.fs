module SharedTypes

type Student = {
    Name : string
    Age : int
}

// Shared specs between Server and Client
type IStudentApi = {
    studentByName : string -> Async<Student option>
    allStudents : unit -> Async<list<Student>>
}
