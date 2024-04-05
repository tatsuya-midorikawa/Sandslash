namespace Sandslash

open System

module String =
  let inline isEmpty (s: string) = System.String.IsNullOrEmpty(s)
  let inline isEmptyOrWhiteSpace (s: string) = System.String.IsNullOrWhiteSpace(s)
  let inline upcase (s: string) = if s = null then null else s.ToUpperInvariant()
  let inline downcase (s: string) = if s = null then null else s.ToLowerInvariant()
  let inline capitalize (s: string) = if s = null then null else $"{s.Substring(0, 1).ToUpperInvariant()}{s.Substring(1).ToLowerInvariant()}"
  let inline split (s: string) = if s = null then null else s.Split([|' '; '\r'; '\n'|])
  let inline splitAnd (trim: bool) (s: string) = if s = null then null else s.Split([|' '; '\r'; '\n'|], if trim then System.StringSplitOptions.None else System.StringSplitOptions.RemoveEmptyEntries)
  let inline splitBy (cs: char array, trim: bool) (s: string) = if s = null then null else s.Split(cs, System.StringSplitOptions.TrimEntries)
  let inline slice (start: int, end': int) (s: string) = if isEmpty s then null else s[start..end']
  let inline replace (oldValue: string, newValue: string) (s: string) = if s = null then null else s.Replace(oldValue, newValue)
  let inline padLeft (totalWidth: int, paddingChar: char) (s: string) = if s = null then null else s.PadLeft(totalWidth, paddingChar)
  let inline padRight (totalWidth: int, paddingChar: char) (s: string) = if s = null then null else s.PadRight(totalWidth, paddingChar)
  let inline remove (startIndex: int, count: int) (s: string) = if s = null then null else s.Remove(startIndex, count)
  let inline removeAfter (startIndex: int) (s: string) = if s = null then null else s.Remove(startIndex)
  let inline indexOf (value: string) (s: string) = if s = null then -1 else s.IndexOf(value)
  let inline substring (startIndex: int, length: int) (s: string) = if s = null then null else s.Substring(startIndex, length)
  let inline substringFrom (startIndex: int) (s: string) = if s = null then null else s.Substring(startIndex)
  let inline asSpan (s: string) = s.AsSpan()
