using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Clustering
{
    public class OPTICS
    {
        int numObject;
        float[,] matrixDistance;
        List<Object> setOfObject;
        float Eps;
        int MinPts;
        int countLabel = 0;

        public OPTICS(ref List<Item> lstMatrix, float eps, int minPts)
        {
            this.Eps = eps;
            this.MinPts = minPts;
            numObject = lstMatrix.Count;
            matrixDistance = new float[numObject, numObject];
            setOfObject = new List<Object>();
            Run(ref lstMatrix);
        }

        public void Run(ref List<Item> lstMatrix)
        {
            int n = lstMatrix.Count;
            for (int i = 0; i < n; i++)
            {
                matrixDistance[i, i] = 0.0f;
                setOfObject.Add(new Object(i));

                //for (int j = 0; j < i; j++)
                //{
                //    if (matrixDistance[i, j] <= eps)
                //    {
                //        int ind = 0;
                //        foreach (Object obj in setOfObject[i].lstNeighbor)
                //        {
                //            ind++;
                //            if (matrixDistance[i, j] < matrixDistance[i, obj.index])
                //                break;
                //        }

                //        setOfObject[i].lstNeighbor.Insert(ind, setOfObject[j]);
                //    }
                //}

                for (int j = i + 1; j < n; j++)
                {
                    float res = distance(lstMatrix[i].Vector.Tf_idf, lstMatrix[j].Vector.Tf_idf);
                    matrixDistance[i, j] = res;
                    matrixDistance[j, i] = res;
                    //if (res <= eps)
                    //{
                    //    int ind = 0;
                    //    foreach (Object obj in setOfObject[i].lstNeighbor)
                    //    {
                    //        ind++;
                    //        if (res < matrixDistance[i, obj.index])
                    //            break;
                    //    }

                    //    setOfObject[i].lstNeighbor.Insert(ind, setOfObject[j]);
                    //}

                }
            }


            n = setOfObject.Count;
            for(int i = 0; i < n; i++)
            {
                Object obj = setOfObject[i];
                List<Object> lstNeighbor = obj.getNeighbor(matrixDistance, setOfObject, Eps);

                if(obj.process == false)
                {
                    obj.process = true;
                    obj.setCoreDist(matrixDistance, Eps, MinPts);

                    if (obj.coreDist != 0)
                    {
                        // Tạo Seed mới
                        Seeds seeds = new Seeds();
                        updateSeeds(obj.lstNeighbor, obj, seeds);

                        foreach (Object objNeighbor in seeds.LstSeeds)
                        {
                            List<Object> lstNeighborOfObj = objNeighbor.getNeighbor(matrixDistance, setOfObject, Eps);
                            objNeighbor.process = true;
                            if (objNeighbor.coreDist != 0.0)
                            {
                                updateSeeds(lstNeighborOfObj, objNeighbor, seeds);
                            }
                        }
                        if (seeds.LstSeeds.Count > 0)
                        {
                            
                            foreach (Object objCluster in seeds.LstSeeds)
                            {
                                lstMatrix[objCluster.index].TmpLabel = countLabel;
                            }

                            countLabel++;
                        }
                    }
                }
            }
        }

        private void updateSeeds(List<Object> lstNeighbor, Object centreObj, Seeds seeds)
        {
            float coreDist = centreObj.coreDist;
            int num = lstNeighbor.Count;
            for(int i = 0; i < num; i++)
            {
                Object obj = lstNeighbor[i];
                if(obj.process == false)
                {
                    float dist = matrixDistance[centreObj.index, obj.index];
                    float tempCoreReach = dist > coreDist ? dist : coreDist;

                    if(obj.coreReachibility == -1)
                    {
                        obj.coreReachibility = tempCoreReach;
                        seeds.Insert(obj);
                    }
                    else
                        if (tempCoreReach < obj.coreReachibility)
                        {
                            seeds.Moveup(obj, tempCoreReach);
                        }
                }
            }
        }
        // Tính khoảng cách tới Object khác
        private float distance(float[] obj1, float[] obj2)
        {
            int n = obj1.Length;
            float res = 0;

            for (int i = 0; i < n; i++)
            {
                float sub = obj1[i] - obj2[i];
                //if (sub < 0) sub = -sub;
                res += (sub*sub);
            }
            return (float)Math.Sqrt(res);
        }
    }


    public class Object
    {
        public float coreDist = 0; // Khoảng cách lõi. 
        public float coreReachibility = -1; // Khoảng cách tới điểm p (core-object). Giá trị -1 = UNDIFINED
        public bool process = false; // Đánh dấu đã kiểm chưa?

        // Chỉ số trong ma trận khoảng cách.
        public int index;
        // Danh sách láng giềng.
        public List<Object> lstNeighbor;

        public Object(int index)
        {
            this.index = index;
            lstNeighbor = new List<Object>();
        }

        public void setCoreDist(float[,] matrixDistance, float eps, int MinPts)
        {
            if (MinPts > lstNeighbor.Count)
                return;
            coreDist = matrixDistance[index, lstNeighbor[MinPts - 1].index];
        }

        public List<Object> getNeighbor(float[,] matrixDistance, List<Object> setOfObject, float eps)
        {
            if(lstNeighbor.Count == 0)
            {
                int num = matrixDistance.GetLength(0);
                for(int i = 0; i < num; i++)
                {
                    if(matrixDistance[index, i] <= eps)
                    {
                        int ind = 0;

                        foreach(Object obj in lstNeighbor)
                        {
                            if(matrixDistance[index, obj.index] > matrixDistance[index, i])
                            {
                                break;
                            }
                            ind++;
                        }

                        lstNeighbor.Insert(ind, setOfObject[i]);
                    }
                }
            }
            return lstNeighbor;
        }
    }

    public class Seeds
    {
        public List<Object> LstSeeds;

        public Seeds(){
            LstSeeds = new List<Object>();
        }

        public void Insert(Object obj)
        {
            int ind = 0;
            foreach(Object objTemp in LstSeeds)
            {
                if (objTemp.coreReachibility > obj.coreReachibility)
                {
                    break;
                }
                ind++;
            }
            LstSeeds.Insert(ind, obj);
        }

        public void Moveup(Object obj, float tempCoreReach)
        {
            int ind = LstSeeds.IndexOf(obj);
            
            int left = 0;
            int right = LstSeeds.Count;
            int pivot = (right + left) / 2;
            //Chia để trị. Xài Pivot
            do
            {
                if(obj.coreReachibility < LstSeeds[pivot].coreReachibility)
                {
                    right = pivot;
                }
                else if (obj.coreReachibility > LstSeeds[pivot].coreReachibility)
                {
                    left = pivot;
                }
                pivot = (right + left) / 2;
            }while(right - left <= 1);

            obj.coreReachibility = tempCoreReach;
            LstSeeds.Insert(pivot, obj);
        }
    }
}
