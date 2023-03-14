Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class TestTheFormula
    Public totalSims As Integer = 2000
    Public timesWrong(32) As Integer
    Public simPerc(32) As Integer
    Public perfectRounds As Integer = 0
    Public bestRound As Integer = 32
    Public worstRound As Integer = 0

    Sub New()

    End Sub

    Public Function getWinnerCount() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = MarchAlgorithm.loadRankings()
        Dim winnerCount(68) As Integer
        Dim tournTeams() As String = {"test",
            "Gonzaga 1", "Norfolk St. 16", "Oklahoma 8", "Missouri 9",
            "Creighton 5", "UC Santa Barbara 12", "Virginia 4", "Ohio 13",
            "USC 6", "Drake 11", "Kansas 3", "Eastern Washington 14",
            "Oregon 7", "VCU 10", "Iowa 2", "Grand Canyon 15",
            "Michigan 1", "Texas Southern 16", "LSU 8", "St. Bonaventure 9",
            "Colorado 5", "Georgetown 12", "Florida St. 4", "UNC Greensboro 13",
            "BYU 6", "UCLA 11", "Texas 3", "Abilene Christian 14",
            "Connecticut 7", "Maryland 10", "Alabama 2", "Iona 15",
            "Baylor 1", "Hartford 16", "North Carolina 8", "Wisconsin 9",
            "Villanova 5", "Winthrop 12", "Purdue 4", "North Texas 13",
            "Texas Tech 6", "Utah St. 11", "Arkansas 3", "Colgate 14",
            "Florida 7", "Virginia Tech 10", "Ohio St. 2", "Oral Roberts 15",
            "Illinois 1", "Drexel 16", "Loyola Chicago 8", "Georgia Tech 9",
            "Tennessee 5", "Oregon St. 12", "Oklahoma St. 4", "Liberty 13",
            "San Diego St. 6", "Syracuse 11", "West Virginia 3", "Morehead St. 14",
            "Clemson 7", "Rutgers 10", "Houston 2", "Cleveland St. 15"
        }

        For i = 0 To totalSims - 1
            Dim theBracket As List(Of String) = MarchAlgorithm.getBracketList2022()
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 64)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 32)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 16)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 8)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 4)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 2)

            Dim winner = Array.IndexOf(tournTeams, theBracket(0))
            winnerCount(winner) += 1

            Threading.Thread.Sleep(200)
        Next

        For i = 0 To tournTeams.Length - 1
            If winnerCount(i) > 0 Then
                sb.Append(winnerCount(i).ToString + ": " + tournTeams(i) + "<br/>")
            End If
        Next

        Return sb.ToString
    End Function

    Public Function simFinalFourCount() As String
        Dim sb As New StringBuilder
        Dim sbTest As New StringBuilder
        Dim theRankings As KenPomRankings = MarchAlgorithm.loadRankings()
        'Dim theFirstFour As List(Of String) = MarchAlgorithm.getFirstFour2020()
        Dim theBracket As List(Of String) = MarchAlgorithm.getBracketList2022()
        Dim r As New Random
        Dim startTime As DateTime = DateTime.Now

        Dim winnerCount(68) As Double
        Dim tournTeams() As String = {"test",
            "Gonzaga 1", "Georgia St. 16", "Boise St. 8", "Memphis 9",
            "Connecticut 5", "New Mexico St. 12", "Arkansas 4", "Vermont 13",
            "Alabama 6", "Notre Dame 11", "Texas Tech 3", "Montana St. 14",
            "Michigan St. 7", "Davidson 10", "Duke 2", "Cal St. Fullerton 15",
            "Baylor 1", "Norfolk St. 16", "North Carolina 8", "Marquette 9",
            "Saint Mary's 5", "Indiana 12", "UCLA 4", "Akron 13",
            "Texas 6", "Virginia Tech 11", "Purdue 3", "Yale 14",
            "Murray St. 7", "San Francisco 10", "Kentucky 2", "Saint Peter's 15",
            "Arizona 1", "Wright St. 16", "Seton Hall 8", "TCU 9",
            "Houston 5", "UAB 12", "Illinois 4", "Chattanooga 13",
            "Colorado St. 6", "Michigan 11", "Tennessee 3", "Longwood 14",
            "Ohio St. 7", "Loyola Chicago 10", "Villanova 2", "Delaware 15",
            "Kansas 1", "Texas Southern 16", "San Diego St. 8", "Creighton 9",
            "Iowa 5", "Richmond 12", "Providence 4", "South Dakota St. 13",
            "LSU 6", "Iowa St. 11", "Wisconsin 3", "Colgate 14",
            "USC 7", "Miami FL 10", "Auburn 2", "Jacksonville St. 15"
        }

        For i = 0 To totalSims - 1
            theBracket = MarchAlgorithm.getBracketList2022()
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 64)
            'Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 32)
            'Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 16)
            'Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 8)

            For j = 0 To 3
                Dim winner = Array.IndexOf(tournTeams, theBracket(j))
                winnerCount(winner) += 1
            Next

            Threading.Thread.Sleep(200)
        Next

        sb.Append("<h2>Total Sims: " + totalSims.ToString("N0") + " brackets</h2><br/>")
        sb.Append("<br/>")

        sb.Append("<table>")

        'For k = 0 To tournTeams.Length - 1
        '    If winnerCount(k) > 0 Then
        '        sb.Append("| | " + winnerCount(k).ToString + ": " + tournTeams(k) + "<br/>")
        '    End If
        'Next

        sb.Append("<table>")

        'East
        sb.Append("<td>")
        For k = 1 To 16
            If winnerCount(k) > 0 Then
                sb.Append("|--   " + winnerCount(k).ToString + " (" + (winnerCount(k) / totalSims * 100).ToString + "%): " + tournTeams(k) + "<br/>")
            Else
                sb.Append("|--   0: " + tournTeams(k) + "<br/>")
            End If
        Next
        sb.Append("<br/>")
        'West
        For l = 17 To 32
            If winnerCount(l) > 0 Then
                sb.Append("|--   " + winnerCount(l).ToString + " (" + (winnerCount(l) / totalSims * 100).ToString + "%): " + tournTeams(l) + "<br/>")
            Else
                sb.Append("|--   0: " + tournTeams(l) + "<br/>")
            End If
        Next
        sb.Append("</td>")

        'South
        sb.Append("<td>")
        For m = 33 To 48
            If winnerCount(m) > 0 Then
                sb.Append("|--   " + winnerCount(m).ToString + " (" + (winnerCount(m) / totalSims * 100).ToString + "%): " + tournTeams(m) + "<br/>")
            Else
                sb.Append("|--   0: " + tournTeams(m) + "<br/>")
            End If
        Next
        sb.Append("<br/>")
        'Midwest
        For n = 49 To 64
            If winnerCount(n) > 0 Then
                sb.Append("|--   " + winnerCount(n).ToString + " (" + (winnerCount(n) / totalSims * 100).ToString + "%): " + tournTeams(n) + "<br/>")
            Else
                sb.Append("|--   0: " + tournTeams(n) + "<br/>")
            End If
        Next
        sb.Append("</td>")

        sb.Append("</table>")

        sb.Append("<br/>Total Sim Time: " + (DateTime.Now - startTime).TotalSeconds.ToString + "s")

        Return sb.ToString
    End Function

    Public Function getWinnerCountSecondChance() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = MarchAlgorithm.loadRankings()
        Dim winnerCount(68) As Integer
        Dim tournTeams() As String = {"test",
            "Gonzaga 1",
            "Creighton 5",
            "USC 6",
            "Oregon 7",
            "Michigan 1",
            "Florida St. 4",
            "UCLA 11",
            "Alabama 2",
            "Baylor 1",
            "Villanova 5",
            "Arkansas 3",
            "Oral Roberts 15",
            "Loyola Chicago 8",
            "Oregon St. 12",
            "Syracuse 11",
            "Houston 2"
        }

        For i = 0 To totalSims - 1
            Dim theBracket As List(Of String) = MarchAlgorithm.getSweetSixteen2021()
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 16)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 8)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 4)
            Threading.Thread.Sleep(1000)
            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 2)

            Dim winner = Array.IndexOf(tournTeams, theBracket(0))
            winnerCount(winner) += 1

            Threading.Thread.Sleep(200)
        Next

        For i = 0 To tournTeams.Length - 1
            If winnerCount(i) > 0 Then
                sb.Append(winnerCount(i).ToString + ": " + tournTeams(i) + "<br/>")
            End If
        Next

        Return sb.ToString
    End Function

    Public Function getWinnerCountOneMatchup() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = MarchAlgorithm.loadRankings()
        Dim winnerCount(68) As Integer
        Dim tournTeams() As String = {"test",
            "Arkansas 3",
            "Oral Roberts 15"
        }

        For i = 0 To totalSims - 1
            Dim theBracket As List(Of String) = New List(Of String)({"Arkansas 3", "Oral Roberts 15"})

            theBracket = MarchAlgorithm.simRound(theRankings, theBracket, 2)

            Dim winner = Array.IndexOf(tournTeams, theBracket(0))
            winnerCount(winner) += 1

            Threading.Thread.Sleep(200)
        Next

        For i = 0 To tournTeams.Length - 1
            If winnerCount(i) > 0 Then
                sb.Append(winnerCount(i).ToString + ": " + tournTeams(i) + "<br/>")
            End If
        Next

        Return sb.ToString
    End Function

    Public Function simTheFirstRound() As String
        Dim sb As New StringBuilder
        Dim sbTest As New StringBuilder
        Dim theRankings As KenPomRankings = MarchAlgorithm.loadRankings()
        'Dim theFirstFour As List(Of String) = MarchAlgorithm.getFirstFour2020()
        Dim theBracket As List(Of String) = MarchAlgorithm.getBracketList2022()
        Dim r As New Random
        Dim startTime As DateTime = DateTime.Now

        For i = 0 To totalSims - 1
            sbTest.Append((i + 1).ToString + ") " + simFirstRound(theRankings, theBracket, 64, r) + "<br/>")
        Next

        sb.Append("<h2>Total Sims: " + totalSims.ToString("N0") + " brackets</h2><br/>")
        sb.Append("<br/>")

        sb.Append("<table>")

        'East
        sb.Append("<td>")
        For j = 0 To 7
            sb.Append(" | " + (100 - (simPerc(j) / totalSims * 100)).ToString + "% win - " + theBracket(j * 2) + "<br/>")
        Next
        sb.Append("<br/>")
        'West
        For k = 8 To 15
            sb.Append(" | " + (100 - (simPerc(k) / totalSims * 100)).ToString + "% win - " + theBracket(k * 2) + "<br/>")
        Next
        sb.Append("</td>")

        'South
        sb.Append("<td>")
        For m = 16 To 23
            sb.Append(" | " + (100 - (simPerc(m) / totalSims * 100)).ToString + "% win - " + theBracket(m * 2) + "<br/>")
        Next
        sb.Append("<br/>")
        'Midwest
        For n = 24 To 31
            sb.Append(" | " + (100 - (simPerc(n) / totalSims * 100)).ToString + "% win - " + theBracket(n * 2) + "<br/>")
        Next
        sb.Append("</td>")

        sb.Append("</table>")

        sb.Append("<br/>Total Sim Time: " + (DateTime.Now - startTime).TotalSeconds.ToString + "s")

        Return sb.ToString
    End Function

    Public Function simFirstRound(theRankings As KenPomRankings, theBracket As List(Of String), teamsInRound As Integer, r As Random) As String
        Dim totalWrong As Integer = 0
        Dim cntr As Integer = 0

        For i = 0 To teamsInRound - 2 Step 2
            Dim teamA As Team = MarchAlgorithm.getKPR(theRankings, theBracket(i))
            Dim teamB As Team = MarchAlgorithm.getKPR(theRankings, theBracket(i + 1))

            If (teamA.AdjO = 0 Or teamA.AdjD = 0 Or teamB.AdjO = 0 Or teamB.AdjD = 0) Then
                Dim breakhere As String = ""
            End If

            Dim oDiff As Double = (teamA.AdjO - teamB.AdjO) * 4
            Dim dDiff As Double = (teamB.AdjD - teamA.AdjD) * 5
            Dim diff As Double = (oDiff + dDiff) / 2

            Dim score = r.Next(1, 100)
            If score < diff + 50 Then
            Else
                simPerc(cntr) += 1
            End If
            cntr += 1
        Next

        Return ""
    End Function

    Public Function testTheFormula() As String
        Dim sb As New StringBuilder
        Dim sbTest As New StringBuilder
        Dim theRankings As KenPomRankings = MarchAlgorithm.loadRankings()
        'Dim theFirstFour As List(Of String) = MarchAlgorithm.getFirstFour2020()
        Dim theBracket As List(Of String) = MarchAlgorithm.getBracketList2021()
        Dim realWinners As List(Of String) = testBracketRd1in2021()
        Dim r As New Random
        Dim startTime As DateTime = DateTime.Now

        For i = 0 To totalSims - 1
            sbTest.Append((i + 1).ToString + ") " + testRound(theRankings, theBracket, realWinners, 64, r) + "<br/>")
        Next

        sb.Append("<h2>Total Sims: " + totalSims.ToString("N0") + " brackets</h2><br/>")
        sb.Append("Perfect Rounds: " + perfectRounds.ToString + "<br/>")
        sb.Append("Best Round: " + (32 - bestRound).ToString + "/32 correct<br/>")
        sb.Append("Worst Round: " + (32 - worstRound).ToString + "/32 correct<br/>")
        sb.Append("<br/>")

        sb.Append("<table>")

        'East
        sb.Append("<td>")
        For j = 0 To 7
            sb.Append((100 - (timesWrong(j) / totalSims * 100)).ToString + "% correct (" + realWinners(j) + ")<br/>")
        Next
        sb.Append("<br/>")
        'West
        For k = 8 To 15
            sb.Append((100 - (timesWrong(k) / totalSims * 100)).ToString + "% correct (" + realWinners(k) + ")<br/>")
        Next
        sb.Append("</td>")

        'South
        sb.Append("<td>")
        For m = 16 To 23
            sb.Append((100 - (timesWrong(m) / totalSims * 100)).ToString + "% correct (" + realWinners(m) + ")<br/>")
        Next
        sb.Append("<br/>")
        'Midwest
        For n = 24 To 31
            sb.Append((100 - (timesWrong(n) / totalSims * 100)).ToString + "% correct (" + realWinners(n) + ")<br/>")
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

    Public Shared Function testBracketRd1in2021() As List(Of String)
        Return New List(Of String) From {
            "Gonzaga 1", "Oklahoma 8",
            "Creighton 5", "Ohio 13",
            "USC 6", "Kansas 3",
            "Oregon 7", "Iowa 2",
            "Michigan 1", "LSU 8",
            "Colorado 5", "Florida St. 4",
            "UCLA 11", "Abilene Christian 14",
            "Maryland 10", "Alabama 2",
            "Baylor 1", "Wisconsin 9",
            "Villanova 5", "North Texas 13",
            "Texas Tech 6", "Arkansas 3",
            "Florida 7", "Oral Roberts 15",
            "Illinois 1", "Loyola Chicago 8",
            "Oregon St. 12", "Oklahoma St. 4",
            "Syracuse 11", "West Virginia 3",
            "Rutgers 10", "Houston 2"
        }
        'Appalachian St. 16 / Norfolk St. 16
        'Drake 11 / Wichita St. 11
        'Texas Southern 16 / Mount St. Mary's 16
        'UCLA 11 / Michigan St. 11
    End Function
End Class
