﻿using Carrotware.CMS.Core;
using Carrotware.CMS.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

/*
* CarrotCake CMS
* http://www.carrotware.com/
*
* Copyright 2011, Samantha Copeland
* Dual licensed under the MIT or GPL Version 3 licenses.
*
* Date: October 2011
*/

namespace Carrotware.CMS.UI.Admin.c3_admin {

	public partial class SiteImport : AdminBasePage {
		public Guid guidImportID = Guid.Empty;
		private SiteExport exSite = null;
		private List<BasicContentData> sitePageList = new List<BasicContentData>();
		private int iPageCount = 0;

		protected void Page_Load(object sender, EventArgs e) {
			guidImportID = GetGuidParameterFromQuery("importid");

			litMessage.Text = "";

			iPageCount = pageHelper.GetSitePageCount(SiteID, ContentPageType.PageType.ContentEntry);

			if (guidImportID != Guid.Empty) {
				exSite = ContentImportExportUtils.GetSerializedSiteExport(guidImportID);

				litName.Text = exSite.TheSite.SiteName;
				litDescription.Text = exSite.TheSite.SiteTagline;
				litImportSource.Text = exSite.CarrotCakeVersion;
				litDate.Text = exSite.ExportDate.ToString();

				if (!IsPostBack) {
					BindData();
				}
			}
		}

		private void BindData() {
			GeneralUtilities.BindDataBoundControl(gvPages, exSite.TheContentPages.Select(s => s.ThePage).OrderBy(s => s.NavOrder));
			GeneralUtilities.BindDataBoundControl(gvPosts, exSite.TheBlogPages.Select(s => s.ThePage).OrderByDescending(s => s.CreateDate));
			GeneralUtilities.BindDataBoundControl(gvSnippets, exSite.TheSnippets.OrderByDescending(s => s.ContentSnippetName));

			GeneralUtilities.BindList(ddlTemplatePage, cmsHelper.Templates);
			GeneralUtilities.BindList(ddlTemplatePost, cmsHelper.Templates);

			lblPages.Text = gvPages.Rows.Count.ToString();
			lblPosts.Text = gvPosts.Rows.Count.ToString();
			lblSnippets.Text = gvSnippets.Rows.Count.ToString();
			lblComments.Text = exSite.TheComments.Count.ToString();
			SetDDLDefaultTemplates();
		}

		protected void SetDDLDefaultTemplates() {
			float iThird = (float)(iPageCount - 1) / (float)3;
			Dictionary<string, float> dictTemplates = null;

			dictTemplates = pageHelper.GetPopularTemplateList(SiteID, ContentPageType.PageType.ContentEntry);
			if (dictTemplates.Any() && dictTemplates.First().Value >= iThird) {
				try { ddlTemplatePage.SelectedValue = dictTemplates.First().Key; } catch { }
			}

			dictTemplates = pageHelper.GetPopularTemplateList(SiteID, ContentPageType.PageType.BlogEntry);
			if (dictTemplates.Any()) {
				try { ddlTemplatePost.SelectedValue = dictTemplates.First().Key; } catch { }
			}
		}

		private int iAccessCounter = 0;

		protected BasicContentData GetFileInfoFromList(SiteData site, string sFilename) {
			if (sitePageList == null || sitePageList.Count < 1 || iAccessCounter % 25 == 0) {
				sitePageList = site.GetFullSiteFileList();
				iAccessCounter = 0;
			}
			iAccessCounter++;

			BasicContentData pageData = (from m in sitePageList
										 where m.FileName.ToLowerInvariant() == sFilename.ToLowerInvariant()
										 select m).FirstOrDefault();

			if (pageData == null) {
				using (ISiteNavHelper navHelper = SiteNavFactory.GetSiteNavHelper()) {
					pageData = BasicContentData.CreateBasicContentDataFromSiteNav(navHelper.GetLatestVersion(site.SiteID, false, sFilename.ToLowerInvariant()));
				}
			}

			return pageData;
		}

		private SiteNav _navHome = null;

