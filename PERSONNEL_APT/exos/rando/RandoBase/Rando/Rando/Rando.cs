using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using Aspose.Gis;
using Aspose.Gis.Geometries;

namespace Rando
{
    public partial class Rando : Form
    {
        public Rando()
        {
            InitializeComponent();
        }

        // Classe Coord pour stocker les coordonnées
        public class Coord
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Coord(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        // Fonction Paint pour dessiner les lignes
        private void Rando_Form_Paint(object sender, PaintEventArgs e)
        {
            string path = @"C:\Users\pn01kbc\Documents\GitHub\323-Programmation_fonctionnelle\PERSONNEL_APT\exos\rando\gpx\Running.gpx";
            Pen myPen = new Pen(Color.Red, 2);

            // Liste pour stocker les points GPX
            List<Coord> tab = new List<Coord>();
            GetPointsFromGPX(path, tab);

            if (tab.Count == 0)
            {
                Debug.WriteLine("Aucun point trouvé dans le GPX.");
                return;
            }

            // Conversion en System.Drawing.Point pour DrawLines
            System.Drawing.Point[] points = tab
                .Select(c => new System.Drawing.Point(Convert.ToInt32(c.X), Convert.ToInt32(c.Y)))
                .ToArray();

            // Dessin des lignes
            e.Graphics.DrawLines(myPen, points);
        }

        // Fonction pour lire les points GPX
        private void GetPointsFromGPX(string path, List<Coord> tab)
        {
            if (!File.Exists(path))
            {
                Debug.WriteLine("Fichier GPX introuvable : " + path);
                return;
            }

            var layer = Drivers.Gpx.OpenLayer(path);

            foreach (var feature in layer)
            {
                var geomType = feature.Geometry.GeometryType;
                Debug.WriteLine("Feature type: " + geomType);

                // Ligne simple
                if (geomType == GeometryType.LineString)
                {
                    LineString ls = (LineString)feature.Geometry;
                    foreach (var point in ls)
                    {
                        tab.Add(new Coord(point.X, point.Y));
                        Debug.WriteLine($"Point ajouté: X={point.X}, Y={point.Y}");
                    }
                }

                // Multi-line
                if (geomType == GeometryType.MultiLineString)
                {
                    MultiLineString mls = (MultiLineString)feature.Geometry;

                    foreach (LineString line in mls) // mls=IEnumerable<LineString>
                    {
                        foreach (var point in line) // line=LineString
                        {
                            tab.Add(new Coord(point.X, point.Y));
                            Debug.WriteLine($"Point ajouté: X={point.X}, Y={point.Y}");
                        }
                    }
                }



                // Point isolé (trkpt)
                if (geomType == GeometryType.Point)
                {
                    Aspose.Gis.Geometries.Point pt = (Aspose.Gis.Geometries.Point)feature.Geometry;
                    tab.Add(new Coord(pt.X, pt.Y));
                    Debug.WriteLine($"Point ajouté: X={pt.X}, Y={pt.Y}");
                }
            }
        }
    }
}
