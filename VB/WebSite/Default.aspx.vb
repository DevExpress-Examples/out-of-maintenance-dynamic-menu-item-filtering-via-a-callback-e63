Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports DevExpress.Web.ASPxMenu

Partial Public Class ASPxperience_Menu_DynamicMenuFilteringOnCallback_DynamicMenuFilteringOnCallback
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		PopulateMenuWithGames()
	End Sub
	Protected Sub OnCallback(ByVal source As Object, ByVal e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase)
		PopulateMenuWithGames()
	End Sub

	Private Sub PopulateMenuWithGames()
		Dim doc As XmlDocument = New XmlDocument()
		doc.Load(MapPath("~/App_Data/Games.xml"))
		menuGames.Items.Clear()
		PopulateMenuWithGamesCore(menuGames, doc, tbFilterBox_GameName.Text)
		lblNoGamesFound.Visible = menuGames.Items.Count = 0
	End Sub
	Private Sub PopulateMenuWithGamesCore(ByVal menu As ASPxMenu, ByVal doc As XmlDocument, ByVal filteringString As String)
		Dim genres As XmlNodeList = doc.SelectNodes("/games/genre")
		For Each genre As XmlNode In genres
			Dim genreGames As XmlNodeList = GetGenreGames(genre, filteringString)
			If genreGames.Count > 0 Then
				CreateMenuItems(menu, genre, genreGames)
			End If
		Next genre
	End Sub
	Private Function GetGenreGames(ByVal genreNode As XmlNode, ByVal filteringString As String) As XmlNodeList
		If String.IsNullOrEmpty(filteringString) Then
			Return genreNode.ChildNodes
		Else
			Dim xPath As String = String.Format("game[contains(translate(@name, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{0}')]", filteringString.ToLowerInvariant())
			Return genreNode.SelectNodes(xPath)
		End If
	End Function
	Private Sub CreateMenuItems(ByVal menu As ASPxMenu, ByVal genre As XmlNode, ByVal games As XmlNodeList)
		Dim genreItemText As String = String.Format("{0} ({1})", genre.Attributes("name").Value, games.Count)
		Dim genreItem As MenuItem = New MenuItem(genreItemText)
		menu.Items.Add(genreItem)
		For Each game As XmlNode In games
			Dim gameItemText As String = String.Format("{0} ({1})", game.Attributes("name").Value, game.Attributes("year").Value)
			Dim gameItem As MenuItem = New MenuItem(gameItemText)
			genreItem.Items.Add(gameItem)
		Next game
	End Sub
End Class
