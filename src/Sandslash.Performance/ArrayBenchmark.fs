namespace Sandslash.Performance

open BenchmarkDotNet.Attributes
open Bogus

type ArrayBenchmark() =
  let fake = Faker()
  let array = [| for _ in 1..10_000 do fake.Random.Int() |]

  [<Benchmark>]
  member __.Sandslash_Array_forall () =
    array |> Sandslash.Array.forall (fun x -> x % 2 = 0)

  [<Benchmark>]
  member __.FSharp_Collections_Array_forall ()=
    array |> FSharp.Collections.Array.forall (fun x -> x % 2 = 0)

  [<Benchmark>]
  member __.Sandslash_Array_exists ()=
    array |> Sandslash.Array.exists (fun x -> x % 2 = 0)    

  [<Benchmark>]
  member __.FSharp_Collections_Array_exists ()=
    array |> FSharp.Collections.Array.exists (fun x -> x % 2 = 0)

