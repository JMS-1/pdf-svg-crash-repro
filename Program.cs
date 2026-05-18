using System.Xml;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using SkiaSharp;
using Svg.Skia;

internal class Program
{
    private class PdfSharpFontResolver : IFontResolver
    {
        public byte[]? GetFont(string faceName)
            => File.ReadAllBytes(Path.Combine(
                Path.GetDirectoryName(typeof(PdfSharpFontResolver).Assembly.Location)!,
                "PdfSharpAssets",
                "fonts",
                $"{faceName}.ttf"
            ));

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
            => new(familyName, isBold, isItalic);
    }

    private static void Main(string[] args)
    {
        GlobalFontSettings.FontResolver = new PdfSharpFontResolver();

        var document = new Document();

        document.Styles["Normal"]?.Font.Name = "Verdana";

        var section = document.AddSection();
        var paragraph = section.AddParagraph();

        using var reader = new XmlNodeReader(GetSvg());
        using var loader = new SKSvg();

        var pic = loader.Load(reader);

        if (pic == null) return;

        using var svgTemp = new MemoryStream();

        if (!loader.Save(
            svgTemp,
            SKColors.Transparent,
            SKEncodedImageFormat.Png,
            100,
            1,
            1
        )) return;


        paragraph.Format.Alignment = ParagraphAlignment.Center;
        paragraph.AddImage($"base64:{Convert.ToBase64String(svgTemp.GetBuffer())}");

        var pdfRenderer = new PdfDocumentRenderer { Document = document };

        pdfRenderer.RenderDocument();

        using var file = new FileStream("some.pdf", FileMode.Create);

        pdfRenderer.PdfDocument.Save(file);
        pdfRenderer.PdfDocument.Close();

        Console.WriteLine("OK");

        while (true) Thread.Sleep(1000);
    }

    private static XmlDocument GetSvg()
    {
        const string svg = @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <svg viewBox=""-10,-10,510,510"">
                <defs>
                    <marker id=""head_v0"" orient=""auto"" markerWidth=""3"" markerHeight=""4"" refX=""1.8"" refY=""2"">
                        <path d=""M0,0 V4 L2,2 Z"" fill=""#ff0000"" />
                    </marker>
                    <marker id=""head_v1"" orient=""auto"" markerWidth=""3"" markerHeight=""4"" refX=""1.8"" refY=""2"">
                        <path d=""M0,0 V4 L2,2 Z"" fill=""#daa520"" />
                    </marker>
                    <marker id=""head_v2"" orient=""auto"" markerWidth=""3"" markerHeight=""4"" refX=""1.8"" refY=""2"">
                        <path d=""M0,0 V4 L2,2 Z"" fill=""#0000ff"" />
                    </marker>
                    <marker id=""head_c0"" orient=""auto"" markerWidth=""3"" markerHeight=""4"" refX=""1.8"" refY=""2"">
                        <path d=""M0,0 V4 L2,2 Z"" fill=""#ff6347"" />
                    </marker>
                    <marker id=""head_c1"" orient=""auto"" markerWidth=""3"" markerHeight=""4"" refX=""1.8"" refY=""2"">
                        <path d=""M0,0 V4 L2,2 Z"" fill=""#ffd700"" />
                    </marker>
                    <marker id=""head_c2"" orient=""auto"" markerWidth=""3"" markerHeight=""4"" refX=""1.8"" refY=""2"">
                        <path d=""M0,0 V4 L2,2 Z"" fill=""#87ceeb"" />
                    </marker>
                </defs>
                <circle cx=""239.58333333333331"" cy=""239.58333333333331"" r=""208.33333333333334"" stroke-width=""2px"" stroke=""gray"" fill=""transparent"" />
                <line x1=""0"" y1=""239.58333333333331"" x2=""479.16666666666663"" y2=""239.58333333333331"" stroke=""gray"" stroke-width=""2px"" />
                <line x1=""239.58333333333331"" y1=""0"" x2=""239.58333333333331"" y2=""479.16666666666663"" stroke=""gray"" stroke-width=""2px"" />
                <g stroke-width=""4"" marker-end=""url(#head_c0)"">
                    <g>
                        <path d=""M 239.58333333333331, 239.58333333333331, 447.9009588167735, 239.58333333333331"" stroke=""#ff6347"" />
                        <text x=""458.75"" y=""224.58333333333331"" fill=""#ff6347"">I L1</text>
                    </g>
                </g>
                <g stroke-width=""4"" marker-end=""url(#head_c1)"">
                    <g>
                        <path d=""M 239.58333333333331, 239.58333333333331, 135.0719174016013, 419.78773982532675"" stroke=""#ffd700"" />
                        <text x=""101.63645747509518"" y=""430.29766963039236"" fill=""#ffd700"">I L2</text>
                    </g>
                </g>
                <g stroke-width=""4"" marker-end=""url(#head_c2)"">
                    <g>
                        <path d=""M 239.58333333333331, 239.58333333333331, 135.50957183936566, 59.107767268908646"" stroke=""#87ceeb"" />
                        <text x=""102.10795493333032"" y=""48.55352149003182"" fill=""#87ceeb"">I L3</text>
                    </g>
                </g>
                <g stroke-width=""2"" marker-end=""url(#head_v0)"">
                    <g>
                        <path d=""M 239.58333333333331, 239.58333333333331, 447.8653342882385, 239.81506446185517"" stroke=""#ff0000"" />
                        <text x=""458.7331694210938"" y=""254.83829095581137"" fill=""#ff0000"">U L1</text>
                    </g>
                </g>
                <g stroke-width=""2"" marker-end=""url(#head_v1)"">
                    <g>
                        <path d=""M 239.58333333333331, 239.58333333333331, 135.12287479793466, 419.8353524076062"" stroke=""#daa520"" />
                        <text x=""127.65497431774243"" y=""445.3817073295822"" fill=""#daa520"">U L2</text>
                    </g>
                </g>
                <g stroke-width=""2"" marker-end=""url(#head_v2)"">
                    <g>
                        <path d=""M 239.58333333333331, 239.58333333333331, 135.87004599392293, 59.026173058320126"" stroke=""#0000ff"" />
                        <text x=""128.4458723400043"" y=""33.39513023981496"" fill=""#0000ff"">U L3</text>
                    </g>
                </g>
            </svg>";

        var doc = new XmlDocument();

        doc.LoadXml(svg);

        return doc;
    }
}