using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Clustering
{
    public class OPTICS
    {
        const int numObject = 500;
        float[,] matrixDistance = new float[numObject, numObject];
        List<Object> setOfObject;
        int eps;
        int minPts;

        public OPTICS(List<Item> lstMatrix, int eps, int minPts)
        {
            this.eps = eps;
            this.minPts = minPts;
            List<Object> setOfObject = new List<Object>();

            int n = lstMatrix.Count;
            for (int i = 0; i < n; i++)
            {
                matrixDistance[i][i] = 0.0;
                setOfObject.Add(new Object(i));

                for(int j  = 0; j < i; j++)
                {
                    if(matrixDistance[i][j] != 0.0 && matrixDistance[i][j] <= eps)
                    {
                        int ind = 0;
                        foreach (Object obj in lstNeighbor)
                        {
                            ind++;
                            if (res < matrixDistance[i][obj.index])
                                break;
                        }

                        setOfObject[i].lstNeighbor.Insert(ind, setOfObject[j]);
                    }
                }

                for (int j = i + 1; j < n; j++)
                {
                    float res = distance(lstMatrix[i].DocVector.Tf_idf, lstMatrix[j].DocVector.Tf_idf);
                    matrixDistance[i][j] = res;
                    matrixDistance[j][i] = res;
                    if(res <= eps)
                    {
                        int ind = 0;
                        foreach(Object obj in lstNeighbor)
                        {
                            ind++;
                            if (res < matrixDistance[i][obj.index])
                                break;
                        }

                        setOfObject[i].lstNeighbor.Insert(ind, setOfObject[j]);
                    }

                }
            }

            Run(setOfObject);
        }

        public void Run(List<Object> setOfObject)
        {
            int n = setOfOject.Count;
            for(int i = 0; i < n; i++)
            {
                Object obj = setOfObject[i];
                ExpandCluster(setOfObject, obj);
            }
        }
        
        private void ExpandCluster(List<Object> setOfObject, Object obj)
        {
            List<Object> lstNeighbor = obj.getNeighbor(setOfObject, eps);
            obj.process = true;
            obj.setCoreDist(matrixDistance, eps, MinPts);

            if(obj.coreDist != 0)
            {
                // Tạo Seed mới
                Seeds seeds = new Seeds();
                updateSeeds(lstNeighbor, obj, seeds);

                foreach (Object objNeighbor in seeds.LstSeeds)
                {
                    List<Object> lstNeiborOfObjNeighbor = objNeighbor.getNeighbor(setOfObject, eps);
                    objNeighbor.process = true;
                    if(objNeighbor.coreDist != 0)
                    {
                        updateSeeds(lstNeiborOfObjNeighbor, objNeighbor, seeds, eps,MinPts);
                    }
                }
            }
        }

        private void updateSeeds(List<Object> lstNeighbor, Object centreObj, Seeds seeds)
        {
            float coreDist = centreObject.setCoreDist();
            int num = lstNeighbor.Count;
            for(int i = 0; i < num; i++)
            {
                Object obj = lstNeighbor[i];
                if(!obj.process)
                {
                    float dist = centreObj.distance(obj);
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
                if (sub < 0) sub = -sub;
                res += sub;
            }
            return res;
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

        public void setCoreDist(double[,] matrixDistance, int eps, int MinPts)
        {
            int count = 0;
            if (MinPts > lstNeighbor)
                return;
            coreDist = lstNeighbor[MinPts - 1];
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
                ind++;
                if (objTemp.coreReachibility > obj.coreReachibility)
                {
                    LstSeeds.Insert(ind, obj);
                    break;
                }
            }
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
            LstSeeds.Insert(ind, obj);
        }
    }
}
