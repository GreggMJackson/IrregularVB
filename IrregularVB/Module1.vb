Imports System
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Imports System.IO
Imports System.Reflection

Module Module1
    Public Sub Main1()
        Dim pattern As String = "(Mr\.? |Mrs\.? |Miss |Ms\.? )"
        Dim names() As String = {"Mr. Henry Hunt", "Ms. Sara Samuels",
                         "Abraham Adams", "Ms. Nicole Norris"}
        For Each name As String In names
            Console.WriteLine(Regex.Replace(name, pattern, String.Empty))
        Next
    End Sub
    Public Sub Main2()
        Dim pattern As String = "\b(\w+?)\s\1\b"
        Dim input As String = "This this is a nice day. What about this? This tastes good. I saw a a dog."
        For Each match As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
            Console.WriteLine("{0} (duplicates '{1}') at position {2}", match.Value, match.Groups(1).Value, match.Index)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main3()
        ' Define text to be parsed.
        Dim Input As String = "Office expenses on 2/13/2008:\n" +
                               "Paper (500 sheets)                 £3.95\n" +
                               "Pencils (box of 10)                £1.00\n" +
                               "Pen (box of 10)                    £4.49\n" +
                               "Erasers                            £2.19\n" +
                               "Ink jet printer                   £69.95\n" +
                               "Total expenses                   £ 81.58\n"
        ' Get current culture's NumberFormatInfo object
        Dim nfi As NumberFormatInfo = CultureInfo.CurrentCulture.NumberFormat
        ' Assign neede poperty values to variables.
        Dim currencySymbol As String = nfi.CurrencySymbol
        Dim symbolPrecedesIfPositive As Boolean = CBool(nfi.CurrencyPositivePattern Mod 2 = 0)
        Dim groupSeparator As String = nfi.CurrencyGroupSeparator
        Dim decimalSeparator As String = nfi.CurrencyDecimalSeparator

        ' Form regular expression.
        Dim pattern As String = Regex.Escape(CStr(IIf(symbolPrecedesIfPositive, currencySymbol, ""))) +
                                "\s*[-+]?" + "([0-9]{0,3}(" + groupSeparator + "[0-9]{3})*(" +
                                Regex.Escape(decimalSeparator) + "[0-9]+)?)" +
                                CStr(IIf(Not symbolPrecedesIfPositive, currencySymbol, ""))
        Console.WriteLine("The regular expression pattern is: ")
        Console.WriteLine("    " + pattern)

        ' Get text that matches regular expression pattern.
        Dim matches As MatchCollection = Regex.Matches(Input, pattern, RegexOptions.IgnorePatternWhitespace)
        Console.WriteLine("Found {0} matches.", matches.Count)

        ' Get numeric string, convert it to a value, and add it to List object.
        Dim expenses As New List(Of Decimal)

        For Each match As Match In matches
            expenses.Add(Decimal.Parse(match.Groups.Item(1).Value))
        Next

        ' Determine whether total is present and if present, whether it is correct.
        Dim total As Decimal
        For Each value As Decimal In expenses
            total += value
        Next

        If total / 2 = expenses(expenses.Count - 1) Then
            Console.WriteLine("The expenses total {0:C2}.", expenses(expenses.Count - 1))
        Else
            Console.WriteLine("The expenses total {0:C2}.", total)
        End If
        Console.ReadLine()
    End Sub
    Public Sub Main4()
        Dim delimited As String = "\G(.+)[\t\u007c](.+)\r?\n"
        Dim input As String = "Mumbair, India|13,922,125" + vbCrLf +
            "Shanghai, China" + vbTab + "13,831,900" + vbCrLf +
            "Karachi, Pakistan|12,991,000" + vbCrLf +
            "Delhi, India" + vbTab + "12,259,230" + vbCrLf +
            "Istanbul, Turkey|11,372,613" + vbCrLf
        Console.WriteLine("Population of the world's largest cities, 2009")
        Console.WriteLine()
        Console.WriteLine("{0,-20} {1,10}", "City", "Population")
        Console.WriteLine()
        For Each match As Match In Regex.Matches(input, delimited)
            Console.WriteLine("{0,-20} {1,10}", match.Groups(1).Value, match.Groups(2).Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main5()
        Dim pattern As String = "gr[ae]y\s\S+?[\s\p{P}]"
        Dim input As String = "The gray wolf jumped over the grey wall."
        Dim matches As MatchCollection = Regex.Matches(input, pattern)
        For Each match As Match In matches
            Console.WriteLine($"'{match.Value}'")
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main6()
        Dim pattern As String = "\b[A-Z]\w*\b"
        Dim input As String = "A city Albany Zulu maritime Marseilles"
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine(match.Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main7()
        Dim pattern As String = "\bth[^o]\w+\b"
        Dim input As String = "thought thing though them through thus thorough this"
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine(match.Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main8()
        Dim pattern As String = "^.+"
        Dim input As String = "This is one line and" + vbCrLf + "this is the second"
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine(Regex.Escape(match.Value))
        Next

        Console.WriteLine()
        For Each match As Match In Regex.Matches(input, pattern, RegexOptions.Singleline)
            Console.WriteLine(Regex.Escape(match.Value))
        Next
        Console.ReadLine()

    End Sub
    Public Sub Main9()
        Dim pattern As String = "\b.*[.?!;:](\s|\z)"
        Dim input As String = "this. what: is? go, thing."
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine(match.Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main10()
        Dim pattern As String = "\b(\p{IsGreek}+(\s)?)+\p{Pd}\s(\p{IsBasicLatin}+(\s)?)+"
        Dim input As String = "Κατα Μαθθαίον - The Gospel of Matthew"
        Console.WriteLine(Regex.IsMatch(input, pattern))
        Console.ReadLine()
    End Sub
    Public Sub Main11()

        Dim pattern As String = "(\P{Sc})+"
        Dim values() As String = {"$164,091.78", "£1,073,142.68", "73¢", "€120"}
        For Each value As String In values
            Console.WriteLine(Regex.Match(value, pattern).Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main12()
        Dim pattern As String = "(\w)\1"
        Dim words() As String = {"trellis", "seer", "latter", "summer", "hoarse", "lesser", "aardvark", "stunned"}
        For Each word As String In words
            Dim match As Match = Regex.Match(word, pattern)
            If (match.Success) Then
                Console.WriteLine("'{0}' found in '{1}' at position {2}", match.Value, word, match.Index)
            Else
                Console.WriteLine("No double characters found in '{0}'", word)
            End If
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main13()
        Dim pattern As String = "\b(\w+)(\W){1,2}"
        Dim input As String = "The old, grey mare slowly walked across the narrow, green pasture."
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine(match.Value)
            Console.Write("    Non-word character(s):")
            Dim captures As CaptureCollection = match.Groups(2).Captures
            For ctr As Integer = 0 To captures.Count - 1
                Console.Write("'{0}' (\u{1}){2}",
                              captures(ctr).Value,
                              Convert.ToUInt16(captures(ctr).Value.Chars(0)).ToString("X4"),
                              If(ctr < captures.Count - 1, ", ", ""))
            Next
            Console.WriteLine()
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main14()
        Dim pattern As String = "\b\w+(e)?s(\s|$)"
        Dim input As String = "matches stores stops leave leaves"
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine(match.Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main15()
        Dim pattern As String = "\b(\S+)\s?"
        Dim input As String = "This is the first sentence of the first paragraph. " +
                "This is the second sentence." + vbCrLf +
                "This is the only sentence of the second paragraph."
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine(match.Groups(1))
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main16()
        Dim pattern As String = "^(\(?\d{3}\)?[\s-])?\d{3}-\d{4}$"
        Dim inputs() As String = {"111 111-1111", "222-2222", "222 333-444", "(212) 111-1111", "111-AB1-1111", "212-111-1111", "01 999-9999"}
        For Each input As String In inputs
            If (Regex.IsMatch(input, pattern)) Then
                Console.WriteLine(String.Format("{0,-15}", input) + ": matched")
            Else
                Console.WriteLine(String.Format("{0,-15}", input) + ": match failed")
            End If
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main17()
        Dim pattern As String = "^\D\d{1,5}\D*$"
        Dim inputs() As String = {"A1039C", "AA0001", "C18A", "Y938518"}
        For Each input As String In inputs
            If Regex.IsMatch(input, pattern) Then
                Console.WriteLine(String.Format("{0,-15} : matched", input))
            Else
                Console.WriteLine(String.Format("{0,-15} : match failed", input))
            End If
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main18()
        Dim chars() As Char = {"a"c, "X"c, "8"c, ","c, " "c, ChrW(9), "!"c}
        For Each ch As Char In chars
            Console.WriteLine("'{0}' {1}", Regex.Escape(ch.ToString()), Char.GetUnicodeCategory(ch))
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main19()
        Dim pattern As String = "^[0-9-[2468]]+$"
        Dim inputs() As String = {"123", "1357953", "3557798", "335599901"}
        For Each input As String In inputs
            Dim match As Match = Regex.Match(input, pattern)
            If match.Success Then
                Console.WriteLine(match.Value)
            End If
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main20()
        Dim startPos As Integer = 0
        Dim endPos As Integer = 70
        Dim input As String = "Brooklyn Dodgers, National League, 1911, 1912, 1932-1957" + vbCrLf +
                              "Chicago Cubs, National League, 1903-present" + vbCrLf +
                              "Detroit Tigers, American League, 1901-present" + vbCrLf +
                              "New York Giants, National League, 1885-1957" + vbCrLf +
                              "Washington Senators, American League, 1901-1960" + vbCrLf
        Dim match As Match
        Dim pattern As String = "^((\w+(\s?)){2,}),\s(\w+\s\w+),(\s\d{4}(-(\d{4}|present))?,?)+"

        'Provide minimal validation in the event the input is invalid
        If (input.Substring(startPos, endPos).Contains(",")) Then
            match = Regex.Match(input, pattern)
            Do While match.Success
                Console.Write("The {0} played in the {1} in", match.Groups(1).Value, match.Groups(4).Value)
                For Each capture As Capture In match.Groups(5).Captures
                    Console.Write(capture.Value)
                Next
                Console.WriteLine(".")
                startPos = match.Index + match.Length
                endPos = CInt(IIf(startPos + 70 <= input.Length, 70, input.Length - startPos))
                If Not input.Substring(startPos, endPos).Contains(",") Then Exit Do
                match = match.NextMatch()
            Loop
            Console.WriteLine()
        End If

        startPos = 0
        endPos = 70
        If input.Substring(startPos, endPos).Contains(",") Then
            match = Regex.Match(input, pattern, RegexOptions.Multiline)
            Do While match.Success
                Console.Write("The {0} played in the {1} in", match.Groups(1).Value, match.Groups(4).Value)
                For Each capture As Capture In match.Groups(5).Captures
                    Console.Write(capture.Value)
                Next
                Console.WriteLine(".")
                startPos = match.Index + match.Length
                endPos = CInt(IIf(startPos + 70 <= input.Length, 70, input.Length - startPos))
                If Not input.Substring(startPos, endPos).Contains(",") Then Exit Do
                match = match.NextMatch()
            Loop
            Console.WriteLine()
        End If
        Console.ReadLine()
    End Sub
    Public Sub Main21()
        Dim startPos As Integer = 0
        Dim endPos As Integer = 70
        Dim input As String = "Brooklyn Dodgers, National League, 1911, 1912, 1932-1957" + vbCrLf +
                              "Chicago Cubs, National League, 1903-present" + vbCrLf +
                              "Detroit Tigers, American League, 1901-present" + vbCrLf +
                              "New York Giants, National League, 1885-1957" + vbCrLf +
                              "Washington Senators, American League, 1901-1960" + vbCrLf
        Dim match As Match
        Dim basePattern As String = "^((\w+(\s?)){2,}),\s(\w+\s\w+),(\s\d{4}(-(\d{4}|present))?,?)+"
        Dim pattern As String = basePattern + "$"
        Console.WriteLine("Attempting to match the entire input string:")
        'Provide minimal validation in the event the input is invalid
        If (input.Substring(startPos, endPos).Contains(",")) Then
            match = Regex.Match(input, pattern)
            Do While match.Success
                Console.Write("The {0} played in the {1} in", match.Groups(1).Value, match.Groups(4).Value)
                For Each capture As Capture In match.Groups(5).Captures
                    Console.Write(capture.Value)
                Next
                Console.WriteLine(".")
                startPos = match.Index + match.Length
                endPos = CInt(IIf(startPos + 70 <= input.Length, 70, input.Length - startPos))
                If Not input.Substring(startPos, endPos).Contains(",") Then Exit Do
            Loop
            Console.WriteLine()
        End If

        Dim teams() As String = input.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        Console.WriteLine("Attempting to match each element in a string array:")
        For Each team As String In teams
            If team.Length > 70 Then Continue For
            match = Regex.Match(team, pattern)
            If match.Success Then
                Console.Write("The {0} played in the {1} in", match.Groups(1).Value, match.Groups(4).Value)
                For Each capture As Capture In match.Groups(5).Captures
                    Console.Write(capture.Value)
                Next
                Console.WriteLine(".")
            End If
        Next
        Console.WriteLine()

        startPos = 0
        endPos = 70
        Console.WriteLine("Attempting to match each line of an input string with '$':")
        ' Provide minimal validation in the event the input is invalid.
        If input.Substring(startPos, endPos).Contains(",") Then
            match = Regex.Match(input, pattern, RegexOptions.Multiline)
            Do While match.Success
                Console.Write("The {0} played in the {1} in", match.Groups(1).Value, match.Groups(4).Value)
                For Each capture As Capture In match.Groups(5).Captures
                    Console.Write(capture.Value)
                Next
                Console.WriteLine(".")
                startPos = match.Index + match.Length
                endPos = CInt(IIf(startPos + 70 <= input.Length, 70, input.Length - startPos))
                If Not input.Substring(startPos, endPos).Contains(",") Then Exit Do
                match = match.NextMatch()
            Loop
            Console.WriteLine()
        End If

        startPos = 0
        endPos = 70
        pattern = basePattern + "\r?$"
        Console.WriteLine("Attempting to match each line of an input string with '\r?$':")
        ' Provide minimal validation in the event the input is invalid
        If input.Substring(startPos, endPos).Contains(",") Then
            match = Regex.Match(input, pattern, RegexOptions.Multiline)
            Do While match.Success
                Console.Write("The {0} played in the {1} in", match.Groups(1).Value, match.Groups(4).Value)
                For Each capture As Capture In match.Groups(5).Captures
                    Console.Write(capture.Value)
                Next
                Console.WriteLine(".")
                startPos = match.Index + match.Length
                endPos = CInt(IIf(startPos + 70 <= input.Length, 70, input.Length - startPos))
                If Not input.Substring(startPos, endPos).Contains(",") Then Exit Do
                match = match.NextMatch()
            Loop
            Console.WriteLine()
        End If
        Console.ReadLine()
    End Sub
    Public Sub Main22()
        Dim startPos As Integer = 0
        Dim endPos As Integer = 70
        Dim input As String = "Brooklyn Dodgers, National League, 1911, 1912, 1932-1957" + vbCrLf +
                              "Chicago Cubs, National League, 1903-present" + vbCrLf +
                              "Detroit Tigers, American League, 1901-present" + vbCrLf +
                              "New York Giants, National League, 1885-1957" + vbCrLf +
                              "Washington Senators, American League, 1901-1960" + vbCrLf
        Dim pattern As String = "\A((\w+(\s?)){2,}),\s(\w+\s\w+),(\s\d{4}(-(\d{4}|present))?,?)+"
        Dim match As Match

        'Provide minimal validation in the event the input is invalid
        If (input.Substring(startPos, endPos).Contains(",")) Then
            match = Regex.Match(input, pattern, RegexOptions.Multiline)
            Do While match.Success
                Console.Write("The {0} played in the {1} in", match.Groups(1).Value, match.Groups(4).Value)
                For Each capture As Capture In match.Groups(5).Captures
                    Console.Write(capture.Value)
                Next
                Console.WriteLine(".")
                startPos = match.Index + match.Length
                endPos = CInt(IIf(startPos + 70 <= input.Length, 70, input.Length - startPos))
                If Not input.Substring(startPos, endPos).Contains(",") Then Exit Do
                match = match.NextMatch()
            Loop
            Console.WriteLine()
        End If
        Console.ReadLine()
    End Sub
    Public Sub Main23()
        Dim inputs As String() = {"Brooklyn Dodgers, National League, 1911, 1912, 1932-1957",
                                  "Chicago Cubs, National League, 1903-present" + vbCrLf,
                                  "Detroit Tigers, American League, 1901-present" + vbLf,
                                  "New York Giants, National League, 1885-1957",
                                  "Washington Senators, American League, 1901-1960" + vbCrLf}
        Dim pattern As String = "^((\w+(\s?)){2,}),\s(\w+\s\w+),(\s\d{4}(-(\d{4}|present))?,?)+\r?\Z"

        For Each input As String In inputs
            If input.Length > 70 Or Not input.Contains(",") Then Continue For
            Console.WriteLine(Regex.Escape(input))
            Dim match As Match = Regex.Match(input, pattern)
            If match.Success Then
                Console.WriteLine("     Match succeeded")
            Else
                Console.WriteLine("     Match failed")
            End If
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main24()
        Dim inputs As String() = {"Brooklyn Dodgers, National League, 1911, 1912, 1932-1957",
                                  "Chicago Cubs, National League, 1903-present" + vbCrLf,
                                  "Detroit Tigers, American League, 1901-present" + vbLf,
                                  "New York Giants, National League, 1885-1957",
                                  "Washington Senators, American League, 1901-1960" + vbCrLf}
        Dim pattern As String = "^((\w+(\s?)){2,}),\s(\w+\s\w+),(\s\d{4}(-(\d{4}|present))?,?)+\r?\z"
        For Each input As String In inputs
            If input.Length > 70 Or Not input.Contains(",") Then Continue For
            Console.WriteLine(Regex.Escape(input))
            Dim match As Match = Regex.Match(input, pattern)
            If match.Success Then
                Console.WriteLine("     Match succeeded.")
            Else
                Console.WriteLine("     Match failed")
            End If
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main25()
        Dim input As String = "capybara,squirrel,chipmunk,porcupine,gopher," +
                              "beaver,groundhog,hamster,guinea pig,gerbil," +
                              "chinchilla,prairie dog,mouse,rat"
        Dim pattern As String = "\G(\w+\s?\w*),?"
        Dim match As Match = Regex.Match(input, pattern)
        While match.Success
            Console.WriteLine(match.Groups(1).Value)
            match = match.NextMatch()
        End While
        Console.ReadLine()
    End Sub
    Public Sub Main26()
        Dim input As String = "area, bare, arena, mare"
        Dim pattern As String = "\bare\w*\b"
        Console.WriteLine("Words that begin with 'are':")
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine("'{0}' found at position {1}", match.Value, match.Index)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main27()
        Dim input As String = "equity queen equip acquaint quiet"
        Dim pattern As String = "\Bqu\w+"
        For Each match As Match In Regex.Matches(input, pattern)
            Console.WriteLine("'{0}' found at position {1}", match.Value, match.Index)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main28()
        Dim pattern As String = "(\w+)\s(\1)"
        Dim input As String = "He said that that was the the correct answer."
        For Each match As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
            Console.WriteLine("Duplicate '{0}' found at positions {1} and {2}",
                              match.Groups(1).Value,
                              match.Groups(1).Index,
                              match.Groups(2).Index)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main29()
        Dim pattern As String = "(?<duplicateWord>\w+)\s\k<duplicateWord>\W(?<nextWord>\w+)"
        Dim input As String = "He said that that was the the answer."
        For Each match As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
            Console.WriteLine("A duplicate '{0}' at position {1} followed by {2}",
                              match.Groups("duplicateWord").Value,
                              match.Groups("duplicateWord").Index,
                              match.Groups("nextWord").Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main30()
        Dim pattern As String = "\D+(?<digit>\d+)\D+(?<digit>\d+)?"
        Dim inputs As String() = {"abc123def456", "abc123def"}
        For Each input As String In inputs
            Dim m As Match = Regex.Match(input, pattern)
            If m.Success Then
                Console.WriteLine($"Match: {m.Value}")
                For i As Integer = 1 To m.Groups.Count - 1
                    Dim g As Group = m.Groups(i)
                    Console.WriteLine($"Group {i}: {g.Value}")
                    For c As Integer = 0 To g.Captures.Count - 1
                        Console.WriteLine($"    Capture {0}: {g.Captures(c).Value}")
                    Next
                Next
            Else
                Console.WriteLine("The match failed.")
            End If
            Console.WriteLine()
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main31()
        Dim pattern As String = "^[^<>]*" +                   '
                                "(" +                         '<Group 1>
                                "((?'Open'<)[^<>]*)+" +       '  <Group 2/>
                                "((?'Close-Open'>)[^<>]*)+" + '  <Group 3/>
                                ")*" +                        '<\Group 1>
                                "(?(Open)(?!))$"              '
        Dim input As String = "<abc><def><mno<xyz<uvw><stu>><pqr>>"          '<Group 0/>
        Dim m As Match = Regex.Match(input, pattern)
        If m.Success Then
            Console.WriteLine("Input: ""{0}"" \nMatch: ""{1}""", input, m)
            Dim i As Integer = 0
            For Each grp As Group In m.Groups
                Console.WriteLine("    Group {0}: {1}", i, grp.Value)
                i = i + 1
                Dim c As Integer = 0
                For Each cap As Capture In grp.Captures
                    Console.WriteLine("     Capture {0}: {1}", c, cap.Value)
                    c = c + 1
                Next
            Next
        Else
            Console.WriteLine("match failed.")
        End If
        Console.ReadLine()
    End Sub
    Public Sub Main32()
        Dim pattern As String = "(?:\b(?:\w+)\W*)+\."
        Dim input As String = "This is a short sentence."
        Dim match As Match = Regex.Match(input, pattern)
        Console.WriteLine("Match: {0}", match.Value)
        For i As Integer = 1 To match.Groups.Count - 1
            Console.WriteLine("    Group {0}: {1}", i, match.Groups(i).Value)
        Next
        Console.ReadLine()
    End Sub
    Public Sub Main33()
        Dim pattern As String = "\b(?ix: d \w+)\s"
        Dim input As String = "Dogs are decidedly good pets."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at index {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main34()
        Dim pattern As String = "\b\w+(?=\sis\b)"
        Dim inputs As String() = {"The dog is a Malamute.",
                                  "The island has beautiful birds.",
                                  "The pitch missed home plate.",
                                  "Sunday is a weekend day."}
        For Each input As String In inputs
            Dim m As Match = Regex.Match(input, pattern)
            If m.Success Then
                cw(String.Format("'{0}' precedes 'is'", m.Value))
            Else
                cw(String.Format("'{0}' does not match the pattern", input))
            End If
        Next
        cr()
    End Sub
    Public Sub Main35()
        Dim pattern As String = "\b(?!un)\w+\b"
        Dim input As String = "unite one unethical ethics use untie ultimate"
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
            cw(String.Format("{0}", m.Value))
        Next
        cr()
    End Sub
    Public Sub Main36()
        Dim pattern As String = "\b\w+\b(?!\p{P})"
        Dim input As String = "Disconnected, disjointed thoughts in a sentence fragment."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("{0}", m.Value))
        Next
        cr()
    End Sub
    Public Sub Main37()
        Dim input As String = "2010 1999 1861 2140 2009"
        Dim pattern As String = "(?<=\b20)\d{2}\b"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("{0}", m.Value))
        Next
        cr()
    End Sub
    Public Sub Main38()
        Dim dates As String() = {"Monday February 1, 2010",
                                 "Wednesday February 3, 2010",
                                 "Saturday February 6, 2010",
                                 "Sunday February 7, 2010",
                                 "Monday February 8, 2010"}
        Dim pattern As String = "(?<!(Saturday|Sunday) )\b\w+ \d{1,2}, \d{4}\b"
        For Each d As String In dates
            Dim m As Match = Regex.Match(d, pattern)
            If m.Success Then
                cw(String.Format("{0}", m.Value))
            End If
        Next
        cr()
    End Sub
    Public Sub Main39()
        Dim inputs As String() = {"cccd.", "aaad", "aaaa"}
        Dim back As String = "(\w)\1+.\b"
        Dim noback As String = "(?>(\w+)\1+).\b"
        For Each input As String In inputs
            Dim m1 As Match = Regex.Match(input, back)
            Dim m2 As Match = Regex.Match(input, noback)
            cw(String.Format("{0}", input))

            cw("    Backtracking:")
            If m1.Success Then
                cw(m1.Value)
            Else
                cw("No match.")
            End If

            cw("    Nonbacktracking:")
            If m2.Success Then
                cw(m2.Value)
            Else
                cw("No match.")
            End If
        Next
        cr()
    End Sub
    Public Sub Main40()
        Dim pattern As String = "(\b(\w+)\W+)+"
        Dim input As String = "This is a short sentence."
        Dim m As Match = Regex.Match(input, pattern)
        cw(String.Format("Match: {0}", m.Value))
        For i As Integer = 1 To m.Groups.Count - 1
            cw(String.Format("    Group {0}: '{1}'", i, m.Groups(i).Value))
            For Each cap As Capture In m.Groups(i).Captures
                cw(String.Format("      Capture {0}: '{1}'", i, cap.Value))
            Next
        Next
        cr()
    End Sub
    Public Sub Main41()
        Dim pattern As String = "\b91*9*\b"
        Dim input As String = "99 95 919 929 9119 9219 999 9919 91119"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main42()
        Dim pattern As String = "\ban+\w*?\b"
        Dim input As String = "Autumn is a great time for an annual announcement to all antique dealers."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main43()
        Dim pattern As String = "\ban?\b"
        Dim input As String = "An amiable animal with a large snout and an animated nose."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main44()
        Dim pattern As String = "\b\d+\,\d{3}\b"
        Dim input As String = "Sales totalled 103,524 million in January," +
                              "106,971 million in February, but only" +
                              "943 million in March."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position  {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main45()
        Dim pattern As String = "\b\d{2,}\b\D+"
        Dim input As String = "7 days, 10 weeks, 300 years"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main46()
        Dim pattern As String = "(00\s){2,4}"
        Dim input As String = "0x00 FF 00 00 18 17 FF 00 00 00 21 00 00 00 00 00"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main47()
        Dim pattern As String = "\b\w*?oo\w*?\b"
        Dim input As String = "woof root root rob oof woo woe"
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main48()
        Dim pattern As String = "\b\w+?\b"
        Dim input As String = "Aa Bb Cc Dd Ee Ff"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main49()
        Dim pattern As String = "^\s*(System.)??Console.Write(Line)??\(??"
        Dim input As String = "System.Console.WriteLine(""Hello!"")" + vbCrLf +
                                     "Console.Write(""Hello!"")" + vbCrLf +
                                     "Console.WriteLine(""Hello!"")" + vbCrLf +
                                     "Console.ReadLine()" + vbCrLf +
                                     "   Console.WriteLine"
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace Or
                                                             RegexOptions.IgnoreCase Or
                                                             RegexOptions.Multiline)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main50()
        Dim pattern As String = "\b(\w{3,}?\.){2}?\w{3,}?\b"
        Dim input As String = "www.microsoft.com msdn.microsoft.com mywebsite mycompany.com"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main51()
        Dim pattern As String = "\b[A-Z](\w*?\s*?){1,10}[.!?]"
        Dim input As String = "Hi. I am writing a short note. Its purpose is " +
                              "to test a regular expression that attempts to find " +
                              "sentences with ten or fewer words. Most sentences " +
                              "in this note are short."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main52()
        Dim greedyPattern As String = "\b.*([0-9]{4})\b"
        Dim input As String = "1112223333 3992991999"
        For Each m As Match In Regex.Matches(input, greedyPattern)
            cw(String.Format("Account ending in ******{0}.", m.Value))
        Next
        cr()
    End Sub
    Public Sub Main53()
        Dim lazyPattern As String = "\b.*?([0-9]{4})\b"
        Dim input As String = "1112223333 3992991999"
        For Each m As Match In Regex.Matches(input, lazyPattern)
            cw(String.Format("Account ending in ******{0}.", m.Value))
        Next
        cr()
    End Sub
    Public Sub Main54()
        Dim pattern As String = "(a?)*"
        Dim input As String = "aaabbb"
        Dim m As Match = Regex.Match(input, pattern)
        cw(String.Format("Match: '{0}' at index {1}", m.Value, m.Index))
        For i As Integer = 1 To m.Groups.Count
            cw(String.Format("    Group {0}: '{1}' at index {2}", i, m.Groups(i).Value, m.Groups(i).Index))
            Dim j As Integer = 0
            For Each c As Capture In m.Groups(i).Captures
                cw(String.Format("      Capture {0}: '{1}' at index {2}", j, c.Value, c.Index))
                j = j + 1
            Next
        Next
        cr()
    End Sub
    Public Sub Main55()
        Dim pattern As String = "(a\1|(?(1)\1)){0,2}"
        Dim input As String = "aaabbb"
        cw(String.Format("Regex pattern: {0}", pattern))
        Dim m As Match = Regex.Match(input, pattern)
        cw(String.Format("Match: '{0}' at index {1}", m.Value, m.Index))
        If m.Groups.Count > 1 Then
            For i As Integer = 1 To m.Groups.Count - 1
                cw(String.Format("    Group {0}: '{1}' at index {2}", i, m.Groups(i).Value, m.Groups(i).Index))
                Dim j As Integer = 0
                For Each c As Capture In m.Groups(i).Captures
                    cw(String.Format("      Capture {0}: '{1}' at index {2}", j, c.Value, c.Index))
                    j = j + 1
                Next
            Next
        End If
        cw("")

        pattern = "(a\1|(?(1)\1)){2}"
        cw(String.Format("Regex pattern: {0}", pattern))
        m = Regex.Match(input, pattern)
        cw(String.Format("Match: '{0}' at index {1}", m.Value, m.Index))
        If m.Groups.Count > 1 Then
            For i As Integer = 1 To m.Groups.Count - 1
                cw(String.Format("    Group {0}: '{1}' at index {2}", i, m.Groups(i).Value, m.Groups(i).Index))
                Dim j As Integer = 0
                For Each c As Capture In m.Groups(i).Captures
                    cw(String.Format("      Capture {0}: '{1}' at index {2}", j, c.Value, c.Index))
                    j = j + 1
                Next
            Next
        End If
        cr()
    End Sub
    Public Sub Main56()
        Dim pattern As String = "(\w)\1"
        Dim input As String = "trellis llama webbing dresser swagger"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main57()
        Dim pattern As String = "(?<char>\w)\k<char>"
        Dim input As String = "trellis llama webbing dresser swagger"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main58()
        Dim pattern As String = "(?<2>\w)\k<2>"
        Dim input As String = "trellis llama webbing dresser swagger"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main59()
        cw(Regex.IsMatch("aa", "(?<char>\w)\k<1>"))
        cr()
    End Sub
    Public Sub Main60()
        cw(Regex.IsMatch("aa", "(?<2>\w)\k<1>"))
        cr()
    End Sub
    Public Sub Main61()
        Dim pattern As String = "(?<1>a)(?<1>\1b)*"
        Dim input As String = "aababb"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("Match: {0}", m.Value))
            For Each g As Group In m.Groups
                cw(String.Format("    Group: {0}", m.Value))
            Next
        Next
        cr()
    End Sub
    Public Sub Main62()
        Dim pattern As String = "\b(\p{Lu}{2})(\d{2})?(\p{Lu}{2})\b"
        Dim inputs As String() = {"AA22ZZ", "AABB"}
        For Each input As String In inputs
            For Each m As Match In Regex.Matches(input, pattern)
                If m.Success Then
                    cw(String.Format("Match in {0}: {1}", input, m.Value))
                    If m.Groups.Count > 1 Then
                        Dim i As Integer = 0
                        For Each g As Group In m.Groups
                            cw(String.Format("Group {0}: {1}", i, IIf(g.Success, g.Value, "<no match>")))
                        Next
                    End If
                End If
            Next
        Next
        cr()
    End Sub
    Public Sub Main63()
        'Regular expression using character class
        Dim pattern1 As String = "\bgr[ae]y\b"
        'Regular expression using either/or
        Dim pattern2 As String = "\bgr(a|e)y\b"
        Dim input As String = "The gray wolf blended in among the grey rocks."
        For Each m As Match In Regex.Matches(input, pattern1)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cw("")
        For Each m As Match In Regex.Matches(input, pattern2)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main64()
        Dim pattern As String = "\b(\d{2}-\d{7}|\d{3}-\d{2}-\d{4})\b"
        Dim input As String = "01-9999999 020-333333 777-88-9999"
        cw(String.Format("Matches for {0}", pattern))
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("     {0} at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main65()
        Dim pattern As String = "\b(?(\d{2}-)\d{2}-\d{7}|\d{3}-\d{2}-\d{4})\b"
        Dim input As String = "01-9999999 020-333333 777-88-9999"
        cw(String.Format("Matches for {0}", pattern))
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("     {0} at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main66()
        Dim pattern As String = "\b(?<n2>\d{2}-)?(?(n2)\d{7}|\d{3}-\d{2}-\d{4})\b"
        Dim input As String = "01-9999999 020-333333 777-88-9999"
        cw(String.Format("Matches for {0}", pattern))
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("     {0} at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main67()
        Dim pattern As String = "\b(\d{2}-)?(?(1)\d{7}|\d{3}-\d{2}-\d{4})\b"
        Dim input As String = "01-9999999 020-333333 777-88-9999"
        cw(String.Format("Matches for {0}", pattern))
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("     {0} at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main68()
        Dim pattern As String = "\p{Sc}*(\s?\d+[.,]?\d*)\p{Sc}*"
        Dim replacement As String = "$1"
        Dim input As String = "$16.32 12.19 £16.29 €18.29 €18,29"
        Dim result As String = Regex.Replace(input, pattern, replacement)
        cw(result)
        cr()
    End Sub
    Public Sub Main69()
        Dim pattern As String = "\p{Sc}*((?<amount>\s?\d+[.,]?\d*))\p{Sc}*"
        Dim replacement As String = "${amount}"
        Dim input As String = "$16.32 12.19 £16.29 €18.29 €18,29"
        Dim result As String = Regex.Replace(input, pattern, replacement)
        cw(result)
        cr()
    End Sub
    Public Sub Main70()
        ' Define array of decimal values.
        Dim values As String() = {"16.35", "19.72", "1234", "0.99"}
        ' Determine whether currency precedes (True) or follows (False) numver.
        Dim precedes As Boolean = NumberFormatInfo.CurrentInfo.CurrencyPositivePattern Mod 2 = 0
        ' Get decimal separator.
        Dim cSeparator As String = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator
        ' Get currency symbol.
        Dim symbol As String = NumberFormatInfo.CurrentInfo.CurrencySymbol
        ' If symbol is a "$", add an extra "$".
        If symbol = "$" Then symbol = "$$"

        ' Define regular expression pattern and replacement string
        Dim pattern As String = "\b(\d+)(" + cSeparator + "(\d+))?"
        Dim replacement As String = "$1$2"
        replacement = IIf(precedes, symbol + " " + replacement, replacement + " " + symbol)
        For Each value As String In values
            cw(String.Format("{0} --> {1}", value, Regex.Replace(value, pattern, replacement)))
        Next
        cr()
    End Sub
    Public Sub Main71()
        Dim pattern As String = "^(\w+\s?)+$"
        Dim titles As String() = {"A Tale of Two Cities",
                                  "The Hound of the Baskervilles",
                                  "The Protestant Ethic and the Spirit of Capitalism",
                                  "The Origin of Species"}
        Dim replacement As String = """$&"""
        For Each title As String In titles
            cw(String.Format(Regex.Replace(title, pattern, replacement)))
        Next
        cr()
    End Sub
    Public Sub Main72()
        Dim pattern As String = "\d+"
        Dim input As String = "aa1bb2cc3dd4ee5"
        Dim substitution As String = "$`"
        cw(String.Format("Matches:"))
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("    {0} at position {1}", m.Value, m.Index))
        Next
        cw(String.Format("Input string:  {0}", input))
        cw(String.Format("Output string: {0}", Regex.Replace(input, pattern, substitution)))
        cr()
    End Sub
    Public Sub Main73()
        Dim pattern As String = "\d+"
        Dim input As String = "aa1bb2cc3dd4ee5"
        Dim substitution As String = "$'"
        cw(String.Format("Matches:"))
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("    {0} at position {1}", m.Value, m.Index))
        Next
        cw(String.Format("Input string:  {0}", input))
        cw(String.Format("Output string: {0}", Regex.Replace(input, pattern, substitution)))
        cr()
    End Sub
    Public Sub Main74()
        Dim pattern As String = "\b(\w+)\s\1\b"
        Dim substitution As String = "$+"
        Dim input As String = "The the dog jumped over the fence fence."
        cw(String.Format(Regex.Replace(input, pattern, substitution)))
        cr()
    End Sub
    Public Sub Main75()
        Dim pattern As String = "\d+"
        Dim input As String = "ABC123DEF456"
        Dim substitution As String = "$_"
        cw(String.Format("Original string:              {0}", input))
        cw(String.Format("String with substitution:     {0}", Regex.Replace(input, pattern, substitution)))
        cr()
    End Sub
    Public Sub Main76()
        Dim pattern As String = "d \w+ \s"
        Dim input As String = "Dogs are decidedly good pets."
        Dim options As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace
        For Each m As Match In Regex.Matches(input, pattern, options)
            cw(String.Format("'{0}' found at index {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main77()
        Dim pattern As String = "(?ix)d \w+ \s"
        Dim input As String = "Dogs are decidedly good pets but fish might not be bad either."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at index {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main78()
        Dim pattern As String = "(?ix: d \w+)\s"
        Dim input As String = "Dogs are decidedly good pets but fish might not be bad either."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("'{0}' found at index {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main79()
        Dim pattern As String = "\bthe\w*\b"
        Dim input As String = "The man then told them about that event."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("Found {0} at position {1}", m.Value, m.Index))
        Next
        cw("")
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
            cw(String.Format("Found {0} at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main80()
        Dim pattern As String = "\b(?i:t)he\w*\b"
        Dim input As String = "The man then told them about that event."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("Found {0} at position {1}", m.Value, m.Index))
        Next
        cw("")
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
            cw(String.Format("Found {0} at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Class DescendingComparer(Of T) : Implements IComparer(Of T)
        Public Function Compare(x As T, y As T) As Integer Implements IComparer(Of T).Compare
            Return Comparer(Of T).Default.Compare(x, y) * -1
        End Function
    End Class
    Public Sub Main81()
        Dim scores As New SortedList(Of Integer, String)(New DescendingComparer(Of Integer)())
        Dim input As String = "Joe 164" + vbCrLf +
                              "Sam 208" + vbCrLf +
                              "Allison 211" + vbCrLf +
                              "Gwen 171"
        Dim pattern As String = "^(\w+)\s(\d+)$"
        Dim matched As Boolean = False

        cw("Without Multiline option")
        For Each m As Match In Regex.Matches(input, pattern)
            scores.Add(CInt(m.Groups(2).Value), m.Groups(1).Value)
            matched = True
        Next
        If Not matched Then
            cw("    No matches.")
        End If
        cw("")

        ' Redefine the pattern to handle multiple lines
        pattern = "^(\w+)\s(\d+)\r*$"
        cw("With Multiline option:")
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.Multiline)
            scores.Add(CInt(m.Groups(2).Value), m.Groups(1).Value)
            matched = True
        Next

        For Each kvp As KeyValuePair(Of Integer, String) In scores
            cw(String.Format("{0}: {1}", kvp.Value, kvp.Key))
        Next
        cr()
    End Sub
    Public Sub Main82()
        Dim pattern As String = "^.+"
        Dim input As String = "This is one line and" + Environment.NewLine + "this is the second."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(Regex.Escape(m.Value))
        Next
        cw("")
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.Singleline)
            cw(Regex.Escape(m.Value))
        Next
        cr()
    End Sub
    Public Sub Main83()
        Dim pattern As String = "(?s)^.+"
        Dim input As String = "This is one line and" + Environment.NewLine + "this is the second."
        For Each m As Match In Regex.Matches(input, pattern)
            cw(Regex.Escape(m.Value))
        Next
        cr()
    End Sub
    Public Sub Main84()
        Dim input As String = "This is the first sentence. Is it the beginning " +
                              "of a literary masterpice? I think not. Instead, " +
                              "it is a nonsensical paragraph."
        Dim pattern As String = "\b\(?((?>\w+),?\s?)+[\.!?]\)?"
        cw("With implicit captures:")
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("The Match: {0}", m.Value))
            Dim i As Integer = 0
            For Each g As Group In m.Groups
                cw(String.Format("    Group: {0}: {1}", i, g.Value))
                i = i + 1
                Dim j As Integer = 0
                For Each c As Capture In g.Captures
                    cw(String.Format("      Capture: {0}: {1}", j, c.Value))
                    j = j + 1
                Next
            Next
        Next
        cw("Without implicit captures")
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.ExplicitCapture)
            cw(String.Format("The Match: {0}", m.Value))
            Dim i As Integer = 0
            For Each g As Group In m.Groups
                i = i + 1
                cw(String.Format("    Group: {0}: {1}", i, g.Value))
                Dim j As Integer = 0
                For Each c As Capture In g.Captures
                    cw(String.Format("      Capture: {0}: {1}", j, c.Value))
                    j = j + 1
                Next
            Next
        Next
        cr()
    End Sub
    Public Sub Main85()
        Dim input As String = "This is the first sentence. Is it the beginning " +
                              "of a literary masterpice? I think not. Instead, " +
                              "it is a nonsensical paragraph."
        Dim pattern As String = "(?n)\b\(?((?>\w+),?\s?)+[\.!?]\)?"

        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("The Match: {0}", m.Value))
            Dim i As Integer = 0
            For Each g As Group In m.Groups
                cw(String.Format("    Group: {0}: {1}", i, g.Value))
                i = i + 1
                Dim j As Integer = 0
                For Each c As Capture In g.Captures
                    cw(String.Format("      Capture: {0}: {1}", j, c.Value))
                    j = j + 1
                Next
            Next
        Next
        cr()
    End Sub
    Public Sub Main86()
        Dim input As String = "This is the first sentence. Is it the beginning " +
                              "of a literary masterpice? I think not. Instead, " +
                              "it is a nonsensical paragraph."
        Dim pattern As String = "\b\(?(?n:(?>\w+),?\s?)+[\.!?]\)?"

        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("The Match: {0}", m.Value))
            Dim i As Integer = 0
            For Each g As Group In m.Groups
                cw(String.Format("    Group: {0}: {1}", i, g.Value))
                i = i + 1
                Dim j As Integer = 0
                For Each c As Capture In g.Captures
                    cw(String.Format("      Capture: {0}: {1}", j, c.Value))
                    j = j + 1
                Next
            Next
        Next
        cr()
    End Sub
    Public Sub Main87()
        Dim input As String = "This is the first sentence. Is it the beginning " +
                              "of a literary masterpice? I think not. Instead, " +
                              "it is a nonsensical paragraph."
        Dim pattern As String = "\b\(?(?n:(?>\w+),?\s?)+[\.!?]\)?"
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace)
            cw(String.Format("The Match: {0}", m.Value))
        Next
        cr()
    End Sub
    Public Sub Main88()
        Dim input As String = "This is the first sentence. Is it the beginning " +
                              "of a literary masterpice? I think not. Instead, " +
                              "it is a nonsensical paragraph."
        Dim pattern As String = "(?x)\b \(? ( (?>\w+) ,?\s? )+ [\.!?] \)? # Matches an entire sentence."
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace)
            cw(String.Format("The Match: {0}", m.Value))
        Next
        cr()
    End Sub
    Public Sub Main89()
        Dim pattern As String = "\bb\w+\s"
        Dim input As String = "builder rob rabble"
        For Each m As Match In Regex.Matches(input, pattern, RegexOptions.RightToLeft)
            cw(String.Format("'{0}' found at position {1}", m.Value, m.Index))
        Next
        cr()
    End Sub
    Public Sub Main90()
        Dim pattern As String = "(?<=\d{1,2}\s)\w+,?\s\d{4}"
        Dim inputs As String() = {"1 May 1917", "June 16, 2003"}
        For Each input As String In inputs
            Dim m As Match = Regex.Match(input, pattern)
            If m.Success Then
                cw(String.Format("The date occurs in {0}", m.Value))
            Else
                cw(String.Format("{0} does not match.", input))
            End If
        Next
        cr()
    End Sub
    Public Sub Main91()
        Dim pattern As String = "\b(\w+\s*)+"
        Dim values As String() = {"целый мир", "the whole world"}
        For Each value As String In values
            cw("Canonical matching:")
            If Regex.IsMatch(value, pattern) Then
                cw(String.Format("'{0}' matches the pattern", value))
            Else
                cw(String.Format("'{0}' does not match the pattern", value))
            End If
            cw("ECMAScript matching:")
            If Regex.IsMatch(value, pattern, RegexOptions.ECMAScript) Then
                cw(String.Format("'{0}' matches the pattern", value))
            Else
                cw(String.Format("'{0}' does not match the pattern", value))
            End If
        Next
        cr()
    End Sub
    Public Sub Main93()
        Dim defaultCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
        Thread.CurrentThread.CurrentCulture = New CultureInfo("tr-TR")

        Dim input As String = "file://C:/Documents.MyReport.Doc"
        Dim pattern As String = "$FILE://"

        cw("Culture-sensitive matching: " + Thread.CurrentThread.CurrentCulture.Name + " culture...")
        If Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase) Then
            cw("URLs that access files are not allowed")
        Else
            cw("Access to " + input + " is allowed.")
        End If
        Thread.CurrentThread.CurrentCulture = defaultCulture
        cr()
    End Sub
    Public Sub Main94()
        Dim defaultCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
        Thread.CurrentThread.CurrentCulture = New CultureInfo("tr-TR")

        Dim input As String = "file://C:/Documents.MyReport.Doc"
        Dim pattern As String = "$FILE://"

        cw("Culture-insensitive matching: " + Thread.CurrentThread.CurrentCulture.Name + " culture...")
        If Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant) Then
            cw("URLs that access files are not allowed")
        Else
            cw("Access to " + input + " is allowed.")
        End If
        Thread.CurrentThread.CurrentCulture = defaultCulture
        cr()
    End Sub
    Public Sub Main95()
        Dim pattern As String
        Dim input As String = "double dare double Double a Drooling dog The dreaded Deep"

        pattern = "\b(D\w+)\s(d\w+)\b"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("{0}", m.Value))
            If m.Groups.Count > 1 Then
                For i As Integer = 1 To m.Groups.Count - 1
                    cw(String.Format("    Group {0}: {1}", i, m.Groups(1).Value))
                Next
            End If
        Next
        cw("")
        pattern = "\b(D\w+)(?ixn) \s (d\w+) \b"
        For Each m As Match In Regex.Matches(input, pattern)
            cw(String.Format("{0}", m.Value))
            If m.Groups.Count > 1 Then
                For i As Integer = 1 To m.Groups.Count - 1
                    cw(String.Format("    Group {0}: {1}", i, m.Groups(1).Value))
                Next
            End If
        Next

        cr()
    End Sub
    Public Sub Main96()
        Dim pattern As String = "\b((?# case-sensitive comparison)D\w+)\s(?ixn)((?#case-insensitive comparison)d\w+)\b"
        Dim input As String = "double dare double Double a Drooling dog The dreaded Deep"
        Dim rgx As Regex = New Regex(pattern)

        cw("Pattern: " + pattern)
        ' Match pattern using default options
        For Each m As Match In rgx.Matches(input)
            cw(String.Format("{0}", m.Value))
            If m.Groups.Count > 1 Then
                For i As Integer = 1 To m.Groups.Count - 1
                    cw(String.Format("    Group {0}: {1}", i, m.Groups(1).Value))
                Next
            End If
        Next
        cr()
    End Sub
    Public Sub Main97()
        Dim pattern As String = "\{\d+(,-*\d+)*(\:\w{1,4}?)*\}(?x) # Looks for a composite format item."
        Dim input As String = "{0,-3:F}"
        cw(input)
        If Regex.IsMatch(input, pattern) Then
            cw("   Contains a composite format item.")
        Else
            cw("   Does not contain a composite format item.")
        End If
        cr()
    End Sub
    Public Sub Main98()
        Dim sw As Stopwatch
        Dim addresses As String() = {"AAAAAAAAAAA@contoso.com", "AAAAAAAAAAaaaaaaaaaa!@contoso.com"}
        ' The following regular expression should Not actually be used to
        ' validate an email address.
        Dim pattern As String = "^[0-9A-Z]([-.\w]*[0-9A-Z])*$"
        Dim input As String = ""
        For Each address As String In addresses
            Dim mailbox As String = address.Substring(0, address.IndexOf("@"))
            Dim i As Integer = 0
            For ctr As Integer = mailbox.Length - 1 To 0 Step -1
                i += 1
                input = mailbox.Substring(ctr, i)
                sw = Stopwatch.StartNew()
                Dim m As Match = Regex.Match(input, pattern, RegexOptions.IgnoreCase)
                sw.Stop()
                If m.Success Then
                    cw(String.Format("{0,2}. Matched '{1,25}' in {2}", i, m.Value, sw.Elapsed))
                Else
                    cw(String.Format("{0,2}. Failed '{1,25}' in {2}", i, input, sw.Elapsed))
                End If
            Next
        Next
        cr()
    End Sub
    Public Function IsValidCurrency(ccyValue As String)
        Dim pattern As String = "\p{Sc}+\s*\d+"
        Return Regex.IsMatch(ccyValue, pattern)
    End Function
    Public Sub Main99()
        Dim pattern As String = "\b(\w+((\r?\n)|,?\s))*\w+[.?!;:]"
        Dim sw As Stopwatch
        Dim m As Match
        Dim i As Integer

        Dim inFile As StreamReader = New StreamReader("C:\Users\Dragon\Documents\Books\Not art of war.txt")
        Dim input As String = inFile.ReadToEnd()
        inFile.Close()

        ' Read the first ten pages with interpreted Regex
        cw("Read the first ten pages with interpreted Regex")
        sw = Stopwatch.StartNew()
        Dim int10 As Regex = New Regex(pattern, RegexOptions.Singleline)
        m = int10.Match(input)
        For i = 1 To 10
            If m.Success Then
                m = m.NextMatch()
            Else
                Exit For
            End If
        Next
        sw.Stop()
        cw(String.Format("   {0} matches in {1}", i, sw.Elapsed))
        ' Read the first ten pages with compiled Regex
        cw("Read the first ten pages with compiled Regex")
        sw = Stopwatch.StartNew()
        Dim comp10 As Regex = New Regex(pattern, RegexOptions.Singleline Or RegexOptions.Compiled)
        m = comp10.Match(input)
        For i = 1 To 10
            If m.Success Then
                m = m.NextMatch()
            Else
                Exit For
            End If
        Next
        sw.Stop()
        cw(String.Format("   {0} matches in {1}", i, sw.Elapsed))

        ' Read all pages with interpreted Regex
        cw("Read all pages with interpreted Regex")
        sw = Stopwatch.StartNew()
        Dim intAll As Regex = New Regex(pattern, RegexOptions.Singleline)
        m = intAll.Match(input)
        Dim matches As Integer = 0
        While m.Success
            matches += 1
            m = m.NextMatch()
        End While
        sw.Stop()
        cw(String.Format("   {0} matches in {1}", i, sw.Elapsed))

        ' Read all pages with compiled Regex
        cw("Read all pages with compiled Regex")
        sw = Stopwatch.StartNew()
        Dim compAll As Regex = New Regex(pattern, RegexOptions.Singleline Or RegexOptions.Compiled)
        m = compAll.Match(input)
        matches = 0
        While m.Success
            matches += 1
            m = m.NextMatch()
        End While
        sw.Stop()
        cw(String.Format("   {0} matches in {1}", i, sw.Elapsed))


        cr()
    End Sub
    Public Sub Main100()
        Dim sentencePattern As RegexCompilationInfo = New RegexCompilationInfo(
            "\b(\w+((\r?\n)|,?\s))*\w+[.?!;:]",
            RegexOptions.Multiline,
            "SentencePattern",
            "Utilities.RegularExpressions",
            True)
        Dim regexes As RegexCompilationInfo() = {sentencePattern}
        Dim assemName As AssemblyName = New AssemblyName("RegexLib, Version=1.0.0.1001, Culture=neutral, PublicKeyToken=null")
        Regex.CompileToAssembly(regexes, assemName)
        cr()
    End Sub

    Public Sub cw(intStr As String)
        Console.WriteLine(intStr)
    End Sub
    Public Sub cr()
        Console.ReadLine()
    End Sub
`
End Module
Module example
    Dim pattern As String
    Public Sub Main92()
        pattern = "((a+)(\1) ?)+"
        Dim input As String = "aa aaaa aaaaaa "

        ' Match using canonical matching
        AnalyseMatch(Regex.Match(input, pattern))
        ' Match using ECMAScript
        AnalyseMatch(Regex.Match(input, pattern, RegexOptions.ECMAScript))
        cr()
    End Sub
    Private Sub AnalyseMatch(m As Match)
        If m.Success Then
            cw(String.Format("'{0}' matches {1} at position {2}", pattern, m.Value, m.Index))
            Dim i As Integer = 0
            For Each g As Group In m.Groups
                cw(String.Format("    {0}: '{1}'", i, g.Value))
                i = i + 1
                Dim j As Integer = 0
                For Each c As Capture In g.Captures
                    cw(String.Format("     {0}: '{1}'", j, c.Value))
                    j = j + 1
                Next
            Next
        Else
            cw("No match found.")
        End If
    End Sub
End Module
