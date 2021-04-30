<%@ Page Title="Sim" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="BracketProjector._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <p class="lead"><% =MarchAlgorithm.runSecondChance() %></p>
    </div>

</asp:Content>
