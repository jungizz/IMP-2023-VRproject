using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PropMaker;

namespace PropMaker
{
    public abstract class Prop : MonoBehaviour
    {

        //Editor variables
        [HideInInspector] public int editorTab = 0; //Curretly selected tab.
        [HideInInspector] public bool autoUpdate = true; //Call UpdateBridge() when a change is made?
        [HideInInspector] public bool hasLoaded = false; //Used to update the bridge from the editor when first created. 
        [HideInInspector] public int seed = 0;
        [HideInInspector] public int vertexCount = 0;
        [HideInInspector] public int triangleCount = 0;


        //References
        public MeshFilter meshFilter { get; protected set; }
        public MeshCollider meshCollider { get; protected set; }
        public MeshRenderer meshRenderer { get; protected set; }

        //Mesh Data
        public Mesh mesh { get; protected set; }
        public List<Vector3> vertices { get; protected set; } = new List<Vector3>(); //All vertices in the mesh.
        protected List<Vector3> normals = new List<Vector3>(); //All normal vectors.
        protected List<Vector2> uvs = new List<Vector2>(); //UV0.
        public string meshName;
        public abstract void Clear();
        protected abstract void SetMesh();

        /// <summary>
        /// Makes sure none of the references is null.
        /// </summary>
        protected virtual void GetReferences()
        {
            if (meshFilter == null)
                meshFilter = GetComponent<MeshFilter>();
            if (meshRenderer == null)
                meshRenderer = GetComponent<MeshRenderer>();
            if (meshCollider == null)
                meshCollider = GetComponent<MeshCollider>();

        }

        /// <summary>
        /// Connects two circles into a cylinder.
        /// </summary>
        /// <param name="circleA"></param>
        /// <param name="circleB"></param>
        /// <param name="tris"></param>
        /// <param name="uvStart"></param>
        /// <param name="uvEnd"></param>
        /// <param name="flipTris"></param>
        protected void ConnectCircles(Vector3[] circleA, Vector3[] circleB, List<int> tris, float uvStart = 0, float uvEnd = 1, bool flipTris = false)
        {

            int normalSign = flipTris ? -1 : 1;

            if (circleA.Length != circleB.Length) return;

            int vert = vertices.Count;

            vertices.AddRange(circleA);

            Vector3 centerA = Vector3.zero;

            for (int i = 0; i < circleA.Length; i++)
                centerA += circleA[i];

            centerA /= circleA.Length;




            vertices.AddRange(circleB);


            Vector3 centerB = Vector3.zero;

            for (int i = 0; i < circleB.Length; i++)
                centerB += circleB[i];

            centerB /= circleB.Length;


            Vector3 up = (centerB - centerA).normalized;

            if (centerA.y > centerB.y)
                up = -up;

            float radiusA = Vector3.Distance(circleA[0], centerA);
            float radiusB = Vector3.Distance(circleB[0], centerB);



            if (centerA != centerB)
            {
                float a = Vector3.Distance(centerA, centerB);
                float c = Vector3.Distance(circleA[0], circleB[0]);
                float Ө = 90 - Mathf.Rad2Deg * Mathf.Asin(a / c);
                if (Ө == float.NaN)
                    Ө = 0;




                if (radiusB > radiusA)
                    Ө = -Ө;

                for (int i = 0; i < circleA.Length; i++)
                {
                    Vector3 normal = (circleA[i] - centerA).normalized;


                    //The radius calculation has a small marginal of error that this if check takes into account.
                    if (Mathf.Abs(radiusA - radiusB) > 0.001f)
                    {
                        Vector3 side = Vector3.Cross(normal, up);
                        normal = Quaternion.AngleAxis(Ө, side) * normal;
                    }

                    normals.Add(normal * normalSign);
                }

                for (int i = 0; i < circleB.Length; i++)
                {
                    Vector3 normal = (circleB[i] - centerB).normalized;


                    //The radius calculation has a small marginal of error that this if check takes into account.
                    if (Mathf.Abs(radiusA - radiusB) > 0.001f)
                    {
                        Vector3 side = Vector3.Cross(normal, up);
                        normal = Quaternion.AngleAxis(Ө, side) * normal;
                    }

                    normals.Add(normal * normalSign);
                }


            }

            else
            {
                for (int i = 0; i < circleA.Length + circleB.Length; i++)
                {
                    if (radiusA > radiusB)
                        normals.Add(up * normalSign);
                    else
                        normals.Add(-up * normalSign);
                }
            }


            for (int i = 0; i < circleA.Length - 1; i++)
            {
                if (!flipTris)
                {
                    tris.Add(vert);
                    tris.Add(vert + 1);
                    tris.Add(vert + circleA.Length);

                    tris.Add(vert + 1);
                    tris.Add(vert + circleA.Length + 1);
                    tris.Add(vert + circleA.Length);
                }

                else
                {
                    tris.Add(vert + circleA.Length);
                    tris.Add(vert + 1);
                    tris.Add(vert);

                    tris.Add(vert + circleA.Length);
                    tris.Add(vert + circleA.Length + 1);
                    tris.Add(vert + 1);
                }

                vert++;
            }


            vertices.Add(circleA[0]); //count -1
            normals.Add(normals[normals.Count - circleA.Length]);
            vertices.Add(circleB[0]); //count -2
            normals.Add(normals[normals.Count - circleB.Length - 1]);

            if (!flipTris)
            {
                tris.Add(vertices.Count - 1);
                tris.Add(vertices.Count - 3);
                tris.Add(vertices.Count - 2);

                tris.Add(vertices.Count - 2);
                tris.Add(vertices.Count - 3);
                tris.Add(vertices.Count - 3 - circleA.Length);
            }

            else
            {
                tris.Add(vertices.Count - 2);
                tris.Add(vertices.Count - 3);
                tris.Add(vertices.Count - 1);

                tris.Add(vertices.Count - 3 - circleA.Length);
                tris.Add(vertices.Count - 3);
                tris.Add(vertices.Count - 2);
            }

            for (int i = 0; i < circleA.Length; i++)
            {
                uvs.Add(new Vector2((float)i / circleA.Length, uvStart));
            }

            for (int i = 0; i < circleB.Length; i++)
            {
                uvs.Add(new Vector2((float)i / circleB.Length, uvEnd));
            }

            uvs.Add(new Vector2(1, uvStart));
            uvs.Add(new Vector2(1, uvEnd));



        }


