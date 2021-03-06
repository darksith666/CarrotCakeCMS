﻿using Carrotware.CMS.Core;
using Carrotware.CMS.Interface;
using Carrotware.Web.UI.Controls;
using System;

namespace Carrotware.CMS.UI.Plugins.PhotoGallery {

	public partial class PhotoGalleryAdminMetaData : AdminModule {
		private Guid gTheID = Guid.Empty;
		public string sImageFile = String.Empty;

		protected FileDataHelper helpFile = new FileDataHelper();

		protected void Page_Load(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(Request.QueryString["id"])) {
				gTheID = new Guid(Request.QueryString["id"].ToString());
			}
			if (!string.IsNullOrEmpty(Request.QueryString["parm"])) {
				sImageFile = CMSConfigHelper.DecodeBase64(Request.QueryString["parm"].ToString());
			}

			litImgName.Text = sImageFile;
			ImageSizer1.ImageUrl = sImageFile;
			ImageSizer1.ToolTip = sImageFile;

			if (!IsPostBack) {
				LoadForm();
			}
		}

		private void LoadForm() {
			GalleryHelper gh = new GalleryHelper(SiteID);
			var meta = gh.GalleryMetaDataGetByFilename(sImageFile);

			if (meta != null) {
				txtMetaInfo.Text = meta.ImageMetaData;
				txtTitle.Text = meta.ImageTitle;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			GalleryHelper gh = new GalleryHelper(SiteID);
			var meta = gh.GalleryMetaDataGetByFilename(sImageFile);

			if (meta == null) {
				meta = new GalleryMetaData();
				meta.GalleryImageMetaID = Guid.Empty;
				meta.SiteID = SiteID;
				meta.GalleryImage = sImageFile.ToLower();
			}

			meta.ImageMetaData = txtMetaInfo.Text;
			meta.ImageTitle = txtTitle.Text;

			meta.Save();

			Response.Redirect(SiteData.CurrentScriptName + "?" + Request.QueryString.ToString());
		}
	}
}