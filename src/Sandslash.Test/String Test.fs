namespace Sandslash.Test

open Sandslash
open Xunit
open Xunit.Abstractions

type ``String Test`` (Console: ITestOutputHelper) =
  [<Fact>]
  member __.split() =
    let actual = String.split "aaa\nbbb\rccc ddd\r\neee"
    let expected = [| "aaa"; "bbb"; "ccc"; "ddd"; "eee" |]
    expected
    |> Array.iter2 (fun actual expected ->
        Console.WriteLine($"actual: {actual}, expected: {expected}")
        Assert.Equal(expected, actual))
        actual
        

  [<Fact>]
  member __.slice()=
    let src = "abcdef"
    let actual = src |> String.slice (0, 0)
    let expected = src[0..0]
    Console.WriteLine($"actual: {actual}, expected: {expected}")

    let actual = src |> String.slice (0, 1)
    let expected = src[0..1]
    Console.WriteLine($"actual: {actual}, expected: {expected}")

    let actual = src |> String.slice (1, 3)
    let expected = src[1..3]
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

    let actual = src |> String.slice (8, 3)
    let expected = src[8..3]
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Fact>]
  member __.sandbox()=
    let s = "abcdef"
    Console.WriteLine(s |> String.slice (0, 0))
    Assert.True(true)
