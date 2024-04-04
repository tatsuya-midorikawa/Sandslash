namespace Sandslash

open System.Linq

module Array =
  let inline all ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = array.All(predicate)
  let inline any ([<InlineIfLambda>]predicate: ^T -> bool) (array: array<^T>) = array.Any(predicate)
  let inline cast (src: array<^T>) = 
    let acc = Array.zeroCreate<^U>(src.Length)
    for i in 0..src.Length-1 do
      acc.[i] <- (^U : (static member op_Implicit: ^T -> ^U) src.[i])
    acc
  let inline contains (value: ^T) (array: array<^T>) = array.Contains(value)
  let inline chunk (size: int) (array: array<^T>) = array.Chunk(size).ToArray()
  let inline asSpan (array: array<^T>) = System.MemoryExtensions.AsSpan(array)
