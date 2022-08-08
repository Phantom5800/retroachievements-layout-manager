﻿using CefSharp;
using CefSharp.Web;
using Retro_Achievement_Tracker.Controllers;
using Retro_Achievement_Tracker.Models;
using Retro_Achievement_Tracker.Properties;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Retro_Achievement_Tracker
{
    public partial class UserStatsWindow : Form
    {
        private TaskController TaskController;
        public UserStatsWindow()
        {
            ClientSize = new Size(0, 0);

            Name = "RA Tracker - User Stats";
            Text = "RA Tracker - User Stats";

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));

            Shown += UserStatsWindow_Shown;
            FormClosed += UserStatsWindow_FormClosed;

            TaskController = new TaskController();

            SetupBrowser();
        }

        private void UserStatsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserInfoController.IsOpen = false;
        }

        private void UserStatsWindow_Shown(object sender, EventArgs e)
        {
            UserInfoController.IsOpen = true;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
        public async void AssignJavaScriptVariables()
        {

            await TaskController.Enqueue(() => ExecuteScript(
                "container = document.getElementById(\"container\");" +
                "rankName = document.getElementById(\"rank-name\");" +
                "rankValue = document.getElementById(\"rank-value\");" +
                "ratioName = document.getElementById(\"ratio-name\");" +
                "ratioValue = document.getElementById(\"ratio-value\");" +
                "pointsName = document.getElementById(\"points-name\");" +
                "pointsValue = document.getElementById(\"points-value\");" +
                "truePointsName = document.getElementById(\"true-points-name\");" +
                "truePointsValue = document.getElementById(\"true-points-value\");" +
                "allElements = document.getElementsByClassName(\"has-font\");" +
                "allNames = document.getElementsByClassName(\"name\");" +
                "allValues = document.getElementsByClassName(\"value\");"));
        }
        public async void SetWindowBackgroundColor(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("container.style.backgroundColor = \"" + value + "\";"));
        }
        public async void SetSimpleFontFamily(FontFamily value)
        {
            int lineSpacing = value.GetLineSpacing(FontStyle.Regular) / value.GetEmHeight(FontStyle.Regular);
            await TaskController.Enqueue(() => ExecuteScript(
                "for (var i = 0; i < allElements.length; i++) { " +
                "   allElements[i].style.lineHeight = " + (lineSpacing == 0 ? 1 : lineSpacing) + ";" +
                "   allElements[i].style.fontFamily = \"" + value.Name.Replace(":", "\\:") + "\";" +
                "}"));
            await TaskController.Enqueue(() => ExecuteScript(
                "for (var i = 0; i < allNames.length; i++) { " +
                "   textFit(allNames[i], { alignVert: true, reProcess: true });" +
                "}"));
            await TaskController.Enqueue(() => ExecuteScript(
                "for (var i = 0; i < allValues.length; i++) { " +
                "   textFit(allValues[i], { alignVert: true, reProcess: true });" +
                "}"));
        }
        public async void SetSimpleFontColor(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("for (var i = 0; i < allElements.length; i++) { allElements[i].style.color = \"" + value + "\"; }"));
        }

        public async void SetSimpleFontOutline(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("for (var i = 0; i < allElements.length; i++) { allElements[i].style.webkitTextStroke = \"" + value + "\"; }"));
        }

        public async void SetNameFontFamily(FontFamily value)
        {
            int lineSpacing = value.GetLineSpacing(FontStyle.Regular) / value.GetEmHeight(FontStyle.Regular);
            await TaskController.Enqueue(() => ExecuteScript(
                "for (var i = 0; i < allNames.length; i++) { " +
                "   allNames[i].style.lineHeight = " + (lineSpacing == 0 ? 1 : lineSpacing) + ";" +
                "   allNames[i].style.fontFamily = \"" + value.Name.Replace(":", "\\:") + "\";" +
                "   textFit(allNames[i], { alignVert: true, reProcess: true });" +
                "}"));
        }

        public async void SetValueFontFamily(FontFamily value)
        {
            int lineSpacing = value.GetLineSpacing(FontStyle.Regular) / value.GetEmHeight(FontStyle.Regular);
            await TaskController.Enqueue(() => ExecuteScript(
                "for (var i = 0; i < allValues.length; i++) { " +
                "   allValues[i].style.lineHeight = " + (lineSpacing == 0 ? 1 : lineSpacing) + ";" +
                "   allValues[i].style.fontFamily = \"" + value.Name.Replace(":", "\\:") + "\";" +
                "   textFit(allValues[i], { alignVert: true, reProcess: true });" +
                "}"));
        }

        public async void SetNameColor(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript(
                 "for (var i = 0; i < allNames.length; i++) { allNames[i].style.color = \"" + value + "\"; }"));
        }

        public async void SetValueColor(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript(
                 "for (var i = 0; i < allValues.length; i++) { allValues[i].style.color = \"" + value + "\"; }"));
        }

        public async void SetNameOutline(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript(
                  "for (var i = 0; i < allNames.length; i++) { allNames[i].style.webkitTextStroke = \"" + value + "\"; }"));
        }

        public async void SetValueOutline(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript(
                  "for (var i = 0; i < allValues.length; i++) { allValues[i].style.webkitTextStroke = \"" + value + "\"; }"));
        }
        public async void SetRankName(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("rankName.innerHTML = \"" + value + ":\";" +
                "textFit(rankName, { alignVert: true, reProcess: true });"));
        }
        public async void SetRankValue(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("rankValue.innerHTML = \"" + value + "\";" +
                "textFit(rankValue, { alignVert: true, reProcess: true });"));
        }
        public async void SetRankVisibility(bool isVisible)
        {
            await TaskController.Enqueue(() => ExecuteScript(isVisible ? "$(\"#rank\").fadeIn();" : "$(\"#rank\").fadeOut();"));
        }
        //Points
        public async void SetPointsName(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("pointsName.innerHTML = \"" + value + ":\";" +
                "textFit(pointsName, { alignVert: true, reProcess: true });"));
        }
        public async void SetPointsValue(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("pointsValue.innerHTML = \"" + value + "\";" +
                "textFit(pointsValue, { alignVert: true, reProcess: true });"));
        }
        public async void SetPointsVisibility(bool isVisible)
        {
            await TaskController.Enqueue(() => ExecuteScript(isVisible ? "$(\"#points\").fadeIn();" : "$(\"#points\").fadeOut();"));
        }
        //True Points
        public async void SetTruePointsName(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("truePointsName.innerHTML = \"" + value + ":\";" +
                "textFit(truePointsName, { alignVert: true, reProcess: true });"));
        }
        public async void SetTruePointsValue(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("truePointsValue.innerHTML = \"" + value + "\";" +
                "textFit(truePointsValue, { alignVert: true, reProcess: true });"));
        }
        public async void SetTruePointsVisibility(bool isVisible)
        {
            await TaskController.Enqueue(() => ExecuteScript(isVisible ? "$(\"#true-points\").fadeIn();" : "$(\"#true-points\").fadeOut();"));
        }
        //Ratio
        public async void SetRatioName(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("ratioName.innerHTML = \"" + value + ":\";" +
                "textFit(ratioName, { alignVert: true, reProcess: true });"));
        }
        public async void SetRatioValue(string value)
        {
            await TaskController.Enqueue(() => ExecuteScript("ratioValue.innerHTML = \"" + value + "\";" +
                "textFit(ratioValue, { alignVert: true, reProcess: true });"));
        }
        public async void SetRatioVisibility(bool isVisible)
        {
            await TaskController.Enqueue(() => ExecuteScript(isVisible ? "$(\"#ratio\").fadeIn();" : "$(\"#ratio\").fadeOut();"));
        }
        protected async Task ExecuteScript(string script)
        {
            if (chromiumWebBrowser != null)
            {
                try
                {
                    await chromiumWebBrowser.EvaluateScriptAsync(script, TimeSpan.FromSeconds(5));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
        public void SetupBrowser()
        {
            chromiumWebBrowser = new CefSharp.WinForms.ChromiumWebBrowser(new HtmlString(Resources.UserStatsWindow))
            {
                ActivateBrowserOnCreation = false,
                Location = new Point(0, 0),
                Name = "chromiumWebBrowser",
                Size = new Size(805, 300),
                TabIndex = 0,
                Dock = DockStyle.None,
                RequestHandler = new CustomRequestHandler()
            };

            chromiumWebBrowser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>((sender, frameLoadEndEventArgs) =>
            {
                Invoke((MethodInvoker)delegate
                {
                    ClientSize = new Size(805, 290);
                });

                UserInfoController.Instance.SetAllSettings();
            });

            chromiumWebBrowser.LoadHtml(Resources.UserStatsWindow);

            Controls.Add(chromiumWebBrowser);
        }

        public CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser;
    }
}