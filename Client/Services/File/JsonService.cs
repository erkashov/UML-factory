using Commands.Use_Case;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using Client.Services.Figure;

namespace Client.Services.File;

public class JsonService
{
    private readonly FigureService _figureService;

    public JsonService()
    {

    }

    public JsonService(FigureService figureService)
    {
        _figureService = figureService;
    }

    public async Task OpenJSON(Diagram? diagram, Panel ImgDiagram)
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
                diagram = await JsonSerializer.DeserializeAsync<Diagram>(fileStream);

                _figureService.DrawShapes(diagram, ImgDiagram);
            }
        }
    }

    public async Task SaveJSON(Diagram? diagram, Panel ImgDiagram)
    {
        var saves = new Microsoft.Win32.SaveFileDialog
        {
            DefaultExt = ".JSON",
            Filter = "JSON (.JSON)|*.JSON"
        };
        if (saves.ShowDialog() == true)
        {
            using (var fileStream = new FileStream(saves.FileName, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fileStream, diagram.Elements);
            }
        }
    }
}