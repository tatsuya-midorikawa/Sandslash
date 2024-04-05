namespace Sandslash.Test

open Sandslash
open System
open Xunit
open Xunit.Abstractions
open Microsoft.FSharp.NativeInterop

#nowarn "9"

type ``String Test`` (Console: ITestOutputHelper) =
  [<Theory>]
  [<InlineData ("", true)>]
  [<InlineData (" ", false)>]
  [<InlineData (null, true)>]
  [<InlineData ("a", false)>]
  member __.isEmpty(src, expected)=
    let actual = String.isEmpty src
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData ("", true)>]
  [<InlineData (" ", true)>]
  [<InlineData (null, true)>]
  [<InlineData ("a", false)>]
  member __.isEmptyOrWhiteSpace(src, expected)=
    let actual = String.isEmptyOrWhiteSpace src
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)
    

  [<Theory>]
  [<InlineData ("aAa")>]
  [<InlineData ("Aあa")>]
  member __.upcase(src: string)=
    let actual = String.upcase src
    let expected = src.ToUpperInvariant()
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)

  
  [<Theory>]
  [<InlineData ("AaA")>]
  [<InlineData ("aあA")>]
  member __.downcase(src: string)=
    let actual = String.downcase src
    let expected = src.ToLowerInvariant()
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData ("aAa", "Aaa")>]
  [<InlineData ("aあa", "Aあa")>]
  [<InlineData ("あaa", "あaa")>]
  member __.capitalize(src, expected)=
    let actual = String.capitalize src
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


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


  [<Theory>]
  [<InlineData ("a", "b", "abc")>]
  [<InlineData ("a", "b", "")>]
  member __.replace(oldValue, newValue, src)=
    let actual = src |> String.replace (oldValue, newValue)
    let expected = src.Replace(oldValue, newValue)
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData (5, 'a', "abc")>]
  [<InlineData (2, 'a', "abc")>]
  [<InlineData (1, 'a', "abc")>]
  [<InlineData (0, 'a', "abc")>]
  member __.padl(totalWidth, paddingChar, src)=
    let actual = src |> String.padl (totalWidth, paddingChar)
    let expected = src.PadLeft(totalWidth, paddingChar)
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData (5, 'a', "abc")>]
  [<InlineData (2, 'a', "abc")>]
  [<InlineData (1, 'a', "abc")>]
  [<InlineData (0, 'a', "abc")>]
  member __.padr(totalWidth, paddingChar, src)=
    let actual = src |> String.padr (totalWidth, paddingChar)
    let expected = src.PadRight(totalWidth, paddingChar)
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData (1, 1, "abc")>]
  [<InlineData (1, 2, "abc")>]
  [<InlineData (0, 1, "abc")>]
  [<InlineData (0, 0, "abc")>]
  member __.remove(startIndex, count, src)=
    let actual = src |> String.remove (startIndex, count)
    let expected = src.Remove(startIndex, count)
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData (1, "abc")>]
  [<InlineData (0, "abc")>]
  member __.removeAfter(startIndex, src)=
    let actual = src |> String.removeAfter startIndex
    let expected = src.Remove(startIndex)
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData ("bc", "abc")>]
  [<InlineData ("bc", "abcabc")>]
  [<InlineData ("bc", "abcabcabc")>]
  [<InlineData ("bc", "abcabcabcabc")>]
  member __.indexOf(value, src)=
    let actual = src |> String.indexOf value
    let expected = src.IndexOf(value)
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData (1, 1, "abc")>]
  [<InlineData (1, 2, "abc")>]
  [<InlineData (0, 1, "abc")>]
  [<InlineData (0, 0, "abc")>]
  member __.substring(startIndex, length, src)=
    let actual = src |> String.substring (startIndex, length)
    let expected = src.Substring(startIndex, length)
    Console.WriteLine($"actual: {actual}, expected: {expected}")
    Assert.Equal(expected, actual)


  [<Theory>]
  [<InlineData (1, "abc")>]
  [<InlineData (0, "abc")>]
  member __.substringFrom(startIndex, src)=
    let actual = src |> String.substringFrom startIndex
    let expected = src.Substring(startIndex)
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
