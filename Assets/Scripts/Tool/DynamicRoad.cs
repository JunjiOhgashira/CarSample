using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Car
{
    namespace Tool
    {
        public class DynamicRoad : MonoBehaviour
        {
            public GameManager gm;

            private MeshFilter meshFilter;
            private Mesh mesh;
            private List<Vector3> vertices = new List<Vector3>();
            private List<int> triangles = new List<int>();
            public float width;
            private float length = 20;
            private float UnlimitedLength = 1000;
            private float height = 0;
            private float step = 0.5f;
            private float angle = 90;
            private float radius = 20;

            private bool StraightMode = true;
            private bool PreStraightMode = true;
            private float StraightInnerProduct = 0;
            private float StraightPreInnerProduct = 0;
            private float CurveInnerProduct = 0;
            private float CurvePreInnerProduct = 0;
            private Vector3 CurveEnd = new Vector3(0, 0, 0);
            private float StraightLength = 2000;
            private float UnknownLength = 15;

            private List<Vector3> Coordinates = new List<Vector3>();
            private List<float> Angles = new List<float>();
            private int index = 0;

            Vector3 v_out;
            Vector3 v_in;

            public float DegToRad(float angle)
            {
                return (float)(angle * Mathf.PI / 180);
            }

            public void StraightLine(float l)
            {
                v_out = new Vector3(
                    width / 2 * Mathf.Cos(DegToRad(Angles.Last())),
                    height,
                    width / 2 * Mathf.Sin(DegToRad(Angles.Last()))) + Coordinates.Last();

                vertices.Add(v_out);

                v_in = new Vector3(
                    width / 2 * Mathf.Cos(DegToRad(180 + Angles.Last())),
                    height,
                    width / 2 * Mathf.Sin(DegToRad(180 + Angles.Last()))) + Coordinates.Last();

                vertices.Add(v_in);

                v_out = new Vector3(
                    width / 2 * Mathf.Cos(DegToRad(Angles.Last())) + l * Mathf.Cos(DegToRad(90 + Angles.Last())),
                    height,
                    width / 2 * Mathf.Sin(DegToRad(Angles.Last())) + l * Mathf.Sin(DegToRad(90 + Angles.Last()))) + Coordinates.Last();

                vertices.Add(v_out);

                v_in = new Vector3(
                    width / 2 * Mathf.Cos(DegToRad(180 + Angles.Last())) + l * Mathf.Cos(DegToRad(90 + Angles.Last())),
                    height,
                    width / 2 * Mathf.Sin(DegToRad(180 + Angles.Last())) + l * Mathf.Sin(DegToRad(90 + Angles.Last()))) + Coordinates.Last();

                vertices.Add(v_in);

                triangles.Add(index + 0);
                triangles.Add(index + 1);
                triangles.Add(index + 2);
                triangles.Add(index + 2);
                triangles.Add(index + 1);
                triangles.Add(index + 3);
            }

            public void CounterClockWise(float a)
            {
                for (int i = 0; i <= (int)(a / step); i++)
                {
                    v_out = new Vector3(
                        radius * Mathf.Cos(DegToRad(Angles.Last() + 180)) + (radius + width / 2) * Mathf.Cos(DegToRad(Angles.Last() + (float)(step * i))),
                        height,
                        radius * Mathf.Sin(DegToRad(Angles.Last() + 180)) + (radius + width / 2) * Mathf.Sin(DegToRad(Angles.Last() + (float)(step * i)))) + Coordinates.Last();

                    vertices.Add(v_out);

                    v_in = new Vector3(
                        radius * Mathf.Cos(DegToRad(Angles.Last() + 180)) + (radius - width / 2) * Mathf.Cos(DegToRad(Angles.Last() + (float)(step * i))),
                        height,
                        radius * Mathf.Sin(DegToRad(Angles.Last() + 180)) + (radius - width / 2) * Mathf.Sin(DegToRad(Angles.Last() + (float)(step * i)))) + Coordinates.Last();

                    vertices.Add(v_in);
                }

                for (int i = 0; i < (int)(a / step) * 2; i += 2)
                {
                    triangles.Add(index + i + 0);
                    triangles.Add(index + i + 1);
                    triangles.Add(index + i + 2);
                    triangles.Add(index + i + 2);
                    triangles.Add(index + i + 1);
                    triangles.Add(index + i + 3);
                }

                index = index + (int)(a / step) * 2 + 2;
                CurveEnd = Coordinates.Last() + new Vector3(
                    radius * Mathf.Cos(DegToRad(180 + Angles.Last())) + radius * Mathf.Cos(DegToRad(a + Angles.Last())),
                    height,
                    radius * Mathf.Sin(DegToRad(180 + Angles.Last())) + radius * Mathf.Sin(DegToRad(a + Angles.Last())));
                Coordinates.Add(CurveEnd);
                Angles.Add(Angles.Last() + a);
            }

            public void ClockWise(float a)
            {
                for (int i = 0; i <= (int)(a / step); i++)
                {
                    v_out = new Vector3(
                        radius * Mathf.Cos(DegToRad(Angles.Last())) + (radius + width / 2) * Mathf.Cos(DegToRad(180 + Angles.Last() - (float)(step * i))),
                        height,
                        radius * Mathf.Sin(DegToRad(Angles.Last())) + (radius + width / 2) * Mathf.Sin(DegToRad(180 + Angles.Last() - (float)(step * i)))) + Coordinates.Last();

                    vertices.Add(v_out);

                    v_in = new Vector3(
                        radius * Mathf.Cos(DegToRad(Angles.Last())) + (radius - width / 2) * Mathf.Cos(DegToRad(180 + Angles.Last() - (float)(step * i))),
                        height,
                        radius * Mathf.Sin(DegToRad(Angles.Last())) + (radius - width / 2) * Mathf.Sin(DegToRad(180 + Angles.Last() - (float)(step * i)))) + Coordinates.Last();

                    vertices.Add(v_in);
                }

                for (int i = 0; i < (int)(a / step) * 2; i += 2)
                {
                    triangles.Add(index + i + 0);
                    triangles.Add(index + i + 2);
                    triangles.Add(index + i + 1);
                    triangles.Add(index + i + 1);
                    triangles.Add(index + i + 2);
                    triangles.Add(index + i + 3);
                }

                index = index + (int)(a / step) * 2 + 2;
                CurveEnd = Coordinates.Last() + new Vector3(
                    radius * Mathf.Cos(DegToRad(Angles.Last())) + radius * Mathf.Cos(DegToRad(180 - a + Angles.Last())),
                    height,
                    radius * Mathf.Sin(DegToRad(Angles.Last())) + radius * Mathf.Sin(DegToRad(180 - a + Angles.Last())));
                Coordinates.Add(CurveEnd);
                Angles.Add(Angles.Last() - a);
            }

            public void DrawStraight()
            {
                index = 0;
                vertices.Clear();
                triangles.Clear();

                meshFilter = gameObject.GetComponent<MeshFilter>();
                mesh = new Mesh();

                StraightLine(UnlimitedLength);

                mesh.SetVertices(vertices);
                mesh.SetTriangles(triangles, 0);
                meshFilter.mesh = mesh;
            }

            public void DrawCurve()
            {
                index = 0;
                vertices.Clear();
                triangles.Clear();

                meshFilter = gameObject.GetComponent<MeshFilter>();
                mesh = new Mesh();

                StraightLine(StraightLength);
                index = index + 4;
                Coordinates.Add(Coordinates.Last() + new Vector3(
                    StraightLength * Mathf.Cos(DegToRad(90 + Angles.Last())),
                    height,
                    StraightLength * Mathf.Sin(DegToRad(90 + Angles.Last()))));

                float angle = Random.Range(60.0f, 120.0f);

                if (Mathf.CeilToInt(angle) % 2 == 0)
                {
                    CounterClockWise(angle);
                }
                else
                {
                    ClockWise(angle);
                }

                StraightLine(UnlimitedLength);

                mesh.SetVertices(vertices);
                mesh.SetTriangles(triangles, 0);
                meshFilter.mesh = mesh;
            }

            void Start()
            {
                Coordinates.Add(new Vector3(0, 0, 0));
                Angles.Add(0);
                DrawStraight();
            }

            void FixedUpdate()
            {
                var StraightRegion = Coordinates.Last() + new Vector3(
                    UnknownLength * Mathf.Cos(DegToRad(90 + Angles.Last())),
                    height,
                    UnknownLength * Mathf.Sin(DegToRad(90 + Angles.Last())));

                StraightInnerProduct = Mathf.Cos(DegToRad(90 + Angles.Last())) * ((float)(gm.pys) - StraightRegion.x)
                                     + Mathf.Sin(DegToRad(90 + Angles.Last())) * ((float)(gm.pxs) - StraightRegion.z);

                CurveInnerProduct = Mathf.Cos(DegToRad(90 + Angles.Last())) * ((float)(gm.pys) - CurveEnd.x)
                                  + Mathf.Sin(DegToRad(90 + Angles.Last())) * ((float)(gm.pxs) - CurveEnd.z);

                if (StraightMode == true)
                {
                    if (StraightPreInnerProduct < 0 && StraightInnerProduct > 0)
                    {
                        DrawCurve();
                        StraightMode = false;
                    }
                }

                if(StraightMode == false)
                {
                    if(CurvePreInnerProduct < 0 && CurveInnerProduct > 0)
                    {
                        DrawStraight();
                        StraightMode = true;
                    }
                }

                StraightPreInnerProduct = StraightInnerProduct;
                CurvePreInnerProduct = CurveInnerProduct;
            }
        }
    }
}