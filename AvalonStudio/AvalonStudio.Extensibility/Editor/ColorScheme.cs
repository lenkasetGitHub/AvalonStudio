﻿using AvalonStudio.Platforms;
using AvalonStudio.Utils;
using System.IO;
using Newtonsoft.Json;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia;
using AvalonStudio.Extensibility.Plugin;
using System;

namespace AvalonStudio.Extensibility.Editor
{
    public class DefaultColorSchemes : IExtension
    {
        public void Activation()
        {
            ColorScheme.Register(ColorScheme.Default);
            ColorScheme.Register(ColorScheme.SolarizedDark);
            ColorScheme.Register(ColorScheme.SolarizedLight);
        }

        public void BeforeActivation()
        {
        }
    }

    public class ColorScheme
    {
        private static List<ColorScheme> s_colorSchemes = new List<ColorScheme>();
        private static Dictionary<string, ColorScheme> s_colorSchemeIDs = new Dictionary<string, ColorScheme>();
        private static readonly ColorScheme DefaultColorScheme = ColorScheme.SolarizedLight;
        private static readonly Dictionary<string, Func<IBrush>> s_colorAccessors = new Dictionary<string, Func<IBrush>>();
        public static IEnumerable<ColorScheme> ColorSchemes => s_colorSchemes;

        static ColorScheme()
        {
            s_colorAccessors["background"] = () => CurrentColorScheme.Background;
            s_colorAccessors["background.accented"] = () => CurrentColorScheme.BackgroundAccent;
            s_colorAccessors["text"] = () => CurrentColorScheme.Text;
            s_colorAccessors["comment"] = () => CurrentColorScheme.Comment;
            s_colorAccessors["delegate.name"] = () => CurrentColorScheme.DelegateName;
            s_colorAccessors["keyword"] = () => CurrentColorScheme.Keyword;
            s_colorAccessors["literal"] = () => CurrentColorScheme.Literal;
            s_colorAccessors["identifier"] = () => CurrentColorScheme.Identifier;
            s_colorAccessors["callexpression"] = () => CurrentColorScheme.CallExpression;
            s_colorAccessors["numericliteral"] = () => CurrentColorScheme.NumericLiteral;
            s_colorAccessors["enumconst"] = () => CurrentColorScheme.EnumConstant;
            s_colorAccessors["enum"] = () => CurrentColorScheme.EnumType;
            s_colorAccessors["operator"] = () => CurrentColorScheme.Operator;
            s_colorAccessors["struct.name"] = () => CurrentColorScheme.StructName;
            s_colorAccessors["interface"] = () => CurrentColorScheme.InterfaceType;
            s_colorAccessors["punctuation"] = () => CurrentColorScheme.Punctuation;
            s_colorAccessors["type"] = () => CurrentColorScheme.Type;
            s_colorAccessors["xml.tag"] = () => CurrentColorScheme.XmlTag;
            s_colorAccessors["xml.property"] = () => CurrentColorScheme.XmlProperty;
            s_colorAccessors["xml.property.value"] = () => CurrentColorScheme.XmlPropertyValue;
            s_colorAccessors["xaml.markupextension"] = () => CurrentColorScheme.XamlMarkupExtension;
            s_colorAccessors["xaml.markupextension.property"] = () => CurrentColorScheme.XamlMarkupExtensionProperty;
            s_colorAccessors["xaml.markupextension.property.value"] = () => CurrentColorScheme.XamlMarkupExtensionPropertyValue;
        }

        public static void Register(ColorScheme colorScheme)
        {
            s_colorSchemes.Add(colorScheme);
            s_colorSchemeIDs.Add(colorScheme.Name, colorScheme);
        }

        public static readonly ColorScheme Default = new ColorScheme
        {
            Name = "Default",
            Background = Brush.Parse("#1e1e1e"),
            BackgroundAccent = Brush.Parse("#1e1e1e"),
            Text = Brush.Parse("#DCDCDC"),
            Comment = Brush.Parse("#57A64A"),
            Keyword = Brush.Parse("#569CD6"),
            Literal = Brush.Parse("#D69D85"),
            Identifier = Brush.Parse("#C8C8C8"),
            CallExpression = Brush.Parse("#DCDCAA"),
            EnumConstant = Brush.Parse("#B5CEA8"),
            InterfaceType = Brush.Parse("#B5CEA8"),
            EnumType = Brush.Parse("#B5CEA8"),
            NumericLiteral = Brush.Parse("#B5CEA8"),
            Punctuation = Brush.Parse("#808080"),
            Type = Brush.Parse("#4EC9B0"),
            StructName = Brush.Parse("#4EC9B0"),
            Operator = Brush.Parse("#B4B4B4"),
            DelegateName = Brush.Parse("#4EC9B0"),
            XmlTag = Brush.Parse("#DCDCDC"),
            XmlProperty = Brush.Parse("#90C7EA"),
            XmlPropertyValue = Brush.Parse("#569CD6"),
            XamlMarkupExtension = Brush.Parse("#BBA08C"),
            XamlMarkupExtensionProperty = Brush.Parse("#D4B765"),
            XamlMarkupExtensionPropertyValue = Brush.Parse("#B1B1B1")
        };

