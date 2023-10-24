using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace rusiVSlizards.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ISeries[] Series { get; set; } = new ISeries[]
    {
        new LineSeries<double>
        {
            Values = new double[] {0, 4, 10, 12, 8, 2, -2},
            Fill = new SolidColorPaint(new SKColor(0, 90, 120)),
            Stroke = new SolidColorPaint(new SKColor(120, 152, 203)),
            LineSmoothness = 50
        }
    };
}