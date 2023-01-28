using System.Windows.Controls;
using Commands.Use_Case;
using DiagramsElementsLibrary.Use_Case;

namespace Client.Services.Figure;

/// <summary>
/// Class FigureService.
/// </summary>
public class FigureService
{
    /// <summary>
    /// Draws the shapes.
    /// </summary>
    /// <param name="diagram">The diagram.</param>
    /// <param name="ImgDiagram">The img diagram.</param>
    public void DrawShapes(Diagram? diagram, Panel ImgDiagram)
    {
        if (diagram?.Elements == null) 
            return;

        foreach (var element in diagram.Elements)
        {
            if (element?.GetType() == typeof(Precedent))
            {
                (new AddPrecedent()).Draw(element, ImgDiagram, diagram.Elements.Count);
            }
            else if (element?.GetType() == typeof(Actor))
            {
                (new AddActor()).Draw(element, ImgDiagram, 0);
            }
            else if (element?.GetType() == typeof(Relation))
            {
                (new AddRelation()).Draw(element, ImgDiagram, 0);
            }
        }
    }
}