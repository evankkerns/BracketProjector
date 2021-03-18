Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web
Imports System.Web.Script.Serialization

Public Class MarchAlgorithm
    Dim timesWrong(16) As Integer

    Public Sub New()
        predictBracket()
    End Sub

    Public Shared Function runBracket() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = loadRankings()
        Dim theBracket As List(Of String) = getBracketList2021()

        sb.Append("<table>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 1))
        theBracket = simRound(theRankings, theBracket, 64)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 2))
        theBracket = simRound(theRankings, theBracket, 32)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 4))
        theBracket = simRound(theRankings, theBracket, 16)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 8))
        theBracket = simRound(theRankings, theBracket, 8)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 16))
        theBracket = simRound(theRankings, theBracket, 4)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 32))
        theBracket = simRound(theRankings, theBracket, 2)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 64))
        sb.Append("</td>")

        sb.Append("</table>")

        Return sb.ToString
    End Function

    Public Shared Function simRound(theRankings As KenPomRankings, theBracket As List(Of String), teamsInRound As Integer) As List(Of String)
        Dim newBracket As New List(Of String)
        Dim r As New Random

        For i = 0 To teamsInRound - 2 Step 2
            Dim teamA As Team = getKPR(theRankings, theBracket(i))
            Dim teamB As Team = getKPR(theRankings, theBracket(i + 1))

            Dim oDiff As Double = (teamA.AdjO - teamB.AdjO) * 4
            Dim dDiff As Double = (teamB.AdjD - teamA.AdjD) * 5
            Dim diff As Double = (oDiff + dDiff) / 2

            'Threading.Thread.Sleep(100)
            Dim score = r.Next(1, 101)
            If score < diff + 50 Then
                newBracket.Add(theBracket(i))
            Else
                newBracket.Add(theBracket(i + 1))
            End If
        Next

        Return newBracket
    End Function

    Public Shared Function printTeams(theBracket As List(Of String), round As Integer) As String
        Dim sb As New StringBuilder

        For Each team In theBracket
            sb.Append(team)
            For i = 0 To round - 1
                sb.Append("<br/>")
            Next
        Next

        Return sb.ToString
    End Function

    Public Shared Function getKPR(theRankings As KenPomRankings, teamName As String) As Team
        For Each team In theRankings.teams
            If teamName = team.Team Then
                Return team
            End If
        Next

        Return New Team
    End Function


    Public Shared Function getBracketList2021() As List(Of String)
        Return New List(Of String) From {
            "Gonzaga 1", "Norfolk St. 16", "Oklahoma 8", "Missouri 9",
            "Creighton 5", "UC Santa Barbara 12", "Virginia 4", "Ohio 13",
            "USC 6", "Wichita St. 11", "Kansas 3", "Eastern Washington 14",
            "Oregon 7", "VCU 10", "Iowa 2", "Grand Canyon 15",
            "Michigan 1", "Mount St. Mary's 16", "LSU 8", "St. Bonaventure 9",
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
        'Appalachian St. 16 / Norfolk St. 16
        'Drake 11 / Wichita St. 11
        'Texas Southern 16 / Mount St. Mary's 16
        'UCLA 11 / Michigan St. 11
    End Function

    'Public Shared Function runBracketPerRound() As String
    '    Dim sb As New StringBuilder
    '    Dim theRankings As KenPomRankings = loadRankings()
    '    Dim theBracket As List(Of String) = getBracketList2020()
    '    Dim theSecondRound As List(Of String) = getBracketList2020SecondRound()
    '    Dim theSweetSixteen As List(Of String) = getBracketList2020SweetSixteen()
    '    Dim theEliteEight As List(Of String) = getBracketList2020EliteEight()
    '    Dim theFinalFour As List(Of String) = getBracketList2020FinalFour()
    '    Dim theChampionship As List(Of String) = getBracketList2020Championship()
    '    Dim theFirstFour As List(Of String) = getFirstFour2020()

    '    'testDataIntegrity(theRankings, theBracket)
    '    'theBracket(1) = HttpContext.Current.Session("0game0")
    '    'theBracket(25) = HttpContext.Current.Session("0game1")
    '    'theBracket(49) = HttpContext.Current.Session("0game2")
    '    'theBracket(57) = HttpContext.Current.Session("0game3")

    '    Dim game = HttpContext.Current.Request.QueryString("game")
    '    Dim round = HttpContext.Current.Request.QueryString("round")
    '    If game <> "" Then
    '        If round = "0" Then
    '            HttpContext.Current.Session(round + "game" + game) = simOneGame(theRankings, theFirstFour, Integer.Parse(game))
    '        Else
    '            HttpContext.Current.Session(round + "game" + game) = simOneGame(theRankings, theChampionship, Integer.Parse(game))
    '        End If
    '    End If

    '    'theBracket(1) = HttpContext.Current.Session("0game0")
    '    'theBracket(25) = HttpContext.Current.Session("0game1")
    '    'theBracket(49) = HttpContext.Current.Session("0game2")
    '    'theBracket(57) = HttpContext.Current.Session("0game3")

    '    sb.Append("<table>")

    '    sb.Append("<td>")
    '    sb.Append(printTeams(theBracket, 1))
    '    'theBracket = simRound(theRankings, theBracket, 64)
    '    sb.Append("</td>")

    '    sb.Append("<td>")
    '    sb.Append(printTeams(theSecondRound, 2))
    '    'theSecondRound = simRound(theRankings, theSecondRound, 32)
    '    sb.Append("</td>")

    '    sb.Append("<td>")
    '    sb.Append(printTeams(theSweetSixteen, 4))
    '    'theBracket = simRound(theRankings, theBracket, 16)
    '    sb.Append("</td>")

    '    sb.Append("<td>")
    '    sb.Append(printTeams(theEliteEight, 8))
    '    'theBracket = simRound(theRankings, theBracket, 8)
    '    sb.Append("</td>")

    '    sb.Append("<td>")
    '    sb.Append(printTeams(theFinalFour, 16))
    '    'theBracket = simRound(theRankings, theBracket, 4)
    '    sb.Append("</td>")

    '    sb.Append("<td>")
    '    sb.Append(printTeams(theChampionship, 32))
    '    'theBracket = simRound(theRankings, theBracket, 2)
    '    sb.Append("</td>")

    '    sb.Append("<td>")
    '    sb.Append(printNewGames(theChampionship, 64, 1))
    '    sb.Append("</td>")

    '    sb.Append("</table>")

    '    Return sb.ToString
    'End Function

    'Public Shared Function simOneGame(theRankings As KenPomRankings, theBracket As List(Of String), game As Integer) As String
    '    Dim winner As String = ""
    '    Dim index As Integer = game * 2
    '    Dim r As New Random

    '    Dim teamA As Team = getKPR(theRankings, theBracket(index))
    '    Dim teamB As Team = getKPR(theRankings, theBracket(index + 1))

    '    Dim oDiff As Double = (teamA.AdjO - teamB.AdjO) * 3
    '    Dim dDiff As Double = (teamB.AdjD - teamA.AdjD) * 4
    '    Dim diff As Double = (oDiff + dDiff) / 2

    '    Dim score = r.Next(1, 100)
    '    If score < diff + 50 Then
    '        winner = theBracket(index)
    '    Else
    '        winner = theBracket(index + 1)
    '    End If

    '    Return winner
    'End Function

    'Public Shared Function printNewGames(theBracket As List(Of String), round As Integer, teams As Integer) As String
    '    Dim sb As New StringBuilder
    '    Dim cntr As Integer = 0
    '    Select Case round
    '        Case 1
    '            For Each team In theBracket
    '                Dim output As String = "<a href=""?game=" + cntr.ToString + "&round=" + round.ToString + """>Play Game!</a>"
    '                If HttpContext.Current.Session(round.ToString + "game" + cntr.ToString) <> "" Then
    '                    output = HttpContext.Current.Session(round.ToString + "game" + cntr.ToString)
    '                End If
    '                If team <> "" Then
    '                    sb.Append(team)
    '                Else
    '                    Dim firstFour As String = ""
    '                    Select Case cntr
    '                        Case 1
    '                            firstFour = "0"
    '                        Case 25
    '                            firstFour = "1"
    '                        Case 49
    '                            firstFour = "2"
    '                        Case 57
    '                            firstFour = "3"
    '                    End Select
    '                    sb.Append("<a href=""?game=" + firstFour + "&round=0"">Play Game!</a>")
    '                End If

    '                For i = 0 To round - 1
    '                    sb.Append("<br/>")
    '                Next
    '                cntr += 1
    '            Next
    '        Case 2
    '            For i = 0 To 31
    '                Dim output As String = "<a href=""?game=" + cntr.ToString + "&round=" + round.ToString + """>Play Game!</a>"
    '                If HttpContext.Current.Session(round.ToString + "game" + cntr.ToString) <> "" Then
    '                    output = HttpContext.Current.Session(round.ToString + "game" + cntr.ToString)
    '                End If
    '                sb.Append(output)
    '                For j = 0 To round - 1
    '                    sb.Append("<br/>")
    '                Next
    '            Next
    '        Case 4, 8, 16, 32, 64
    '            For i = 0 To teams - 1
    '                Dim output As String = "<a href=""?game=" + i.ToString + "&round=" + round.ToString + """>Play Game!</a>"
    '                If HttpContext.Current.Session(round.ToString + "game" + i.ToString) <> "" Then
    '                    output = HttpContext.Current.Session(round.ToString + "game" + i.ToString)
    '                End If
    '                For j = 0 To (round / 4) - 1
    '                    sb.Append("<br/>")
    '                Next
    '                sb.Append(output)
    '                For k = 0 To (round / 1) - Math.Sqrt(round)
    '                    sb.Append("<br/>")
    '                Next
    '            Next
    '    End Select


    '    Return sb.ToString
    'End Function

    Public Shared Function predictBracket() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = loadRankings()
        Dim theBracket As List(Of String) = getBracketList2019()

        sb.Append("<table>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 1))
        theBracket = simRound(theRankings, theBracket, 64)
        sb.Append("</td>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 2))
        theBracket = simRound(theRankings, theBracket, 32)
        sb.Append("</td>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 4))
        theBracket = simRound(theRankings, theBracket, 16)
        sb.Append("</td>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 8))
        theBracket = simRound(theRankings, theBracket, 8)
        sb.Append("</td>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 16))
        theBracket = simRound(theRankings, theBracket, 4)
        sb.Append("</td>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 32))
        theBracket = simRound(theRankings, theBracket, 2)
        sb.Append("</td>")

        sb.Append("<td>")
        sb.Append("<b>" + printTeams(theBracket, 64) + "</b>")
        sb.Append("</td>")

        sb.Append("</table>")

        Return sb.ToString
    End Function

    '    "Duke 1", "North Dakota St. 16", "VCU 8", "UCF 9",
    '"Mississippi St. 5", "Liberty 12", "Virginia Tech 4", "Saint Louis 13",
    '"Maryland 6", "Belmont 11", "LSU 3", "Yale 14",
    '"Louisville 7", "Minnesota 10", "Michigan St. 2", "Bradley 15",
    '"Gonzaga 1", "Fairleigh Dickinson 16", "Syracuse 8", "Baylor 9",
    '"Marquette 5", "Murray St. 12", "Florida St. 4", "Vermont 13",
    '"Buffalo 6", "Arizona St. 11", "Texas Tech 3", "Northern Kentucky 14",
    '"Nevada 7", "Florida 10", "Michigan 2", "Montana 15",
    '"Virginia 1", "Gardner Webb 16", "Mississippi 8", "Oklahoma 9",
    '"Wisconsin 5", "Oregon 12", "Kansas St. 4", "UC Irvine 13",
    '"Villanova 6", "Saint Mary's 11", "Purdue 3", "Old Dominion 14",
    '"Cincinnati 7", "Iowa 10", "Tennessee 2", "Colgate 15",
    '"North Carolina 1", "Iona 16", "Utah St. 8", "Washington 9",
    '"Auburn 5", "New Mexico St. 12", "Kansas 4", "Northeastern 13",
    '"Iowa St. 6", "Ohio St. 11", "Houston 3", "Georgia St. 14",
    '"Wofford 7", "Seton Hall 10", "Kentucky 2", "Abilene Christian 15"

    Public Shared Function getBracketList2020() As List(Of String)
        Return New List(Of String) From {
            "Kansas", "Robert Morris", "Arizona St.", "Florida",
            "Wisconsin", "East Tennessee St.", "Kentucky", "Vermont",
            "Illinois", "Cincinnati", "Duke", "Little Rock",
            "Michigan", "Utah St.", "Creighton", "Belmont",
            "Baylor", "Boston University", "Marquette", "Arizona",
            "Ohio St.", "Yale", "Butler", "Bradley",
            "Virginia", "Richmond", "Maryland", "UC Irvine",
            "Providence", "LSU", "Florida St.", "Northern Kentucky",
            "Dayton", "Winthrop", "Saint Mary's", "Oklahoma",
            "Auburn", "Akron", "Louisville", "Liberty",
            "West Virginia", "Rutgers", "Michigan St.", "North Texas",
            "USC", "Indiana", "Villanova", "North Dakota St.",
            "Gonzaga", "Siena", "Houston", "Colorado",
            "Penn St.", "Stephen F. Austin", "Oregon", "New Mexico St.",
            "BYU", "Stanford", "Seton Hall", "Hofstra",
            "Iowa", "Xavier", "San Diego St.", "Eastern Washington"
            }
    End Function

    Public Shared Function getFirstFour2020() As List(Of String)
        Return New List(Of String) From {
            "North Carolina Central", "Robert Morris", "Richmond", "Wichita St.",
            "Prairie View A&M", "Siena", "Stanford", "UCLA"
            }
    End Function

    Public Shared Function getBracketList2020SecondRound() As List(Of String)
        Return New List(Of String) From {
            "Kansas", "Arizona St.",
            "Wisconsin", "Vermont",
            "Cincinnati", "Duke",
            "Utah St.", "Creighton",
            "Baylor", "Marquette",
            "Yale", "Butler",
            "Virginia", "Maryland",
            "LSU", "Florida St.",
            "Dayton", "Oklahoma",
            "Akron", "Liberty",
            "Rutgers", "Michigan St.",
            "Indiana", "Villanova",
            "Gonzaga", "Houston",
            "Penn St.", "Oregon",
            "Stanford", "Seton Hall",
            "Xavier", "San Diego St."
            }
    End Function

    Public Shared Function getBracketList2020SweetSixteen() As List(Of String)
        Return New List(Of String) From {
            "Arizona St.",
            "Wisconsin",
            "Duke",
            "Utah St.",
            "Marquette",
            "Yale",
            "Virginia",
            "Florida St.",
            "Oklahoma",
            "Liberty",
            "Rutgers",
            "Indiana",
            "Gonzaga",
            "Oregon",
            "Stanford",
            "San Diego St."
            }
    End Function

    Public Shared Function getBracketList2020EliteEight() As List(Of String)
        Return New List(Of String) From {
            "Wisconsin",
            "Duke",
            "Marquette",
            "Virginia",
            "Oklahoma",
            "Rutgers",
            "Gonzaga",
            "Stanford"
            }
    End Function

    Public Shared Function getBracketList2020FinalFour() As List(Of String)
        Return New List(Of String) From {
            "Wisconsin",
            "Marquette",
            "Rutgers",
            "Gonzaga"
            }
    End Function

    Public Shared Function getBracketList2020Championship() As List(Of String)
        Return New List(Of String) From {
            "Wisconsin",
            "Gonzaga"
            }
    End Function

    Public Shared Function getBracketList2019() As List(Of String)
        Return New List(Of String) From {
            "Duke 1", "North Dakota St. 16", "VCU 8", "UCF 9",
            "Mississippi St. 5", "Liberty 12", "Virginia Tech 4", "Saint Louis 13",
            "Maryland 6", "Belmont 11", "LSU 3", "Yale 14",
            "Louisville 7", "Minnesota 10", "Michigan St. 2", "Bradley 15",
            "Gonzaga 1", "Fairleigh Dickinson 16", "Syracuse 8", "Baylor 9",
            "Marquette 5", "Murray St. 12", "Florida St. 4", "Vermont 13",
            "Buffalo 6", "Arizona St. 11", "Texas Tech 3", "Northern Kentucky 14",
            "Nevada 7", "Florida 10", "Michigan 2", "Montana 15",
            "Virginia 1", "Gardner Webb 16", "Mississippi 8", "Oklahoma 9",
            "Wisconsin 5", "Oregon 12", "Kansas St. 4", "UC Irvine 13",
            "Villanova 6", "Saint Mary's 11", "Purdue 3", "Old Dominion 14",
            "Cincinnati 7", "Iowa 10", "Tennessee 2", "Colgate 15",
            "North Carolina 1", "Iona 16", "Utah St. 8", "Washington 9",
            "Auburn 5", "New Mexico St. 12", "Kansas 4", "Northeastern 13",
            "Iowa St. 6", "Ohio St. 11", "Houston 3", "Georgia St. 14",
            "Wofford 7", "Seton Hall 10", "Kentucky 2", "Abilene Christian 15"
        }
    End Function

    Public Shared Function testDataIntegrity(theRankings As KenPomRankings, theBracket As List(Of String)) As Boolean
        Dim pass As Boolean = True

        For Each team In theBracket
            Dim kpTeam As Team = getKPR(theRankings, team)
            If kpTeam.Team = "" Then
                pass = False
            End If
        Next

        Return pass
    End Function

    Public Shared Function loadRankings() As KenPomRankings
        Return New JavaScriptSerializer().Deserialize(Of KenPomRankings)(System.IO.File.ReadAllText("C:\Users\ekerns\Documents\GitHub\BracketProjector\BracketProjector\App_Data\rankings21.json"))
    End Function
End Class
