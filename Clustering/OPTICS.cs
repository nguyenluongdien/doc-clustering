using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clustering
{
    public class OPTICS
    {
        public void OPTICS(List<Object> setOfObject,int eps, int MinPts)
        {
            int n = setOfOject.Count;
            for(int i = 0; i < n; i++)
            {
                Object obj = setOfObject[i];
                ExpandCluster(setOfObject, obj, eps, MinPts);
            }
        }
        
        private void ExpandCluster(List<Object> setOfObject, Object obj,int eps, int MinPts)
        {
            List<Object> lstNeighbor = obj.getNeighbor(setOfObject, eps);
            obj.process = true;
            obj.setCoreDist(lstNeighbor, eps, MinPts);

            if(obj.coreDist != 0)
            {
                // Tạo Seed mới
                Seeds seeds = new Seeds();
                updateSeeds(lstNeighbor, obj, seeds, eps, MinPts);

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

        private void updateSeeds(List<Object> lstNeighbor, Object centreObj, Seeds seeds, int eps, int MinPts)
        {
            float coreDist = centreObject.setCoreDist();
            int num = lstNeighbor.Count;
            for(int i = 0; i < num; i++)
            {
                Object obj = lstNeighbor[i];
                if(!obj.process)
                {
                    float dist = centreObj.dist(obj);
                    float tempCoreReach = dist > coreDist ? dist : coreDist;

                    if(obj.coreReachibility == -1)
                    {
                        obj.coreReachibility = tempCoreReach;
                        seeds.Insert(obj);
                    }
                    else
                        if (tempCoreReach < obj.coreReachibility)
                        {
                            seeds.moveup(obj, tempCoreReach);
                        }
                }
            }
        }

    }


    private class Object
    {
        public float coreDist = 0; // Khoảng cách lõi. 
        public float coreReachibility = -1; // Khoảng cách tới điểm p (core-object). Giá trị -1 = UNDIFINED
        public bool process = false; // Đánh dấu đã kiểm chưa?

        // Vector đặc trưng / thuoc tính bla bla
        // Đọc file lên lấy property ra làm luôn.
        public int[] Vector;
        public int NumKey;

        public Object()
        {

        }

        void setCoreDist(List<Object> neibor, int Eps, int MinPts)
        {
            return 0;
        }

        // Định nghĩa hàm gán (=) giữa 2 OBject
        void Assigned(Object obj)
        {

        }

        // Tìm các láng giềng gần (trong bán kính eps)
        List<Object> getNeighbor(List<Object> setOfObject, int eps)
        {
            List<Object> lstNeighbor = new List<Object>();

            return lstNeighbor;
        }

        // Tính khoảng cách tới Object khác
        float dist(Object obj)
        {
            return 0.0;
        }
    }

    private class Seeds
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

        public void moveup(Object obj, float tempCoreReach)
        {
            int ind = LstSeeds.IndexOf(obj);
            foreach (Object objTemp in LstSeeds)
            {
                //ind++;
                //if (objTemp.coreReachibility > obj.coreReachibility)
                //{
                //    LstSeeds.Insert(ind, obj);
                //    break;
                //}
            }
        }
    }
}
