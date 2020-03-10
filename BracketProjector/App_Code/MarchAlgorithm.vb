Imports System.Web.Script.Serialization

Public Class MarchAlgorithm
    Dim timesWrong(16) As Integer

    Public Sub New()
        predictBracket()
    End Sub

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

    Public Shared Function simRound(theRankings As KenPomRankings, theBracket As List(Of String), teamsInRound As Integer) As List(Of String)
        Dim newBracket As New List(Of String)
        Dim r As New Random

        For i = 0 To teamsInRound - 2 Step 2
            Dim teamA As Team = getKPR(theRankings, theBracket(i))
            Dim teamB As Team = getKPR(theRankings, theBracket(i + 1))

            Dim oDiff As Double = (teamA.AdjO - teamB.AdjO) * 3
            Dim dDiff As Double = (teamB.AdjD - teamA.AdjD) * 4
            Dim diff As Double = (oDiff + dDiff) / 2

            Dim score = r.Next(1, 100)
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
        Return New JavaScriptSerializer().Deserialize(Of KenPomRankings)(System.IO.File.ReadAllText("C:\Users\Monster\source\repos\BracketProjector\BracketProjector\App_Data\rankings.json"))
    End Function
End Class
