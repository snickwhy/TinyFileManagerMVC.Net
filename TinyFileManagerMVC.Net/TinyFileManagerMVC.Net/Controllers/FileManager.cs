using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyFileManagerMVC.Net.Library;
using TinyFileManagerMVC.Net.Models;

namespace TinyFileManagerMVC.Net.Controllers
{
    public class FileManagerController : Controller
    {
        //
        // GET: /FileManager/

        public ActionResult Index()
        {
            FileManagerModel model = new FileManagerModel();

            model.strCmd = Request.QueryString["cmd"] + "";
            model.strType = Request.QueryString["type"] + "";
            model.strFolder = Request.QueryString["folder"] + "";
            model.strFile = Request.QueryString["file"] + "";
            model.strLang = Request.QueryString["lang"] + "";      //not used right now, but grab it
            model.strEditor = Request.QueryString["editor"] + "";
            model.strCurrPath = Request.QueryString["currpath"] + "";
            model.strProfile = Request.QueryString["profile"] + "";

            // load config
            model.objConfig = new FileManagerConfig(model.strProfile);

            //check inputs
            if (model.strCurrPath.Length > 0)
            {
                model.strCurrPath = model.strCurrPath.TrimEnd('\\') + "\\";
            }

            //set the apply string, based on the passed type
            if (model.strType == "")
            {
                model.strType = "0";
            }
            switch (model.strType)
            {
                case "1":
                    model.strApply = "apply_img";
                    model.boolOnlyImage = true;
                    model.strAllowedFileExt = model.objConfig.strAllowedImageExtensions;
                    break;
                case "2":
                    model.strApply = "apply_link";
                    model.strAllowedFileExt = model.objConfig.strAllowedAllExtensions;
                    break;
                default:
                    if (Convert.ToInt32(model.strType) >= 3)
                    {
                        model.strApply = "apply_video";
                        model.boolOnlyVideo = true;
                        model.strAllowedFileExt = model.objConfig.strAllowedVideoExtensions;
                    }
                    else
                    {
                        model.strApply = "apply";
                        model.strAllowedFileExt = model.objConfig.strAllowedAllExtensions;
                    }
                    break;
            }

            //setup current link
            model.strCurrLink = "/FileManager/Index?type=" + model.strType + "&editor=" + model.strEditor + "&lang=" + model.strLang + "&profile=" + model.strProfile;

            switch (model.strCmd)
            {
                case "debugsettings":
                    Response.Write("<style>");
                    Response.Write("body {font-family: Verdana; font-size: 10pt;}");
                    Response.Write(".table {display: table; border-collapse: collapse; margin: 20px; background-color: #e7e5e5;}");
                    Response.Write(".tcaption {display: table-caption; padding: 5px; font-size: 14pt; font-weight: bold; background-color: #9fcff7;}");
                    Response.Write(".tr {display: table-row;}");
                    Response.Write(".tr:hover {background-color: #f0f2f3;}");
                    Response.Write(".td {display: table-cell; padding: 5px; border: 1px solid #a19e9e;}");
                    Response.Write("</style>");

                    Response.Write("<div class=\"table\">");   //start table

                    Response.Write("<div class=\"tcaption\">Operating Settings</div>");  //caption

                    Response.Write("<div class=\"tbody\">");  //start body

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowCreateFolder:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.boolAllowCreateFolder + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowDeleteFile:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.boolAllowDeleteFile + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowDeleteFolder:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.boolAllowDeleteFolder + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowUploadFile:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.boolAllowUploadFile + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>MaxUploadSizeMb:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.intMaxUploadSizeMb + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowedAllExtensions:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strAllowedAllExtensions + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowedFileExtensions:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strAllowedFileExtensions + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowedImageExtensions:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strAllowedImageExtensions + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowedMiscExtensions:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strAllowedMiscExtensions + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowedMusicExtensions:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strAllowedMusicExtensions + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>AllowedVideoExtensions:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strAllowedVideoExtensions + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>BaseURL:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strBaseURL + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>DocRoot:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strDocRoot + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>ThumbPath:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strThumbPath + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>ThumbURL:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strThumbURL + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>UploadPath:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strUploadPath + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>UploadURL:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strUploadURL + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>FillSelector:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strFillSelector + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("<div class=\"tr\">");   // start row
                    Response.Write("<div class=\"td\"><b>PopupCloseCode:</b></div>");
                    Response.Write("<div class=\"td\">" + model.objConfig.strPopupCloseCode + "</div>");
                    Response.Write("</div>");   //end row

                    Response.Write("</div>");   //end body
                    Response.Write("</div>");   //end table



                    Response.End();
                    break;
                case "createfolder":
                    try
                    {
                        model.strFolder = Request.Form["folder"] + "";
                        //forge ahead without checking for existence
                        //catch will save us
                        Directory.CreateDirectory(model.objConfig.strUploadPath + "\\" + model.strFolder);
                        Directory.CreateDirectory(model.objConfig.strThumbPath + "\\" + model.strFolder);

                        // end response, since it's an ajax call
                        Response.End();
                    }
                    catch
                    {
                        //TODO: write error
                    }
                    break;

                case "upload":
                    model.strFolder = Request.Form["folder"] + "";
                    HttpPostedFileBase filUpload = Request.Files["file"];
                    string strTargetFile;
                    string strThumbFile;

                    //check file was submitted
                    if ((filUpload != null) && (filUpload.ContentLength > 0))
                    {
                        strTargetFile = model.objConfig.strUploadPath + model.strFolder + filUpload.FileName;
                        strThumbFile = model.objConfig.strThumbPath + model.strFolder + filUpload.FileName;
                        filUpload.SaveAs(strTargetFile);

                        if (model.isImageFile(strTargetFile))
                        {
                            createThumbnail(strTargetFile, strThumbFile);
                        }
                    }

                    // end response
                    if (Request.Form["fback"] == "true")
                    {
                        Response.Redirect(model.strCurrLink);
                    }
                    else
                    {
                        Response.End();
                    }

                    break;

                case "download":
                    FileInfo objFile = new FileInfo(model.objConfig.strUploadPath + "\\" + model.strFile);
                    Response.ClearHeaders();
                    Response.AddHeader("Pragma", "private");
                    Response.AddHeader("Cache-control", "private, must-revalidate");
                    Response.AddHeader("Content-Type", "application/octet-stream");
                    Response.AddHeader("Content-Length", objFile.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(model.strFile));
                    Response.WriteFile(model.objConfig.strUploadPath + "\\" + model.strFile);
                    break;

                case "delfile":
                    try
                    {
                        System.IO.File.Delete(model.objConfig.strUploadPath + "\\" + model.strFile);
                        if (System.IO.File.Exists(model.objConfig.strThumbPath + "\\" + model.strFile))
                        {
                            System.IO.File.Delete(model.objConfig.strThumbPath + "\\" + model.strFile);
                        }
                    }
                    catch
                    {
                        //TODO: set error
                    }
                    goto default;

                case "delfolder":
                    try
                    {
                        Directory.Delete(model.objConfig.strUploadPath + "\\" + model.strFolder, true);
                        Directory.Delete(model.objConfig.strThumbPath + "\\" + model.strFolder, true);
                    }
                    catch
                    {
                        //TODO: set error
                    }
                    goto default;

                default:    //just a regular page load
                    if (model.strCurrPath != "")
                    {
                        // add "up one" folder
                        model.objFItem = new FileManagerFileItem();
                        model.objFItem.strName = "..";
                        model.objFItem.boolIsFolder = true;
                        model.objFItem.boolIsFolderUp = true;
                        model.objFItem.intColNum = model.getNextColNum();
                        model.objFItem.strPath = model.getUpOneDir(model.strCurrPath);
                        model.objFItem.strClassType = "dir";
                        model.objFItem.strDeleteLink = "<a class=\"btn erase-button top-right disabled\" title=\"Erase\"><i class=\"icon-trash\"></i></a>";
                        model.objFItem.strThumbImage = "/Content/images/ico/folder_return.png";
                        model.objFItem.strLink = "<a title=\"Open\" href=\"" + model.strCurrLink + "&currpath=" + model.objFItem.strPath + "\"><img class=\"directory-img\" src=\"" + model.objFItem.strThumbImage + "\" alt=\"folder\" /><h3>..</h3></a>";
                        model.arrLinks.Add(model.objFItem);
                    }

                    //load folders
                    model.arrFolders = Directory.GetDirectories(model.objConfig.strUploadPath + model.strCurrPath);
                    foreach (string strF in model.arrFolders)
                    {
                        model.objFItem = new FileManagerFileItem();
                        model.objFItem.strName = Path.GetFileName(strF);
                        model.objFItem.boolIsFolder = true;
                        model.objFItem.intColNum = model.getNextColNum();
                        model.objFItem.strPath = model.strCurrPath + Path.GetFileName(strF);
                        model.objFItem.strClassType = "dir";
                        if (model.objConfig.boolAllowDeleteFolder)
                        {
                            model.objFItem.strDeleteLink = "<a href=\"" + model.strCurrLink + "&cmd=delfolder&folder=" + model.objFItem.strPath + "&currpath=" + model.strCurrPath + "\" class=\"btn erase-button top-right\" onclick=\"return confirm('Are you sure to delete the folder and all the objects in it?');\" title=\"Erase\"><i class=\"icon-trash\"></i></a>";
                        }
                        else
                        {
                            model.objFItem.strDeleteLink = "<a class=\"btn erase-button top-right disabled\" title=\"Erase\"><i class=\"icon-trash\"></i></a>";
                        }
                        model.objFItem.strThumbImage = "/Content/images/ico/folder.png";
                        model.objFItem.strLink = "<a title=\"Open\" href=\"" + model.strCurrLink + "&currpath=" + model.objFItem.strPath + "\"><img class=\"directory-img\" src=\"" + model.objFItem.strThumbImage + "\" alt=\"folder\" /><h3>" + model.objFItem.strName + "</h3></a>";
                        model.arrLinks.Add(model.objFItem);
                    }

                    // load files
                    model.arrFiles = Directory.GetFiles(model.objConfig.strUploadPath + model.strCurrPath);
                    foreach (string strF in model.arrFiles)
                    {
                        model.objFItem = new FileManagerFileItem();
                        model.objFItem.strName = Path.GetFileNameWithoutExtension(strF);
                        model.objFItem.boolIsFolder = false;
                        model.objFItem.strPath = model.strCurrPath + Path.GetFileName(strF);
                        model.objFItem.boolIsImage = model.isImageFile(Path.GetFileName(strF));
                        model.objFItem.boolIsVideo = model.isVideoFile(Path.GetFileName(strF));
                        model.objFItem.boolIsMusic = model.isMusicFile(Path.GetFileName(strF));
                        model.objFItem.boolIsMisc = model.isMiscFile(Path.GetFileName(strF));

                        // check to see if it's the type of file we are looking at
                        if ((model.boolOnlyImage && model.objFItem.boolIsImage) || (model.boolOnlyVideo && model.objFItem.boolIsVideo) || (!model.boolOnlyImage && !model.boolOnlyVideo))
                        {
                            model.objFItem.intColNum = model.getNextColNum();
                            // get display class type
                            if (model.objFItem.boolIsImage)
                            {
                                model.objFItem.strClassType = "2";
                            }
                            else
                            {
                                if (model.objFItem.boolIsMisc)
                                {
                                    model.objFItem.strClassType = "3";
                                }
                                else
                                {
                                    if (model.objFItem.boolIsMusic)
                                    {
                                        model.objFItem.strClassType = "5";
                                    }
                                    else
                                    {
                                        if (model.objFItem.boolIsVideo)
                                        {
                                            model.objFItem.strClassType = "4";
                                        }
                                        else
                                        {
                                            model.objFItem.strClassType = "1";
                                        }
                                    }
                                }
                            }
                            // get delete link
                            if (model.objConfig.boolAllowDeleteFile)
                            {
                                model.objFItem.strDeleteLink = "<a href=\"" + model.strCurrLink + "&cmd=delfile&file=" + model.objFItem.strPath + "&currpath=" + model.strCurrPath + "\" class=\"btn erase-button\" onclick=\"return confirm('Are you sure to delete this file?');\" title=\"Erase\"><i class=\"icon-trash\"></i></a>";
                            }
                            else
                            {
                                model.objFItem.strDeleteLink = "<a class=\"btn erase-button disabled\" title=\"Erase\"><i class=\"icon-trash\"></i></a>";
                            }
                            // get thumbnail image
                            if (model.objFItem.boolIsImage)
                            {
                                // first check to see if thumb exists
                                if (!System.IO.File.Exists(model.objConfig.strThumbPath + model.objFItem.strPath))
                                {
                                    // thumb doesn't exist, create it
                                    strTargetFile = model.objConfig.strUploadPath + model.objFItem.strPath;
                                    strThumbFile = model.objConfig.strThumbPath + model.objFItem.strPath;
                                    createThumbnail(strTargetFile, strThumbFile);
                                }
                                model.objFItem.strThumbImage = model.objConfig.strThumbURL + "/" + model.objFItem.strPath.Replace('\\', '/');
                            }
                            else
                            {
                                if (System.IO.File.Exists(Directory.GetParent(Request.PhysicalPath).FullName + "\\img\\ico\\" + Path.GetExtension(strF).TrimStart('.').ToUpper() + ".png"))
                                {
                                    model.objFItem.strThumbImage = "/Content/images/ico/" + Path.GetExtension(strF).TrimStart('.').ToUpper() + ".png";
                                }
                                else
                                {
                                    model.objFItem.strThumbImage = "/Content/images/ico/Default.png";
                                }
                            }
                            model.objFItem.strDownFormOpen = "<form action=\"/FileManager/Index?cmd=download&file=" + model.objFItem.strPath + "\" method=\"post\" class=\"download-form\">";
                            if (model.objFItem.boolIsImage)
                            {
                                model.objFItem.strPreviewLink = "<a class=\"btn preview\" title=\"Preview\" data-url=\"" + model.objConfig.strUploadURL + "/" + model.objFItem.strPath.Replace('\\', '/') + "\" data-toggle=\"lightbox\" href=\"#previewLightbox\"><i class=\"icon-eye-open\"></i></a>";
                            }
                            else
                            {
                                model.objFItem.strPreviewLink = "<a class=\"btn preview disabled\" title=\"Preview\"><i class=\"icon-eye-open\"></i></a>";
                            }
                            model.objFItem.strLink = "<a href=\"#\" title=\"Select\" onclick=\"" + model.strApply + "('" + model.objConfig.strUploadURL + "/" + model.objFItem.strPath.Replace('\\', '/') + "'," + model.strType + ")\";\"><img data-src=\"holder.js/140x100\" alt=\"140x100\" src=\"" + model.objFItem.strThumbImage + "\" height=\"100\"><h4>" + model.objFItem.strName + "</h4></a>";

                            model.arrLinks.Add(model.objFItem);
                        }
                    } // foreach

                    break;
            }   // switch


            return View(model);
        }



