namespace Sandslash

module String =
  let inline isEmpty (s: string) = System.String.IsNullOrEmpty(s)
  let inline upcase (s: string) = if isEmpty s then null else s.ToUpperInvariant()
  let inline downcase (s: string) = if isEmpty s then null else s.ToLowerInvariant()
  let inline capitalize (s: string) = if isEmpty s then null else $"{s.Substring(0, 1).ToUpperInvariant()}{s.Substring(1).ToLowerInvariant()}"
  let inline split (s: string) = if isEmpty s then null else s.Split([|' '; '\r'; '\n'|], System.StringSplitOptions.RemoveEmptyEntries)
  let inline splitBy (s: string) (cs: char array, trim: bool) = if isEmpty s then null else s.Split(cs, System.StringSplitOptions.TrimEntries)
