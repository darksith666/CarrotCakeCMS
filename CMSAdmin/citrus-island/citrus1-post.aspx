<%@ Page Language="C#" AutoEventWireup="true" Inherits="Carrotware.CMS.UI.Base.GenericPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<carrot:jquery runat="server" ID="jquery1" />
	<carrot:jqueryui runat="server" ID="jqueryui1" />
	<link runat="server" id="theCSS" href="style.css" rel="stylesheet" type="text/css" media="screen" />
	<title>Citrus Island</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<carrot:SiteCanonicalURL runat="server" ID="SiteCanonicalURL1" />
	<carrot:RSSFeed runat="server" ID="RSSFeed1" />
</head>
<body>
	<form id="form1" runat="server">
	<div id="wrap">
		<div id="header">
			<div class="search">
				<carrot:SearchBox ID="search1" runat="server">
					<SearchTemplate>
						<p>
							<asp:TextBox ID="SearchText" runat="server" CssClass="textbox" MaxLength="40" />
							<asp:Button ID="btnSiteSearch" runat="server" CssClass="button" Text="Search" />
						</p>
					</SearchTemplate>
				</carrot:SearchBox>
			</div>
			<h1 id="logo">
				<a href="/">
					<carrot:SiteDataProperty runat="server" ID="SiteDataProperty1" DataField="SiteName" /></a>
			</h1>
			<h2 id="slogan">
				<carrot:SiteDataProperty runat="server" ID="SiteDataProperty2" DataField="SiteTagline" />
			</h2>
		</div>
		<div id="menu-wrapper">
			<carrot:TwoLevelNavigation runat="server" ID="TwoLevelNavigation1" FontSize="10px" ForeColor="#FFFFFF" BackColor="#F4845A" />
		</div>
		<div id="sidebar">
			<carrot:SiteMetaWordList ID="SiteMetaWordList1" HeadWrapTag="h1" runat="server" ContentType="Category" MetaDataTitle="Categories" CssClass="sidemenu" />
			<carrot:SiteMetaWordList ID="SiteMetaWordList2" HeadWrapTag="h1" runat="server" ContentType="Tag" MetaDataTitle="Tags" CssClass="sidemenu" />
			<carrot:WidgetContainer ID="phLeftTop" runat="server">
			</carrot:WidgetContainer>
			<carrot:ContentContainer EnableViewState="false" ID="BodyLeft" runat="server" />
			<carrot:WidgetContainer ID="phLeftBottom" runat="server">
			</carrot:WidgetContainer>
		</div>
		<div id="main">
			<h1>
				<carrot:ContentPageProperty runat="server" ID="ContentPageProperty3" DataField="PageHead" /></h1>
			<p>
				By
				<carrot:ContentPageProperty runat="server" ID="ContentPageProperty1" DataField="Author_FullName_FirstLast" />
				on
				<carrot:ContentPageProperty runat="server" ID="ContentPageProperty2" DataField="GoLiveDate" FieldFormat="{0:MMMM d, yyyy}" />
			</p>
			<carrot:WidgetContainer ID="phCenterTop" runat="server">
			</carrot:WidgetContainer>
			<carrot:ContentContainer EnableViewState="false" ID="BodyCenter" runat="server" />
			<carrot:WidgetContainer ID="phCenterBottom" runat="server">
			</carrot:WidgetContainer>
			<div style="clear: both;">
			</div>
			<p>
				<carrot:PostMetaWordList HtmlTagNameInner="span" HtmlTagNameOuter="span" ID="wpl1" runat="server" ContentType="Category" MetaDataTitle="Categories:" />
				<br />
				<carrot:PostMetaWordList HtmlTagNameInner="span" HtmlTagNameOuter="span" ID="wpl2" runat="server" ContentType="Tag" MetaDataTitle="Tags:" />
			</p>
			<div>
				<carrot:ContentCommentForm runat="server" ID="commentFrm">
					<CommentEntryTemplate>
						<carrot:jsHelperLib runat="server" ID="jsHelperLib1" />
						<div>
							<asp:Label ID="ContentCommentFormMsg" runat="server" Text="" />
						</div>
						<div class="input-form">
							<p class="padding10">
								<label>
									name:
									<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ContentCommentForm" ControlToValidate="CommenterName" ErrorMessage="*" />
								</label>
								<asp:TextBox runat="server" ID="CommenterName" Columns="30" MaxLength="100" ValidationGroup="ContentCommentForm" />
								<label>
									email:
									<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ContentCommentForm" ControlToValidate="CommenterEmail" ErrorMessage="*" />
								</label>
								<asp:TextBox runat="server" ID="CommenterEmail" Columns="30" MaxLength="100" ValidationGroup="ContentCommentForm" />
								<label>
									website:
								</label>
								<asp:TextBox runat="server" ID="CommenterURL" Columns="30" MaxLength="100" ValidationGroup="ContentCommentForm" />
								<label>
									comment:
									<asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="ContentCommentForm" ControlToValidate="VisitorComments" ClientValidationFunction="__carrotware_ValidateLongText"
										EnableClientScript="true" ErrorMessage="**" />
								</label>
								<asp:TextBox runat="server" ID="VisitorComments" TextMode="MultiLine" Rows="8" Columns="40" MaxLength="1024" />
								<div class="padding10">
									<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ContentCommentForm" ControlToValidate="ContentCommentCaptcha"
										ErrorMessage="**" />
									<carrot:Captcha runat="server" ID="ContentCommentCaptcha" ValidationGroup="ContentCommentForm" />
								</div>
								<div class="padding10">
									<asp:Button ID="SubmitCommentButton" CssClass="button padding10" runat="server" Text="Submit Comment" ValidationGroup="ContentCommentForm" />
								</div>
								<script type="text/javascript">
									__carrotware_PageValidate();
								</script>
							</p>
						</div>
					</CommentEntryTemplate>
				</carrot:ContentCommentForm>
				<div style="clear: both;">
				</div>
				<br />
				<hr />
				<div style="clear: both;">
				</div>
				<carrot:PagedComments ID="PagedComments1" runat="server" PageSize="5" CSSSelectedPage="selected">
					<CommentHeaderTemplate>
						<div>
					</CommentHeaderTemplate>
					<CommentTemplate>
						<div>
							<p>
								<b>
									<carrot:ListItemCommentText runat="server" ID="ListItemCommentText1" DataField="CommenterName" />
								</b>
								<carrot:ListItemCommentText runat="server" ID="ListItemCommentText2" DataField="CreateDate" FieldFormat="{0:d}" />
								<br />
								<carrot:ListItemCommentText runat="server" ID="ListItemCommentText3" DataField="PostCommentEscaped" />
							</p>
						</div>
					</CommentTemplate>
					<CommentFooterTemplate>
						</div>
					</CommentFooterTemplate>
					<PagerHeaderTemplate>
						<div class="pagerfooterlinks">
					</PagerHeaderTemplate>
					<PagerTemplate>
						<carrot:ListItemWrapperForPager HtmlTagName="div" ID="ListItemWrapperForPager1" runat="server" CSSSelected="selectedwrap" CssClassNormal="pagerlink">
							<carrot:NavLinkForPagerTemplate ID="lnkBtn" CSSSelected="selected" runat="server" />
						</carrot:ListItemWrapperForPager>
					</PagerTemplate>
					<PagerFooterTemplate>
						</div>
					</PagerFooterTemplate>
				</carrot:PagedComments>
				<div style="clear: both;">
				</div>
			</div>
			<div style="clear: both;">
			</div>
		</div>
		<div id="rightbar">
			<div class="padding10">
				<div class="rsszone">
					<carrot:RSSFeed runat="server" ID="RSSFeed2" RSSFeedType="BlogOnly" CssClass="rssimage" RenderRSSMode="ImageLink" />
					&nbsp;&nbsp;&nbsp;&nbsp;
					<carrot:RSSFeed runat="server" ID="RSSFeed3" RSSFeedType="PageOnly" CssClass="rssimage" RenderRSSMode="ImageLink" />
				</div>
			</div>
			<div class="padding10 ">
				<div class="previous-next">
					<span class="nav-previous">
						<carrot:ContentPageNext runat="server" ID="prevLink" UseDefaultText="false" NavigationDirection="Prev"><span class="meta-nav">&larr;</span> Previous</carrot:ContentPageNext>
					</span><span class="nav-next">
						<carrot:ContentPageNext runat="server" ID="nextLink" UseDefaultText="false" NavigationDirection="Next"> Next <span class="meta-nav">&rarr;</span> </carrot:ContentPageNext>
					</span>
				</div>
			</div>
			<carrot:WidgetContainer ID="phRightTop" runat="server">
			</carrot:WidgetContainer>
			<carrot:ContentContainer EnableViewState="false" ID="BodyRight" runat="server" />
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
	</asp:Panel>
	</form>
</body>
</html>
