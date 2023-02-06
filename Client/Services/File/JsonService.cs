using Commands.Use_Case;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using Client.Services.Figure;

namespace Client.Services.File;

/// <summary>
/// Interface IFileService
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Opens the json.
    /// </summary>
    /// <param name="diagram">The diagram.</param>
    /// <param name="imgDiagram">The img diagram.</param>
    /// <returns>Task.</returns>
    Task OpenJSON(Diagram? diagram, Panel imgDiagram);

    /// <summary>
    /// Saves the json.
    /// </summary>
    /// <param name="diagram">The diagram.</param>
    /// <param name="imgDiagram">The img diagram.</param>
    /// <returns>Task.</returns>
    Task SaveJSON(Diagram? diagram, Panel imgDiagram);
}

/// <summary>
/// Class JsonService.
/// </summary>
public class JsonService : IFileService
{
    /// <summary>
    /// The figure service
    /// </summary>
    private readonly FigureService _figureService;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonService" /> class.
    /// </summary>
    public JsonService()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonService" /> class.
    /// </summary>
    /// <param name="figureService">The figure service.</param>
    public JsonService(FigureService figureService)
    {
        _figureService = figureService;
    }

    /// <summary>
    /// Opens the json.
    /// </summary>
    /// <param name="diagram">The diagram.</param>
    /// <param name="imgDiagram">The img diagram.</param>
    public async Task OpenJSON(Diagram? diagram, Panel imgDiagram)
    {
        var saves = new Microsoft.Win32.OpenFileDialog()
        {
            DefaultExt = ".JSON",
            Filter = "JSON (.JSON)|*.JSON"
        };
        if (saves.ShowDialog() == true)
        {
            using (var fileStream = new FileStream(saves.FileName, FileMode.OpenOrCreate))
            {
                //todo: Исправить исключение при загрузке JSON
                diagram = await JsonSerializer.DeserializeAsync<Diagram>(fileStream);

                _figureService.DrawShapes(diagram, imgDiagram);
            }
        }
    }

    /// <summary>
    /// Saves the json.
    /// </summary>
    /// <param name="diagram">The diagram.</param>
    /// <param name="imgDiagram">The img diagram.</param>
    public async Task SaveJSON(Diagram? diagram, Panel imgDiagram)
    {
        var saves = new Microsoft.Win32.SaveFileDialog
        {
            DefaultExt = ".JSON",
            Filter = "JSON (.JSON)|*.JSON"
        };
        if (saves.ShowDialog() == true)
        {
            await using (var fileStream = new FileStream(saves.FileName, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fileStream, diagram?.Elements);
            }
        }
    }
}