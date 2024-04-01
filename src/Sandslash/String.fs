namespace Sandslash

module String =
  let inline upcase (s: string) = s.ToUpperInvariant()
  let inline downcase (s: string) = s.ToLowerInvariant()

