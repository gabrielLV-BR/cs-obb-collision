using System;
using System.Collections.Generic;
using Raylib_cs;
using System.Numerics;
using System.Linq;

using OBBAlgorithm.interfaces;
using OBBAlgorithm.utils;
using System.Diagnostics;

namespace OBBAlgorithm
{
    public class AxisAlignedBoundingBox
    {
        public static bool IsColliding(Rectangle rectA, Rectangle rectB)
        {
            return (
                rectA.x < rectB.x + rectB.width &&
                rectA.x + rectA.width > rectB.x &&
                rectA.y < rectB.y + rectB.height &&
                rectA.y + rectA.height > rectB.y
            );
        }
    }

    public class OrientedBoundingBox
    {

        public static bool IsColliding(ISmartObject rectA, ISmartObject rectB)
        {
            // Pega os vértices dos retângulos
            var pointsA = rectA.GetVertices();
            var pointsB = rectB.GetVertices();

            Debug.DrawPoints(pointsA);
            Debug.DrawPoints(pointsB);

            // Pega todas as normais. Pra isso, ele loopa por todas as arestas
            // do rectA junto com as do rectB, e pega o vetor perpendicular à
            // normal (que é o vetor apontando de um vértice pro outro, ou seja, uma aresta)
            // Essa sintaxe é mto foda, parece uma query SQL, mas são só chamadas
            //  à métodos q as listas do C# provêm
            var normals =
                from normal in rectA.GetEdges().Concat(rectB.GetEdges())
                select VectorUtils.Perpendicular(normal);

            // Para cada normal...
            foreach (var normal in normals)
            {
                double min1, min2;
                double max1, max2;

                min1 = min2 = double.PositiveInfinity;
                max1 = max2 = double.NegativeInfinity;

                foreach (var point in pointsA)
                {

                    // O Vector2.Dot retorna o produto escalar de dois vetores,
                    // que é como se tu pegasse um vetor A, projetasse ele em outro
                    // vetor B e retornasse a distância entre a origem de B até a
                    // ponta da projeção de A

                    // Eu guardo a mínima e a máxima dessas projeções porque assim
                    // consigo comparar e ver se existe intersecção

                    double projected = Vector2.Dot(point, normal);

                    min1 = Math.Min(min1, projected);
                    max1 = Math.Max(max1, projected);
                }

                foreach (var point in pointsB)
                {
                    // Faço a mesma coisa pros vértices do rectB
                    double projected = Vector2.Dot(point, normal);

                    min2 = Math.Min(min2, projected);
                    max2 = Math.Max(max2, projected);
                }

                // Verifico se os intervalos se intersectam
                if (!MathUtils.Overlaps(min1, max1, min2, max2))
                {
                    // Se não, quer dizer que nesse eixo não há colisão,
                    // o que significa que eles não estão se tocando
                    // Recomendo pesquisar pelo Teorema de Separação de Eixos,
                    // que é a base  desse algoritmo  
                    return false;
                }
                // Se não se intersectaram, verificamos todos os intervalos pra ter certeza
            }
            // Se chegamos até aqui, todos os eixos apresentaram intersecção e, portanto,
            // sabemos que as formas estão colidindo
            return true;
        }
    }

    public class Debug
    {
        public static void DrawPoints(IEnumerable<Vector2> points, float radius = 5f)
        {
            foreach (var point in points)
            {
                Raylib.DrawCircle((int)point.X, (int)point.Y, radius, Color.GREEN);
            }
        }
    }

}