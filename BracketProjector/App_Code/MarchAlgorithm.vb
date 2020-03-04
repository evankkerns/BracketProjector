Imports System.Web.Script.Serialization

Public Class MarchAlgorithm
    Public Sub New()
        predictBracket()
    End Sub

    Public Shared Function predictBracket() As String
        Dim sb As New StringBuilder
        Dim theRankings As KenPomRankings = loadRankings()
        Dim theBracket As List(Of String) = getBracketList2019()

        Return sb.ToString
    End Function

    Public Shared Function firstRound(ByVal theRankings As KenPomRankings, ByVal theBracket As List(Of String)) As List(Of String)
    End Function

    Public Shared Function getBracketList2019() As List(Of String)
        Return New List(Of String) From {
            "Duke", "North Dakota St.", "VCU", "UCF",
            "Mississippi St.", "Liberty", "Virgina Tech", "Saint Louis",
            "Maryland", "Belmont", "LSU", "Yale",
            "Louisville", "Minnesota", "Michigan St.", "Bradley",
            "Gonzaga",
            "Fairleigh Dickinson",
            "Syracuse",
            "Baylor",
            "Marquette",
            "Murray St.",
            "Florida St.",
            "Vermont",
            "Buffalo",
            "Arizona St.",
            "Texas Tech",
            "Northern Ky.",
            "Nevada",
            "Florida",
            "Michigan",
            "Montana",
            "Virgina",
            "Gardner-Webb",
            "Ole Miss",
            "Oklahoma",
            "Wisconsin",
            "Oregon",
            "Kansas St.",
            "UC Irvine",
            "Villanova",
            "Saint Mary's",
            "Purdue",
            "Old Dominion",
            "Cincinnati",
            "Iowa",
            "Tennessee",
            "Colgate",
            "North Carolina",
            "Iona",
            "Utah St.",
            "Washington",
            "Auburn",
            "New Mexico St.",
            "Kansas",
            "Northeastern",
            "Iowa St.",
            "Ohio St.",
            "Houston",
            "Georgia St.",
            "Wofford",
            "Seton Hall",
            "Kentucky",
            "Abilene Christian"
        }
    End Function

    Public Shared Function loadRankings() As KenPomRankings
        Return New JavaScriptSerializer().Deserialize(Of KenPomRankings)(System.IO.File.ReadAllText("C:\Users\Monster\source\repos\BracketProjector\BracketProjector\App_Data\rankings.json"))
    End Function
End Class
