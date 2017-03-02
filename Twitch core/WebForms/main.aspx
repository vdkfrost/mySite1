<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Twitch_core.WebForms.main" Async="true" %>

<%@ Register Src="~/UserControls/page-footer.ascx" TagName="footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/page-header.ascx" TagName="header" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>STREAMER SPACE - Личный помощник каждого стримера</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="~/Content/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <uc:header ID="page_top" runat="server" />

        <div class="full-width usual-block" style="background-color: white;">
            <h1 class="main-part-header">О нас</h1>
            <span class="main-part-text"><font style="font-family: 'PT Sans Bold'">KIBCODE</font>&nbsp- сравнительно небольшая и совсем молодая команда разработчиков, занимающаяся написанием как сайтов, так и клиентских приложений. Присулшиваясь ко мнениям пользователей 
                мы стремимся разработать максимально комфортный интерфейс наряду с максмально полезным инструментарием. Streamerspace.ru - наша первая большая и серьезная разработка, которой мы будем уделять довольно
                большое внимание.
            </span>

            <h1 class="main-part-header">О streamer space</h1>
            <span class="main-part-text">Исследовав рынок разработок связанных с программным обеспечением для стримеров, наша команда пришла к выводу, что еще не существует многофункционального сервиса, 
                способного предоставить все необходимые стримеру услуги, без использования при этом 3 сайтов и более. Следовательно, это довольно некомфортно - отслеживать статусы многих приложений, которые ты используешь. Именно
                по этой причине и создан этот сервис. 
            </span>

            <h1 class="main-part-header">Почему именно наш сервис?</h1>
            <span class="main-part-text">Особенностью STREAMER SPACE является вариативный процент коммисии на вывод средств. Что это означает? Процент на вывод денежных средств колеблется от 2% до 5%. Дело в том, что целью данного
                проекта является не заработок денежных средств, а лишь предоставление услуг пользователям. Поэтому проект удерживает процент от общей суммы средств, имеющихся на его счету, но при этом не превышающий 5%. 
                При достижении суммы денег необходимых для обеспечения проекта, STREAMER SPACE снижает процентную ставку, но до значения не менее
                2%. Таким образом, выгоднее всего будет снимать денежные средства к концу месяца. Как на работе, не правда ли?
                <span class="main-part-hint">Допустим, что для обеспечения работоспособности проекта необходимо 5.000 рублей. Соответственно, чтобы покрыть расходы,
                    сайту необходимо принять 100.000 рублей от людей, желающих пожертвовать деньги стримеру. При этом, коммисия на вывод денежных средств будет равна 5%. Если же проект принимает большее
                    количество денежных средств, то процент перерасчитывается по формуле:
                    <font style="display: table; background-color: rgba(215, 115, 0, 0.5); padding: 10px 20px; margin: 10px 0px; font: 13px 'Source Sans Pro'; border-width: 0px 0px 0px 5px;">% = (5.000 руб. * 100%) / общее количество средств на текущий момент</font>
                    но не менее 2%. Всё очень просто!
                    Текущий процент коммисии можно отслеживать у себя в личном кабинете.
                </span>
                Так же, нашему сервису присущ широкий функционал, объединяющий в себе работу таких сайтов как streamlabs.com, donationalerts.ru и twitch.moobot.tv и расширяющий их. Приятная для глаз, интуитивно
                понятная и удобная панель управления поможет адаптироваться для более комфортной работы.
            </span>

            <asp:Panel ID="additionalInfo" runat="server" Visible="false">
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
                            <div class="body" style="background-image: url(/images/svg/donate.svg)">
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
            </asp:Panel>
        </div>

        <uc:footer runat="server"></uc:footer>
    </form>
</body>
</html>
