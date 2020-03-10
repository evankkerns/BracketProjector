Public Class TestTheFormula
    Public totalSims As Integer = 200000
    Public timesWrong(32) As Integer
    Public perfectRounds As Integer = 0
    Public bestRound As Integer = 32
    Public worstRound As Integer = 0

    Sub New()

    End Sub

    Public Function testTheFormula() As String
        Dim sb As New StringBuilder
        Dim sbTest As New StringBuilder
        Dim theRankings As KenPomRankings = MarchAlgorithm.loadRankings()
        Dim theBracket As List(Of String) = MarchAlgorithm.getBracketList2019()
        Dim realWinners As List(Of String) = testBracketRd1in2019()
        Dim r As New Random
        Dim startTime As DateTime = DateTime.Now

        For i = 0 To totalSims - 1
            sbTest.Append((i + 1).ToString + ") " + testRound(theRankings, theBracket, realWinners, 64, r) + "<br/>")
        Next

        sb.Append("<h2>Total Sims: " + totalSims.ToString("N0") + " brackets</h2><br/>")
        sb.Append("Perfect Rounds: " + perfectRounds.ToString + "<br/>")
        sb.Append("Best Round: " + bestRound.ToString + "/32 wrong<br/>")
        sb.Append("Worst Round: " + worstRound.ToString + "/32 wrong<br/>")
        sb.Append("<br/>")

        sb.Append("<table>")

        'East
        sb.Append("<td>")
        For j = 0 To 7
            sb.Append((timesWrong(j) / totalSims * 100).ToString + "% wrong. (" + realWinners(j) + ")<br/>")
        Next
        sb.Append("<br/>")
        'West
        For k = 8 To 15
            sb.Append((timesWrong(k) / totalSims * 100).ToString + "% wrong. (" + realWinners(k) + ")<br/>")
        Next
        sb.Append("</td>")

        'South
        sb.Append("<td>")
        For m = 16 To 23
            sb.Append((timesWrong(m) / totalSims * 100).ToString + "% wrong. (" + realWinners(m) + ")<br/>")
        Next
        sb.Append("<br/>")
        'Midwest
        For n = 24 To 31
            sb.Append((timesWrong(n) / totalSims * 100).ToString + "% wrong. (" + realWinners(n) + ")<br/>")
        Next
        sb.Append("</td>")

        sb.Append("</table>")

        sb.Append("<br/>Total Sim Time: " + (DateTime.Now - startTime).TotalSeconds.ToString + "s")

        Return sb.ToString
    End Function

    Public Function testRound(theRankings As KenPomRankings, theBracket As List(Of String), realWinners As List(Of String), teamsInRound As Integer, r As Random) As String
        Dim totalWrong As Integer = 0
        Dim cntr As Integer = 0

        For i = 0 To teamsInRound - 2 Step 2
            Dim teamA As Team = MarchAlgorithm.getKPR(theRankings, theBracket(i))
            Dim teamB As Team = MarchAlgorithm.getKPR(theRankings, theBracket(i + 1))

            Dim oDiff As Double = (teamA.AdjO - teamB.AdjO) * 3
            Dim dDiff As Double = (teamB.AdjD - teamA.AdjD) * 4
            Dim diff As Double = (oDiff + dDiff) / 2

            Dim score = r.Next(1, 100)
            If score < diff + 50 Then
                If theBracket(i) <> realWinners(cntr) Then
                    timesWrong(cntr) += 1
                    totalWrong += 1
                    'Return "Gm" + cntr.ToString + " " + theBracket(i) + " was an incorret pick."
                End If
            Else
                If theBracket(i + 1) <> realWinners(cntr) Then
                    timesWrong(cntr) += 1
                    totalWrong += 1
                    'Return "Gm" + cntr.ToString + " " + theBracket(i + 1) + " was an incorret pick."
                End If
            End If
            cntr += 1
        Next

        If totalWrong = 0 Then perfectRounds += 1
        If totalWrong > worstRound Then worstRound = totalWrong
        If totalWrong < bestRound Then bestRound = totalWrong

        Return "=== Perfect round! ==="
    End Function

    Public Function testBracketRd1in2019() As List(Of String)
        Return New List(Of String) From {
            "Duke 1", "UCF 9",
            "Liberty 12", "Virginia Tech 4",
            "Maryland 6", "LSU 3",
            "Minnesota 10", "Michigan St. 2",
            "Gonzaga 1", "Baylor 9",
            "Murray St. 12", "Florida St. 4",
            "Buffalo 6", "Texas Tech 3",
            "Florida 10", "Michigan 2",
            "Virginia 1", "Oklahoma 9",
            "Oregon 12", "UC Irvine 13",
            "Villanova 6", "Purdue 3",
            "Iowa 10", "Tennessee 2",
            "North Carolina 1", "Washington 9",
            "Auburn 5", "Kansas 4",
            "Ohio St. 11", "Houston 3",
            "Wofford 7", "Kentucky 2"
        }
    End Function
End Class
