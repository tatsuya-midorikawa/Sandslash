namespace Sandslash.Test

open Sandslash
open System
open Xunit
open Xunit.Abstractions
open Microsoft.FSharp.NativeInterop

#nowarn "9"

type ``String Test`` (Console: ITestOutputHelper) =
  [<Fact>]
  member __.split() =
    let actual = String.split "aaa\nbbb\rccc ddd\r\neee"
    let expected = [| "aaa"; "bbb"; "ccc"; "ddd"; ""; "eee" |]
    expected
    |> Array.iter2 (fun actual expected ->
        Console.WriteLine($"actual: {actual}, expected: {expected}")
        Assert.Equal(expected, actual))
        actual


  [<Theory>]
  [<InlineData (0, 0)>]
  [<InlineData (0, 1)>]
  [<InlineData (1, 3)>]
  [<InlineData (10, 1)>]
  [<InlineData (-1, 2)>]
  member __.slice(starti, endi)=
    let src = "abcdef"
    let actual = src |> String.slice (starti, endi)
    let expected = src[starti..endi]
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Fact>]
  member __.sandbox()=
    let s = "abcdef"
    let cs = s.ToCharArray().AsSpan()
    let p = NativePtr.stackalloc<char> s.Length |> NativePtr.toVoidPtr
    let cs' = Span<char> (p, s.Length)
    s.CopyTo(cs')
    
    Console.WriteLine(s |> String.slice (0, 0))
    Assert.True(true)