        public static readonly ColorScheme SolarizedDark = new ColorScheme {
            Name = "Solarized Dark",
            Background = Brush.Parse("#002b36"),
            BackgroundAccent = Brush.Parse("#073642"),
            Text = Brush.Parse("#839496"),
            Comment =Brush.Parse("#586e75"),
            Keyword = Brush.Parse("#859900"),
            Literal = Brush.Parse("#2aa198"),
            Identifier = Brush.Parse("#839496"),
            CallExpression = Brush.Parse("#268bd2"),
            EnumConstant = Brush.Parse("#b58900"),
            InterfaceType = Brush.Parse("#b58900"),
            EnumType = Brush.Parse("#b58900"),
            NumericLiteral = Brush.Parse("#2aa198"),
            Punctuation = Brush.Parse("#839496"),
            Type = Brush.Parse("#b58900"),
            StructName = Brush.Parse("Red"),
            Operator = Brush.Parse("Red")
        };

        public static readonly ColorScheme SolarizedLight = new ColorScheme
        {
            Name = "Solarized Light",
            Background = Brush.Parse("#fdf6e3"),
            BackgroundAccent = Brush.Parse("#eee8d5"),
            Text = Brush.Parse("#657b83"),
            Comment = Brush.Parse("#93a1a1"),
            Keyword = Brush.Parse("#859900"),
            Literal = Brush.Parse("#2aa198"),
            Identifier = Brush.Parse("#839496"),
            CallExpression = Brush.Parse("#268bd2"),
            EnumConstant = Brush.Parse("#b58900"),
            InterfaceType = Brush.Parse("#b58900"),
            EnumType = Brush.Parse("#b58900"),
            NumericLiteral = Brush.Parse("#2aa198"),
            Punctuation = Brush.Parse("#839496"),
            Type = Brush.Parse("#b58900"),
            StructName = Brush.Parse("Red"),
            Operator = Brush.Parse("Red"),
            XmlTag = Brush.Parse("DarkMagenta"),
            XmlProperty = Brush.Parse("Red"),
            XmlPropertyValue = Brush.Parse("Blue")
        };

        public static ColorScheme LoadColorScheme(string name)
        {
            if (!string.IsNullOrEmpty(name) && s_colorSchemeIDs.ContainsKey(name))
            {
                LoadColorScheme(s_colorSchemeIDs[name]);

                return s_colorSchemeIDs[name];
            }
            else
            {
                LoadColorScheme(Default);

                return Default;
            }
        }

        public static void LoadColorScheme(ColorScheme colorScheme)
        {
            Application.Current.Resources["EditorColorScheme"] = colorScheme;
            CurrentColorScheme = colorScheme;
        }

        public static ColorScheme CurrentColorScheme { get; private set; }

        public IBrush this[string key]
        {
            get
            {
                key = key.ToLower();

                if(s_colorAccessors.ContainsKey(key))
                {
                    return s_colorAccessors[key]();
                }

                return Brushes.Red;
            }
        }

        public string Name { get; private set; }

        public string Description { get; set; }

        [JsonProperty(PropertyName = "editor.background")]
        public IBrush Background { get; set; }

        [JsonProperty(PropertyName = "editor.background.accented")]
        public IBrush BackgroundAccent { get; set; }

        [JsonProperty(PropertyName = "editor.text")]
        public IBrush Text { get; set; }

        [JsonProperty(PropertyName ="editor.comment")]
        public IBrush Comment { get; set; }

        [JsonProperty(PropertyName = "editor.delegate.name")]
        public IBrush DelegateName { get; set; }

        [JsonProperty(PropertyName ="editor.keyword")]
        public IBrush Keyword { get; set; }

        [JsonProperty(PropertyName = "editor.literal")]
        public IBrush Literal { get; set; }

        [JsonProperty(PropertyName = "editor.identifier")]
        public IBrush Identifier { get; set; }

        [JsonProperty(PropertyName = "editor.callexpression")]
        public IBrush CallExpression { get; set; }

        [JsonProperty(PropertyName = "editor.numericliteral")]
        public IBrush NumericLiteral { get; set; }

        [JsonProperty(PropertyName = "editor.enumconst")]
        public IBrush EnumConstant { get; set; }

        [JsonProperty(PropertyName = "editor.enum")]
        public IBrush EnumType { get; set; }

        [JsonProperty(PropertyName = "editor.operator")]
        public IBrush Operator { get; set; }

        [JsonProperty(PropertyName = "editor.struct.name")]
        public IBrush StructName { get; set; }

        [JsonProperty(PropertyName = "editor.interface")]
        public IBrush InterfaceType { get; set; }

        [JsonProperty(PropertyName = "editor.punctuation")]
        public IBrush Punctuation { get; set; }

        [JsonProperty(PropertyName = "editor.type")]
        public IBrush Type { get; set; }

        [JsonProperty(PropertyName = "editor.xml.tag")]
        public IBrush XmlTag { get; set; }

        [JsonProperty(PropertyName = "editor.xml.property")]
        public IBrush XmlProperty { get; set; }

        [JsonProperty(PropertyName = "editor.xml.property.value")]
        public IBrush XmlPropertyValue { get; set; }

        [JsonProperty(PropertyName = "editor.xaml.markupext")]
        public IBrush XamlMarkupExtension { get; set; }

        [JsonProperty(PropertyName = "editor.xaml.markupext.property")]
        public IBrush XamlMarkupExtensionProperty { get; set; }

        [JsonProperty(PropertyName = "editor.xaml.markupext.property.value")]
        public IBrush XamlMarkupExtensionPropertyValue { get; set; }

        public void Save(string fileName)
        {
            SerializedObject.Serialize(Path.Combine(Platform.SettingsDirectory, fileName), this);
        }

        public static ColorScheme Load (string fileName)
        {
            return SerializedObject.Deserialize<ColorScheme>(fileName);
        }
    }
}