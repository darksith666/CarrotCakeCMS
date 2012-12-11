<%@ Page Language="C#" AutoEventWireup="true" Inherits="Carrotware.CMS.UI.Base.GenericPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
	<carrot:jquery runat="server" ID="jquery1" JQVersion="1.6" />
	<carrot:jqueryui runat="server" ID="jqueryui1" />
	<title>Citrus Island</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<asp:PlaceHolder ID="myCSS" runat="server">
		<link href="<%=pageContents.TemplateFolderPath %>style.css" rel="stylesheet" type="text/css" media="screen" />
	</asp:PlaceHolder>
	<carrot:RSSFeed runat="server" ID="RSSFeed1" />
</head>
<body>
	<form id="form1" runat="server">
	<div id="wrap">
		<div id="header">
			<%--<form method="post" class="search" action="http://www.free-css.com/">
			<p>
				<input name="search_query" class="textbox" type="text" />
				<input name="search" class="button" value="Search" type="submit" />
			</p>
			</form>--%>
			<p class="search">
			</p>
			<asp:PlaceHolder ID="myHeading" runat="server">
				<h1 id="logo">
					<a href="/">
						<%=theSite.SiteName%></a>
				</h1>
				<h2 id="slogan">
					<%=theSite.SiteTagline%>
				</h2>
			</asp:PlaceHolder>
		</div>
		<div id="menu">
			<carrot:TwoLevelNavigation MenuWidth="960px" MenuHeight="10px" FontSize="11px" ForeColor="#FFFFFF" BackColor="#F4845A" runat="server" ID="TwoLevelNavigation1" />
		</div>
		<div id="sidebar">
			<carrot:SiteMetaWordList ID="SiteMetaWordList1" runat="server" ContentType="DateMonth" MetaDataTitle="Dates" TakeTop="14" />
			<carrot:SiteMetaWordList ID="SiteMetaWordList2" runat="server" ContentType="Category" MetaDataTitle="Categories" ShowUseCount="true" />
			<carrot:SiteMetaWordList ID="SiteMetaWordList3" runat="server" ContentType="Tag" MetaDataTitle="Tags" ShowUseCount="true" />
			<carrot:WidgetContainer ID="phLeftTop" runat="server">
			</carrot:WidgetContainer>
			<carrot:ContentContainer EnableViewState="false" ID="BodyLeft" runat="server">
			</carrot:ContentContainer>
			<carrot:WidgetContainer ID="phLeftBottom" runat="server">
			</carrot:WidgetContainer>
		</div>
		<div id="main">
			<h1>
				<asp:Literal ID="litPageHeading" runat="server" /></h1>
			<carrot:WidgetContainer ID="phCenterTop" runat="server">
			</carrot:WidgetContainer>
			<carrot:ContentContainer EnableViewState="false" ID="BodyCenter" runat="server">
			</carrot:ContentContainer>
			<div style="clear: both;">
			</div>
			<carrot:PagedDataSummary ID="PagedDataSummary1" runat="server" ContentType="Blog" PageSize="5" CSSSelectedPage="selected">
				<SummaryHeaderTemplate>
					<div>
				</SummaryHeaderTemplate>
				<SummaryTemplate>
					<div>
						<p>
							<b class="green" style="font-size: 110%;">
								<carrot:NavLinkForTemplate CssClassNormal="green" ID="NavLinkForTemplate1" runat="server" UseDefaultText="true" />
								&nbsp;|&nbsp;
								<carrot:ListItemNavText runat="server" ID="ListItemNavText1" DataField="CreateDate" FieldFormat="{0:d}" />
								<%--by
								<carrot:ListItemNavText runat="server" ID="ListItemNavText11" DataField="Author_FullName_FirstLast" />--%>
								<%--<%# String.Format("{0}", DataBinder.Eval(Container.DataItem, "EditDate"))%>--%>
							</b>
							<br />
							<carrot:ListItemNavText runat="server" ID="ListItemNavText2" DataField="PageTextPlainSummary" />
						</p>
						<p>
							<carrot:PostMetaWordList HtmlTagNameInner="span" HtmlTagNameOuter="span" ID="wpl1" runat="server" ContentType="Category" MetaDataTitle="Categories:" />
							<br />
							<carrot:PostMetaWordList HtmlTagNameInner="span" HtmlTagNameOuter="span" ID="wpl2" runat="server" ContentType="Tag" MetaDataTitle="Tags:" />
						</p>
						<p class="post-footer align-right">
							<carrot:NavLinkForTemplate CssClassNormal="readmore" ID="NavLinkForTemplate2" runat="server" UseDefaultText="false">
								Read more</carrot:NavLinkForTemplate>
							<span class="comments">Comments
								<carrot:ListItemNavText runat="server" ID="ListItemNavText4" DataField="CommentCount" FieldFormat=" ({0}) " />
							</span><span class="date">
								<carrot:ListItemNavText runat="server" ID="ListItemNavText3" DataField="CreateDate" FieldFormat="{0:MMM d, yyyy}" />
							</span>
						</p>
					</div>
				</SummaryTemplate>
				<SummaryFooterTemplate>
					</div>
				</SummaryFooterTemplate>
				<PagerHeaderTemplate>
					<div class="pagerfooterlinks">
				</PagerHeaderTemplate>
				<PagerTemplate>
					<carrot:ListItemWrapperForPager HtmlTagName="div" ID="wrap" runat="server" CSSSelected="selectedwrap" CssClassNormal="pagerlink">
						<%--<div class="pagerlink">--%>
						<carrot:NavLinkForPagerTemplate ID="lnkBtn" CSSSelected="selected" runat="server" />
						<%--<carrot:NavLinkForPagerTemplate ID="lnkBtn" CSSSelected="selected" runat="server" UseDefaultText="false">
							[[
							<carrot:NavPageNumberDisplay ID="txt" runat="server" />
							]]
						</carrot:NavLinkForPagerTemplate>--%>
						<%--</div>--%>
					</carrot:ListItemWrapperForPager>
				</PagerTemplate>
				<PagerFooterTemplate>
					</div>
				</PagerFooterTemplate>
			</carrot:PagedDataSummary>
			<div style="clear: both;">
			</div>
			<carrot:WidgetContainer ID="phCenterBottom" runat="server">
			</carrot:WidgetContainer>
			<br />
		</div>
		<div id="rightbar">
			<carrot:WidgetContainer ID="phRightTop" runat="server">
			</carrot:WidgetContainer>
			<carrot:ContentContainer EnableViewState="false" ID="BodyRight" runat="server">
			</carrot:ContentContainer>
			<carrot:WidgetContainer ID="phRightBottom" runat="server">
			</carrot:WidgetContainer>
		</div>
	</div>
	<div id="footer">
		<div id="footer-content">
			<div id="footer-right">
				<%--<a href="http://www.free-css.com/">Home</a> | <a href="http://www.free-css.com/">Site Map</a> | <a href="http://www.free-css.com/">
					RSS Feed</a>--%>
			</div>
			<div id="footer-left">
				<asp:PlaceHolder ID="myFooter" runat="server">
					<%=String.Format("&copy;  {0}, {1}. ", DateTime.Now.Year, theSite.SiteName) %>
					All rights reserved. | Site built with <a target="_blank" href="http://www.carrotware.com/carrotcake-cms.aspx">carrotcake cms</a>
					<br />
					Design by: <a target="_blank" href="http://www.styleshout.com/">styleshout</a> | Valid <a target="_blank" href="http://validator.w3.org/check/referer">XHTML</a>
					| <a target="_blank" href="http://jigsaw.w3.org/css-validator/check/referer">CSS</a> </asp:PlaceHolder>
			</div>
		</div>
	</div>
	<asp:Panel ID="pnlHiddenControls" Visible="false" runat="server">
		<br />
		<carrot:WidgetContainer ID="phExtraZone1" runat="server">
		</carrot:WidgetContainer>
		<carrot:WidgetContainer ID="phExtraZone2" runat="server">
		</carrot:WidgetContainer>
		<carrot:WidgetContainer ID="phExtraZone3" runat="server">
		</carrot:WidgetContainer>
		<carrot:WidgetContainer ID="phExtraZone4" runat="server">
		</carrot:WidgetContainer>
		<carrot:WidgetContainer ID="phExtraZone5" runat="server">
		</carrot:WidgetContainer>
		<carrot:WidgetContainer ID="phExtraZone6" runat="server">
		</carrot:WidgetContainer>
	</asp:Panel>
	</form>
</body>
</html>
