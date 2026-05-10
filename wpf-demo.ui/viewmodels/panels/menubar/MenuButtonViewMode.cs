using Logging;
using System;
using UserComponent.Core.menu;

namespace wpf.ui.viewmodels.panels.menubar
{
    public class MenuButtonViewMode : MenuButtonBaseVm
    {
        public int Page { get; init; } = 1;
        public MenuButtonViewMode(string id, string content) : base(id, content)
        {
        }

        protected override void AdjustContentFontSize()
        {
            try
            {
                if (Content == null)
                {
                    return;
                }

                if (Content.Contains("\n"))
                {
                    ContentFontSize = 16;
                    Content = Content.TrimEnd('\n');
                }
                else
                {
                    ContentFontSize = 20;
                }
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex);
            }
        }

    }
}