		protected SiteNav GetHomePage(SiteData site) {
			if (_navHome == null) {
				using (ISiteNavHelper navHelper = SiteNavFactory.GetSiteNavHelper()) {
					_navHome = navHelper.FindHome(site.SiteID, false);
				}
			}
			return _navHome;
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			try {
				ImportStuff();
			} catch (Exception ex) {
				litMessage.Text += ex.ToString();
			}
		}

		private Guid FindUser(Guid userId) {
			MembershipUser usr = SecurityData.GetUserByGuid(userId);

			if (usr == null) {
				return SecurityData.CurrentUserGuid;
			} else {
				return userId;
			}
		}

		private void SetMsg(string sMessage) {
			if (!String.IsNullOrEmpty(sMessage)) {
				litMessage.Text = sMessage;
			}
		}

		private void ImportStuff() {
			SiteData.CurrentSite = null;

			SiteData site = SiteData.CurrentSite;

			litMessage.Text = "<p>No Items Selected For Import</p>";
			string sMsg = "";

			if (chkSite.Checked || chkPages.Checked || chkPosts.Checked) {
				List<string> tags = site.GetTagList().Select(x => x.TagSlug.ToLowerInvariant()).ToList();
				List<string> cats = site.GetCategoryList().Select(x => x.CategorySlug.ToLowerInvariant()).ToList();

				exSite.TheTags.RemoveAll(x => tags.Contains(x.TagSlug.ToLowerInvariant()));
				exSite.TheCategories.RemoveAll(x => cats.Contains(x.CategorySlug.ToLowerInvariant()));

				sMsg += "<p>Imported Tags and Categories</p>";

				List<ContentTag> lstTag = (from l in exSite.TheTags.Distinct()
										   select new ContentTag {
											   ContentTagID = Guid.NewGuid(),
											   SiteID = site.SiteID,
											   IsPublic = l.IsPublic,
											   TagSlug = l.TagSlug,
											   TagText = l.TagText
										   }).ToList();

				List<ContentCategory> lstCat = (from l in exSite.TheCategories.Distinct()
												select new ContentCategory {
													ContentCategoryID = Guid.NewGuid(),
													SiteID = site.SiteID,
													IsPublic = l.IsPublic,
													CategorySlug = l.CategorySlug,
													CategoryText = l.CategoryText
												}).ToList();

				foreach (var v in lstTag) {
					v.Save();
				}
				foreach (var v in lstCat) {
					v.Save();
				}
			}
			SetMsg(sMsg);

			if (chkSnippet.Checked) {
				List<string> snippets = site.GetContentSnippetList().Select(x => x.ContentSnippetSlug.ToLowerInvariant()).ToList();

				exSite.TheSnippets.RemoveAll(x => snippets.Contains(x.ContentSnippetSlug.ToLowerInvariant()));

				sMsg += "<p>Imported Content Snippets</p>";

				List<ContentSnippet> lstSnip = (from l in exSite.TheSnippets.Distinct()
												select new ContentSnippet {
													SiteID = site.SiteID,
													Root_ContentSnippetID = Guid.NewGuid(),
													ContentSnippetID = Guid.NewGuid(),
													CreateUserId = SecurityData.CurrentUserGuid,
													CreateDate = site.Now,
													EditUserId = SecurityData.CurrentUserGuid,
													EditDate = site.Now,
													RetireDate = l.RetireDate,
													GoLiveDate = l.GoLiveDate,
													ContentSnippetActive = l.ContentSnippetActive,
													ContentBody = l.ContentBody,
													ContentSnippetSlug = l.ContentSnippetSlug,
													ContentSnippetName = l.ContentSnippetName
												}).ToList();

				foreach (var v in lstSnip) {
					v.Save();
				}
			}
			SetMsg(sMsg);

			if (chkSite.Checked) {
				sMsg += "<p>Updated Site Name</p>";
				site.SiteName = exSite.TheSite.SiteName;
				site.SiteTagline = exSite.TheSite.SiteTagline;
				site.Save();
			}
			SetMsg(sMsg);

			if (!chkMapAuthor.Checked) {
				exSite.TheUsers = new List<SiteExportUser>();
			}

			//itterate author collection and find if in the system
			foreach (SiteExportUser seu in exSite.TheUsers) {
				seu.ImportUserID = Guid.Empty;

				MembershipUser usr = null;
				//attempt to find the user in the userbase
				usr = SecurityData.GetUserListByEmail(seu.Email).FirstOrDefault();
				if (usr != null) {
					seu.ImportUserID = new Guid(usr.ProviderUserKey.ToString());
				} else {
					usr = SecurityData.GetUserListByName(seu.Login).FirstOrDefault();
					if (usr != null) {
						seu.ImportUserID = new Guid(usr.ProviderUserKey.ToString());
					}
				}

				if (chkAuthors.Checked) {
					if (seu.ImportUserID == Guid.Empty) {
						usr = Membership.CreateUser(seu.Login, ProfileManager.GenerateSimplePassword(), seu.Email);
						Roles.AddUserToRole(seu.Login, SecurityData.CMSGroup_Users);
						seu.ImportUserID = new Guid(usr.ProviderUserKey.ToString());
					}

					if (seu.ImportUserID != Guid.Empty) {
						ExtendedUserData ud = new ExtendedUserData(seu.ImportUserID);
						if (ud != null) {
							if (!String.IsNullOrEmpty(seu.FirstName) || !String.IsNullOrEmpty(seu.LastName)) {
								ud.FirstName = seu.FirstName;
								ud.LastName = seu.LastName;
								ud.Save();
							}
						} else {
							throw new Exception(String.Format("Could not find new user: {0} ({1})", seu.Login, seu.Email));
						}
					}
				}
			}

			if (chkPages.Checked) {
				sMsg += "<p>Imported Pages</p>";
				sitePageList = site.GetFullSiteFileList();

				int iOrder = 0;

				SiteNav navHome = GetHomePage(site);

				if (navHome != null) {
					iOrder = 2;
				}

				foreach (var impCP in (from c in exSite.ThePages
									   where c.ThePage.ContentType == ContentPageType.PageType.ContentEntry
									   orderby c.ThePage.NavOrder, c.ThePage.NavMenuText
									   select c).ToList()) {
					ContentPage cp = impCP.ThePage;
					cp.Root_ContentID = impCP.NewRootContentID;
					cp.ContentID = Guid.NewGuid();
					cp.SiteID = site.SiteID;
					cp.ContentType = ContentPageType.PageType.ContentEntry;
					cp.EditDate = SiteData.CurrentSite.Now;
					cp.EditUserId = exSite.FindImportUser(impCP.TheUser);
					cp.CreateUserId = exSite.FindImportUser(impCP.TheUser);
					if (impCP.CreditUser != null) {
						cp.CreditUserId = exSite.FindImportUser(impCP.CreditUser);
					}
					cp.NavOrder = iOrder;
					cp.TemplateFile = ddlTemplatePage.SelectedValue;

					ContentPageExport parent = (from c in exSite.ThePages
												where c.ThePage.ContentType == ContentPageType.PageType.ContentEntry
												  && c.ThePage.FileName.ToLowerInvariant() == impCP.ParentFileName.ToLowerInvariant()
												select c).FirstOrDefault();

					BasicContentData navParent = null;
					BasicContentData navData = GetFileInfoFromList(site, cp.FileName);

					if (parent != null) {
						cp.Parent_ContentID = parent.NewRootContentID;
						navParent = GetFileInfoFromList(site, parent.ThePage.FileName);
					}

					//if URL exists already, make this become a new version in the current series
					if (navData != null) {
						cp.Root_ContentID = navData.Root_ContentID;

						impCP.ThePage.RetireDate = navData.RetireDate;
						impCP.ThePage.GoLiveDate = navData.GoLiveDate;

						if (navData.NavOrder == 0) {
							cp.NavOrder = 0;
						}
					}
					//preserve homepage
					if (navHome != null && navHome.FileName.ToLowerInvariant() == cp.FileName.ToLowerInvariant()) {
						cp.NavOrder = 0;
					}
					//if the file url in the upload has an existing ID, use that, not the ID from the queue
					if (navParent != null) {
						cp.Parent_ContentID = navParent.Root_ContentID;
					}

					cp.RetireDate = impCP.ThePage.RetireDate;
					cp.GoLiveDate = impCP.ThePage.GoLiveDate;

					cp.SavePageEdit();

					iOrder++;
				}
			}
			SetMsg(sMsg);

			if (chkPosts.Checked) {
				sMsg += "<p>Imported Posts</p>";
				sitePageList = site.GetFullSiteFileList();

				List<ContentTag> lstTags = site.GetTagList();
				List<ContentCategory> lstCategories = site.GetCategoryList();

				foreach (var impCP in (from c in exSite.ThePages
									   where c.ThePage.ContentType == ContentPageType.PageType.BlogEntry
									   orderby c.ThePage.CreateDate
									   select c).ToList()) {
					ContentPage cp = impCP.ThePage;
					cp.Root_ContentID = impCP.NewRootContentID;
					cp.ContentID = Guid.NewGuid();
					cp.SiteID = site.SiteID;
					cp.Parent_ContentID = null;
					cp.ContentType = ContentPageType.PageType.BlogEntry;
					cp.EditDate = SiteData.CurrentSite.Now;
					cp.EditUserId = exSite.FindImportUser(impCP.TheUser);
					cp.CreateUserId = exSite.FindImportUser(impCP.TheUser);
					if (impCP.CreditUser != null) {
						cp.CreditUserId = exSite.FindImportUser(impCP.CreditUser);
					}
					cp.NavOrder = SiteData.BlogSortOrderNumber;
					cp.TemplateFile = ddlTemplatePost.SelectedValue;

					cp.ContentCategories = (from l in lstCategories
											join o in impCP.ThePage.ContentCategories on l.CategorySlug.ToLowerInvariant() equals o.CategorySlug.ToLowerInvariant()
											select l).Distinct().ToList();

					cp.ContentTags = (from l in lstTags
									  join o in impCP.ThePage.ContentTags on l.TagSlug.ToLowerInvariant() equals o.TagSlug.ToLowerInvariant()
									  select l).Distinct().ToList();

					BasicContentData navData = GetFileInfoFromList(site, cp.FileName);

					//if URL exists already, make this become a new version in the current series
					if (navData != null) {
						cp.Root_ContentID = navData.Root_ContentID;

						impCP.ThePage.RetireDate = navData.RetireDate;
						impCP.ThePage.GoLiveDate = navData.GoLiveDate;
					}

					cp.RetireDate = impCP.ThePage.RetireDate;
					cp.GoLiveDate = impCP.ThePage.GoLiveDate;

					cp.SavePageEdit();
				}

				using (ContentPageHelper cph = new ContentPageHelper()) {
					//cph.BulkBlogFileNameUpdateFromDate(site.SiteID);
					cph.ResolveDuplicateBlogURLs(site.SiteID);
					cph.FixBlogNavOrder(site.SiteID);
				}
			}
			SetMsg(sMsg);

			if (chkComments.Checked) {
				sMsg += "<p>Imported Comments</p>";
				sitePageList = site.GetFullSiteFileList();

				foreach (var impCP in (from c in exSite.TheComments
									   orderby c.TheComment.CreateDate
									   select c).ToList()) {
					int iCommentCount = -1;
					PostComment pc = impCP.TheComment;
					BasicContentData navData = GetFileInfoFromList(site, pc.FileName);
					if (navData != null) {
						pc.Root_ContentID = navData.Root_ContentID;
						pc.ContentCommentID = Guid.NewGuid();

						iCommentCount = PostComment.GetCommentCountByContent(site.SiteID, pc.Root_ContentID, pc.CreateDate, pc.CommenterIP, pc.PostCommentText);
						if (iCommentCount < 1) {
							iCommentCount = PostComment.GetCommentCountByContent(site.SiteID, pc.Root_ContentID, pc.CreateDate, pc.CommenterIP);
						}

						if (iCommentCount < 1) {
							pc.Save();
						}
					}
				}
			}
			SetMsg(sMsg);

			BindData();
		}
	}
}