        private void createThumbnail(string strFilename, string strThumbFilename)
        {
            System.Drawing.Image.GetThumbnailImageAbort objCallback;
            System.Drawing.Image objFSImage;
            System.Drawing.Image objTNImage;
            System.Drawing.RectangleF objRect;
            System.Drawing.GraphicsUnit objUnits = System.Drawing.GraphicsUnit.Pixel;
            int intHeight = 0;
            int intWidth = 0;

            // open image and get dimensions in pixels
            objFSImage = System.Drawing.Image.FromFile(strFilename);
            objRect = objFSImage.GetBounds(ref objUnits);

            // what are we going to resize to, to fit inside 156x78
            getProportionalResize(Convert.ToInt32(objRect.Width), Convert.ToInt32(objRect.Height), ref intWidth, ref intHeight);

            // create thumbnail
            objCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            objTNImage = objFSImage.GetThumbnailImage(intWidth, intHeight, objCallback, IntPtr.Zero);

            // finish up
            objFSImage.Dispose();
            objTNImage.Save(strThumbFilename);
            objTNImage.Dispose();

        } // createThumbnail

        private void getProportionalResize(int intOldWidth, int intOldHeight, ref int intNewWidth, ref int intNewHeight)
        {
            int intHDiff = 0;
            int intWDiff = 0;
            decimal decProp = 0;
            int intTargH = 78;
            int intTargW = 156;

            if ((intOldHeight <= intTargH) && (intOldWidth <= intTargW))
            {
                // no resize needed
                intNewHeight = intOldHeight;
                intNewWidth = intOldWidth;
                return;
            }

            //get the differences between desired and current height and width
            intHDiff = intOldHeight - intTargH;
            intWDiff = intOldWidth - intTargW;

            //whichever is the bigger difference is the chosen proportion
            if (intHDiff > intWDiff)
            {
                decProp = (decimal)intTargH / (decimal)intOldHeight;
                intNewHeight = intTargH;
                intNewWidth = Convert.ToInt32(Math.Round(intOldWidth * decProp, 0));
            }
            else
            {
                decProp = (decimal)intTargW / (decimal)intOldWidth;
                intNewWidth = intTargW;
                intNewHeight = Convert.ToInt32(Math.Round(intOldHeight * decProp, 0));
            }
        } // getProportionalResize

        private bool ThumbnailCallback()
        {
            return false;
        } // ThumbnailCallback

    }
}
