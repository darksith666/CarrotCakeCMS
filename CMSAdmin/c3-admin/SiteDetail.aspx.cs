﻿using Carrotware.CMS.Core;
using Carrotware.CMS.UI.Controls;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

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

	public partial class SiteDetail : AdminBasePage {
		private Guid guidSiteID = Guid.Empty;
		private SiteData theSite = null;

		protected void Page_Load(object sender, EventArgs e) {
			Master.ActivateTab(AdminBaseMasterPage.SectionID.SiteIndex);
			guidSiteID = GetGuidIDFromQuery();

			btnAddUsers.Visible = SecurityData.IsAdmin;
			btnRemove.Visible = SecurityData.IsAdmin;

			theSite = SiteData.GetSiteByID(guidSiteID);

			if (theSite != null) {
				litID.Text = theSite.SiteID.ToString();
				litSiteName.Text = theSite.SiteName;
				litTagline.Text = theSite.SiteTagline;
				litURL.Text = theSite.MainURL;
			}

			if (!IsPostBack) {
				GetUserList();
			}
		}

		protected void GetUserList() {
			List<ExtendedUserData> usrs = theSite.GetMappedUsers();
			GeneralUtilities.BindDataBoundControl(gvUsers, usrs);

			if (usrs.Count < 1) {
				btnRemove.Visible = false;
			}
		}

		protected void btnAddUsers_Click(object sender, EventArgs e) {
			if (!String.IsNullOrEmpty(hdnUserID.Value)) {
				ExtendedUserData exUsr = new ExtendedUserData(hdnUserID.Value);
				exUsr.AddToSite(guidSiteID);

				if (chkAddToEditor.Checked) {
					exUsr.AddToRole(SecurityData.CMSGroup_Editors);
				}
			}

			Response.Redirect(SiteData.CurrentScriptName + "?id=" + guidSiteID.ToString());
		}

		protected void btnRemove_Click(object sender, EventArgs e) {
			CheckBox chkSelected = null;
			HiddenField hdnUserId = null;

			foreach (GridViewRow dgItem in gvUsers.Rows) {
				hdnUserId = (HiddenField)dgItem.FindControl("hdnUserId");

				if (hdnUserId != null) {
					chkSelected = (CheckBox)dgItem.FindControl("chkSelected");

					Guid guidUsrID = new Guid(hdnUserId.Value);
					ExtendedUserData exUsr = new ExtendedUserData(guidUsrID);

					if (chkSelected.Checked) {
						exUsr.RemoveFromSite(guidSiteID);
					}
				}
			}

			Response.Redirect(SiteData.CurrentScriptName + "?id=" + guidSiteID.ToString());
		}
	}
}