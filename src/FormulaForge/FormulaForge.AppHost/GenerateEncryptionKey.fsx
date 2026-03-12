open System
open System.Security.Cryptography

let key = 32 |> RandomNumberGenerator.GetBytes |> Convert.ToBase64String

printfn $"Key is: {key}"
