namespace Sandslash.Test

open Sandslash
open System
open Xunit
open Xunit.Abstractions
open Microsoft.FSharp.NativeInterop

#nowarn "9"

type ``Array Test`` (Console: ITestOutputHelper) =
  
  [<Fact>]
  member __.forall () =
    let empty = [||]
    let actual = empty |> Sandslash.Array.forall (fun n -> n > 0)
    let expected = empty |> FSharp.Collections.Array.forall (fun n -> n > 0)
    Console.WriteLine($"0) actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let data = [| 1..10|]
    let actual = data |> Sandslash.Array.forall (fun n -> n > 0)
    let expected = data |> FSharp.Collections.Array.forall (fun n -> n > 0)
    Console.WriteLine($"1) actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.forall (fun n -> n > 1)
    let expected = data |> FSharp.Collections.Array.forall (fun n -> n > 1)
    Console.WriteLine($"2) actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.forall (fun n -> n > 10)
    let expected = data |> FSharp.Collections.Array.forall (fun n -> n > 10)
    Console.WriteLine($"3) actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let data = [| -10..2..10|]
    let actual = data |> Sandslash.Array.forall (fun n -> n < 0)
    let expected = data |> FSharp.Collections.Array.forall (fun n -> n < 0)
    Console.WriteLine($"4) actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.forall (fun n -> n < -11)
    let expected = data |> FSharp.Collections.Array.forall (fun n -> n < -11)
    Console.WriteLine($"5) actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.forall (fun n -> n >= -10)
    let expected = data |> FSharp.Collections.Array.forall (fun n -> n >= -10)
    Console.WriteLine($"6) actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

  [<Fact>]
  member __.exists()=
    let empty = [||]
    let actual = empty |> Sandslash.Array.exists (fun n -> n = 1)
    let expected = empty |> FSharp.Collections.Array.exists (fun n -> n = 1)
    Console.WriteLine($"empty -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)
    
    let data = [| 1..10|]
    let actual = data |> Sandslash.Array.exists (fun n -> n = 0)
    let expected = data |> FSharp.Collections.Array.exists (fun n -> n = 0)
    Console.WriteLine($"data(0) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)
   
    let actual = data |> Sandslash.Array.exists (fun n -> n = 1)
    let expected = data |> FSharp.Collections.Array.exists (fun n -> n = 1)
    Console.WriteLine($"data(1) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.exists (fun n -> n = 10)
    let expected = data |> FSharp.Collections.Array.exists (fun n -> n = 10)
    Console.WriteLine($"data(10) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = data |> Sandslash.Array.exists (fun n -> n = 11)
    let expected = data |> FSharp.Collections.Array.exists (fun n -> n = 11)
    Console.WriteLine($"data(11) -> actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

  [<Fact>]
  member __.contains()=
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
  member __.countBy()=
    let empty = [||]
    let actual = empty |> Sandslash.Array.countBy (fun n -> n = 1)
    let expected = empty |> FSharp.Collections.Array.countBy (fun n -> n = 1)
    Console.WriteLine($"empty -> actual: {actual.Length}, expected: {expected.Length}")
    Assert.Equal(expected.Length, actual.Length)
    
    let data = [| 1..10|]
    let actual = data |> Sandslash.Array.countBy (fun n -> n = 0)
    let expected = data |> FSharp.Collections.Array.countBy (fun n -> n = 0)
    Console.WriteLine($"data(0) -> actual: {actual.Length}, expected: {expected.Length}")
    if actual.Length = expected.Length
      then
        for i = 0 to (actual.Length - 1) do
          Assert.Equal(expected[i], actual[i])
      else
        Assert.Fail("actual.Length is not equal to expected.Length")

    let actual = data |> Sandslash.Array.countBy (fun n -> n = 1)
    let expected = data |> FSharp.Collections.Array.countBy (fun n -> n = 1)
    Console.WriteLine($"data(1) -> actual: {actual.Length}, expected: {expected.Length}")
    if actual.Length = expected.Length
      then
        for i = 0 to (actual.Length - 1) do
          Assert.Equal(expected[i], actual[i])
      else
        Assert.Fail("actual.Length is not equal to expected.Length")

    // let actual = data |> Sandslash.Array.countBy (fun n -> n = 10)
    // let expected = data |> FSharp.Collections.Array.countBy (fun n -> n = 10)
    // Console.WriteLine($"data(10) -> actual: {actual}, expected: {expected}")
    // Assert.Equal(expected, actual)

    // let actual = data |> Sandslash.Array.countBy (fun n -> n = 11)
    // let expected = data |> FSharp.Collections.Array.countBy (fun n -> n = 11)
    // Console.WriteLine($"data(11) -> actual: {actual}, expected: {expected}")
    // Assert.Equal(expected, actual)

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
