<%@ Page Title="Sims" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sims.aspx.vb" Inherits="BracketProjector.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <p class="lead"><% Dim ttf As New TestTheFormula %>
           <% =ttf.testTheFormula() %></p>
    </div>

</asp:Content>
