namespace Sandslash

#nowarn "9"
open System
open System.Linq
open System.Runtime.Intrinsics

module vArray =
  open System.Runtime.Intrinsics.X86
  open Microsoft.FSharp.NativeInterop
  
  let inline throw_empty() = raise (System.InvalidOperationException "The source is empty.")
  let inline debug_writel(str) = System.Diagnostics.Debug.WriteLine(str)

  type vec128 = System.Runtime.Intrinsics.Vector128
  type vec128<'T when 'T: unmanaged and 'T: struct and 'T: comparison and 'T: (new: unit -> 'T) and 'T:> System.ValueType> = System.Runtime.Intrinsics.Vector128<'T>
  type vec256 = System.Runtime.Intrinsics.Vector256
  type vec256<'T when 'T: unmanaged and 'T: struct and 'T: comparison and 'T: (new: unit -> 'T) and 'T:> System.ValueType> = System.Runtime.Intrinsics.Vector256<'T>
  
  let inline defaultof<'T> = Unchecked.defaultof<'T>
  
  let inline checkNonNull argName arg =
    if isNull arg then nullArg argName
    
  let inline public contains<^T when ^T: unmanaged and ^T: struct and ^T: comparison and ^T: (new: unit -> ^T) and ^T:> System.ValueType>
    (value: ^T) (src: array<^T>) =
      use p = fixed &src[0]
      
      let mutable current = NativePtr.toNativeInt p
      let lastp = current + nativeint ((src.Length - Vector512<^T>.Count) * sizeof<^T>) 
      let v = Vector512.Create value
      
      let rec loop () =
        if current < lastp
          then 
            if Vector512.EqualsAny(Vector512.Load (NativePtr.ofNativeInt<^T> current), v)
              then true
              else current <- current + 64n; loop ()
          else 
            Vector512.EqualsAny(Vector512.Load (NativePtr.ofNativeInt<^T> lastp), v)
      loop ()
      // if not vec128.IsHardwareAccelerated || src.Length < vec128<^T>.Count
      //   // Not SIMD
      //   then
      //     let rec search i =
      //       if i < src.Length
      //         then if src[i] = value then true else search (i + 1)
      //         else false
      //     search 0
      //   elif not vec256.IsHardwareAccelerated || src.Length < vec256<^T>.Count
      //     // SIMD : 128bit
      //     then
      //       use p = fixed &src[0]
      //
      //       let mutable current = NativePtr.toNativeInt p
      //       let lastp = current + nativeint ((src.Length - Vector128<^T>.Count) * sizeof<^T>) 
      //       let v = Vector128.Create value
      //
      //       let rec loop () =
      //         if current < lastp
      //           then 
      //             if Vector128.EqualsAny(Vector128.Load (NativePtr.ofNativeInt<^T> current), v)
      //               then true
      //               else current <- current + 16n; loop ()
      //           else 
      //             Vector128.EqualsAny(Vector128.Load (NativePtr.ofNativeInt<^T> lastp), v)
      //       loop ()
      //     // SIMD : 256bit
      //     else
      //       use p = fixed &src[0]
      //
      //       let mutable current = NativePtr.toNativeInt p
      //       let lastp = current + nativeint ((src.Length - Vector256<^T>.Count) * sizeof<^T>) 
      //       let v = Vector256.Create value
      //
      //       let rec loop () =
      //         if current < lastp
      //           then 
      //             if Vector256.EqualsAny(Vector256.Load (NativePtr.ofNativeInt<^T> current), v)
      //               then true
      //               else current <- current + 32n; loop ()
      //           else 
      //             Vector256.EqualsAny(Vector256.Load (NativePtr.ofNativeInt<^T> lastp), v)
      //       loop ()
          
module Array =

  let inline checkNonNull argName arg =
    if isNull arg then nullArg argName

  let inline forall ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) =
    checkNonNull "array" array
    let len = array.Length
    let rec loop i =
        i >= len || (predicate array[i] && loop (i + 1))
    loop 0

  let inline exists ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) =
    checkNonNull "array" array
    let rec loop i =
      (i < array.Length) && (predicate array[i] || loop (i + 1))
    loop 0

  let inline contains (value: ^T) (array: array<^T>) =    
    checkNonNull "array" array
    let rec loop i =
      (i < array.Length) && (array[i] = value || loop (i + 1))
    loop 0

  // Temporarily commented out because I misunderstood the behavior of Array.countBy
  // see: https://fsharp.github.io/fsharp-core-docs/reference/fsharp-collections-arraymodule.html#countBy
  //
  // let inline countBy ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>): ^U =
  //   if array.Length = 0
  //     then [||]
  //     else
  //       let mutable count = 0
  //       for x in array do
  //         if predicate x then count <- count + 1
  //       [| (true, count); (false, array.Length - count) |]

  let inline countBy ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) =
    Array.countBy predicate array
  
  let inline distinct (array: array<^T>) = array.Distinct().ToArray()
  let inline distinctBy ([<InlineIfLambda>]selector: ^T -> ^U) (array: array<^T>) = array.DistinctBy(selector).ToArray()
  let inline elementAt (index: int) (array: array<^T>) = array.ElementAt(index)
  let inline elementAtOrDefault (index: int) (array: array<^T>) = 
    let elem = array.ElementAtOrDefault(index)
    if elem = null then ValueNone else ValueSome elem
  let inline first ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = array.First(predicate)
  let inline firstOrDefault ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = 
    let elem = array.FirstOrDefault(predicate)
    if elem = null then ValueNone else ValueSome elem
  let inline last ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = array.Last(predicate)
  let inline lastOrDefault ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = 
    let elem = array.LastOrDefault(predicate)
    if elem = null then ValueNone else ValueSome elem

  let inline cast (src: array<^T>) = 
    let acc = Array.zeroCreate<^U>(src.Length)
    for i in 0..src.Length - 1 do
      acc[i] <- (^U : (static member op_Implicit: ^T -> ^U) src[i])
    acc
  let inline chunk (size: int) (array: array<^T>) = array.Chunk(size).ToArray()
  let inline reverse (array: array<^T>) = array.Reverse().ToArray()
  let inline zip (array2: array<^T2>) (array1: array<^T1>) = array1.Zip(array2).ToArray()
  let inline zip3 (array3: array<^T3>) (array2: array<^T2>) (array1: array<^T1>) = array1.Zip(array2, array3).ToArray()

  let inline asSpan (array: array<^T>) = System.MemoryExtensions.AsSpan(array)
