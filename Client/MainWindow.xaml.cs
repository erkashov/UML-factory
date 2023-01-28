using Commands.Services.Use_Case;
using Commands.Use_Case;
using DiagramsElementsLibrary.Save;
using DiagramsElementsLibrary.Use_Case;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows;
using Client.Services.Figure;
using Client.Services.File;

namespace Client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// The separator
    /// </summary>
    private const string Separator = "\r\n";

    /// <summary>
    /// The diagram
    /// </summary>
    private Diagram? _diagram = new() { Elements = new List<IElement?>()};

    /// <summary>
    /// The figure service
    /// </summary>
    private readonly FigureService _figureService;

    /// <summary>
    /// The json service
    /// </summary>
    private readonly JsonService _jsonService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    /// <param name="figureService">The figure service.</param>
    /// <param name="jsonService">The json service.</param>
    public MainWindow(FigureService figureService, JsonService jsonService)
    {
        _figureService = figureService;
        _jsonService = jsonService;
        InitializeComponent();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Strings the format rich text box.
    /// </summary>
    /// <param name="richTextBox">The rich text box.</param>
    /// <returns>System.String.</returns>
    private static string StringFormatRichTextBox(RichTextBox richTextBox)
    {
        var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

        return textRange.Text;
    }

    /// <summary>
    /// Handles the KeyDown event of the TbConsole control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
    private async void TbConsole_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.F1:
                {
                    DrawFigures();
                    break;
                }
            case Key.F2:
                {
                    SaveImages();
                    break;
                }
            case Key.F3:
                {
                    await _jsonService.SaveJSON(_diagram, ImgDiagram);
                    break;
                }
            case Key.F4:
                {
                    await _jsonService.OpenJSON(_diagram, ImgDiagram);
                    break;
                }
        }
    }

    /// <summary>
    /// Saves the images.
    /// </summary>
    private void SaveImages()
    {
        var saves = new Microsoft.Win32.SaveFileDialog
        {
            DefaultExt = ".PNG",
            Filter = "Image (.PNG)|*.PNG"
        };
        if (saves.ShowDialog() == true)
        {
            SaveImage.ToImageSource(ImgDiagram, saves.FileName);
        }
    }

    /// <summary>
    /// Draws the figures.
    /// </summary>
    private void DrawFigures()
    {
        ClearWhenRestarting();
        var commandSet = StringFormatRichTextBox(TbConsole).Split(Separator);

        foreach (var command in commandSet)
        {
            var regex = new Regex(@".+\+.+");
            var matchCollection = regex.Matches(command);

            _diagram?.Elements?.Add(matchCollection.Count == 0
                ? AddCommandService.AddCommandAction(command)
                : AddRelationService.AddRelationAction(command, _diagram));
        }

        DrawShapes();
    }

    /// <summary>
    /// Clears the when restarting.
    /// </summary>
    private void ClearWhenRestarting()
    {
        ImgDiagram.Children.Clear();
        _diagram?.Elements?.Clear();
        Precedent.Count = 0;
        Actor.Count = 0;
    }

    /// <summary>
    /// Draws the shapes.
    /// </summary>
    private void DrawShapes()
    {
        if (_diagram?.Elements == null) return;
        foreach (var element in _diagram.Elements)
        {
            if (element?.GetType() == typeof(Precedent))
            {
                (new AddPrecedent()).Draw(element, ImgDiagram, _diagram.Elements.Count - Actor.Count);
            }
            else if (element?.GetType() == typeof(Actor))
            {
                (new AddActor()).Draw(element, ImgDiagram, _diagram.Elements.Count - Precedent.Count);
            }
            else if (element?.GetType() == typeof(Relation))
            {
                (new AddRelation()).Draw(element, ImgDiagram, _diagram.Elements.Count);
            }            
        }
    }
}