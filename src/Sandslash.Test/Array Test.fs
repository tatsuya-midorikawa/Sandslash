namespace Sandslash.Test

open Sandslash
open System
open Xunit
open Xunit.Abstractions
open Microsoft.FSharp.NativeInterop

#nowarn "9"

type ``Array Test`` (Console: ITestOutputHelper) =
  
  [<Fact>]
  member __.contains2()=
    let empty = [||]
    let actual = empty |> Sandslash.Array.contains 1
    let expected = empty |> FSharp.Collections.Array.contains 1
    Console.WriteLine($"empty -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)
    
    let data = [| 1..10|]
    let actual = data |> Sandslash.Array.contains 0
    let expected = data |> FSharp.Collections.Array.contains 0
    Console.WriteLine($"data(0) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)
   
    let actual = data |> Sandslash.Array.contains 1
    let expected = data |> FSharp.Collections.Array.contains 1
    Console.WriteLine($"data(1) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.contains 10
    let expected = data |> FSharp.Collections.Array.contains 10
    Console.WriteLine($"data(10) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.contains 11
    let expected = data |> FSharp.Collections.Array.contains 11
    Console.WriteLine($"data(11) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Fact>]
  member __.sandbox()=
    let s = [| 3; 2; 1 |]
    s
    |> Sandslash.Array.contains 1
    |> fun s -> Console.WriteLine $"{s}"

    let actual = s |> Sandslash.Array.exists (fun n -> n = 1)
    let expected = s |> FSharp.Collections.Array.exists (fun n -> n = 1)

    Console.WriteLine($"actual: {actual}, expected: {expected}")

    Assert.True(true)
