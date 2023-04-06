<%@ Page Title="Sims" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sims.aspx.vb" Inherits="BracketProjector.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <p class="lead"><% Dim ttf As New TestTheFormula %>
           <% =ttf.simTheFirstRound() %></p>
    </div>

</asp:Content>
