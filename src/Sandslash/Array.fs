namespace Sandslash

open System.Linq

module Array =
  
  let inline checkNonNull argName arg =
      if isNull arg then nullArg argName

  let inline forall ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) =
    checkNonNull "array" array
    let len = array.Length
    let rec loop i =
        i >= len || (predicate array.[i] && loop (i + 1))
    loop 0

  let inline exists ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) =
    checkNonNull "array" array
    let mutable state = false
    let mutable i = 0

    while not state && i < array.Length do
        state <- predicate array.[i]
        i <- i + 1

    state

  let inline contains (value: ^T) (array: array<^T>) = array.Contains(value)
  let inline count ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = array.Count(predicate)
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
    for i in 0..src.Length-1 do
      acc.[i] <- (^U : (static member op_Implicit: ^T -> ^U) src.[i])
    acc
  let inline chunk (size: int) (array: array<^T>) = array.Chunk(size).ToArray()
  let inline reverse (array: array<^T>) = array.Reverse().ToArray()
  let inline zip (array2: array<^T2>) (array1: array<^T1>) = array1.Zip(array2).ToArray()
  let inline zip3 (array3: array<^T3>) (array2: array<^T2>) (array1: array<^T1>) = array1.Zip(array2, array3).ToArray()

  let inline asSpan (array: array<^T>) = System.MemoryExtensions.AsSpan(array)
