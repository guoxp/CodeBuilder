﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeBuilder.WinForm.UI.OptionsPages
{
    using Util;
    using Configuration;

    public partial class TraceOptionsPage : BaseOptionsPage
    {
        private static Logger logger = InternalTrace.GetLogger(typeof(TraceOptionsPage));

        public TraceOptionsPage()
        {
            InitializeComponent();
        }

        public TraceOptionsPage(string key)
            : base(key)
        {
            InitializeComponent();
        }

        public override void LoadSettings()
        {
            this.isLoaded = true;
            traceLevelCombox.Text = ConfigManager.OptionSection.Options["Options.InternalTraceLevel"].Value;
            logDirectoryLabel.Text = ConfigManager.LogDirectory;
        }

        public override void ApplySettings()
        {
            try
            {
                ConfigManager.OptionSection.Options["Options.InternalTraceLevel"].Value = traceLevelCombox.Text;
                ConfigManager.Save();
                ConfigManager.RefreshOptions();
            }
            catch (Exception ex)
            {
                logger.Error("Save Options.InternalTraceLevel", ex);
                return;
            }

            InternalTraceLevel level = (InternalTraceLevel)Enum.Parse(InternalTraceLevel.Default.GetType(), traceLevelCombox.Text, true);
            InternalTrace.ReInitialize("CodeBuilder_%p.log", level);
        }
    }
}
