using wpf.ui.model.diagrams.electric;
using System.Windows;
using System.Windows.Controls;

namespace wpf.ui.converters
{
    public class RailPotentialTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? OpenTemplate { get; set; }
        public DataTemplate? CloseTemplate { get; set; }
        public DataTemplate? FaultTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            // 根据站点状态返回对应的模板
            if (item is RailPotentialItem station)
            {
                return station.RailPotentialState switch
                {
                    RailPotentialState.Open => OpenTemplate,
                    RailPotentialState.Close => CloseTemplate,
                    RailPotentialState.Fault => FaultTemplate,
                    _ => base.SelectTemplate(item, container)
                };
            }
            return base.SelectTemplate(item, container);
        }
    }
}
