<%@ Page Language="C#" Title="Twitch-assist - лучший инструментарий стримера" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Twitch_core.WebForms.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Content/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="full-width main-top-block" <% Response.Write("style=\" background: fixed url(../images/backgrounds/back" + new Random().Next(1, 11).ToString() + ".jpg)\""); %>>
            <div style="width: 100%; position: absolute; background: linear-gradient(to top, rgb(45, 46, 51), transparent); top: 0px; opacity: 0.9; height: 400px;"></div>
            <span class="header">TWITCH-ASSIST</span>
            <span class="desc">Главный помощник стримера</span>
            <%
                string output = "<a href=\"{0}\" class=\"action {1}\">{2}</a>";
                if (Session["user-id"] == null)
                    output = string.Format(output, "/join", "join", "Присоединиться");
                else
                    output = string.Format(output, "/" + Session["user-name"] + "/panel", "go-to-panel", "Перейти к панели управления");
                Response.Write(output); %>
        </div>

        <div class="full-width usual-block" style="background-color: white; padding-top: 60px">
            <span class="main-part-header">О нас</span>
            <span class="main-part-text">KIBCODE - сравнительно небольшая и совсем молодая команда разработчиков, занимающаяся написанием как сайтов, так и клиентских приложений. Присулшиваясь ко мнениям пользователей 
                мы стремимся разработать максимально комфортный интерфейс наряду с максмально полезным инструментарием. Twitch-assist.ru - наша первая большая и серьезная разработка, которой мы будем уделять довольно
                большое внимание.
            </span>

            <span class="main-part-header">Почему twitch-assist?</span>
            <span class="main-part-text" style="margin-bottom: 5px">Исследовав рынок разработок связанных с программным обеспечением для стримеров, наша команда пришла к выводу, что еще не существует многофункционального сервиса, 
                способного предоставить все необходимые стримеру услуги, без использования при этом 3 сайтов и более. Следовательно, это довольно некомфортно - отслеживать статусы многих сервисов, которые ты используешь. Именно
                по этой причине и создан этот сервис. 
                <br />
                <br />
                Так же, особенностью twitch-assist является вариативный процент коммисии на вывод средств. Что это означает? Процент на вывод денежных средств колеблется от 1% до 5%. Дело в том, что целью данного
                проекта является не заработок денежных средств, а лишь предоставление услуг пользователям. Поэтому проект удерживает процент от общей суммы средств, имеющихся на его счету, но при этом не превышающий 5%. 
                При достижении суммы денег необходимых для обеспечения проекта (общая сумма на счету проекта * 0.05 = сумма обеспечения проекта), twitch-assist снижает процентную ставку, но до минимального значения
                в 1%. Таким образом, выгоднее всего будет снимать денежные средства к концу месяца. Как на работе, не правда ли?
                <br />
                <br />
                <font style="font-family: 'PT Sans Bold'">Подробнее о причинах выбрать именно наш сервис:</font>
            </span>
            <ul style="margin-bottom: 50px">
                <li>Широкий функционал, который есть во всех других сервисах, но с дополнениями.</li>
                <li>Приятная на глаз, интуитивно понятная и удобная панель управления.</li>
                <li>Вариативный процент коммисии на вывод средств.</li>
                <li style="color: darkorange">Бот, клиентское приложение и уведомления о пожертвованиях - всё в одном!</li>
            </ul>

            <div class="main-current-info">
                <span class="text">Сейчас мы помогаем</span>
                <asp:Label ID="currentUsersCount" runat="server" CssClass="info" Style="background-image: url(../images/svg/twitch-icon.svg); background-color: rgb(100, 65, 165)">0</asp:Label>
                <span class="text">стримерам</span>
            </div>

            <div class="main-parts-usings">
                <div class="part">
                    <div class="circle" style="border-color: #f26d7d">
                        <div class="body" style="background-image: url(../images/svg/donate.svg)">
                            <asp:Label ID="donationsUsingsCount" runat="server" CssClass="value">123</asp:Label>
                        </div>
                    </div>
                    <span class="description" style="background-color: #f26d7d">Используют Уведомления о пожертвованиях</span>
                </div>

                <div class="part" style="padding-top: 80px">
                    <div class="circle" style="border-color: #2bc6ca">
                        <div class="body" style="background-image: url(../images/svg/robot.svg)">
                            <asp:Label ID="botUsingsCount" runat="server" CssClass="value">251</asp:Label>
                        </div>
                    </div>
                    <span class="description" style="background-color: #2bc6ca">Используют бота</span>
                </div>

                <div class="part">
                    <div class="circle" style="border-color: #ffae22">
                        <div class="body" style="background-image: url(../images/svg/pc.svg)">
                            <asp:Label ID="Label2" runat="server" CssClass="value">98</asp:Label>
                        </div>
                    </div>
                    <span class="description" style="background-color: #ffae22">Используют клиентское приложение</span>
                </div>
            </div>

            <div class="main-streamers usual-block">
                <span class="text">Стримеры, использующие наши системы</span>
                <asp:Table ID="streamers" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <a href="/users/ruhub">
                                <span class="nickname">RUHUB dotapit</span>
                                <img src="https://static-cdn.jtvnw.net/previews-ttv/live_user_ruhub_dotapit-320x180.jpg" />
                                <div class="hover"></div>
                                <span class="viewers">382</span>
                            </a>
                        </asp:TableCell>
                        <asp:TableCell>
                            <a href="/users/ruhub">
                                <span class="nickname">RUHUB dotapit</span>
                                <img src="https://static-cdn.jtvnw.net/previews-ttv/live_user_ruhub_dotapit-320x180.jpg" />
                                <div class="hover"></div>
                                <span class="viewers">382</span>
                            </a>
                        </asp:TableCell>
                        <asp:TableCell>
                            <a href="/users/ruhub">
                                <span class="nickname">RUHUB dotapit</span>
                                <img src="https://static-cdn.jtvnw.net/previews-ttv/live_user_ruhub_dotapit-320x180.jpg" />
                                <div class="hover"></div>
                                <span class="viewers">382</span>
                            </a>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <a href="/users/ruhub">
                                <span class="nickname">RUHUB dotapit</span>
                                <img src="https://static-cdn.jtvnw.net/previews-ttv/live_user_ruhub_dotapit-320x180.jpg" />
                                <div class="hover"></div>
                                <span class="viewers">382</span>
                            </a>
                        </asp:TableCell>
                        <asp:TableCell>
                            <a href="/users/ruhub">
                                <span class="nickname">RUHUB dotapit</span>
                                <img src="https://static-cdn.jtvnw.net/previews-ttv/live_user_ruhub_dotapit-320x180.jpg" />
                                <div class="hover"></div>
                                <span class="viewers">382</span>
                            </a>
                        </asp:TableCell>
                        <asp:TableCell>
                            <a href="/users/ruhub">
                                <span class="nickname">RUHUB dotapit</span>
                                <img src="https://static-cdn.jtvnw.net/previews-ttv/live_user_ruhub_dotapit-320x180.jpg" />
                                <div class="hover"></div>
                                <span class="viewers">382</span>
                            </a>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>

        <div class="full-width main-footer">
            <span>Разработано командой <a href="www.kibcode.ru">KIBCODE</a> Twitch-assist.ru - главный помощник стримера</span>
        </div>
    </form>
</body>
</html>
