using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Commands.Use_Case;

namespace DiagramsElementsLibrary.Use_Case;

/// <summary>
/// Class AddSystemBoundary.
/// Implements the <see cref="DiagramsElementsLibrary.IFigure" />
/// </summary>
/// <seealso cref="DiagramsElementsLibrary.IFigure" />
public class AddSystemBoundary : IFigure
{
    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    /// <value>The x.</value>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    /// <value>The y.</value>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the w.
    /// </summary>
    /// <value>The w.</value>
    public double W { get; set; }

    /// <summary>
    /// Gets or sets the h.
    /// </summary>
    /// <value>The h.</value>
    public double H { get; set; }

    /// <summary>
    /// Draws this instance.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="panel">The panel.</param>
    /// <param name="numberOfElements">The number of elements.</param>
    /// <returns>StackPanel.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Draw(IElement element, Panel panel, int numberOfElements)
    {
        var canvas = new Canvas();
        panel.Children.Add(canvas);

        var rectangle = new Rectangle()
        {
            Height = ((element as SystemBoundary)!).H,
            Width = ((element as SystemBoundary)!).W,
            Stroke = Brushes.Black
        };

        canvas.Children.Add(rectangle);

        Canvas.SetLeft(canvas.Children[0], element.X);
        Canvas.SetTop(canvas.Children[0], element.Y);
    }
}