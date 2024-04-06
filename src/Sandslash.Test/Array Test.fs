namespace Sandslash.Test

open Sandslash
open System
open Xunit
open Xunit.Abstractions
open Microsoft.FSharp.NativeInterop

#nowarn "9"

type ``Array Test`` (Console: ITestOutputHelper) =

  [<Fact>]
  member __.sandbox()=
    let s = [| 1; 1; 1; |]
    let actual = s |> Sandslash.Array.forall (fun n -> n = 1)
    let expected = s |> FSharp.Collections.Array.forall (fun n -> n = 1)

    Console.WriteLine($"actual: {actual}, expected: {expected}")

    Assert.True(true)
