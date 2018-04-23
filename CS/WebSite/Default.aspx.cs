using System;
using System.Web;
using System.Xml;
using DevExpress.Web.ASPxMenu;

public partial class ASPxperience_Menu_DynamicMenuFilteringOnCallback_DynamicMenuFilteringOnCallback : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        PopulateMenuWithGames();
    }
    protected void OnCallback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e) {
        PopulateMenuWithGames();
    }

    private void PopulateMenuWithGames() {
        XmlDocument doc = new XmlDocument();
        doc.Load(MapPath("~/App_Data/Games.xml"));
        menuGames.Items.Clear();
        PopulateMenuWithGamesCore(menuGames, doc, tbFilterBox_GameName.Text);
        lblNoGamesFound.Visible = menuGames.Items.Count == 0;
    }
    private void PopulateMenuWithGamesCore(ASPxMenu menu, XmlDocument doc, string filteringString) {
        XmlNodeList genres = doc.SelectNodes("/games/genre");
        foreach(XmlNode genre in genres) {
            XmlNodeList genreGames = GetGenreGames(genre, filteringString);
            if(genreGames.Count > 0)
                CreateMenuItems(menu, genre, genreGames);
        }
    }
    private XmlNodeList GetGenreGames(XmlNode genreNode, string filteringString) {
        if(string.IsNullOrEmpty(filteringString))
            return genreNode.ChildNodes;
        else {
            string xPath = string.Format("game[contains(translate(@name, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{0}')]",
                filteringString.ToLowerInvariant());
            return genreNode.SelectNodes(xPath);
        }
    }
    private void CreateMenuItems(ASPxMenu menu, XmlNode genre, XmlNodeList games) {
        string genreItemText = string.Format("{0} ({1})", genre.Attributes["name"].Value, games.Count);
        MenuItem genreItem = new MenuItem(genreItemText);
        menu.Items.Add(genreItem);
        foreach(XmlNode game in games) {
            string gameItemText = string.Format("{0} ({1})", game.Attributes["name"].Value, game.Attributes["year"].Value);
            MenuItem gameItem = new MenuItem(gameItemText);
            genreItem.Items.Add(gameItem);
        }
    }
}