        /// <summary>
        /// Creates a plane of vertices with an even uv-tiling.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="size"></param>
        /// <param name="xDir"></param>
        /// <param name="yDir"></param>
        /// <param name="tiling"></param>
        /// <param name="tris"></param>
        /// <param name="flipTris"></param>
        /// <param name="invertUVX"></param>
        /// <param name="invertUVY"></param>
        protected void CreatePlane(Vector3 startPoint, Vector2 size, Vector3 xDir, Vector3 yDir, Vector2 tiling, List<int> tris, bool flipTris = false, bool invertUVX = false, bool invertUVY = false)
        {
            float xSections = (size.x / tiling.x).Floor();
            float ySections = (size.y / tiling.y).Floor();
            float endUVX = (size.x - tiling.x * xSections) / tiling.x;
            float endUVY = (size.y - tiling.y * ySections) / tiling.y;


            int vertCount;

            int[] trisToAdd;


            Vector3 normal = Vector3.Cross(xDir, yDir);

            if (flipTris)
                normal = -normal;


            for (int x = 0; x < xSections; x++)
            {
                for (int y = 0; y < ySections; y++)
                {
                    vertCount = vertices.Count;
                    vertices.Add(startPoint + yDir * tiling.y * y + xDir * tiling.x * x);
                    vertices.Add(startPoint + yDir * tiling.y * y + xDir * tiling.x * (x + 1));
                    vertices.Add(startPoint + yDir * tiling.y * (y + 1) + xDir * tiling.x * x);
                    vertices.Add(startPoint + yDir * tiling.y * (y + 1) + xDir * tiling.x * (x + 1));
                    trisToAdd = new int[] { vertCount, vertCount + 1, vertCount + 2, vertCount + 1, vertCount + 3, vertCount + 2 };

                    if (flipTris)
                        trisToAdd = trisToAdd.ReverseOrder();

                    tris.AddRange(trisToAdd);
                    uvs.AddRange(new Vector2[] {
                        new Vector2(invertUVX ? 1 : 0, invertUVY ? 1 : 0),
                        new Vector2(invertUVX ? 0 : 1, invertUVY ? 1 : 0),
                        new Vector2(invertUVX ? 1 : 0, invertUVY ? 0 : 1),
                        new Vector2(invertUVX ? 0 : 1, invertUVY ? 0 : 1)});

                    for (int i = 0; i < 4; i++)
                        normals.Add(normal);
                }

                vertCount = vertices.Count;
                vertices.Add(startPoint + yDir * tiling.y * ySections + xDir * tiling.x * x);
                vertices.Add(startPoint + yDir * tiling.y * ySections + xDir * tiling.x * (x + 1));
                vertices.Add(startPoint + yDir * size.y + xDir * tiling.x * x);
                vertices.Add(startPoint + yDir * size.y + xDir * tiling.x * (x + 1));
                trisToAdd = new int[] { vertCount, vertCount + 1, vertCount + 2, vertCount + 1, vertCount + 3, vertCount + 2 };

                if (flipTris)
                    trisToAdd = trisToAdd.ReverseOrder();

                tris.AddRange(trisToAdd);
                uvs.AddRange(new Vector2[] {
                        new Vector2(invertUVX ? 1 : 0, invertUVY ? 1: 0),
                        new Vector2(invertUVX ? 0 : 1, invertUVY ? 1: 0),
                        new Vector2(invertUVX ? 1 : 0, invertUVY ? 1 - endUVY : endUVY),
                        new Vector2(invertUVX ? 0 : 1, invertUVY ? 1 - endUVY: endUVY)});
                //uvs.AddRange(new Vector2[] { Vector2.zero, Vector2.right, new Vector2(0, endUVY), new Vector2(1, endUVY) });

                for (int i = 0; i < 4; i++)
                    normals.Add(normal);
            }

            for (int y = 0; y < ySections; y++)
            {
                vertCount = vertices.Count;
                vertices.Add(startPoint + yDir * tiling.y * y + xDir * tiling.x * xSections);
                vertices.Add(startPoint + yDir * tiling.y * y + xDir * size.x);
                vertices.Add(startPoint + yDir * tiling.y * (y + 1) + xDir * tiling.x * xSections);
                vertices.Add(startPoint + yDir * tiling.y * (y + 1) + xDir * size.x);

                trisToAdd = new int[] { vertCount, vertCount + 1, vertCount + 2, vertCount + 1, vertCount + 3, vertCount + 2 };

                if (flipTris)
                    trisToAdd = trisToAdd.ReverseOrder();

                tris.AddRange(trisToAdd);
                uvs.AddRange(new Vector2[] {
                        new Vector2(invertUVX ? endUVX : 0, invertUVY ? 1 : 0),
                        new Vector2(invertUVX ? 0 : endUVX, invertUVY ? 1 : 0),
                        new Vector2(invertUVX ? endUVX : 0, invertUVY ? 0 : 1),
                        new Vector2(invertUVX ? 0 : endUVX, invertUVY ? 0 : 1)});

                for (int i = 0; i < 4; i++)
                    normals.Add(normal);
            }

            vertCount = vertices.Count;
            vertices.Add(startPoint + yDir * tiling.y * ySections + xDir * tiling.x * xSections);
            vertices.Add(startPoint + yDir * tiling.y * ySections + xDir * size.x);
            vertices.Add(startPoint + yDir * size.y + xDir * tiling.x * xSections);
            vertices.Add(startPoint + yDir * size.y + xDir * size.x);

            trisToAdd = new int[] { vertCount, vertCount + 1, vertCount + 2, vertCount + 1, vertCount + 3, vertCount + 2 };

            if (flipTris)
                trisToAdd = trisToAdd.ReverseOrder();

            tris.AddRange(trisToAdd);
            uvs.AddRange(new Vector2[] {
                        new Vector2(invertUVX ? endUVX : 0, invertUVY ? 1 : 0),
                        new Vector2(invertUVX ? 0 : endUVX, invertUVY ? 1: 0),
                        new Vector2(invertUVX ? endUVX : 0, invertUVY ? 1 - endUVY  : endUVY),
                        new Vector2(invertUVX ? 0 : endUVX, invertUVY ? 1 - endUVY : endUVY)});

            for (int i = 0; i < 4; i++)
                normals.Add(normal);


        }


    }
}
