using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebAssistant.Bll
{
    public class OperateBll
    {
        private WebBrowser myWebBrowser;

        public OperateBll(WebBrowser webBrowser)
        {
            myWebBrowser = webBrowser;
        }

        #region 设置值
        public void SetValue(HtmlDocument htmlDocument, string key, string value, bool isName)
        {
            foreach (HtmlElement htmlElement in htmlDocument.All)
            {
                if (isName)
                {
                    if (htmlElement.Name == key)
                    {
                        htmlElement.SetAttribute("value", value);
                    }
                }
                else
                {
                    if (htmlElement.Id == key)
                    {
                        htmlElement.SetAttribute("value", value);
                    }
                }
            }
        }

        public void SetValue(string key, string value, bool isName)
        {
            try
            {
                SetValue(myWebBrowser.Document, key, value, isName);

                foreach (HtmlWindow HtmlWindow in myWebBrowser.Document.Window.Frames)
                {
                    SetValue(HtmlWindow.Document, key, value, isName);

                    foreach (HtmlWindow HtmlWindow1 in HtmlWindow.Document.Window.Frames)
                    {
                        SetValue(HtmlWindow1.Document, key, value, isName);
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region 勾选
        private void CheckByName(HtmlDocument htmlDocument, string key, bool isName)
        {
            foreach (HtmlElement htmlElement in htmlDocument.All)
            {
                if (isName)
                {
                    if (htmlElement.Name == key)
                    {
                        htmlElement.SetAttribute("checked", "checked");
                    }
                }
                else
                {
                    if (htmlElement.Id == key)
                    {
                        htmlElement.SetAttribute("checked", "checked");
                    }
                }
            }
        }

        public void Check(string key, bool isName)
        {
            try
            {
                CheckByName(myWebBrowser.Document, key, isName);

                foreach (HtmlWindow htmlWindow in myWebBrowser.Document.Window.Frames)
                {
                    CheckByName(htmlWindow.Document, key, isName);

                    foreach (HtmlWindow htmlWindow1 in htmlWindow.Document.Window.Frames)
                    {
                        CheckByName(htmlWindow1.Document, key, isName);
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region 设置取款密码
        private void SetMoneyPasswordSelectValue(HtmlDocument htmlDocument, string key, bool isName)
        {
            Random random = new Random();

            foreach (HtmlElement htmlElement in htmlDocument.All)
            {
                string randomNumber = random.Next(10).ToString();

                if (isName)
                {
                    if (htmlElement.Name == key)
                    {
                        HtmlElementCollection options = htmlElement.GetElementsByTagName("option");

                        foreach (HtmlElement option in options)
                        {
                            string optionValue = option.InnerText;

                            if (optionValue.IndexOf(randomNumber) != -1)
                            {
                                option.SetAttribute("selected", "selected");
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (htmlElement.Id == key)
                    {
                        HtmlElementCollection options = htmlElement.GetElementsByTagName("option");

                        foreach (HtmlElement option in options)
                        {
                            string optionValue = option.InnerText;

                            if (optionValue.IndexOf(randomNumber) != -1)
                            {
                                option.SetAttribute("selected", "selected");
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void SetMoneyPasswordSelectValue(string key, bool isName)
        {
            try
            {
                SetMoneyPasswordSelectValue(myWebBrowser.Document, key, isName);

                foreach (HtmlWindow htmlWindow in myWebBrowser.Document.Window.Frames)
                {
                    SetMoneyPasswordSelectValue(htmlWindow.Document, key, isName);

                    foreach (HtmlWindow htmlWindow1 in htmlWindow.Frames)
                    {
                        SetMoneyPasswordSelectValue(htmlWindow1.Document, key, isName);
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        public HtmlElement GetHtmlElementByName(string name)
        {
            HtmlElement htmlElement = myWebBrowser.Document.All[name];
            return htmlElement;
        }

        public HtmlElement GetHtmlElementById(string id)
        {
            HtmlElement htmlElement = null;

            foreach (HtmlElement temphtmlElement in myWebBrowser.Document.All)
            {
                if(temphtmlElement.Id == id)
                {
                    htmlElement = temphtmlElement;
                }
            }

            return htmlElement;
        }

        public void SetSelectValue(string name, string value)
        {
            HtmlElement rootHtmlElement = myWebBrowser.Document.All[name];
            HtmlElementCollection options = rootHtmlElement.GetElementsByTagName("option");

            foreach (HtmlElement option in options)
            {
                string optionValue = option.InnerText;

                if (optionValue.IndexOf(value) >= 0)
                {
                    option.SetAttribute("selected", "selected");
                }
            }
        }

        public void SetBodyValue(string value)
        {
            foreach (HtmlWindow htmlWindow in myWebBrowser.Document.Window.Frames)
            {
                HtmlElementCollection htmlElementCollection = htmlWindow.Document.GetElementsByTagName("body");

                if (htmlElementCollection != null)
                {
                    foreach (HtmlElement htmlElement in htmlElementCollection)
                    {
                        htmlElement.InnerHtml = value;
                    }
                }
            }
        }

        public void ClickByName(string name)
        {
            myWebBrowser.Document.All[name].InvokeMember("click");
        }
    }
}
