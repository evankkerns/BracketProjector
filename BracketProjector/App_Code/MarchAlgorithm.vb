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
        Dim theRankings As KenPomRankings = loadRankings() 'Update file location within this function to correct year of data
        Dim theBracket As List(Of String) = getBracketList2023()

        'Dim tester = testDataIntegrity(theRankings, theBracket)

        Dim thePrintList(12) As String

        thePrintList = printList(theBracket, 1, thePrintList)
        theBracket = simRound(theRankings, theBracket, 64)

        'Threading.Thread.Sleep(1000)

        thePrintList = printList(theBracket, 2, thePrintList)
        theBracket = simRound(theRankings, theBracket, 32)

        'Threading.Thread.Sleep(1000)

        thePrintList = printList(theBracket, 3, thePrintList)
        theBracket = simRound(theRankings, theBracket, 16)

        'Threading.Thread.Sleep(1000)

        thePrintList = printList(theBracket, 4, thePrintList)
        theBracket = simRound(theRankings, theBracket, 8)

        'Threading.Thread.Sleep(1000)

        thePrintList = printList(theBracket, 5, thePrintList)
        theBracket = simRound(theRankings, theBracket, 4)

        'Threading.Thread.Sleep(1000)

        thePrintList = printList(theBracket, 6, thePrintList)
        theBracket = simRound(theRankings, theBracket, 2)

        'Threading.Thread.Sleep(1000)

        thePrintList = printList(theBracket, 7, thePrintList)

        sb.Append("<table>")
        sb.Append("<td style=""width: 259px; vertical-align:top"">-")
        sb.Append("</td>")
        For Each col In thePrintList
            sb.Append("<td style=""width: 259px; vertical-align:top"">")
            sb.Append(col)
            sb.Append("</td>")
        Next
        sb.Append("<td style=""width: 259px; vertical-align:top"">-")
        sb.Append("</td>")
        sb.Append("</table>")

        Return sb.ToString
    End Function

    Public Shared Function runBracketOld() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = loadRankings()
        Dim theBracket As List(Of String) = getBracketList2021()
        Dim thePrintList(12) As String

        sb.Append("<table>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 1))
        thePrintList = printList(theBracket, 1, thePrintList)
        theBracket = simRound(theRankings, theBracket, 64)
        sb.Append("</td>")

        Threading.Thread.Sleep(1000)

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 2))
        thePrintList = printList(theBracket, 2, thePrintList)
        theBracket = simRound(theRankings, theBracket, 32)
        sb.Append("</td>")

        Threading.Thread.Sleep(1000)

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 4))
        thePrintList = printList(theBracket, 3, thePrintList)
        theBracket = simRound(theRankings, theBracket, 16)
        sb.Append("</td>")

        Threading.Thread.Sleep(1000)

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 8))
        thePrintList = printList(theBracket, 4, thePrintList)
        theBracket = simRound(theRankings, theBracket, 8)
        sb.Append("</td>")

        Threading.Thread.Sleep(1000)

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 16))
        thePrintList = printList(theBracket, 5, thePrintList)
        theBracket = simRound(theRankings, theBracket, 4)
        sb.Append("</td>")

        Threading.Thread.Sleep(1000)

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 32))
        thePrintList = printList(theBracket, 6, thePrintList)
        theBracket = simRound(theRankings, theBracket, 2)
        sb.Append("</td>")

        Threading.Thread.Sleep(1000)

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 64))
        thePrintList = printList(theBracket, 7, thePrintList)
        sb.Append("</td>")

        sb.Append("</table>")

        Return sb.ToString
    End Function

    Public Shared Function runSecondChance() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = loadRankings()
        Dim theBracket As List(Of String) = getSweetSixteen2021()

        sb.Append("<table>")

        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 1))
        theBracket = simRound(theRankings, theBracket, 16)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 2))
        theBracket = simRound(theRankings, theBracket, 8)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 4))
        theBracket = simRound(theRankings, theBracket, 4)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 8))
        theBracket = simRound(theRankings, theBracket, 2)
        sb.Append("</td>")
        Threading.Thread.Sleep(1000)
        sb.Append("<td>")
        sb.Append(printTeams(theBracket, 16))
        sb.Append("</td>")

        sb.Append("</table>")

        Return sb.ToString
    End Function

    Public Shared Function simRound(theRankings As KenPomRankings, theBracket As List(Of String), teamsInRound As Integer) As List(Of String)
        Dim newBracket As New List(Of String)
        Dim r As New Random
        Dim multi As Double = 2
        Select Case teamsInRound
            Case 64
                multi = 1
            Case 32
                multi = 1.5
            Case 16
                multi = 2
            Case 8
                multi = 2
            Case Else
                multi = 2
        End Select


        For i = 0 To teamsInRound - 2 Step 2
            Dim teamA As Team = getKPR(theRankings, theBracket(i))
            Dim teamB As Team = getKPR(theRankings, theBracket(i + 1))

            Dim off As Double = r.Next(250, 500)
            Dim def As Double = r.Next(250, 500)
            off = off / 100
            def = def / 100

            Dim oDiff As Double = (teamA.AdjO - teamB.AdjO) * off * multi
            Dim dDiff As Double = (teamB.AdjD - teamA.AdjD) * def * multi
            Dim diff As Double = (oDiff + dDiff) / 2

            'Threading.Thread.Sleep(100)
            Dim score = r.Next(1, 10001)
            score = score / 100
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

    Public Shared Function printList(theBracket As List(Of String), round As Integer, theList() As String) As String()
        Dim sbLeft As New StringBuilder
        Dim sbRight As New StringBuilder

        If round <= 1 Then
            For leftSide = 0 To (theBracket.Count / 2) - 1
                sbLeft.Append(theBracket(leftSide))
                For i = 0 To (2 ^ (round - 1)) - 1
                    sbLeft.Append("<br/>")
                Next
            Next

            For rightSide = theBracket.Count / 2 To theBracket.Count - 1
                sbRight.Append(theBracket(rightSide))
                For i = 0 To (2 ^ (round - 1)) - 1
                    sbRight.Append("<br/>")
                Next
            Next
        Else
            For leftSide = 0 To (theBracket.Count / 2) - 1
                For i = 0 To (((2 ^ (round - 1)) - 1) / 2)
                    sbLeft.Append("<br/>")
                Next
                sbLeft.Append(theBracket(leftSide))
                For i = 0 To (((2 ^ (round - 1)) - 1) / 2)
                    sbLeft.Append("<br/>")
                Next
            Next

            For rightSide = theBracket.Count / 2 To theBracket.Count - 1
                For i = 0 To (((2 ^ (round - 1)) - 1) / 2)
                    sbRight.Append("<br/>")
                Next
                sbRight.Append(theBracket(rightSide))
                For i = 0 To (((2 ^ (round - 1)) - 1) / 2)
                    sbRight.Append("<br/>")
                Next
            Next
        End If

        If round = 7 Then
            For i = 0 To (((2 ^ (round - 1)) - 1) / 6)
                sbRight.Append("<br/>")
            Next
            sbRight.Append(theBracket(0))
        End If

        theList(round - 1) = sbLeft.ToString
        theList(13 - round) = sbRight.ToString

        Return theList
    End Function

    Public Shared Function getKPR(theRankings As KenPomRankings, teamName As String) As Team
        For Each team In theRankings.teams
            If teamName = team.Team Then
                Return team
            End If
        Next

        Return New Team
    End Function

    Public Shared Function getBracketList2023() As List(Of String)
        Return New List(Of String) From {
            "Alabama 1", "Texas A&M Corpus Chris 16", "Maryland 8", "West Virginia 9",
            "San Diego St. 5", "Charleston 12", "Virginia 4", "Furman 13",
            "Creighton 6", "N.C. State 11", "Baylor 3", "UC Santa Barbara 14",
            "Missouri 7", "Utah St. 10", "Arizona 2", "Princeton 15",
            "Purdue 1", "Fairleigh Dickinson 16", "Memphis 8", "Florida Atlantic 9",
            "Duke 5", "Oral Roberts 12", "Tennessee 4", "Louisiana 13",
            "Kentucky 6", "Providence 11", "Kansas St. 3", "Montana St. 14",
            "Michigan St. 7", "USC 10", "Marquette 2", "Vermont 15",
            "Houston 1", "Northern Kentucky 16", "Iowa 8", "Auburn 9",
            "Miami FL 5", "Drake 12", "Indiana 4", "Kent St. 13",
            "Iowa St. 6", "Pittsburgh 11", "Xavier 3", "Kennesaw St. 14",
            "Texas A&M 7", "Penn St. 10", "Texas 2", "Colgate 15",
            "Kansas 1", "Howard 16", "Arkansas 8", "Illinois 9",
            "Saint Mary's 5", "VCU 12", "Connecticut 4", "Iona 13",
            "TCU 6", "Arizona St. 11", "Gonzaga 3", "Grand Canyon 14",
            "Northwestern 7", "Boise St. 10", "UCLA 2", "UNC Asheville 15"
        }
        'Texas A&M Corpus Chris 16 / Southeast Missouri St. 16
        'Texas Southern 16 / Fairleigh Dickinson 16
        'Mississippi St. 11 / Pittsburgh 11
        'Arizona St. 11 / Nevada 11
    End Function

    Public Shared Function getBracketList2022() As List(Of String)
        Return New List(Of String) From {
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
        'Rutgers 11 / Notre Dame 11
        'Indiana 12 / Wyoming 12
        'Wright St. 16 / Bryant 16
        'Texas Southern 16 / Texas A&M Corpus Chris 16
    End Function

    Public Shared Function getBracketList2021() As List(Of String)
        Return New List(Of String) From {
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
        'Appalachian St. 16 / Norfolk St. 16
        'Drake 11 / Wichita St. 11
        'Texas Southern 16 / Mount St. Mary's 16
        'UCLA 11 / Michigan St. 11
    End Function

    Public Shared Function getSweetSixteen2021() As List(Of String)
        Return New List(Of String) From {
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
        Return New JavaScriptSerializer().Deserialize(Of KenPomRankings)(System.IO.File.ReadAllText("C:\Users\ekerns\Documents\GitHub\BracketProjector\BracketProjector\App_Data\rankings23.json"))
    End Function
End Class
