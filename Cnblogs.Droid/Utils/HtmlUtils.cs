using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Java.Util.Regex;
using System.Text.RegularExpressions;

namespace Cnblogs.Droid.Utils
{
    public class HtmlUtils
    {
        public static string ReadHtml(Android.Content.Res.AssetManager assets)
        {
            var body = "";
            using (var stream = assets.Open("content.html"))
            {
                StreamReader sr = new StreamReader(stream);
                body = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            return body;
        }
        public static string ReplaceHtml(string body)
        {
            if (body == null)
                return "";
            body = body.Replace("\\r\\n", @"
").Replace("\\n", @"
").Replace("\\t", "&nbsp;&nbsp;").Replace("\\u0004", "").Replace("\\", "");
            return body;
        }
        public static bool IsImgUrl(string url)
        {
            switch (url.Substring(url.LastIndexOf('.')).ToLower())
            {
                case "jpg":
                    return true;
                case "jpeg":
                    return true;
                case "gif":
                    return true;
                case "png":
                    return true;
                case "bmp":
                    return true;
            }
            return false;
        }
        public static string GetScoreName(int score)
        {
            if (score > 100000)
            {
                return "��ţ�ż�";
            }
            if (score > 50000)
            {
                return "ţ�˰˼�";
            }
            if (score > 20000)
            {
                return "�����߼�";
            }
            if (score > 10000)
            {
                return "ר������";
            }
            if (score > 5000)
            {
                return "�����弶";
            }
            if (score > 2000)
            {
                return "�����ļ�";
            }
            if (score > 500)
            {
                return "СϺ����";
            }
            if (score > 200)
            {
                return "��ѧһ��";
            }
            return "��ѧһ��";
        }

    }
}