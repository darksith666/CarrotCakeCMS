﻿using Carrotware.CMS.Core;
using System;

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

	public partial class CommentAddEditPop : AdminBasePage {

		protected void Page_Load(object sender, EventArgs e) {
			ucCommentAddEdit1.FetchItem();

			if (ucCommentAddEdit1.pageType == ContentPageType.PageType.BlogEntry) {
				Master.ActivateTab(AdminBaseMasterPage.SectionID.BlogComment);
			}

			if (ucCommentAddEdit1.pageType == ContentPageType.PageType.ContentEntry) {
				Master.ActivateTab(AdminBaseMasterPage.SectionID.PageComment);
			}
		}
	}
}