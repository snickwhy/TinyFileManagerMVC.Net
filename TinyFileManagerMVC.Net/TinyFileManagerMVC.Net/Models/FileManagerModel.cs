using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TinyFileManagerMVC.Net.Library;

namespace TinyFileManagerMVC.Net.Models
{
    public class FileManagerModel
    {
        public string strType;
        public string strApply;
        public string strCmd;
        public string strFolder;
        public string strFile;
        public string strLang;
        public string strEditor;
        public string strCurrPath;
        public string strCurrLink;     
        public List<FileManagerFileItem> arrLinks = new List<FileManagerFileItem>();
        public string strAllowedFileExt;
        public FileManagerConfig objConfig = null;

        public int intColNum;
        public string[] arrFolders;
        public string[] arrFiles;
        public FileManagerFileItem objFItem;
        public bool boolOnlyImage;
        public bool boolOnlyVideo;
        public string strProfile;


        public string getBreadCrumb()
        {
            string strRet;
            string[] arrFolders;
            string strTempPath = "";
            int intCount = 0;

            strRet = "<li><a href=\"" + this.strCurrLink + "&currpath=\"><i class=\"icon-home\"></i></a>";
            arrFolders = this.strCurrPath.Split('\\');

            foreach (string strFolder in arrFolders)
            {
                if (strFolder != "")
                {
                    strTempPath += strFolder + "\\";
                    intCount++;

                    if (intCount == (arrFolders.Length - 1))
                    {
                        strRet += " <span class=\"divider\">/</span></li> <li class=\"active\">" + strFolder + "</li>";
                    }
                    else
                    {
                        strRet += " <span class=\"divider\">/</span></li> <li><a href=\"" + this.strCurrLink + "&currpath=" + strTempPath + "\">" + strFolder + "</a>";
                    }
                }
            }   // foreach

            return strRet;
        }   // getBreadCrumb 

        public bool isImageFile(string strFilename)
        {
            int intPosition;

            intPosition = Array.IndexOf(this.objConfig.arrAllowedImageExtensions, Path.GetExtension(strFilename).TrimStart('.'));
            return (intPosition > -1);  // if > -1, then it was found in the list of image file extensions
        } // isImageFile

        public bool isVideoFile(string strFilename)
        {
            int intPosition;

            intPosition = Array.IndexOf(this.objConfig.arrAllowedVideoExtensions, Path.GetExtension(strFilename).TrimStart('.'));
            return (intPosition > -1);  // if > -1, then it was found in the list of video file extensions
        } // isVideoFile

        public bool isMusicFile(string strFilename)
        {
            int intPosition;

            intPosition = Array.IndexOf(this.objConfig.arrAllowedMusicExtensions, Path.GetExtension(strFilename).TrimStart('.'));
            return (intPosition > -1);  // if > -1, then it was found in the list of music file extensions
        } // isMusicFile

        public bool isMiscFile(string strFilename)
        {
            int intPosition;

            intPosition = Array.IndexOf(this.objConfig.arrAllowedMiscExtensions, Path.GetExtension(strFilename).TrimStart('.'));
            return (intPosition > -1);  // if > -1, then it was found in the list of misc file extensions
        } // isMiscFile

        public string getEndOfLine(int intColNum)
        {
            if (intColNum == 6)
            {
                return "</div><div class=\"space10\"></div>";
            }
            else
            {
                return "";
            }
        } // getEndOfLine

        public string getStartOfLine(int intColNum)
        {
            if (intColNum == 1)
            {
                return "<div class=\"row-fluid\">";
            }
            else
            {
                return "";
            }
        } // getStartOfLine

        public int getNextColNum()
        {
            this.intColNum++;
            if (this.intColNum > 6)
            {
                this.intColNum = 1;
            }
            return this.intColNum;
        } // getNextColNum

        public string getUpOneDir(string strInput)
        {
            string[] arrTemp;

            arrTemp = strInput.TrimEnd('\\').Split('\\');
            arrTemp[arrTemp.Length - 1] = "";
            return String.Join("\\", arrTemp);
        }
    }
}