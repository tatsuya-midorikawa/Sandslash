namespace Sandslash

open System.Linq

module Array =
  let inline all ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = array.All(predicate)
  let inline any ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = array.Any(predicate)
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

  let inline asSpan (array: array<^T>) = System.MemoryExtensions.AsSpan(array)
